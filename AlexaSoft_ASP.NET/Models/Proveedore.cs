using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models
{
    public partial class Proveedore
    {
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La descripción del proveedor es obligatoria")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "El número de teléfono del proveedor es obligatorio")]
        [Phone(ErrorMessage = "El número de teléfono no es válido")]
        public string Telefono { get; set; } = null!;

        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
    }
}
