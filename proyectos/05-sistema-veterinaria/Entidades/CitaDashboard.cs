namespace Entidades
{
    public class CitaDashboard
    {
        public TimeSpan Hora { get; set; }
        public string Paciente { get; set; } = "";
        public string Veterinario { get; set; } = "";
        public string Motivo { get; set; } = "";
        public string Estado { get; set; } = "";
        public string TipoCita { get; set; } = "";  // "Veterinaria" o "Grooming"
    }
}
