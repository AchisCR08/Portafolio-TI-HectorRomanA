using System.Web.Mvc;
using WebCentralAdmin.Infraestructura;
using WebCentralAdmin.Utilidades;

namespace WebCentralAdmin.Controllers
{
    // Login (ADM1) y "Salir del sitio" (ADM2).
    // El metodo Login (vista + validacion contra WS_AUTENTICACION1)
    // lo implementa Kendall como historia ADM1. Aqui queda la base
    // (metodo y limpieza de sesion).
    public class AccountController : Controller
    {
        private readonly AutenticacionServiceClient autenticacionClient = new AutenticacionServiceClient();
        [HttpGet]
        public ActionResult Login(string mensaje = null)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                ViewBag.Mensaje = "Usuario y/o contraseña incorrectos";
                return View();
            }

            try
            {
                RespuestaServicioAutenticacion respuesta = autenticacionClient.Autenticar(
                    CifradorAES.Cifrar(usuario.Trim()), CifradorAES.Cifrar(contrasena), "1");
                if (respuesta != null && respuesta.Resultado && !string.IsNullOrWhiteSpace(respuesta.Identificacion))
                {
                    SesionHelper.IniciarSesion(respuesta.Identificacion, "1", respuesta.Identificacion);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch { }

            ViewBag.Mensaje = "Usuario y/o contraseña incorrectos";
            return View();
        }

        [HttpGet]
        public ActionResult Salir()
        {
            SesionHelper.CerrarSesion();
            return RedirectToAction("Login", "Account");
        }
    }
}
