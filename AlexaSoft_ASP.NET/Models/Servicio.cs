using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }
    
    [Required]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "El campo Nombre no debe contener números.")]
    public string Nombre { get; set; } = null!;
    [Required]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "El campo Descripcion no debe contener números.")]
    public string Descripcion { get; set; } = null!;

    public int TiempoMinutos { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<PaquetesServicio> PaquetesServicios { get; set; } = new List<PaquetesServicio>();

    public virtual ICollection<ServiciosProducto> ServiciosProductos { get; set; } = new List<ServiciosProducto>();

}
