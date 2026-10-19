using Entidades;
using Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmUsuarios : Form
    {
        private UsuarioRepository _usuarioRepo;
        private int? _currentIdUsuario = null;
        private CatalogoRepository _catalogoRepo;
        public FrmUsuarios()
        {
            InitializeComponent();
            _usuarioRepo = new UsuarioRepository();
            _catalogoRepo = new CatalogoRepository();

            // Cargar los datos del usuario al seleccionarlo en la grilla
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;

            CargarGrilla();
            CargarRoles();
            LimpiarCampos();
            CargarPersonasSinUsuario();
            dgvUsuarios.ClearSelection();

        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {

        }




        private void CargarGrilla()
        {
            // Mostrar TODOS los usuarios (activos e inactivos) para poder reactivar
            var usuarios = _usuarioRepo.ListarUsuarios(false).ToList();
            dgvUsuarios.DataSource = usuarios;

            // Ocultar columnas internas
            string[] columnasOcultas = { "IdUsuario", "IdPersona", "IdRol" };
            foreach (var col in columnasOcultas)
                if (dgvUsuarios.Columns[col] != null)
                    dgvUsuarios.Columns[col].Visible = false;

            // Renombrar columnas para mostrar amigable
            if (dgvUsuarios.Columns["Username"] != null)
                dgvUsuarios.Columns["Username"].HeaderText = "Usuario";
            if (dgvUsuarios.Columns["PersonaNombre"] != null)
                dgvUsuarios.Columns["PersonaNombre"].HeaderText = "Persona";
            if (dgvUsuarios.Columns["RolNombre"] != null)
                dgvUsuarios.Columns["RolNombre"].HeaderText = "Rol";
            if (dgvUsuarios.Columns["SucursalNombre"] != null)
                dgvUsuarios.Columns["SucursalNombre"].HeaderText = "Sucursal";
            if (dgvUsuarios.Columns["Activo"] != null)
                dgvUsuarios.Columns["Activo"].HeaderText = "Activo";

            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void CargarPersonasSinUsuario()
        {
            var personas = _usuarioRepo.ListarPersonasSinUsuario().ToList();
            cmbPersona.DisplayMember = "Nombre";
            cmbPersona.ValueMember = "IdPersona";
            cmbPersona.DataSource = personas;
            cmbPersona.SelectedIndex = -1;
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null && dgvUsuarios.CurrentRow.Cells["IdUsuario"] != null && dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value != null)
            {
                int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value);
                CargarDatosUsuario(idUsuario);
            }
        }



        private void CargarDatosUsuario(int idUsuario)
        {
            var usuario = _usuarioRepo.ObtenerUsuarioPorId(idUsuario);
            if (usuario == null) return;

            _currentIdUsuario = usuario.IdUsuario;
            txtUsername.Text = usuario.Username;
            chkActivo.Checked = usuario.Activo;

            // Seleccionar el rol en el ComboBox
            if (usuario.IdRol > 0)
            {
                cmbRolUsuario.SelectedValue = usuario.IdRol;
                if (cmbRolUsuario.SelectedValue == null)
                    MessageBox.Show("El rol asociado a este usuario está inactivo o no existe.\nSeleccione un rol antes de guardar.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Modo edición: deshabilitar combo de persona y mostrar la persona asociada
            cmbPersona.Enabled = false;
            lblPersonaSeleccionada.Text = usuario.PersonaNombre;

            // En modo edición, podrías permitir cambiar el rol o no según requieras
            cmbRolUsuario.Enabled = true;   // si permites cambio de rol

            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
        }



        private void LimpiarCampos()
        {
            _currentIdUsuario = null;
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();   
            chkActivo.Checked = true;
            cmbRolUsuario.SelectedIndex = -1;

            
            cmbPersona.Enabled = true;
            cmbPersona.DataSource = null;
            cmbPersona.SelectedIndex = -1;
            lblPersonaSeleccionada.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones existentes...
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("El nombre de usuario es obligatorio.");
                return;
            }
            if (_currentIdUsuario == null && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Debe ingresar una contraseña para el nuevo usuario.");
                return;
            }
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }
            // Validar que se haya seleccionado un rol
            if (cmbRolUsuario.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un rol para el usuario.");
                return;
            }
            int idRol = (int)cmbRolUsuario.SelectedValue;

            try
            {
                if (_currentIdUsuario == null) // NUEVO
                {
                    if (cmbPersona.SelectedValue == null)
                    {
                        MessageBox.Show("Debe seleccionar una persona.");
                        return;
                    }
                    int idPersona = (int)cmbPersona.SelectedValue;
                    // Llamada al repositorio con idRol
                    _usuarioRepo.CrearUsuario(idPersona, txtUsername.Text, txtPassword.Text, idRol);
                    MessageBox.Show("Usuario creado correctamente.");
                }
                else // EDITAR
                {
                    string nuevoUsername = txtUsername.Text;
                    string nuevaPassword = string.IsNullOrEmpty(txtPassword.Text) ? null : txtPassword.Text;
                    // Llamada al repositorio que permita actualizar también el rol y el estado activo
                    _usuarioRepo.ActualizarUsuario(_currentIdUsuario.Value, nuevoUsername, nuevaPassword, idRol, chkActivo.Checked);
                    MessageBox.Show("Usuario actualizado correctamente.");
                }

                CargarGrilla();
                LimpiarCampos();
                CargarPersonasSinUsuario();
                dgvUsuarios.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_currentIdUsuario == null) return;
            if (MessageBox.Show("¿Desactivar este usuario? (No se elimina físicamente)", "Confirmar",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _usuarioRepo.DesactivarUsuario(_currentIdUsuario.Value);
                MessageBox.Show("Usuario desactivado.");
                CargarGrilla();
                LimpiarCampos();
                CargarPersonasSinUsuario();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarPersonasSinUsuario();
            dgvUsuarios.ClearSelection();
        }

        

        

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarPersonasSinUsuario();
            
            if (cmbRolUsuario.Items.Count == 0)
                CargarRoles();
            else
                cmbRolUsuario.SelectedIndex = -1;
            dgvUsuarios.ClearSelection();
        }

        private void CargarRoles()
        {
            var roles = _catalogoRepo.ListarRolesActivos().ToList();
            cmbRolUsuario.DisplayMember = "Nombre";
            cmbRolUsuario.ValueMember = "IdRol";
            cmbRolUsuario.DataSource = roles;
            cmbRolUsuario.SelectedIndex = -1;
        }
    }




}

