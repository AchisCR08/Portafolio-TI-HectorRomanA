using System.ComponentModel.DataAnnotations;

namespace Taller_Todo_Terreno.Models
{
    public class Cita
    {
        public int CitaId { get; set; }

        [Required]
        public int VehiculoId { get; set; }
        public Vehiculo? Vehiculo { get; set; }

        [Required]
        public int HorarioId { get; set; }
        public Horario? Horario { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        // "Activa" o "Cancelada". Si ya pasó la fecha y sigue "Activa", se muestra como "Completada" (calculado, no guardado).
        public string Estado { get; set; } = "Activa";

    }
}
