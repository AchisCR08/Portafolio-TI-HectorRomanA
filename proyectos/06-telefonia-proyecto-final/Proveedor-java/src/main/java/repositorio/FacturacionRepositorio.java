package repositorio;

import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.Date;
import java.sql.SQLException;
import java.time.LocalDate;

public class FacturacionRepositorio {

    public boolean calcularFacturacionPostpago(LocalDate fechaCalculo, LocalDate fechaMaximaPago) {
        String sql = "{call dbo.sp_calcular_facturacion_postpago(?, ?)}";

        try (Connection conn = DatabaseConnection.getConnection();
             CallableStatement stmt = conn.prepareCall(sql)) {
            stmt.setDate(1, Date.valueOf(fechaCalculo));
            stmt.setDate(2, Date.valueOf(fechaMaximaPago));
            stmt.execute();
            return true;
        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        }
    }
}
