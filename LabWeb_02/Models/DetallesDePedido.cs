using System;
using System.Collections.Generic;

#nullable disable

namespace LabWeb_02.Models
{
    public partial class DetallesDePedido
    {
        public int? IdPedido { get; set; }
        public int IdProducto { get; set; }
        public decimal PrecioUnidad { get; set; }
        public short Cantidad { get; set; }
        public float Descuento { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
