using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Ventasdetalleproducto
{
    public int IdVentaDetalleProducto { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int PrecioUnitario { get; set; }

    public int Cantidad { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
