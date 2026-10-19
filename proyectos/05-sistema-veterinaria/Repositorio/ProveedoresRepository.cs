using Dapper;
using Entidades;
using System.Collections.Generic;
using System.Data;

namespace Repositorio
{
    public class ProveedoresRepository
    {
        private readonly VeterinariaDbContext _context = new();

        public IEnumerable<Proveedor> Listar(bool soloActivos = true)
        {
            using var conn = _context.CreateConnection();
            return conn.Query<Proveedor>("sp_Proveedor_Listar",
                new { solo_activos = soloActivos ? 1 : 0 },
                commandType: CommandType.StoredProcedure);
        }

        public int Crear(string nombre, string telefono, string correo)
        {
            using var conn = _context.CreateConnection();
            return conn.QueryFirstOrDefault<int>("sp_Proveedor_Crear",
                new { nombre, telefono, correo },
                commandType: CommandType.StoredProcedure);
        }

        public void Actualizar(int idProveedor, string nombre, string telefono, string correo, bool activo)
        {
            using var conn = _context.CreateConnection();
            conn.Execute("sp_Proveedor_Actualizar",
                new
                {
                    id_proveedor = idProveedor,
                    nombre,
                    telefono,
                    correo,
                    activo
                },
                commandType: CommandType.StoredProcedure);
        }

        public void Eliminar(int idProveedor)
        {
            using var conn = _context.CreateConnection();
            conn.Execute("sp_Proveedor_Eliminar",
                new { id_proveedor = idProveedor },
                commandType: CommandType.StoredProcedure);
        }
    }
}
