
using MongoDB.Driver;
using Licenta.Models;

namespace scd_proiect.Data
{
    public interface IDbContext
    {
        IMongoCollection<User> GetUserCollection();
    }
}