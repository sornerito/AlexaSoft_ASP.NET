﻿using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int TiempoMinutos { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<PaquetesServicio> PaquetesServicios { get; set; } = new List<PaquetesServicio>();

    public virtual ICollection<ServiciosProducto> ServiciosProductos { get; set; } = new List<ServiciosProducto>();
}
