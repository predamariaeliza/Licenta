using System.Threading.Tasks;
using Server.Models;

namespace Server.Data
{
    /*  Aceasta interfata implementeaza 3 metode:
        - metoda register (cu user si password ca parametrii)
        returneaza un integer -> ideea de utilizator
    */
    public interface IAuthRepository
    {
        // returneaza un Task cu un ServiceResponse
        Task<ServiceResponse<string>> Register (User user, string pasword);
        Task<ServiceResponse<string>> Login(string username, string password);

    }
}