using System.ComponentModel.DataAnnotations;

namespace WebCentralAdmin.Models
{
    // Datos del formulario "Nuevo" (ADM3): registrar una linea disponible.
    // El estado siempre es DISPONIBLE (no se pide al usuario).
    public class LineaNuevaViewModel
    {
        [Required(ErrorMessage = "El número de teléfono es requerido.")]
        [RegularExpression(@"^\d{8,15}$", ErrorMessage = "El número de teléfono debe tener entre 8 y 15 dígitos.")]
        public string NumeroTelefono { get; set; }

        [Required(ErrorMessage = "El identificador del teléfono es requerido.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "El identificador del teléfono debe tener 16 dígitos.")]
        public string IdentificadorTelefono { get; set; }

        [Required(ErrorMessage = "El identificador de la tarjeta es requerido.")]
        [RegularExpression(@"^\d{19}$", ErrorMessage = "El identificador de la tarjeta debe tener 19 dígitos.")]
        public string IdentificadorTarjeta { get; set; }

        [Required(ErrorMessage = "Seleccione el tipo de servicio.")]
        public string TipoServicio { get; set; }
    }
}
