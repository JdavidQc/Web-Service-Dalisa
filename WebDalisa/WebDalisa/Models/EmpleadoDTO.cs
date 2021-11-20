using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDalisa.Models
{
    public class EmpleadoDTO
    {
        public string idEmpleado { get; set; }
        public string area { get; set; }
        public double sueldo { get; set; }
        public string estado { get; set; }
        public UsuarioDTO usuario { get; set; }
    }
}