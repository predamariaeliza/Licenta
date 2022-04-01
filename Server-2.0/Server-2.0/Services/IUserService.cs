using Server_2._0.Models;

namespace Server_2._0.Services
{
    public interface IUserService
    {
        // returneaza o lista a UserModelilor
        Task<ServiceResponse<List<UserModel>>> GetAllUsers();
        // returneaza un UserModel dupa id
        Task<ServiceResponse<UserModel>> GetUserById(String Id);
        // returneaza un UserModel dupa email
        Task<ServiceResponse<UserModel>> GetUserByEmail(String Email);

        // creeaza un nou UserModel
        Task<ServiceResponse<List<UserModel>>> AddUser(UserModel NewUserModel);
    }
}
