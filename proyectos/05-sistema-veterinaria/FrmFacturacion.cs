using Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmFacturacion : Form
    {
        private readonly FacturaRepository _facturaRepo;

        // Lista en memoria de las líneas que el usuario va agregando
        private readonly List<DetalleFacturaLocal> _lineas = new();

        public FrmFacturacion()
        {
            InitializeComponent();
            _facturaRepo = new FacturaRepository();

            // Configurar la grilla ANTES de cualquier evento que intente agregar filas
            ConfigurarGrilla();

            // Conectar eventos de los radio buttons
            rbProducto.CheckedChanged += RbTipo_CheckedChanged;
            rbCitaVet.CheckedChanged += RbTipo_CheckedChanged;
            rbCitaGrooming.CheckedChanged += RbTipo_CheckedChanged;

            // Cuando cambia el item seleccionado en el combo, auto-rellenar precio
            cmbItem.SelectedIndexChanged += CmbItem_SelectedIndexChanged;

            btnAgregarLinea.Click += BtnAgregarLinea_Click;
            btnQuitarLinea.Click += BtnQuitarLinea_Click;
            btnEmitir.Click += BtnEmitir_Click;
            btnLimpiar.Click += BtnLimpiar_Click;

            // Conectar el evento Load para cargar combos y datos al abrir el form
            this.Load += FrmFacturacion_Load;
        }

        private void FrmFacturacion_Load(object sender, EventArgs e)
        {
            CargarDatosIniciales();
        }

        // ─────────────────────────────────────────────────────────────────
        // CARGA INICIAL
        // ─────────────────────────────────────────────────────────────────

        private void CargarDatosIniciales()
        {
            // Sucursal y fecha automáticas desde la sesión
            lblSucursalFactura.Text = SesionActual.NombreSucursal;
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            // Clientes
            var personas = _facturaRepo.ListarPersonas().ToList();
            cmbCliente.DisplayMember = "Nombre";
            cmbCliente.ValueMember = "IdPersona";
            cmbCliente.DataSource = personas;
            cmbCliente.SelectedIndex = -1;

            // Tipos de pago
            var tiposPago = _facturaRepo.ListarTiposPago().ToList();
            cmbTipoPago.DisplayMember = "Nombre";
            cmbTipoPago.ValueMember = "IdTipoPago";
            cmbTipoPago.DataSource = tiposPago;
            cmbTipoPago.SelectedIndex = -1;

            // Seleccionar Producto por defecto
            rbProducto.Checked = true;
        }

        private void ConfigurarGrilla()
        {
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.ReadOnly = true;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.MultiSelect = false;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.RowHeadersVisible = false;
            dgvDetalle.BackgroundColor = Color.White;

            dgvDetalle.Columns.Clear();
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tipo", HeaderText = "Tipo", Width = 90 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Descripcion", HeaderText = "Descripción", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cant.", Width = 60 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Precio", HeaderText = "Precio Unit.", Width = 110 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subtotal", HeaderText = "Subtotal", Width = 110 });
        }

        // ─────────────────────────────────────────────────────────────────
        // CAMBIO DE TIPO (Radio buttons)
        // ─────────────────────────────────────────────────────────────────

        private void RbTipo_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is RadioButton rb && !rb.Checked) return;

            cmbItem.DataSource = null;
            cmbItem.DisplayMember = "";
            cmbItem.ValueMember = "";
            txtCantidad.Clear();
            txtPrecioUnitario.Clear();

            int idSucursal = SesionActual.IdSucursal;

            if (rbProducto.Checked)
            {
                var productos = _facturaRepo.ListarProductosActivos().ToList();
                cmbItem.DisplayMember = "Nombre";
                cmbItem.ValueMember = "IdProducto";
                cmbItem.DataSource = productos;
                txtCantidad.Enabled = true;
            }
            else if (rbCitaVet.Checked)
            {
                var citas = _facturaRepo.ListarCitasVetPendientes(idSucursal).ToList();
                cmbItem.DisplayMember = "Display";
                cmbItem.ValueMember = "IdCitaVet";
                cmbItem.DataSource = citas;
                txtCantidad.Text = "1";
                txtCantidad.Enabled = false;   // siempre 1
            }
            else if (rbCitaGrooming.Checked)
            {
                var citas = _facturaRepo.ListarCitasGroomingPendientes(idSucursal).ToList();
                cmbItem.DisplayMember = "Display";
                cmbItem.ValueMember = "IdCitaGrooming";
                cmbItem.DataSource = citas;
                txtCantidad.Text = "1";
                txtCantidad.Enabled = false;   // siempre 1
            }

            cmbItem.SelectedIndex = -1;
        }

        // ─────────────────────────────────────────────────────────────────
        // AUTO-RELLENO DE PRECIO al seleccionar item
        // ─────────────────────────────────────────────────────────────────

        private void CmbItem_SelectedIndexChanged(object? sender, EventArgs e)
        {
            txtPrecioUnitario.Clear();

            if (cmbItem.SelectedItem == null) return;

            if (rbProducto.Checked && cmbItem.SelectedItem is ProductoConDetalle prod)
            {
                txtPrecioUnitario.Text = prod.PrecioVenta.ToString("0.##");
            }
            else if (rbCitaGrooming.Checked && cmbItem.SelectedItem is CitaGroomingCombo cg)
            {
                txtPrecioUnitario.Text = cg.PrecioFinal.ToString("0.##");
                txtCantidad.Text = "1";
            }
            else if (rbCitaVet.Checked)
            {
                // Las citas vet no tienen precio fijo en el modelo; el usuario lo ingresa
                txtPrecioUnitario.Clear();
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // AGREGAR LÍNEA
        // ─────────────────────────────────────────────────────────────────

        private void BtnAgregarLinea_Click(object? sender, EventArgs e)
        {
            if (cmbItem.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un ítem.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecioUnitario.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio unitario válido mayor a 0.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var linea = new DetalleFacturaLocal { PrecioUnitario = precio };

            if (rbProducto.Checked)
            {
                if (!int.TryParse(txtCantidad.Text, out int cant) || cant <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida mayor a 0.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var prod = (ProductoConDetalle)cmbItem.SelectedItem;
                linea.Tipo = DetalleFacturaLocal.TipoLinea.Producto;
                linea.IdItem = prod.IdProducto;
                linea.Descripcion = prod.Nombre;
                linea.Cantidad = cant;
            }
            else if (rbCitaVet.Checked)
            {
                var cita = (CitaVetCombo)cmbItem.SelectedItem;
                linea.Tipo = DetalleFacturaLocal.TipoLinea.CitaVeterinaria;
                linea.IdItem = cita.IdCitaVet;
                linea.Descripcion = cita.Display;
                linea.Cantidad = 1;
            }
            else if (rbCitaGrooming.Checked)
            {
                var cita = (CitaGroomingCombo)cmbItem.SelectedItem;
                linea.Tipo = DetalleFacturaLocal.TipoLinea.CitaGrooming;
                linea.IdItem = cita.IdCitaGrooming;
                linea.Descripcion = cita.Display;
                linea.Cantidad = 1;
            }

            // Verificar si ya existe la misma cita en la lista (duplicado)
            if (linea.Tipo != DetalleFacturaLocal.TipoLinea.Producto &&
                _lineas.Any(l => l.Tipo == linea.Tipo && l.IdItem == linea.IdItem))
            {
                MessageBox.Show("Esta cita ya fue agregada al detalle.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _lineas.Add(linea);
            RefrescarGrilla();

            // Limpiar campos de línea para la siguiente
            cmbItem.SelectedIndex = -1;
            txtCantidad.Clear();
            txtPrecioUnitario.Clear();
            if (rbProducto.Checked) txtCantidad.Focus();
        }

        // ─────────────────────────────────────────────────────────────────
        // QUITAR LÍNEA
        // ─────────────────────────────────────────────────────────────────

        private void BtnQuitarLinea_Click(object? sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow == null) return;

            int index = dgvDetalle.CurrentRow.Index;
            if (index >= 0 && index < _lineas.Count)
            {
                _lineas.RemoveAt(index);
                RefrescarGrilla();
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // REFRESCAR GRILLA Y TOTAL
        // ─────────────────────────────────────────────────────────────────

        private void RefrescarGrilla()
        {
            dgvDetalle.Rows.Clear();

            foreach (var l in _lineas)
            {
                string tipoStr = l.Tipo switch
                {
                    DetalleFacturaLocal.TipoLinea.Producto => "Producto",
                    DetalleFacturaLocal.TipoLinea.CitaVeterinaria => "Cita Vet",
                    DetalleFacturaLocal.TipoLinea.CitaGrooming => "Grooming",
                    _ => ""
                };

                dgvDetalle.Rows.Add(
                    tipoStr,
                    l.Descripcion,
                    l.Cantidad,
                    $"₡{l.PrecioUnitario:N2}",
                    $"₡{l.Subtotal:N2}");
            }

            decimal total = _lineas.Sum(l => l.Subtotal);
            lblTotal.Text = $"₡{total:N2}";
        }

        // ─────────────────────────────────────────────────────────────────
        // EMITIR FACTURA
        // ─────────────────────────────────────────────────────────────────

        private void BtnEmitir_Click(object? sender, EventArgs e)
        {
            // Validaciones
            if (cmbCliente.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un cliente.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_lineas.Count == 0)
            {
                MessageBox.Show("Agregue al menos una línea al detalle.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTipoPago.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un tipo de pago.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = _lineas.Sum(l => l.Subtotal);

            string resumen = $"Cliente: {cmbCliente.Text}\n" +
                             $"Líneas:  {_lineas.Count}\n" +
                             $"Total:   ₡{total:N2}\n" +
                             $"Pago:    {cmbTipoPago.Text}\n\n" +
                             "¿Confirmar emisión de la factura?";

            if (MessageBox.Show(resumen, "Confirmar factura",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int idFactura = _facturaRepo.EmitirFactura(
                    idPersona: (int)cmbCliente.SelectedValue,
                    idSucursal: SesionActual.IdSucursal,
                    lineas: _lineas,
                    idTipoPago: (int)cmbTipoPago.SelectedValue,
                    montoTotal: total);

                MessageBox.Show(
                    $"✅ Factura #{idFactura} emitida correctamente.\nTotal cobrado: ₡{total:N2}",
                    "Factura emitida",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al emitir la factura:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // LIMPIAR
        // ─────────────────────────────────────────────────────────────────

        private void BtnLimpiar_Click(object? sender, EventArgs e) => LimpiarFormulario();

        private void LimpiarFormulario()
        {
            _lineas.Clear();
            dgvDetalle.Rows.Clear();
            lblTotal.Text = "₡0,00";
            cmbCliente.SelectedIndex = -1;
            cmbTipoPago.SelectedIndex = -1;
            cmbItem.SelectedIndex = -1;
            txtCantidad.Clear();
            txtPrecioUnitario.Clear();
            rbProducto.Checked = true;

            // Recargar citas pendientes (pueden haber cambiado tras facturar)
            RbTipo_CheckedChanged(rbProducto, EventArgs.Empty);
        }

        private void FrmFacturacion_Load_1(object sender, EventArgs e)
        {

        }
    }
}
