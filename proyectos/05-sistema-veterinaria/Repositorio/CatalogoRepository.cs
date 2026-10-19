using Entidades;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

public class CatalogoRepository



{

    /*-----------algunos metodos de utilidad, mejor hacerlo aqui porque con Procedimientos se hacen un monton*/
    private readonly VeterinariaDbContext _context;
    public CatalogoRepository() => _context = new VeterinariaDbContext();

    public IEnumerable<Rol> ListarRolesActivos()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<Rol>("SELECT id_rol, nombre FROM Rol WHERE activo = 1 ORDER BY nombre");
    }

    public IEnumerable<Sucursal> ListarSucursalesActivas()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<Sucursal>("SELECT id_sucursal, nombre FROM Sucursal WHERE activo = 1 ORDER BY nombre");
    }

    public IEnumerable<Provincia> ListarProvincias()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<Provincia>("SELECT id_provincia, nombre FROM Provincia ORDER BY nombre");
    }

    public IEnumerable<Canton> ListarCantonesPorProvincia(int idProvincia)
    {
        using (var conn = _context.CreateConnection())
        {
            string sql = @"
            SELECT 
                id_canton AS IdCanton, 
                nombre AS Nombre, 
                id_provincia AS IdProvincia
            FROM Canton 
            WHERE id_provincia = @idProvincia 
            ORDER BY nombre";
            return conn.Query<Canton>(sql, new { idProvincia });
        }
    }

    public IEnumerable<Distrito> ListarDistritosPorCanton(int idCanton)
    {
        using (var conn = _context.CreateConnection())
        {
            string sql = @"
            SELECT 
                id_distrito AS IdDistrito, 
                nombre AS Nombre, 
                id_canton AS IdCanton
            FROM Distrito 
            WHERE id_canton = @idCanton 
            ORDER BY nombre";
            return conn.Query<Distrito>(sql, new { idCanton });
        }
    }



    public UbicacionInfo ObtenerUbicacionPorDistrito(int idDistrito)
    {

        using (var conn = _context.CreateConnection())
        {
            string sql = @"
            SELECT 
                d.id_distrito AS IdDistrito, 
                d.id_canton AS IdCanton, 
                c.id_provincia AS IdProvincia,
                d.nombre AS NombreDistrito,
                c.nombre AS NombreCanton,
                p.nombre AS NombreProvincia
            FROM Distrito d
            INNER JOIN Canton c ON c.id_canton = d.id_canton
            INNER JOIN Provincia p ON p.id_provincia = c.id_provincia
            WHERE d.id_distrito = @idDistrito";
            return conn.QueryFirstOrDefault<UbicacionInfo>(sql, new { idDistrito });
        }

    }

    public class UbicacionInfo
    {
        public int IdDistrito { get; set; }
        public int IdCanton { get; set; }
        public int IdProvincia { get; set; }
        public string NombreDistrito { get; set; }
        public string NombreCanton { get; set; }
        public string NombreProvincia { get; set; }
    }
}