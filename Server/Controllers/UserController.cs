using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    /* ATRIBUTE */
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
        public async Task<ActionResult<ServiceResponse<List<User>>>> Get()
        {
            // await apeleaza metoda asyncrona
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        // metoda ce selecteaza UN SINGUR USER dupa ID
        public async Task<ActionResult<ServiceResponse<User>>> GetSingle(string id)
        {
            // await apeleaza metoda asyncrona
            return Ok(await _userService.GetUserById(id)); 
        }

        [HttpPost]
        // metoda ce creeaza un USER NOU
        public async Task<ActionResult<ServiceResponse<List<User>>>> AddUser(User newUser)
        {
            // await apeleaza metoda asyncrona
            return Ok(await _userService.AddUser(newUser)); 
        }
    }
}