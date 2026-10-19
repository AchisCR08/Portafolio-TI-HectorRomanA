using System.ServiceModel;
using WS_Autenticacion.Modelos;

namespace WS_Autenticacion
{
    [ServiceContract]
    public interface IAutenticacionService
    {
        // la historia WS_AUTENTICACION1 autentica un usuario (empleado/proveedor o cliente).
        // usuario y contrasena viajan cifrados
        [OperationContract]
        RespuestaServicio Autenticar(
            string usuario,
            string contrasena,
            string tipoUsuario);

        // WS_AUTENTICACION2 - crear usuario nuevo.
        [OperationContract]
        RespuestaServicio CrearUsuario(
            string identificacion,
            string nombre,
            string primerApellido,
            string segundoApellido,
            string correoElectronico,
            string usuario,
            string contrasena,
            string estado,
            string tipo);

        // WS_AUTENTICACION2 -  modificar datos de un usuario existente.
        
        [OperationContract]
        RespuestaServicio ModificarUsuario(
            string identificacion,
            string nombre,
            string primerApellido,
            string segundoApellido,
            string correoElectronico,
            string usuario,
            string contrasena);

        // WS_AUTENTICACION2 -  activar/inactivar un usuario.
        [OperationContract]
        RespuestaServicio CambiarEstadoUsuario(
            string identificacion,
            string estado);

        // Consulta de datos basicos de un usuario por identificacion.
        // esto se usaen  WS_ConsultasProveedor para mostrar el nombre del cliente
      
        [OperationContract]
        RespuestaObtenerUsuario ObtenerUsuario(
            string identificacion);

        // WS_AUTENTICACION2 - listar usuarios por tipo (ADM7: administradores tipo "1").
        [OperationContract]
        UsuarioDTO[] ListarUsuarios(
            string tipo);

        // WS_AUTENTICACION2 - eliminar un usuario por identificacion (ADM7).
        [OperationContract]
        RespuestaServicio EliminarUsuario(
            string identificacion);
    }
}
