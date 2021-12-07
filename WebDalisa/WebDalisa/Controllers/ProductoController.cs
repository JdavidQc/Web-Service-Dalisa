using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDalisa.Service;
using WebDalisa.Models;
using System.IO;

namespace WebDalisa.Controllers
{
    public class ProductoController : Controller
    {
        ProductoService prodService = new ProductoService();
        CategoriaService catService = new CategoriaService();
        public ActionResult listadoProducto()
        {
            
                 return View(prodService.listadoProducto());
         
           
        }
      
        public ActionResult InsertarProducto()
        {
            ViewBag.Categoria =  (catService.listadoCategoria());
            ViewBag.nextCodigo = prodService.nextProducto();
            return View(new ProductoDTO());
        }
        [HttpPost]
        public ActionResult InsertarProducto(ProductoDTO reg, string codigo, string cate,HttpPostedFileBase file)
        {
            try
            {
                if(file != null && file.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/img/"), Path.GetFileName(file.FileName));
                    if (!System.IO.File.Exists(path))
                    {
                        file.SaveAs(path);
                    }
                   
                }
                    reg.categoria = new CategoriaDTO()
                    {
                        codigoCategoria = cate
                        
                    };
                    reg.codigoProducto = codigo;
                    reg.fecha = DateTime.Today.ToString("dd-MM-yyyy");
                    reg.eliminado = "0";
                    reg.marca = "M0001";
                    reg.foto = file.FileName;

                   prodService.registrarProducto(reg);
                    return RedirectToAction("listadoProducto");
              
                
            }
           catch(Exception ex)
            {
                return View();
            }
        }
        public ActionResult ModificarProducto(string id)
        {
            ProductoDTO reg = prodService.BuscarProducto(id);
            ViewBag.Categoria = (catService.listadoCategoria());
            ViewBag.CodCate = reg.categoria.codigoCategoria;
            return View(reg);
        }
        [HttpPost]
        public ActionResult ModificarProducto(ProductoDTO reg, string cate, HttpPostedFileBase file)
        {
            try
            {
                ProductoDTO registro = prodService.BuscarProducto(reg.codigoProducto);

                reg.fecha = registro.fecha;
                reg.eliminado = "0";
                reg.marca = "M0001";
                reg.foto = registro.foto;
                if (file != null && file.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/img/"), Path.GetFileName(file.FileName));
                    if (!System.IO.File.Exists(path))
                    {
                        file.SaveAs(path);
                        reg.foto = file.FileName;
                    }

                }
             

                reg.categoria = new CategoriaDTO()
                    {
                        codigoCategoria = cate
                    };
                    prodService.modificarProducto(reg);
                    return RedirectToAction("listadoProducto");
                
                
            }
            catch (Exception ex)
            {
                return View();
            }
        }
       
        
        public ActionResult EliminarProducto(string id)
        {
            ProductoDTO pro = prodService.BuscarProducto(id);
            prodService.BajaProducto(pro);
            return RedirectToAction("listadoProducto");
        }
    }
    
}