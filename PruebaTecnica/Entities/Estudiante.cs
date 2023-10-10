namespace PruebaTecnica.Entities;

public partial class Estudiante
{
    public int Idestudiante { get; set; }

    public string ApelEst { get; set; } = null!;

    public string NombEst { get; set; } = null!;

    public DateTime FnacEst { get; set; }

    public string SexoEst { get; set; } = null!;

    public string DireEst { get; set; } = null!;

    public string TcolEst { get; set; } = null!;

    public string GinsEst { get; set; } = null!;

    public int Iddistrito { get; set; }
    public virtual Distrito Distrito { get; set; } = null!;
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
