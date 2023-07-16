using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabWeb_02.ViewModel
{
    public class ConsultaPedidosporAnio
    {
        public int nropedido { get; set; }
        public int anioVenta { get; set; }
        public string cliente { get; set; }
        public decimal total { get; set; }
    }
}
