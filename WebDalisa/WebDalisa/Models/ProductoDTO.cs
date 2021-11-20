using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDalisa.Models
{
    public class ProductoDTO
    {
        public string codigoProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public CategoriaDTO categoria { get; set; }
        public string marca { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public int punto { get; set; }
        public string foto { get; set; }
        public string fecha { get; set; }
        public string eliminado { get; set; }
    }
}