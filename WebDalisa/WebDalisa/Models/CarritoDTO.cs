using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDalisa.Models
{
    public class CarritoDTO
    {
        public ProductoDTO producto { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public double importoTal { get; set; }

    }
}