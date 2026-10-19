namespace Entidades
{
    /// 
    /// Línea de compra que se mantiene en memoria en FrmCompras
    /// hasta que se confirma el registro.
    /// 
    public class LineaCompraLocal
    {
        public int     IdProducto     { get; set; }
        public string  ProductoNombre { get; set; } = "";
        public int     Cantidad       { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal       => Cantidad * PrecioUnitario;
    }
}
