using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Services;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Dynamic;



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

        [WebMethod]// Metodo para crear la conexion 
        private MySqlConnection Conexion()
        {
            string connectionString = "Server=localhost;Port=3306;Database=concesionario;Uid=root;Pwd='';";
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        [WebMethod]//metodo para crear la bd
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




        [WebMethod] // metodo para crear la tabla 
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

        [WebMethod] //guardar datos 
        public string SaveAuto(string placa, string marca, string modelo, string color) {
            MySqlConnection conexion = Conexion();

            try {
                conexion.Open();

                string query = "INSERT INTO auto (placa, marca, modelo, color) VALUES (@placa, @marca, @modelo, @color)";
                MySqlCommand command = new MySqlCommand(query, conexion);
                command.Parameters.AddWithValue("@placa", placa);
                command.Parameters.AddWithValue("@marca", marca);
                command.Parameters.AddWithValue("@modelo", modelo);
                command.Parameters.AddWithValue("@color", color);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return "Auto guardado correctamente";
                }
                else
                {
                    return "No se pudo guardar el auto";
                }

            } catch (MySqlException ex) {

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
        public string BuscarAuto(string criterioBusqueda)
        {
            MySqlConnection conexion = Conexion();
            try
            {
                conexion.Open();
                string query = "SELECT * FROM auto WHERE placa = @criterioBusqueda";
                MySqlCommand command = new MySqlCommand(query, conexion);
                command.Parameters.AddWithValue("@criterioBusqueda", criterioBusqueda);

                MySqlDataReader reader = command.ExecuteReader();

                // Verificar si se encontraron resultados
                if (reader.HasRows)
                {
                    List<dynamic> autos = new List<dynamic>();
                    while (reader.Read())
                    {
                        // Construye un objeto dinámico para representar los datos de la fila actual
                        dynamic auto = new ExpandoObject();
                        auto.Placa = reader["placa"].ToString();
                        auto.Marca = reader["marca"].ToString();
                        auto.Modelo = Convert.ToInt32(reader["modelo"]);
                        auto.Color = reader["color"].ToString();

                        // Agrega el objeto dinámico a la lista
                        autos.Add(auto);
                    }

                    // Serializa la lista de objetos dinámicos a JSON
                    string resultadoEnJson = JsonConvert.SerializeObject(autos);

                    // Devuelve el JSON resultante
                    return resultadoEnJson;


                }
                else {
                    return "null";
                }
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
               string dato =  SaveAuto(placa, marca, modelo, color);
               return dato;
                     }



        }


    }
}
