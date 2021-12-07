using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDalisa.Models
{
    public class MovimientoDTO
    {
        public int codigoMovimiento{ get; set; }
        public SocioDTO idSocioEnvio { get; set; }
        public SocioDTO idSocioRecibo { get; set; }
        public double monto_transferencia { get; set; }
        public string fecha_movimiento { get; set; }
        public string descrip { get; set; }
        public string estado { get; set; }
 

    }

}