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
    /// Lógica de interacción para ControlGuideBarFirstRG.xaml
    /// </summary>
    public partial class ControlGuideBarFirstRG : UserControl, IControlTooling
    {
        public ControlGuideBarFirstRG()
        {
            InitializeComponent();
        }

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();
            Propiedad A = new Propiedad();
            obj.Codigo = codigo;
            A.Valor= double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.Propiedades.Add(A);

            return DataManager.SetFirstRG(obj);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(dimA.Text) & !string.IsNullOrWhiteSpace(dimA.Text))
                return true;
            else
                return false;
        }

        public bool ValidaRangos()
        {
            return false;
        }
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Text))
            {
                Regex regex = new Regex("[^0-9.]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }
    }
}
