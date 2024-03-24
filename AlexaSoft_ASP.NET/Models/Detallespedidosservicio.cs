using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Detallespedidosservicio
{
    public int IdDetallePedidoServicio { get; set; }

    public int IdPaquete { get; set; }

    public int IdPedido { get; set; }

    public int Precio { get; set; }

    public virtual Paquete IdPaqueteNavigation { get; set; } = null!;

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}
