package Controladores;

import Servicios.ProveedorServicio;
import org.json.JSONObject;
import utilidades.Formateador;

public class MensajeControlador {
    private final ProveedorServicio servicio = new ProveedorServicio();

    public String procesarMensaje(String mensajeJson) {
        try {
            JSONObject json = new JSONObject(mensajeJson);
            String tipo = json.getString("tipo");

            switch (tipo) {
                case "verificar_saldo":
                    return procesarVerificacionSaldo(json).toString();

                case "registrar_llamada":
                    return procesarRegistroLlamada(json).toString();

                case "registrar_linea":
                case "registrar_linea_disponible":
                    return procesarRegistroLinea(json).toString();

                case "cambiar_estado_linea":
                case "activar_linea":
                case "desactivar_linea":
                    return procesarCambioEstadoLinea(json).toString();

                case "calcular_facturacion_postpago":
                case "calcular_postpago":
                case "facturacion_postpago":
                    return procesarFacturacionPostpago(json).toString();

                default:
                    return respuestaError().toString();
            }
        } catch (Exception e) {
            e.printStackTrace();
            return respuestaError().toString();
        }
    }

    private JSONObject procesarVerificacionSaldo(JSONObject json) {
        String telefono = json.getString("telefono");
        int tipoTransaccion = json.getInt("tipoTransaccion"); // 1=llamada, 2=consulta
        int tipoLlamada = json.optInt("tipoLlamada", 1);      // 1,2,3
        String telefonoDestino = json.optString("telefonoDestino", "");

        if (tipoTransaccion == 1 && telefonoDestino.isBlank()) {
            return respuestaError();
        }

        return servicio.verificarSaldo(telefono, tipoTransaccion, tipoLlamada, telefonoDestino);
    }

    private JSONObject procesarRegistroLlamada(JSONObject json) {
        String telefonoOrigen = json.getString("telefono");
        int fecha = json.getInt("fecha"); // YYYYMMDD
        int hora = json.getInt("hora");   // HHMMSS
        String telefonoDestino = json.getString("telefonoDestino");
        long costoTotal = json.getLong("costoTotal"); // centesimas
        int duracionSegundos = obtenerDuracionEnSegundos(json);

        return servicio.registrarLlamada(
                telefonoOrigen,
                fecha,
                hora,
                telefonoDestino,
                costoTotal,
                duracionSegundos
        );
    }

    private JSONObject procesarRegistroLinea(JSONObject json) {
        return servicio.registrarLineaDisponible(
                json.optString("telefono", ""),
                json.optString("identificadorTelefono", ""),
                json.optString("identificadorTarjeta", ""),
                json.optString("tipoServicio", json.optString("tipoLinea", "")),
                json.optString("estado", "")
        );
    }

    private JSONObject procesarCambioEstadoLinea(JSONObject json) {
        String estado = json.optString("estado", "");

        if (estado.isBlank()) {
            String tipo = json.optString("tipo", "");
            if ("activar_linea".equalsIgnoreCase(tipo)) {
                estado = "ACTIVAR";
            } else if ("desactivar_linea".equalsIgnoreCase(tipo)) {
                estado = "DESACTIVAR";
            }
        }

        return servicio.cambiarEstadoLinea(
                json.optString("telefono", ""),
                json.optString("identificadorTelefono", ""),
                json.optString("identificadorTarjeta", ""),
                json.optString("tipoServicio", json.optString("tipoLinea", "")),
                obtenerIdentificacionDuenno(json),
                estado
        );
    }

    private String obtenerIdentificacionDuenno(JSONObject json) {
        String valor = json.optString("identificacionDuenno", "");
        if (!valor.isBlank()) {
            return valor;
        }

        valor = json.optString("identificacionDueno", "");
        if (!valor.isBlank()) {
            return valor;
        }

        valor = json.optString("identificacionCliente", "");
        if (!valor.isBlank()) {
            return valor;
        }

        return json.optString("identificacion", "");
    }

    private JSONObject procesarFacturacionPostpago(JSONObject json) {
        return servicio.calcularFacturacionPostpago(
                obtenerFechaCalculo(json),
                obtenerFechaMaximaPago(json)
        );
    }

    private String obtenerFechaCalculo(JSONObject json) {
        String valor = json.optString("fechaCalculo", "");
        if (!valor.isBlank()) {
            return valor;
        }

        valor = json.optString("fecha_calculo", "");
        if (!valor.isBlank()) {
            return valor;
        }

        return json.optString("fecha", "");
    }

    private String obtenerFechaMaximaPago(JSONObject json) {
        String valor = json.optString("fechaMaximaPago", "");
        if (!valor.isBlank()) {
            return valor;
        }

        valor = json.optString("fecha_maxima_pago", "");
        if (!valor.isBlank()) {
            return valor;
        }

        return json.optString("fechaPago", "");
    }

    private int obtenerDuracionEnSegundos(JSONObject json) {
        Object valor = json.get("duracion");

        if (valor instanceof Number) {
            return ((Number) valor).intValue();
        }

        String duracion = String.valueOf(valor);
        if (!duracion.matches("\\d{6}")) {
            throw new IllegalArgumentException("La duracion debe venir en formato HHMMSS");
        }

        return Formateador.hhmmssASegundos(duracion);
    }

    private JSONObject respuestaError() {
        return new JSONObject().put("status", "ERROR");
    }
}
