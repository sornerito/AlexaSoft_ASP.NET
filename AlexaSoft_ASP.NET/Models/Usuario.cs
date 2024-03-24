using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Instagram { get; set; }

    public string Contrasena { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime FechaInteraccion { get; set; }

    public int IdRol { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
