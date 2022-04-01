using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server_2._0.Models;
using Server_2._0.Services;

namespace Server_2._0.Controllers
{
    /* ATRIBUTE */
    [Authorize]
    [ApiController]     // atribue raspunsuri HTTP API
    [Route("[controller]")]     /* controller-ul poate fi accesat dupa numele sau
    //                             in acest caz va fi 'User' */

    // adaugam mereu ControllerBase
    public class UserController : ControllerBase
    {
        // creem un nou private field pentru userService din constructor (linia 22)
        private readonly IUserService _userService;

        // constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        // metoda ce preia o lista cu TOTI USERII
        public async Task<IActionResult> GetAll()
        {
            // await apeleaza metoda asyncrona
            var response = await _userService.GetAllUsers();

            //verificam daca operatiunea a avut succes
            if(response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("{email}")]
        //metoda ce preia utilizatorul dupa email
        public async Task<ActionResult<ServiceResponse<UserModel>>> GetByEmail(String Email)
        {
            return Ok(await _userService.GetUserByEmail(Email));
        }

        [HttpGet("{id}")]
        // metoda ce selecteaza UN SINGUR USER dupa ID
        public async Task<ActionResult<ServiceResponse<UserModel>>> GetSingle(String Id)
        {
            // await apeleaza metoda asyncrona
            return Ok(await _userService.GetUserById(Id));
        }

        [HttpPost]
        // metoda ce creeaza un USER NOU
        public async Task<ActionResult<ServiceResponse<List<UserModel>>>> AddUser(UserModel NewUser)
        {
            // await apeleaza metoda asyncrona
            return Ok(await _userService.AddUser(NewUser));
        }
    }
}
