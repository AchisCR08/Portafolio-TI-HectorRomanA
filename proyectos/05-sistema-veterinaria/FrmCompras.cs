using Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmCompras : Form
    {
        private readonly ComprasRepository  _comprasRepo;
        private readonly ProductoRepository _productoRepo;

        // Líneas en memoria hasta confirmar el registro
        private readonly List<LineaCompraLocal> _lineas = new();

        // Productos activos completos, para filtrar por proveedor sin recargar
        private List<ProductoConDetalle> _productos = new();

        private bool   _aplicandoFiltro;   // evita recursión al revertir la selección
        private object _proveedorAnterior; // selección previa, para revertir si cancela

        public FrmCompras()
        {
            InitializeComponent();
            _comprasRepo  = new ComprasRepository();
            _productoRepo = new ProductoRepository();

            ConfigurarGrilla();

            // Eventos
            this.Load             += FrmCompras_Load;
            btnAgregarLinea.Click += BtnAgregarLinea_Click;
            btnQuitarLinea.Click  += BtnQuitarLinea_Click;
            btnRegistrar.Click    += BtnRegistrar_Click;
            btnLimpiar.Click      += BtnLimpiar_Click;

            // Autocompletar precio al cambiar producto
            cmbProducto.SelectedIndexChanged += CmbProducto_SelectedIndexChanged;

            // Filtrar productos según el proveedor elegido
            cmbProveedor.SelectedIndexChanged += CmbProveedor_SelectedIndexChanged;
        }

        // ─────────────────────────────────────────────────────────────────
        // CARGA INICIAL
        // ─────────────────────────────────────────────────────────────────

        private void FrmCompras_Load(object? sender, EventArgs e)
        {
            CargarDatosIniciales();
        }

        private void CargarDatosIniciales()
        {
            // Sucursal desde sesión activa
            lblSucursal.Text = SesionActual.NombreSucursal;

            // Fecha por defecto: hoy
            dateTimePicker1.Value = DateTime.Today;

            // Productos activos completos (el combo se filtra por proveedor)
            _productos = _productoRepo.ListarProductos(soloActivos: true).ToList();

            // Cargar proveedores activos
            var proveedores = _productoRepo.ListarProveedoresActivos().ToList();
            cmbProveedor.DisplayMember = "Nombre";
            cmbProveedor.ValueMember   = "IdProveedor";
            cmbProveedor.DataSource    = proveedores;
            cmbProveedor.SelectedIndex = -1;

            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 9999;
            numericUpDown1.Value   = 1;
            txtPrecioUnitario.Text = "";

            ActualizarTotal();
        }

        // ─────────────────────────────────────────────────────────────────
        // CONFIGURACIÓN GRILLA
        // ─────────────────────────────────────────────────────────────────

        private void ConfigurarGrilla()
        {
            dgvLineas.ReadOnly            = true;
            dgvLineas.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            dgvLineas.MultiSelect         = false;
            dgvLineas.AllowUserToAddRows  = false;
            dgvLineas.AutoGenerateColumns = false;
            dgvLineas.RowHeadersVisible   = false;
            dgvLineas.BackgroundColor     = Color.White;
            dgvLineas.BorderStyle         = BorderStyle.None;
            dgvLineas.CellBorderStyle     = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLineas.GridColor           = Color.FromArgb(220, 220, 220);

            dgvLineas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name             = "ColProducto",
                HeaderText       = "Producto",
                DataPropertyName = "ProductoNombre",
                AutoSizeMode     = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvLineas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name             = "ColCantidad",
                HeaderText       = "Cantidad",
                DataPropertyName = "Cantidad",
                Width            = 80
            });
            dgvLineas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name             = "ColPrecio",
                HeaderText       = "Precio Unit. ₡",
                DataPropertyName = "PrecioUnitario",
                Width            = 120
            });
            dgvLineas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name             = "ColSubtotal",
                HeaderText       = "Subtotal ₡",
                DataPropertyName = "Subtotal",
                Width            = 110
            });

            // Formato numérico
            dgvLineas.Columns["ColPrecio"]!.DefaultCellStyle.Format    = "N2";
            dgvLineas.Columns["ColSubtotal"]!.DefaultCellStyle.Format  = "N2";
            dgvLineas.Columns["ColCantidad"]!.DefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleCenter;
            dgvLineas.Columns["ColPrecio"]!.DefaultCellStyle.Alignment    = DataGridViewContentAlignment.MiddleRight;
            dgvLineas.Columns["ColSubtotal"]!.DefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleRight;

            // Estilo encabezado
            dgvLineas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 99, 235);
            dgvLineas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLineas.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvLineas.EnableHeadersVisualStyles               = false;
            dgvLineas.ColumnHeadersHeight                     = 32;
        }

        // ─────────────────────────────────────────────────────────────────
        // AUTOCOMPLETAR PRECIO DESDE EL PRODUCTO SELECCIONADO
        // ─────────────────────────────────────────────────────────────────

        private void CmbProducto_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem is ProductoConDetalle prod)
                // InvariantCulture: evita que el locale español convierta
                // 5000.00 en "5.000,00" causando errores al parsear luego
                txtPrecioUnitario.Text = prod.PrecioVenta.ToString(
                    "F2", System.Globalization.CultureInfo.InvariantCulture);
        }

        // ─────────────────────────────────────────────────────────────────
        // FILTRO DE PRODUCTOS POR PROVEEDOR
        // ─────────────────────────────────────────────────────────────────

        private void CmbProveedor_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_aplicandoFiltro) return;

            // Una compra solo puede tener productos de un único proveedor
            if (_lineas.Count > 0)
            {
                var res = MessageBox.Show(
                    $"Al cambiar de proveedor se descartarán las {_lineas.Count} línea(s) agregadas.\n\n¿Continuar?",
                    "Cambiar proveedor",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (res != DialogResult.Yes)
                {
                    _aplicandoFiltro = true;
                    cmbProveedor.SelectedValue = _proveedorAnterior;
                    _aplicandoFiltro = false;
                    return;
                }
            }

            _proveedorAnterior = cmbProveedor.SelectedValue;
            _lineas.Clear();
            RefrescarGrilla();

            cmbProducto.DataSource = null;

            if (cmbProveedor.SelectedValue is int idProveedor)
            {
                var productos = _productos
                    .Where(p => p.IdProveedor == idProveedor && p.Activo)
                    .ToList();

                cmbProducto.DisplayMember = "Nombre";
                cmbProducto.ValueMember   = "IdProducto";
                cmbProducto.DataSource    = productos;
            }

            LimpiarCamposLinea();
        }

        // ─────────────────────────────────────────────────────────────────
        // AGREGAR LÍNEA
        // ─────────────────────────────────────────────────────────────────

        private void BtnAgregarLinea_Click(object? sender, EventArgs e)
        {
            // Validar producto seleccionado
            if (cmbProducto.SelectedItem is not ProductoConDetalle prod)
            {
                MessageBox.Show("Seleccione un producto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar precio unitario con InvariantCulture para evitar
            // problemas de separadores en locale español
            if (!decimal.TryParse(
                    txtPrecioUnitario.Text.Trim(),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio unitario válido mayor a 0.\nUse punto como separador decimal (ej: 1500.00)",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioUnitario.Focus();
                return;
            }

            int cantidad = (int)numericUpDown1.Value;

            // Si el producto ya está en la lista, actualizar cantidad y precio
            var existente = _lineas.FirstOrDefault(l => l.IdProducto == prod.IdProducto);
            if (existente != null)
            {
                existente.Cantidad       += cantidad;
                existente.PrecioUnitario  = precio; // actualizar precio
            }
            else
            {
                _lineas.Add(new LineaCompraLocal
                {
                    IdProducto     = prod.IdProducto,
                    ProductoNombre = prod.Nombre,
                    Cantidad       = cantidad,
                    PrecioUnitario = precio
                });
            }

            RefrescarGrilla();
            LimpiarCamposLinea();
        }

        // ─────────────────────────────────────────────────────────────────
        // QUITAR LÍNEA SELECCIONADA
        // ─────────────────────────────────────────────────────────────────

        private void BtnQuitarLinea_Click(object? sender, EventArgs e)
        {
            if (dgvLineas.CurrentRow == null) return;

            int idx = dgvLineas.CurrentRow.Index;
            if (idx >= 0 && idx < _lineas.Count)
            {
                _lineas.RemoveAt(idx);
                RefrescarGrilla();
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // REGISTRAR COMPRA
        // ─────────────────────────────────────────────────────────────────

        private void BtnRegistrar_Click(object? sender, EventArgs e)
        {
            // Validar proveedor
            if (cmbProveedor.SelectedItem is not Proveedor prov)
            {
                MessageBox.Show("Seleccione un proveedor.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProveedor.Focus();
                return;
            }

            // Validar que haya al menos una línea
            if (_lineas.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto a la compra.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmar
            decimal total = _lineas.Sum(l => l.Subtotal);
            var res = MessageBox.Show(
                $"¿Confirmar el registro de la compra?\n\n" +
                $"Proveedor: {prov.Nombre}\n" +
                $"Productos: {_lineas.Count} línea(s)\n" +
                $"Total: ₡{total:N2}",
                "Confirmar compra",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            try
            {
                int idCompra = _comprasRepo.RegistrarCompra(
                    fecha:        dateTimePicker1.Value.Date,
                    idProveedor:  prov.IdProveedor,
                    idSucursal:   SesionActual.IdSucursal,
                    lineas:       _lineas);

                MessageBox.Show(
                    $"✅ Compra N° {idCompra:D6} registrada correctamente.\n" +
                    $"El inventario de la sucursal ha sido actualizado.",
                    "Compra registrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la compra:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // LIMPIAR
        // ─────────────────────────────────────────────────────────────────

        private void BtnLimpiar_Click(object? sender, EventArgs e) => LimpiarTodo();

        private void LimpiarTodo()
        {
            // Limpiar líneas ANTES de resetear el proveedor para que el
            // handler de cambio no muestre el diálogo de confirmación
            _lineas.Clear();
            RefrescarGrilla();

            cmbProveedor.SelectedIndex  = -1;
            dateTimePicker1.Value       = DateTime.Today;
            LimpiarCamposLinea();
        }

        private void LimpiarCamposLinea()
        {
            cmbProducto.SelectedIndex  = -1;
            numericUpDown1.Value       = 1;
            txtPrecioUnitario.Text     = "";
        }

        // ─────────────────────────────────────────────────────────────────
        // HELPERS
        // ─────────────────────────────────────────────────────────────────

        private void RefrescarGrilla()
        {
            // Forzar rebind completo para que Subtotal calculado se refresque
            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = _lineas.ToList();
            dgvLineas.DataSource     = bindingSource;
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            decimal total  = _lineas.Sum(l => l.Subtotal);
            lblTotal.Text  = $"₡{total:N2}";
        }
    }
}
