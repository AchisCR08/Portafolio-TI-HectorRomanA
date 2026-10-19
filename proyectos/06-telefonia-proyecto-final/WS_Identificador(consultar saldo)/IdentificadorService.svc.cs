using System;
using System.ServiceModel.Activation;
using WS_Identificador.Infraestructura;
using WS_Identificador.Modelos;

namespace WS_Identificador
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class IdentificadorService : IIdentificadorService
    {
        private readonly IdentificadorSocketClient identificadorClient = new IdentificadorSocketClient();

        public RespuestaSaldo ConsultarSaldo(
            string numeroTelefono,
            string origenWeb,
            string tipoTransaccion)
        {
            const string mensajeError = "Usuario ya existe o datos incorrectos o incompletos.";

            try
            {
                if (string.IsNullOrWhiteSpace(numeroTelefono)
                    || !EsOrigenWeb(origenWeb)
                    || !string.Equals(tipoTransaccion, "saldo", StringComparison.OrdinalIgnoreCase))
                {
                    return CrearError(mensajeError);
                }

                RespuestaIdentificador respuesta = identificadorClient.ConsultarSaldoWeb(numeroTelefono);
                if (respuesta != null
                    && string.Equals(respuesta.Status, "OK", StringComparison.OrdinalIgnoreCase)
                    && !string.IsNullOrWhiteSpace(respuesta.Saldo))
                {
                    return new RespuestaSaldo
                    {
                        Resultado = true,
                        Mensaje = "ok",
                        Saldo = respuesta.Saldo
                    };
                }

                return CrearError(mensajeError);
            }
            catch
            {
                return CrearError(mensajeError);
            }
        }

        private static bool EsOrigenWeb(string origenWeb)
        {
            return string.Equals(origenWeb, "WEB", StringComparison.OrdinalIgnoreCase)
                || string.Equals(origenWeb, "true", StringComparison.OrdinalIgnoreCase)
                || string.Equals(origenWeb, "1", StringComparison.OrdinalIgnoreCase);
        }

        private static RespuestaSaldo CrearError(string mensaje)
        {
            return new RespuestaSaldo
            {
                Resultado = false,
                Mensaje = mensaje,
                Saldo = null
            };
        }
    }
}
