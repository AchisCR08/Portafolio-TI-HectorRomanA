package Servicios;

import org.json.JSONObject;
import utilidades.Configuracion;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetSocketAddress;
import java.net.Socket;

public class IdentificadorCliente {

    public boolean notificarCambioLinea(JSONObject trama) {
        if (!debeNotificar()) {
            return true;
        }

        String host = Configuracion.obtener("identificador.host", "localhost");
        int puerto = Integer.parseInt(Configuracion.obtener("identificador.puerto", "8000"));
        int timeout = Integer.parseInt(Configuracion.obtener("identificador.timeout.ms", "5000"));

        try (Socket socket = new Socket()) {
            socket.connect(new InetSocketAddress(host, puerto), timeout);
            socket.setSoTimeout(timeout);

            PrintWriter salida = new PrintWriter(socket.getOutputStream(), true);
            BufferedReader entrada = new BufferedReader(
                    new InputStreamReader(socket.getInputStream())
            );

            salida.println(trama);
            String respuestaTexto = entrada.readLine();
            JSONObject respuesta = new JSONObject(respuestaTexto);
            return "OK".equalsIgnoreCase(respuesta.optString("status"));
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }

    private boolean debeNotificar() {
        return Boolean.parseBoolean(
                Configuracion.obtener("identificador.notificar", "false")
        );
    }
}
