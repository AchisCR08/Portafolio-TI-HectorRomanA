namespace Taller_Todo_Terreno.Models
{
    public class Horario
    {
        public int HorarioId { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public int CupoMaximo { get; set; } = 1;

        // Un horario puede tener varias citas (hasta el CupoMaximo)
        public ICollection<Cita>? Citas { get; set; }
    }
}
