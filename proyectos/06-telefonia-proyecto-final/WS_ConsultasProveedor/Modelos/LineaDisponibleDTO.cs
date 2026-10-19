using System.Runtime.Serialization;

namespace WS_ConsultasProveedor.Modelos
{
    [DataContract]
    public class LineaDisponibleDTO
    {
        [DataMember]
        public string NumeroTelefono { get; set; }

        [DataMember]
        public string IdentificadorTarjeta { get; set; }

        // Se agrego para ADM4: la activacion exige el identificador del telefono.
        [DataMember]
        public string IdentificadorTelefono { get; set; }

        [DataMember]
        public string TipoServicio { get; set; }
    }
}
