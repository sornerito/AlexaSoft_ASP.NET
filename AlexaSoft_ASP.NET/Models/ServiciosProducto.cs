using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class ServiciosProducto
{
    public int IdProductoXservicio { get; set; }

    public int IdServicio { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public string UnidadMedida { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
