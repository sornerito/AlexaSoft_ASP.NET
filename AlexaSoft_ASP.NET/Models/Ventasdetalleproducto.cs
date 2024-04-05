using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models
{
    public partial class Ventasdetalleproducto
    {
        public int IdVentaDetalleProducto { get; set; }

        public int IdVenta { get; set; }

        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio")]
        public int PrecioUnitario { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero")]
        public int Cantidad { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;

        public virtual Venta IdVentaNavigation { get; set; } = null!;
    }
}
