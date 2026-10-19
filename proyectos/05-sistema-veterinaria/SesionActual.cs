using Entidades;

namespace Veterinaria1
{
    public static class SesionActual
    {
        public static UsuarioSesion? Usuario { get; private set; }

        public static bool EstaAutenticado => Usuario != null;
        public static int IdUsuario => Usuario?.IdUsuario ?? 0;
        public static int IdPersona => Usuario?.IdPersona ?? 0;
        public static int IdRol => Usuario?.IdRol ?? 0;
        public static int IdSucursal => Usuario?.IdSucursal ?? 0;
        public static string NombreRol => Usuario?.RolNombre ?? "";
        public static string NombrePersona => Usuario?.PersonaNombre ?? "";
        public static string NombreSucursal => Usuario?.SucursalNombre ?? "";

        public static void Iniciar(UsuarioSesion usuario)
        {
            Usuario = usuario;
        }

        public static void Cerrar()
        {
            Usuario = null;
        }
    }
}
