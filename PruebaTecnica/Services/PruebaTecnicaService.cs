using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTOs;
using PruebaTecnica.Repository;

namespace PruebaTecnica.Services
{
    public interface IPruebaTecnicaService
    {
        ActionResult<EstudianteResponse> InsertEstudiante(EstudianteInsertRequest request);
        ActionResult<List<EstudianteResponse>> ListEstudiantesPorProvincia(int idProvincia);
        ActionResult<NumeroEstudiantesPorDocenteResponse> NumeroEstudiantesPorDocente(int idDocente);
    }
    public class PruebaTecnicaService : IPruebaTecnicaService
    {
        private readonly PruebaTecnicaContext _context;
        public PruebaTecnicaService(PruebaTecnicaContext context) 
        { 
            _context = context;
        }
        public ActionResult<EstudianteResponse> InsertEstudiante(EstudianteInsertRequest request)
        {
            //Chequeamos que la request esté seteada y el distrito exista
            if (request == null || _context.Distritos.Find(request.IDDistrito) == null) return new UnprocessableEntityResult();

            var nuevoEstudiante = request.GetEstudiante();
            _context.Estudiantes.Add(nuevoEstudiante);
            _context.SaveChanges();

            return new ObjectResult(new EstudianteResponse(nuevoEstudiante));
        }

        public ActionResult<List<EstudianteResponse>> ListEstudiantesPorProvincia(int idProvincia)
        {
            //Chequeamos que la provincia exista
            var provinciaSeleccionada = _context.Provincias.Find(idProvincia);
            if (provinciaSeleccionada == null) return new UnprocessableEntityResult();

            var estudiantesPorProvincia = provinciaSeleccionada
                .Distritos
                .SelectMany(
                    distrito => distrito.Estudiantes
                 )
                .Distinct()
                .Select(
                    estudiante => new EstudianteResponse(estudiante)
                 )
                .ToList();

            return new JsonResult(estudiantesPorProvincia == null ? new List<EstudianteResponse>() : estudiantesPorProvincia);
        } 

        public ActionResult<NumeroEstudiantesPorDocenteResponse> NumeroEstudiantesPorDocente(int idDocente)
        {
            var docenteSeleccionado = _context.Docentes.Find(idDocente);
            if(docenteSeleccionado == null) return new UnprocessableEntityResult();

            var cantEstudiantes = docenteSeleccionado
                .Asignaciones
                .Select(
                    asignacion => asignacion.Curso
                )
                .Distinct()
                .SelectMany(
                    curso => curso.Matriculas
                )
                .Distinct()
                .Select(
                    matricula => matricula.Estudiante
                )
                .Count();

            var numeroEstudiantesPorDocente = new NumeroEstudiantesPorDocenteResponse() 
            { 
                IDDocente = idDocente, 
                NombreDocente = docenteSeleccionado.NombDoc, 
                CantidadEstudiantes = cantEstudiantes 
            };

            return new JsonResult(numeroEstudiantesPorDocente);
        }
    }
}
