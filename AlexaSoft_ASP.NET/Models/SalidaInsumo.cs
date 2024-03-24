using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class SalidaInsumo
{
    public int IdInsumo { get; set; }

    public int IdProducto { get; set; }

    public DateTime FechaRetiro { get; set; }

    public int Cantidad { get; set; }

    public string? MotivoAnular { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
