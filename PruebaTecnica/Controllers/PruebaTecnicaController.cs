using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTOs;
using PruebaTecnica.Services;
using PruebaTecnica.SimpleAuthorization;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SimpleAuthorize]
    public class PruebaTecnicaController : ControllerBase
    {
        //Los servicios provistos por Program son luego consumidos mediante inyeccion de dependencias en Controllers
        private readonly IPruebaTecnicaService _pruebaTecnicaService;
        public PruebaTecnicaController(IPruebaTecnicaService pruebaTecnicaService)
        {
            _pruebaTecnicaService = pruebaTecnicaService;
        }

        [HttpPost]
        [Route("InsertarEstudiante")]
        public ActionResult<EstudianteResponse> InsertarEstudiante(EstudianteInsertRequest request)
        {
            return _pruebaTecnicaService.InsertEstudiante(request);
        }

        [HttpGet]
        [Route("EstudiantesPorProvincia")]
        public ActionResult<List<EstudianteResponse>> EstudiantesPorProvincia(int idProvincia)
        {
            return _pruebaTecnicaService.ListEstudiantesPorProvincia(idProvincia);
        }

        [HttpGet]
        [Route("CantidadEstudiantesPorDocente")]
        public ActionResult<NumeroEstudiantesPorDocenteResponse> CantidadEstudiantesPorDocente(int idDocente)
        {
            return _pruebaTecnicaService.NumeroEstudiantesPorDocente(idDocente); 
        }
    }
}
