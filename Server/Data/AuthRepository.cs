using System.Threading.Tasks;
using Licenta.Models;
using Server.Models;

namespace Server.Data
{
    public class AuthRepository : IAuthRepository
    {
        public Task<ServiceResponse<int>> Register(User user, string pasword)
        {
            throw new System.NotImplementedException();
        }
    }
}