using System.Configuration;

namespace WebCentralAdmin.Utilidades
{
    // URLs de los Web Services consumidos por la Web Administrativa.
    // Se configuran en Web.config (appSettings).
    public static class ConfiguracionWS
    {
        public static string UrlProveedor =>
            ConfigurationManager.AppSettings["WS_Proveedor.Url"]
            ?? "http://localhost:55951/ProveedorService.svc";

        public static string UrlAutenticacion =>
            ConfigurationManager.AppSettings["WS_Autenticacion.Url"]
            ?? "http://localhost:55960/AutenticacionService.svc";

        public static string UrlIdentificador =>
            ConfigurationManager.AppSettings["WS_Identificador.Url"]
            ?? "http://localhost:51907/IdentificadorService.svc";

        public static string UrlConsultas =>
            ConfigurationManager.AppSettings["WS_Consultas.Url"]
            ?? "http://localhost:55980/ConsultasProveedorService.svc";
    }
}
