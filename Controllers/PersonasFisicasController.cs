using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using webappPrueba.Models;

namespace webappPrueba.Controllers
{
    public class PersonasFisicasController : ApiController
    {
        // GET: api/PersonasFisicas
        public IEnumerable<PersonasFisicasCompleto> Get()
        {
            MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
            List<PersonasFisicasCompleto> resultados = new List<PersonasFisicasCompleto>();
            resultados = mpf.Listar();
            return resultados;
        }

        // GET: api/PersonasFisicas/5
        [ResponseType(typeof(PersonasFisicasCompleto))]
        public IHttpActionResult Get(int id)
        {
            MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
            PersonasFisicasCompleto resultado = new PersonasFisicasCompleto();
            resultado = mpf.Encontrar(id);
            if(resultado.id == 0 || resultado.nombre == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(resultado);
            }
        }

        // POST: api/PersonasFisicas
        [ResponseType(typeof(string))]
        public IHttpActionResult Post(PersonasFisicas obj)
        {
            MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
            DataTable resultados = new DataTable();
            resultados = mpf.Insertar(obj);
            string mensaje = resultados.Rows[0]["MENSAJEERROR"].ToString();
            if (Convert.ToInt32(resultados.Rows[0]["ERROR"].ToString()) < 0)
            {
                return BadRequest(mensaje);
            }
            return Ok(mensaje);
        }

        // PUT: api/PersonasFisicas/5
        [ResponseType(typeof(string))]
        public IHttpActionResult Put(int id, [FromBody]PersonasFisicas obj)
        {
            MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
            DataTable resultados = new DataTable();
            resultados = mpf.Actualizar(id, obj);
            string mensaje = resultados.Rows[0]["MENSAJEERROR"].ToString();
            if (Convert.ToInt32(resultados.Rows[0]["ERROR"].ToString()) < 0)
            {
                return BadRequest(mensaje);
            }
            return Ok(mensaje);
        }

        // DELETE: api/PersonasFisicas/5
        [ResponseType(typeof(string))]
        public IHttpActionResult Delete(int id)
        {
            MantenimientoPersonasFisicas mpf = new MantenimientoPersonasFisicas();
            DataTable resultados = new DataTable();
            resultados = mpf.Eliminar(id);
            string mensaje = "Registro desactivado";
            if(!(resultados.Rows.Count == 0))
            {
                mensaje = resultados.Rows[0]["MENSAJEERROR"].ToString();
                if (Convert.ToInt32(resultados.Rows[0]["ERROR"].ToString()) < 0)
                {
                    return BadRequest(mensaje);
                }
            }
            return Ok(mensaje);
        }
    }
}
