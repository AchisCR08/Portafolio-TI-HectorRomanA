using Dapper;
using Entidades;

namespace Repositorio;

public class AtencionRepository
{
    private readonly VeterinariaDbContext _context = new();
    public IEnumerable<OpcionCatalogo> Mascotas() { using var c = _context.CreateConnection(); return c.Query<OpcionCatalogo>("SELECT id_mascota Id, nombre Nombre FROM Mascota WHERE activo=1 ORDER BY nombre"); }
    public IEnumerable<OpcionCatalogo> Personas() { using var c = _context.CreateConnection(); return c.Query<OpcionCatalogo>("SELECT id_persona Id, nombre Nombre FROM Persona WHERE activo=1 ORDER BY nombre"); }
    public IEnumerable<OpcionCatalogo> Veterinarios() { using var c = _context.CreateConnection(); return c.Query<OpcionCatalogo>("SELECT p.id_persona Id, p.nombre Nombre FROM Persona p INNER JOIN Usuario u ON u.id_persona = p.id_persona INNER JOIN Rol r ON r.id_rol = u.id_rol WHERE p.activo=1 AND u.activo=1 AND r.activo=1 AND r.nombre='Veterinario' ORDER BY p.nombre"); }
    public IEnumerable<OpcionCatalogo> Groomers() { using var c = _context.CreateConnection(); return c.Query<OpcionCatalogo>("SELECT p.id_persona Id, p.nombre Nombre FROM Persona p INNER JOIN Usuario u ON u.id_persona = p.id_persona INNER JOIN Rol r ON r.id_rol = u.id_rol WHERE p.activo=1 AND u.activo=1 AND r.activo=1 AND r.nombre='Groomer' ORDER BY p.nombre"); }
    public IEnumerable<OpcionCatalogo> Estados() { using var c = _context.CreateConnection(); return c.Query<OpcionCatalogo>("SELECT id_estado_cita Id, nombre Nombre FROM EstadoCita WHERE activo=1 ORDER BY nombre"); }
    public IEnumerable<OpcionCatalogo> Servicios() { using var c = _context.CreateConnection(); return c.Query<OpcionCatalogo>("SELECT id_servicio_grooming Id, nombre Nombre FROM ServicioGrooming WHERE activo=1 ORDER BY nombre"); }
    public IEnumerable<OpcionCatalogo> Vacunas() { using var c = _context.CreateConnection(); return c.Query<OpcionCatalogo>("SELECT id_vacuna Id, nombre Nombre FROM Vacuna WHERE activo=1 ORDER BY nombre"); }
    public IEnumerable<CitaRegistro> ListarCitas(bool grooming, int sucursal) { using var c = _context.CreateConnection(); var tabla = grooming ? "CitaGrooming" : "CitaVeterinaria"; var responsable = grooming ? "g.id_groomer" : "g.id_veterinario"; var servicio = grooming ? "sg.nombre" : "g.motivo"; var servicioId = grooming ? "g.id_servicio_grooming" : "CAST(NULL AS INT)"; var join = grooming ? "LEFT JOIN ServicioGrooming sg ON sg.id_servicio_grooming=g.id_servicio_grooming" : ""; return c.Query<CitaRegistro>($"SELECT g.{(grooming ? "id_cita_grooming" : "id_cita_vet")} Id,g.fecha Fecha,g.hora Hora,g.id_mascota IdMascota,{responsable} IdResponsable,g.id_estado_cita IdEstado,{servicioId} IdServicio,m.nombre Mascota,p.nombre Responsable,{servicio} Servicio,e.nombre Estado FROM {tabla} g JOIN Mascota m ON m.id_mascota=g.id_mascota JOIN Persona p ON p.id_persona={responsable} JOIN EstadoCita e ON e.id_estado_cita=g.id_estado_cita {join} WHERE g.id_sucursal=@sucursal ORDER BY g.fecha DESC,g.hora DESC", new { sucursal }); }
   
    public IEnumerable<VacunaRegistro> HistorialVacunas() { using var c = _context.CreateConnection(); return c.Query<VacunaRegistro>("SELECT h.id_historial IdHistorial,h.id_mascota IdMascota,h.id_vacuna IdVacuna,m.nombre Mascota,v.nombre Vacuna,h.fecha_aplicacion FechaAplicacion,h.proxima_dosis ProximaDosis FROM HistorialVacuna h JOIN Mascota m ON m.id_mascota=h.id_mascota JOIN Vacuna v ON v.id_vacuna=h.id_vacuna ORDER BY h.proxima_dosis"); }
    public IEnumerable<CitaRegistro> HistorialMedico(int sucursal) { using var c = _context.CreateConnection(); return c.Query<CitaRegistro>("SELECT cv.id_cita_vet Id,cv.fecha Fecha,cv.hora Hora,m.nombre Mascota,p.nombre Responsable,COALESCE(co.diagnostico,cv.motivo) Servicio,e.nombre Estado FROM CitaVeterinaria cv JOIN Mascota m ON m.id_mascota=cv.id_mascota JOIN Persona p ON p.id_persona=cv.id_veterinario JOIN EstadoCita e ON e.id_estado_cita=cv.id_estado_cita LEFT JOIN Consulta co ON co.id_cita_vet=cv.id_cita_vet WHERE cv.id_sucursal=@sucursal ORDER BY cv.fecha DESC,cv.hora DESC", new { sucursal }); }
    public ConsultaDetalle? ObtenerConsultaPorCita(int cita)
    {
        using var c = _context.CreateConnection();
        return c.QueryFirstOrDefault<ConsultaDetalle>(
            "SELECT diagnostico Diagnostico, tratamiento Tratamiento, " +
            "observaciones Observaciones FROM Consulta WHERE id_cita_vet = @cita",
            new { cita });
    }
    

