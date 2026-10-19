namespace Entidades
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
        public bool Activo { get; set; }
    }
}
