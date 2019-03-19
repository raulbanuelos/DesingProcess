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
using System.Windows.Shapes;

namespace View.Forms.Tooling
{
    /// <summary>
    /// Lógica de interacción para ControlClosingBandLapeado.xaml
    /// </summary>
    public partial class ControlClosingBandLapeado : UserControl, IControlTooling
    {
        #region Propiedades
        Herramental obj;
        private string codigo;
        #endregion

        public ControlClosingBandLapeado()
        {
            InitializeComponent();
        }

        #region Métodos

        /// <summary>
        /// Método que guarda la información registrada
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public int Guardar(string codigo)
        {
            //Declaración de los objetos
            Herramental datos = new Herramental();

            PropiedadCadena Descripcion = new PropiedadCadena();
            PropiedadCadena MedidaNominal = new PropiedadCadena();

            //Asignación de los valores
            datos.Codigo = codigo;

            Descripcion.Valor = Convert.ToString(DescripcionUC.Text);
            MedidaNominal.Valor = Convert.ToString(MedidaNominalUC.Text);

            //Agregamos las propiedades
            datos.PropiedadesCadena.Add(Descripcion);
            datos.PropiedadesCadena.Add(MedidaNominal);

            //Mandamos llamar el método para guardar los datos
            return DataManager.SetNewClosingBandLapeado(datos);
        }

        /// <summary>
        /// Método que inicializa los valores de los componentes que se muestran en la pantalla
        /// </summary>
        public void Inicializa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Método que valida si los campos se encuentran vacios.
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(DescripcionUC.Text) || !string.IsNullOrEmpty(MedidaNominalUC.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que valida los rangos, como no hay rangos se retorna un valor verdadero
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            return true;
        }

        /// <summary>
        /// Método que valida la entrada del textbox solo sea número flotante
        /// </summary>
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
        private void KeyValidation(object sender, KeyEventArgs e)
        {
            //si la tecla presionada es un espacio no la escribe
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        /// <summary>
        /// Método que actualiza los datos de un componente
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            //Declaramos los objetos
            Herramental data = new Herramental();

            PropiedadCadena Descripcion = new PropiedadCadena();
            PropiedadCadena MedidaNominal = new PropiedadCadena();

            //Asignamos los valores
            data.Codigo = codigo;
            data.idHerramental = obj.idHerramental;

            Descripcion.Valor = Convert.ToString(DescripcionUC.Text);
            MedidaNominal.Valor = Convert.ToString(MedidaNominalUC.Text);

            //Agregamos las propiedades
            data.PropiedadesCadena.Add(Descripcion);
            data.PropiedadesCadena.Add(MedidaNominal);

            //Mandamos llamarel método para guardar los datos
            return DataManager.UpdateClosingBandLapeado(data);
        }

        /// <summary>
        /// Método que carga los datos cuando se van a actualizar o eliminar
        /// </summary>
        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfoClosingBandLapeado(codigoHerramental);

            codigo = obj.Codigo;

            DescripcionUC.Text = Convert.ToString(obj.PropiedadesCadena[0].Valor);
            MedidaNominalUC.Text = Convert.ToString(obj.PropiedadesCadena[1].Valor);
        }

        /// <summary>
        /// Método que borra un componente seleccionado
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return DataManager.DeleteClosingBandLapeado(obj.idHerramental);
        }

        #endregion

    }
}
