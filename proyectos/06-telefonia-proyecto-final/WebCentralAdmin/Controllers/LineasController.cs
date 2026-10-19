using System;
using System.Web.Mvc;
using WebCentralAdmin.Infraestructura;
using WebCentralAdmin.Models;
using WebCentralAdmin.Utilidades;

namespace WebCentralAdmin.Controllers
{
    // ADM3: poner a disposicion nuevas lineas telefonicas.
    // Lista las DISPONIBLES (WS_Consultas.M1), permite eliminarlas (M2)
    // y registrarlas (WS_PROVEEDOR1 -> Java).
    public class LineasController : BaseController
    {
        private readonly ConsultasServiceClient consultasClient = new ConsultasServiceClient();
        private readonly ProveedorServiceClient proveedorClient = new ProveedorServiceClient();

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                LineaDisponibleDTO[] lineas = consultasClient.ListarLineasDisponibles();
                return View(lineas);
            }
            catch
            {
                TempData["Error"] = "Error al realizar el proceso";
                return View(new LineaDisponibleDTO[0]);
            }
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            return View(new LineaNuevaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(LineaNuevaViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            try
            {
                RespuestaServicio respuesta = proveedorClient.RegistrarServicioDisponible(
                    CifradorAES.Cifrar(modelo.NumeroTelefono.Trim()),
                    CifradorAES.Cifrar(modelo.IdentificadorTelefono.Trim()),
                    CifradorAES.Cifrar(modelo.IdentificadorTarjeta.Trim()),
                    modelo.TipoServicio,
                    "DISPONIBLE");

                if (respuesta.Resultado)
                {
                    TempData["Mensaje"] = "¡Proceso finalizado de forma exitosa!";
                }
                else if (string.Equals(respuesta.Mensaje, "Teléfono en uso", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["Error"] = "El número de teléfono ya está en uso.";
                }
                else
                {
                    TempData["Error"] = "Error al realizar el proceso";
                }
            }
            catch
            {
                TempData["Error"] = "Error al realizar el proceso";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Eliminar(string numeroTelefono)
        {
            if (string.IsNullOrWhiteSpace(numeroTelefono))
            {
                TempData["Error"] = "Error al realizar el proceso";
                return RedirectToAction("Index");
            }

            try
            {
                RespuestaOperacion respuesta = consultasClient.EliminarLineaDisponible(
                    CifradorAES.Cifrar(numeroTelefono.Trim()));

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
