using System.Configuration;
using MongoDB.Driver;
using WS_Autenticacion.Modelos;

namespace WS_Autenticacion.Infraestructura
{
    public class MongoConexion
    {
        public IMongoCollection<UsuarioDocumento> ObtenerColeccionUsuarios()
        {
            string cadenaConexion = ConfigurationManager.AppSettings["Mongo.ConnectionString"]
                                     ?? "mongodb://localhost:27017";
            string nombreBaseDatos = ConfigurationManager.AppSettings["Mongo.BaseDatos"]
                                      ?? "TelefoniaAuth";
            string nombreColeccion = ConfigurationManager.AppSettings["Mongo.Coleccion"]
                                      ?? "usuarios";

            MongoClient cliente = new MongoClient(cadenaConexion);
            IMongoDatabase baseDatos = cliente.GetDatabase(nombreBaseDatos);

            return baseDatos.GetCollection<UsuarioDocumento>(nombreColeccion);
        }
    }
}
