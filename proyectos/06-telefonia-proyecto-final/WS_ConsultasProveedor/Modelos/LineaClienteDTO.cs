using System.Runtime.Serialization;

namespace WS_ConsultasProveedor.Modelos
{
    [DataContract]
    public class LineaClienteDTO
    {
        [DataMember]
        public string NumeroTelefono { get; set; }

        [DataMember]
        public string TipoServicio { get; set; }

        [DataMember]
        public decimal Saldo { get; set; }

        [DataMember]
        public decimal MontoFacturaPendiente { get; set; }

        // Campos extra para CLIENTE7 (devolucion): se reenvian tal cual a
        // WS_Proveedor.ActivarDesactivarLinea (espejo de LineaEnUsoDTO).
        [DataMember]
        public string NumeroTelefonoCifrado { get; set; }

        [DataMember]
        public string IdentificadorTarjeta { get; set; }

        [DataMember]
        public string IdentificadorTelefono { get; set; }

        [DataMember]
        public string IdentificacionCliente { get; set; }

        [DataMember]
        public string IdentificacionClienteCifrado { get; set; }

        [DataMember]
        public string IdentificadorTarjetaCifrado { get; set; }

        [DataMember]
        public string IdentificadorTelefonoCifrado { get; set; }
    }
}
