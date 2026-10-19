using System.Runtime.Serialization;

namespace WS_Identificador.Modelos
{
    [DataContract]
    public class RespuestaSaldo
    {
        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public string Saldo { get; set; }
    }
}
