using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class PaquetesServicio
{
    public int IdPaqueteXservicio { get; set; }

    public int IdPaquete { get; set; }

    public int IdServicio { get; set; }

    public virtual Paquete IdPaqueteNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
