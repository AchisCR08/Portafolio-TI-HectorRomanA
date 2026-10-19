package servidor;

import org.json.JSONObject;
import utilidades.CifradorAES;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;

public class PruebaProveedor4 {
    private static final String HOST = "localhost";
    private static final int PUERTO = 6000;

    public static void main(String[] args) {
        JSONObject registroCorrecto = new JSONObject()
                .put("tipo", "registrar_linea")
                .put("telefono", CifradorAES.cifrar("89990001"))
                .put("identificadorTelefono", CifradorAES.cifrar("4444444444444444"))
                .put("identificadorTarjeta", CifradorAES.cifrar("4444444444444444444"))
                .put("tipoServicio", "PREPAGO")
                .put("estado", "DISPONIBLE");

        probar("PROVEEDOR4 - registrar linea disponible", registroCorrecto);
        probar("PROVEEDOR4 - telefono en uso", registroCorrecto);

        probar("PROVEEDOR4 - datos incompletos", new JSONObject()
                .put("tipo", "registrar_linea")
                .put("telefono", CifradorAES.cifrar("89990002"))
                .put("tipoServicio", "POSTPAGO")
                .put("estado", "DISPONIBLE")
        );
    }

    private static void probar(String nombrePrueba, JSONObject mensaje) {
        System.out.println();
        System.out.println("------------------------------");
        System.out.println("Prueba: " + nombrePrueba);
        System.out.println("Enviando: " + mensaje);

        try (
                Socket socket = new Socket(HOST, PUERTO);
                PrintWriter salida = new PrintWriter(socket.getOutputStream(), true);
                BufferedReader entrada = new BufferedReader(
                        new InputStreamReader(socket.getInputStream()))
        ) {
            salida.println(mensaje);
            String respuesta = entrada.readLine();
            System.out.println("Respuesta: " + respuesta);
        } catch (Exception e) {
            System.out.println("ERROR conectando con Java: " + e.getMessage());
        }
    }
}
