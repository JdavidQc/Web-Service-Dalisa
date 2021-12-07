using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDalisa.Models;
namespace WebDalisa.Interfaces
{
    abstract class CategoriaDAO
    {
        public abstract List<CategoriaDTO> listadoCategoria();
    }
}