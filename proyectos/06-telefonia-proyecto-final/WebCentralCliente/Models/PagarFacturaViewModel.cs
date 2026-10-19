using WebCentralCliente.Infraestructura;

namespace WebCentralCliente.Models
{
    // Datos del formulario de pago con tarjeta (CLIENTE6).
    public class PagarFacturaViewModel
    {
        public string Telefono { get; set; }

        public string NumeroTarjeta { get; set; }

        public string NombreTarjeta { get; set; }

        public string MesVencimiento { get; set; }

        public string AnioVencimiento { get; set; }

        public string CodigoSeguridad { get; set; }

        // Solo visualizacion: el monto de la factura no es modificable por el usuario.
        public decimal MontoFactura { get; set; }
    }

    // Combina la lista de lineas postpago con el formulario de pago,
    // para la pantalla CLIENTE6 (que hace ambas cosas segun el caso).
    public class PagarFacturaPageViewModel
    {
        public LineaClienteDTO[] Postpago { get; set; }

        public bool MostrarFormulario { get; set; }

        public decimal MontoFacturaPendiente { get; set; }

        public PagarFacturaViewModel Formulario { get; set; }
    }
}
