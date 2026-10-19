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

        // Se llena solo en Autenticar() cuando el login es exitoso.
        // Las webs (Admin/Cliente) la necesitan para saber que usuario
        // quedo logueado y poder consultar despues sus datos/lineas.
        [DataMember]
        public string Identificacion { get; set; }
    }
}
