using WebCentralCliente.Infraestructura;

namespace WebCentralCliente.Models
{
    public class RecargaViewModel
    {
        public string Telefono { get; set; }

        public string NumeroTarjeta { get; set; }

        public string NombreTarjeta { get; set; }

        public string MesVencimiento { get; set; }

        public string AnioVencimiento { get; set; }

        public string CodigoSeguridad { get; set; }

        public string Monto { get; set; }
    }

    // Combina la lista de lineas prepago con el formulario de carga
    // para la pantalla CLIENTE5 (que hace ambas cosas segun el caso).
    public class RecargarSaldoPageViewModel
    {
        public LineaClienteDTO[] Prepago { get; set; }

        public bool MostrarFormulario { get; set; }

        public decimal SaldoActual { get; set; }

        public RecargaViewModel Formulario { get; set; }
    }
}
