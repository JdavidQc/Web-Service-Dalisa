using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDalisa.Models;

namespace WebDalisa.Interfaces
{
    abstract class ProductoDAO
    {
     public abstract List<ProductoDTO> listadoProducto();

     public abstract void modificarProducto(ProductoDTO p);

     public abstract void registrarProducto(ProductoDTO p);

     public abstract   ProductoDTO  BuscarProducto(string id);

     public abstract void BajaProducto(ProductoDTO p);
        public abstract string nextProducto();
    }
}