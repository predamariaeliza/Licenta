using System.Threading.Tasks;
using Licenta.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Dtos.User;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]// atribue raspunsuri HTTP API
    [Route("[controller]")]     /* controller-ul poate fi accesat dupa numele sau
    //                             in acest caz va fi 'User' */
    
    // adaugam mereu ControllerBase
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }


        [HttpPost("Register")]
        // adaugam metoda de inregistrare
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepo.Register(
                new User { Username= request.Username },
                request.Password
            );

            // verificam daca response-ul este fals
            // daca da, returnam 'Bad Request'
            if(response.Succes)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}