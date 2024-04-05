using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    public string Estado { get; set; } = null!;

    public int Total { get; set; }

    public decimal Iva { get; set; }

    public int IdCliente { get; set; }

    public int IdColaborador { get; set; }

    public virtual ICollection<Detallespedidosproducto> Detallespedidosproductos { get; set; } = new List<Detallespedidosproducto>();

    public virtual ICollection<Detallespedidosservicio> Detallespedidosservicios { get; set; } = new List<Detallespedidosservicio>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Colaboradore IdColaboradorNavigation { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
