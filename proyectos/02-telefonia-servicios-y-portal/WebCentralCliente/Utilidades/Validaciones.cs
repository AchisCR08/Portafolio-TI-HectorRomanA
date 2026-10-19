using System;
using System.Text.RegularExpressions;

namespace WebCentralCliente.Utilidades
{
    // Reglas de validacion del enunciado, espejo de WS_Autenticacion.Utilidades.Validaciones
    // para dar feedback inmediato en la web antes de llamar al Web Service.
    public static class Validaciones
    {
        private static readonly Regex PatronNombre =
            new Regex(@"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ]+([ ][A-Za-zÁÉÍÓÚÜÑáéíóúüñ]+)*$");

        private static readonly Regex PatronCorreo =
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private static readonly Regex PatronMayuscula = new Regex(@"[A-Z]");
        private static readonly Regex PatronMinuscula = new Regex(@"[a-z]");
        private static readonly Regex PatronNumero = new Regex(@"[0-9]");
        private static readonly Regex PatronEspecial = new Regex(@"[^A-Za-z0-9]");

        public static bool EsNombreValido(string valor)
        {
            return !string.IsNullOrWhiteSpace(valor) && PatronNombre.IsMatch(valor.Trim());
        }

        public static bool EsCorreoValido(string valor)
        {
            return !string.IsNullOrWhiteSpace(valor) && PatronCorreo.IsMatch(valor.Trim());
        }

        // Contrasena: exactamente 14 caracteres, con mayuscula, minuscula, numero y caracter especial.
        public static bool EsContrasenaValida(string valor)
        {
            if (string.IsNullOrEmpty(valor) || valor.Length != 14)
            {
                return false;
            }

            return PatronMayuscula.IsMatch(valor)
                && PatronMinuscula.IsMatch(valor)
                && PatronNumero.IsMatch(valor)
                && PatronEspecial.IsMatch(valor);
        }

        // Numero de tarjeta: 12 digitos (se aceptan espacios/guiones como separadores de grupos de 4).
        public static bool EsNumeroTarjetaValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            string soloDigitos = Regex.Replace(valor, @"[^0-9]", "");
            return soloDigitos.Length == 12;
        }

        public static bool EsCodigoSeguridadValido(string valor)
        {
            return !string.IsNullOrWhiteSpace(valor) && Regex.IsMatch(valor.Trim(), @"^[0-9]{3}$");
        }

        // Fecha de vencimiento en formato MM/AA. Debe ser igual o posterior al mes actual.
        public static bool EsFechaVencimientoValida(string mes, string anio)
        {
            if (!Regex.IsMatch(mes ?? "", @"^(0[1-9]|1[0-2])$"))
            {
                return false;
            }

            if (!Regex.IsMatch(anio ?? "", @"^[0-9]{2}$"))
            {
                return false;
            }

            int mesNum = int.Parse(mes);
            int anioNum = 2000 + int.Parse(anio);

            DateTime vencimiento = new DateTime(anioNum, mesNum, 1);
            DateTime mesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            return vencimiento >= mesActual;
        }

        // Monto valido: entero positivo, sin decimales.
        public static bool EsMontoValido(string valor, out int monto)
        {
            return int.TryParse(valor, out monto) && monto > 0;
        }
    }
}
