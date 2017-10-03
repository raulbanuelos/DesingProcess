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
    /// Lógica de interacción para ControlExternalGR3P_1.xaml
    /// </summary>
    public partial class ControlExternalGR3P_1 : UserControl, IControlTooling
    {
        public ControlExternalGR3P_1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int Guardar(string codigo)
        {
            Coil obj = new Coil();

            obj.codigo = codigo;
            obj.code = code.Text;
            obj.dimA = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimB= double.Parse(dimB.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimC = double.Parse(dimC.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_min = double.Parse(WMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_max = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);

            return DataManager.SetExternal_GR_3P_1(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Inicializa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(code.Text) & !string.IsNullOrEmpty(dimA.Text) & !string.IsNullOrEmpty(dimB.Text) & !string.IsNullOrEmpty(dimC.Text)
                     & !string.IsNullOrEmpty(WMin.Text) & !string.IsNullOrEmpty(WMax.Text))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            double wmin, wmax;
            wmin = double.Parse(WMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            wmax = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);

            if (wmin < wmax)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
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
        /// 
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
