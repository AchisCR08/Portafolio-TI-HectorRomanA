using System;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioRepository _usuarioRepo;

        public FrmLogin()
        {
            InitializeComponent();
            _usuarioRepo = new UsuarioRepository();
            txtContrasenia.PasswordChar = '*';
            AcceptButton = btnIngresar;
            btnIngresar.Click += btnIngresar_Click;
        }

        private void btnIngresar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Debe ingresar el usuario.");
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContrasenia.Text))
            {
                MessageBox.Show("Debe ingresar la contrasena.");
                txtContrasenia.Focus();
                return;
            }

            try
            {
                var usuario = _usuarioRepo.ValidarLogin(txtUsuario.Text.Trim(), txtContrasenia.Text);

                if (usuario == null)
                {
                    MessageBox.Show("Usuario o contrasena incorrectos, o el usuario no tiene sucursal activa.");
                    txtContrasenia.Clear();
                    txtContrasenia.Focus();
                    return;
                }

                SesionActual.Iniciar(usuario);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar el usuario: {ex.Message}");
            }
        }
    }
}
