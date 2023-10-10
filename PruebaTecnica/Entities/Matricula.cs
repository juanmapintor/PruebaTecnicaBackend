namespace PruebaTecnica.Entities;

public partial class Matricula
{
    public int Idmatricula { get; set; }

    public DateTime FechaMat { get; set; }

    public int Idestudiante { get; set; }

    public int Idcurso { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
