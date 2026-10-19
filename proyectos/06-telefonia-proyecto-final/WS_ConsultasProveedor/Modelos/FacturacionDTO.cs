using System.Runtime.Serialization;

namespace WS_ConsultasProveedor.Modelos
{
    [DataContract]
    public class FacturacionDTO
    {
        [DataMember]
        public string FechaCalculo { get; set; }

        [DataMember]
        public string FechaMaximaPago { get; set; }

        [DataMember]
        public bool Existe { get; set; }
    }
}
