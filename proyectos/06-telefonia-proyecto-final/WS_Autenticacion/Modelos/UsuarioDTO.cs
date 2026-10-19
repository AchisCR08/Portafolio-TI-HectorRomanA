using System.Runtime.Serialization;

namespace WS_Autenticacion.Modelos
{
    // Datos de un usuario para el mantenimiento de usuarios administrativos (ADM7).
    // Usuario y contrasena llegan descifrados para poder mostrarse en pantalla.
    [DataContract]
    public class UsuarioDTO
    {
        [DataMember]
        public string Identificacion { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string PrimerApellido { get; set; }

        [DataMember]
        public string SegundoApellido { get; set; }

        [DataMember]
        public string CorreoElectronico { get; set; }

        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Contrasena { get; set; }

        [DataMember]
        public string Estado { get; set; }
    }
}
