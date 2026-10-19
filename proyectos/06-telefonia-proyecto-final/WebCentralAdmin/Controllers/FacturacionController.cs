using System;
using System.Globalization;
using System.Web.Mvc;
using WebCentralAdmin.Infraestructura;
using WebCentralAdmin.Models;

namespace WebCentralAdmin.Controllers
{
    // ADM6: calcular facturacion postpago.
    // Muestra la ultima facturacion (WS_Consultas.M5), valida las fechas
    // y ejecuta el calculo via WS_PROVEEDOR3 (socket a Java -> SP).
    public class FacturacionController : BaseController
    {
        private const string FormatoFecha = "yyyy-MM-dd";
        private const string FormatoWs = "yyyyMMdd";

        private readonly ConsultasServiceClient consultasClient = new ConsultasServiceClient();
        private readonly ProveedorServiceClient proveedorClient = new ProveedorServiceClient();

        [HttpGet]
        public ActionResult Index()
        {
            FacturacionViewModel modelo = new FacturacionViewModel();
            CargarUltima(modelo);
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ejecutar(FacturacionViewModel modelo)
        {
            CargarUltima(modelo);

            DateTime fechaCalculo;
            DateTime fechaMaximaPago;

            if (!DateTime.TryParseExact(modelo.FechaCalculo, FormatoFecha,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaCalculo))
            {
                ModelState.AddModelError(nameof(modelo.FechaCalculo), "La fecha de cálculo no es válida.");
            }

            if (!DateTime.TryParseExact(modelo.FechaMaximaPago, FormatoFecha,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaMaximaPago))
            {
                ModelState.AddModelError(nameof(modelo.FechaMaximaPago), "La fecha máxima de pago no es válida.");
            }

            if (ModelState.IsValid && fechaCalculo > fechaMaximaPago)
            {
                ModelState.AddModelError("", "La fecha de cálculo no puede ser posterior a la fecha máxima de pago.");
            }

            if (ModelState.IsValid && modelo.ExisteUltima)
            {
                DateTime ultimaCalculo;
                DateTime ultimaPago;

                if (DateTime.TryParseExact(modelo.UltimaFechaCalculo, FormatoFecha,
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out ultimaCalculo)
                    && DateTime.TryParseExact(modelo.UltimaFechaMaximaPago, FormatoFecha,
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out ultimaPago))
                {
                    if (fechaCalculo <= ultimaCalculo)
                    {
                        ModelState.AddModelError("",
                            "La fecha de cálculo debe ser posterior a la última ejecución.");
                    }
                    else if (fechaCalculo < ultimaCalculo.AddDays(1)
                          || fechaCalculo > ultimaPago.AddDays(1))
                    {
                        ModelState.AddModelError("",
                            "No pueden quedar días sin calcular. La fecha debe continuar inmediatamente después del último período.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return View("Index", modelo);
            }

            try
            {
                RespuestaServicio respuesta = proveedorClient.CalcularFacturacionPostpago(
                    fechaCalculo.ToString(FormatoWs),
                    fechaMaximaPago.ToString(FormatoWs));

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

        private void CargarUltima(FacturacionViewModel modelo)
        {
            try
            {
                FacturacionDTO ultima = consultasClient.UltimaFacturacion();
                modelo.ExisteUltima = ultima.Existe;
                modelo.UltimaFechaCalculo = ultima.FechaCalculo;
                modelo.UltimaFechaMaximaPago = ultima.FechaMaximaPago;
            }
            catch
            {
                modelo.ExisteUltima = false;
            }
        }
    }
}
