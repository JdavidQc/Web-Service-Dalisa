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
     class ProductoService : ProductoDAO
    {
        public override List<ProductoDTO> listadoProducto()
        {
            try
            {
                List<ProductoDTO> lista = new List<ProductoDTO>();
                SqlConnection con = SqlServerConnection.getConnection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from TBLPRODUCTO where ELIMINADO  in(0)", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ProductoDTO()
                    {
                        codigoProducto = dr[0].ToString(),
                        nombre = dr[1].ToString(),
                        descripcion = dr[2].ToString(),
                        categoria = new CategoriaDTO() { codigoCategoria = dr[3].ToString()},
                        marca = dr[4].ToString(),
                        precio =Convert.ToDouble(dr[5]),
                        cantidad = Convert.ToInt32(dr[6]),
                        punto = Convert.ToInt32(dr[7]),
                        foto = dr[8].ToString(),
                        fecha =dr[9].ToString().Substring(0,10),
                        eliminado = dr[10].ToString(),
                    });
                }

                return lista;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public override void modificarProducto(ProductoDTO p)
        {
            throw new NotImplementedException();
        }
    }
}