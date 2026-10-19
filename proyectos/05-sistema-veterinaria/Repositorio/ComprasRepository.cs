using Dapper;
using Entidades;
using Repositorio;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class ComprasRepository
{
    private readonly VeterinariaDbContext _context;

    public ComprasRepository()
    {
        _context = new VeterinariaDbContext();
    }

    
    /// Registra el encabezado de la compra y todas sus lineas en una sola transacción.
    /// El trigger trg_DetalleCompra_ValidarYActualizarStock sube el stock automáticamente.
    
    public int RegistrarCompra(DateTime fecha, int idProveedor, int idSucursal,
        IEnumerable<LineaCompraLocal> lineas)
    {
        using var conn = _context.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            // 1. Insertar encabezado 
            var p = new DynamicParameters();
            p.Add("fecha",        fecha.Date,   DbType.Date);
            p.Add("id_proveedor", idProveedor,  DbType.Int32);
            p.Add("id_sucursal",  idSucursal,   DbType.Int32);
            p.Add("id_compra",    dbType: DbType.Int32,
                                  direction: ParameterDirection.Output);

            conn.Execute("sp_Compra_Registrar", p,
                transaction: tx,
                commandType: CommandType.StoredProcedure);

            int idCompra = p.Get<int>("id_compra");

            // 2. Insertar cada línea — el trigger actualiza el stock
            foreach (var l in lineas)
            {
                conn.Execute("sp_Compra_AgregarDetalle",
                    new
                    {
                        id_compra        = idCompra,
                        id_producto      = l.IdProducto,
                        cantidad         = l.Cantidad,
                        precio_unitario  = l.PrecioUnitario
                    },
                    transaction: tx,
                    commandType: CommandType.StoredProcedure);
            }

            tx.Commit();
            return idCompra;
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    /// esto lista las compras de una sucursal por fechas
    public IEnumerable<CompraVista> ListarCompras(int idSucursal, DateTime fechaInicio, DateTime fechaFin)
    {
        using var conn = _context.CreateConnection();
        return conn.Query<CompraVista>("sp_Compra_Listar",
            new
            {
                id_sucursal  = idSucursal,
                fecha_inicio = fechaInicio.Date,
                fecha_fin    = fechaFin.Date
            },
            commandType: CommandType.StoredProcedure);
    }

    /// Devuelve las lineas de detalle de una compra específica
    public IEnumerable<DetalleCompraVista> ObtenerDetalle(int idCompra)
    {
        using var conn = _context.CreateConnection();
        return conn.Query<DetalleCompraVista>("sp_Compra_ObtenerDetalle",
            new { id_compra = idCompra },
            commandType: CommandType.StoredProcedure);
    }
}
