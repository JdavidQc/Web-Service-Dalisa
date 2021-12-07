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


        public override SocioDTO consultarSocio(string codigo, string opc)
        {
            try
            {

                con = SqlServerConnection.getConnection();
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_CONSULTAR_USUARIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COD", codigo);
                cmd.Parameters.AddWithValue("@OPC", opc);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    SocioDTO sc = new SocioDTO()
                    {
                        idSocio = dr[25].ToString(),
                        puntoBono = Convert.ToInt32(dr[26]),
                        activado = dr[27].ToString(),
                        rango = dr[28].ToString(),
                        paqueteInscrito = dr[29].ToString(),
                        usuario = new UsuarioDTO()
                        {
                            idUsuario = dr[0].ToString(),
                            pais = dr[1].ToString(),
                            nombreUse = dr[2].ToString(),
                            apellidoUse = dr[3].ToString(),
                            fechaNaci = dr[4].ToString(),
                            estadoCivil = dr[5].ToString(),
                            sexo = dr[6].ToString(),
                            dniUse = dr[7].ToString(),
                            direccion = dr[8].ToString(),
                            departamento = dr[9].ToString(),
                            provincia = dr[10].ToString(),
                            distrito = dr[11].ToString(),
                            celularUse = dr[12].ToString(),
                            correoUse = dr[13].ToString(),
                            use = dr[14].ToString(),
                            pass = dr[15].ToString(),
                            ctnDeposito = dr[16].ToString()

                        },
                        PadreSocio = dr[31].ToString(),
                        puntoTotalProd = Convert.ToInt32(dr[32]),
                    };
                    try
                    {
                        sc.usuario.banco = new BancoDTO() { idBanco = Convert.ToInt64(dr[17]), nombreBanco = dr[22].ToString(), tipoAhorro = dr[23].ToString() };
                    }
                    catch (Exception e)
                    {
                        sc.usuario.banco = null;
                    }



                    dr.Close();
                    return sc;
                }

            }
            catch (SqlException e)
            {
                throw e;

            }
            return null;
        }
        public override UsuarioDTO consultarUsuario(string codigo, string opc)
        {
            try
            {

                con = SqlServerConnection.getConnection();
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_CONSULTAR_USUARIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COD", codigo);
                cmd.Parameters.AddWithValue("@OPC", opc);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {


                    UsuarioDTO usuario = new UsuarioDTO()
                    {
                        idUsuario = dr[0].ToString(),
                        pais = dr[1].ToString(),
                        nombreUse = dr[2].ToString(),
                        apellidoUse = dr[3].ToString(),
                        fechaNaci = dr[4].ToString(),
                        estadoCivil = dr[5].ToString(),
                        sexo = dr[6].ToString(),
                        dniUse = dr[7].ToString(),
                        direccion = dr[8].ToString(),
                        departamento = dr[9].ToString(),
                        provincia = dr[10].ToString(),
                        distrito = dr[11].ToString(),
                        celularUse = dr[12].ToString(),
                        correoUse = dr[13].ToString(),
                        use = dr[14].ToString(),
                        pass = dr[15].ToString(),
                        ctnDeposito = dr[16].ToString()

                    };
                    try
                    {
                        usuario.banco = new BancoDTO() { idBanco = Convert.ToInt64(dr[17]), nombreBanco = dr[22].ToString(), tipoAhorro = dr[23].ToString() };
                    }
                    catch (Exception e)
                    {
                        usuario.banco = null;
                    }


                    dr.Close();
                    return usuario;
                }

            }
            catch (SqlException e)
            {
                throw e;

            }
            return null;
        }
        public override UsuarioDTO consultarXCorreo(string correo)
        {
            try
            {

                con = SqlServerConnection.getConnection();
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_CONSULTAR_USUARIO_CORREO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CORREO", correo);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {


                    UsuarioDTO usuario = new UsuarioDTO()
                    {

                        use = dr[0].ToString(),
                        pass = dr[1].ToString(),

                    };


                    dr.Close();
                    return usuario;
                }

            }
            catch (SqlException e)
            {
                throw e;

            }
            return null;
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
                cmd.Parameters.AddWithValue("@PASS", pass);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    UsuarioDTO usuario = new UsuarioDTO()
                    {
                        idUsuario = dr[0].ToString(),
                        pais = dr[1].ToString(),
                        nombreUse = dr[2].ToString(),
                        apellidoUse = dr[3].ToString(),
                        fechaNaci = dr[4].ToString(),
                        estadoCivil = dr[5].ToString(),
                        sexo = dr[6].ToString(),
                        dniUse = dr[7].ToString(),
                        direccion = dr[8].ToString(),
                        departamento = dr[9].ToString(),
                        provincia = dr[10].ToString(),
                        distrito = dr[11].ToString(),
                        celularUse = dr[12].ToString(),
                        correoUse = dr[13].ToString(),
                        use = dr[14].ToString(),
                        pass = dr[15].ToString(),
                        ctnDeposito = dr[16].ToString()
                    };
                    try
                    {
                        usuario.banco = new BancoDTO() { idBanco = Convert.ToInt64(dr[17]) };
                    }
                    catch (Exception e)
                    {
                        usuario.banco = null;
                    }

                    dr.Close();
                    return usuario;
                }

            }
            catch (SqlException e)
            {
                throw e;

            }
            return null;
        }


        public override bool UpdatePassword(string password, string codigo)
        {
            SqlConnection cn = SqlServerConnection.getConnection();
            SqlCommand cmd = new SqlCommand("USP_ACTUALIZAR_PASS", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PASS", password);
            cmd.Parameters.AddWithValue("@idusuario", codigo);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { cn.Close(); }

        }
        public override void actualizarUsuario(UsuarioDTO u)
        {
            SqlConnection cn = SqlServerConnection.getConnection();
            SqlCommand cmd = new SqlCommand("USP_ACTUALIZAR_MIS_DATOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@idusuario", u.idUsuario);
            cmd.Parameters.AddWithValue("@pais", u.pais);
            cmd.Parameters.AddWithValue("@Nombre", u.nombreUse);
            cmd.Parameters.AddWithValue("@apellido", u.apellidoUse);
            cmd.Parameters.AddWithValue("@fechanaci", Convert.ToDateTime(u.fechaNaci).ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@estadociv", u.estadoCivil);
            cmd.Parameters.AddWithValue("@sexo", u.sexo);
            cmd.Parameters.AddWithValue("@dni", u.dniUse);
            cmd.Parameters.AddWithValue("@direccion", u.direccion);
            cmd.Parameters.AddWithValue("@celular", u.celularUse);
            cmd.Parameters.AddWithValue("@correo", u.correoUse);
            cmd.Parameters.AddWithValue("@cuenta", u.ctnDeposito);
            cmd.Parameters.AddWithValue("@idbanco", u.banco.idBanco);

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { cn.Close(); }
        }

        public override List<BancoDTO> listadoBanco()
        {
            try
            {
                List<BancoDTO> lista = new List<BancoDTO>();
                SqlConnection con = SqlServerConnection.getConnection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from TBLBANCO where ELIMINADO  in(0)", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new BancoDTO()
                    {
                        idBanco = Convert.ToInt32(dr[0]),
                        nombreBanco = dr[1].ToString(),
                        tipoAhorro = dr[2].ToString(),
                    });
                }

                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public override List<SocioDTO> listadoSocio()
        {

            try
            {
                List<SocioDTO> lista = new List<SocioDTO>();
                con = SqlServerConnection.getConnection();
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from TBLSOCIO as c join TBLUSUARIO as u on u.[ID USUARIO] = c.[ID SOCIO H]", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new SocioDTO()
                    {
                        idSocio = dr[0].ToString(),
                        puntoBono = Convert.ToInt32(dr[1]),
                        activado = dr[2].ToString(),
                        rango = dr[3].ToString(),
                        PadreSocio = dr[6].ToString(),
                        puntoTotalProd = Convert.ToInt32(dr[7]),
                        usuario = new UsuarioDTO() { nombreUse = dr[11].ToString(), apellidoUse = dr[12].ToString() }


                    });



                }
                dr.Close();
                return lista;
            }
            catch (SqlException e)
            {
                throw e;

            }

        }

        public List<Dictionary<string, SocioDTO>> listarPadreHijo(string idSocio)
        {
            bool bandera = false;
            List<SocioDTO> listado = listadoSocio();
            List<Dictionary<string, SocioDTO>> listJson = new List<Dictionary<string, SocioDTO>>();

            for (int i = 0; i < listado.Count(); i++)
            {
                if (!bandera)
                {
                    if (idSocio.Equals(listado[i].idSocio))
                    {
                        Dictionary<string, SocioDTO> hash = new Dictionary<string, SocioDTO>();
                        hash.Add("key", listado[i]);
                        listJson.Add(hash);
                        bandera = true;
                    }
                }
                if (bandera)
                {
                    var hijoData = obtenerHijos(listado, listado[i].idSocio, i);
                    if (hijoData.Count() > 0)
                    {
                        for (int j = 0; j < hijoData.Count(); j++)
                        {
                            for (int k = 0; k < listJson.Count(); k++)
                            {
                                if (hijoData[j]["key"].PadreSocio.Equals(listJson[k]["key"].idSocio))
                                {
                                    listJson.Add(hijoData[j]); break;
                                }
                            }
                        }
                    }

                }

            }

            return listJson;
        }
        private List<Dictionary<string, SocioDTO>> obtenerHijos(List<SocioDTO> lista, string idPadre, int posi)
        {
            List<Dictionary<string, SocioDTO>> listJson = new List<Dictionary<string, SocioDTO>>();
            for (var i = posi + 1; i < lista.Count(); i++)
            {
                if (lista[i].PadreSocio.Equals(idPadre))
                {
                    Dictionary<string, SocioDTO> hash = new Dictionary<string, SocioDTO>();
                    hash.Add("key", lista[i]);
                    listJson.Add(hash);
                }
            }
            return listJson;
        }

        public override void ConfirmarMoviento(SocioDTO idSocioEnvio, SocioDTO idSocioRecibo, double monto_transferencia, string descrip)
        {

            try
            {

                con = SqlServerConnection.getConnection();
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_TRANFERIR_MOVIENTO_SOCIO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idenvio", idSocioEnvio.idSocio);
                cmd.Parameters.AddWithValue("@idrecibe", idSocioRecibo.idSocio);
                cmd.Parameters.AddWithValue("@monto", monto_transferencia);
                cmd.Parameters.AddWithValue("@descripcion", descrip);
                cmd.ExecuteNonQuery();


            }
            catch (SqlException e)
            {
                throw e;

            }


        }
        public override void updateBono(SocioDTO socio)
        {
            try
            {

                con = SqlServerConnection.getConnection();
                con.Open();
                string sql = "update TBLSOCIO set[PUNTO BONO] =" + socio.puntoBono + " where [ID SOCIO H] ='" + socio.idSocio + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;

            }
        }

        public override void Afiliar(SocioDTO s)
        {

            SqlConnection cn = SqlServerConnection.getConnection();
            SqlCommand cmd = new SqlCommand("USP_AFILIAR_SOCIO", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@idsosio", s.idSocio);
            cmd.Parameters.AddWithValue("@pais", s.usuario.pais);
            cmd.Parameters.AddWithValue("@Nombre", s.usuario.nombreUse);
            cmd.Parameters.AddWithValue("@apellido", s.usuario.apellidoUse);
            cmd.Parameters.AddWithValue("@fechanaci", Convert.ToDateTime(s.usuario.fechaNaci).ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@estadociv", s.usuario.estadoCivil);
            cmd.Parameters.AddWithValue("@sexo", s.usuario.sexo);
            cmd.Parameters.AddWithValue("@dni", s.usuario.dniUse);
            cmd.Parameters.AddWithValue("@direccion", s.usuario.direccion);
            cmd.Parameters.AddWithValue("@departamento", s.usuario.departamento);
            cmd.Parameters.AddWithValue("@provincia", s.usuario.provincia);
            cmd.Parameters.AddWithValue("@distrito", s.usuario.distrito);
            cmd.Parameters.AddWithValue("@celular", s.usuario.celularUse);
            cmd.Parameters.AddWithValue("@correo", s.usuario.correoUse);
            cmd.Parameters.AddWithValue("@pass", s.usuario.pass);
            cmd.Parameters.AddWithValue("@paquete", s.paqueteInscrito);
            cmd.Parameters.AddWithValue("@idsociop", s.PadreSocio);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally { cn.Close(); }
        }

        public override string Codigo_Corralivo()
        {
            try
            {

                con = SqlServerConnection.getConnection();
                con.Open();
                string sql = "SP_NEXT_CODIGO 'S'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return dr[0].ToString();
                }
            }
            catch (SqlException e)
            {
                throw e;

            }
            return null;
        }

        public override List<MovimientoDTO> listaMovimiento()
        {
            try
            {
                List<MovimientoDTO> lista = new List<MovimientoDTO>();
                con = SqlServerConnection.getConnection();
                con.Open();

                SqlCommand cmd = new SqlCommand("SP_LISTAR_MOVIMIENTO", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new MovimientoDTO()
                    {
                        codigoMovimiento = Convert.ToInt32(dr[0]),
                        idSocioEnvio = new SocioDTO() { usuario = new UsuarioDTO() { nombreUse = dr[1].ToString() } },
                        idSocioRecibo = new SocioDTO() { usuario = new UsuarioDTO() { nombreUse = dr[2].ToString() } },
                        monto_transferencia = Convert.ToDouble(dr[3].ToString()),
                        fecha_movimiento = dr[4].ToString(),
                        descrip = dr[5].ToString(),
                        estado = dr[6].ToString(),
                    });

                }
                dr.Close();
                return lista;
            }
            catch (SqlException e)
            {
                throw e;

            }
        }
    }
}