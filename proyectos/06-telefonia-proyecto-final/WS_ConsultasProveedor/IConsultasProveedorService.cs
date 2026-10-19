using System.ServiceModel;
using WS_ConsultasProveedor.Modelos;

namespace WS_ConsultasProveedor
{
    [ServiceContract]
    public interface IConsultasProveedorService
    {
        [OperationContract]
        LineaDisponibleDTO[] ListarLineasDisponibles();

        [OperationContract]
        RespuestaOperacion EliminarLineaDisponible(string numeroTelefono);

        [OperationContract]
        LineaEnUsoDTO[] ListarLineasEnUso();

        [OperationContract]
        LineaClienteDTO[] ListarLineasPorCliente(string identificacionCliente);

        [OperationContract]
        FacturacionDTO UltimaFacturacion();

        [OperationContract]
        RespuestaOperacion CargarSaldo(string numeroTelefono, decimal monto);

        [OperationContract]
        RespuestaOperacion PagarFactura(string numeroTelefono);
    }
}
