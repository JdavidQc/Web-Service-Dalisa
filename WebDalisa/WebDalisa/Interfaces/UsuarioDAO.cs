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

        public abstract bool EnviarCorreo(string form, string body);

        public abstract UsuarioDTO consultarXCorreo(string correo);

        public abstract UsuarioDTO consultarUsuario(string codigo);

        public abstract bool UpdatePassword(string password, string codigo);

        public abstract UsuarioDTO Afiliar(string usuario);

        public abstract  string Codigo_Corralico(string fill);
    }
}