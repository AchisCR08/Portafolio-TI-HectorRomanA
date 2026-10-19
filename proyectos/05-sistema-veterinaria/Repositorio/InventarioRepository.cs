using Dapper;
using Entidades;
using Repositorio;
using System.Collections.Generic;
using System.Data;

public class InventarioRepository
{
    private readonly VeterinariaDbContext _context;

    public InventarioRepository()
    {
        _context = new VeterinariaDbContext();
    }

    public IEnumerable<InventarioSucursal> ConsultarInventario(int? idSucursal = null, int? idProducto = null)
    {
        using var conn = _context.CreateConnection();
        return conn.Query<InventarioSucursal>("sp_Inventario_Consultar",
            new
            {
                id_sucursal = idSucursal,
                id_producto = idProducto
            },
            commandType: CommandType.StoredProcedure);
    }

    public void GuardarInventario(int idSucursal, int idProducto, int stock)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("sp_Inventario_Guardar",
            new
            {
                id_sucursal = idSucursal,
                id_producto = idProducto,
                stock
            },
            commandType: CommandType.StoredProcedure);
    }

    public void EliminarInventario(int idSucursal, int idProducto)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("sp_Inventario_Eliminar",
            new
            {
                id_sucursal = idSucursal,
                id_producto = idProducto
            },
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<ProductoConDetalle> ListarProductosActivos()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<ProductoConDetalle>("sp_Producto_Listar",
            new { solo_activos = 1 },
            commandType: CommandType.StoredProcedure);
    }
}
