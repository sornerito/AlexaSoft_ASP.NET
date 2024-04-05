using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models
{
    public partial class Detallespedidosservicio
    {
        public int IdDetallePedidoServicio { get; set; }

        public int IdPaquete { get; set; }

        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public int Precio { get; set; }

        public virtual Paquete IdPaqueteNavigation { get; set; } = null!;

        public virtual Pedido IdPedidoNavigation { get; set; } = null!;
    }
}
