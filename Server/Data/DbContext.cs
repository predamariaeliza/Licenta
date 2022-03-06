using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Licenta.Models;
using Server.Data;

namespace Server.Data
{
    // DBCONTEXT = DATACONTEXT
    public class DbContext : IDbContext
    {
        private readonly IMongoCollection<User> _users; 
        public DbContext(IOptions<DbConfig> DbConfig)
        {
            var client = new MongoClient(DbConfig.Value.ConnectionString);
            var database = client.GetDatabase(DbConfig.Value.DatabaseName);
            _users = database.GetCollection<User>("User");

        }
        public IMongoCollection<User> GetUserCollection() => _users;


    }
}