namespace Entidades
{
    public class PersonaCombo
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; } = "";
    }

    public class TipoPagoCombo
    {
        public int IdTipoPago { get; set; }
        public string Nombre { get; set; } = "";
    }

    public class CitaVetCombo
    {
        public int IdCitaVet { get; set; }
        public string MascotaNombre { get; set; } = "";
        public string VeterinarioNombre { get; set; } = "";
        public DateTime Fecha { get; set; }  // DateTime: Dapper no soporta DateOnly con SqlClient
        public TimeSpan Hora { get; set; }
        public string Estado { get; set; } = "";

        // Texto que se muestra en el ComboBox
        public string Display =>
            $"{Fecha:dd/MM/yyyy} {DateTime.Today.Add(Hora):hh:mm tt} — {MascotaNombre} ({VeterinarioNombre})";
    }

    public class CitaGroomingCombo
    {
        public int IdCitaGrooming { get; set; }
        public string MascotaNombre { get; set; } = "";
        public string ServicioNombre { get; set; } = "";
        public DateTime Fecha { get; set; }  // DateTime: Dapper no soporta DateOnly con SqlClient
        public TimeSpan Hora { get; set; }
        public decimal PrecioFinal { get; set; }
        public string Estado { get; set; } = "";

        // Texto que se muestra en el ComboBox
        public string Display =>
            $"{Fecha:dd/MM/yyyy} {DateTime.Today.Add(Hora):hh:mm tt} — {MascotaNombre} / {ServicioNombre}";
    }
}
