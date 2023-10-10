using System;
using System.Collections.Generic;

namespace PruebaTecnica.Entities;

public partial class Distrito
{
    public int Iddistrito { get; set; }

    public string NombDis { get; set; } = null!;

    public int Idprovincia { get; set; }

    public virtual Provincia Provincia { get; set; } = null!;

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
