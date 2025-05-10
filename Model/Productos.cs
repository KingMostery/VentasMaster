
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MasterVentas.Model
{
    public class Producto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("precio")]
        public decimal Precio { get; set; }


        [BsonElement("categoria")]
        public string Categoria { get; set; }
        //[BsonElement("categoriaId")]
        //[BsonRepresentation(BsonType.ObjectId)]


        public string CategoriaId { get; set; }

        [BsonElement("codigoBarras")]
        public string CodigoBarras { get; set; }

        [BsonElement("activo")]
        public bool Activo { get; set; } = true;
    }
}


