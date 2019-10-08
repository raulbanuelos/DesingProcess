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
    /// Lógica de interacción para ControlBobinadoTarjetRoll.xaml
    /// </summary>
    public partial class ControlBobinadoTarjetRoll : UserControl, IControlTooling
    {
        #region Myregion

        private Herramental obj;
        private string Codigo;

        #endregion
        public ControlBobinadoTarjetRoll()
        {
            InitializeComponent();
            obj = new Herramental();
        }
        
        public int Guardar(string codigo)
        {
            // Mandamos llamar al método para insertar el objeto y retornamos el resultado
            return DataManager.InsertarBobinadoTargetRoll(codigo, Convert.ToDouble(tbx_A.Text), Convert.ToDouble(tbx_B.Text));
        }

        public int Update()
        {
            // Declaramos propiedades
            Herramental herramental = new Herramental();
            Propiedad propiedada = new Propiedad();
            Propiedad propiedadb = new Propiedad();

            // Asignamos valores
            herramental.Codigo = Codigo;
            herramental.idHerramental = obj.idHerramental;

            propiedada.Valor = double.Parse(tbx_A.Text);
            propiedadb.Valor = double.Parse(tbx_B.Text);

            // Agregamos propiedades
            herramental.Propiedades.Add(propiedada);
            herramental.Propiedades.Add(propiedadb);

            // Mandamos llamar el métoso para actualizar un registro
            return DataManager.ActualizarBobinadoTargetRoll(obj.idHerramental, Codigo, Convert.ToDouble(tbx_A.Text), Convert.ToDouble(tbx_B.Text));
        }

        public int Delete()
        {
            // Mandamos llamar el método para eliminar un registro
            return DataManager.EliminarBobinadoTargetRoll(obj.idHerramental);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfo_TargetRoll(codigoHerramental);
            Codigo = obj.Codigo;
            tbx_A.Text = Convert.ToString(obj.Propiedades[0].Valor);
            tbx_B.Text = Convert.ToString(obj.Propiedades[1].Valor);
        }
       
        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(tbx_A.Text) && !string.IsNullOrEmpty(tbx_B.Text))
                return true;
            else
                return false;
        }

        public bool ValidaRangos()
        {
            return true;
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
