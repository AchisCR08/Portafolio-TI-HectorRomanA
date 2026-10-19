using System.Runtime.Serialization;

namespace WS_Proveedor.Modelos
{
    [DataContract]
    public class RespuestaServicio
    {
        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }
    }
}
