using System.Web.Mvc;
using System.Web.Routing;
using WebCentralAdmin.Utilidades;

namespace WebCentralAdmin.Controllers
{
    // Controlador base de la Web Administrativa.
    // Exige sesion iniciada y rol administrador (Tipo = "1").
    public abstract class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!SesionHelper.EsAdministrador())
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "Login" },
                        { "mensaje", "Sesión expirada. Inicie sesión nuevamente." }
                    });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
