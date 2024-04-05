using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models
{
    public partial class SalidaInsumo
    {
        public int IdInsumo { get; set; }

        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La fecha de retiro es obligatoria")]
        public DateTime FechaRetiro { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero")]
        public int Cantidad { get; set; }

       
        public string? MotivoAnular { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
