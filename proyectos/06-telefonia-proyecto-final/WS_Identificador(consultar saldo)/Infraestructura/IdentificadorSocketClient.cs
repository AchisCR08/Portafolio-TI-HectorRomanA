using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Web.Script.Serialization;

namespace WS_Identificador.Infraestructura
{
    public class IdentificadorSocketClient
    {
        private readonly JavaScriptSerializer serializador = new JavaScriptSerializer();

        public RespuestaIdentificador ConsultarSaldoWeb(string numeroTelefono)
        {
            var trama = new Dictionary<string, object>
            {
                { "tipo", "saldo" },
                { "telefono", numeroTelefono },
                { "origenWeb", true },
                { "tipoTransaccion", "saldo" }
            };

            Dictionary<string, object> respuesta = EnviarTrama(trama);
            return new RespuestaIdentificador
            {
                Status = ObtenerTexto(respuesta, "status"),
                Saldo = ObtenerTexto(respuesta, "saldo")
            };
        }

        private Dictionary<string, object> EnviarTrama(Dictionary<string, object> trama)
        {
            string host = ConfigurationManager.AppSettings["Identificador.Host"] ?? "localhost";
            int puerto = ObtenerEntero("Identificador.Puerto", 8000);
            int timeout = ObtenerEntero("Identificador.TimeoutMs", 5000);

            using (TcpClient cliente = new TcpClient())
            {
                IAsyncResult conexion = cliente.BeginConnect(host, puerto, null, null);
                if (!conexion.AsyncWaitHandle.WaitOne(timeout))
                {
                    throw new TimeoutException("No se pudo conectar con el identificador.");
                }

                cliente.EndConnect(conexion);
                cliente.ReceiveTimeout = timeout;
                cliente.SendTimeout = timeout;

                using (NetworkStream stream = cliente.GetStream())
                using (StreamWriter salida = new StreamWriter(stream, new UTF8Encoding(false)))
                using (StreamReader entrada = new StreamReader(stream, Encoding.UTF8))
                {
                    salida.AutoFlush = true;
                    salida.WriteLine(serializador.Serialize(trama));

                    string respuestaTexto = entrada.ReadLine();
                    if (string.IsNullOrWhiteSpace(respuestaTexto))
                    {
                        throw new InvalidOperationException("El identificador no devolvio respuesta.");
                    }

                    return serializador.Deserialize<Dictionary<string, object>>(respuestaTexto);
                }
            }
        }

        private static int ObtenerEntero(string llave, int valorDefecto)
        {
            int valor;
            return int.TryParse(ConfigurationManager.AppSettings[llave], out valor) ? valor : valorDefecto;
        }

        private static string ObtenerTexto(Dictionary<string, object> datos, string llave)
        {
            return datos != null && datos.ContainsKey(llave) && datos[llave] != null
                ? Convert.ToString(datos[llave])
                : string.Empty;
        }
    }

    public class RespuestaIdentificador
    {
        public string Status { get; set; }
        public string Saldo { get; set; }
    }
}
