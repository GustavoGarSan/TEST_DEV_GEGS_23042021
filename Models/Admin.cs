using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webappPrueba.Models
{
    public class Admin
    {
        public string email { get; set; }
        public string password { get; set; }

        public List<string> Errores = new List<string>();

        public Admin(string email = "", string password = "")
        {
            this.email = email;
            this.password = password;
        }
    }
}