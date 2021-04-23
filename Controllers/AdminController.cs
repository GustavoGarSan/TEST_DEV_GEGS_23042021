using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using webappPrueba.Models;

namespace webappPrueba.Controllers
{
    public class AdminController : Controller
    {
        PersonasFisicasCompleto pfc = new PersonasFisicasCompleto();
        // GET: Admin
        public ActionResult Index(int resultado = 0)
        {
            if(Session["user"] != null)
            {
                MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
                List<PersonasFisicasCompleto> ListaPFC = new List<PersonasFisicasCompleto>();
                ListaPFC = mpf.Listar();
                ViewData["PersonasFisicas"] = ListaPFC;
                if (resultado != 0)
                {
                    ViewData["resultado"] = resultado;
                }
                return View();
            }
            else
            {
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index",
                });
            }
        }

        public ActionResult Consumidores()
        {
            if(Session["user"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index",
                });
            }
        }

        public ActionResult Crear()
        {
            if (Session["user"] != null)
            {
                MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
                PersonasFisicas pf = new PersonasFisicas();
                return View(pf);
            }
            else
            {
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index",
                });
            }
        }

        [HttpPost]
        public ActionResult Crear(FormCollection values)
        {
            DataTable resultados = new DataTable();
            MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
            PersonasFisicas pf = new PersonasFisicas();
            pf.Sincronizar(values);
            pf.Validacion();
            int resultado = 1;
            List<string> errores = pf.getErrores();
            if (errores.Count > 0)
            {
                ViewBag.Errores = errores;
                return View(pf);
            }
            else
            {
                resultados = mpf.Insertar(pf);
                string mensaje = resultados.Rows[0]["MENSAJEERROR"].ToString();
                if (Convert.ToInt32(resultados.Rows[0]["ERROR"].ToString()) < 0)
                {
                    errores.Add(mensaje);
                    ViewBag.Errores = errores;
                    return View(pf);
                }
                else
                {
                    return RedirectToAction("Index", "Admin", new { resultado = resultado });
                }
            }
        }

        public ActionResult Actualizar(int id)
        {
            if (Session["user"] != null)
            {
                MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
                pfc = mpf.Encontrar(id);
                return View(pfc);
            }
            else
            {
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index",
                });
            }
        }

        [HttpPost]
        public ActionResult Actualizar(FormCollection values)
        {
            DataTable resultados = new DataTable();
            MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
            int id = int.Parse(values["personaFisica[id]"]);
            pfc.Sincronizar(values);
            PersonasFisicas pf = pfc.Sincronizar();
            pf.Validacion();
            List<string> errores = pf.getErrores();
            if(errores.Count > 0)
            {
                ViewBag.Errores = errores;
                return View(pfc);
            } else
            {
                resultados = mpf.Actualizar(id, pf);
                string mensaje = resultados.Rows[0]["MENSAJEERROR"].ToString();
                if (Convert.ToInt32(resultados.Rows[0]["ERROR"].ToString()) < 0)
                {
                    errores.Add(mensaje);
                    ViewBag.Errores = errores;
                    return View(pfc);
                }
                else
                {
                    int resultado = 2;
                    return RedirectToAction("Index", "Admin", new { resultado = resultado });
                }
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            DataTable resultados = new DataTable();
            MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
            resultados = mpf.Eliminar(id);
            List<string> errores = new List<string>();
            int resultado = 0;
            string mensaje = "Registro desactivado";
            if (!(resultados.Rows.Count == 0))
            {
                mensaje = resultados.Rows[0]["MENSAJEERROR"].ToString();
                if (Convert.ToInt32(resultados.Rows[0]["ERROR"].ToString()) < 0)
                {
                    errores.Add(mensaje);
                    ViewBag.Errores = errores;
                    resultado = 4;
                    return RedirectToAction("Index", "Admin", new { resultado = resultado });
                }
            }
            resultado = 3;
            return RedirectToAction("Index", "Admin", new { resultado = resultado });
        }
    }
}