using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para ControlWorkCam.xaml
    /// </summary>
    public partial class ControlWorkCam : UserControl, IControlTooling
    {
        public ControlWorkCam()
        {
            InitializeComponent();
        }

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();
            PropiedadCadena descripcion = new PropiedadCadena();
            PropiedadCadena medidaNominal = new PropiedadCadena();

            obj.Codigo = codigo;
            descripcion.Valor = desc.Text;
            medidaNominal.Valor = medidaN.Text;

            obj.PropiedadesCadena.Add(descripcion);
            obj.PropiedadesCadena.Add(medidaNominal);

            return DataManager.SetWorkCam(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Inicializa()
        {
            InitializeComponent();
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(desc.Text) || !string.IsNullOrWhiteSpace(desc.Text) || !string.IsNullOrEmpty(medidaN.Text))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            return true;
        }

        /// <summary>
        /// Método que valida si la tecla recibida es un espacio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyValidation(object sender, KeyEventArgs e)
        {
            //si la tecla presionada es un espacio no escribe el caracter.
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}
