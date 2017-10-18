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
    /// Lógica de interacción para ControlBushing.xaml
    /// </summary>
    public partial class ControlBushing : UserControl, IControlTooling
    {
        ObservableCollection<string> ListaDim;

        public ControlBushing()
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
            Herramental obj = new Herramental();

            obj.Codigo = codigo;
            obj.Plano = Plano.Text;
            Propiedad MedidaNominal = new Propiedad();
            PropiedadCadena DimB = new PropiedadCadena();

            DimB.Valor = comboB.SelectedValuePath;
            MedidaNominal.Valor = double.Parse(medidaN.Text, CultureInfo.InvariantCulture.NumberFormat);

            obj.Propiedades.Add(MedidaNominal);
            obj.PropiedadesCadena.Add(DimB);
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Inicializa()
        {
            InitializeComponent();
            ListaDim = new ObservableCollection<string>();
            ListaDim.Add("4°");
            ListaDim.Add("9°");
            ListaDim.Add("15°");

            comboB.ItemsSource=ListaDim;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if (comboB.SelectedValue != null & !string.IsNullOrEmpty(medidaN.Text) & !string.IsNullOrEmpty(Plano.Text))
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
            return true;
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
