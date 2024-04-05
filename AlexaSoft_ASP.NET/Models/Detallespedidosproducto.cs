using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models
{
    public partial class Detallespedidosproducto
    {
        public int IdDetallePedidoProducto { get; set; }

        public int IdPedido { get; set; }

        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad de unidades por producto es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de unidades por producto debe ser mayor que cero")]
        public int UnidadesXproducto { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio")]
        public int PrecioUnitario { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; } = null!;

        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
