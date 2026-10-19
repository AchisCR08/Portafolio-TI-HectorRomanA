using System.Web.Mvc;
using WebCentralCliente.Infraestructura;
using WebCentralCliente.Models;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Controllers
{
    // CLIENTE3: registro (autogestion) de un nuevo cliente.
    // No hereda de BaseController: se usa sin sesion, desde el login.
    public class RegistroController : Controller
    {
        private readonly AutenticacionServiceClient autenticacionClient = new AutenticacionServiceClient();

        [HttpGet]
        public ActionResult Index()
        {
            return View(new RegistroViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RegistroViewModel model)
        {
            if (!EsValido(model, out string error))
            {
                ViewBag.Error = error;
                return View(LimpiarContrasena(model));
            }

            string usuarioCifrado = CifradorAES.Cifrar(model.Usuario.Trim());
            string contrasenaCifrada = CifradorAES.Cifrar(model.Contrasena);

            RespuestaServicioAutenticacion respuesta = autenticacionClient.CrearUsuario(
                model.Identificacion.Trim(),
                model.Nombre.Trim(),
                model.PrimerApellido.Trim(),
                model.SegundoApellido.Trim(),
                model.CorreoElectronico.Trim(),
                usuarioCifrado,
                contrasenaCifrada,
                "activo",
                "2");

            if (respuesta == null || !respuesta.Resultado)
            {
                ViewBag.Error = respuesta != null ? respuesta.Mensaje : "Error al realizar el proceso";
                return View(LimpiarContrasena(model));
            }

            ViewBag.Mensaje = "Registro exitoso";
            ViewBag.RegistroCompleto = true;
            return View(new RegistroViewModel());
        }

        private static RegistroViewModel LimpiarContrasena(RegistroViewModel model)
        {
            if (model != null)
            {
                model.Contrasena = string.Empty;
            }

            return model;
        }

        private static bool EsValido(RegistroViewModel model, out string error)
        {
            if (model == null
                || string.IsNullOrWhiteSpace(model.Identificacion)
                || string.IsNullOrWhiteSpace(model.Usuario))
            {
                error = "Usuario ya existe o datos incorrectos o incompletos.";
                return false;
            }

            if (!Validaciones.EsNombreValido(model.Nombre)
                || !Validaciones.EsNombreValido(model.PrimerApellido)
                || !Validaciones.EsNombreValido(model.SegundoApellido))
            {
                error = "Nombre y apellidos son obligatorios y no deben contener números.";
                return false;
            }

            if (!Validaciones.EsCorreoValido(model.CorreoElectronico))
            {
                error = "El correo electrónico no es válido.";
                return false;
            }

            // La contrasena puede digitarse en claro o ya cifrada; se valida
            // sobre la forma descifrada (si es posible) para aceptar ambas.
            string contrasenaPlano = CifradorAES.DescifrarSiEsPosible(model.Contrasena);

            if (!Validaciones.EsContrasenaValida(contrasenaPlano))
            {
                error = "La contraseña debe tener 14 caracteres, con al menos una mayúscula, una minúscula, un número y un carácter especial.";
                return false;
            }

            error = null;
            return true;
        }
    }
}
