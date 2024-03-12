using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;



namespace APPCONCESIONARIO
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string CrearBaseDatos()
        {
            string connectionString = "Server=localhost;Port=3306;Database=;Uid=root;Pwd=;";
            MySqlConnection conexion = new MySqlConnection(connectionString);
            try
            {
                conexion.Open();
                string query = "CREATE DATABASE IF NOT EXISTS concesionario;";
                MySqlCommand command = new MySqlCommand(query, conexion);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return "Base de datos 'concesionario' creada correctamente";
                }
                else
                {
                    return "La base de datos 'concesionario' ya existe";
                }
            }
            catch (MySqlException ex)
            {
                return "ERROR: No se pudo conectar con el servidor de la base de datos: " + ex.Message;
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }


        [WebMethod]
        private MySqlConnection Conexion() {
            string connectionString = "Server=localhost;Port=3306;Database=concesionario;Uid=root;Pwd='';";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        [WebMethod]
        public string CrearTable()
        {
            string mensaje;
            MySqlConnection conexion = Conexion();

            try
            {
                conexion.Open();
                string queryCheck = "SHOW TABLES LIKE 'auto'";
                MySqlCommand commandCheck = new MySqlCommand(queryCheck, conexion);
                object result = commandCheck.ExecuteScalar();

                if (result != null)
                {
                    mensaje = "La tabla 'auto' ya existe";
                }
                else
                {
                    string queryCreate = "CREATE TABLE auto(placa VARCHAR(6) PRIMARY KEY, marca VARCHAR(15) NOT NULL,modelo INT NOT NULL, color VARCHAR(10) NOT NULL)";
                    MySqlCommand commandCreate = new MySqlCommand(queryCreate, conexion);
                    int rowsAffected = commandCreate.ExecuteNonQuery();
                    
                        mensaje = "Tabla 'auto' creada correctamente";
                    
                }

                return mensaje;
            }
            catch (MySqlException ex)
            {
                return "ERROR: no se pudo conectar con la base de datos: " + ex.Message;
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public Boolean Validar_numero(string texto)
        {
            bool valor = int.TryParse(texto, out _);
            if (!valor) {
                return false;
                    }
            else {
                return true;
                    
            }


        }


        [WebMethod]
        public string Formulario(string placa, string  marca, string modelo, string color)
        {

            if (placa.Length == 0 || marca.Length == 0 || modelo.Length == 0 || color.Length == 0)
            {

                return "false";
            }
            else {

               return "true";
                     }



        }


    }
}
