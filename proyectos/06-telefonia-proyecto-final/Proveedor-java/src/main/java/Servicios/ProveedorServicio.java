package Servicios;

import modelos.Cuenta;
import modelos.Llamada;
import modelos.LineaTelefonica;
import modelos.Tarifa;
import org.json.JSONObject;
import repositorio.CuentaRepositorio;
import repositorio.FacturacionRepositorio;
import repositorio.LlamadaRepositorio;
import repositorio.LineaRepositorio;
import repositorio.TarifaRepositorio;
import utilidades.CifradorAES;
import utilidades.Configuracion;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;

public class ProveedorServicio {
    private final CuentaRepositorio cuentaRepo = new CuentaRepositorio();
    private final TarifaRepositorio tarifaRepo = new TarifaRepositorio();
    private final LlamadaRepositorio llamadaRepo = new LlamadaRepositorio();
    private final LineaRepositorio lineaRepo = new LineaRepositorio();
    private final FacturacionRepositorio facturacionRepo = new FacturacionRepositorio();
    private final IdentificadorCliente identificadorCliente = new IdentificadorCliente();

    public JSONObject verificarSaldo(String telefono, int tipoTransaccion,
                                     int tipoLlamada, String telefonoDestino) {
        JSONObject respuesta = new JSONObject();

        try {
            Cuenta cuenta = cuentaRepo.obtenerPorTelefono(telefono);
            if (cuenta == null) {
                return respuesta.put("status", "ERROR");
            }

            if (tipoTransaccion == 2) {
                return consultarSaldo(cuenta);
            }

            if (tipoTransaccion != 1) {
                return respuesta.put("status", "ERROR");
            }

            return autorizarLlamada(cuenta, tipoLlamada, telefonoDestino);
        } catch (Exception e) {
            e.printStackTrace();
            return respuesta.put("status", "ERROR");
        }
    }

    public JSONObject registrarLlamada(String telefonoOrigen, int fecha, int hora,
                                       String telefonoDestino, long costoTotal,
                                       int duracionSegundos) {
        JSONObject respuesta = new JSONObject();

        try {
            Cuenta cuenta = cuentaRepo.obtenerPorTelefono(telefonoOrigen);
            if (cuenta == null) {
                return respuesta.put("status", "ERROR");
            }

            boolean mismoProveedor = esDestinoMismoProveedor(cuenta, telefonoDestino);
            double costoColones = costoTotal / 100.0;
            if (!cuenta.esPostpago() && !tieneFondosParaRebajo(cuenta, costoColones, mismoProveedor)) {
                return respuesta.put("status", "ERROR");
            }

            Llamada llamada = new Llamada(
                    telefonoOrigen,
                    fecha,
                    hora,
                    telefonoDestino,
                    costoTotal,
                    duracionSegundos
            );

            boolean registrada = llamadaRepo.registrarLlamada(llamada);
            if (!registrada) {
                return respuesta.put("status", "ERROR");
            }

            if (!cuenta.esPostpago()) {
                boolean saldoActualizado = rebajarPrepago(cuenta, telefonoOrigen, costoColones, mismoProveedor);
                if (!saldoActualizado) {
                    return respuesta.put("status", "ERROR");
                }
            }

            return respuesta.put("status", "OK");
        } catch (Exception e) {
            e.printStackTrace();
            return respuesta.put("status", "ERROR");
        }
    }

