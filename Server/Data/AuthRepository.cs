using System.Threading.Tasks;
using Licenta.Models;
using MongoDB.Driver;
using scd_proiect.Data;
using Server.Models;

namespace Server.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IMongoCollection<User> _users; 
        public AuthRepository(IDbContext Data)
        {
            _users = Data.GetUserCollection();
        }
        public Task<ServiceResponse<string>> Login(string username, string password)
        {
            _users.InsertOneAsync(new User{
                Username = username, 
                
            });
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<int>> Register(User user, string pasword)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}