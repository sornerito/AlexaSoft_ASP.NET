using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Horario
{
    public int IdHorario { get; set; }

    public int NumeroDia { get; set; }

    public TimeSpan InicioJornada { get; set; }

    public TimeSpan FinJornada { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
