using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Ventasdetallesservicio
{
    public int IdVentaDetalleServicio { get; set; }

    public int IdVenta { get; set; }

    public int IdPaquete { get; set; }

    public int Cantidad { get; set; }

    public int Subtotal { get; set; }

    public virtual Paquete IdPaqueteNavigation { get; set; } = null!;

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
