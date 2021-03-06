using MongoDB.Driver;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Data
{
    public interface IDbContext
    {
        IMongoCollection<User> GetUserCollection();
    }
}