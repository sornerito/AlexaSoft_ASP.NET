using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

namespace AlexaSoft_ASP.NET.Models
{
    public partial class CategoriaProducto
    {
        public int IdCategoriaProducto { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
