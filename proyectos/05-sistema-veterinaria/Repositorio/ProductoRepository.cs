using Dapper;
using Entidades;
using Repositorio;
using System.Collections.Generic;
using System.Data;

public class ProductoRepository
{
    private readonly VeterinariaDbContext _context;

    public ProductoRepository()
    {
        _context = new VeterinariaDbContext();
    }

    public IEnumerable<ProductoConDetalle> ListarProductos(bool soloActivos = true)
    {
        using var conn = _context.CreateConnection();
        return conn.Query<ProductoConDetalle>("sp_Producto_Listar",
            new { solo_activos = soloActivos ? 1 : 0 },
            commandType: CommandType.StoredProcedure);
    }

    /*lo hago por consulta para no crear un sp, igual no creo que ocupen esto porque es de mi parte de inventario y producto*/
    public ProductoConDetalle ObtenerProductoPorId(int idProducto)
    {
        using var conn = _context.CreateConnection();
        string sql = @"
            SELECT
                p.id_producto AS IdProducto,
                p.nombre AS Nombre,
                p.descripcion AS Descripcion,
                p.precio_venta AS PrecioVenta,
                p.id_categoria AS IdCategoria,
                p.id_proveedor AS IdProveedor,
                p.activo AS Activo,
                c.nombre AS CategoriaNombre,
                pr.nombre AS ProveedorNombre
            FROM Producto p
            INNER JOIN Categoria c ON c.id_categoria = p.id_categoria
            INNER JOIN Proveedor pr ON pr.id_proveedor = p.id_proveedor
            WHERE p.id_producto = @idProducto";

        return conn.QueryFirstOrDefault<ProductoConDetalle>(sql, new { idProducto });
    }

    /*se llama el procedimiento para crear producto*/
    public void CrearProducto(string nombre, string descripcion, decimal precioVenta, int idCategoria, int idProveedor)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("sp_Producto_Crear",
            new
            {
                nombre,
                descripcion,
                precio_venta = precioVenta,
                id_categoria = idCategoria,
                id_proveedor = idProveedor
            },
            commandType: CommandType.StoredProcedure);
    }

    /*se llama procedimiento para actualizar producto*/
    public void ActualizarProducto(int idProducto, string nombre, string descripcion, decimal precioVenta, int idCategoria, int idProveedor, bool activo)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("sp_Producto_Actualizar",
            new
            {
                id_producto = idProducto,
                nombre,
                descripcion,
                precio_venta = precioVenta,
                id_categoria = idCategoria,
                id_proveedor = idProveedor,
                activo
            },
            commandType: CommandType.StoredProcedure);
    }

    /*se llama procedimiento para desactivar */
    public void DesactivarProducto(int idProducto)
    {
        using var conn = _context.CreateConnection();
        conn.Execute("sp_Producto_Eliminar",
            new { id_producto = idProducto },
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Categoria> ListarCategoriasActivas()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<Categoria>("sp_Categoria_Listar",
            new { solo_activas = 1 },
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<Proveedor> ListarProveedoresActivos()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<Proveedor>("sp_Proveedor_Listar",
            new { solo_activos = 1 },
            commandType: CommandType.StoredProcedure);
    }
}
