using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabWeb_02.ViewModel
{
    public class ConsultaPedidosporClienteyAnio
    {
        public int NroPedido { get; set; }
        public string Cliente { get; set; }
        public DateTime? FechaPedido { get; set; }
        public string MesVenta { get; set; }
        public int AnioVenta { get; set; }
        public decimal? CargoEnvio { get; set; }
    }
}
