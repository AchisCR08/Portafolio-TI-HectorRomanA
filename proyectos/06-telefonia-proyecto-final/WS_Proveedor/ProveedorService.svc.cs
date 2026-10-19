using System;
using System.ServiceModel.Activation;
using WS_Proveedor.Infraestructura;
using WS_Proveedor.Modelos;

namespace WS_Proveedor
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProveedorService : IProveedorService
    {
        private readonly JavaProveedorClient javaProveedorClient = new JavaProveedorClient();

        public RespuestaServicio RegistrarServicioDisponible(
            string numeroTelefono,
            string identificadorTelefono,
            string identificadorTarjeta,
            string tipoServicio,
            string estado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroTelefono)
                    || string.IsNullOrWhiteSpace(identificadorTelefono)
                    || string.IsNullOrWhiteSpace(identificadorTarjeta)
                    || string.IsNullOrWhiteSpace(tipoServicio)
                    || string.IsNullOrWhiteSpace(estado))
                {
                    return CrearRespuesta(false, "Problemas al incluir la informacion.");
                }

                string respuestaProveedor = javaProveedorClient.RegistrarLineaDisponible(
                    numeroTelefono,
                    identificadorTelefono,
                    identificadorTarjeta,
                    tipoServicio,
                    estado);

                if (string.Equals(respuestaProveedor, "OK", StringComparison.OrdinalIgnoreCase))
                {
                    return CrearRespuesta(true, "Exitoso");
                }

                return CrearRespuesta(false, "Problemas al incluir la informacion.");
            }
            catch
            {
                return CrearRespuesta(false, "Problemas al incluir la informacion.");
            }
        }

        public RespuestaServicio ActivarDesactivarLinea(
            string numeroTelefono,
            string identificadorTelefono,
            string identificadorTarjeta,
            string tipoServicio,
            string identificacionDuenno,
            string estado)
        {
            const string mensajeError = "Problemas al activar/desactivar la linea.";

            try
            {
                if (string.IsNullOrWhiteSpace(numeroTelefono)
                    || string.IsNullOrWhiteSpace(identificadorTelefono)
                    || string.IsNullOrWhiteSpace(identificadorTarjeta)
                    || string.IsNullOrWhiteSpace(tipoServicio)
                    || string.IsNullOrWhiteSpace(identificacionDuenno)
                    || string.IsNullOrWhiteSpace(estado))
                {
                    return CrearRespuesta(false, mensajeError);
                }

                string respuestaProveedor = javaProveedorClient.CambiarEstadoLinea(
                    numeroTelefono,
                    identificadorTelefono,
                    identificadorTarjeta,
                    tipoServicio,
                    identificacionDuenno,
                    estado);

                return string.Equals(respuestaProveedor, "OK", StringComparison.OrdinalIgnoreCase)
                    ? CrearRespuesta(true, "Exitoso")
                    : CrearRespuesta(false, mensajeError);
            }
            catch
            {
                return CrearRespuesta(false, mensajeError);
            }
        }

        public RespuestaServicio CalcularFacturacionPostpago(
            string fechaCalculo,
            string fechaMaximaPago)
        {
            const string mensajeError = "Problemas al realizar el calculo.";

            try
            {
                if (string.IsNullOrWhiteSpace(fechaCalculo)
                    || string.IsNullOrWhiteSpace(fechaMaximaPago))
                {
                    return CrearRespuesta(false, mensajeError);
                }

                string respuestaProveedor = javaProveedorClient.CalcularFacturacionPostpago(
                    fechaCalculo,
                    fechaMaximaPago);

                return string.Equals(respuestaProveedor, "OK", StringComparison.OrdinalIgnoreCase)
                    ? CrearRespuesta(true, "Exitoso")
                    : CrearRespuesta(false, mensajeError);
            }
            catch
            {
                return CrearRespuesta(false, mensajeError);
            }
        }

        private static RespuestaServicio CrearRespuesta(bool resultado, string mensaje)
        {
            return new RespuestaServicio
            {
                Resultado = resultado,
                Mensaje = mensaje
            };
        }
    }
}
