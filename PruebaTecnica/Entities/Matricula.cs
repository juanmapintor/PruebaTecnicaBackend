using System;
using System.Collections.Generic;

namespace PruebaTecnica.Entities;

public partial class Matricula
{
    public int Idmatricula { get; set; }

    public DateTime FechaMat { get; set; }

    public int Idestudiante { get; set; }

    public int Idcurso { get; set; }

    public virtual Curso IdcursoNavigation { get; set; } = null!;

    public virtual Estudiante IdestudianteNavigation { get; set; } = null!;
}
