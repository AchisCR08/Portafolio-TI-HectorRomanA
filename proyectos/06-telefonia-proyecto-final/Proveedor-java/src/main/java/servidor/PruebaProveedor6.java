package servidor;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;

public class PruebaProveedor6 {
    private static final String HOST = "localhost";
    private static final int PUERTO = 6000;

    public static void main(String[] args) {
        probar("PROVEEDOR6 - calcular facturacion postpago", new JSONObject()
                .put("tipo", "calcular_facturacion_postpago")
                .put("fechaCalculo", "2026-07-20")
                .put("fechaMaximaPago", "2026-07-30")
        );

        probar("PROVEEDOR6 - fecha maxima menor a fecha calculo", new JSONObject()
                .put("tipo", "calcular_facturacion_postpago")
                .put("fechaCalculo", "20260720")
                .put("fechaMaximaPago", "20260710")
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
