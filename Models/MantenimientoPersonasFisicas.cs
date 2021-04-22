using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webappPrueba.Models
{
    public class MantenimientoPersonasFisicas
    {
        private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["administracion"].ToString();
            con = new SqlConnection(constr);
        }

        public List<PersonasFisicasCompleto> Listar()
        {
            Conectar();
            List<PersonasFisicasCompleto> personasFisicas = new List<PersonasFisicasCompleto>();

            SqlCommand com = new SqlCommand("SELECT * FROM Tb_PersonasFisicas", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                PersonasFisicasCompleto personaFisica = new PersonasFisicasCompleto()
                {
                    id = int.Parse(registros["IdPersonaFisica"].ToString()),
                    fechaRegistro = registros["FechaRegistro"].ToString(),
                    fechaActualizacion = registros["FechaActualizacion"].ToString(),
                    nombre = registros["Nombre"].ToString(),
                    apellidoPaterno = registros["ApellidoPaterno"].ToString(),
                    apellidoMaterno = registros["ApellidoMaterno"].ToString(),
                    rfc = registros["RFC"].ToString(),
                    fechaNacimiento = registros["FechaNacimiento"].ToString(),
                    usuarioAgrega = int.Parse(registros["UsuarioAgrega"].ToString()),
                    activo = bool.Parse(registros["Activo"].ToString())

                };
                personasFisicas.Add(personaFisica);
            }
            con.Close();
            return personasFisicas;
        }

        public PersonasFisicasCompleto Encontrar(int id)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("SELECT * FROM Tb_PersonasFisicas WHERE IdPersonaFisica=@id", con);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = id;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            PersonasFisicasCompleto personaFisica = new PersonasFisicasCompleto();
            if (registros.Read())
            {
                personaFisica.id = int.Parse(registros["IdPersonaFisica"].ToString());
                personaFisica.fechaRegistro = registros["FechaRegistro"].ToString();
                personaFisica.fechaActualizacion = registros["FechaActualizacion"].ToString();
                personaFisica.nombre = registros["Nombre"].ToString();
                personaFisica.apellidoPaterno = registros["ApellidoPaterno"].ToString();
                personaFisica.apellidoMaterno = registros["ApellidoMaterno"].ToString();
                personaFisica.rfc = registros["RFC"].ToString();
                personaFisica.fechaNacimiento = registros["FechaNacimiento"].ToString();
                personaFisica.usuarioAgrega = int.Parse(registros["UsuarioAgrega"].ToString());
                personaFisica.activo = bool.Parse(registros["Activo"].ToString());
            }
            con.Close();
            return personaFisica;
        }

        public DataTable Insertar(PersonasFisicas obj)
        {
            Conectar();
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            try
            {
                SqlCommand comando = new SqlCommand("sp_AgregarPersonaFisica", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = obj.nombre;
                comando.Parameters.Add("@ApellidoPaterno", SqlDbType.VarChar).Value = obj.apellidoPaterno;
                comando.Parameters.Add("@ApellidoMaterno", SqlDbType.VarChar).Value = obj.apellidoMaterno;
                comando.Parameters.Add("@RFC", SqlDbType.VarChar).Value = obj.rfc;
                comando.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = obj.fechaNacimiento;
                comando.Parameters.Add("@UsuarioAgrega", SqlDbType.Int).Value = obj.usuarioAgrega;
                con.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public DataTable Actualizar(int id, PersonasFisicas obj)
        {
            Conectar();
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            try
            {
                SqlCommand comando = new SqlCommand("sp_ActualizarPersonaFisica", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@IdPersonaFisica", SqlDbType.Int).Value = id;
                comando.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = obj.nombre;
                comando.Parameters.Add("@ApellidoPaterno", SqlDbType.VarChar).Value = obj.apellidoPaterno;
                comando.Parameters.Add("@ApellidoMaterno", SqlDbType.VarChar).Value = obj.apellidoMaterno;
                comando.Parameters.Add("@RFC", SqlDbType.VarChar).Value = obj.rfc;
                comando.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = obj.fechaNacimiento;
                comando.Parameters.Add("@UsuarioAgrega", SqlDbType.Int).Value = obj.usuarioAgrega;
                con.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public DataTable Eliminar(int id)
        {
            Conectar();
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            try
            {
                SqlCommand comando = new SqlCommand("sp_EliminarPersonaFisica", con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@IdPersonaFisica", SqlDbType.Int).Value = id;
                con.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
    }
}