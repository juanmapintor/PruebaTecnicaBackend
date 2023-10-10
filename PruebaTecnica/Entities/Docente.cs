using System;
using System.Collections.Generic;

namespace PruebaTecnica.Entities;

public partial class Docente
{
    public int Iddocente { get; set; }

    public string ApelDoc { get; set; } = null!;

    public string NombDoc { get; set; } = null!;

    public string DireDoc { get; set; } = null!;

    public string NtelDoc { get; set; } = null!;

    public string NcelDoc { get; set; } = null!;

    public string GradDoc { get; set; } = null!;

    public int Idprofesion { get; set; }

    public virtual Profesion IdprofesionNavigation { get; set; } = null!;

    public virtual ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();
}
