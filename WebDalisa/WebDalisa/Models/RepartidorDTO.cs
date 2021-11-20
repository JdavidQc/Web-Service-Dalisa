using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDalisa.Models
{
    public class RepartidorDTO
    {

        public string idRepartidor { get; set; }
        public int cantidadEntregar { get; set; }
        public int cantidadEntregado { get; set; }
        public EmpleadoDTO empleado { get; set; }
    }
}