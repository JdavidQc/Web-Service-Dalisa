using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDalisa.Models;

namespace WebDalisa.Interfaces
{
    abstract class UsuarioDAO
    {
        
        public abstract UsuarioDTO ValidarAcceso(string use, string pass);


        public abstract UsuarioDTO consultarXCorreo(string correo);

        public abstract SocioDTO consultarSocio(string codigo,string opc);

       public abstract bool UpdatePassword(string password, string codigo);

        public abstract void Afiliar(SocioDTO s);

        public abstract  string Codigo_Corralivo();

        public abstract void updateBono(SocioDTO socio);
        public abstract void actualizarUsuario(UsuarioDTO u);
        public abstract UsuarioDTO consultarUsuario(string codigo, string opc);
        public abstract List<SocioDTO> listadoSocio();

        public abstract List<BancoDTO> listadoBanco();

      public abstract List<MovimientoDTO> listaMovimiento();

      public abstract void ConfirmarMoviento(SocioDTO idSocioEnvio, SocioDTO idSocioRecibo, double monto_transferencia, string descrip);

    }
}