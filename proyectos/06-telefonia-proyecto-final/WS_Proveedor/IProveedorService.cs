using System.ServiceModel;
using WS_Proveedor.Modelos;

namespace WS_Proveedor
{
    [ServiceContract]
    public interface IProveedorService
    {
        [OperationContract]
        RespuestaServicio RegistrarServicioDisponible(
            string numeroTelefono,
            string identificadorTelefono,
            string identificadorTarjeta,
            string tipoServicio,
            string estado);

        [OperationContract]
        RespuestaServicio ActivarDesactivarLinea(
            string numeroTelefono,
            string identificadorTelefono,
            string identificadorTarjeta,
            string tipoServicio,
            string identificacionDuenno,
            string estado);

        [OperationContract]
        RespuestaServicio CalcularFacturacionPostpago(
            string fechaCalculo,
            string fechaMaximaPago);
    }
}
