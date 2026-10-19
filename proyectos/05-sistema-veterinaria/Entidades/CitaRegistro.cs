namespace Entidades;

public class CitaRegistro
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
    public int IdMascota { get; set; }
    public int IdResponsable { get; set; }
    public int IdEstado { get; set; }
    public int? IdServicio { get; set; }
    public string Mascota { get; set; } = "";
    public string Responsable { get; set; } = "";
    public string Servicio { get; set; } = "";
    public string Estado { get; set; } = "";
}

public class OpcionCatalogo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
}

public class VacunaRegistro
{
    public int IdHistorial { get; set; }
    public int IdMascota { get; set; }
    public int IdVacuna { get; set; }
    public string Mascota { get; set; } = "";
    public string Vacuna { get; set; } = "";
    public DateTime? FechaAplicacion { get; set; }
    public DateTime? ProximaDosis { get; set; }
}

public class ConsultaDetalle
{
    public string Diagnostico { get; set; } = "";
    public string Tratamiento { get; set; } = "";
    public string Observaciones { get; set; } = "";
}
