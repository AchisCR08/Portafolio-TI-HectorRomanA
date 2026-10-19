using Entidades;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmCitasVeterinaria : Form
    {
        private readonly AtencionRepository _repo = new();
        private int? _idCitaSeleccionada;

        public FrmCitasVeterinaria()
        {
            InitializeComponent();
        }

        private void FrmCitasVeterinaria_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarCitas();
            LimpiarCampos();
        }

        private void CargarCombos()
        {
            CargarCombo(cobMascota, _repo.Mascotas());
            CargarCombo(cmbVeterinario, _repo.Veterinarios());
            CargarCombo(cmbEstado, _repo.Estados());
        }

        private static void CargarCombo(
            ComboBox combo,
            IEnumerable<OpcionCatalogo> datos)
        {
            combo.DataSource = datos.ToList();
            combo.DisplayMember = "Nombre";
            combo.ValueMember = "Id";
        }

        private void CargarCitas()
        {
            dgvCitas.DataSource =
                _repo.ListarCitas(false, SesionActual.IdSucursal).ToList();

            dgvCitas.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvCitas.Columns["Id"] != null)
                dgvCitas.Columns["Id"].HeaderText = "Código";

            if (dgvCitas.Columns["Servicio"] != null)
                dgvCitas.Columns["Servicio"].HeaderText = "Motivo";

            foreach (var col in new[] { "IdMascota", "IdResponsable", "IdEstado", "IdServicio" })
                if (dgvCitas.Columns[col] != null)
                    dgvCitas.Columns[col].Visible = false;
        }

        private void LimpiarCampos()
        {
            _idCitaSeleccionada = null;

            dtpFecha.Value = DateTime.Today;
            dtpHora.Value = DateTime.Now;

            cobMascota.SelectedIndex = -1;
            cmbVeterinario.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;

            txtMotivo.Clear();
            dgvCitas.ClearSelection();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cobMascota.SelectedValue is not int idMascota ||
                cmbVeterinario.SelectedValue is not int idVeterinario ||
                cmbEstado.SelectedValue is not int idEstado)
            {
                MessageBox.Show(
                    "Debe seleccionar mascota, veterinario y estado.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                _repo.CrearCita(
                    false,
                    dtpFecha.Value.Date,
                    dtpHora.Value.TimeOfDay,
                    idMascota,
                    idVeterinario,
                    idEstado,
                    SesionActual.IdSucursal,
                    txtMotivo.Text.Trim(),
                    null);

                MessageBox.Show("Cita registrada correctamente.");

                CargarCitas();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al guardar la cita: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_idCitaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una cita para cancelarla.");
                return;
            }

            if (MessageBox.Show(
                    "¿Desea cancelar la cita seleccionada?",
                    "Confirmar cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _repo.CancelarCitaVeterinaria(_idCitaSeleccionada.Value);
                MessageBox.Show("Cita cancelada correctamente.");
                CargarCitas();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cancelar la cita: {ex.Message}");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_idCitaSeleccionada == null)
            {
                MessageBox.Show(
                    "Seleccione una cita de la tabla primero.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cobMascota.SelectedValue is not int idMascota ||
                cmbVeterinario.SelectedValue is not int idVeterinario ||
                cmbEstado.SelectedValue is not int idEstado)
            {
                MessageBox.Show("Debe seleccionar mascota, veterinario y estado.");
                return;
            }

            try
            {
                _repo.ActualizarCitaVeterinaria(
                    _idCitaSeleccionada.Value,
                    dtpFecha.Value.Date, dtpHora.Value.TimeOfDay,
                    idMascota, idVeterinario, idEstado,
                    SesionActual.IdSucursal, txtMotivo.Text.Trim());

                MessageBox.Show("Cita modificada correctamente.");
                CargarCitas();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la cita: {ex.Message}");
            }
        }

        private void dgvCitas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCitas.CurrentRow?.DataBoundItem is not CitaRegistro cita)
                return;

            _idCitaSeleccionada = cita.Id;

            dtpFecha.Value = cita.Fecha;
            dtpHora.Value = DateTime.Today.Add(cita.Hora);

            cobMascota.SelectedValue = cita.IdMascota;
            cmbVeterinario.SelectedValue = cita.IdResponsable;
            cmbEstado.SelectedValue = cita.IdEstado;

            txtMotivo.Text = cita.Servicio;
        }
    }
}
