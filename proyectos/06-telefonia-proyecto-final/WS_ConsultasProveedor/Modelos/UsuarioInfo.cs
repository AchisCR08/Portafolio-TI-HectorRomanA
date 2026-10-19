using System.Runtime.Serialization;

namespace WS_ConsultasProveedor.Modelos
{
    // Copia cliente de WS_Autenticacion.Modelos.UsuarioInfo.
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_Autenticacion.Modelos")]
    public class UsuarioInfo
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
    }
}
