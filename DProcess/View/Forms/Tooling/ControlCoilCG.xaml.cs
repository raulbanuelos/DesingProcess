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
    /// Lógica de interacción para ControlCoilCG.xaml
    /// </summary>
    public partial class ControlCoilCG : UserControl, IControlTooling
    {
        public ControlCoilCG()
        {
            InitializeComponent();
        }

        public int Guardar(string codigo)
        {
            Coil obj = new Coil();

            obj.codigo = codigo;
            obj.code = code.Text;
            obj.dimA = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimB = double.Parse(dimB.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.dimC = double.Parse(dimC.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_min = double.Parse(WMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_max = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.radial_wire_min = double.Parse(RMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.radial_wire_max = double.Parse(Rmax.Text, CultureInfo.InvariantCulture.NumberFormat);
            return DataManager.SetCOIL_CENTER_GUIDE(obj); 

        }

        public void Inicializa()
        {
           InitializeComponent();
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(code.Text) &!string.IsNullOrEmpty(dimA.Text) & !string.IsNullOrEmpty(dimA.Text) & !string.IsNullOrEmpty(dimB.Text) & !string.IsNullOrWhiteSpace(dimC.Text) &
                !string.IsNullOrEmpty(WMin.Text) & !string.IsNullOrEmpty(WMax.Text) & !string.IsNullOrEmpty(code.Text) & !string.IsNullOrEmpty(Rmax.Text) & !string.IsNullOrEmpty(RMin.Text))
                return true;
            else
                return false;
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Text)) {
                Regex regex = new Regex("[^0-9.]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        } 
        
        private void KeyValidation(object sender, KeyEventArgs e)
         {
            if (e.Key == Key.Space)
                e.Handled = false;
        }      
    }
}
