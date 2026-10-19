using Repositorio;
using System;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmHistorialMedico1 : Form
    {
        private readonly AtencionRepository _repo = new();
        private int? _idCitaSeleccionada;

        public FrmHistorialMedico1()
        {
            InitializeComponent();
        }

        private void FrmHistorialMedico1_Load(object sender, EventArgs e)
        {
            CargarHistorial();
            LimpiarCampos();
        }

        private void CargarHistorial()
        {
            dgvHistorial.DataSource =
                _repo.HistorialMedico(SesionActual.IdSucursal).ToList();

            dgvHistorial.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            foreach (var col in new[] { "IdMascota", "IdResponsable", "IdEstado", "IdServicio" })
                if (dgvHistorial.Columns[col] != null)
                    dgvHistorial.Columns[col].Visible = false;
        }

        private void LimpiarCampos()
        {
            _idCitaSeleccionada = null;
            txtDiagnostico.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dgvHistorial.ClearSelection();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (_idCitaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una cita de la tabla.");
                return;
            }

            try
            {
                _repo.GuardarConsulta(
                    _idCitaSeleccionada.Value,
                    txtDiagnostico.Text.Trim(),
                    textBox2.Text.Trim(),
                    textBox3.Text.Trim());

                MessageBox.Show("Consulta guardada.");
                CargarHistorial();
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

        private void dgvHistorial_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dgvHistorial.CurrentRow?.Cells["Id"].Value == null)
                return;

            _idCitaSeleccionada = Convert.ToInt32(
                dgvHistorial.CurrentRow.Cells["Id"].Value);

            try
            {
                var consulta = _repo.ObtenerConsultaPorCita(
                    _idCitaSeleccionada.Value);

                txtDiagnostico.Text = consulta?.Diagnostico ?? "";
                textBox2.Text = consulta?.Tratamiento ?? "";
                textBox3.Text = consulta?.Observaciones ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la consulta: {ex.Message}");
            }
        }

        private void dgvHistorial_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
