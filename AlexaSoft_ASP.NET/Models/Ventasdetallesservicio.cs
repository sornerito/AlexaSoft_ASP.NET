using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models
{
    public partial class Ventasdetallesservicio
    {
        public int IdVentaDetalleServicio { get; set; }

        public int IdVenta { get; set; }

        public int IdPaquete { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio")]
        public int Subtotal { get; set; }

        public virtual Paquete IdPaqueteNavigation { get; set; } = null!;

        public virtual Venta IdVentaNavigation { get; set; } = null!;
    }
}
