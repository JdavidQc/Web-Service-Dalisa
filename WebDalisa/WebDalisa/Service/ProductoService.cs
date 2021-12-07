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
                SqlCommand cmd = new SqlCommand("select * from TBLPRODUCTO as p join TBLCATEGORIA as c on p.[ID CATEGORIA] = c.[ID CATEGORIA] where p.ELIMINADO  in(0)", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ProductoDTO()
                    {
                        codigoProducto = dr[0].ToString(),
                        nombre = dr[1].ToString(),
                        descripcion = dr[2].ToString(),
                        categoria = new CategoriaDTO() { codigoCategoria = dr[3].ToString(),nomCategoria=dr[12].ToString()},
                        precio =Convert.ToDouble(dr[5]),
                        cantidad = Convert.ToInt32(dr[6]),
                        punto = Convert.ToInt32(dr[7]),
                        foto = dr[8].ToString(),
                        fecha =dr[9].ToString().Substring(0,10),
                    });
                }

                return lista;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public override void registrarProducto(ProductoDTO p)
        {
            SqlConnection cn = SqlServerConnection.getConnection();
            SqlCommand cmd = new SqlCommand("usp_ProductoInsertar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", p.codigoProducto);
            cmd.Parameters.AddWithValue("@nombreproducto", p.nombre);
            cmd.Parameters.AddWithValue("@descripcion", p.descripcion);
            cmd.Parameters.AddWithValue("@idcategoria", p.categoria.codigoCategoria);
            cmd.Parameters.AddWithValue("@idmarca", p.marca);
            cmd.Parameters.AddWithValue("@precio", p.precio);
            cmd.Parameters.AddWithValue("@cantidad", p.cantidad);
            cmd.Parameters.AddWithValue("@puntoprod", p.punto);
            cmd.Parameters.AddWithValue("@fotoprod", p.foto);
            cmd.Parameters.AddWithValue("@fechareg", p.fecha);
            cmd.Parameters.AddWithValue("@eliminado", p.eliminado);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.Write(ex.Message);
                throw ex;
            }
            finally { cn.Close(); }
        }

        public override void modificarProducto(ProductoDTO p)
        {
            SqlConnection cn = SqlServerConnection.getConnection();
            SqlCommand cmd = new SqlCommand("usp_ProductoActualiza", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", p.codigoProducto);
            cmd.Parameters.AddWithValue("@nombreproducto", p.nombre);
            cmd.Parameters.AddWithValue("@descripcion", p.descripcion);
            cmd.Parameters.AddWithValue("@idcategoria", p.categoria.codigoCategoria);
            cmd.Parameters.AddWithValue("@precio", p.precio);
            cmd.Parameters.AddWithValue("@cantidad", p.cantidad);
            cmd.Parameters.AddWithValue("@puntoprod", p.punto);
            cmd.Parameters.AddWithValue("@fotoprod", p.foto);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.Write(ex.Message);
                throw ex;
            }
            finally { cn.Close(); }
        }

        public override ProductoDTO BuscarProducto(string id)
        {
            ProductoDTO reg = null;
            SqlConnection cn = SqlServerConnection.getConnection();
            SqlCommand cmd = new SqlCommand("usp_BuscaProducto", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", id);

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    reg = new ProductoDTO()
                    {
                        codigoProducto = dr[0].ToString(),
                        nombre = dr[1].ToString(),
                        descripcion = dr[2].ToString(),
                        categoria = new CategoriaDTO() { codigoCategoria = dr[3].ToString() },
                        marca = dr[4].ToString(),
                        precio = Convert.ToDouble(dr[5]),
                        cantidad = Convert.ToInt32(dr[6]),
                        punto = Convert.ToInt32(dr[7]),
                        foto = dr[8].ToString(),
                        fecha = dr[9].ToString().Substring(0, 10),
                        eliminado = dr[10].ToString(),
                    };
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { cn.Close(); }
            return reg;
        }

        public override void BajaProducto(ProductoDTO p)
        {
            SqlConnection cn = SqlServerConnection.getConnection();
            SqlCommand cmd = new SqlCommand("usp_ProductoBaja", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", p.codigoProducto);

            try
            {
                cn.Open();
                bool ires = cmd.ExecuteNonQuery() == 0 ? true : false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { cn.Close(); }
        }

        public override string nextProducto()
        {
        
            SqlConnection cn = SqlServerConnection.getConnection();
            SqlCommand cmd = new SqlCommand("SP_NEXT_CODIGO", cn);
            cmd.Parameters.AddWithValue("@OPC", "P");
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return dr[0].ToString();
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { cn.Close(); }
            return null;
        }
    }
}