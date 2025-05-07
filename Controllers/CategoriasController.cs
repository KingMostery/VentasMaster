using System.Collections.Generic;
using System.Linq;
using MasterVentas.Connection;
using MasterVentas.Model.ModelViews.Categorias;
using MasterVentas.ModelViews.User;
using MongoDB.Driver;

namespace MasterVentas.Controllers
{
    public class CategoriasController
    {
        private readonly IMongoCollection<Categorias> _categorias;


        public CategoriasController()
        {
            // Establecer la conexión con MongoDB y acceder a la colección Categorias
            var config = new MongoDbConfig();
            var client = new MongoClient(config.ConnectionUri);
            var db = client.GetDatabase(config.DatabaseName);
            _categorias = db.GetCollection<Categorias>("Categorias");
        }

        public bool InsertCategoria(Categorias nuevo)
        {
            // Verificar si ya existe una categoria con el mismo nombre
            var existente = _categorias.Find(u => u.Nombre == nuevo.Nombre).FirstOrDefault();
            if (existente != null)
            {
                return false; // Ya existe una categoria con ese nombre
            }
            _categorias.InsertOne(nuevo);
            return true;
        }

        public bool UpdateCategoria(Categorias categoria)
        {
            var filter = Builders<Categorias>.Filter.Eq(u => u.Id, categoria.Id);
            var update = Builders<Categorias>.Update
                .Set(u => u.Nombre, categoria.Nombre)
                .Set(u => u.Descripcion, categoria.Descripcion)
                .Set(u => u.Activo, categoria.Activo);
            var result = _categorias.UpdateOne(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}
