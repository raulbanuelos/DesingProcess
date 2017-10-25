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
    /// Lógica de interacción para ControlCoilFR.xaml
    /// </summary>
    public partial class ControlCoilFR : UserControl, IControlTooling
    {
        Herramental herramental;
        public ControlCoilFR()
        {
            InitializeComponent();
        }

        public int Guardar(string codigo)
        {
            Coil obj = new Coil();

            obj.codigo = codigo;
            obj.code = code.Text;
            obj.dimA = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimB = double.Parse(dimB.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimC = double.Parse(dimC.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimD = double.Parse(dimD.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_min = double.Parse(WMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_max = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);

            return DataManager.SetCOIL_FEED_ROLLER(obj);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(code.Text) & !string.IsNullOrWhiteSpace(code.Text) & !string.IsNullOrEmpty(dimA.Text) & !string.IsNullOrWhiteSpace(dimA.Text)
                    & !string.IsNullOrWhiteSpace(dimB.Text) & !string.IsNullOrEmpty(dimB.Text) & !string.IsNullOrWhiteSpace(dimC.Text) & !string.IsNullOrEmpty(dimC.Text) & !string.IsNullOrEmpty(dimD.Text)
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

            if (wmin < wmax )
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

        public int Update()
        {
            Coil coil = new Coil();

            coil.codigo = herramental.Codigo;
            coil.ID = herramental.idHerramental;
            coil.code = code.Text;
            coil.dimA = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            coil.dimB = double.Parse(dimB.Text, CultureInfo.InvariantCulture.NumberFormat);
            coil.dimC = double.Parse(dimC.Text, CultureInfo.InvariantCulture.NumberFormat);
            coil.dimD = double.Parse(dimD.Text, CultureInfo.InvariantCulture.NumberFormat);
            coil.wire_width_min = double.Parse(WMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            coil.wire_width_max = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);

            return DataManager.UpdateCOIL_FEED_ROLLER(coil);
        }

        public void InicializaCampos(string codigoHerramental)
        {
            herramental = DataManager.GetInfoCOIL_Feed_Roller(codigoHerramental);

            code.Text = herramental.PropiedadesCadena[0].Valor;
            dimA.Text = Convert.ToString(herramental.Propiedades[0].Valor);
            dimB.Text = Convert.ToString(herramental.Propiedades[1].Valor);
            dimC.Text = Convert.ToString(herramental.Propiedades[2].Valor);
            dimD.Text = Convert.ToString(herramental.Propiedades[3].Valor);
            WMin.Text = Convert.ToString(herramental.Propiedades[4].Valor);
            WMax.Text = Convert.ToString(herramental.Propiedades[5].Valor);
        }

        public int Delete()
        {
            return DataManager.DeleteCOIL_FEED_ROLLER(herramental.idHerramental);
        }

    }
}
