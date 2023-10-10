namespace PruebaTecnica.Entities;

public partial class Provincia
{
    public int Idprovincia { get; set; }

    public string NombPro { get; set; } = null!;

    public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();
}
