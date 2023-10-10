using System;
using System.Collections.Generic;

namespace PruebaTecnica.Entities;

public partial class Asignacion
{
    public int Idasignacion { get; set; }

    public DateTime FechaAsi { get; set; }

    public int Idcurso { get; set; }

    public int Iddocente { get; set; }

    public virtual Curso IdcursoNavigation { get; set; } = null!;

    public virtual Docente IddocenteNavigation { get; set; } = null!;
}
