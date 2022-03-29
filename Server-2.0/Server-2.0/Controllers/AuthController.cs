using Microsoft.AspNetCore.Mvc;
using Server_2._0.Dtos.User;
using Server_2._0.Models;
using Server_2._0.Repository;

namespace Server_2._0.Controllers
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
                new UserModel { Username = request.Username,
                Email = request.Email},
                request.Password
            );

            // verificam daca response-ul este fals
            // daca da, returnam 'Bad Request'
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        // adaugam metoda de inregistrare
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authRepo.Login(
                request.Username,
                request.Password
            );

            // verificam daca response-ul este fals
            // daca da, returnam 'Bad Request'
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
