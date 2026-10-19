using System.Text.RegularExpressions;

namespace WS_Autenticacion.Utilidades
{
    public static class Validaciones
    {
        // Nombre / apellidos: no vacio, sin espacios en blanco puros, sin numeros.
        
        private static readonly Regex PatronNombre =
            new Regex(@"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ]+([ ][A-Za-zÁÉÍÓÚÜÑáéíóúüñ]+)*$");

        // correo electronico con formato 
        private static readonly Regex PatronCorreo =
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        // contrasena: exactamente 14 caracteres, con al menos 1 mayuscula,
        // 1 minuscula, 1 numero y 1 caracter especial.
        private static readonly Regex PatronMayuscula = new Regex(@"[A-Z]");
        private static readonly Regex PatronMinuscula = new Regex(@"[a-z]");
        private static readonly Regex PatronNumero = new Regex(@"[0-9]");
        private static readonly Regex PatronEspecial = new Regex(@"[^A-Za-z0-9]");

        public static bool EsNombreValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            return PatronNombre.IsMatch(valor.Trim());
        }

        public static bool EsCorreoValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            return PatronCorreo.IsMatch(valor.Trim());
        }

        public static bool EsContrasenaValida(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return false;
            }

            if (valor.Length != 14)
            {
                return false;
            }

            return PatronMayuscula.IsMatch(valor)
                && PatronMinuscula.IsMatch(valor)
                && PatronNumero.IsMatch(valor)
                && PatronEspecial.IsMatch(valor);
        }

        public static bool EsTipoValido(string valor)
        {
            return valor == "1" || valor == "2";
        }

        public static bool EsEstadoValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            string normalizado = valor.Trim().ToLowerInvariant();
            return normalizado == "activo" || normalizado == "inactivo";
        }
    }
}
