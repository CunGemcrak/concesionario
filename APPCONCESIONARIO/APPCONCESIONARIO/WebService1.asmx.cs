using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;



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
