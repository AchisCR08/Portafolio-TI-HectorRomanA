using Repositorio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class frmProveedores : Form
    {
        private readonly ProveedoresRepository _repo = new();
        private int? _idProveedorSeleccionado;

        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarCampos();
        }

        private void CargarGrilla()
        {
            dgvProveedores.DataSource = _repo.Listar(true).ToList();

            if (dgvProveedores.Columns["IdProveedor"] != null)
                dgvProveedores.Columns["IdProveedor"].HeaderText = "Código";

            if (dgvProveedores.Columns["Nombre"] != null)
                dgvProveedores.Columns["Nombre"].HeaderText = "Proveedor";

            if (dgvProveedores.Columns["Telefono"] != null)
                dgvProveedores.Columns["Telefono"].HeaderText = "Teléfono";

            if (dgvProveedores.Columns["Correo"] != null)
                dgvProveedores.Columns["Correo"].HeaderText = "Correo";

            if (dgvProveedores.Columns["Activo"] != null)
                dgvProveedores.Columns["Activo"].HeaderText = "Activo";

            dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LimpiarCampos()
        {
            _idProveedorSeleccionado = null;
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            chkActivo.Checked = true;
            dgvProveedores.ClearSelection();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "El nombre del proveedor es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                if (_idProveedorSeleccionado == null)
                {
                    _repo.Crear(
                        txtNombre.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtCorreo.Text.Trim());

                    MessageBox.Show("Proveedor creado correctamente.");
                }
                else
                {
                    _repo.Actualizar(
                        _idProveedorSeleccionado.Value,
                        txtNombre.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtCorreo.Text.Trim(),
                        chkActivo.Checked);

                    MessageBox.Show("Proveedor actualizado correctamente.");
                }

                CargarGrilla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (_idProveedorSeleccionado == null)
            {
                MessageBox.Show(
                    "Debe seleccionar un proveedor de la tabla.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (MessageBox.Show(
                    "¿Desea desactivar este proveedor?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _repo.Eliminar(_idProveedorSeleccionado.Value);
                MessageBox.Show("Proveedor desactivado.");
                CargarGrilla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void dgvProveedores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProveedores.CurrentRow == null ||
                dgvProveedores.CurrentRow.Cells["IdProveedor"].Value == null)
            {
                return;
            }

            _idProveedorSeleccionado = Convert.ToInt32(
                dgvProveedores.CurrentRow.Cells["IdProveedor"].Value);

            txtNombre.Text = Convert.ToString(
                dgvProveedores.CurrentRow.Cells["Nombre"].Value);

            txtTelefono.Text = Convert.ToString(
                dgvProveedores.CurrentRow.Cells["Telefono"].Value);

            txtCorreo.Text = Convert.ToString(
                dgvProveedores.CurrentRow.Cells["Correo"].Value);

            chkActivo.Checked = Convert.ToBoolean(
                dgvProveedores.CurrentRow.Cells["Activo"].Value);
        }
    }
}
