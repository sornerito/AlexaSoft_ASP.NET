using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Detallespedidosproducto
{
    public int IdDetallePedidoProducto { get; set; }

    public int IdPedido { get; set; }

    public int IdProducto { get; set; }

    public int UnidadesXproducto { get; set; }

    public int PrecioUnitario { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
