using System.Runtime.Serialization;
using System.ServiceModel;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Infraestructura
{
    // Copia cliente de WS_Identificador.Modelos.RespuestaSaldo.
    // El Namespace debe coincidir EXACTAMENTE con el servidor para que
    // DataContractSerializer deserialice el SOAP correctamente.
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_Identificador.Modelos")]
    public class RespuestaSaldo
    {
        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public string Saldo { get; set; }
    }

    // Contrato cliente para consumir WS_Identificador (consultar saldo).
    [ServiceContract]
    public interface IIdentificadorService
    {
        [OperationContract]
        RespuestaSaldo ConsultarSaldo(
            string numeroTelefono,
            string origenWeb,
            string tipoTransaccion);
    }

    public class IdentificadorServiceClient
    {
        public RespuestaSaldo ConsultarSaldo(string numeroTelefono, string origenWeb, string tipoTransaccion)
        {
            using (ChannelFactory<IIdentificadorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().ConsultarSaldo(numeroTelefono, origenWeb, tipoTransaccion);
            }
        }

        private static ChannelFactory<IIdentificadorService> CrearFabrica()
        {
            return new ChannelFactory<IIdentificadorService>(
                new BasicHttpBinding(),
                new EndpointAddress(ConfiguracionWS.UrlIdentificador));
        }
    }
}
