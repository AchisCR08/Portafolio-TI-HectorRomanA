using Microsoft.Extensions.Configuration;
using Repositorio;
using Veterinaria1;

namespace Veterinaria1
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.ThreadException += (sender, e) =>
            {
                MessageBox.Show(
                    "Error inesperado:\n\n" + e.Exception.GetType().Name + ": " + e.Exception.Message + "\n\n" + e.Exception.StackTrace,
                    "Error de aplicacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                MessageBox.Show(
                    "Error critico:\n\n" + e.ExceptionObject.ToString(),
                    "Error critico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            };

            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string? connectionString = configuration.GetConnectionString("VeterinariaDB");

                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show(
                        "No se encontro la cadena de conexion VeterinariaDB en appsettings.json.",
                        "Error de configuracion",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DbContextConfig.ConnectionString = connectionString;

                ApplicationConfiguration.Initialize();

                using var login = new FrmLogin();
                if (login.ShowDialog() != DialogResult.OK || !SesionActual.EstaAutenticado)
                {
                    return;
                }

                Application.Run(new FrmMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al iniciar la aplicacion:\n\n" + ex.GetType().Name + ": " + ex.Message + "\n\n" + ex.StackTrace,
                    "Error de inicio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
