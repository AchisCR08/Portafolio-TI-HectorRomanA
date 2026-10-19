using System.Configuration;
using System.ServiceModel;
using WS_ConsultasProveedor.Modelos;

namespace WS_ConsultasProveedor.Infraestructura
{
    // Contrato cliente para consumir WS_Autenticacion.ObtenerUsuario. (interfaces)
    
    [ServiceContract]
    public interface IAutenticacionService
    {
        [OperationContract]
        RespuestaObtenerUsuario ObtenerUsuario(string identificacion);
    }

    public class AutenticacionClient
    {
        public RespuestaObtenerUsuario ObtenerUsuario(string identificacion)
        {
            string url = ConfigurationManager.AppSettings["WS_Autenticacion.Url"]
                         ?? "http://localhost:55960/AutenticacionService.svc";

            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(url);

            using (var factory = new ChannelFactory<IAutenticacionService>(binding, endpoint))
            {
                IAutenticacionService canal = factory.CreateChannel();
                return canal.ObtenerUsuario(identificacion);
            }
        }
    }
}
