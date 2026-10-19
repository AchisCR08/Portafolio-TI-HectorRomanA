namespace Entidades;

public class MascotaGestion
{
    public int IdMascota { get; set; }
    public string Nombre { get; set; } = "";
    public DateTime? FechaNacimiento { get; set; }
    public string? Sexo { get; set; }
    public decimal? PesoActual { get; set; }
    public int IdRaza { get; set; }
    public string Raza { get; set; } = "";
    public bool Activo { get; set; }
}

public class DuenoMascotaRegistro
{
    public int IdPersona { get; set; }
    public int IdMascota { get; set; }
    public string Dueno { get; set; } = "";
    public string Mascota { get; set; } = "";
    public bool EsPrincipal { get; set; }
}
