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
    /// Lógica de interacción para ControlBobinadoUpperRoll.xaml
    /// </summary>
    public partial class ControlBobinadoUpperRoll : UserControl, IControlTooling
    {
        #region Myregion

        private Herramental obj;
        private string Codigo;

        #endregion

        public ControlBobinadoUpperRoll()
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
            return DataManager.InsertarBobinadoUpperRoll(codigo, Convert.ToDouble(tbx_Wire_Width_Min.Text), Convert.ToDouble(tbx_Wire_Width_Max.Text), Convert.ToDouble(tbx_Dia_Min.Text), Convert.ToDouble(tbx_Dia_Max.Text), Convert.ToString(tbx_Detalle_Engrane.Text), Convert.ToDouble(tbx_Medida.Text));
        }

        /// <summary>
        /// Método para actualizar registros
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            // Declaramos propiedades
            Herramental herramental = new Herramental();
            Propiedad propiedadwiremin = new Propiedad();
            Propiedad propiedadwiremax = new Propiedad();
            Propiedad propiedaddiamin = new Propiedad();
            Propiedad propiedaddiamax = new Propiedad();
            PropiedadCadena propiedaddetalle = new PropiedadCadena();
            Propiedad propiedadmedida = new Propiedad();          

            // Asignamos valores 
            herramental.Codigo = Codigo;
            herramental.idHerramental = obj.idHerramental;

            propiedadwiremin.Valor = double.Parse(tbx_Wire_Width_Min.Text);
            propiedadwiremax.Valor = double.Parse(tbx_Wire_Width_Max.Text);
            propiedaddiamin.Valor = double.Parse(tbx_Dia_Min.Text);
            propiedaddiamax.Valor = double.Parse(tbx_Dia_Max.Text);
            propiedaddetalle.Valor = Convert.ToString(tbx_Detalle_Engrane.Text);
            propiedadmedida.Valor = double.Parse(tbx_Medida.Text);

            // Agregamos propiedades
            herramental.Propiedades.Add(propiedadwiremin);
            herramental.Propiedades.Add(propiedadwiremax);
            herramental.Propiedades.Add(propiedaddiamin);
            herramental.Propiedades.Add(propiedaddiamax);
            herramental.PropiedadesCadena.Add(propiedaddetalle);
            herramental.Propiedades.Add(propiedadmedida);

            // Mandamos llamar el método para actualizar un registro
            return DataManager.ActualizarBobinadoUpperRoll(obj.idHerramental, Codigo, Convert.ToDouble(tbx_Wire_Width_Min.Text), Convert.ToDouble(tbx_Wire_Width_Max.Text), Convert.ToDouble(tbx_Dia_Min.Text), Convert.ToDouble(tbx_Dia_Max.Text), Convert.ToString(tbx_Detalle_Engrane.Text), Convert.ToDouble(tbx_Medida.Text));
        }

        /// <summary>
        /// Método para eliminar registros
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            // Mandamos llamar el método para elminar un registro
            return DataManager.EliminarBobinadoUpperRoll(obj.idHerramental);
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
            obj = DataManager.GetInfo_UpperRoll(codigoHerramental);
            Codigo = obj.Codigo;
            tbx_Wire_Width_Min.Text = Convert.ToString(obj.Propiedades[0].Valor);
            tbx_Wire_Width_Max.Text = Convert.ToString(obj.Propiedades[1].Valor);
            tbx_Dia_Min.Text = Convert.ToString(obj.Propiedades[2].Valor);
            tbx_Dia_Max.Text = Convert.ToString(obj.Propiedades[3].Valor);
            tbx_Detalle_Engrane.Text = Convert.ToString(obj.PropiedadesCadena[0].Valor);
            tbx_Medida.Text = Convert.ToString(obj.Propiedades[4].Valor);
        }        

        /// <summary>
        /// Método para validar que los campos no seas nulos
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(tbx_Wire_Width_Min.Text) && !string.IsNullOrEmpty(tbx_Wire_Width_Max.Text) && !string.IsNullOrEmpty(tbx_Dia_Min.Text) && !string.IsNullOrEmpty(tbx_Dia_Max.Text) && !string.IsNullOrEmpty(tbx_Detalle_Engrane.Text) && !string.IsNullOrEmpty(tbx_Medida.Text))
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
            double wirewidthmin, wirewidthmax, diamin, diamax;

            wirewidthmin = Convert.ToDouble(tbx_Wire_Width_Min.Text);
            wirewidthmax = Convert.ToDouble(tbx_Wire_Width_Max.Text);
            diamin = Convert.ToDouble(tbx_Dia_Min.Text);
            diamax = Convert.ToDouble(tbx_Dia_Max.Text);

            if (wirewidthmin < wirewidthmax && diamin < diamax)            
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
