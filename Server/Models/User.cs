using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id {get;set;}
        public string Username {get;set;}
        
        // pastram doar valoarea Hash, nu si parola in sine
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }= "User";
        public string Email {get;set;}
    }
}