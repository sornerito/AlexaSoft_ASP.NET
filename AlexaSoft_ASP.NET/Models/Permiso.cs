using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<RolesPermiso> RolesPermisos { get; set; } = new List<RolesPermiso>();
}
