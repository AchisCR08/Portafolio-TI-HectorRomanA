using System.Runtime.Serialization;

namespace WS_Autenticacion.Modelos
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
