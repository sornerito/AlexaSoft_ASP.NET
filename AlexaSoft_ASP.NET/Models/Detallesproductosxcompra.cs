using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Detallesproductosxcompra
{
    public int IdDetalleProductoXcompra { get; set; }

    public int IdProducto { get; set; }

    public int IdCompra { get; set; }

    public int Unidades { get; set; }

    public virtual Compra? IdCompraNavigation { get; set; } 

    public virtual Producto? IdProductoNavigation { get; set; }
}
