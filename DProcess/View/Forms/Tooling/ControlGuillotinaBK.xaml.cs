using Model;
using Model.Interfaces;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Forms.Tooling
{
    /// <summary>
    /// Lógica de interacción para ControlGuillotinaBK.xaml
    /// </summary>
    public partial class ControlGuillotinaBK : UserControl, IControlTooling
    {
        Herramental obj;

        public ControlGuillotinaBK()
        {
            InitializeComponent();
            obj = new Herramental();
        }

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();
            PropiedadCadena Pmedida = new PropiedadCadena();
            PropiedadCadena Pwidth = new PropiedadCadena();
            PropiedadCadena psobrem = new PropiedadCadena();

            obj.Codigo = codigo;
            Pmedida.Valor = medidaN.Text;
            obj.PropiedadesCadena.Add(Pmedida);

            Pwidth.Valor = width.Text;
            obj.PropiedadesCadena.Add(Pwidth);

            psobrem.Valor = SobreMedida.Text;
            obj.PropiedadesCadena.Add(psobrem);

            return DataManager.SetGuillotinaBK(obj);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(medidaN.Text) & !string.IsNullOrEmpty(width.Text) & !string.IsNullOrEmpty(SobreMedida.Text))
                return true;
            else
                return false;
        }

        public bool ValidaRangos()
        {
            return true;
        }

        public int Update()
        {
            Herramental herram = new Herramental();
            PropiedadCadena Pmedida = new PropiedadCadena();
            PropiedadCadena Pwidth = new PropiedadCadena();
            PropiedadCadena psobrem = new PropiedadCadena();

            herram.Codigo = obj.Codigo;
            herram.idHerramental = obj.idHerramental;

            Pmedida.Valor = medidaN.Text;
            herram.PropiedadesCadena.Add(Pmedida);

            Pwidth.Valor = width.Text;
            herram.PropiedadesCadena.Add(Pwidth);

            psobrem.Valor = SobreMedida.Text;
            herram.PropiedadesCadena.Add(psobrem);

            return DataManager.UpdateGuillotinaBK(herram);
        }

        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfoGuillotinaBK(codigoHerramental);
            medidaN.Text = obj.PropiedadesCadena[0].Valor;
            width.Text = obj.PropiedadesCadena[1].Valor;
            SobreMedida.Text = obj.PropiedadesCadena[2].Valor;
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
            Regex regex = new Regex("[^0-9./]+");
            //Valida si el caracter es un número o punto, si el texto ya contiene un punto no permite escribir otro punto.
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
                //Si no es un número o punto no escribe el caracter.
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
