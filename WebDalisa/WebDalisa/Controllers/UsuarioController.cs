using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Net.Mail;

using WebDalisa.Service;
using WebDalisa.Models;

namespace WebDalisa.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        UsuarioService useService = new UsuarioService();
        CategoriaService catService = new CategoriaService();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ListaMovimientoSocio()
        {

            return View(useService.listaMovimiento());
        }
        [HttpPost]
        public ActionResult Login(string use, string pass)
        {

            UsuarioDTO usuario = useService.ValidarAcceso(use, pass);
            if (usuario != null)
            {
                usuario.fechaNaci = usuario.fechaNaci.Substring(0, 10);
                string rol = usuario.idUsuario.Substring(0, 1);

                Session["rol"] = rol;
                if (rol.Equals("S"))
                {
                    Session["use"] = useService.consultarSocio(usuario.idUsuario, "S");
                    return RedirectToAction("Dashboard");
                }

                else if (rol.Equals("E"))
                {
                    Session["use"] = usuario;

                    return RedirectToAction("Administrador");
                }


            }
            else
            {
                return RedirectToAction("Error");
            }


            return View();
        }
        public ActionResult EnvioCorreo()
        {
            return View();
        }
        [HttpPost]
        public JsonResult RealizarEnvio(string correo)
        {
           try
            {

                UsuarioDTO use = useService.consultarXCorreo(correo);
                if(use != null){
                    //CONFIGURAR VALORES
                    string Host = "smtp.office365.com";
                    int Puerto = 587;
                    string Usuario = "i201922732@cibertec.edu.pe";
                    string Clave = "Peru21familia21";//clave generada para aplicación en GMAIL

                    //PROPORCIONAMOS AUTENTICACION DE GMAIL
                    SmtpClient smtp = new SmtpClient(Host, Puerto);
                    MailMessage msg = new MailMessage();


                    //CREAMOS EL CONTENIDO DEL CORREO
                    string[] Destinatario = correo.Split(',');

                    msg.From = new MailAddress(Usuario, "SERVIDOR CORREO");
                    foreach (string c in Destinatario) if (c != "") msg.To.Add(new MailAddress(correo));
                    msg.Subject = "TU CUENTA DE SOCIO DALISA";
                    msg.IsBodyHtml = false;
                    msg.Body = "USERNAME: " + use.use + "\nPASSWORD: " + use.pass;


                    //ENVIAMOS EL CORREO
                    smtp.Credentials = new NetworkCredential(Usuario, Clave);
                    smtp.EnableSsl = true;
                    smtp.Send(msg);
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
             
            }
            catch(Exception e)
            {
             

            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }
        public ActionResult arbolGeneologico()
        {
            return View();
        }

        [HttpPost]
        public JsonResult arbolGeneologicoSocio()
        {
            SocioDTO sc = (SocioDTO)Session["use"];
            return Json(useService.listarPadreHijo(sc.idSocio), JsonRequestBehavior.AllowGet);
        }
        public ActionResult nuevoSocio()
        {
            ViewBag.producto = pdservice.listadoProducto();
            return View();
        }
        [HttpPost]
        public JsonResult AfiliarSocio( SocioDTO s)
        {
            try
            {
                SocioDTO sc = (SocioDTO)Session["use"];
                s.PadreSocio = sc.idSocio;
                s.idSocio = useService.Codigo_Corralivo();
                useService.Afiliar(s);
                return Json(s, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
           
        }

        [HttpPost]
        public JsonResult generarBoleta(List<DetalleBoletaDTO> lista)
        {
            try
            {
            
            VentaService vservice = new VentaService();
            
                List<DetalleBoletaDTO> dp = new List<DetalleBoletaDTO>();
                SocioDTO sc = (SocioDTO)Session["use"];
                CabeceraBoletaDTO cb = new CabeceraBoletaDTO();
                cb.nBoleta = vservice.nextBoleta();
                cb.socio = new SocioDTO() { idSocio=lista[0].cabeceraBoleta.socio.idSocio}; 
                cb.estado = "PENDIENTE";
                cb.nomRepartidor = "R0001";
                cb.fechaEngrega = "null";
                int cantidad = 0;
                foreach (DetalleBoletaDTO c in lista)
                {
                    DetalleBoletaDTO dt = new DetalleBoletaDTO();
                ProductoDTO p = new ProductoService().BuscarProducto(c.producto.codigoProducto);
                    dt.cabeceraBoleta = cb;
                    dt.producto =p;
                    dt.cantidad = c.cantidad;
                    dt.descuento = 0;
                    dt.igv = 0;
                    dt.precio = p.precio/2;
                    dt.punto =p.punto;
                    dt.importePagar = dt.precio*c.cantidad;

                    dp.Add(dt);
                    cb.total_Punto += dt.punto;
                    cb.impotTotal += dt.importePagar;
                    cantidad += dt.cantidad;
                }

                switch (cantidad)
                {
                    case 2: cb.impotTotal = 205; break;
                    case 4: cb.impotTotal = 355; break;
                    case 8: cb.impotTotal = 650; break;
                    case 20: cb.impotTotal = 1550; break;
                }

                vservice.GenerarBoleta(cb, dp);
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Administrador()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Password()
        {
            return View();
        }
        public ActionResult Misdatos()
        {
            ViewBag.Banco = useService.listadoBanco();
            return View();
        }
        public ActionResult CerrarSeccion()
        {

            Session.Abandon();
            return RedirectToAction("Login");
        }
        public ActionResult cobroMonedero()
        {

            return View();
        }
        public ActionResult MovimientoSocio()
        {

            return View();
        }
     
        ProductoService pdservice = new ProductoService();

        [HttpPost]
        public JsonResult buscarProducto(string codigo)
        {
                ProductoDTO p = pdservice.BuscarProducto(codigo);
                if (p != null)
                {
                CarritoDTO c =new CarritoDTO()
                    {
                        producto = p,
                        cantidad = 1,
                        precio = (p.precio * 50) / 100,
                        importoTal = (p.precio * 50) / 100
                    }; return Json(c, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult cobroMonedero(string monto)
        {
            SocioDTO socio = (SocioDTO)Session["use"];

            socio.puntoBono -=Convert.ToDouble(monto);
            useService.updateBono(socio);
               Session["use"] = useService.consultarSocio(socio.idSocio, "S");
      
            return RedirectToAction("cobroMonedero");
        }
    
        [HttpPost]
        public ActionResult RealizarMovimientoSocio(string dni,string sole,string decrip)
        { 
     
            SocioDTO socioEnvio =(SocioDTO) Session["use"];
            SocioDTO socioRecibe = useService.consultarSocio(dni,"D");
           
            if(socioRecibe != null)
            {
                useService.ConfirmarMoviento(new SocioDTO() {idSocio= socioEnvio.idSocio }, new SocioDTO() { idSocio = socioRecibe.idSocio } ,double.Parse(sole), decrip);
                Session["use"] = useService.consultarSocio(socioEnvio.idSocio, "S");
            }
            return RedirectToAction("MovimientoSocio");
        }
        [HttpPost]
        public JsonResult updatePassword(string password)
        {
            try
            {
                SocioDTO s = (SocioDTO)Session["use"];
                useService.UpdatePassword(password, s.usuario.idUsuario);
                Session["use"] = useService.consultarSocio(s.usuario.idUsuario, "S");
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
           
        }
        
        [HttpPost]
        public ActionResult ActualizarDatos(string idusuario = null, string pais = null, string Nombre = null, string apellido = null,
          string fechanaci = null, string estadociv = null, string sexo = null, string dni = null, string direccion = null
         , string celular = null, string correo = null, string cuenta = null, int idbanco = 0)
        {
            useService.actualizarUsuario
                (new UsuarioDTO()

            {
                idUsuario = idusuario,
                pais = pais,
                nombreUse = Nombre,
                apellidoUse = apellido,
                fechaNaci = fechanaci,
                estadoCivil = estadociv,
                sexo = sexo,
                dniUse = dni,
                direccion = direccion,
                celularUse = celular,
                correoUse = correo,
                ctnDeposito = cuenta,
                banco = new BancoDTO()
                {
                    idBanco = idbanco,
                },


            });
            Session["use"] = useService.consultarSocio(idusuario,"S");
            return RedirectToAction("Misdatos");
        }


    }
}