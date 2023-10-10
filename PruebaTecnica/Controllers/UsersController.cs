using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTOs;
using PruebaTecnica.Entities;
using PruebaTecnica.Helpers;
using PruebaTecnica.Repository;
using PruebaTecnica.Services;
using PruebaTecnica.SimpleAuthorization;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) { 
           _userService = userService;
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(UserRegisterRequest user)
        {
            if (_userService.Exists(user.Username)) return UnprocessableEntity();

            var newUser = _userService.Register(user.Username, user.Password);
            if(newUser == null) return UnprocessableEntity();

            return Ok(newUser);
        }

        [HttpGet]
        [Route("Test")]
        [SimpleAuthorize]
        public ActionResult Test()
        {
            return Ok("Has iniciado sesión correctamente");
        }
    }
}
