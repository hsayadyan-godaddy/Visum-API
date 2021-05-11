using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace VisumAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [BsonElement("UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [BsonElement("Password")]
        public string Password { get; set; }

        
        public string PasswordHash { get; set; }
        public string Email { get; set; }


    }
}
