using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDalisa.Interfaces;
using WebDalisa.Models;

using System.Data;
using System.Data.SqlClient;
using WebDalisa.libreria;

namespace WebDalisa.Service
{
     class UsuarioService : UsuarioDAO
    {
        private SqlConnection con;

        public UsuarioService()
        {
            con = null;
        }
        public override UsuarioDTO Afiliar(string usuario)
        {
            throw new NotImplementedException();
        }

        public override string Codigo_Corralico(string fill)
        {
            throw new NotImplementedException();
        }

        public override UsuarioDTO consultarUsuario(string codigo)
        {
            throw new NotImplementedException();
        }

        public override UsuarioDTO consultarXCorreo(string correo)
        {
            throw new NotImplementedException();
        }

        public override bool EnviarCorreo(string form, string body)
        {
            throw new NotImplementedException();
        }

        public override bool UpdatePassword(string password, string codigo)
        {
            throw new NotImplementedException();
        }

        public override UsuarioDTO ValidarAcceso(string use, string pass)
        {
            try
            {
                
                con = SqlServerConnection.getConnection();
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_INGRESAR", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USE", use);
                cmd.Parameters.AddWithValue("@PASS",pass);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                { UsuarioDTO usuario= new UsuarioDTO()
                    {
          idUsuario = dr[0].ToString(),
          pais =dr[1].ToString(),
          nombreUse= dr[2].ToString(),
          apellidoUse= dr[3].ToString(),
          fechaNaci = dr[4].ToString(),
          estadoCivil= dr[5].ToString(),
          sexo= dr[6].ToString(),
          dniUse= dr[7].ToString(),
          direccion= dr[8].ToString(),
          departamento= dr[9].ToString(),
          provincia= dr[10].ToString(),
          distrito= dr[11].ToString(),
          celularUse = dr[12].ToString(),
          correoUse= dr[13].ToString(),
          use = dr[14].ToString(),
          pass = dr[15].ToString(),
         ctnDeposito = dr[16].ToString(),
          banco = new BancoDTO() {idBanco =Convert.ToInt64(dr[17])},
                    };

                    dr.Close(); 
                    return usuario;
                }
             
            }
            catch(SqlException e)
            {
                throw e;
            
            }
            return null;
        }
    }
}