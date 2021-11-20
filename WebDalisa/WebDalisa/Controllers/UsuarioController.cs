using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebDalisa.Service;
using WebDalisa.Models;

namespace WebDalisa.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        UsuarioService useService = new UsuarioService();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string use, string pass)
        {
            UsuarioDTO usuario = useService.ValidarAcceso(use, pass);
            if(usuario != null)
            {
                string rol = usuario.idUsuario.Substring(0, 1);
                Session["use"] = usuario;
                Session["rol"] = rol;
                if (rol.Equals("S"))
                {
                    return RedirectToAction("Dashboard");
                }
                else if(rol.Equals("E"))
                { 
                    return RedirectToAction("Administrador");
                }

            }
            return View();
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
    }
}