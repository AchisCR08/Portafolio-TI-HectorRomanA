using Entidades;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmCitasGrooming : Form
    {
        private readonly AtencionRepository _repo = new();
        private int? _idCitaSeleccionada;

        public FrmCitasGrooming()
        {
            InitializeComponent();
            this.Load += FrmCitasGrooming_Load;
        }

        private void FrmCitasGrooming_Load(object sender, EventArgs e)
        {
            CargarCombo(cmbMascota, _repo.Mascotas());
            CargarCombo(cmbGroomer, _repo.Groomers());
            CargarCombo(cmbEstado, _repo.Estados());
            CargarCombo(cmbServicio, _repo.Servicios());

            CargarCitas();
            LimpiarCampos();
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
            dgvCitasGrooming.DataSource =
                _repo.ListarCitas(true, SesionActual.IdSucursal).ToList();

            dgvCitasGrooming.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            foreach (var col in new[] { "IdMascota", "IdResponsable", "IdEstado", "IdServicio" })
                if (dgvCitasGrooming.Columns[col] != null)
                    dgvCitasGrooming.Columns[col].Visible = false;
        }

        private void LimpiarCampos()
        {
            _idCitaSeleccionada = null;

            dptFecha.Value = DateTime.Today;
            dtpHora.Value = DateTime.Now;

            cmbMascota.SelectedIndex = -1;
            cmbGroomer.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;
            cmbServicio.SelectedIndex = -1;

            dgvCitasGrooming.ClearSelection();
        }

        private bool Validar(out int mascota, out int groomer,
            out int estado, out int servicio)
        {
            mascota = groomer = estado = servicio = 0;

            if (cmbMascota.SelectedValue is not int ||
                cmbGroomer.SelectedValue is not int ||
                cmbEstado.SelectedValue is not int ||
                cmbServicio.SelectedValue is not int)
            {
                MessageBox.Show("Complete todos los campos.");
                return false;
            }

            mascota = (int)cmbMascota.SelectedValue;
            groomer = (int)cmbGroomer.SelectedValue;
            estado = (int)cmbEstado.SelectedValue;
            servicio = (int)cmbServicio.SelectedValue;
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar(out int mascota, out int groomer,
                out int estado, out int servicio))
                return;

            try
            {
                _repo.CrearCita(
                    true,
                    dptFecha.Value.Date,
                    dtpHora.Value.TimeOfDay,
                    mascota,
                    groomer,
                    estado,
                    SesionActual.IdSucursal,
                    "",
                    servicio);

                MessageBox.Show("Cita de grooming guardada.");
                CargarCitas();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_idCitaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una cita de la tabla.");
                return;
            }

            if (!Validar(out int mascota, out int groomer,
                out int estado, out int servicio))
                return;

            try
            {
                _repo.ActualizarCitaGrooming(
                    _idCitaSeleccionada.Value,
                    dptFecha.Value.Date,
                    dtpHora.Value.TimeOfDay,
                    mascota,
                    groomer,
                    estado,
                    servicio);

                MessageBox.Show("Cita modificada.");
                CargarCitas();
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

        private void dgvCitasGrooming_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dgvCitasGrooming.CurrentRow?.DataBoundItem is not CitaRegistro cita)
                return;

            _idCitaSeleccionada = cita.Id;

            dptFecha.Value = cita.Fecha;
            dtpHora.Value = DateTime.Today.Add(cita.Hora);

            cmbMascota.SelectedValue = cita.IdMascota;
            cmbGroomer.SelectedValue = cita.IdResponsable;
            cmbEstado.SelectedValue = cita.IdEstado;
            cmbServicio.SelectedValue = cita.IdServicio ?? 0;
        }

        private void dgvCitasGrooming_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}