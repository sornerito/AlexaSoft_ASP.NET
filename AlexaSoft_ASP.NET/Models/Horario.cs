using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models;

public partial class Horario
{
    public int IdHorario { get; set; }
    [Required(ErrorMessage = "Por favor, escriba el dia de la semana(En Numero).")]
    public int NumeroDia { get; set; }
    [Required(ErrorMessage = "Por favor, Seleccione una hora para iniciar la jornada.")]
    public TimeSpan InicioJornada { get; set; }
    [Required(ErrorMessage = "Por favor, Seleccione una hora para finalizar la jornada.")]
    public TimeSpan FinJornada { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
