using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.DAL.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
        
        [BsonElement("username")]
        public string UserName { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("gender")]
        public int Gender { get; set; }

        [BsonElement("height")]
        public double Height { get; set; }

        [BsonElement("weight")]
        public double Weight { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime updatedAt { get; set; }
    }
}
