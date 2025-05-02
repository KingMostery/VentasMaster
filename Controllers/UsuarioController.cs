using System.Collections.Generic;
using System.Linq;
using MasterVentas.Connection;
using MasterVentas.ModelViews.User;
using MongoDB.Driver;

namespace MasterVentas.Controllers
{
    public class UsuarioController
    {
        private readonly IMongoCollection<Usuario> _usuarios;

       
        public UsuarioController()
        {
            // Establecer la conexión con MongoDB y acceder a la colección Usuarios
            var config = new MongoDbConfig();
            var client = new MongoClient(config.ConnectionUri);
            var db = client.GetDatabase(config.DatabaseName);
            _usuarios = db.GetCollection<Usuario>("Usuarios"); 
        }

        /// <summary>
        /// Método para validar el usuario con la contraseña en texto plano
        /// </summary>
        public Usuario ValidarUsuario(string username, string passwordIngresado)
        {
            var usuario = _usuarios.Find(u => u.Username == username && u.Password == passwordIngresado).FirstOrDefault();
            return usuario;
        }

        public bool InsertUsuario(Usuario nuevo)
        {
            // Verificar si ya existe un usuario con el mismo nombre
            var existente = _usuarios.Find(u => u.Username == nuevo.Username).FirstOrDefault();

            if (existente != null)
            {
                return false; // Ya existe un usuario con ese nombre
            }

            _usuarios.InsertOne(nuevo);
            return true;
        }

        /// <summary>
        /// Actualiza la informacion del usuario seleccionado
        /// </summary>
        public bool UpdateUsuario(Usuario usuario)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.Id, usuario.Id);
            var update = Builders<Usuario>.Update
                .Set(u => u.Nombre, usuario.Nombre)
                .Set(u => u.Rol, usuario.Rol)
                .Set(u => u.Password,usuario.Password);

            var result = _usuarios.UpdateOne(filter, update);
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Consulta todos los usuario en el sistema
        /// </summary>
        public List<Usuario> GetAllUsuarios()
        {
            return _usuarios.Find(_ => true).ToList();
        }

    }
}

