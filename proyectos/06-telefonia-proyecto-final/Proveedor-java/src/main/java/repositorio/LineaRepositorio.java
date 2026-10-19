package repositorio;

import modelos.LineaTelefonica;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class LineaRepositorio {

    public boolean existeTelefono(String telefono) {
        String sql = "SELECT 1 FROM Cuentas WHERE numero_telefono = ?";

        try (Connection conn = DatabaseConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setString(1, telefono);

            try (ResultSet rs = stmt.executeQuery()) {
                return rs.next();
            }
        } catch (SQLException e) {
            e.printStackTrace();
            throw new IllegalStateException("No se pudo validar si el telefono existe", e);
        }
    }

    public boolean registrarDisponible(LineaTelefonica linea) {
        String sql = """
                INSERT INTO Cuentas (
                    numero_telefono,
                    numero_telefono_cifrado,
                    identificador_telefono,
                    identificador_tarjeta,
                    tipo_servicio,
                    saldo,
                    proveedor,
                    bono_mismo_proveedor,
                    estado
                )
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)
                """;

        try (Connection conn = DatabaseConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setString(1, linea.getNumeroTelefono());
            stmt.setString(2, linea.getNumeroTelefonoCifrado());
            stmt.setString(3, linea.getIdentificadorTelefono());
            stmt.setString(4, linea.getIdentificadorTarjeta());
            stmt.setString(5, linea.getTipoServicio());
            stmt.setDouble(6, linea.getSaldo());
            stmt.setString(7, linea.getProveedor());
            stmt.setDouble(8, linea.getBonoMismoProveedor());
            stmt.setString(9, linea.getEstado());

            return stmt.executeUpdate() > 0;
        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        }
    }

    public LineaTelefonica obtenerPorTelefono(String telefono) {
        String sql = """
                SELECT
                    numero_telefono,
                    numero_telefono_cifrado,
                    identificador_telefono,
                    identificador_tarjeta,
                    tipo_servicio,
                    saldo,
                    proveedor,
                    bono_mismo_proveedor,
                    estado,
                    identificacion_duenno
                FROM Cuentas
                WHERE numero_telefono = ?
                """;

        try (Connection conn = DatabaseConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setString(1, telefono);

            try (ResultSet rs = stmt.executeQuery()) {
                if (!rs.next()) {
                    return null;
                }

                LineaTelefonica linea = new LineaTelefonica();
                linea.setNumeroTelefono(rs.getString("numero_telefono"));
                linea.setNumeroTelefonoCifrado(rs.getString("numero_telefono_cifrado"));
                linea.setIdentificadorTelefono(rs.getString("identificador_telefono"));
                linea.setIdentificadorTarjeta(rs.getString("identificador_tarjeta"));
                linea.setTipoServicio(rs.getString("tipo_servicio"));
                linea.setSaldo(rs.getDouble("saldo"));
                linea.setProveedor(rs.getString("proveedor"));
                linea.setBonoMismoProveedor(rs.getDouble("bono_mismo_proveedor"));
                linea.setEstado(rs.getString("estado"));
                linea.setIdentificacionDuenno(rs.getString("identificacion_duenno"));
                return linea;
            }
        } catch (SQLException e) {
            e.printStackTrace();
            throw new IllegalStateException("No se pudo consultar la linea telefonica", e);
        }
    }

    public boolean activarLinea(String telefono, String tipoServicio, String identificacionDuenno,
                                String telefonoCifrado, String identificadorTelefono,
                                String identificadorTarjeta) {
        String sql = """
                UPDATE Cuentas
                   SET estado = 'ACTIVO',
                       identificacion_duenno = ?,
                       saldo = CASE WHEN tipo_servicio = 'PREPAGO' THEN 1000.00 ELSE saldo END,
                       numero_telefono_cifrado = ?,
                       identificador_telefono = ?,
                       identificador_tarjeta = ?,
                       fecha_activacion = GETDATE(),
                       fecha_desactivacion = NULL
                 WHERE numero_telefono = ?
                   AND tipo_servicio = ?
                   AND estado = 'DISPONIBLE'
                """;

        try (Connection conn = DatabaseConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setString(1, identificacionDuenno);
            stmt.setString(2, telefonoCifrado);
            stmt.setString(3, identificadorTelefono);
            stmt.setString(4, identificadorTarjeta);
            stmt.setString(5, telefono);
            stmt.setString(6, tipoServicio);
            return stmt.executeUpdate() > 0;
        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        }
    }

    public boolean desactivarLinea(String telefono, String tipoServicio, String identificacionDuenno) {
        String sql = """
                UPDATE Cuentas
                   SET estado = 'INACTIVO',
                       identificacion_duenno = NULL,
                       fecha_desactivacion = GETDATE()
                 WHERE numero_telefono = ?
                   AND tipo_servicio = ?
                   AND estado = 'ACTIVO'
                   AND identificacion_duenno = ?
                """;

        try (Connection conn = DatabaseConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setString(1, telefono);
            stmt.setString(2, tipoServicio);
            stmt.setString(3, identificacionDuenno);
            return stmt.executeUpdate() > 0;
        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        }
    }
}
