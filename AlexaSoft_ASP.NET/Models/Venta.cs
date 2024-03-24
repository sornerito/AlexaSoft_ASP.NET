using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public string NumeroFactura { get; set; } = null!;

    public int IdPedido { get; set; }

    public int IdColaborador { get; set; }

    public DateTime Fecha { get; set; }

    public string MotivoAnular { get; set; } = null!;

    public int IdUsuario { get; set; }

    public int Total { get; set; }

    public int Iva { get; set; }

    public virtual Colaboradore IdColaboradorNavigation { get; set; } = null!;

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Ventasdetalleproducto> Ventasdetalleproductos { get; set; } = new List<Ventasdetalleproducto>();

    public virtual ICollection<Ventasdetallesservicio> Ventasdetallesservicios { get; set; } = new List<Ventasdetallesservicio>();
}
