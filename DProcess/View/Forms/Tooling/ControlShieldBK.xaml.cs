using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para ControlShieldBK.xaml
    /// </summary>
    public partial class ControlShieldBK : UserControl, IControlTooling
    {
        Herramental herram = new Herramental();
        public ControlShieldBK()
        {
            InitializeComponent();
        }

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();

            obj.Codigo = codigo;

            Propiedad fMin = new Propiedad();
            Propiedad fMax = new Propiedad();
            PropiedadCadena Pdetalle = new PropiedadCadena();
            PropiedadCadena PFraccMin = new PropiedadCadena();
            PropiedadCadena PFraccMax = new PropiedadCadena();

            fMin.Valor = double.Parse(fractMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            fMax.Valor = double.Parse(fractMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            Pdetalle.Valor = detalle.Text;
            PFraccMax.Valor = fraccMax.Text;
            PFraccMin.Valor = fraccMin.Text;

            obj.Propiedades.Add(fMin);
            obj.Propiedades.Add(fMin);
            obj.PropiedadesCadena.Add(Pdetalle);
            obj.PropiedadesCadena.Add(PFraccMin);
            obj.PropiedadesCadena.Add(PFraccMax);

            return DataManager.SetShieldBK(obj);
        }

        public void Inicializa()
        {
             InitializeComponent();
        }

        public void InicializaCampos(string codigoHerramental)
        {
            herram = DataManager.GetInfoShielBK(codigoHerramental);

            fractMin.Text= Convert.ToString(herram.Propiedades[0].Valor);
            fractMax.Text = Convert.ToString(herram.Propiedades[1].Valor);
            detalle.Text = herram.PropiedadesCadena[2].Valor;
            fraccMin.Text= herram.PropiedadesCadena[0].Valor;
            fraccMax.Text = herram.PropiedadesCadena[1].Valor;
        }

        public int Update()
        {
            Herramental obj = new Herramental();

            obj.Codigo = herram.Codigo;
            obj.idHerramental = herram.idHerramental;
            Propiedad fMin = new Propiedad();
            Propiedad fMax = new Propiedad();
            PropiedadCadena Pdetalle = new PropiedadCadena();
            PropiedadCadena PFraccMin = new PropiedadCadena();
            PropiedadCadena PFraccMax = new PropiedadCadena();

            fMin.Valor = double.Parse(fractMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            fMax.Valor = double.Parse(fractMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            Pdetalle.Valor = detalle.Text;
            PFraccMax.Valor = fraccMax.Text;
            PFraccMin.Valor = fraccMin.Text;

            obj.Propiedades.Add(fMin);
            obj.Propiedades.Add(fMin);
            obj.PropiedadesCadena.Add(Pdetalle);
            obj.PropiedadesCadena.Add(PFraccMin);
            obj.PropiedadesCadena.Add(PFraccMax);

            return DataManager.SetShieldBK(obj);
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(detalle.Text) & !string.IsNullOrEmpty(fraccMin.Text) & !string.IsNullOrEmpty(fraccMax.Text)
                & !string.IsNullOrEmpty(fractMin.Text) & !string.IsNullOrEmpty(fractMax.Text))
                return true;
            else
                return false;
        }

        public bool ValidaRangos()
        {
            double min = double.Parse(fractMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            double max = double.Parse(fractMax.Text, CultureInfo.InvariantCulture.NumberFormat);

            if (min < max)
                return true;
            else return false;
        }

        /// <summary>
        /// Método que válida la entrada del textbox sólo sea número flotante.
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
                e.Handled = true;
            }
        }

        /// <summary>
        /// Método que valída si la tecla recibida es un espacio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyValidation(object sender, KeyEventArgs e)
        {
            //si la tecla presionada es un espacio no la escribe
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        /// <summary>
        /// Valída fracciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FraccValidation(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            string text = textBox.Text;
            Regex regex = new Regex("[^0-9/]+");
            e.Handled = regex.IsMatch(e.Text);
              
        }
    }
}
