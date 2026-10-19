using System.Web.Mvc;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Controllers
{
    // Punto de entrada del sitio (ruta por defecto "/").
    // Si hay sesion de cliente activa, muestra CLIENTE4 (Mis lineas).
    // Si no, manda al login (CLIENTE1).
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (SesionHelper.EsCliente())
            {
                return RedirectToAction("Index", "MisLineas");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
