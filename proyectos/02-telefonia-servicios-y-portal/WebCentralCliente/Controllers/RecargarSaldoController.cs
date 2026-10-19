using System.Linq;
using System.Web.Mvc;
using WebCentralCliente.Infraestructura;
using WebCentralCliente.Models;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Controllers
{
    // CLIENTE5: cargar saldo en una linea prepago.
    public class RecargarSaldoController : BaseController
    {
        private readonly ConsultasServiceClient consultasClient = new ConsultasServiceClient();

        // Sin telefono: muestra la lista de lineas prepago (entrada desde el menu).
        // Con telefono: muestra el formulario de tarjeta (entrada desde CLIENTE4 o recarga fallida).
        public ActionResult Index(string telefono)
        {
            string identificacion = SesionHelper.ObtenerIdentificacion();
            LineaClienteDTO[] lineas = consultasClient.ListarLineasPorCliente(identificacion) ?? new LineaClienteDTO[0];

            LineaClienteDTO[] prepago = lineas
                .Where(l => string.Equals(l.TipoServicio, "prepago", System.StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (string.IsNullOrWhiteSpace(telefono))
            {
                return View(new RecargarSaldoPageViewModel { Prepago = prepago, MostrarFormulario = false });
            }

            LineaClienteDTO lineaSeleccionada = prepago.FirstOrDefault(l => l.NumeroTelefono == telefono);

            if (lineaSeleccionada == null)
            {
                TempData["Error"] = "La línea indicada no pertenece a este cliente o no es prepago.";
                return RedirectToAction("Index");
            }

            return View(new RecargarSaldoPageViewModel
            {
                Prepago = prepago,
                MostrarFormulario = true,
                SaldoActual = lineaSeleccionada.Saldo,
                Formulario = new RecargaViewModel { Telefono = telefono }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cargar(RecargaViewModel model)
        {
            if (!EsValido(model, out int monto, out string error))
            {
                TempData["Error"] = error;
                return RedirectToAction("Index", new { telefono = model?.Telefono });
            }

            RespuestaOperacion respuesta = consultasClient.CargarSaldo(model.Telefono, monto);

            if (respuesta != null && respuesta.Resultado)
            {
                TempData["Mensaje"] = "Registro exitoso";
                return RedirectToAction("Index", "MisLineas");
            }

            TempData["Error"] = respuesta != null ? respuesta.Mensaje : "Error al realizar el proceso";
            return RedirectToAction("Index", new { telefono = model.Telefono });
        }

        private static bool EsValido(RecargaViewModel model, out int monto, out string error)
        {
            monto = 0;

            if (model == null || string.IsNullOrWhiteSpace(model.Telefono))
            {
                error = "Debe seleccionar una línea.";
                return false;
            }

            if (!Validaciones.EsNumeroTarjetaValido(model.NumeroTarjeta))
            {
                error = "El número de tarjeta debe tener 12 dígitos.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.NombreTarjeta))
            {
                error = "Debe indicar el nombre del dueño de la tarjeta.";
                return false;
            }

            if (!Validaciones.EsFechaVencimientoValida(model.MesVencimiento, model.AnioVencimiento))
            {
                error = "La fecha de vencimiento no es válida.";
                return false;
            }

            if (!Validaciones.EsCodigoSeguridadValido(model.CodigoSeguridad))
            {
                error = "El código de seguridad debe tener 3 dígitos.";
                return false;
            }

            if (!Validaciones.EsMontoValido(model.Monto, out monto))
            {
                error = "El monto a cargar debe ser un número entero positivo.";
                return false;
            }

            error = null;
            return true;
        }
    }
}
