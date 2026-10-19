using Dapper;
using Entidades;
using Repositorio;
using System.Collections.Generic;
using System.Data;

public class FacturaRepository
{
    private readonly VeterinariaDbContext _context;

    public FacturaRepository()
    {
        _context = new VeterinariaDbContext();
    }

    // esto es para los combobox de factura, para que cargue informacion 

    public IEnumerable<PersonaCombo> ListarPersonas()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<PersonaCombo>(
            "SELECT id_persona AS IdPersona, nombre AS Nombre FROM Persona WHERE activo = 1 ORDER BY nombre");
    }

    /*carga los combobox para los metodos de pago, los metodos de pago se crean desde base para no hacer un crud xd */
    public IEnumerable<TipoPagoCombo> ListarTiposPago()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<TipoPagoCombo>("sp_TipoPago_Listar",
            new { solo_activos = 1 },
            commandType: CommandType.StoredProcedure);
    }

    /*Solo muestra los productos disponibles, si activo =1 esta disponible, si activo= 2 adivinen xd*/
    public IEnumerable<ProductoConDetalle> ListarProductosActivos()
    {
        using var conn = _context.CreateConnection();
        return conn.Query<ProductoConDetalle>("sp_Producto_Listar",
            new { solo_activos = 1 },
            commandType: CommandType.StoredProcedure);
    }


    public IEnumerable<CitaVetCombo> ListarCitasVetPendientes(int idSucursal)
    {
        using var conn = _context.CreateConnection();
        return conn.Query<CitaVetCombo>("sp_CitaVeterinaria_PendientesCobro",
            new { id_sucursal = idSucursal },
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<CitaGroomingCombo> ListarCitasGroomingPendientes(int idSucursal)
    {
        using var conn = _context.CreateConnection();
        return conn.Query<CitaGroomingCombo>("sp_CitaGrooming_PendientesCobro",
            new { id_sucursal = idSucursal },
            commandType: CommandType.StoredProcedure);
    }

    /*Emitir factura */

    public int EmitirFactura(int idPersona, int idSucursal,
        IEnumerable<DetalleFacturaLocal> lineas, int idTipoPago, decimal montoTotal)
    {
        using var conn = _context.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        
        var idFactura = conn.ExecuteScalar<int>("sp_Factura_Crear",
            new { id_persona = idPersona, id_sucursal = idSucursal },
            transaction: tx,
            commandType: CommandType.StoredProcedure);

        // 2. se va agrando producto uno a uno, o cita o gromming 
        foreach (var linea in lineas)
        {
            switch (linea.Tipo)
            {
                case DetalleFacturaLocal.TipoLinea.Producto:
                    conn.Execute("sp_Factura_AgregarProducto",
                        new
                        {
                            id_factura      = idFactura,
                            id_producto     = linea.IdItem,
                            cantidad        = linea.Cantidad,
                            precio_unitario = linea.PrecioUnitario
                        },
                        transaction: tx,
                        commandType: CommandType.StoredProcedure);
                    break;

                case DetalleFacturaLocal.TipoLinea.CitaVeterinaria:
                    conn.Execute("sp_Factura_AgregarCitaVeterinaria",
                        new
                        {
                            id_factura      = idFactura,
                            id_cita_vet     = linea.IdItem,
                            precio_unitario = linea.PrecioUnitario
                        },
                        transaction: tx,
                        commandType: CommandType.StoredProcedure);
                    break;

                case DetalleFacturaLocal.TipoLinea.CitaGrooming:
                    conn.Execute("sp_Factura_AgregarCitaGrooming",
                        new
                        {
                            id_factura        = idFactura,
                            id_cita_grooming  = linea.IdItem,
                            precio_unitario   = linea.PrecioUnitario
                        },
                        transaction: tx,
                        commandType: CommandType.StoredProcedure);
                    break;
            }
        }

        /*registra el pago completo para no hacerlo complicado*/
        conn.Execute("sp_Pago_Registrar",
            new
            {
                id_factura   = idFactura,
                id_tipo_pago = idTipoPago,
                monto        = montoTotal
            },
            transaction: tx,
            commandType: CommandType.StoredProcedure);

        tx.Commit();
        return idFactura;
    }
}
