using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTOs;
using PruebaTecnica.Services;

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
        public ActionResult<UserResponse> Register(UserRegisterRequest user)
        {
            return _userService.Register(user);
        }
    }
}
