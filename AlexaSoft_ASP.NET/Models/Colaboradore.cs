using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models;

public partial class Colaboradore
{
    public int IdColaborador { get; set; }

    [Required(ErrorMessage = "Por favor, escriba su Nombre.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "Por favor, escriba su Cedula.")]

    public string Cedula { get; set; } = null!;
    [Required(ErrorMessage = "Por favor, escriba su Correo.")]
    public string Correo { get; set; } = null!;
    [Required(ErrorMessage = "Por favor, escriba su Telefono.")]
    public string Telefono { get; set; } = null!;
    [Required(ErrorMessage = "Por favor, escriba una contraseña.")]
    public string Contrasena { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public int IdRol { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
