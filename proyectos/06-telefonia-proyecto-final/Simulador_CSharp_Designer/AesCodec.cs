using System.Security.Cryptography;
using System.Text;

namespace SimuladorTelefoniaDesigner;

internal static class AesCodec
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("REEMPLAZAR_EN_CONFIGURACION_LOCAL");

    public static string Encrypt(string value)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor();
        var plain = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(encryptor.TransformFinalBlock(plain, 0, plain.Length));
    }
}
