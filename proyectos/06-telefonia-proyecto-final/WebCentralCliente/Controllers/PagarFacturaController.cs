using System;
using System.Linq;
using System.Web.Mvc;
using WebCentralCliente.Infraestructura;
using WebCentralCliente.Models;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Controllers
{
    // CLIENTE6: cancelar el monto de la factura pendiente de una linea postpago.
    public class PagarFacturaController : BaseController
    {
        private readonly ConsultasServiceClient consultasClient = new ConsultasServiceClient();
        private readonly AutenticacionServiceClient autenticacionClient = new AutenticacionServiceClient();

        // Sin telefono: lista las lineas postpago del cliente (entrada desde el menu).
        // Con telefono: muestra el formulario de tarjeta con el monto de la factura.
        public ActionResult Index(string telefono)
        {
            string identificacion = SesionHelper.ObtenerIdentificacion();
            LineaClienteDTO[] lineas = consultasClient.ListarLineasPorCliente(identificacion) ?? new LineaClienteDTO[0];

            LineaClienteDTO[] postpago = lineas
                .Where(l => string.Equals(l.TipoServicio, "postpago", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (string.IsNullOrWhiteSpace(telefono))
            {
                return View(new PagarFacturaPageViewModel { Postpago = postpago, MostrarFormulario = false });
            }

            LineaClienteDTO lineaSeleccionada = postpago.FirstOrDefault(l => l.NumeroTelefono == telefono);

            if (lineaSeleccionada == null)
            {
                TempData["Error"] = "La línea indicada no pertenece a este cliente o no es postpago.";
                return RedirectToAction("Index");
            }

            if (lineaSeleccionada.MontoFacturaPendiente <= 0)
            {
                TempData["Error"] = "La línea no tiene factura pendiente por cancelar.";
                return RedirectToAction("Index");
            }

            return View(new PagarFacturaPageViewModel
            {
                Postpago = postpago,
                MostrarFormulario = true,
                MontoFacturaPendiente = lineaSeleccionada.MontoFacturaPendiente,
                Formulario = new PagarFacturaViewModel { Telefono = telefono }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pagar(PagarFacturaViewModel model)
        {
            if (!EsValido(model, out string error))
            {
                TempData["Error"] = error;
                return RedirectToAction("Index", new { telefono = model != null ? model.Telefono : null });
            }

            // Verificar que la linea siga teniendo factura pendiente antes de pagar.
            string identificacion = SesionHelper.ObtenerIdentificacion();
            LineaClienteDTO linea = (consultasClient.ListarLineasPorCliente(identificacion) ?? new LineaClienteDTO[0])
                .FirstOrDefault(l => l.NumeroTelefono == model.Telefono
                    && string.Equals(l.TipoServicio, "postpago", StringComparison.OrdinalIgnoreCase));

            if (linea == null || linea.MontoFacturaPendiente <= 0)
            {
                TempData["Error"] = "La línea no tiene factura pendiente por cancelar.";
                return RedirectToAction("Index");
            }

            RespuestaOperacion respuesta = consultasClient.PagarFactura(model.Telefono);

            if (respuesta != null && respuesta.Resultado)
            {
                TempData["Mensaje"] = "Registro exitoso";
                EnviarCorreoFactura(identificacion, linea);
                return RedirectToAction("Index");
            }

            TempData["Error"] = respuesta != null ? respuesta.Mensaje : "Error al realizar el proceso";
            return RedirectToAction("Index", new { telefono = model.Telefono });
        }

        // CLIENTE6: al pagar, se envia un correo al cliente con el detalle de la facturacion.
        private void EnviarCorreoFactura(string identificacion, LineaClienteDTO linea)
        {
            try
            {
                RespuestaObtenerUsuario respuesta = autenticacionClient.ObtenerUsuario(identificacion);

                if (respuesta == null || !respuesta.Resultado || respuesta.Usuario == null)
                {
                    return;
                }

                string correo = respuesta.Usuario.CorreoElectronico;
                if (string.IsNullOrWhiteSpace(correo))
                {
                    return;
                }

                string nombre = string.Format("{0} {1}", respuesta.Usuario.Nombre, respuesta.Usuario.PrimerApellido).Trim();

                string cuerpo = string.Format(
                    "Estimado(a) {0}:\n\n" +
                    "Le informamos que su factura telefónica fue cancelada exitosamente.\n\n" +
                    "Detalle de la facturación:\n" +
                    "- Número de teléfono: {1}\n" +
                    "- Monto cancelado: {2:C}\n" +
                    "- Fecha de pago: {3}\n\n" +
                    "¡Gracias por su pago!\n" +
                    "Central General",
                    nombre,
                    linea.NumeroTelefono,
                    linea.MontoFacturaPendiente,
                    DateTime.Now);

                CorreoHelper.Enviar(correo, "Pago de factura - Central General", cuerpo);
            }
            catch
            {
                // El correo no debe romper el flujo de pago.
            }
        }

        private static bool EsValido(PagarFacturaViewModel model, out string error)
        {
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

            error = null;
            return true;
        }
    }
}
