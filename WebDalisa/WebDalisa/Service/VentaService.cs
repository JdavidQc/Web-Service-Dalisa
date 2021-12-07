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
     class VentaService : VentaDAO
    {

        public override void GenerarBoleta(CabeceraBoletaDTO cb, List<DetalleBoletaDTO> db)
        {
            try
            {
                List<BancoDTO> lista = new List<BancoDTO>();
                SqlConnection con = SqlServerConnection.getConnection();
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_CabeceraBoleta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numeBoleta", cb.nBoleta);
                cmd.Parameters.AddWithValue("@idsocio",cb.socio.idSocio);
                cmd.Parameters.AddWithValue("@idrepar",cb.nomRepartidor);
                cmd.Parameters.AddWithValue("@fecha",cb.fechaEngrega);
                cmd.Parameters.AddWithValue("@totaPunto",cb.total_Punto);
                cmd.Parameters.AddWithValue("@impt",cb.impotTotal);
                cmd.Parameters.AddWithValue("@estado",cb.estado);
                cmd.ExecuteNonQuery();
                foreach(var item in db)
                {
                    cmd = new SqlCommand("SP_DetalleBoleta", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nBoleta", cb.nBoleta);
                    cmd.Parameters.AddWithValue("@idProducto", item.producto.codigoProducto);
                    cmd.Parameters.AddWithValue("@precio" ,item.precio);
                    cmd.Parameters.AddWithValue("@dec", item.descuento);
                    cmd.Parameters.AddWithValue("@cant", item.cantidad);
                    cmd.Parameters.AddWithValue("@punt", item.punto);
                    cmd.Parameters.AddWithValue("@igv", item.igv); 
                    cmd.Parameters.AddWithValue("@impt",item.importePagar);
                    cmd.ExecuteNonQuery();
                }
              

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override string nextBoleta()
        {
            try
            {
                List<BancoDTO> lista = new List<BancoDTO>();
                SqlConnection con = SqlServerConnection.getConnection();
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_NEXT_CODIGO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OPC", "B");
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return dr[0].ToString();
                }
          

            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }




        public override List<CabeceraBoletaDTO> listadoCabcera(string idSocio,string opc)
        {
            try
            {
                List<CabeceraBoletaDTO> lista = new List<CabeceraBoletaDTO>();
                UsuarioService use = new UsuarioService();
                SqlConnection con = SqlServerConnection.getConnection();
                con.Open();
                SqlCommand cmd = new SqlCommand("listarCabecera", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", idSocio);
                cmd.Parameters.AddWithValue("@opc", opc);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new CabeceraBoletaDTO()
                    {
                        nBoleta=dr[0].ToString(),
                        nomRepartidor= dr[1].ToString(),
                        total_Punto=Convert.ToInt32(dr[2]),
                        fechaPedido=dr[3].ToString(),
                        fechaEngrega = dr[4].ToString(),
                        impotTotal = Convert.ToDouble(dr[5]),
                        estado=dr[6].ToString()
                    });
                }

                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override List<DetalleBoletaDTO> listadoDettalle(string nboleta)
        {
            try
            {
                List<DetalleBoletaDTO> lista = new List<DetalleBoletaDTO>();
                SqlConnection con = SqlServerConnection.getConnection();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM TBLDETALLE_BOLETA where NUM_BOLETA = '" + nboleta + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new DetalleBoletaDTO()
                    {
                        cabeceraBoleta = new CabeceraBoletaDTO() { nBoleta = dr[1].ToString() },
                        producto = new ProductoService().BuscarProducto(dr[2].ToString()),
                        precio = Convert.ToDouble(dr[3]),
                        descuento = Convert.ToDouble(dr[4]),
                        cantidad = Convert.ToInt32(dr[5]),
                        punto = Convert.ToInt32(dr[6]),
                        igv = Convert.ToDouble(dr[7]),
                        importePagar = Convert.ToDouble(dr[8]),
                    }) ;
                }

                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override void ProcedoBoleta(string nboleta, string opc)
        {
            try
            {
                List<DetalleBoletaDTO> lista = new List<DetalleBoletaDTO>();
                SqlConnection con = SqlServerConnection.getConnection();
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_ActualizaEstado_Boleta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BNOL", nboleta);
                cmd.Parameters.AddWithValue("@OPC", opc);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            

        }
    }
}