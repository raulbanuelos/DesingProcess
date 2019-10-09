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
        #region Myregion

        private Herramental obj;
        private string Codigo;

        #endregion

        public ControlBobinadoCenterWafer()
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
            return DataManager.InsertarBobinadoCenterWafer(codigo, Convert.ToDouble(tbx_Dim_A_Min.Text), Convert.ToDouble(tbx_Dim_A_Max.Text), Convert.ToDouble(tbx_Wire_Width.Text), Convert.ToString(tbx_Detalle.Text), Convert.ToDouble(tbx_Dia_B.Text), Convert.ToDouble(tbx_F_Width.Text));
        }

        /// <summary>
        /// Método para actualizar registros
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            // Declaramos propiedades
            Herramental herramental = new Herramental();
            Propiedad propiedaddimamin = new Propiedad();
            Propiedad propiedaddimamax = new Propiedad();
            Propiedad propiedadwirewidth = new Propiedad();
            PropiedadCadena propiedaddetalle = new PropiedadCadena();
            Propiedad propiedaddiab = new Propiedad();
            Propiedad propiedadfwidth = new Propiedad();

            // Asignamos valores 
            herramental.Codigo = Codigo;
            herramental.idHerramental = obj.idHerramental;

            propiedaddimamin.Valor = double.Parse(tbx_Dim_A_Min.Text);
            propiedaddimamax.Valor = double.Parse(tbx_Dim_A_Max.Text);
            propiedadwirewidth.Valor = double.Parse(tbx_Wire_Width.Text);
            propiedaddetalle.Valor = Convert.ToString(tbx_Detalle.Text);
            propiedaddiab.Valor = double.Parse(tbx_Dia_B.Text);
            propiedadfwidth.Valor = double.Parse(tbx_F_Width.Text);

            // Agregamos propiedades
            herramental.Propiedades.Add(propiedaddimamin);
            herramental.Propiedades.Add(propiedaddimamax);
            herramental.Propiedades.Add(propiedadwirewidth);
            herramental.PropiedadesCadena.Add(propiedaddetalle);
            herramental.Propiedades.Add(propiedaddiab);
            herramental.Propiedades.Add(propiedadfwidth);

            // Mandamos llamar el método para actualizar un registro
            return DataManager.ActualizarBobinadoCenterWafer(obj.idHerramental, Codigo, Convert.ToDouble(tbx_Dim_A_Min.Text), Convert.ToDouble(tbx_Dim_A_Max.Text), Convert.ToDouble(tbx_Wire_Width.Text), Convert.ToString(tbx_Detalle.Text), Convert.ToDouble(tbx_Dia_B.Text), Convert.ToDouble(tbx_F_Width.Text));
        }

        /// <summary>
        /// Método para eliminar registros
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            // Mandamos llamar al método para eliminar un registro
            return DataManager.EliminarBobinadoCenterWafer(obj.idHerramental);
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
            obj = DataManager.GetInfo_CenterWafer(codigoHerramental);
            Codigo = obj.Codigo;
            tbx_Dim_A_Min.Text = Convert.ToString(obj.Propiedades[0].Valor);
            tbx_Dim_A_Max.Text = Convert.ToString(obj.Propiedades[1].Valor);
            tbx_Wire_Width.Text = Convert.ToString(obj.Propiedades[2].Valor);
            tbx_Detalle.Text = Convert.ToString(obj.PropiedadesCadena[0].Valor);
            tbx_Dia_B.Text = Convert.ToString(obj.Propiedades[3].Valor);
            tbx_F_Width.Text = Convert.ToString(obj.Propiedades[4].Valor);
        }        

        /// <summary>
        /// Método para validar que los campos no sean nulos
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(tbx_Dim_A_Min.Text) & !string.IsNullOrEmpty(tbx_Dim_A_Max.Text) & !string.IsNullOrEmpty(tbx_Wire_Width.Text) & !string.IsNullOrEmpty(tbx_Detalle.Text) & !string.IsNullOrEmpty(tbx_Dia_B.Text) & !string.IsNullOrEmpty(tbx_F_Width.Text))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Método para validar rangos (el mínimo no sea mayor al máximo y máximo no sea menor al mínimo)
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            double diaamin, diaamax;

            diaamin = Convert.ToDouble(tbx_Dim_A_Min.Text);
            diaamax = Convert.ToDouble(tbx_Dim_A_Max.Text);

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
