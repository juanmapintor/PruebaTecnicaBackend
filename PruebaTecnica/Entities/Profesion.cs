namespace PruebaTecnica.Entities;

public partial class Profesion
{
    public int Idprofesion { get; set; }

    public string NombPro { get; set; } = null!;

    public virtual ICollection<Docente> Docentes { get; set; } = new List<Docente>();
}
