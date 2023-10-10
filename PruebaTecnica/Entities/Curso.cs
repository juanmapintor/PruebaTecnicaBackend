using System;
using System.Collections.Generic;

namespace PruebaTecnica.Entities;

public partial class Curso
{
    public int Idcurso { get; set; }

    public string NombCur { get; set; } = null!;

    public double CostCur { get; set; }

    public int DuraCur { get; set; }

    public virtual ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
