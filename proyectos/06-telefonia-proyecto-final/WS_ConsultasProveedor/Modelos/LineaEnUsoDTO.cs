using System.Runtime.Serialization;

namespace WS_ConsultasProveedor.Modelos
{
    [DataContract]
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
}
