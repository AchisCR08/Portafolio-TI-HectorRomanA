using Dapper;
using Entidades;

namespace Repositorio;

public class GestionMascotasRepository
{
    private readonly VeterinariaDbContext _context = new();

    public IEnumerable<Persona> ListarPersonas()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<Persona>("SELECT id_persona IdPersona, nombre Nombre, telefono Telefono, correo Correo, direccion Direccion, id_distrito IdDistrito, id_sucursal IdSucursal, activo Activo FROM Persona ORDER BY activo DESC, nombre");
    }

    public void GuardarPersona(Persona persona)
    {
        using var conn = _context.CreateConnection();
        if (persona.IdPersona == 0)
            conn.Execute("dbo.sp_Persona_Crear", new { nombre = persona.Nombre, telefono = persona.Telefono, correo = persona.Correo, direccion = persona.Direccion, id_distrito = persona.IdDistrito, id_sucursal = persona.IdSucursal }, commandType: System.Data.CommandType.StoredProcedure);
        else
            conn.Execute("dbo.sp_Persona_Actualizar", new { id_persona = persona.IdPersona, nombre = persona.Nombre, telefono = persona.Telefono, correo = persona.Correo, direccion = persona.Direccion, id_distrito = persona.IdDistrito, id_sucursal = persona.IdSucursal, activo = persona.Activo }, commandType: System.Data.CommandType.StoredProcedure);
    }

    public void DesactivarPersona(int idPersona)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("dbo.sp_Persona_Eliminar", new { id_persona = idPersona }, commandType: System.Data.CommandType.StoredProcedure);
    }

    public IEnumerable<OpcionCatalogo> ListarRazas()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<OpcionCatalogo>("SELECT id_raza Id, nombre Nombre FROM Raza WHERE activo = 1 ORDER BY nombre");
    }

    public IEnumerable<MascotaGestion> ListarMascotas()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<MascotaGestion>("SELECT m.id_mascota IdMascota, m.nombre Nombre, m.fecha_nacimiento FechaNacimiento, m.sexo Sexo, m.peso_actual PesoActual, m.id_raza IdRaza, r.nombre Raza, m.activo Activo FROM Mascota m INNER JOIN Raza r ON r.id_raza = m.id_raza ORDER BY m.activo DESC, m.nombre");
    }

    public void GuardarMascota(MascotaGestion mascota, int? idDuenoPrincipal)
    {
        using var conn = _context.CreateConnection();
        if (mascota.IdMascota == 0)
            conn.Execute("dbo.sp_Mascota_Crear", new { nombre = mascota.Nombre, fecha_nacimiento = mascota.FechaNacimiento, sexo = mascota.Sexo, peso_actual = mascota.PesoActual, id_raza = mascota.IdRaza, id_dueno_principal = idDuenoPrincipal }, commandType: System.Data.CommandType.StoredProcedure);
        else
            conn.Execute("dbo.sp_Mascota_Actualizar", new { id_mascota = mascota.IdMascota, nombre = mascota.Nombre, fecha_nacimiento = mascota.FechaNacimiento, sexo = mascota.Sexo, peso_actual = mascota.PesoActual, id_raza = mascota.IdRaza, activo = mascota.Activo }, commandType: System.Data.CommandType.StoredProcedure);
    }

    public void DesactivarMascota(int idMascota)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("dbo.sp_Mascota_Eliminar", new { id_mascota = idMascota, eliminacion_fisica = false }, commandType: System.Data.CommandType.StoredProcedure);
    }

    public IEnumerable<DuenoMascotaRegistro> ListarDuenosMascotas()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<DuenoMascotaRegistro>("SELECT dm.id_persona IdPersona, dm.id_mascota IdMascota, p.nombre Dueno, m.nombre Mascota, dm.es_principal EsPrincipal FROM Duenos_Mascotas dm INNER JOIN Persona p ON p.id_persona = dm.id_persona INNER JOIN Mascota m ON m.id_mascota = dm.id_mascota ORDER BY m.nombre, dm.es_principal DESC, p.nombre");
    }

    public void AsignarDueno(int idPersona, int idMascota, bool esPrincipal)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("dbo.sp_Mascota_AsignarDueno", new { id_persona = idPersona, id_mascota = idMascota, es_principal = esPrincipal }, commandType: System.Data.CommandType.StoredProcedure);
    }

    public void QuitarDueno(int idPersona, int idMascota)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("DELETE FROM Duenos_Mascotas WHERE id_persona = @idPersona AND id_mascota = @idMascota", new { idPersona, idMascota });
    }
}
