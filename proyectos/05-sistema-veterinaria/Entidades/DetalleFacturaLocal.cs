namespace Entidades
{
    ///
    /// 
    /// esto se queda en memoria hasta que se emita la factura
    /// 
    public class DetalleFacturaLocal
    {
        // sirve para ver si la linea es producto citaveterinaria o groming
        public enum TipoLinea { Producto, CitaVeterinaria, CitaGrooming }

        public TipoLinea Tipo { get; set; }

        // ID real del item 
        public int IdItem { get; set; }

        
        public string Descripcion { get; set; } = "";

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
