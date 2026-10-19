package servidor;

import org.json.JSONObject;
import utilidades.CifradorAES;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;

public class PruebaProveedor5 {
    private static final String HOST = "localhost";
    private static final int PUERTO = 6000;

    public static void main(String[] args) {
        String telefono = "89990000";
        String idTelefono = "5555555555555555";
        String idTarjeta = "5555555555555555555";
        String identificacionDuenno = "118880999";

        JSONObject activar = crearTrama(
                "activar_linea",
                telefono,
                idTelefono,
                idTarjeta,
                "PREPAGO",
                identificacionDuenno,
                "ACTIVAR"
        );

        probar("PROVEEDOR5 - activar linea prepago disponible", activar);

        probar("PROVEEDOR5 - consultar saldo inicial 1000", new JSONObject()
                .put("tipo", "verificar_saldo")
                .put("telefono", telefono)
                .put("tipoTransaccion", 2)
        );

        probar("PROVEEDOR5 - activar nuevamente debe indicar telefono en uso", activar);

        probar("PROVEEDOR5 - desactivar linea activa", crearTrama(
                "desactivar_linea",
                telefono,
                idTelefono,
                idTarjeta,
                "PREPAGO",
                identificacionDuenno,
                "DESACTIVAR"
        ));

        probar("PROVEEDOR5 - desactivar con duenno incorrecto", crearTrama(
                "desactivar_linea",
                telefono,
                idTelefono,
                idTarjeta,
                "PREPAGO",
                "999999999",
                "DESACTIVAR"
        ));
    }

    private static JSONObject crearTrama(String tipo, String telefono, String idTelefono,
                                         String idTarjeta, String tipoServicio,
                                         String identificacionDuenno, String estado) {
        return new JSONObject()
                .put("tipo", tipo)
                .put("telefono", CifradorAES.cifrar(telefono))
                .put("identificadorTelefono", CifradorAES.cifrar(idTelefono))
                .put("identificadorTarjeta", CifradorAES.cifrar(idTarjeta))
                .put("tipoServicio", tipoServicio)
                .put("identificacionDuenno", identificacionDuenno)
                .put("estado", estado);
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
