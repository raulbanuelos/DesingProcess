using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para ControlBushing.xaml
    /// </summary>
    public partial class ControlBushing : UserControl, IControlTooling
    {


        #region Propiedades
        ObservableCollection<string> ListaDim;
        Herramental obj;
        private string codigo;
        #endregion

        public ControlBushing()
        {
            InitializeComponent();
            obj = new Herramental();
        }

        /// <summary>
        ///  Método que guarda la información registrada.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int Guardar(string codigo)
        {
            //Declaramos el objeto.
            Herramental obj = new Herramental();     
            Propiedad MedidaNominal = new Propiedad();
            PropiedadCadena DimB = new PropiedadCadena();
            //Asignamos los valores.
            obj.Codigo = codigo;
            obj.Plano = Plano.Text;
            DimB.Valor = comboB.SelectedValuePath;
            MedidaNominal.Valor = double.Parse(medidaN.Text, CultureInfo.InvariantCulture.NumberFormat);
            //Agregamos las propiedades.
            obj.Propiedades.Add(MedidaNominal);
            obj.PropiedadesCadena.Add(DimB);

            //Mandamos a llamar al método para insertar el objeto y retornamos el resultado.
            return DataManager.SetBushing(obj);
        }

        /// <summary>
        /// Método que inicializa los componentes que se muestran en pantalla.
        /// </summary>
        public void Inicializa()
        {
            InitializeComponent();
            ListaDim = new ObservableCollection<string>();
            ListaDim.Add("2°");
            ListaDim.Add("4°");
            ListaDim.Add("9°");
            ListaDim.Add("15°");

            comboB.ItemsSource=ListaDim;
        }

        /// <summary>
        /// Método que valída si los campos se encuentran vacíos.
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            //Si los campos no están vacíos, retorna true.
            if (comboB.SelectedValue != null & !string.IsNullOrEmpty(medidaN.Text) & !string.IsNullOrEmpty(Plano.Text))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Método que valída los rangos, como no hay rangos regresa verdadero.
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            return true;
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
            Regex regex = new Regex("[^0-9.]+");

            //Valida si el caracter es un número o punto, si el texto ya contiene un punto no permite escribir otro punto
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
                //Si no es un número o punto no escribe el caracter
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

        public int Update()
        {
            Herramental herram = new Herramental();
            Propiedad MedidaNominal = new Propiedad();
            PropiedadCadena DimB = new PropiedadCadena();
            //Asignamos los valores.
            herram.Codigo = codigo;
            herram.idHerramental = obj.idHerramental;
            herram.Plano = Plano.Text;
            DimB.Valor = comboB.SelectedValuePath;
            MedidaNominal.Valor = double.Parse(medidaN.Text, CultureInfo.InvariantCulture.NumberFormat);
            //Agregamos las propiedades.
            herram.Propiedades.Add(MedidaNominal);
            herram.PropiedadesCadena.Add(DimB);

            return DataManager.UpdateBushingBB(herram);
        }

        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfoBushing_BatesBore(codigoHerramental);

            codigo = obj.Codigo;
            medidaN.Text =Convert.ToString( obj.Propiedades[0].Valor);
            Plano.Text = obj.Plano;
            comboB.SelectedValuePath = obj.PropiedadesCadena[0].Valor;

        }

        public int Delete()
        {
            return DataManager.DeleteBushingBB(obj.idHerramental);
        }
    }
}
