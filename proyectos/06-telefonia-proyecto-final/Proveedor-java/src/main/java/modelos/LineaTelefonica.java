package modelos;

public class LineaTelefonica {
    private String numeroTelefono;
    private String numeroTelefonoCifrado;
    private String identificadorTelefono;
    private String identificadorTarjeta;
    private String tipoServicio;
    private double saldo;
    private String proveedor;
    private String estado;
    private double bonoMismoProveedor;
    private String identificacionDuenno;

    public String getNumeroTelefono() {
        return numeroTelefono;
    }

    public void setNumeroTelefono(String numeroTelefono) {
        this.numeroTelefono = numeroTelefono;
    }

    public String getNumeroTelefonoCifrado() {
        return numeroTelefonoCifrado;
    }

    public void setNumeroTelefonoCifrado(String numeroTelefonoCifrado) {
        this.numeroTelefonoCifrado = numeroTelefonoCifrado;
    }

    public String getIdentificadorTelefono() {
        return identificadorTelefono;
    }

    public void setIdentificadorTelefono(String identificadorTelefono) {
        this.identificadorTelefono = identificadorTelefono;
    }

    public String getIdentificadorTarjeta() {
        return identificadorTarjeta;
    }

    public void setIdentificadorTarjeta(String identificadorTarjeta) {
        this.identificadorTarjeta = identificadorTarjeta;
    }

    public String getTipoServicio() {
        return tipoServicio;
    }

    public void setTipoServicio(String tipoServicio) {
        this.tipoServicio = tipoServicio;
    }

    public double getSaldo() {
        return saldo;
    }

    public void setSaldo(double saldo) {
        this.saldo = saldo;
    }

    public String getProveedor() {
        return proveedor;
    }

    public void setProveedor(String proveedor) {
        this.proveedor = proveedor;
    }

    public String getEstado() {
        return estado;
    }

    public void setEstado(String estado) {
        this.estado = estado;
    }

    public double getBonoMismoProveedor() {
        return bonoMismoProveedor;
    }

    public void setBonoMismoProveedor(double bonoMismoProveedor) {
        this.bonoMismoProveedor = bonoMismoProveedor;
    }

    public String getIdentificacionDuenno() {
        return identificacionDuenno;
    }

    public void setIdentificacionDuenno(String identificacionDuenno) {
        this.identificacionDuenno = identificacionDuenno;
    }
}
