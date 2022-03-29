using MongoDB.Driver;
using Server_2._0.Models;

namespace Server_2._0.Data
{
    public interface IDbContext
    {
        IMongoCollection<UserModel> GetUserCollection();
    }
}