    public void ActualizarCitaGrooming(
     int idCita,
     DateTime fecha,
     TimeSpan hora,
     int idMascota,
     int idGroomer,
     int idEstado,
     int idServicio)
    {
        using var conn = _context.CreateConnection();

        conn.Execute(
            "dbo.sp_CitaGrooming_Actualizar",
            new
            {
                id_cita_grooming = idCita,
                fecha,
                hora,
                id_mascota = idMascota,
                id_groomer = idGroomer,
                id_estado_cita = idEstado,
                id_servicio_grooming = idServicio
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public void ActualizarCitaVeterinaria(
        int idCita, DateTime fecha, TimeSpan hora, int idMascota,
        int idVeterinario, int idEstado, int idSucursal, string motivo)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("dbo.sp_CitaVeterinaria_Actualizar", new
        {
            id_cita_vet = idCita, fecha, hora, motivo,
            id_mascota = idMascota, id_veterinario = idVeterinario,
            id_estado_cita = idEstado, id_sucursal = idSucursal
        }, commandType: System.Data.CommandType.StoredProcedure);
    }

    public void CancelarCitaVeterinaria(int idCita)
    {
        using var conn = _context.CreateConnection();
        int? idEstadoCancelado = conn.QueryFirstOrDefault<int?>(
            "SELECT TOP 1 id_estado_cita FROM EstadoCita " +
            "WHERE UPPER(nombre) IN ('CANCELADA', 'CANCELADO') AND activo = 1");

        if (idEstadoCancelado == null)
            throw new Exception("No existe un estado activo llamado Cancelada.");

        conn.Execute("dbo.sp_CitaVeterinaria_CambiarEstado", new
        {
            id_cita_vet = idCita,
            id_estado_cita = idEstadoCancelado.Value
        }, commandType: System.Data.CommandType.StoredProcedure);
    }

    public void ActualizarHistorialVacuna(
        int idHistorial,
        int idMascota,
        int idVacuna,
        DateTime fechaAplicacion,
        DateTime? proximaDosis)
    {
        using var conn = _context.CreateConnection();

        conn.Execute(
            "dbo.sp_HistorialVacuna_Actualizar",
            new
            {
                id_historial = idHistorial,
                id_mascota = idMascota,
                id_vacuna = idVacuna,
                fecha_aplicacion = fechaAplicacion,
                proxima_dosis = proximaDosis
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public void CrearCita(
bool grooming,
DateTime fecha,
TimeSpan hora,
int mascota,
int responsable,
int estado,
int sucursal,
string motivo,
int? servicio)
    {
        using var conn = _context.CreateConnection();

        if (grooming)
        {
            if (servicio == null)
                throw new Exception("Debe seleccionar un servicio de grooming.");

            conn.Execute(
                "dbo.sp_CitaGrooming_Agendar",
                new
                {
                    fecha,
                    hora,
                    id_mascota = mascota,
                    id_groomer = responsable,
                    id_estado_cita = estado,
                    id_servicio_grooming = servicio.Value,
                    id_sucursal = sucursal,
                    precio_final = (decimal?)null
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
        else
        {
            conn.Execute(
                "dbo.sp_CitaVeterinaria_Agendar",
                new
                {
                    fecha,
                    hora,
                    motivo,
                    id_mascota = mascota,
                    id_veterinario = responsable,
                    id_estado_cita = estado,
                    id_sucursal = sucursal
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
    public void RegistrarVacuna(
    int mascota,
    int vacuna,
    DateTime fecha,
    DateTime? proxima)
    {
        using var conn = _context.CreateConnection();

        conn.Execute(
            "dbo.sp_HistorialVacuna_Registrar",
            new
            {
                id_mascota = mascota,
                id_vacuna = vacuna,
                fecha_aplicacion = fecha,
                proxima_dosis = proxima
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
    public void GuardarConsulta(
    int cita,
    string diagnostico,
    string tratamiento,
    string observaciones)
    {
        using var conn = _context.CreateConnection();

        int? idConsulta = conn.QueryFirstOrDefault<int?>(
            "SELECT id_consulta FROM Consulta WHERE id_cita_vet = @cita",
            new { cita });

        if (idConsulta == null)
        {
            conn.Execute(
                "dbo.sp_Consulta_Registrar",
                new
                {
                    id_cita_vet = cita,
                    diagnostico,
                    tratamiento,
                    observaciones
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
        else
        {
            conn.Execute(
                "dbo.sp_Consulta_Actualizar",
                new
                {
                    id_consulta = idConsulta.Value,
                    diagnostico,
                    tratamiento,
                    observaciones
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
        }
    }

