using System;
using System.Linq;
using System.Web.Mvc;
using WebCentralCliente.Infraestructura;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Controllers
{
    public class DevolverLineaController : BaseController
    {
        private readonly ConsultasServiceClient consultasClient = new ConsultasServiceClient();
        private readonly ProveedorServiceClient proveedorClient = new ProveedorServiceClient();

        public ActionResult Index()
        {
            try { return View(consultasClient.ListarLineasPorCliente(SesionHelper.ObtenerIdentificacion()) ?? new LineaClienteDTO[0]); }
            catch { TempData["Error"] = "Error al realizar el proceso"; return View(new LineaClienteDTO[0]); }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Devolver(string numeroTelefono)
        {
            try
            {
                LineaClienteDTO linea = (consultasClient.ListarLineasPorCliente(SesionHelper.ObtenerIdentificacion()) ?? new LineaClienteDTO[0]).FirstOrDefault(x => x.NumeroTelefono == numeroTelefono);
                if (linea == null) { TempData["Error"] = "La línea indicada no pertenece al cliente."; return RedirectToAction("Index"); }
                if (string.Equals(linea.TipoServicio, "postpago", StringComparison.OrdinalIgnoreCase) && linea.MontoFacturaPendiente > 0) { TempData["Error"] = "Primero debe cancelar la factura pendiente para devolver la línea."; return RedirectToAction("Index"); }
                RespuestaServicio respuesta = proveedorClient.ActivarDesactivarLinea(linea.NumeroTelefonoCifrado, linea.IdentificadorTelefonoCifrado, linea.IdentificadorTarjetaCifrado, linea.TipoServicio, linea.IdentificacionClienteCifrado, "INACTIVO");
                TempData[respuesta != null && respuesta.Resultado ? "Mensaje" : "Error"] = respuesta != null && respuesta.Resultado ? "¡Proceso finalizado de forma exitosa!" : (respuesta != null ? respuesta.Mensaje : "Error al realizar el proceso");
            }
            catch { TempData["Error"] = "Error al realizar el proceso"; }
            return RedirectToAction("Index");
        }
    }
}
