using System.Runtime.Serialization;

namespace WebCentralCliente.Infraestructura
{
    // Copias cliente de WS_ConsultasProveedor.Modelos.
    // El Namespace debe coincidir EXACTAMENTE con el servidor para que
    // DataContractSerializer deserialice el SOAP correctamente.
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_ConsultasProveedor.Modelos")]
    public class LineaDisponibleDTO
    {
        [DataMember]
        public string NumeroTelefono { get; set; }

        [DataMember]
        public string IdentificadorTarjeta { get; set; }

        [DataMember]
        public string TipoServicio { get; set; }
    }

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_ConsultasProveedor.Modelos")]
    public class LineaEnUsoDTO
    {
        [DataMember]
        public string NumeroTelefono { get; set; }

        [DataMember]
        public string IdentificadorTarjeta { get; set; }

        [DataMember]
        public string IdentificadorTelefono { get; set; }

        [DataMember]
        public string IdentificacionCliente { get; set; }

        [DataMember]
        public string IdentificacionClienteCifrado { get; set; }

        [DataMember]
        public string NombreCliente { get; set; }

        [DataMember]
        public string TipoServicio { get; set; }

        [DataMember]
        public string NumeroTelefonoCifrado { get; set; }

        [DataMember]
        public string IdentificadorTarjetaCifrado { get; set; }

        [DataMember]
        public string IdentificadorTelefonoCifrado { get; set; }
    }

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_ConsultasProveedor.Modelos")]
    public class LineaClienteDTO
    {
        [DataMember]
        public string NumeroTelefono { get; set; }

        [DataMember]
        public string TipoServicio { get; set; }

        [DataMember]
        public decimal Saldo { get; set; }

        [DataMember]
        public decimal MontoFacturaPendiente { get; set; }
    }

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_ConsultasProveedor.Modelos")]
    public class FacturacionDTO
    {
        [DataMember]
        public string FechaCalculo { get; set; }

        [DataMember]
        public string FechaMaximaPago { get; set; }

        [DataMember]
        public bool Existe { get; set; }
    }

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_ConsultasProveedor.Modelos")]
    public class RespuestaOperacion
    {
        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }
    }
}
