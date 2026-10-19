using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using WebCentralAdmin.Infraestructura;
using WebCentralAdmin.Models;
using WebCentralAdmin.Utilidades;

namespace WebCentralAdmin.Controllers
{
    public class UsuariosController : BaseController
    {
        private readonly AutenticacionServiceClient client = new AutenticacionServiceClient();
        public ActionResult Index() { try { return View(client.ListarUsuarios("1") ?? new UsuarioDTO[0]); } catch { TempData["Error"] = "Error al realizar el proceso"; return View(new UsuarioDTO[0]); } }
        public ActionResult Nuevo() { return View(new UsuarioAdminViewModel()); }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Nuevo(UsuarioAdminViewModel model)
        {
            string error; if (!Valido(model, out error)) return ErrorVista(model, error);
            RespuestaServicioAutenticacion r = Ejecutar(() => client.CrearUsuario(model.Identificacion.Trim(), model.Nombre.Trim(), model.PrimerApellido.Trim(), model.SegundoApellido.Trim(), model.CorreoElectronico.Trim(), CifradorAES.Cifrar(model.Usuario.Trim()), CifradorAES.Cifrar(model.Contrasena), "activo", "1"));
            if (r != null && r.Resultado) { TempData["Mensaje"] = "Registro exitoso"; return RedirectToAction("Index"); }
            return ErrorVista(model, r != null ? r.Mensaje : "Error al realizar el proceso");
        }

        public ActionResult Editar(string id)
        {
            UsuarioDTO u = Obtener(id); if (u == null) { TempData["Error"] = "Usuario no encontrado."; return RedirectToAction("Index"); }
            return View("Nuevo", Mapear(u));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Editar(UsuarioAdminViewModel model)
        {
            string error; if (!Valido(model, out error)) return ErrorVista(model, error);
            RespuestaServicioAutenticacion r = Ejecutar(() => client.ModificarUsuario(model.Identificacion.Trim(), model.Nombre.Trim(), model.PrimerApellido.Trim(), model.SegundoApellido.Trim(), model.CorreoElectronico.Trim(), CifradorAES.Cifrar(model.Usuario.Trim()), CifradorAES.Cifrar(model.Contrasena)));
            if (r != null && r.Resultado) { TempData["Mensaje"] = "Registro exitoso"; return RedirectToAction("Index"); }
            return ErrorVista(model, r != null ? r.Mensaje : "Error al realizar el proceso");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CambiarEstado(string identificacion, string estado)
        {
            RespuestaServicioAutenticacion r = Ejecutar(() => client.CambiarEstadoUsuario(identificacion, string.Equals(estado, "activo", StringComparison.OrdinalIgnoreCase) ? "inactivo" : "activo"));
            TempData[r != null && r.Resultado ? "Mensaje" : "Error"] = r != null && r.Resultado ? "Registro actualizado" : (r != null ? r.Mensaje : "Error al realizar el proceso"); return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Eliminar(string identificacion)
        {
            RespuestaServicioAutenticacion r = Ejecutar(() => client.EliminarUsuario(identificacion));
            TempData[r != null && r.Resultado ? "Mensaje" : "Error"] = r != null && r.Resultado ? "Borrado exitoso" : (r != null ? r.Mensaje : "Error al realizar el proceso"); return RedirectToAction("Index");
        }

        private UsuarioDTO Obtener(string id) { try { return (client.ListarUsuarios("1") ?? new UsuarioDTO[0]).FirstOrDefault(x => x.Identificacion == id); } catch { return null; } }
        private static UsuarioAdminViewModel Mapear(UsuarioDTO x) { return new UsuarioAdminViewModel { Identificacion=x.Identificacion, Nombre=x.Nombre, PrimerApellido=x.PrimerApellido, SegundoApellido=x.SegundoApellido, CorreoElectronico=x.CorreoElectronico, Usuario=x.Usuario, Contrasena=x.Contrasena }; }
        private ActionResult ErrorVista(UsuarioAdminViewModel m, string e) { m.Contrasena = string.Empty; ViewBag.Error = e; return View("Nuevo", m); }
        private static RespuestaServicioAutenticacion Ejecutar(Func<RespuestaServicioAutenticacion> a) { try { return a(); } catch { return null; } }
        private static bool Valido(UsuarioAdminViewModel m, out string error)
        {
            if (m == null || new[] {m.Identificacion,m.Nombre,m.PrimerApellido,m.SegundoApellido,m.CorreoElectronico,m.Usuario,m.Contrasena}.Any(string.IsNullOrWhiteSpace)) { error="Todos los campos son obligatorios."; return false; }
            if (!Regex.IsMatch(m.CorreoElectronico.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) { error="El correo electrónico no es válido."; return false; }
            string p=m.Contrasena; if (p.Length != 14 || !Regex.IsMatch(p,"[A-Z]") || !Regex.IsMatch(p,"[a-z]") || !Regex.IsMatch(p,"[0-9]") || !Regex.IsMatch(p,"[^A-Za-z0-9]")) { error="La contraseña debe tener 14 caracteres, mayúscula, minúscula, número y carácter especial."; return false; }
            error=null; return true;
        }
    }
}
