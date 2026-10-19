using Dapper;
using Entidades;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UsuarioRepository
{
    private readonly VeterinariaDbContext _context;

    public UsuarioRepository()
    {
        _context = new VeterinariaDbContext();
    }

    // Listar todos los usuarios 
    public IEnumerable<UsuarioConPersona> ListarUsuarios(bool soloActivos = true)
    {

        using var conn = _context.CreateConnection();
        return conn.Query<UsuarioConPersona>("sp_Usuario_Listar",
        new { solo_activos = soloActivos ? 1 : 0 },
        commandType: CommandType.StoredProcedure);

    }

    public UsuarioConPersona ObtenerUsuarioPorId(int idUsuario)
    {
        using var conn = _context.CreateConnection();
        // lo hago por consulta para no hacer un Sp, si lo ocupan pueden llamae este medoto
        string sql = @"
        SELECT u.id_usuario, u.id_persona, u.username, u.activo, u.id_rol AS IdRol,
               p.nombre AS PersonaNombre,
               r.nombre AS RolNombre,
               s.nombre AS SucursalNombre
        FROM Usuario u
        INNER JOIN Persona p ON p.id_persona = u.id_persona
        INNER JOIN Rol r ON r.id_rol = u.id_rol
        LEFT JOIN Sucursal s ON s.id_sucursal = p.id_sucursal
        WHERE u.id_usuario = @idUsuario";
        return conn.QueryFirstOrDefault<UsuarioConPersona>(sql, new { idUsuario });
    }

    public UsuarioSesion ValidarLogin(string username, string passwordPlain)
    {
        using var conn = _context.CreateConnection();
        return conn.QueryFirstOrDefault<UsuarioSesion>("sp_Usuario_ValidarLogin",
            new { username = username, password_hash = passwordPlain },
            commandType: CommandType.StoredProcedure);
    }

    // Crea U N  usuario
    public void CrearUsuario(int idPersona, string username, string passwordPlain, int idRol)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("sp_Usuario_Crear",
            new { id_persona = idPersona, username = username, password_hash = passwordPlain, id_rol = idRol },
            commandType: CommandType.StoredProcedure);
    }

    // Actualizar usuario (también permite reactivar con activo = true)
    public void ActualizarUsuario(int idUsuario, string nuevoUsername, string nuevaPasswordPlain, int nuevoIdRol, bool activo)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("sp_Usuario_Actualizar",
            new { id_usuario = idUsuario, username = nuevoUsername, password_hash = nuevaPasswordPlain, id_rol = nuevoIdRol, activo },
            commandType: CommandType.StoredProcedure);
    }

    // Desactivar usuario (borrado logico) 

    public void DesactivarUsuario(int idUsuario)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("sp_Usuario_Desactivar",
            new { id_usuario = idUsuario },
            commandType: CommandType.StoredProcedure);
    }

    
    


   


    public IEnumerable<Persona> ListarPersonasSinUsuario()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<Persona>("sp_Persona_SinUsuario_Listar",
            commandType: CommandType.StoredProcedure);
    }


    


}
