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
    /// Lógica de interacción para ControlBobinadoLowerRoll.xaml
    /// </summary>
    public partial class ControlBobinadoLowerRoll : UserControl, IControlTooling
    {
        #region Myregion

        private Herramental obj;
        private string Codigo;

        #endregion
        public ControlBobinadoLowerRoll()
        {
            InitializeComponent();
            obj = new Herramental();
        }        

        public int Guardar(string codigo)            
        {
            // Mandamos llamar al método para insertar el objeto y retornamos el resultado
            return DataManager.InsertarBobinadoLowerRoll(codigo, Convert.ToString(tbx_Detalle_Rodillo.Text), Convert.ToString(tbx_Detalle_Engrane.Text), Convert.ToDouble(tbx_Wire_Width_Min.Text), Convert.ToDouble(tbx_Wire_Width_Max.Text), Convert.ToDouble(tbx_Dia_Min.Text), Convert.ToDouble(tbx_Dia_Max.Text), Convert.ToDouble(tbx_Side_Plate_Dia.Text));
        }

        public int Update()
        {
            // Declaramos propiedades
            Herramental herramental = new Herramental();
            PropiedadCadena propiedaddetallerodillo = new PropiedadCadena();
            PropiedadCadena propiedaddetalleengrane = new PropiedadCadena();
            Propiedad propiedadwiremin = new Propiedad();
            Propiedad propiedadwiremax = new Propiedad();
            Propiedad propiedaddiamin = new Propiedad();
            Propiedad propiedaddiamax = new Propiedad();
            Propiedad propiedadsideplate = new Propiedad();

            // Asignamos valores
            herramental.Codigo = Codigo;
            herramental.idHerramental = obj.idHerramental;

            propiedaddetallerodillo.Valor = Convert.ToString(tbx_Detalle_Rodillo.Text);
            propiedaddetalleengrane.Valor = Convert.ToString(tbx_Detalle_Engrane.Text);
            propiedadwiremin.Valor = double.Parse(tbx_Wire_Width_Min.Text);
            propiedadwiremax.Valor = double.Parse(tbx_Wire_Width_Max.Text);
            propiedaddiamin.Valor = double.Parse(tbx_Dia_Min.Text);
            propiedaddiamax.Valor = double.Parse(tbx_Dia_Max.Text);
            propiedadsideplate.Valor = double.Parse(tbx_Side_Plate_Dia.Text);

            // Agregamos propiedades
            herramental.PropiedadesCadena.Add(propiedaddetallerodillo);
            herramental.PropiedadesCadena.Add(propiedaddetalleengrane);
            herramental.Propiedades.Add(propiedadwiremin);
            herramental.Propiedades.Add(propiedadwiremax);
            herramental.Propiedades.Add(propiedaddiamin);
            herramental.Propiedades.Add(propiedaddiamax);
            herramental.Propiedades.Add(propiedadsideplate);

            // Mandamos llamar el método para actualizar un registro
            return DataManager.ActualizarBobinadoLowerRoll(obj.idHerramental, Codigo, Convert.ToString(tbx_Detalle_Rodillo.Text), Convert.ToString(tbx_Detalle_Engrane.Text), Convert.ToDouble(tbx_Wire_Width_Min.Text), Convert.ToDouble(tbx_Wire_Width_Max.Text), Convert.ToDouble(tbx_Dia_Min.Text), Convert.ToDouble(tbx_Dia_Max.Text), Convert.ToDouble(tbx_Side_Plate_Dia.Text));
        }

        public int Delete()
        {
            // Mandamos llamar el método para eliminar un registro
            return DataManager.EliminarBobinadoLowerRoll(obj.idHerramental);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfo_LowerRoll(codigoHerramental);
            Codigo = obj.Codigo;
            tbx_Detalle_Rodillo.Text = Convert.ToString(obj.PropiedadesCadena[0].Valor);
            tbx_Detalle_Engrane.Text = Convert.ToString(obj.PropiedadesCadena[1].Valor);
            tbx_Wire_Width_Min.Text = Convert.ToString(obj.Propiedades[0].Valor);
            tbx_Wire_Width_Max.Text = Convert.ToString(obj.Propiedades[1].Valor);
            tbx_Dia_Min.Text = Convert.ToString(obj.Propiedades[2].Valor);
            tbx_Dia_Max.Text = Convert.ToString(obj.Propiedades[3].Valor);
            tbx_Side_Plate_Dia.Text = Convert.ToString(obj.Propiedades[4].Valor);
        }

        

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(tbx_Detalle_Rodillo.Text) & !string.IsNullOrEmpty(tbx_Detalle_Engrane.Text) & !string.IsNullOrEmpty(tbx_Wire_Width_Min.Text) & !string.IsNullOrEmpty(tbx_Wire_Width_Max.Text) & !string.IsNullOrEmpty(tbx_Dia_Min.Text) & !string.IsNullOrEmpty(tbx_Dia_Max.Text) & !string.IsNullOrEmpty(tbx_Side_Plate_Dia.Text))
                return true;
            else
                return false;
        }

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