    public JSONObject registrarLineaDisponible(String telefono, String identificadorTelefono,
                                               String identificadorTarjeta, String tipoServicio,
                                               String estado) {
        JSONObject respuesta = new JSONObject();

        try {
            if (estaVacio(telefono) || estaVacio(identificadorTelefono)
                    || estaVacio(identificadorTarjeta) || estaVacio(tipoServicio)
                    || estaVacio(estado)) {
                return respuesta.put("status", "Datos Incompletos");
            }

            String telefonoPlano = normalizarTelefonoNacional(CifradorAES.descifrarSiEsPosible(telefono));
            String idTelefonoPlano = CifradorAES.descifrarSiEsPosible(identificadorTelefono).trim();
            String idTarjetaPlano = CifradorAES.descifrarSiEsPosible(identificadorTarjeta).trim();
            String tipoServicioNormalizado = tipoServicio.trim().toUpperCase();
            String estadoNormalizado = estado.trim().toUpperCase();

            if (!telefonoPlano.matches("\\d{8,15}")
                    || !idTelefonoPlano.matches("\\d{16}")
                    || !idTarjetaPlano.matches("\\d{19}")
                    || !esTipoServicioValido(tipoServicioNormalizado)
                    || !"DISPONIBLE".equals(estadoNormalizado)) {
                return respuesta.put("status", "Datos Incompletos");
            }

            if (lineaRepo.existeTelefono(telefonoPlano)) {
                return respuesta.put("status", "Tel\u00e9fono en uso");
            }

            LineaTelefonica linea = new LineaTelefonica();
            linea.setNumeroTelefono(telefonoPlano);
            linea.setNumeroTelefonoCifrado(obtenerValorCifrado(telefono, telefonoPlano));
            linea.setIdentificadorTelefono(obtenerValorCifrado(identificadorTelefono, idTelefonoPlano));
            linea.setIdentificadorTarjeta(obtenerValorCifrado(identificadorTarjeta, idTarjetaPlano));
            linea.setTipoServicio(tipoServicioNormalizado);
            linea.setSaldo(0.00);
            linea.setProveedor(Configuracion.obtener("proveedor.nombre", "XYZ"));
            linea.setBonoMismoProveedor(0.00);
            linea.setEstado(estadoNormalizado);

            boolean registrada = lineaRepo.registrarDisponible(linea);
            return respuesta.put("status", registrada ? "OK" : "ERROR");
        } catch (Exception e) {
            e.printStackTrace();
            return respuesta.put("status", "ERROR");
        }
    }

    public JSONObject cambiarEstadoLinea(String telefono, String identificadorTelefono,
                                         String identificadorTarjeta, String tipoServicio,
                                         String identificacionDuenno, String estado) {
        JSONObject respuesta = new JSONObject();

        try {
            if (estaVacio(telefono) || estaVacio(identificadorTelefono)
                    || estaVacio(identificadorTarjeta) || estaVacio(tipoServicio)
                    || estaVacio(identificacionDuenno) || estaVacio(estado)) {
                return respuesta.put("status", "Datos Incompletos");
            }

            String telefonoPlano = normalizarTelefonoNacional(CifradorAES.descifrarSiEsPosible(telefono));
            String idTelefonoPlano = CifradorAES.descifrarSiEsPosible(identificadorTelefono).trim();
            String idTarjetaPlano = CifradorAES.descifrarSiEsPosible(identificadorTarjeta).trim();
            String tipoServicioNormalizado = tipoServicio.trim().toUpperCase();
            String identificacionNormalizada = identificacionDuenno.trim();
            String accion = normalizarAccionEstado(estado);

            if (!telefonoPlano.matches("\\d{8,15}")
                    || !idTelefonoPlano.matches("\\d{16}")
                    || !idTarjetaPlano.matches("\\d{19}")
                    || !esTipoServicioValido(tipoServicioNormalizado)
                    || accion == null) {
                return respuesta.put("status", "Datos Incompletos");
            }

            LineaTelefonica linea = lineaRepo.obtenerPorTelefono(telefonoPlano);

            if ("ACTIVAR".equals(accion)) {
                return activarLinea(
                        respuesta,
                        linea,
                        telefono,
                        telefonoPlano,
                        identificadorTelefono,
                        idTelefonoPlano,
                        identificadorTarjeta,
                        idTarjetaPlano,
                        tipoServicioNormalizado,
                        identificacionNormalizada
                );
            }

            return desactivarLinea(
                    respuesta,
                    linea,
                    telefono,
                    telefonoPlano,
                    identificadorTelefono,
                    idTelefonoPlano,
                    identificadorTarjeta,
                    idTarjetaPlano,
                    tipoServicioNormalizado,
                    identificacionNormalizada
            );
        } catch (Exception e) {
            e.printStackTrace();
            return respuesta.put("status", "ERROR");
        }
    }

