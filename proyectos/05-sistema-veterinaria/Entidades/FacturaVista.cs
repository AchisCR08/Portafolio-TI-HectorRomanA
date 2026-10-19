namespace Entidades
{
    /// Fila del grid de facturas emitidas
    public class FacturaVista
    {
        public int      IdFactura      { get; set; }
        public DateTime Fecha          { get; set; }  // DateTime: Dapper no soporta DateOnly con SqlClient
        public decimal  Total          { get; set; }
        public int      IdPersona      { get; set; }
        public string   PersonaNombre  { get; set; } = "";
        public int      IdSucursal     { get; set; }
        public string   SucursalNombre { get; set; } = "";
    }

    /// Línea del grid de detalle de una factura
    public class DetalleFacturaVista
    {
        public int     IdDetalleFactura { get; set; }
        public int     IdFactura        { get; set; }
        public string  TipoLinea        { get; set; } = "";
        public string  Descripcion      { get; set; } = "";
        public int     Cantidad         { get; set; }
        public decimal PrecioUnitario   { get; set; }
        public decimal Subtotal         { get; set; }
        public int?    IdProducto       { get; set; }
        public int?    IdCitaVet        { get; set; }
        public int?    IdCitaGrooming   { get; set; }
    }

    /// Pago asociado a una factura
    public class PagoVista
    {
        public int      IdPago          { get; set; }
        public int      IdFactura       { get; set; }
        public decimal  Monto           { get; set; }
        public DateTime FechaPago       { get; set; }  // DateTime: Dapper no soporta DateOnly con SqlClient
        public int      IdTipoPago      { get; set; }
        public string   TipoPagoNombre  { get; set; } = "";
    }
}
