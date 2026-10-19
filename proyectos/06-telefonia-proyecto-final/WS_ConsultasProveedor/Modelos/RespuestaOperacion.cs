using System.Runtime.Serialization;

namespace WS_ConsultasProveedor.Modelos
{
    [DataContract]
    public class RespuestaOperacion
    {
        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }
    }
}
