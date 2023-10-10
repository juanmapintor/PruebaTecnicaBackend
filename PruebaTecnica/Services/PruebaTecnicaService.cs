using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.DTOs;
using PruebaTecnica.Entities;
using PruebaTecnica.Repository;

namespace PruebaTecnica.Services
{
    public interface IPruebaTecnicaService
    {
        ActionResult<Estudiante> InsertEstudiante(EstudianteInsertRequest request);
        ActionResult<List<Estudiante>> ListEstudiantesPorProvincia(int idProvincia);
        ActionResult<NumeroEstudiantesPorDocenteResponse> NumeroEstudiantesPorDocente(int idDocente);
    }
    public class PruebaTecnicaService : IPruebaTecnicaService
    {
        private readonly PruebaTecnicaContext _context;
        public PruebaTecnicaService(PruebaTecnicaContext context) 
        { 
            _context = context;
        }
        public ActionResult<Estudiante> InsertEstudiante(EstudianteInsertRequest request)
        {
            //Chequeamos que la request esté seteada y el distrito exista
            if (request == null || _context.Distritos.Find(request.IDDistrito) == null) return new UnprocessableEntityResult();

            var nuevoEstudiante = request.GetEstudiante();
            _context.Estudiantes.Add(nuevoEstudiante);
            _context.SaveChanges();

            return new ObjectResult(nuevoEstudiante);
        }

        public ActionResult<List<Estudiante>> ListEstudiantesPorProvincia(int idProvincia)
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
                .ToList();

            return new JsonResult(estudiantesPorProvincia == null ? new List<Estudiante>() : estudiantesPorProvincia);
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
