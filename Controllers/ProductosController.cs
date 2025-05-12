using System.Collections.Generic;
using MasterVentas.Connection;
using MasterVentas.Model;
using MasterVentas.Util;
using MongoDB.Driver;
using ZXing;
using ZXing.Common;

namespace MasterVentas.Controllers
{
    public class ProductosController
    {
        private readonly IMongoCollection<Producto> _productos;

        public ProductosController()
        {
            // Establecer la conexión con MongoDB y acceder a la colección Productos
            var config = new MongoDbConfig();
            var client = new MongoClient(config.ConnectionUri);
            var db = client.GetDatabase(config.DatabaseName);
            _productos = db.GetCollection<Producto>("Productos");
        }

        /// <summary>
        /// Obtiene todos los productos de la base de datos.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        public List<Producto> ObtenerTodos()
        {
            // Devuelve todos los documentos de la colección
            return _productos.Find(p => true).ToList();
        }

        public bool InsertarProducto(Producto nuevo)
        {
            // Generar siempre el código de barras
            nuevo.CodigoBarras = CodeBarGenerador.GenerarCodigo();

            // Validar duplicados por nombre
            var existe = _productos.Find(p => p.Nombre == nuevo.Nombre).FirstOrDefault();
            if (existe != null)
                return false;

            _productos.InsertOne(nuevo);
            return true;
        }

   


    }
}