    public JSONObject calcularFacturacionPostpago(String fechaCalculo, String fechaMaximaPago) {
        JSONObject respuesta = new JSONObject();

        try {
            if (estaVacio(fechaCalculo) || estaVacio(fechaMaximaPago)) {
                return respuesta.put("status", "ERROR");
            }

            LocalDate fechaCalculoValida = parsearFecha(fechaCalculo);
            LocalDate fechaMaximaPagoValida = parsearFecha(fechaMaximaPago);

            if (fechaMaximaPagoValida.isBefore(fechaCalculoValida)) {
                return respuesta.put("status", "ERROR");
            }

            boolean calculado = facturacionRepo.calcularFacturacionPostpago(
                    fechaCalculoValida,
                    fechaMaximaPagoValida
            );

            return respuesta.put("status", calculado ? "OK" : "ERROR");
        } catch (Exception e) {
            e.printStackTrace();
            return respuesta.put("status", "ERROR");
        }
    }

    private JSONObject consultarSaldo(Cuenta cuenta) {
        JSONObject respuesta = new JSONObject();
        respuesta.put("status", "OK");

        if (cuenta.esPostpago()) {
            respuesta.put("saldo", "-1");
        } else {
            long centesimas = Math.round(cuenta.getSaldo() * 100);
            respuesta.put("saldo", formatearSaldo(centesimas));
        }

        return respuesta;
    }

    private JSONObject autorizarLlamada(Cuenta cuenta, int tipoLlamada, String telefonoDestino) {
        JSONObject respuesta = new JSONObject();

        if (tipoLlamada < 1 || tipoLlamada > 3) {
            return respuesta.put("status", "ERROR");
        }

        String tipoDestino = determinarTipoDestino(telefonoDestino, tipoLlamada);
        String prefijo = tipoLlamada == 3 ? extraerPrefijo(telefonoDestino) : null;
        Tarifa tarifa = tarifaRepo.obtenerTarifa(tipoLlamada, tipoDestino, prefijo);

        if (tarifa == null) {
            return respuesta.put("status", "ERROR");
        }

        double costoPorMinuto = tarifa.getCostoPorMinuto();

        if (cuenta.esPostpago()) {
            respuesta.put("status", "OK");
            respuesta.put("tarifa", "9999999999");
            respuesta.put("tiempo", "245959");
            return respuesta;
        }

        double saldoDisponible = saldoDisponibleParaLlamada(cuenta, tipoLlamada);

        if (saldoDisponible < costoPorMinuto) {
            return respuesta.put("status", "INSUF");
        }

        long tarifaCentesimas = Math.round(costoPorMinuto * 100);
        respuesta.put("status", "OK");
        respuesta.put("tarifa", formatearTarifa(tarifaCentesimas));
        respuesta.put("tiempo", calcularTiempoDisponible(saldoDisponible, costoPorMinuto));
        return respuesta;
    }

    private double saldoDisponibleParaLlamada(Cuenta cuenta, int tipoLlamada) {
        if (tipoLlamada == 1) {
            return cuenta.getSaldo() + cuenta.getBonoMismoProveedor();
        }
        return cuenta.getSaldo();
    }

    private boolean rebajarPrepago(Cuenta cuenta, String telefonoOrigen,
                                   double costoColones, boolean mismoProveedor) {
        double nuevoSaldo = cuenta.getSaldo();
        double nuevoBono = cuenta.getBonoMismoProveedor();

        if (mismoProveedor && nuevoBono > 0) {
            double aplicadoBono = Math.min(nuevoBono, costoColones);
            nuevoBono -= aplicadoBono;
            costoColones -= aplicadoBono;
        }

        if (nuevoSaldo < costoColones) {
            return false;
        }

        nuevoSaldo -= costoColones;
        return cuentaRepo.actualizarSaldoYBono(telefonoOrigen, nuevoSaldo, nuevoBono);
    }

    private boolean tieneFondosParaRebajo(Cuenta cuenta, double costoColones, boolean mismoProveedor) {
        double disponible = cuenta.getSaldo();
        if (mismoProveedor) {
            disponible += cuenta.getBonoMismoProveedor();
        }
        return disponible >= costoColones;
    }

