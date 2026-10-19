using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace WebCentralAdmin.Utilidades
{
    // Mismo esquema que utilidades.CifradorAES (Java), Capa_Negocio/CifradorAES.py
    // y Utilidades/CifradorAES.cs de WS_Autenticacion / WS_ConsultasProveedor:
    // AES / ECB / PKCS7 (PKCS5 = PKCS7 para bloques de 16 bytes) / Base64.
    public static class CifradorAES
    {
        private static byte[] ObtenerLlave()
        {
            string clave = ConfigurationManager.AppSettings["Cifrado.Clave"] ?? "REEMPLAZAR_EN_CONFIGURACION_LOCAL";
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
