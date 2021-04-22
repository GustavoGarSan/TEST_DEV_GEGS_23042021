using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webappPrueba.Models
{
    public class MantenimientoAdministradores
    {
        private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["administracion"].ToString();
            con = new SqlConnection(constr);
        }
        public Admin existeUsuario(Admin obj)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("SELECT * FROM Usuarios WHERE Email=@email", con);
            comando.Parameters.Add("@email", SqlDbType.VarChar);
            comando.Parameters["@email"].Value = obj.email;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            Admin usuario = new Admin();
            if (registros.Read())
            {
                usuario.email = registros["Email"].ToString();
                usuario.password = registros["Password"].ToString();
            }
            con.Close();
            return usuario;
        }

        public static bool comprobarPassword(Admin usuario, Admin registro)
        {
            registro.password = registro.password.Trim();
            if (usuario.password != registro.password){
                usuario.Errores.Add("El password es incorrecto");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}