    private boolean esDestinoMismoProveedor(Cuenta cuentaOrigen, String telefonoDestino) {
        String destino = normalizarTelefonoNacional(telefonoDestino);
        Cuenta cuentaDestino = cuentaRepo.obtenerPorTelefono(destino);
        return cuentaDestino != null
                && cuentaOrigen.getProveedor() != null
                && cuentaOrigen.getProveedor().equalsIgnoreCase(cuentaDestino.getProveedor());
    }

    private String determinarTipoDestino(String telefonoDestino, int tipoLlamada) {
        if (tipoLlamada == 3) {
            return null;
        }

        if (telefonoDestino == null || telefonoDestino.isBlank()) {
            return "fijo";
        }

        char primero = telefonoDestino.charAt(0);
        if (primero == '6' || primero == '7' || primero == '8') {
            return "movil";
        }

        return "fijo";
    }

    private String extraerPrefijo(String telefonoDestino) {
        if (telefonoDestino == null) {
            return "";
        }

        String numero = telefonoDestino.replace("+", "").trim();
        return numero.length() >= 4 ? numero.substring(0, 4) : numero;
    }

    private String normalizarTelefonoNacional(String telefono) {
        if (telefono == null) {
            return "";
        }

        String numero = telefono.replace("+", "")
                .replace("(", "")
                .replace(")", "")
                .replace("-", "")
                .replace(" ", "")
                .trim();

        if (numero.startsWith("506") && numero.length() == 11) {
            return numero.substring(3);
        }

        return numero;
    }

    private boolean estaVacio(String valor) {
        return valor == null || valor.isBlank();
    }

    private boolean esTipoServicioValido(String tipoServicio) {
        return "PREPAGO".equals(tipoServicio) || "POSTPAGO".equals(tipoServicio);
    }

    private LocalDate parsearFecha(String fecha) {
        String valor = fecha.trim();

        try {
            if (valor.matches("\\d{8}")) {
                return LocalDate.parse(valor, DateTimeFormatter.BASIC_ISO_DATE);
            }

            return LocalDate.parse(valor, DateTimeFormatter.ISO_LOCAL_DATE);
        } catch (DateTimeParseException e) {
            throw new IllegalArgumentException("Fecha invalida: " + fecha, e);
        }
    }

    private JSONObject activarLinea(JSONObject respuesta, LineaTelefonica linea,
                                    String telefonoOriginal, String telefonoPlano,
                                    String idTelefonoOriginal, String idTelefonoPlano,
                                    String idTarjetaOriginal, String idTarjetaPlano,
                                    String tipoServicio, String identificacionDuenno) {
        if (linea == null
                || !"DISPONIBLE".equalsIgnoreCase(linea.getEstado())
                || !tipoServicio.equalsIgnoreCase(linea.getTipoServicio())
                || !coincideIdentificadorGuardado(linea.getIdentificadorTelefono(), idTelefonoPlano)
                || !coincideIdentificadorGuardado(linea.getIdentificadorTarjeta(), idTarjetaPlano)) {
            return respuesta.put("status", "Tel\u00e9fono en uso");
        }

        String telefonoCifrado = obtenerValorCifrado(telefonoOriginal, telefonoPlano);
        String idTelefonoCifrado = obtenerValorCifrado(idTelefonoOriginal, idTelefonoPlano);
        String idTarjetaCifrado = obtenerValorCifrado(idTarjetaOriginal, idTarjetaPlano);

        JSONObject tramaIdentificador = crearTramaIdentificador(
                telefonoCifrado,
                idTelefonoCifrado,
                idTarjetaCifrado,
                tipoServicio,
                identificacionDuenno,
                "ACTIVO"
        );

        if (!identificadorCliente.notificarCambioLinea(tramaIdentificador)) {
            return respuesta.put("status", "Activaci\u00f3n fallida");
        }

        boolean actualizado = lineaRepo.activarLinea(
                telefonoPlano,
                tipoServicio,
                identificacionDuenno,
                telefonoCifrado,
                idTelefonoCifrado,
                idTarjetaCifrado
        );

        return respuesta.put("status", actualizado ? "OK" : "ERROR");
    }

