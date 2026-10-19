using System.Configuration;
using System.ServiceModel;
using WebCentralAdmin.Utilidades;

namespace WebCentralAdmin.Infraestructura
{
    // Contrato cliente para consumir WS_ConsultasProveedor.
    // El nombre del contrato y las operaciones coinciden con el servidor
    // para que las acciones SOAP default sean identicas.
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

    public class ConsultasServiceClient
    {
        public LineaDisponibleDTO[] ListarLineasDisponibles()
        {
            using (ChannelFactory<IConsultasProveedorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().ListarLineasDisponibles();
            }
        }

        public RespuestaOperacion EliminarLineaDisponible(string numeroTelefono)
        {
            using (ChannelFactory<IConsultasProveedorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().EliminarLineaDisponible(numeroTelefono);
            }
        }

        public LineaEnUsoDTO[] ListarLineasEnUso()
        {
            using (ChannelFactory<IConsultasProveedorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().ListarLineasEnUso();
            }
        }

        public LineaClienteDTO[] ListarLineasPorCliente(string identificacionCliente)
        {
            using (ChannelFactory<IConsultasProveedorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().ListarLineasPorCliente(identificacionCliente);
            }
        }

        public FacturacionDTO UltimaFacturacion()
        {
            using (ChannelFactory<IConsultasProveedorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().UltimaFacturacion();
            }
        }

        public RespuestaOperacion CargarSaldo(string numeroTelefono, decimal monto)
        {
            using (ChannelFactory<IConsultasProveedorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().CargarSaldo(numeroTelefono, monto);
            }
        }

        public RespuestaOperacion PagarFactura(string numeroTelefono)
        {
            using (ChannelFactory<IConsultasProveedorService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().PagarFactura(numeroTelefono);
            }
        }

        private static ChannelFactory<IConsultasProveedorService> CrearFabrica()
        {
            return new ChannelFactory<IConsultasProveedorService>(
                new BasicHttpBinding(),
                new EndpointAddress(ConfiguracionWS.UrlConsultas));
        }
    }
}
