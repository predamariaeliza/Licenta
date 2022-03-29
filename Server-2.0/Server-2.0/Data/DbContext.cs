using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Server_2._0.Models;

namespace Server_2._0.Data
{
    // DBCONTEXT = DATACONTEXT
    public class DbContext : IDbContext
    {
        private readonly IMongoCollection<UserModel> _users;
        public DbContext(IOptions<DbConfig> DbConfig)
        {
            var client = new MongoClient(DbConfig.Value.ConnectionString);
            var database = client.GetDatabase(DbConfig.Value.DatabaseName);
            _users = database.GetCollection<UserModel>("User");

        }
        public IMongoCollection<UserModel> GetUserCollection() => _users;


    }
}
