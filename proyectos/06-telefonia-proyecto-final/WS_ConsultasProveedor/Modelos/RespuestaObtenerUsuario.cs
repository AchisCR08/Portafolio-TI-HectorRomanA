using System.Runtime.Serialization;

namespace WS_ConsultasProveedor.Modelos
{
    // Copia cliente de WS_Autenticacion.Modelos.RespuestaObtenerUsuario.
    
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_Autenticacion.Modelos")]
    public class RespuestaObtenerUsuario
    {
        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public UsuarioInfo Usuario { get; set; }
    }
}
