using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDalisa.Models
{
    public class CabeceraBoletaDTO
    {
        public  string nBoleta { get; set; }
        public SocioDTO socio { get; set; }
        public string nomRepartidor { get; set; }
        public int total_Punto { get; set; }
        public string fechaPedido { get; set; }
        public string fechaEngrega { get; set; }
        public double impotTotal { get; set; }
        public string estado { get; set; }
    }
}