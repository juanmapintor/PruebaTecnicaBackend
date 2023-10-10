using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            //Obtenemos el nuevo estudiante desde el DTO y lo guardamos en la base de datos
            var nuevoEstudiante = request.GetEstudiante();
            _context.Estudiantes.Add(nuevoEstudiante);
            _context.SaveChanges();

            return new ObjectResult(new EstudianteResponse(nuevoEstudiante));
        }

        public ActionResult<List<EstudianteResponse>> ListEstudiantesPorProvincia(int idProvincia)
        {
            //Chequeamos que la provincia exista e incluimos las relaciones que nos importan
            var provinciaSeleccionada = _context.Provincias
                .Where(provincia => provincia.Idprovincia == idProvincia)
                .Include(provincia => provincia.Distritos)
                .ThenInclude(distrito => distrito.Estudiantes)
                .FirstOrDefault();

            //Si la provincia no existe, informamos que no se pudo procesar la entidad
            if (provinciaSeleccionada == null) return new UnprocessableEntityResult();

            //Generamos la lista de estudiantes
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
            //Chequeamos que el docente exista e incluimos las relaciones que nos importan
            var docenteSeleccionado = _context.Docentes
                .Where(docente => docente.Iddocente == idDocente)
                .Include(docente => docente.Asignaciones)
                .ThenInclude(asignacion => asignacion.Curso)
                .ThenInclude(curso => curso.Matriculas)
                .ThenInclude(matricula => matricula.Estudiante)
                .FirstOrDefault();

            //Si el docente no existe, decimos que la entidad no se puede procesar
            if(docenteSeleccionado == null) return new UnprocessableEntityResult();

            //Contamos los estudiantes
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

            //Creamos el DTO de respuesta
            var numeroEstudiantesPorDocente = new NumeroEstudiantesPorDocenteResponse() 
            { 
                IDDocente = docenteSeleccionado.Iddocente, 
                NombreDocente = docenteSeleccionado.NombDoc,
                ApellidoDocente = docenteSeleccionado.ApelDoc,
                CantidadEstudiantes = cantEstudiantes 
            };

            return new JsonResult(numeroEstudiantesPorDocente);
        }
    }
}
