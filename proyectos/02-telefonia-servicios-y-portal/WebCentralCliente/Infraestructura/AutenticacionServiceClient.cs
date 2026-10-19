using System.Runtime.Serialization;
using System.ServiceModel;
using WebCentralCliente.Utilidades;

namespace WebCentralCliente.Infraestructura
{
    // Copias cliente de WS_Autenticacion.Modelos.
    // El Namespace debe coincidir EXACTAMENTE con el servidor para que
    // DataContractSerializer deserialice el SOAP correctamente.
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_Autenticacion.Modelos")]
    public class RespuestaServicioAutenticacion
    {
        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        // Solo viene lleno cuando Autenticar() fue exitoso.
        [DataMember]
        public string Identificacion { get; set; }
    }

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_Autenticacion.Modelos")]
    public class RespuestaObtenerUsuario
    {
        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public UsuarioInfo Usuario { get; set; }
    }

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WS_Autenticacion.Modelos")]
    public class UsuarioInfo
    {
        [DataMember]
        public string Identificacion { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string PrimerApellido { get; set; }

        [DataMember]
        public string SegundoApellido { get; set; }

        [DataMember]
        public string CorreoElectronico { get; set; }
    }

    // Contrato cliente para consumir WS_Autenticacion (Mongo).
    [ServiceContract]
    public interface IAutenticacionService
    {
        [OperationContract]
        RespuestaServicioAutenticacion Autenticar(
            string usuario,
            string contrasena,
            string tipoUsuario);

        [OperationContract]
        RespuestaServicioAutenticacion CrearUsuario(
            string identificacion,
            string nombre,
            string primerApellido,
            string segundoApellido,
            string correoElectronico,
            string usuario,
            string contrasena,
            string estado,
            string tipo);

        [OperationContract]
        RespuestaServicioAutenticacion ModificarUsuario(
            string identificacion,
            string nombre,
            string primerApellido,
            string segundoApellido,
            string correoElectronico,
            string usuario,
            string contrasena);

        [OperationContract]
        RespuestaServicioAutenticacion CambiarEstadoUsuario(
            string identificacion,
            string estado);

        [OperationContract]
        RespuestaObtenerUsuario ObtenerUsuario(
            string identificacion);
    }

    public class AutenticacionServiceClient
    {
        public RespuestaServicioAutenticacion Autenticar(string usuario, string contrasena, string tipoUsuario)
        {
            using (ChannelFactory<IAutenticacionService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().Autenticar(usuario, contrasena, tipoUsuario);
            }
        }

        public RespuestaServicioAutenticacion CrearUsuario(
            string identificacion,
            string nombre,
            string primerApellido,
            string segundoApellido,
            string correoElectronico,
            string usuario,
            string contrasena,
            string estado,
            string tipo)
        {
            using (ChannelFactory<IAutenticacionService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().CrearUsuario(
                    identificacion, nombre, primerApellido, segundoApellido,
                    correoElectronico, usuario, contrasena, estado, tipo);
            }
        }

        public RespuestaServicioAutenticacion ModificarUsuario(
            string identificacion,
            string nombre,
            string primerApellido,
            string segundoApellido,
            string correoElectronico,
            string usuario,
            string contrasena)
        {
            using (ChannelFactory<IAutenticacionService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().ModificarUsuario(
                    identificacion, nombre, primerApellido, segundoApellido,
                    correoElectronico, usuario, contrasena);
            }
        }

        public RespuestaServicioAutenticacion CambiarEstadoUsuario(string identificacion, string estado)
        {
            using (ChannelFactory<IAutenticacionService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().CambiarEstadoUsuario(identificacion, estado);
            }
        }

        public RespuestaObtenerUsuario ObtenerUsuario(string identificacion)
        {
            using (ChannelFactory<IAutenticacionService> fabrica = CrearFabrica())
            {
                return fabrica.CreateChannel().ObtenerUsuario(identificacion);
            }
        }

        private static ChannelFactory<IAutenticacionService> CrearFabrica()
        {
            return new ChannelFactory<IAutenticacionService>(
                new BasicHttpBinding(),
                new EndpointAddress(ConfiguracionWS.UrlAutenticacion));
        }
    }
}
