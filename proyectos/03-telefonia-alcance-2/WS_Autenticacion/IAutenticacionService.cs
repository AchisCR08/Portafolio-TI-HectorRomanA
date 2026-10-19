using System.ServiceModel;
using WS_Autenticacion.Modelos;

namespace WS_Autenticacion
{
    [ServiceContract]
    public interface IAutenticacionService
    {
        // WS_AUTENTICACION1: autentica un usuario (empleado/proveedor o cliente).
        // usuario y contrasena viajan cifrados (AES, igual que el resto del sistema).
        [OperationContract]
        RespuestaServicio Autenticar(
            string usuario,
            string contrasena,
            string tipoUsuario);

        // WS_AUTENTICACION2 - punto 1: crear usuario nuevo.
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

        // WS_AUTENTICACION2 - punto 2: modificar datos de un usuario existente.
        // Los campos que no se envian (null o vacios) no se modifican.
        [OperationContract]
        RespuestaServicio ModificarUsuario(
            string identificacion,
            string nombre,
            string primerApellido,
            string segundoApellido,
            string correoElectronico,
            string usuario,
            string contrasena);

        // WS_AUTENTICACION2 - punto 3: activar/inactivar un usuario.
        [OperationContract]
        RespuestaServicio CambiarEstadoUsuario(
            string identificacion,
            string estado);
    }
}
