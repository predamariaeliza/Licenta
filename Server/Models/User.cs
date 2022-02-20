using MongoDB.Bson.Serialization.Attributes;

namespace Licenta.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id {get;set;}
        public string Username {get;set;}
        public string Name {get;set;}
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
                
        public string Role { get; set; }= "User";
        public string Email {get;set;}
        public byte[] PasswordSalt { get; internal set; }
    }
}