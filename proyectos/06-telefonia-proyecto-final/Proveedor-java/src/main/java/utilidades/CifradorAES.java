package utilidades;

import javax.crypto.Cipher;
import javax.crypto.spec.SecretKeySpec;
import java.nio.charset.StandardCharsets;
import java.util.Base64;

public class CifradorAES {
    private static final String ALGORITMO = "AES";
    private static final String TRANSFORMACION = "AES/ECB/PKCS5Padding";

    private CifradorAES() {
    }

    public static String cifrar(String textoPlano) {
        try {
            Cipher cipher = Cipher.getInstance(TRANSFORMACION);
            cipher.init(Cipher.ENCRYPT_MODE, crearLlave());
            byte[] cifrado = cipher.doFinal(textoPlano.getBytes(StandardCharsets.UTF_8));
            return Base64.getEncoder().encodeToString(cifrado);
        } catch (Exception e) {
            throw new IllegalStateException("No se pudo cifrar el dato", e);
        }
    }

    public static String descifrar(String textoCifrado) {
        try {
            Cipher cipher = Cipher.getInstance(TRANSFORMACION);
            cipher.init(Cipher.DECRYPT_MODE, crearLlave());
            byte[] bytes = Base64.getDecoder().decode(textoCifrado);
            byte[] descifrado = cipher.doFinal(bytes);
            return new String(descifrado, StandardCharsets.UTF_8);
        } catch (Exception e) {
            throw new IllegalArgumentException("El dato no se pudo descifrar", e);
        }
    }

    public static String descifrarSiEsPosible(String valor) {
        if (valor == null || valor.isBlank()) {
            return valor;
        }

        try {
            return descifrar(valor);
        } catch (IllegalArgumentException e) {
            return valor;
        }
    }

    private static SecretKeySpec crearLlave() {
        String clave = Configuracion.obtener("cifrado.clave", "REEMPLAZAR_EN_CONFIGURACION_LOCAL");
        byte[] bytes = clave.getBytes(StandardCharsets.UTF_8);

        if (bytes.length != 16) {
            throw new IllegalStateException("La clave AES debe tener 16 caracteres");
        }

        return new SecretKeySpec(bytes, ALGORITMO);
    }
}
