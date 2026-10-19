using System.Runtime.Serialization;
using System.ServiceModel;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Infraestructura
{
    // Copia cliente de WS_Proveedor.Modelos.RespuestaServicio.
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_Proveedor.Modelos")]
    public class RespuestaServicio
    {
        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }
    }

    // Contrato cliente para consumir WS_Proveedor (socket a Java).
    [ServiceContract]
    public interface IProveedorService
    {
        [OperationContract]
        RespuestaServicio RegistrarServicioDisponible(
            string numeroTelefono,
            string identificadorTelefono,
            string identificadorTarjeta,
            string tipoServicio,
            string estado);

        [OperationContract]
        RespuestaServicio ActivarDesactivarLinea(
            string numeroTelefono,
            string identificadorTelefono,
            string identificadorTarjeta,
            string tipoServicio,
            string identificacionDuenno,
            string estado);

        [OperationContract]
        RespuestaServicio CalcularFacturacionPostpago(
            string fechaCalculo,
            string fechaMaximaPago);
    }

    public class ProveedorServiceClient
    {
        public RespuestaServicio RegistrarServicioDisponible(
            string numeroTelefono,
            string identificadorTelefono,
            string identificadorTarjeta,
            string tipoServicio,
            string estado)
        {
            using (ChannelFactory<IProveedorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().RegistrarServicioDisponible(
                    numeroTelefono, identificadorTelefono, identificadorTarjeta, tipoServicio, estado);
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
            using (ChannelFactory<IProveedorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().ActivarDesactivarLinea(
                    numeroTelefono, identificadorTelefono, identificadorTarjeta,
                    tipoServicio, identificacionDuenno, estado);
            }
        }

        public RespuestaServicio CalcularFacturacionPostpago(
            string fechaCalculo,
            string fechaMaximaPago)
        {
            using (ChannelFactory<IProveedorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().CalcularFacturacionPostpago(fechaCalculo, fechaMaximaPago);
            }
        }

        private static ChannelFactory<IProveedorService> CrearFabrica()
        {
            return new ChannelFactory<IProveedorService>(
                new BasicHttpBinding(),
                new EndpointAddress(ConfiguracionWS.UrlProveedor));
        }
    }
}
