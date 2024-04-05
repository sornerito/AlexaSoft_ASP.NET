using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models
{
    public partial class Venta
    {
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "El número de factura es obligatorio.")]
        public string NumeroFactura { get; set; }

        [Required(ErrorMessage = "El pedido es obligatorio.")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "El colaborador es obligatorio.")]
        public int IdColaborador { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public string MotivoAnular { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El total debe ser mayor o igual a cero.")]
        public int Total { get; set; }

        [Required(ErrorMessage = "El IVA es obligatorio.")]
        [Range(0, 100, ErrorMessage = "El IVA debe estar entre 0 y 100.")]
        public int Iva { get; set; }

        public virtual Colaboradore IdColaboradorNavigation { get; set; }
        public virtual Pedido IdPedidoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }

        public virtual ICollection<Ventasdetalleproducto> Ventasdetalleproductos { get; set; } = new List<Ventasdetalleproducto>();
        public virtual ICollection<Ventasdetallesservicio> Ventasdetallesservicios { get; set; } = new List<Ventasdetallesservicio>();
    }
}
