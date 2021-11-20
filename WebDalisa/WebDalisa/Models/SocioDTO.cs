using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDalisa.Models
{
    public class SocioDTO
    {
        public string idSocio { get; set; }
        public double puntoBono { get; set; }
        public string activado { get; set; }
        public string rango { get; set; }
        public string paqueteInscrito { get; set; }
        public UsuarioDTO usuario { get; set; }
        public string PadreSocio { get; set; }
        public int puntoTotalProd { get; set; }
    }
}