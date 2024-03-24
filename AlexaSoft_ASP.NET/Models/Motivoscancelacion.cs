using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Motivoscancelacion
{
    public int IdMotivo { get; set; }

    public string Motivo { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
