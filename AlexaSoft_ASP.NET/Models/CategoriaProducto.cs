using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class CategoriaProducto
{
    public int IdCategoriaProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
