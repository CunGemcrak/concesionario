using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concesionario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }


        ServiceReference1.WebService1SoapClient ws = new ServiceReference1.WebService1SoapClient();
        private void button2_Click(object sender, EventArgs e)
        {
            PlacaTxt.Text = "";
            MarcaTxt.Text = "";
            ModeloTxt.Text = "";
            ColorTxt.Text = "";

        }



        private void button1_Click(object sender, EventArgs e)
        {

            

            string datos = ws.Formulario(
                PlacaTxt.Text,
                MarcaTxt.Text,
                ModeloTxt.Text,
                ColorTxt.Text).ToString();
           
            if (datos == "false")
            {
                MessageBox.Show("Se deben llenar todos los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else {
                MessageBox.Show("Datos Guardados Correctamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }


        }







        private void Modelo_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string textoNumerico = "";

            if (textBox.Name == "ModeloTxt")
            {
                label14.Text = textBox.Text;
            }



            if (textBox.Text.Length > 4)
            {
                // Mostrar un mensaje de advertencia
                

                // Cortar el texto para que tenga solo 10 caracteres
                textBox.Text = textBox.Text.Substring(0, 4);

                // Establecer el cursor al final del TextBox
                textBox.SelectionStart = textBox.Text.Length;
                MessageBox.Show("El modelo no puede tener más de 4 caracteres", "Precaución", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {





                foreach (char caracter in textBox.Text)
                {
                    // Verificar si el carácter es un dígito
                    if (!char.IsDigit(caracter))
                    {
                        // Si es un dígito, añadirlo al texto numerico
                        textBox.Text = textoNumerico;
                        textBox.SelectionStart = textBox.Text.Length;

                        MessageBox.Show("Solo se adminten Números", "Preacusion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        textoNumerico += caracter;
                    }
                }
                
            }
        }







        private void PlacaTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
           

            if (textBox.Name == "PlacaTxt")
            {
                label14.Text = textBox.Text;
            }


            if (textBox.Text.Length > 6)
            {
              
                textBox.Text = textBox.Text.Substring(0, 6);

               
                textBox.SelectionStart = textBox.Text.Length;
                MessageBox.Show("No se pueden superar los 6 Caracteres", "Preacusion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

      

        private void MarcaTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;


            if (textBox.Name == "MarcaTxt")
            {
                label14.Text = textBox.Text;
            }


            if (textBox.Text.Length > 15)
            {

                textBox.Text = textBox.Text.Substring(0, 15);


                textBox.SelectionStart = textBox.Text.Length;
                MessageBox.Show("No se pueden superar los 15 Caracteres", "Preacusion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        private void ColorTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;


            if (textBox.Name == "ColorTxt")
            {
                label14.Text = textBox.Text;
            }


            if (textBox.Text.Length > 10)
            {

                textBox.Text = textBox.Text.Substring(0, 10);


                textBox.SelectionStart = textBox.Text.Length;
                MessageBox.Show("No se pueden superar los 10 Caracteres", "Preacusion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Actualizar datos", "Actualizar", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            if (result == DialogResult.OK)
            {
                MessageBox.Show("Datos Actualizados", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Se Cancelo Proceso de Actualizacion", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
