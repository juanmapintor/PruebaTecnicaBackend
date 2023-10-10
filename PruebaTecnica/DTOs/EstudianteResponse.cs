using PruebaTecnica.Entities;

namespace PruebaTecnica.DTOs
{
    public class EstudianteResponse
    {
        public int IDEstudiante { get; set; }
        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public DateTime FechaNacimiento { get; set; }

        public string Sexo { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Tcol { get; set; } = null!;

        public string Gins { get; set; } = null!;

        public EstudianteResponse() { }
        public EstudianteResponse(Estudiante estudiante)
        {
            IDEstudiante = estudiante.Idestudiante;
            Apellido = estudiante.ApelEst;
            Nombre = estudiante.NombEst;
            FechaNacimiento = estudiante.FnacEst;
            Sexo = estudiante.SexoEst;
            Direccion = estudiante.DireEst;
            Tcol = estudiante.TcolEst;
            Gins = estudiante.GinsEst;
        }
    }
}
