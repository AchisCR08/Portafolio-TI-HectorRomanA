using System.Web;

namespace WebCentralCliente.Utilidades
{
    // Manejo de la sesion de la Web del Cliente.
    // "Usuario" y "Tipo" los establece el login al autenticar.
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

        public static bool EsCliente()
        {
            HttpContext contexto = HttpContext.Current;
            return EstaLogueado()
                && string.Equals(contexto.Session["Tipo"] as string, "2", System.StringComparison.Ordinal);
        }

        public static void IniciarSesion(string usuario, string tipo, string nombre, string identificacion)
        {
            HttpContext.Current.Session["Usuario"] = usuario;
            HttpContext.Current.Session["Tipo"] = tipo;
            HttpContext.Current.Session["Nombre"] = nombre;
            HttpContext.Current.Session["Identificacion"] = identificacion;
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

        public static string ObtenerIdentificacion()
        {
            HttpContext contexto = HttpContext.Current;
            return contexto != null && contexto.Session != null
                ? contexto.Session["Identificacion"] as string
                : null;
        }
    }
}
