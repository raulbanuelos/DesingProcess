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
    /// Lógica de interacción para ControlLoadingGuideAnillos_.xaml
    /// </summary>
    public partial class ControlLoadingGuideAnillos_ : UserControl, IControlTooling
    {
        Herramental obj;
        private string codigo;

        public ControlLoadingGuideAnillos_()
        {
            InitializeComponent();
            obj = new Herramental();
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
            PropiedadCadena MedidaNominal = new PropiedadCadena();

            //Asignación de los valores
            datos.Codigo = codigo;
            MedidaNominal.Valor = Convert.ToString(MedidaNominalUC.Text);

            //Agregamos las propiedades
            datos.PropiedadesCadena.Add(MedidaNominal);

            //Mandamos llamar el método para guardar los datos
            return DataManager.SetNewLoadingGuideAnillos(datos);
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
            if (!string.IsNullOrEmpty(MedidaNominalUC.Text))
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
            //Declaración de los objetos
            Herramental datos = new Herramental();
            PropiedadCadena MedidaNominal = new PropiedadCadena();

            //Asignación de los valores
            datos.Codigo = codigo;
            datos.idHerramental = obj.idHerramental;
            MedidaNominal.Valor = Convert.ToString(MedidaNominalUC.Text);

            //Agregamos las propiedades
            datos.PropiedadesCadena.Add(MedidaNominal);

            //Mandamos llamarel método para guardar los datos
            return DataManager.UpdateLoadingGuideAnillos(datos);
        }

        /// <summary>
        /// Método que carga los datos cuando se van a actualizar o eliminar
        /// </summary>
        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfoLoadingGuideAnillos(codigoHerramental);

            codigo = obj.Codigo;
            MedidaNominalUC.Text = Convert.ToString(obj.PropiedadesCadena[0].Valor);
        }

        /// <summary>
        /// Método que borra un componente seleccionado
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            return DataManager.DeleteLoadingGuideAnillos(obj.idHerramental);
        }

        #endregion
    }
}
