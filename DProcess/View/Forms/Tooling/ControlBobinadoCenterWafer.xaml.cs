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
    /// Lógica de interacción para ControlBobinadoCenterWafer.xaml
    /// </summary>
    public partial class ControlBobinadoCenterWafer : UserControl, IControlTooling
    {
        public ControlBobinadoCenterWafer()
        {
            InitializeComponent();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }

        public int Guardar(string codigo)
        {
            // Mandamos llamar al método para insertar el objeto y retornamos el resultado
            return DataManager.InsertarBobinadoCenterWafer(codigo, Convert.ToDouble(tbx_Dia_A_Min.Text), Convert.ToDouble(tbx_Dia_A_Max.Text), Convert.ToDouble(tbx_Wire_Width.Text), Convert.ToString(tbx_Detalle.Text), Convert.ToDouble(tbx_Dia_B.Text), Convert.ToDouble(tbx_F_Width.Text));
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public void InicializaCampos(string codigoHerramental)
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(tbx_Dia_A_Min.Text) & !string.IsNullOrEmpty(tbx_Dia_A_Max.Text) & !string.IsNullOrEmpty(tbx_Wire_Width.Text) & !string.IsNullOrEmpty(tbx_Detalle.Text) & !string.IsNullOrEmpty(tbx_Dia_B.Text) & !string.IsNullOrEmpty(tbx_F_Width.Text))
                return true;
            else
                return false;
        }

        public bool ValidaRangos()
        {
            double diaamin, diaamax;

            diaamin = Convert.ToDouble(tbx_Dia_A_Min.Text);
            diaamax = Convert.ToDouble(tbx_Dia_A_Max.Text);

            if (diaamin < diaamax)
                return true;
            else
                return false;            
        }

        /// <summary>
        /// Valida que solo se introduzcan numeros
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
        /// Valida la tecla espacio
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
