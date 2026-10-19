using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace WS_Autenticacion.Utilidades
{
    // Mismo esquema que utilidades.CifradorAES (Java) y Capa_Negocio/CifradorAES.py:
    // AES / ECB / PKCS7 (PKCS5 = PKCS7 para bloques de 16 bytes) / Base64.
    // La clave se lee de Web.config (appSettings -> cifrado.clave) para poder
    // compartirla facilmente con el resto del equipo sin tocar codigo.
    public static class CifradorAES
    {
        private static byte[] ObtenerLlave()
        {
            string clave = ConfigurationManager.AppSettings["cifrado.clave"] ?? "REEMPLAZAR_EN_CONFIGURACION_LOCAL";
            byte[] bytes = Encoding.UTF8.GetBytes(clave);

            if (bytes.Length != 16)
            {
                throw new InvalidOperationException("La clave AES debe tener 16 caracteres.");
            }

            return bytes;
        }

        public static string Cifrar(string textoPlano)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = ObtenerLlave();
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] datos = Encoding.UTF8.GetBytes(textoPlano ?? string.Empty);
                    byte[] cifrado = encryptor.TransformFinalBlock(datos, 0, datos.Length);
                    return Convert.ToBase64String(cifrado);
                }
            }
        }

        public static string Descifrar(string textoCifrado)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = ObtenerLlave();
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] datos = Convert.FromBase64String(textoCifrado);
                    byte[] descifrado = decryptor.TransformFinalBlock(datos, 0, datos.Length);
                    return Encoding.UTF8.GetString(descifrado);
                }
            }
        }

        // Util para validar reglas de negocio (formato de contrasena, etc.)
        // sin explotar si el valor recibido no viene realmente cifrado.
        public static string DescifrarSiEsPosible(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return valor;
            }

            try
            {
                return Descifrar(valor);
            }
            catch
            {
                return valor;
            }
        }
    }
}

