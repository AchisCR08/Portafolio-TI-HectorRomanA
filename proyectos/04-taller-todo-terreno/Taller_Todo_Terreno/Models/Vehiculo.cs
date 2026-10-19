using System.ComponentModel.DataAnnotations;

namespace Taller_Todo_Terreno.Models
{
    public class Vehiculo
    {
        public int VehiculoId { get; set; }

        [Required(ErrorMessage = "La placa es obligatoria")]
        [StringLength(10, ErrorMessage = "La placa no puede tener más de 10 caracteres")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(1980, 2027, ErrorMessage = "El año debe estar entre 1980 y 2027")]
        public int Año { get; set; }

        [Required(ErrorMessage = "El tipo de transmisión es obligatorio")]
        public string TipoTransmision { get; set; }

        public int DuenoID { get; set; }
        public Dueno? Dueno { get; set; }
        public ICollection<Cita>? Citas { get; set; }



    }
}
