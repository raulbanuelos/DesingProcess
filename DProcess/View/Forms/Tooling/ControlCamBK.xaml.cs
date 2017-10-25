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
    /// Lógica de interacción para ControlCamBK.xaml
    /// </summary>
    public partial class ControlCamBK : UserControl, IControlTooling
    {
        Herramental herram = new Herramental();
        public ControlCamBK()
        {
            InitializeComponent();
        }

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();

            obj.Codigo = codigo;

            Propiedad A = new Propiedad();
            Propiedad B = new Propiedad();
            PropiedadCadena Pdetalle = new PropiedadCadena();

            A.Valor = double.Parse(dima.Text, CultureInfo.InvariantCulture.NumberFormat);
            B.Valor = double.Parse(dimb.Text, CultureInfo.InvariantCulture.NumberFormat);
            Pdetalle.Valor = detalle.Text;

            obj.Propiedades.Add(A);
            obj.Propiedades.Add(B);
            obj.PropiedadesCadena.Add(Pdetalle);

            return DataManager.SetCamBK(obj);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public void InicializaCampos(string codigoHerramental)
        {
            herram = DataManager.GetInfoCamBK(codigoHerramental);

            detalle.Text = herram.PropiedadesCadena[0].Valor;
            dima.Text = Convert.ToString(herram.Propiedades[0].Valor);
            dimb.Text = Convert.ToString(herram.Propiedades[1].Valor);
        }

        public int Update()
        {
            Herramental obj = new Herramental();

            obj.Codigo = herram.Codigo;
            obj.idHerramental = herram.idHerramental;
            Propiedad A = new Propiedad();
            Propiedad B = new Propiedad();
            PropiedadCadena Pdetalle = new PropiedadCadena();

            A.Valor = double.Parse(dima.Text, CultureInfo.InvariantCulture.NumberFormat);
            B.Valor = double.Parse(dimb.Text, CultureInfo.InvariantCulture.NumberFormat);
            Pdetalle.Valor = detalle.Text;

            obj.Propiedades.Add(A);
            obj.Propiedades.Add(B);
            obj.PropiedadesCadena.Add(Pdetalle);

            return DataManager.UpdateCamBK(obj);
        }

        public int Delete()
        {
            return DataManager.DeleteCamBK(herram.idHerramental);
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(detalle.Text) & !string.IsNullOrEmpty(dima.Text) & !string.IsNullOrEmpty(dimb.Text))
                return true;
            else
                return false;
        }

        public bool ValidaRangos()
        {
            return true;
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

    }
}
