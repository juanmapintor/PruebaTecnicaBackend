namespace PruebaTecnica.DTOs
{
    public class NumeroEstudiantesPorDocenteResponse
    {
        public int IDDocente { get; set; }
        public string NombreDocente { get; set; } = String.Empty;
        public string ApellidoDocente { get; set;} = String.Empty;
        public int CantidadEstudiantes { get; set; }
    }
}
