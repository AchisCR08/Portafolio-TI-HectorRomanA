using System.Text.RegularExpressions;
using System.Web.Mvc;
using WebCentralAdmin.Infraestructura;
using WebCentralAdmin.Utilidades;

namespace WebCentralAdmin.Controllers
{
    public class ActivarController : BaseController
    {
        private readonly ConsultasServiceClient consultasClient = new ConsultasServiceClient();
        private readonly ProveedorServiceClient proveedorClient = new ProveedorServiceClient();

        public ActionResult Index()
        {
            try { return View(consultasClient.ListarLineasDisponibles() ?? new LineaDisponibleDTO[0]); }
            catch { TempData["Error"] = "Error al realizar el proceso"; return View(new LineaDisponibleDTO[0]); }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Activar(string numeroTelefono, string identificadorTelefono, string identificadorTarjeta, string tipoServicio, string cedula)
        {
            if (string.IsNullOrWhiteSpace(numeroTelefono) || string.IsNullOrWhiteSpace(identificadorTelefono) || string.IsNullOrWhiteSpace(identificadorTarjeta) || string.IsNullOrWhiteSpace(tipoServicio) || !Regex.IsMatch((cedula ?? string.Empty).Trim(), @"^[0-9]{6,20}$"))
            {
                TempData["Error"] = "La cédula debe contener entre 6 y 20 dígitos.";
                return RedirectToAction("Index");
            }
            try
            {
                RespuestaServicio respuesta = proveedorClient.ActivarDesactivarLinea(CifradorAES.Cifrar(numeroTelefono.Trim()), CifradorAES.Cifrar(identificadorTelefono.Trim()), CifradorAES.Cifrar(identificadorTarjeta.Trim()), tipoServicio.Trim(), CifradorAES.Cifrar(cedula.Trim()), "ACTIVO");
                TempData[respuesta != null && respuesta.Resultado ? "Mensaje" : "Error"] = respuesta != null && respuesta.Resultado ? "¡Proceso finalizado de forma exitosa!" : "Error al realizar el proceso";
            }
            catch { TempData["Error"] = "Error al realizar el proceso"; }
            return RedirectToAction("Index");
        }
    }
}
