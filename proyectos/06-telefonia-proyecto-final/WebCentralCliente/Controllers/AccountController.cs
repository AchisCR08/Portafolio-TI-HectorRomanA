using System.Web.Mvc;
using WebCentralCliente.Infraestructura;
using WebCentralCliente.Models;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Controllers
{
    // CLIENTE1: login del portal de clientes.
    // No hereda de BaseController porque todavia no hay sesion.
    public class AccountController : Controller
    {
        private readonly AutenticacionServiceClient autenticacionClient = new AutenticacionServiceClient();

        [HttpGet]
        public ActionResult Login(string mensaje)
        {
            if (SesionHelper.EsCliente())
            {
                return RedirectToAction("Index", "MisLineas");
            }

            ViewBag.Mensaje = mensaje;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (model == null
                || string.IsNullOrWhiteSpace(model.Usuario)
                || string.IsNullOrWhiteSpace(model.Contrasena))
            {
                ViewBag.Error = "Usuario y/o contraseña incorrectos";
                return View(model);
            }

            string usuarioCifrado = CifradorAES.Cifrar(model.Usuario.Trim());
            string contrasenaCifrada = CifradorAES.Cifrar(model.Contrasena);

            RespuestaServicioAutenticacion respuesta =
                autenticacionClient.Autenticar(usuarioCifrado, contrasenaCifrada, "2");

            if (respuesta == null || !respuesta.Resultado)
            {
                ViewBag.Error = "Usuario y/o contraseña incorrectos";
                return View(new LoginViewModel { Usuario = model.Usuario });
            }

            string nombreCompleto = model.Usuario;
            RespuestaObtenerUsuario datosUsuario = autenticacionClient.ObtenerUsuario(respuesta.Identificacion);

            if (datosUsuario != null && datosUsuario.Resultado && datosUsuario.Usuario != null)
            {
                nombreCompleto = datosUsuario.Usuario.Nombre;
            }

            SesionHelper.IniciarSesion(model.Usuario.Trim(), "2", nombreCompleto, respuesta.Identificacion);

            return RedirectToAction("Index", "MisLineas");
        }

        // Salir del sitio: vuelve al login y reinicia los datos como si fuera la primera vez.
        public ActionResult Salir()
        {
            SesionHelper.CerrarSesion();
            return RedirectToAction("Login");
        }
    }
}
