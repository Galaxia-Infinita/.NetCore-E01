using LabWeb_02.Models;
using LabWeb_02.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LabWeb_02.Controllers
{
    public class VentasIDAT : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        VentasIDATContext db = new VentasIDATContext();
        public IActionResult PedidosporCliente(string codigo=null) 
        {
            //Para cargar todos los clientes en la etiqueta Select
            //viewBag.clientes=
            ViewData["clientes"] = new SelectList(db.Clientes,
                "IdCliente", "NombreCompañia",codigo); 
            //consulta de LINQ
            var query=from p in db.Pedidos where
                      p.IdCliente.Equals(codigo) select p;

            return View(query.ToList());
        }
        public IActionResult PedidosporAño(int? anio) 
        {   //capturando años para el combobox
            var queryanios = (from p in db.Pedidos
                              orderby p.FechaPedido.Value.Year descending
                              select new { p.FechaPedido.Value.Year }).Distinct();
            //leyendo años en el combobox(viewbag.anios va para el htmlcs en el select)
            ViewBag.anios = new SelectList(queryanios.ToList(), "Year", "Year", anio);

            //capturando datos que iran en tabla y calculando total por año
            var query = from p in db.Pedidos
                        join c in db.Clientes on p.IdCliente equals c.IdCliente
                        join dp in db.DetallesDePedidos on p.IdPedido equals dp.IdPedido
                        where p.FechaPedido.Value.Year.Equals(anio)
                        group new { p, c, dp }
                        by new { p.IdPedido, p.FechaPedido.Value.Year, c.NombreCompañia }
                        into gidat
                        select new ConsultaPedidosporAnio
                        {
                            nropedido = gidat.Key.IdPedido,
                            anioVenta = gidat.Key.Year,
                            cliente = gidat.Key.NombreCompañia,
                            total = Math.Round(gidat.Sum(g => g.dp.Cantidad * 
                                    g.dp.PrecioUnidad *(decimal)(1-g.dp.Descuento)),2)
                        };
            //sumando totales que ira en total general 
            ViewBag.sumatotalcompra = query.Sum(x => x.total);

            return View(query.ToList());
        }
        public IActionResult PedidosporClienteyAnio(string codigo=null, int? anio=null) 
        {
            //select de clientes
            ViewData["clientes"] = new SelectList(db.Clientes,
                "IdCliente", "NombreCompañia", codigo);

            var queryanios = (from p in db.Pedidos
                              orderby p.FechaPedido.Value.Year descending
                              select new { p.FechaPedido.Value.Year }).Distinct();
            //select de anios
            ViewBag.anios = new SelectList(queryanios.ToList(), "Year", "Year", anio);

            //consulta LINQ
            var query = from p in db.Pedidos
                        join c in db.Clientes
                        on p.IdCliente equals c.IdCliente
                        where p.IdCliente.Equals(codigo) && p.FechaPedido.Value.Year == anio

                        select new ConsultaPedidosporClienteyAnio
                        {
                            NroPedido = p.IdPedido,
                            Cliente = c.NombreCompañia,
                            FechaPedido = p.FechaPedido,
                            MesVenta = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(
                                p.FechaPedido.Value.Month),    //solo para obtener mes
                            AnioVenta = p.FechaPedido.Value.Year,
                            CargoEnvio = p.Cargo
                        };

            return View(query.ToList());
        }
        public IActionResult CategoriasyProductos() 
        {
            return View();
        }
    }
}
