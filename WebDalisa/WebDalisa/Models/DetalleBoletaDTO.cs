using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDalisa.Models
{
    public class DetalleBoletaDTO
    {
        public int id { get; set; }
        public CabeceraBoletaDTO cabeceraBoleta { get; set; }
        public ProductoDTO producto { get; set; }
        public double precio { get; set; }
        public double descuento { get; set; }
        public int cantidad { get; set; }
        public int punto { get; set; }
        public double igv { get; set; }
        public double importePagar { get; set; }

    }
}