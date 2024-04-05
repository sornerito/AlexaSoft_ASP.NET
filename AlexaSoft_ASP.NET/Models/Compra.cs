using System;
using System.Collections.Generic;


namespace AlexaSoft_ASP.NET.Models
{
    public partial class Compra
    {
        public int IdCompra { get; set; }

        public int? IdProveedor { get; set; }

   
        public int Precio { get; set; }

    
        public DateTime Fecha { get; set; }

       
        public int Subtotal { get; set; }

        public string? MotivoAnular { get; set; }

        public virtual ICollection<Detallesproductosxcompra> Detallesproductosxcompras { get; set; } = new List<Detallesproductosxcompra>();

        public virtual Proveedore? IdProveedorNavigation { get; set; }
    }
}
