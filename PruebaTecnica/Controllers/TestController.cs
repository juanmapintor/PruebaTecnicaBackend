using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Repository;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly PruebaTecnicaContext _context;
        public TestController(PruebaTecnicaContext context) { 
           _context = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_context.Users.ToArray());
        }
    }
}
