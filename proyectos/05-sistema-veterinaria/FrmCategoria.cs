using Repositorio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmCategoria : Form
    {
        private readonly CategoriaRepository _repo = new();
        private int? _idCategoriaSeleccionada;

        public FrmCategoria()
        {
            InitializeComponent();
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarCampos();
        }

        private void CargarGrilla()
        {
            dgvCategorias.DataSource = _repo.Listar(true).ToList();

            if (dgvCategorias.Columns["IdCategoria"] != null)
                dgvCategorias.Columns["IdCategoria"].HeaderText = "Código";

            if (dgvCategorias.Columns["Nombre"] != null)
                dgvCategorias.Columns["Nombre"].HeaderText = "Categoría";

            if (dgvCategorias.Columns["Activo"] != null)
                dgvCategorias.Columns["Activo"].HeaderText = "Activo";

            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LimpiarCampos()
        {
            _idCategoriaSeleccionada = null;
            txtNombre.Clear();
            chkActivo.Checked = true;
            dgvCategorias.ClearSelection();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "El nombre de la categoría es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                if (_idCategoriaSeleccionada == null)
                {
                    _repo.Crear(txtNombre.Text.Trim());
                    MessageBox.Show("Categoría creada correctamente.");
                }
                else
                {
                    _repo.Actualizar(
                        _idCategoriaSeleccionada.Value,
                        txtNombre.Text.Trim(),
                        chkActivo.Checked);

                    MessageBox.Show("Categoría actualizada correctamente.");
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
            if (_idCategoriaSeleccionada == null)
            {
                MessageBox.Show(
                    "Debe seleccionar una categoría de la tabla.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (MessageBox.Show(
                    "¿Desea desactivar esta categoría?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _repo.Eliminar(_idCategoriaSeleccionada.Value);
                MessageBox.Show("Categoría desactivada.");
                CargarGrilla();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void dgvCategorias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null ||
                dgvCategorias.CurrentRow.Cells["IdCategoria"].Value == null)
            {
                return;
            }

            _idCategoriaSeleccionada = Convert.ToInt32(
                dgvCategorias.CurrentRow.Cells["IdCategoria"].Value);

            txtNombre.Text = Convert.ToString(
                dgvCategorias.CurrentRow.Cells["Nombre"].Value);

            chkActivo.Checked = Convert.ToBoolean(
                dgvCategorias.CurrentRow.Cells["Activo"].Value);
        }
    }
}
