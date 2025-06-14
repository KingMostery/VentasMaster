﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MasterVentas.ModelViews.User
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("rol")]
        public string Rol { get; set; }  // "Administrador" o "Cajero"


        [BsonElement("activo")]
        public bool Activo { get; set; }


    }
}
