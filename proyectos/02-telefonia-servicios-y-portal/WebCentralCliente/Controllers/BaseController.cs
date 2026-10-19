using System.Web.Mvc;
using System.Web.Routing;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Controllers
{
    // Controlador base de la Web del Cliente.
    // Exige sesion iniciada y rol cliente (Tipo = "2").
    public abstract class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!SesionHelper.EsCliente())
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