    private JSONObject desactivarLinea(JSONObject respuesta, LineaTelefonica linea,
                                       String telefonoOriginal, String telefonoPlano,
                                       String idTelefonoOriginal, String idTelefonoPlano,
                                       String idTarjetaOriginal, String idTarjetaPlano,
                                       String tipoServicio, String identificacionDuenno) {
        if (linea == null
                || !"ACTIVO".equalsIgnoreCase(linea.getEstado())
                || !tipoServicio.equalsIgnoreCase(linea.getTipoServicio())
                || !identificacionDuenno.equals(linea.getIdentificacionDuenno())
                || !coincideIdentificadorGuardado(linea.getIdentificadorTelefono(), idTelefonoPlano)
                || !coincideIdentificadorGuardado(linea.getIdentificadorTarjeta(), idTarjetaPlano)) {
            return respuesta.put("status", "Tel\u00e9fono no corresponde");
        }

        JSONObject tramaIdentificador = crearTramaIdentificador(
                obtenerValorCifrado(telefonoOriginal, telefonoPlano),
                obtenerValorCifrado(idTelefonoOriginal, idTelefonoPlano),
                obtenerValorCifrado(idTarjetaOriginal, idTarjetaPlano),
                tipoServicio,
                identificacionDuenno,
                "INACTIVO"
        );

        if (!identificadorCliente.notificarCambioLinea(tramaIdentificador)) {
            return respuesta.put("status", "Activaci\u00f3n fallida");
        }

        boolean actualizado = lineaRepo.desactivarLinea(
                telefonoPlano,
                tipoServicio,
                identificacionDuenno
        );

        return respuesta.put("status", actualizado ? "OK" : "ERROR");
    }

    private String normalizarAccionEstado(String estado) {
        String valor = estado.trim().toUpperCase();
        if ("ACTIVAR".equals(valor) || "ACTIVO".equals(valor)) {
            return "ACTIVAR";
        }
        if ("DESACTIVAR".equals(valor) || "INACTIVAR".equals(valor) || "INACTIVO".equals(valor)) {
            return "DESACTIVAR";
        }
        return null;
    }

    private boolean coincideIdentificadorGuardado(String guardado, String valorPlano) {
        if (guardado == null || guardado.isBlank()) {
            return true;
        }

        String guardadoPlano = CifradorAES.descifrarSiEsPosible(guardado).trim();
        return guardadoPlano.equals(valorPlano);
    }

    private JSONObject crearTramaIdentificador(String telefono, String identificadorTelefono,
                                               String identificadorTarjeta, String tipoServicio,
                                               String identificacionDuenno, String estado) {
        return new JSONObject()
                .put("tipo", "actualizar_linea")
                .put("telefono", telefono)
                .put("identificadorTelefono", identificadorTelefono)
                .put("identificadorTarjeta", identificadorTarjeta)
                .put("tipoServicio", tipoServicio)
                .put("identificacionDuenno", identificacionDuenno)
                .put("estado", estado);
    }

    private String obtenerValorCifrado(String valorOriginal, String valorPlano) {
        String descifrado = CifradorAES.descifrarSiEsPosible(valorOriginal);
        if (descifrado != null && descifrado.trim().equals(valorPlano)) {
            try {
                CifradorAES.descifrar(valorOriginal);
                return valorOriginal;
            } catch (IllegalArgumentException e) {
                return CifradorAES.cifrar(valorPlano);
            }
        }

        return CifradorAES.cifrar(valorPlano);
    }

    private String calcularTiempoDisponible(double saldo, double costoPorMinuto) {
        if (costoPorMinuto <= 0) {
            return "000000";
        }

        int totalSegundos = (int) ((saldo / costoPorMinuto) * 60);
        int horas = totalSegundos / 3600;
        int minutos = (totalSegundos % 3600) / 60;
        int segundos = totalSegundos % 60;

        if (horas > 99) {
            horas = 99;
            minutos = 59;
            segundos = 59;
        }

        return String.format("%02d%02d%02d", horas, minutos, segundos);
    }

    private String formatearTarifa(long centesimas) {
        return String.format("%010d", centesimas);
    }

    private String formatearSaldo(long centesimas) {
        return String.format("%019d", centesimas);
    }
}
