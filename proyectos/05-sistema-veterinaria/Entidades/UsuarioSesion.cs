namespace Entidades
{
    public class UsuarioSesion
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public int IdPersona { get; set; }
        public string PersonaNombre { get; set; }
        public int IdRol { get; set; }
        public string RolNombre { get; set; }
        public int IdSucursal { get; set; }
        public string SucursalNombre { get; set; }
    }
}
