using System.Collections.Generic;
using System.Web.Mvc;

namespace webappPrueba.Models
{
    public class PersonasFisicas
    {
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string rfc { get; set; }
        public string fechaNacimiento { get; set; }
        public int usuarioAgrega { get; set; }

        public PersonasFisicas()
        {
            nombre = "";
            apellidoPaterno = "";
            apellidoMaterno = "";
            rfc = "";
            fechaNacimiento = System.DateTime.Now.ToString("yyyy-MM-dd");
            usuarioAgrega = 0;
        }

        public List<string> errores = new List<string>();

        public void Validacion()
        {
            if(this.nombre == "" || this.nombre == null)
            {
                this.errores.Add("El nombre es obligatorio");
            }
            if (this.apellidoPaterno == "" || this.apellidoPaterno == null)
            {
                this.errores.Add("El Apellido Paterno es obligatorio");
            }
            if (this.apellidoMaterno == "" || this.apellidoMaterno == null)
            {
                this.errores.Add("El Apellido Materno es obligatorio");
            }
            if (this.rfc == "" || this.rfc == null)
            {
                this.errores.Add("El RFC es obligatorio");
            }
            if (this.fechaNacimiento == "" || this.fechaNacimiento == null)
            {
                this.errores.Add("La Fecha de Nacimiento es obligatoria o no tiene un formato valido");
            }
        }

        public List<string> getErrores()
        {
            return this.errores;
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