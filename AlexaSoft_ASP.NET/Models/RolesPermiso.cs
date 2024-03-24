using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class RolesPermiso
{
    public int IdPermisoXrol { get; set; }

    public int IdRol { get; set; }

    public int IdPermiso { get; set; }

    public virtual Permiso IdPermisoNavigation { get; set; } = null!;

    public virtual Role IdRolNavigation { get; set; } = null!;
}
