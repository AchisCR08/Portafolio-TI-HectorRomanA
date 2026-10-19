namespace Entidades
{
    public class ProductoConDetalle
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
        public bool Activo { get; set; }
        public string CategoriaNombre { get; set; }
        public string ProveedorNombre { get; set; }
    }
}
