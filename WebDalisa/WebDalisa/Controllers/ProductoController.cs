using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDalisa.Service;

namespace WebDalisa.Controllers
{
    public class ProductoController : Controller
    {
        ProductoService prodService = new ProductoService();
        public ActionResult listadoProducto()
        {
         
            return View(prodService.listadoProducto());
        }
    }
}