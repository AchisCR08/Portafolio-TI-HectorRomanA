using MongoDB.Bson.Serialization.Attributes;

namespace WS_Autenticacion.Modelos
{
    // Coleccion "usuarios" en MongoDB (base "TelefoniaAuth").
    // identificacion se usa como llave (_id) del documento.
    // usuario y contrasena se guardan cifrados (AES), igual que llegan del WS.
    public class UsuarioDocumento
    {
        [BsonId]
        public string Identificacion { get; set; }

        public string Nombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string CorreoElectronico { get; set; }

        // Cifrado con AES (CifradorAES), igual que en el resto del sistema.
        public string Usuario { get; set; }

        // Cifrado con AES (CifradorAES), igual que en el resto del sistema.
        public string Contrasena { get; set; }

        // "activo" o "inactivo"
        public string Estado { get; set; }

        // "1" para empleados (acceso a espacio proveedor), "2" para clientes
        public string Tipo { get; set; }
    }
}
