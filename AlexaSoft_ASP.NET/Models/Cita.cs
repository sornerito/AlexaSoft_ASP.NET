using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models;

public partial class Cita
{
    public int IdCita { get; set; }

    [Required(ErrorMessage = "Por favor, seleccione una fecha.")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Por favor, seleccione una Hora.")]
    public TimeSpan Hora { get; set; }

    [Required(ErrorMessage = "Por favor, describa brevemente lo que se hará.")]
    public string? Detalle { get; set; }

    [Required(ErrorMessage = "Por favor, seleccione un Estado.")]
    public string Estado { get; set; } = null!;

    public int? IdMotivoCancelacion { get; set; }

    public int IdCliente { get; set; }

    public int IdPaquete { get; set; }

    public int IdHorario { get; set; }

    public int IdColaborador { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Colaboradore IdColaboradorNavigation { get; set; } = null!;

    public virtual Horario IdHorarioNavigation { get; set; } = null!;

    public virtual Motivoscancelacion? IdMotivoCancelacionNavigation { get; set; }

    public virtual Paquete IdPaqueteNavigation { get; set; } = null!;
}
