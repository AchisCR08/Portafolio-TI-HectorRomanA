using Dapper;
using Entidades;
using Repositorio;
using System.Collections.Generic;
using System.Data;

public class DetalleVentasRepository
{
    private readonly VeterinariaDbContext _context;

    public DetalleVentasRepository()
    {
        _context = new VeterinariaDbContext();
    }

    /// Lista facturas de una sucursal por rango de fechas.
    public IEnumerable<FacturaVista> ListarFacturas(int idSucursal, DateTime fechaInicio, DateTime fechaFin)
    {
        using var conn = _context.CreateConnection();
        return conn.Query<FacturaVista>("sp_Factura_Listar",
            new
            {
                id_sucursal  = idSucursal,
                fecha_inicio = fechaInicio.Date,   // DateTime — Dapper lo mapea a DATE en SQL
                fecha_fin    = fechaFin.Date
            },
            commandType: CommandType.StoredProcedure);
    }

    /// Devuelve las líneas de detalle de una factura específica
    public IEnumerable<DetalleFacturaVista> ObtenerDetalle(int idFactura)
    {
        using var conn = _context.CreateConnection();
        return conn.Query<DetalleFacturaVista>("sp_Factura_ObtenerDetalle",
            new { id_factura = idFactura },
            commandType: CommandType.StoredProcedure);
    }

    /// Devuelve los pagos de una factura (creo que mejor no hacemos esto por el tiempo)
    public IEnumerable<PagoVista> ListarPagos(int idFactura)
    {
        using var conn = _context.CreateConnection();
        return conn.Query<PagoVista>("sp_Pago_ListarPorFactura",
            new { id_factura = idFactura },
            commandType: CommandType.StoredProcedure);
    }
}
