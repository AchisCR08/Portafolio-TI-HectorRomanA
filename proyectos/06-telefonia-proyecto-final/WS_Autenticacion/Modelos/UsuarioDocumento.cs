using MongoDB.Bson.Serialization.Attributes;

namespace WS_Autenticacion.Modelos
{
    // coleccion "usuarios" en MongoDB 
    // identificacion se usa como llave (_id) del documento.
    // usuario y contrasena se guardan cifrados  igual que llegan del WS.
    public class UsuarioDocumento
    {
        [BsonId]
        public string Identificacion { get; set; }

        public string Nombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string CorreoElectronico { get; set; }

        // todo cifrado  
        public string Usuario { get; set; }

       
        public string Contrasena { get; set; }

        // los estados son activo y inactivo
        public string Estado { get; set; }

        // "1" para empleados y 2 clientes
        public string Tipo { get; set; }
    }
}
