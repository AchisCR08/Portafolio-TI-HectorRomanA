using System;
using System.ServiceModel.Activation;
using WS_Autenticacion.Infraestructura;
using WS_Autenticacion.Modelos;
using WS_Autenticacion.Utilidades;

namespace WS_Autenticacion
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AutenticacionService : IAutenticacionService
    {
        private readonly UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

        
        // WS_AUTENTICACION1
       
        public RespuestaServicio Autenticar(string usuario, string contrasena, string tipoUsuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario)
                    || string.IsNullOrWhiteSpace(contrasena)
                    || string.IsNullOrWhiteSpace(tipoUsuario))
                {
                    return CrearRespuesta(false, "Usuario y/o contrasena incorrectos.");
                }

                string tipoEsperado = MapearTipoUsuario(tipoUsuario);
                if (tipoEsperado == null)
                {
                    return CrearRespuesta(false, "Usuario y/o contrasena incorrectos.");
                }

                // AES/ECB es determinista: mismo texto plano + misma llave
                // siempre produce el mismo cifrado, por lo que se puede
                // comparar directamente el valor cifrado sin descifrar.
                UsuarioDocumento encontrado = usuarioRepositorio.BuscarPorUsuarioCifrado(usuario);

                if (encontrado == null)
                {
                    return CrearRespuesta(false, "Usuario y/o contrasena incorrectos.");
                }

                bool contrasenaCorrecta = encontrado.Contrasena == contrasena;
                bool estadoActivo = "activo".Equals(
                    (encontrado.Estado ?? "").Trim(), StringComparison.OrdinalIgnoreCase);
                bool tipoCoincide = encontrado.Tipo == tipoEsperado;

                if (!contrasenaCorrecta || !estadoActivo || !tipoCoincide)
                {
                    return CrearRespuesta(false, "Usuario y/o contrasena incorrectos.");
                }

                return CrearRespuesta(true, "Exitoso", encontrado.Identificacion);
            }
            catch
            {
                return CrearRespuesta(false, "Usuario y/o contrasena incorrectos.");
            }
        }

        
        // WS_AUTENTICACION2 - punto 1: crear usuario
      
        public RespuestaServicio CrearUsuario(
            string identificacion,
            string nombre,
            string primerApellido,
            string segundoApellido,
            string correoElectronico,
            string usuario,
            string contrasena,
            string estado,
            string tipo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(identificacion)
                    || string.IsNullOrWhiteSpace(usuario)
                    || string.IsNullOrWhiteSpace(contrasena))
                {
                    return CrearRespuesta(false, "Usuario ya existe o datos incorrectos o incompletos.");
                }

                if (usuarioRepositorio.Existe(identificacion))
                {
                    return CrearRespuesta(false, "Usuario ya existe o datos incorrectos o incompletos.");
                }

                // usuario/contrasena llegan cifrados; se descifran solo para saber si el formato esta bien
                
                string contrasenaPlano = CifradorAES.DescifrarSiEsPosible(contrasena);

                if (!Validaciones.EsNombreValido(nombre)
                    || !Validaciones.EsNombreValido(primerApellido)
                    || !Validaciones.EsNombreValido(segundoApellido)
                    || !Validaciones.EsCorreoValido(correoElectronico)
                    || !Validaciones.EsContrasenaValida(contrasenaPlano)
                    || !Validaciones.EsTipoValido(tipo)
                    || !"activo".Equals((estado ?? "").Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return CrearRespuesta(false, "Usuario ya existe o datos incorrectos o incompletos.");
                }

                UsuarioDocumento nuevoUsuario = new UsuarioDocumento
                {
                    Identificacion = identificacion.Trim(),
                    Nombre = nombre.Trim(),
                    PrimerApellido = primerApellido.Trim(),
                    SegundoApellido = segundoApellido.Trim(),
                    CorreoElectronico = correoElectronico.Trim(),
                    Usuario = usuario,
                    Contrasena = contrasena,
                    Estado = "activo",
                    Tipo = tipo
                };

                usuarioRepositorio.Insertar(nuevoUsuario);

                return CrearRespuesta(true, "Exitoso");
            }
            catch
            {
                return CrearRespuesta(false, "Usuario ya existe o datos incorrectos o incompletos.");
            }
        }

        
        // WS_AUTENTICACION2 - punto 2: modificar usuario
       
        public RespuestaServicio ModificarUsuario(
            string identificacion,
            string nombre,
            string primerApellido,
            string segundoApellido,
            string correoElectronico,
            string usuario,
            string contrasena)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(identificacion))
                {
                    return CrearRespuesta(false, "Usuario no existe o datos incorrectos o incompletos.");
                }

                UsuarioDocumento existente = usuarioRepositorio.BuscarPorIdentificacion(identificacion);

                if (existente == null)
                {
                    return CrearRespuesta(false, "Usuario no existe o datos incorrectos o incompletos.");
                }

                // Solo se validan/actualizan los campos que vienen presentes.
                // La identificacion (llave primaria) nunca se modifica.
                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    if (!Validaciones.EsNombreValido(nombre))
                    {
                        return CrearRespuesta(false, "Usuario no existe o datos incorrectos o incompletos.");
                    }
                    existente.Nombre = nombre.Trim();
                }

                if (!string.IsNullOrWhiteSpace(primerApellido))
                {
                    if (!Validaciones.EsNombreValido(primerApellido))
                    {
                        return CrearRespuesta(false, "Usuario no existe o datos incorrectos o incompletos.");
                    }
                    existente.PrimerApellido = primerApellido.Trim();
                }

                if (!string.IsNullOrWhiteSpace(segundoApellido))
                {
                    if (!Validaciones.EsNombreValido(segundoApellido))
                    {
                        return CrearRespuesta(false, "Usuario no existe o datos incorrectos o incompletos.");
                    }
                    existente.SegundoApellido = segundoApellido.Trim();
                }

                if (!string.IsNullOrWhiteSpace(correoElectronico))
                {
                    if (!Validaciones.EsCorreoValido(correoElectronico))
                    {
                        return CrearRespuesta(false, "Usuario no existe o datos incorrectos o incompletos.");
                    }
                    existente.CorreoElectronico = correoElectronico.Trim();
                }

                if (!string.IsNullOrWhiteSpace(usuario))
                {
                    existente.Usuario = usuario;
                }

                if (!string.IsNullOrWhiteSpace(contrasena))
                {
                    string contrasenaPlano = CifradorAES.DescifrarSiEsPosible(contrasena);

                    if (!Validaciones.EsContrasenaValida(contrasenaPlano))
                    {
                        return CrearRespuesta(false, "Usuario no existe o datos incorrectos o incompletos.");
                    }
                    existente.Contrasena = contrasena;
                }

                usuarioRepositorio.Reemplazar(existente);

                return CrearRespuesta(true, "Exitoso");
            }
            catch
            {
                return CrearRespuesta(false, "Usuario no existe o datos incorrectos o incompletos.");
            }
        }

        
        // WS_AUTENTICACION2 - punto 3: activar/inactivar usuario
        
        public RespuestaServicio CambiarEstadoUsuario(string identificacion, string estado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(identificacion) || !Validaciones.EsEstadoValido(estado))
                {
                    return CrearRespuesta(false, "Usuario no existe o datos incorrectos.");
                }

                if (!usuarioRepositorio.Existe(identificacion))
                {
                    return CrearRespuesta(false, "Usuario no existe o datos incorrectos.");
                }

                usuarioRepositorio.ActualizarEstado(identificacion, estado.Trim().ToLowerInvariant());

                return CrearRespuesta(true, "Exitoso");
            }
            catch
            {
                return CrearRespuesta(false, "Usuario no existe o datos incorrectos.");
            }
        }

        
        // Consulta de datos basicos del usuario por identificacion
        
        public RespuestaObtenerUsuario ObtenerUsuario(string identificacion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(identificacion))
                {
                    return new RespuestaObtenerUsuario
                    {
                        Resultado = false,
                        Mensaje = "Identificacion requerida."
                    };
                }

                UsuarioDocumento encontrado =
                    usuarioRepositorio.BuscarPorIdentificacion(identificacion.Trim());

                if (encontrado == null)
                {
                    return new RespuestaObtenerUsuario
                    {
                        Resultado = false,
                        Mensaje = "Usuario no encontrado."
                    };
                }

                return new RespuestaObtenerUsuario
                {
                    Resultado = true,
                    Mensaje = "Exitoso",
                    Usuario = new UsuarioInfo
                    {
                        Identificacion = encontrado.Identificacion,
                        Nombre = encontrado.Nombre,
                        PrimerApellido = encontrado.PrimerApellido,
                        SegundoApellido = encontrado.SegundoApellido,
                        CorreoElectronico = encontrado.CorreoElectronico
                    }
                };
            }
            catch
            {
                return new RespuestaObtenerUsuario
                {
                    Resultado = false,
                    Mensaje = "Error al consultar el usuario."
                };
            }
        }

        
        // Helpers
       
        private static string MapearTipoUsuario(string tipoUsuario)
        {
            string normalizado = tipoUsuario.Trim().ToLowerInvariant();

            if (normalizado == "administrador" || normalizado == "1")
            {
                return "1";
            }

            if (normalizado == "cliente" || normalizado == "2")
            {
                return "2";
            }

            return null;
        }

        private static RespuestaServicio CrearRespuesta(bool resultado, string mensaje, string identificacion = null)
        {
            return new RespuestaServicio
            {
                Resultado = resultado,
                Mensaje = mensaje,
                Identificacion = identificacion
            };
        }
    }
}
