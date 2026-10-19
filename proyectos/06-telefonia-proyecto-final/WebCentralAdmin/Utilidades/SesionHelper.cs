using System.Web;

namespace WebCentralAdmin.Utilidades
{
    // Manejo de la sesion de la Web Administrativa.
    // "Usuario" y "Tipo" los establece ADM1 (login) al autenticar.
    // "Tipo" = "1" administrador, "2" cliente.
    public static class SesionHelper
    {
        public static bool EstaLogueado()
        {
            HttpContext contexto = HttpContext.Current;
            return contexto != null
                && contexto.Session != null
                && contexto.Session["Usuario"] != null;
        }

        public static bool EsAdministrador()
        {
            HttpContext contexto = HttpContext.Current;
            return EstaLogueado()
                && string.Equals(contexto.Session["Tipo"] as string, "1", System.StringComparison.Ordinal);
        }

        public static void IniciarSesion(string usuario, string tipo, string nombre)
        {
            HttpContext.Current.Session["Usuario"] = usuario;
            HttpContext.Current.Session["Tipo"] = tipo;
            HttpContext.Current.Session["Nombre"] = nombre;
        }

        public static void CerrarSesion()
        {
            HttpContext contexto = HttpContext.Current;
            if (contexto != null && contexto.Session != null)
            {
                contexto.Session.Abandon();
            }
        }

        public static string ObtenerNombreUsuario()
        {
            HttpContext contexto = HttpContext.Current;
            return contexto != null && contexto.Session != null
                ? contexto.Session["Nombre"] as string
                : null;
        }
    }
}
