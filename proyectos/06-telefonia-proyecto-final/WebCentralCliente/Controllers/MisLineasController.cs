using System.Linq;
using System.Web.Mvc;
using WebCentralCliente.Infraestructura;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Controllers
{
    // CLIENTE4: lineas prepago y postpago asociadas al cliente conectado.
    public class MisLineasController : BaseController
    {
        private readonly ConsultasServiceClient consultasClient = new ConsultasServiceClient();

        public ActionResult Index()
        {
            string identificacion = SesionHelper.ObtenerIdentificacion();

            LineaClienteDTO[] lineas = consultasClient.ListarLineasPorCliente(identificacion) ?? new LineaClienteDTO[0];

            ViewBag.Prepago = lineas
                .Where(l => string.Equals(l.TipoServicio, "prepago", System.StringComparison.OrdinalIgnoreCase))
                .ToArray();

            ViewBag.Postpago = lineas
                .Where(l => string.Equals(l.TipoServicio, "postpago", System.StringComparison.OrdinalIgnoreCase))
                .ToArray();

            return View();
        }
    }
}
