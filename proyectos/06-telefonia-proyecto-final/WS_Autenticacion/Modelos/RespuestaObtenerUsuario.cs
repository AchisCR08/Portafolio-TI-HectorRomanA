using System.Runtime.Serialization;

namespace WS_Autenticacion.Modelos
{
    // Respuesta de ObtenerUsuario: datos basicos del usuario 
    [DataContract]
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
