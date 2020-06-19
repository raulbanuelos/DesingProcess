using Model;
using Model.Interfaces;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace View.Forms.Tooling
{
    /// <summary>
    /// Lógica de interacción para ControlCoilEntrancGuide.xaml
    /// </summary>
    public partial class ControlCoilEntrancGuide : UserControl, IControlTooling
    {

        Herramental herramental;

        public ControlCoilEntrancGuide()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Método que guarda la información registrada.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int Guardar(string codigo)
        {
            //Declaración del objeto.
            Coil obj = new Coil();

            //Asiganmos los valores.
            obj.codigo = codigo;
            obj.code = code.Text;
            obj.dimA = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimB = double.Parse(dimB.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimC = double.Parse(dimC.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_min = double.Parse(WMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_max = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.radial_wire_min = double.Parse(RMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.radial_wire_max = double.Parse(Rmax.Text, CultureInfo.InvariantCulture.NumberFormat);

            //Mandamos a llamar al método para insertar el objeto y retornamos el resultado.
            return DataManager.SetCOIL_ENTRANCE_GUIDE(obj);

        }

        /// <summary>
        /// Método que inicializa los componentes que se muestran en pantalla.
        /// </summary>
        public void Inicializa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Método que valída si los campos se encuentran vacíos.
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(code.Text) & !string.IsNullOrEmpty(dimA.Text) & !string.IsNullOrEmpty(dimA.Text) & !string.IsNullOrEmpty(dimB.Text) & !string.IsNullOrWhiteSpace(dimC.Text) &
                !string.IsNullOrEmpty(WMin.Text) & !string.IsNullOrEmpty(WMax.Text) & !string.IsNullOrEmpty(code.Text) & !string.IsNullOrEmpty(Rmax.Text) & !string.IsNullOrEmpty(RMin.Text))
                return true;
            else
                return false;
        }

        public int Delete()
        {
            return DataManager.DeleteCOIL_ENTRANCE_GUIDE(herramental.idHerramental);
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

        /// <summary>
        /// Método que valída los rangos.
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            double wmin, wmax, rmin, rmax;
            wmin = double.Parse(WMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            wmax = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            rmin = double.Parse(RMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            rmax = double.Parse(Rmax.Text, CultureInfo.InvariantCulture.NumberFormat);

            if (wmin < wmax && rmin < rmax)
                return true;
            else
                return false;
        }

        public int Update()
        {
            //Declaración del objeto.
            Coil obj = new Coil();

            //Asiganmos los valores.
            obj.codigo = herramental.Codigo;
            obj.ID = herramental.idHerramental;
            obj.code = code.Text;
            obj.dimA = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimB = double.Parse(dimB.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimC = double.Parse(dimC.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_min = double.Parse(WMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_max = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.radial_wire_min = double.Parse(RMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.radial_wire_max = double.Parse(Rmax.Text, CultureInfo.InvariantCulture.NumberFormat);

            return DataManager.UpdateCOIL_ENTRANCE_GUIDE(obj);
        }

        public void InicializaCampos(string codigoHerramental)
        {
            herramental = DataManager.GetInfoCOIL_Entrance_Guide(codigoHerramental);

            code.Text = herramental.PropiedadesCadena[0].Valor;
            dimA.Text = Convert.ToString(herramental.Propiedades[0].Valor);
            dimB.Text = Convert.ToString(herramental.Propiedades[1].Valor);
            dimC.Text = Convert.ToString(herramental.Propiedades[2].Valor);
            WMin.Text = Convert.ToString(herramental.Propiedades[3].Valor);
            WMax.Text = Convert.ToString(herramental.Propiedades[4].Valor);
            RMin.Text = Convert.ToString(herramental.Propiedades[5].Valor);
            Rmax.Text = Convert.ToString(herramental.Propiedades[6].Valor);
        }


    }
}
