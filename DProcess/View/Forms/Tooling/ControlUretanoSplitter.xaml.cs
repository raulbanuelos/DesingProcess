using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Forms.Tooling
{
    /// <summary>
    /// Lógica de interacción para ControlUretanoSplitter.xaml
    /// </summary>
    public partial class ControlUretanoSplitter : UserControl, IControlTooling
    {
        #region Propiedades
        
        Herramental obj;
        #endregion
        public ControlUretanoSplitter()
        {
            InitializeComponent();
        }

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();

            obj.Codigo = codigo;

            Propiedad DMin = new Propiedad();
            Propiedad DMax = new Propiedad();
            PropiedadCadena Pmedidas = new PropiedadCadena();
            PropiedadCadena PColor = new PropiedadCadena();

            DMin.Valor = double.Parse(dimMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            DMax.Valor = double.Parse(dimMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            Pmedidas.Valor = medidas.Text;
            PColor.Valor = color.Text;

            obj.PropiedadesCadena.Add(Pmedidas);
            obj.PropiedadesCadena.Add(PColor);
            obj.Propiedades.Add(DMin);
            obj.Propiedades.Add(DMax);

            return DataManager.SetUretanoSplitter(obj);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(dimMin.Text) & !string.IsNullOrEmpty(dimMax.Text) &  !string.IsNullOrEmpty(medidas.Text) & !string.IsNullOrEmpty(color.Text))
                return true;
            else
                return false;

        }

        public bool ValidaRangos()
        {
            double dmin, dmax;
            dmin = double.Parse(dimMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            dmax = double.Parse(dimMax.Text, CultureInfo.InvariantCulture.NumberFormat);

            if (dmin < dmax)
                return true;
            else
                return false;
        }

        public int Update()
        {
            Herramental herram = new Herramental();

            herram.Codigo = obj.Codigo;
            herram.idHerramental = obj.idHerramental;

            Propiedad DMin = new Propiedad();
            Propiedad DMax = new Propiedad();
            PropiedadCadena Pmedidas = new PropiedadCadena();
            PropiedadCadena PColor = new PropiedadCadena();

            DMin.Valor = double.Parse(dimMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            DMax.Valor = double.Parse(dimMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            Pmedidas.Valor = medidas.Text;
            PColor.Valor = color.Text;

            herram.PropiedadesCadena.Add(Pmedidas);
            herram.PropiedadesCadena.Add(PColor);
            herram.Propiedades.Add(DMin);
            herram.Propiedades.Add(DMax);

            return DataManager.UpdateUretanoSplitter(herram);
        }

        public void InicializaCampos(string codigoHerramental)
        {
           obj = DataManager.GetInfoUretanoSplitter(codigoHerramental);

            dimMin.Text = Convert.ToString(obj.Propiedades[0].Valor);
            dimMax.Text = Convert.ToString(obj.Propiedades[1].Valor);
            medidas.Text = obj.PropiedadesCadena[0].Valor;
            color.Text= obj.PropiedadesCadena[1].Valor;
        }
        /// <summary>
        /// Método que válida la entrada del textbox sólo sea número flotante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyValidation(object sender, KeyEventArgs e)
        {
            //si la tecla presionada es un espacio no la escribe
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}
