using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexaSoft_ASP.NET.Models
{
    public partial class Producto
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La marca del producto es obligatoria")]
        public string Marca { get; set; } = null!;

        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        public int Precio { get; set; }

        [Required(ErrorMessage = "El número de unidades del producto es obligatorio")]
        public int Unidades { get; set; }

        [Required(ErrorMessage = "El estado del producto es obligatorio")]
        public string Estado { get; set; } = null!;

        public int IdCategoriaProducto { get; set; }

        public virtual ICollection<Detallespedidosproducto> Detallespedidosproductos { get; set; } = new List<Detallespedidosproducto>();

        public virtual ICollection<Detallesproductosxcompra> Detallesproductosxcompras { get; set; } = new List<Detallesproductosxcompra>();

        public virtual CategoriaProducto? IdCategoriaProductoNavigation { get; set; }

        public virtual ICollection<SalidaInsumo> SalidaInsumos { get; set; } = new List<SalidaInsumo>();

        public virtual ICollection<ServiciosProducto> ServiciosProductos { get; set; } = new List<ServiciosProducto>();

        public virtual ICollection<Ventasdetalleproducto> Ventasdetalleproductos { get; set; } = new List<Ventasdetalleproducto>();
    }
}
