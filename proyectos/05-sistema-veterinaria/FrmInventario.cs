using System;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmInventario : Form
    {
        private readonly InventarioRepository _inventarioRepo;
        private int? _currentIdProducto = null;

        public FrmInventario()
        {
            InitializeComponent();
            _inventarioRepo = new InventarioRepository();

            dgvInventario.SelectionChanged += dgvInventario_SelectionChanged;

            CargarSucursal();
            CargarComboProductos();
            CargarGrilla();
            LimpiarCampos();
            dgvInventario.ClearSelection();
        }

        private void CargarSucursal()
        {
            lblSucursalNombre.Text = SesionActual.NombreSucursal;
        }

        private void CargarComboProductos()
        {
            var productos = _inventarioRepo.ListarProductosActivos().ToList();
            cmbProducto.DisplayMember = "Nombre";
            cmbProducto.ValueMember = "IdProducto";
            cmbProducto.DataSource = productos;
            cmbProducto.SelectedIndex = -1;
        }

        private void CargarGrilla()
        {
            dgvInventario.DataSource = _inventarioRepo
                .ConsultarInventario(idSucursal: SesionActual.IdSucursal)
                .ToList();

            // Ocultar columnas de IDs
            if (dgvInventario.Columns["IdSucursal"] != null)
                dgvInventario.Columns["IdSucursal"].Visible = false;
            if (dgvInventario.Columns["IdProducto"] != null)
                dgvInventario.Columns["IdProducto"].Visible = false;

            // Renombrar encabezados
            if (dgvInventario.Columns["Sucursal"] != null)
                dgvInventario.Columns["Sucursal"].HeaderText = "Sucursal";
            if (dgvInventario.Columns["Producto"] != null)
                dgvInventario.Columns["Producto"].HeaderText = "Producto";
            if (dgvInventario.Columns["Stock"] != null)
                dgvInventario.Columns["Stock"].HeaderText = "Stock";

            dgvInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LimpiarCampos()
        {
            _currentIdProducto = null;
            cmbProducto.SelectedIndex = -1;
            txtStock.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Debe ingresar un stock válido (número entero mayor o igual a 0).", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProducto = (int)cmbProducto.SelectedValue;

            try
            {
                _inventarioRepo.GuardarInventario(
                    SesionActual.IdSucursal,
                    idProducto,
                    stock);

                MessageBox.Show("Inventario guardado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarGrilla();
                LimpiarCampos();
                dgvInventario.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar inventario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_currentIdProducto == null)
            {
                MessageBox.Show("Debe seleccionar un registro del inventario.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea eliminar este registro de inventario?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _inventarioRepo.EliminarInventario(
                    SesionActual.IdSucursal,
                    _currentIdProducto.Value);

                MessageBox.Show("Registro de inventario eliminado.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarGrilla();
                LimpiarCampos();
                dgvInventario.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar inventario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            dgvInventario.ClearSelection();
        }

        private void dgvInventario_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow == null ||
                dgvInventario.CurrentRow.Cells["IdProducto"]?.Value == null)
                return;

            int idProducto = Convert.ToInt32(dgvInventario.CurrentRow.Cells["IdProducto"].Value);
            int stock = Convert.ToInt32(dgvInventario.CurrentRow.Cells["Stock"].Value);

            _currentIdProducto = idProducto;
            cmbProducto.SelectedValue = idProducto;
            txtStock.Text = stock.ToString();
        }

        private void dgvInventario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
