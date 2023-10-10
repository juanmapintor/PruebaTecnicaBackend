using System;
using System.Collections.Generic;

namespace PruebaTecnica.Entities;

public partial class Asignacion
{
    public int Idasignacion { get; set; }

    public DateTime FechaAsi { get; set; }

    public int Idcurso { get; set; }

    public int Iddocente { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual Docente Docente { get; set; } = null!;
}
