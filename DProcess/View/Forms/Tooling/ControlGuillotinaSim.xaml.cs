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
    /// Lógica de interacción para ControlGuillotinaSim.xaml
    /// </summary>
    public partial class ControlGuillotinaSim : UserControl, IControlTooling
    {
        Herramental herram;
        public ControlGuillotinaSim()
        {
            InitializeComponent();
        }

        public int Delete()
        {
            return DataManager.DeleteGuillotinaSim(herram.idHerramental);
        }

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();
            Propiedad A = new Propiedad();
            Propiedad Min = new Propiedad();
            Propiedad Max = new Propiedad();
            PropiedadCadena CantAnillos = new PropiedadCadena();

            obj.Codigo = codigo;
            A.Valor = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            Min.Valor = double.Parse(wMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            Max.Valor = double.Parse(wMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            CantAnillos.Valor = anillos.Text;

            obj.Propiedades.Add(A);
            obj.Propiedades.Add(Min);
            obj.Propiedades.Add(Max);
            obj.PropiedadesCadena.Add(CantAnillos);

            return DataManager.SetGuillotinaSim(obj);
        }

        public void Inicializa()
        {
            InitializeComponent();
            herram = new Herramental();
        }

        public void InicializaCampos(string codigoHerramental)
        {
            herram = DataManager.GetInfoGuillotinaSim(codigoHerramental);

            if (herram.Codigo != null)
            {
                dimA.Text = Convert.ToString(herram.Propiedades[0].Valor);
                wMin.Text = Convert.ToString(herram.Propiedades[1].Valor);
                wMax.Text = Convert.ToString(herram.Propiedades[2].Valor);
                anillos.Text = herram.PropiedadesCadena[0].Valor;
            }
        }

        public int Update()
        {
            Herramental obj = new Herramental();
            Propiedad A = new Propiedad();
            Propiedad Min = new Propiedad();
            Propiedad Max = new Propiedad();
            PropiedadCadena CantAnillos = new PropiedadCadena();

            obj.idHerramental = herram.idHerramental;
            A.Valor = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            Min.Valor = double.Parse(wMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            Max.Valor = double.Parse(wMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            CantAnillos.Valor = anillos.Text;
            obj.Propiedades.Add(A);
            obj.Propiedades.Add(Min);
            obj.Propiedades.Add(Max);
            obj.PropiedadesCadena.Add(CantAnillos);
            return DataManager.UpdateGuillotinaSim(obj);
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(dimA.Text) && !string.IsNullOrEmpty(wMin.Text) && !string.IsNullOrEmpty(wMax.Text) && !string.IsNullOrEmpty(anillos.Text))
                return true;
            else
                return false;
        }

        public bool ValidaRangos()
        {
            double wmin, wmax;
            wmin = double.Parse(wMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            wmax = double.Parse(wMax.Text, CultureInfo.InvariantCulture.NumberFormat);

            if (wmin < wmax)
                return true;
            else
                return false;
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
        /// Sólo acepta número enteros, para el campo de anillos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntegerValidation(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            string text = textBox.Text;
            Regex regex = new Regex("[^0-9]+");
            //Valida si el caracter es un número o punto, si el texto ya contiene un punto no permite escribir otro punto
          
            e.Handled = regex.IsMatch(e.Text);
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
    }
}
