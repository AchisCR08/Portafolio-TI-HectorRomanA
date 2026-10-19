using System.Web.Mvc;
using WebCentralAdmin.Infraestructura;

namespace WebCentralAdmin.Controllers
{
    // ADM5: devolucion (desactivacion) de una linea telefonica en uso.
    // Lista las ACTIVAS (WS_Consultas.M3) y desactiva via WS_PROVEEDOR2.
    public class DesactivarController : BaseController
    {
        private readonly ConsultasServiceClient consultasClient = new ConsultasServiceClient();
        private readonly ProveedorServiceClient proveedorClient = new ProveedorServiceClient();

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                LineaEnUsoDTO[] lineas = consultasClient.ListarLineasEnUso();
                return View(lineas);
            }
            catch
            {
                TempData["Error"] = "Error al realizar el proceso";
                return View(new LineaEnUsoDTO[0]);
            }
        }

        // Los valores llegan CIFRADOS (tal como se guardaron al activar) para
        // reenviarlos tal cual a WS_PROVEEDOR2: Java compara la cedula con la
        // almacenada (ProveedorServicio.desactivarLinea), asi que debe ser la misma.
        [HttpPost]
        public ActionResult Desactivar(
            string numeroTelefonoCifrado,
            string identificadorTelefonoCifrado,
            string identificadorTarjetaCifrado,
            string tipoServicio,
            string identificacionClienteCifrado)
        {
            try
            {
                RespuestaServicio respuesta = proveedorClient.ActivarDesactivarLinea(
                    numeroTelefonoCifrado,
                    identificadorTelefonoCifrado,
                    identificadorTarjetaCifrado,
                    tipoServicio,
                    identificacionClienteCifrado,
                    "DESACTIVAR");

                TempData[respuesta.Resultado ? "Mensaje" : "Error"] =
                    respuesta.Resultado
                        ? "¡Proceso finalizado de forma exitosa!"
                        : "Error al realizar el proceso";
            }
            catch
            {
                TempData["Error"] = "Error al realizar el proceso";
            }

            return RedirectToAction("Index");
        }
    }
}
