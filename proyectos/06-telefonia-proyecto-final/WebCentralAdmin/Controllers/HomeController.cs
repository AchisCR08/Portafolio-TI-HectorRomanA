using System.Web.Mvc;
using WebCentralAdmin.Utilidades;

namespace WebCentralAdmin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.NombreUsuario = SesionHelper.ObtenerNombreUsuario();
            return View();
        }
    }
}
