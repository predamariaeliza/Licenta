using Server_2._0.Models;

namespace Server_2._0.Repository
{
    public interface IAuthRepository
    {

        /*  Aceasta interfata implementeaza 3 metode:
            - metoda register (cu user si password ca parametrii)
            returneaza un integer -> ideea de utilizator
        */
       
            // returneaza un Task cu un ServiceResponse
      Task<ServiceResponse<string>> Register(UserModel User, string Password);
      Task<ServiceResponse<string>> Login(string Username, string Password);
      Task<bool> IsUserDuplicateAsync(string Username);
      Task<bool> IsEmailDuplicateAsync(string Email);


    }
}
