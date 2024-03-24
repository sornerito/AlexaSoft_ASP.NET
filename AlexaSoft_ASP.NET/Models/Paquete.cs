using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Paquete
{
    public int IdPaquete { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Detallespedidosservicio> Detallespedidosservicios { get; set; } = new List<Detallespedidosservicio>();

    public virtual ICollection<PaquetesServicio> PaquetesServicios { get; set; } = new List<PaquetesServicio>();

    public virtual ICollection<Ventasdetallesservicio> Ventasdetallesservicios { get; set; } = new List<Ventasdetallesservicio>();
}
