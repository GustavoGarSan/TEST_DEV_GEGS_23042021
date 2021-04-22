using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webappPrueba.Models
{
    public class PersonasFisicasCompleto
    {
        public int id { get; set; }
        public string fechaRegistro { get; set; }
        public string fechaActualizacion { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string rfc { get; set; }
        public string fechaNacimiento { get; set; }
        public int usuarioAgrega { get; set; }
        public bool activo { get; set; }

        public PersonasFisicas Sincronizar()
        {
            PersonasFisicas pf = new PersonasFisicas()
            {
                nombre = this.nombre,
                apellidoPaterno = this.apellidoPaterno,
                apellidoMaterno = this.apellidoMaterno,
                rfc = this.rfc,
                fechaNacimiento = this.fechaNacimiento,
                usuarioAgrega = this.usuarioAgrega
            };
            return pf;
        }

        public void Sincronizar(FormCollection values)
        {
            this.nombre = values["personaFisica[nombre]"];
            this.apellidoPaterno = values["personaFisica[apellidoPaterno]"];
            this.apellidoMaterno = values["personaFisica[apellidoMaterno]"];
            this.rfc = values["personaFisica[rfc]"];
            this.fechaNacimiento = values["personaFisica[fechaNacimiento]"];
            this.usuarioAgrega = int.Parse(values["personaFisica[usuarioAgrega]"]);
        }
    }
}