using System.Collections.Generic;
using MongoDB.Driver;
using WS_Autenticacion.Modelos;

namespace WS_Autenticacion.Infraestructura
{
    public class UsuarioRepositorio
    {
        private readonly IMongoCollection<UsuarioDocumento> coleccion;

        public UsuarioRepositorio()
        {
            coleccion = new MongoConexion().ObtenerColeccionUsuarios();
        }

        public UsuarioDocumento BuscarPorIdentificacion(string identificacion)
        {
            return coleccion.Find(u => u.Identificacion == identificacion).FirstOrDefault();
        }

        // El login llega por "usuario" (cifrado), no por identificacion.
        public UsuarioDocumento BuscarPorUsuarioCifrado(string usuarioCifrado)
        {
            return coleccion.Find(u => u.Usuario == usuarioCifrado).FirstOrDefault();
        }

        public bool Existe(string identificacion)
        {
            return BuscarPorIdentificacion(identificacion) != null;
        }

        public void Insertar(UsuarioDocumento usuario)
        {
            coleccion.InsertOne(usuario);
        }

        public void Reemplazar(UsuarioDocumento usuario)
        {
            coleccion.ReplaceOne(u => u.Identificacion == usuario.Identificacion, usuario);
        }

        public void ActualizarEstado(string identificacion, string estado)
        {
            UpdateDefinition<UsuarioDocumento> actualizacion =
                Builders<UsuarioDocumento>.Update.Set(u => u.Estado, estado);

            coleccion.UpdateOne(u => u.Identificacion == identificacion, actualizacion);
        }

        public List<UsuarioDocumento> ListarPorTipo(string tipo)
        {
            return coleccion.Find(u => u.Tipo == tipo).ToList();
        }

        public bool Eliminar(string identificacion)
        {
            DeleteResult resultado = coleccion.DeleteOne(u => u.Identificacion == identificacion);
            return resultado.DeletedCount > 0;
        }
    }
}
