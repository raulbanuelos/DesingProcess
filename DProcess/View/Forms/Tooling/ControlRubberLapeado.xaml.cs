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
    /// Lógica de interacción para ControlRubberLapeado.xaml
    /// </summary>
    public partial class ControlRubberLapeado : UserControl, IControlTooling
    {
        #region Myregion

        private Herramental obj;
        private string Codigo;

        #endregion

        public ControlRubberLapeado()
        {
            InitializeComponent();
            obj = new Herramental();
        }
        
        /// <summary>
        /// Método para guardar registros
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int Guardar(string codigo)
        {
            // Mandamos llamar al método para insertar el objeto y retornamos el resultado
            return DataManager.Insert_RubberLapeado(codigo, Convert.ToDouble(txb_Dim_A.Text), Convert.ToString(txb_Plano.Text));
        }

        /// <summary>
        /// Método para actualizar registros
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            // Declaramos propiedades
            Herramental herramental = new Herramental();
            Propiedad propiedaddima = new Propiedad();
            PropiedadCadena propiedadplano = new PropiedadCadena();

            // Asignamos valores
            herramental.Codigo = Codigo;
            herramental.idHerramental = obj.idHerramental;

            propiedaddima.Valor = double.Parse(txb_Dim_A.Text);
            propiedadplano.Valor = Convert.ToString(txb_Plano.Text);

            // Agregamos propiedades
            herramental.Propiedades.Add(propiedaddima);
            herramental.PropiedadesCadena.Add(propiedadplano);

            // Mandamos llamar el método para actualizar un registro
            return DataManager.Update_RubberLapeado(obj.idHerramental, Codigo, Convert.ToDouble(txb_Dim_A.Text), Convert.ToString(txb_Plano.Text));
        }

        /// <summary>
        /// Método para eliminar registros
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            // Mandamos llamar el método para elminar un registro
            return DataManager.Delete_RubberLapeado(obj.idHerramental);
        }

        /// <summary>
        /// Método para inicializar componente
        /// </summary>
        public void Inicializa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Método para inicializar campos
        /// </summary>
        /// <param name="codigoHerramental"></param>
        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfo_RubberLapeado(codigoHerramental);
            Codigo = obj.Codigo;
            txb_Dim_A.Text = Convert.ToString(obj.Propiedades[0].Valor);
            txb_Plano.Text = Convert.ToString(obj.PropiedadesCadena[0].Valor);
        }
        
        /// <summary>
        /// Método para validar que los campos no sean nulos
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(txb_Dim_A.Text) && !string.IsNullOrEmpty(txb_Plano.Text))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Método para validar rangos (el mínimo no sea mayor al máximo y el máximo no sea menor al mínimo)
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            return true;
        }

        /// <summary>
        /// Valida que solo se introduzcan números
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
