using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDalisa.Models
{
    public class BancoDTO
    {
        public long idBanco { get; set; }
        public string nombreBanco { get; set; }
        public string tipoAhorro { get; set; }
        public string eliminado { get; set; }

    }
}