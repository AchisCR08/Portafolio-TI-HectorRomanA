using System.ComponentModel.DataAnnotations;

namespace WebCentralAdmin.Models
{
    // ADM6: formulario de facturacion postpago.
    // Muestra la ultima facturacion y captura las fechas del nuevo proceso.
    public class FacturacionViewModel
    {
        public bool ExisteUltima { get; set; }

        public string UltimaFechaCalculo { get; set; }

        public string UltimaFechaMaximaPago { get; set; }

        [Required(ErrorMessage = "La fecha de cálculo es requerida.")]
        public string FechaCalculo { get; set; }

        [Required(ErrorMessage = "La fecha máxima de pago es requerida.")]
        public string FechaMaximaPago { get; set; }
    }
}
