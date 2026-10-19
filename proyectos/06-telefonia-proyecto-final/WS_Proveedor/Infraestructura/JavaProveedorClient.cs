using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Web.Script.Serialization;

namespace WS_Proveedor.Infraestructura
{
    public class JavaProveedorClient
    {
        private readonly JavaScriptSerializer serializador = new JavaScriptSerializer();

        public string RegistrarLineaDisponible(
            string numeroTelefono,
            string identificadorTelefono,
            string identificadorTarjeta,
            string tipoServicio,
            string estado)
        {
            var trama = new Dictionary<string, object>
            {
                { "tipo", "registrar_linea" },
                { "telefono", numeroTelefono },
                { "identificadorTelefono", identificadorTelefono },
                { "identificadorTarjeta", identificadorTarjeta },
                { "tipoServicio", tipoServicio },
                { "estado", estado }
            };

            Dictionary<string, object> respuesta = EnviarTrama(trama);
            return ObtenerTexto(respuesta, "status");
        }

        public string CambiarEstadoLinea(
            string numeroTelefono,
            string identificadorTelefono,
            string identificadorTarjeta,
            string tipoServicio,
            string identificacionDuenno,
            string estado)
        {
            var trama = new Dictionary<string, object>
            {
                { "tipo", "cambiar_estado_linea" },
                { "telefono", numeroTelefono },
                { "identificadorTelefono", identificadorTelefono },
                { "identificadorTarjeta", identificadorTarjeta },
                { "tipoServicio", tipoServicio },
                { "identificacionDuenno", identificacionDuenno },
                { "estado", estado }
            };

            return ObtenerTexto(EnviarTrama(trama), "status");
        }

        public string CalcularFacturacionPostpago(string fechaCalculo, string fechaMaximaPago)
        {
            var trama = new Dictionary<string, object>
            {
                { "tipo", "calcular_facturacion_postpago" },
                { "fechaCalculo", fechaCalculo },
                { "fechaMaximaPago", fechaMaximaPago }
            };

            return ObtenerTexto(EnviarTrama(trama), "status");
        }

        private Dictionary<string, object> EnviarTrama(Dictionary<string, object> trama)
        {
            string host = ConfigurationManager.AppSettings["ProveedorJava.Host"] ?? "localhost";
            int puerto = ObtenerEntero("ProveedorJava.Puerto", 6000);
            int timeout = ObtenerEntero("ProveedorJava.TimeoutMs", 5000);
            string json = serializador.Serialize(trama);

            using (TcpClient cliente = new TcpClient())
            {
                IAsyncResult conexion = cliente.BeginConnect(host, puerto, null, null);
                if (!conexion.AsyncWaitHandle.WaitOne(timeout))
                {
                    throw new TimeoutException("No se pudo conectar con el proveedor Java.");
                }

                cliente.EndConnect(conexion);
                cliente.ReceiveTimeout = timeout;
                cliente.SendTimeout = timeout;

                using (NetworkStream stream = cliente.GetStream())
                using (StreamWriter salida = new StreamWriter(stream, new UTF8Encoding(false)))
                using (StreamReader entrada = new StreamReader(stream, Encoding.UTF8))
                {
                    salida.AutoFlush = true;
                    salida.WriteLine(json);

                    string respuestaTexto = entrada.ReadLine();
                    if (string.IsNullOrWhiteSpace(respuestaTexto))
                    {
                        throw new InvalidOperationException("El proveedor Java no devolvio respuesta.");
                    }

                    return serializador.Deserialize<Dictionary<string, object>>(respuestaTexto);
                }
            }
        }

        private static int ObtenerEntero(string llave, int valorDefecto)
        {
            string valor = ConfigurationManager.AppSettings[llave];
            int convertido;
            return int.TryParse(valor, out convertido) ? convertido : valorDefecto;
        }

        private static string ObtenerTexto(Dictionary<string, object> datos, string llave)
        {
            if (datos == null || !datos.ContainsKey(llave) || datos[llave] == null)
            {
                return string.Empty;
            }

            return Convert.ToString(datos[llave]);
        }
    }
}
