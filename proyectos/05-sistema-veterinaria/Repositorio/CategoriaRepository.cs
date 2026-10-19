using Dapper;
using Entidades;
using System.Collections.Generic;
using System.Data;

namespace Repositorio
{
    public class CategoriaRepository
    {
        private readonly VeterinariaDbContext _context = new();

        public IEnumerable<Categoria> Listar(bool soloActivas = true)
        {
            using var conn = _context.CreateConnection();
            return conn.Query<Categoria>("sp_Categoria_Listar",
                new { solo_activas = soloActivas ? 1 : 0 },
                commandType: CommandType.StoredProcedure);
        }

        public int Crear(string nombre)
        {
            using var conn = _context.CreateConnection();
            return conn.QueryFirstOrDefault<int>("sp_Categoria_Crear",
                new { nombre },
                commandType: CommandType.StoredProcedure);
        }

        public void Actualizar(int idCategoria, string nombre, bool activo)
        {
            using var conn = _context.CreateConnection();
            conn.Execute("sp_Categoria_Actualizar",
                new
                {
                    id_categoria = idCategoria,
                    nombre,
                    activo
                },
                commandType: CommandType.StoredProcedure);
        }

        public void Eliminar(int idCategoria)
        {
            using var conn = _context.CreateConnection();
            conn.Execute("sp_Categoria_Eliminar",
                new { id_categoria = idCategoria },
                commandType: CommandType.StoredProcedure);
        }
    }
}
