using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;
using Server.Models;

namespace Server.Services
{
    public interface IUserService
    {
        // returneaza o lista a userilor
        Task<ServiceResponse<List<User>>> GetAllUsers();
        // returneaza un user dupa id
        Task<ServiceResponse<User>> GetUserById(string id);
        // creeaza un nou user
        Task<ServiceResponse<List<User>>> AddUser(User newUser);
    }
}