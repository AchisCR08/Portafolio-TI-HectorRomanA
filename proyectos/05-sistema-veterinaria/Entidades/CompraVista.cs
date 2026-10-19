namespace Entidades
{
    /// Fila del grid de compras realizadas.
    public class CompraVista
    {
        public int      IdCompra       { get; set; }
        public DateTime Fecha          { get; set; }
        public int      IdProveedor    { get; set; }
        public string   ProveedorNombre { get; set; } = "";
        public int      IdSucursal     { get; set; }
        public string   SucursalNombre { get; set; } = "";
        public decimal  Total          { get; set; }
    }

    /// Linea del grid de detalle de una compra.
    public class DetalleCompraVista
    {
        public int     IdDetalleCompra { get; set; }
        public int     IdCompra        { get; set; }
        public int     IdProducto      { get; set; }
        public string  ProductoNombre  { get; set; } = "";
        public int     Cantidad        { get; set; }
        public decimal PrecioUnitario  { get; set; }
        public decimal Subtotal        { get; set; }
    }
}
