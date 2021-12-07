using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebDalisa.Service;
using WebDalisa.Models;

namespace WebDalisa.Controllers
{
    public class VentaProductoController : Controller
    {
        // GET: VentaProducto

        ProductoService pdservice = new ProductoService();
        CategoriaService ctservice = new CategoriaService();
        VentaService vservice = new VentaService();
        public ActionResult TiendaProducto(string cate = null)
        {
            if (cate != null && !cate.Equals("TODO"))
            {
                List<ProductoDTO> lista = new List<ProductoDTO>();
                foreach (ProductoDTO p in pdservice.listadoProducto())
                {
                    if (p.categoria.codigoCategoria.Equals(cate))
                    {
                        lista.Add(p);
                    }
                }
                ViewBag.producto = lista; ViewBag.cantidad = lista.Count();
            }
            else
            {
                List<ProductoDTO> lista = pdservice.listadoProducto();
                ViewBag.producto = lista;
                ViewBag.cantidad = lista.Count();

            }
            ViewBag.categoria = ctservice.listadoCategoria();
            return View();
        }
   
        public ActionResult MisCompras()
        {
            SocioDTO sc = (SocioDTO)Session["use"];
            ViewBag.listadoCabcera = vservice.listadoCabcera(sc.idSocio,"S");
            return View();
        }
        public ActionResult RecojoProducto()
        {
            ViewBag.listadoCabcera = vservice.listadoCabcera("s", "T");
            return View();
        }
        public ActionResult CancelarPedido()
        {
            ViewBag.listadoCabcera = vservice.listadoCabcera("s", "C");
            return View();
        }
     
        public ActionResult SalidaProducto()
        {
            ViewBag.listadoCabcera = vservice.listadoCabcera("s", "D");
            return View();
        }
        [HttpPost]
        public JsonResult DetalleBoleta(string nboleta)
        {
           

            return Json(vservice.listadoDettalle(nboleta), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult transacionBoleta(string nboleta,string opc)
        {
            try
            {
                vservice.ProcedoBoleta(nboleta, opc);
                return RedirectToAction("RecojoProducto");
            }
            catch(Exception e)
            {
                return View();
            }



        }
        [HttpPost]
        public ActionResult EnvioPedido(string nboleta, string opc)
        {
            try
            {
                vservice.ProcedoBoleta(nboleta, opc);
                return RedirectToAction("RecojoProducto");
            }
            catch (Exception e)
            {
                return View();
            }



        }


        [HttpPost]
        public ActionResult agregarCarrito(string codigo, int cantidad = 1)
        {
            bool nuevo = true;
            List<CarritoDTO> lista;
            if (Session["carrito"] == null)
            {
                lista = new List<CarritoDTO>();
                ProductoDTO p = pdservice.BuscarProducto(codigo);
                if (p != null)
                {
                    lista.Add(new CarritoDTO()
                    {
                        producto = p,
                        cantidad = 1,
                        precio = (p.precio * 50) / 100,
                        importoTal = (p.precio * 50) / 100
                    });
                }
                Session["carrito"] = lista;
            }
            else
            {
                lista = (List<CarritoDTO>)Session["carrito"];

                if (lista.Count() > 0)
                {
                    for (int i = 0; i < lista.Count(); i++)
                    {
                        if (lista[i].producto.codigoProducto.Equals(codigo))
                        {
                            lista[i].cantidad = (cantidad < 0) ? (lista[i].cantidad + cantidad) : (lista[i].cantidad + cantidad);
                            lista[i].importoTal = lista[i].precio * lista[i].cantidad;
                            if (lista[i].cantidad <= 0)
                            {
                                lista.RemoveAt(i);
                            }
                            nuevo = false;
                            break;
                        }
                    }
                }

                if (nuevo)
                {
                    ProductoDTO p = pdservice.BuscarProducto(codigo);
                    if (p != null)
                    {
                        lista.Add(new CarritoDTO()
                        {
                            producto = p,
                            cantidad = 1,
                            precio = (p.precio * 50) / 100,
                            importoTal = (p.precio * 50) / 100
                        });
                    }

                }
            }
            return RedirectToAction("TiendaProducto");
        }
      

        [HttpPost]
        public ActionResult generarBoleta(string opc)
        {
         
      
            if (Session["carrito"] != null)
            {
                List<CarritoDTO> lista = (List<CarritoDTO>) Session["carrito"];
                List<DetalleBoletaDTO> dp = new List<DetalleBoletaDTO>();
                SocioDTO sc = (SocioDTO)Session["use"];
              CabeceraBoletaDTO cb = new CabeceraBoletaDTO();
                cb.nBoleta = vservice.nextBoleta();
                cb.socio = sc; cb.estado = "PENDIENTE";
                cb.nomRepartidor = (opc.Equals("D")) ? "R0002" : "R0001";
                cb.fechaEngrega=(opc.Equals("D"))?DateTime.Today.AddDays(2).ToString("yyyy-MM-dd"): "null";
                int cantidad = 0;
                foreach (CarritoDTO c in lista)
                {
                    DetalleBoletaDTO dt = new DetalleBoletaDTO();
                    dt.cabeceraBoleta = cb;
                    dt.producto = c.producto;
                    dt.cantidad = c.cantidad;
                    dt.descuento = 50;
                    dt.igv = (opc.Equals("D"))?18:0;
                    dt.precio = c.precio;
                    dt.punto = c.producto.punto;
                    dt.importePagar = c.importoTal;
                    
                    dp.Add(dt);
                    cb.total_Punto += dt.punto;
                    cb.impotTotal += dt.importePagar;
                    cantidad += dt.cantidad;
                }
         
                switch (cantidad)
                {
                    case 2: cb.impotTotal=205; break;
                    case 4: cb.impotTotal = 355; break;
                    case 8: cb.impotTotal = 650; break;
                    case 20: cb.impotTotal = 1550; break;
                }
              
                vservice.GenerarBoleta(cb, dp);
                Session.Remove("carrito");
            }

            
            return RedirectToAction("TiendaProducto");
        }






    }
}