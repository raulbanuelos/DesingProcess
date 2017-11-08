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
    /// Lógica de interacción para ControlCamisaMoly.xaml
    /// </summary>
    public partial class ControlCamisaMoly : UserControl, IControlTooling
    {
        Herramental herram;

        public ControlCamisaMoly()
        {
            InitializeComponent();
        }

        public int Delete()
        {
            return DataManager.DeleteCamisaMoly(herram.idHerramental);
        }

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();
            Propiedad A = new Propiedad();

            obj.Codigo = codigo;
            obj.Plano = plano.Text;

            A.Valor = double.Parse(dima.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.Propiedades.Add(A);

            return DataManager.SetCamisaMoly(obj);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public void InicializaCampos(string codigoHerramental)
        {
            herram = DataManager.GetInfoCamisaMoly(codigoHerramental);

            if (herram.Codigo != null)
            {
                plano.Text = herram.Plano;
                dima.Text = Convert.ToString(herram.Propiedades[0].Valor);
            }
        }

        public int Update()
        {
            Herramental obj = new Herramental();
            Propiedad A = new Propiedad();

            obj.idHerramental = herram.idHerramental;
            obj.Plano = plano.Text;

            A.Valor = double.Parse(dima.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.Propiedades.Add(A);

            return DataManager.UpdateCamisaMoly(obj);
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(plano.Text) & !string.IsNullOrEmpty(dima.Text))
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
