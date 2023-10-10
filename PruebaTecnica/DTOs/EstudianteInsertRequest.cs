using PruebaTecnica.Entities;

namespace PruebaTecnica.DTOs
{
    public class EstudianteInsertRequest
    {
        public string Apellido { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public DateTime FechaNacimiento { get; set; }

        public string Sexo { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Tcol { get; set; } = null!;

        public string Gins { get; set; } = null!;

        public int IDDistrito { get; set; }

        public Estudiante GetEstudiante()
        {
            return new Estudiante()
            {
                ApelEst = Apellido,
                NombEst = Nombre,
                FnacEst = FechaNacimiento,
                SexoEst = Sexo,
                DireEst = Direccion,
                TcolEst = Tcol,
                GinsEst = Gins,
                Iddistrito = IDDistrito
            };
        }
    }
}
