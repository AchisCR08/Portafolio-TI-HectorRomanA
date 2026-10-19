using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace WebCentralCliente.Utilidades
{
    // Envio de correo con System.Net.Mail (sin NuGet extra).
    // Los datos del SMTP se leen de Web.config (appSettings -> Correo.*).
    public static class CorreoHelper
    {
        public static bool Enviar(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                string host = ConfigurationManager.AppSettings["Correo.Host"] ?? "localhost";
                int puerto = ObtenerEntero("Correo.Puerto", 25);
                string desde = ConfigurationManager.AppSettings["Correo.Desde"];
                bool ssl = ObtenerBooleano("Correo.Ssl", false);
                string usuario = ConfigurationManager.AppSettings["Correo.Usuario"];
                string clave = ConfigurationManager.AppSettings["Correo.Clave"];

                using (MailMessage mensaje = new MailMessage())
                {
                    mensaje.From = new MailAddress(desde, "Central General");
                    mensaje.To.Add(destinatario);
                    mensaje.Subject = asunto;
                    mensaje.Body = cuerpo;
                    mensaje.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient(host, puerto))
                    {
                        smtp.EnableSsl = ssl;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 20000;

                        if (!string.IsNullOrEmpty(usuario))
                        {
                            smtp.Credentials = new NetworkCredential(usuario, clave);
                        }

                        smtp.Send(mensaje);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static int ObtenerEntero(string llave, int valorDefecto)
        {
            int valor;
            return int.TryParse(ConfigurationManager.AppSettings[llave], out valor) ? valor : valorDefecto;
        }

        private static bool ObtenerBooleano(string llave, bool valorDefecto)
        {
            bool valor;
            return bool.TryParse(ConfigurationManager.AppSettings[llave], out valor) ? valor : valorDefecto;
        }
    }
}
