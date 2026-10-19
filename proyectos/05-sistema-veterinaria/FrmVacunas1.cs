using Entidades;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmVacunas1 : Form
    {
        private readonly AtencionRepository _repo = new();
        private int? _idHistorialSeleccionado;

        public FrmVacunas1()
        {
            InitializeComponent();
        }

        private void FrmVacunas1_Load(object sender, EventArgs e)
        {
            CargarCombo(cmbMascotas, _repo.Mascotas());
            CargarCombo(cmbVacunas, _repo.Vacunas());

            CargarVacunas();
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

        private void CargarVacunas()
        {
            dgvVacunas.DataSource = _repo.HistorialVacunas().ToList();
            dgvVacunas.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            foreach (var col in new[] { "IdMascota", "IdVacuna" })
                if (dgvVacunas.Columns[col] != null)
                    dgvVacunas.Columns[col].Visible = false;
        }

        private void LimpiarCampos()
        {
            _idHistorialSeleccionado = null;
            cmbMascotas.SelectedIndex = -1;
            cmbVacunas.SelectedIndex = -1;

            dtpAplicacion.Value = DateTime.Today;
            dtpProxima.Value = DateTime.Today;
            dtpProxima.Checked = false;

            dgvVacunas.ClearSelection();
        }

        private bool Validar(out int mascota, out int vacuna)
        {
            mascota = vacuna = 0;

            if (cmbMascotas.SelectedValue is not int ||
                cmbVacunas.SelectedValue is not int)
            {
                MessageBox.Show("Seleccione mascota y vacuna.");
                return false;
            }

            mascota = (int)cmbMascotas.SelectedValue;
            vacuna = (int)cmbVacunas.SelectedValue;
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar(out int mascota, out int vacuna))
                return;

            try
            {
                _repo.RegistrarVacuna(
                    mascota,
                    vacuna,
                    dtpAplicacion.Value.Date,
                    dtpProxima.Checked
                        ? dtpProxima.Value.Date
                        : null);

                MessageBox.Show("Vacuna registrada.");
                CargarVacunas();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_idHistorialSeleccionado == null)
            {
                MessageBox.Show("Seleccione un registro de la tabla.");
                return;
            }

            if (!Validar(out int mascota, out int vacuna))
                return;

            try
            {
                _repo.ActualizarHistorialVacuna(
                    _idHistorialSeleccionado.Value,
                    mascota,
                    vacuna,
                    dtpAplicacion.Value.Date,
                    dtpProxima.Checked
                        ? dtpProxima.Value.Date
                        : null);

                MessageBox.Show("Registro de vacuna modificado.");
                CargarVacunas();
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

        private void dgvVacunas_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dgvVacunas.CurrentRow?.DataBoundItem is not VacunaRegistro vacuna)
                return;

            _idHistorialSeleccionado = vacuna.IdHistorial;

            cmbMascotas.SelectedValue = vacuna.IdMascota;
            cmbVacunas.SelectedValue = vacuna.IdVacuna;

            if (vacuna.FechaAplicacion.HasValue)
                dtpAplicacion.Value = vacuna.FechaAplicacion.Value;

            dtpProxima.Checked = vacuna.ProximaDosis.HasValue;
            if (vacuna.ProximaDosis.HasValue)
                dtpProxima.Value = vacuna.ProximaDosis.Value;
        }

        private void dgvVacunas_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}