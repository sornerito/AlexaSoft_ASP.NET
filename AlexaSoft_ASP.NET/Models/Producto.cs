using System;
using System.Collections.Generic;

namespace AlexaSoft_ASP.NET.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public int Precio { get; set; }

    public int Unidades { get; set; }

    public string Estado { get; set; } = null!;

    public int IdCategoriaProducto { get; set; }

    public virtual ICollection<Detallespedidosproducto> Detallespedidosproductos { get; set; } = new List<Detallespedidosproducto>();

    public virtual ICollection<Detallesproductosxcompra> Detallesproductosxcompras { get; set; } = new List<Detallesproductosxcompra>();

    public virtual CategoriaProducto IdCategoriaProductoNavigation { get; set; } = null!;

    public virtual ICollection<SalidaInsumo> SalidaInsumos { get; set; } = new List<SalidaInsumo>();

    public virtual ICollection<ServiciosProducto> ServiciosProductos { get; set; } = new List<ServiciosProducto>();

    public virtual ICollection<Ventasdetalleproducto> Ventasdetalleproductos { get; set; } = new List<Ventasdetalleproducto>();
}
