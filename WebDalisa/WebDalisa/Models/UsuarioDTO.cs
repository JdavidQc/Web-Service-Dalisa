using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDalisa.Models
{
    public class UsuarioDTO
    {
        public string idUsuario { get; set; }
        public string pais { get; set; }
        public string nombreUse { get; set; }
        public string apellidoUse { get; set; }
        public string fechaNaci { get; set; }
        public string estadoCivil { get; set; }
        public string sexo { get; set; }
        public string dniUse { get; set; }
        public string direccion { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public string celularUse { get; set; }
        public string correoUse { get; set; }
        public string use { get; set; }
        public string pass { get; set; }
        public string ctnDeposito { get; set; }
        public BancoDTO banco { get; set; }
        public byte[] fotoUSE { get; set; }
        public string fechaRegistro { get; set; }
        public string eliminado { get; set; }
    }
}