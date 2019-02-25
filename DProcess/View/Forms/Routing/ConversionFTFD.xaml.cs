using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View.Forms.Routing
{
    /// <summary>
    /// Lógica de interacción para ConversionFTFD.xaml
    /// </summary>
    public partial class ConversionFTFD : MetroWindow
    {
        /// <summary>
        /// Método que valida la entrada del textbox solo sea número flotante
        /// </summary>
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            string text = textBox.Text;
            Regex regex = new Regex("[^0-9.]+");

            //Valida si el caracter es un número o punto, si el texto ya contiene un punto no permite escribir otro punto
            if (!regex.IsMatch(e.Text))
            {
                //Si el objeto recibido es un punto
                if (e.Text.Equals("."))
                {
                    if (text.Contains("."))
                        e.Handled = true;
                    else
                        e.Handled = false;
                }
                else
                {
                    e.Handled = regex.IsMatch(e.Text);
                }
            }
            else
            {
                //Si no es un número o punto no escribe el caracter
                e.Handled = true;
            }
        }

        /// <summary>
        /// Método que valida si la tecla recibida es un espacio
        /// </summary>
        private void KeyValidation(object sender, KeyEventArgs e)
        {
            //si la tecla presionada es un espacio no la escribe
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        public ConversionFTFD()
        {
            InitializeComponent();
        }
    }
}
