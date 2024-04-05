using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models
{
    public partial class Pedido
    {
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria")]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        [Required(ErrorMessage = "El estado del pedido es obligatorio")]
        public string Estado { get; set; } = null!;

        [Required(ErrorMessage = "El total es obligatorio")]
        public int Total { get; set; }

        [Required(ErrorMessage = "El valor del IVA es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor del IVA no puede ser negativo")]
        public decimal Iva { get; set; }

        public int IdCliente { get; set; }

        public int IdColaborador { get; set; }

        public virtual ICollection<Detallespedidosproducto> Detallespedidosproductos { get; set; } = new List<Detallespedidosproducto>();

        public virtual ICollection<Detallespedidosservicio> Detallespedidosservicios { get; set; } = new List<Detallespedidosservicio>();

        public virtual Cliente IdClienteNavigation { get; set; } = null!;

        public virtual Colaboradore IdColaboradorNavigation { get; set; } = null!;

        public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
    }
}
