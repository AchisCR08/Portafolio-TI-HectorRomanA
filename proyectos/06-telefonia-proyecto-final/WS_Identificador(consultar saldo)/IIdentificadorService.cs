using System.ServiceModel;
using WS_Identificador.Modelos;

namespace WS_Identificador
{
    [ServiceContract]
    public interface IIdentificadorService
    {
        [OperationContract]
        RespuestaSaldo ConsultarSaldo(
            string numeroTelefono,
            string origenWeb,
            string tipoTransaccion);
    }
}
