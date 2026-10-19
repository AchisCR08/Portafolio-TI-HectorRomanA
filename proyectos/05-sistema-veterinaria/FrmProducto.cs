using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmProducto : Form
    {
        private readonly ProductoRepository _productoRepo;
        private int? _currentIdProducto = null;

        public FrmProducto()
        {
            InitializeComponent();
            _productoRepo = new ProductoRepository();

            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;

            CargarCombos();
            CargarGrilla();
            LimpiarCampos();
            dgvProductos.ClearSelection();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
        }

        private void CargarCombos()
        {
            var categorias = _productoRepo.ListarCategoriasActivas().ToList();
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "IdCategoria";
            cmbCategoria.DataSource = categorias;
            cmbCategoria.SelectedIndex = -1;

            var proveedores = _productoRepo.ListarProveedoresActivos().ToList();
            cmbProveedor.DisplayMember = "Nombre";
            cmbProveedor.ValueMember = "IdProveedor";
            cmbProveedor.DataSource = proveedores;
            cmbProveedor.SelectedIndex = -1;
        }

        private void CargarGrilla()
        {
            dgvProductos.DataSource = _productoRepo.ListarProductos(true).ToList();

            string[] columnasOcultas = { "IdProducto", "IdCategoria", "IdProveedor", "Descripcion" };
            foreach (var col in columnasOcultas)
                if (dgvProductos.Columns[col] != null)
                    dgvProductos.Columns[col].Visible = false;

            if (dgvProductos.Columns["Nombre"] != null)
                dgvProductos.Columns["Nombre"].HeaderText = "Producto";
            if (dgvProductos.Columns["PrecioVenta"] != null)
                dgvProductos.Columns["PrecioVenta"].HeaderText = "Precio";
            if (dgvProductos.Columns["CategoriaNombre"] != null)
                dgvProductos.Columns["CategoriaNombre"].HeaderText = "Categoria";
            if (dgvProductos.Columns["ProveedorNombre"] != null)
                dgvProductos.Columns["ProveedorNombre"].HeaderText = "Proveedor";
            if (dgvProductos.Columns["Activo"] != null)
                dgvProductos.Columns["Activo"].HeaderText = "Activo";

            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LimpiarCampos()
        {
            _currentIdProducto = null;
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            checkBox1.Checked = true;
        }

        private void CargarDatosProducto(int idProducto)
        {
            var producto = _productoRepo.ObtenerProductoPorId(idProducto);
            if (producto == null) return;

            _currentIdProducto = producto.IdProducto;
            txtNombre.Text = producto.Nombre;
            txtDescripcion.Text = producto.Descripcion;
            txtPrecio.Text = producto.PrecioVenta.ToString("0.##");
            cmbCategoria.SelectedValue = producto.IdCategoria;
            cmbProveedor.SelectedValue = producto.IdProveedor;
            checkBox1.Checked = producto.Activo;
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            dgvProductos.ClearSelection();
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal precioVenta) || precioVenta <= 0)
            {
                MessageBox.Show("Debe ingresar un precio valido mayor a 0.");
                return;
            }

            if (cmbCategoria.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una categoria.");
                return;
            }

            if (cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un proveedor.");
                return;
            }

            int idCategoria = (int)cmbCategoria.SelectedValue;
            int idProveedor = (int)cmbProveedor.SelectedValue;

            try
            {
                if (_currentIdProducto == null)
                {
                    _productoRepo.CrearProducto(
                        txtNombre.Text.Trim(),
                        txtDescripcion.Text.Trim(),
                        precioVenta,
                        idCategoria,
                        idProveedor);

                    MessageBox.Show("Producto creado correctamente.");
                }
                else
                {
                    _productoRepo.ActualizarProducto(
                        _currentIdProducto.Value,
                        txtNombre.Text.Trim(),
                        txtDescripcion.Text.Trim(),
                        precioVenta,
                        idCategoria,
                        idProveedor,
                        checkBox1.Checked);

                    MessageBox.Show("Producto actualizado correctamente.");
                }

                CargarGrilla();
                LimpiarCampos();
                dgvProductos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnDesactivar_Click(object? sender, EventArgs e)
        {
            if (_currentIdProducto == null)
            {
                MessageBox.Show("Debe seleccionar un producto.");
                return;
            }

            if (MessageBox.Show("Desea desactivar este producto?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                _productoRepo.DesactivarProducto(_currentIdProducto.Value);
                MessageBox.Show("Producto desactivado.");
                CargarGrilla();
                LimpiarCampos();
                dgvProductos.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
            dgvProductos.ClearSelection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnCancelar_Click(sender, e);
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvProductos_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null || dgvProductos.CurrentRow.Cells["IdProducto"]?.Value == null)
                return;

            int idProducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["IdProducto"].Value);
            CargarDatosProducto(idProducto);
        }
    }
}
