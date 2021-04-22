using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webappPrueba.Models;

namespace webappPrueba.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection values)
        {
            Admin auth = new Admin(values["email"], values["password"]);
            MantenimientoAdministradores mA = new MantenimientoAdministradores();
            Admin registro = mA.existeUsuario(auth);
            if(registro.email == "")
            {
                auth.Errores.Add("El usuario no existe");
            }
            else
            {
                bool resultado = MantenimientoAdministradores.comprobarPassword(auth, registro);
                if (resultado)
                {
                    Session["user"] = auth.email;
                    Session["role"] = "admin";
                    return RedirectToRoute(new
                    {
                        controller = "Admin",
                        action = "Index",
                    });
                }
            }
            ViewBag.Errores = auth.Errores;
            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index",
            });
        }
    }
}