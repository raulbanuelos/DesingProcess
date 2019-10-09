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
    /// Lógica de interacción para ControlBarrelGradeBushing.xaml
    /// </summary>
    public partial class ControlBarrelGradeBushing : UserControl, IControlTooling
    {
        #region Myregion

        private Herramental obj;
        private string Codigo;

        #endregion

        public ControlBarrelGradeBushing()
        {
            InitializeComponent();
            obj = new Herramental();
        }       

        public int Guardar(string codigo)
        {
            // Mandamos llamar al método para insertar el objeto y retornamos el resultado
            return DataManager.InsertarBarrelGradeBrushing(codigo, Convert.ToDouble(tbx_Dim_D.Text));
        }
        public int Update()
        {
            // Declaramos propiedades
            Herramental herramental = new Herramental();
            Propiedad propiedaddimd = new Propiedad();

            // Asignamos valores
            herramental.Codigo = Codigo;
            herramental.idHerramental = obj.idHerramental;

            propiedaddimd.Valor = double.Parse(tbx_Dim_D.Text);

            // Agregamos propiedades
            herramental.Propiedades.Add(propiedaddimd);

            // Mandamos llamar el método para actualizar un registro
            return DataManager.ActualizarBarrelGradeBushing(obj.idHerramental, Codigo, Convert.ToDouble(tbx_Dim_D.Text));
        }

        public int Delete()
        {
            // Mandamos llamar al método para eliminar un registro
            return DataManager.EliminarBarrelGradeBushing(obj.idHerramental);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfo_Bushing(codigoHerramental);
            Codigo = obj.Codigo;
            tbx_Dim_D.Text = Convert.ToString(obj.Propiedades[0].Valor);
        }       

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(tbx_Dim_D.Text))
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
