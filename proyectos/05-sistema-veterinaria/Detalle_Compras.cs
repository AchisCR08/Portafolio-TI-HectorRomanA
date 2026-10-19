using Entidades;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class Detalle_Compras : Form
    {
        private readonly ComprasRepository _repo;
        private CompraVista? _compraSeleccionada;

        public Detalle_Compras()
        {
            InitializeComponent();
            _repo = new ComprasRepository();

            ConfigurarGrillaCompras();
            ConfigurarGrillaDetalle();

            // Fechas por defecto: último mes
            dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFechaFin.Value    = DateTime.Today;

            // Eventos
            this.Load                    += Detalle_Compras_Load;
            btnBuscar.Click              += BtnBuscar_Click;
            btnLimpiar.Click             += BtnLimpiar_Click;
            dgvCompras.SelectionChanged  += DgvCompras_SelectionChanged;
        }

        // ─────────────────────────────────────────────────────────────────
        // CARGA INICIAL
        // ─────────────────────────────────────────────────────────────────

        private void Detalle_Compras_Load(object? sender, EventArgs e)
        {
            BuscarCompras();
        }

        // ─────────────────────────────────────────────────────────────────
        // CONFIGURACIÓN DE GRILLAS
        // ─────────────────────────────────────────────────────────────────

        private void ConfigurarGrillaCompras()
        {
            dgvCompras.ReadOnly            = true;
            dgvCompras.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            dgvCompras.MultiSelect         = false;
            dgvCompras.AllowUserToAddRows  = false;
            dgvCompras.AutoGenerateColumns = false;
            dgvCompras.RowHeadersVisible   = false;
            dgvCompras.BackgroundColor     = Color.White;
            dgvCompras.BorderStyle         = BorderStyle.None;
            dgvCompras.CellBorderStyle     = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCompras.GridColor           = Color.FromArgb(220, 220, 220);

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CId", HeaderText = "N°",
                DataPropertyName = "IdCompra", Width = 60
            });
            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CFecha", HeaderText = "Fecha",
                DataPropertyName = "Fecha", Width = 100
            });
            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CProv", HeaderText = "Proveedor",
                DataPropertyName = "ProveedorNombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CTotal", HeaderText = "Total ₡",
                DataPropertyName = "Total", Width = 110
            });

            dgvCompras.Columns["CTotal"]!.DefaultCellStyle.Format    = "N2";
            dgvCompras.Columns["CTotal"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCompras.Columns["CFecha"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCompras.Columns["CId"]!.DefaultCellStyle.Alignment    = DataGridViewContentAlignment.MiddleCenter;

            EstilizarEncabezado(dgvCompras);
        }

        private void ConfigurarGrillaDetalle()
        {
            dgvDetalle.ReadOnly            = true;
            dgvDetalle.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.AllowUserToAddRows  = false;
            dgvDetalle.AutoGenerateColumns = false;
            dgvDetalle.RowHeadersVisible   = false;
            dgvDetalle.BackgroundColor     = Color.White;
            dgvDetalle.BorderStyle         = BorderStyle.None;
            dgvDetalle.CellBorderStyle     = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDetalle.GridColor           = Color.FromArgb(220, 220, 220);

            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DProd", HeaderText = "Producto",
                DataPropertyName = "ProductoNombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DCant", HeaderText = "Cantidad",
                DataPropertyName = "Cantidad", Width = 75
            });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DPrecio", HeaderText = "Precio Unit. ₡",
                DataPropertyName = "PrecioUnitario", Width = 120
            });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DSubtotal", HeaderText = "Subtotal ₡",
                DataPropertyName = "Subtotal", Width = 115
            });

            dgvDetalle.Columns["DPrecio"]!.DefaultCellStyle.Format    = "N2";
            dgvDetalle.Columns["DSubtotal"]!.DefaultCellStyle.Format  = "N2";
            dgvDetalle.Columns["DCant"]!.DefaultCellStyle.Alignment   = DataGridViewContentAlignment.MiddleCenter;
            dgvDetalle.Columns["DPrecio"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalle.Columns["DSubtotal"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            EstilizarEncabezado(dgvDetalle);
        }

        private static void EstilizarEncabezado(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 99, 235);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles               = false;
            dgv.ColumnHeadersHeight                     = 32;
        }

        // ─────────────────────────────────────────────────────────────────
        // BÚSQUEDA
        // ─────────────────────────────────────────────────────────────────

        private void BuscarCompras()
        {
            try
            {
                var compras = _repo.ListarCompras(
                    SesionActual.IdSucursal,
                    dtpFechaInicio.Value.Date,
                    dtpFechaFin.Value.Date).ToList();

                dgvCompras.DataSource = compras;
                LimpiarDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar compras:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LimpiarDetalle()
        {
            _compraSeleccionada  = null;
            lblNumCompra.Text    = "Compra N°: —";
            lblFecha.Text        = "Fecha: —";
            lblProveedor.Text    = "Proveedor: —";
            lblSucursal.Text     = "Sucursal: —";
            lblTotal.Text        = "Total: ₡0.00";
            dgvDetalle.DataSource = null;
        }

        // ─────────────────────────────────────────────────────────────────
        // SELECCIÓN DE COMPRA → CARGAR DETALLE
        // ─────────────────────────────────────────────────────────────────

        private void DgvCompras_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCompras.CurrentRow?.DataBoundItem is not CompraVista compra)
                return;

            _compraSeleccionada = compra;

            try
            {
                lblNumCompra.Text = $"Compra N°: {compra.IdCompra:D6}";
                lblFecha.Text     = $"Fecha: {compra.Fecha:dd/MM/yyyy}";
                lblProveedor.Text = $"Proveedor: {compra.ProveedorNombre}";
                lblSucursal.Text  = $"Sucursal: {compra.SucursalNombre}";
                lblTotal.Text     = $"Total: ₡{compra.Total:N2}";

                var lineas = _repo.ObtenerDetalle(compra.IdCompra).ToList();
                dgvDetalle.DataSource = lineas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar detalle:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // BOTONES
        // ─────────────────────────────────────────────────────────────────

        private void BtnBuscar_Click(object? sender, EventArgs e) => BuscarCompras();

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFechaFin.Value    = DateTime.Today;
            BuscarCompras();
        }
    }
}
