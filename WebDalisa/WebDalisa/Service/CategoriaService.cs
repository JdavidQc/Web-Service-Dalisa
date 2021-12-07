using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using WebDalisa.Interfaces;
using WebDalisa.Models;
using WebDalisa.libreria;

namespace WebDalisa.Service
{
     class CategoriaService : CategoriaDAO
    {
        public override List<CategoriaDTO> listadoCategoria()
        {
            List<CategoriaDTO> lista = new List<CategoriaDTO>();
            SqlConnection cn = SqlServerConnection.getConnection();
            SqlCommand cmd = new SqlCommand("usp_ListaCategoria", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CategoriaDTO reg = new CategoriaDTO()
                    {
                        codigoCategoria = dr[0].ToString(),
                        nomCategoria = dr[1].ToString()
                    };
                    lista.Add(reg);
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            { throw ex; }
            return lista;
        }
    }
}
