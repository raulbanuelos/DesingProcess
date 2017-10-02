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
    /// Lógica de interacción para ControlCollarBK.xaml
    /// </summary>
    public partial class ControlCollarBK : UserControl, IControlTooling
    {
        public ControlCollarBK()
        {
            InitializeComponent();
        }

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();
            PropiedadCadena parteC = new PropiedadCadena();
            Propiedad dim_A = new Propiedad();
            Propiedad dim_B = new Propiedad();

            obj.Codigo = codigo;
            obj.Plano = plano.Text;
            parteC.Valor = parte.Text;
            dim_A.Unidad = dimA_unidades.Text;
            dim_A.Valor = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            dim_B.Unidad = dimB_unidades.Text;
            dim_B.Valor= double.Parse(dimB.Text, CultureInfo.InvariantCulture.NumberFormat);

            obj.Propiedades.Add(dim_A);
            obj.Propiedades.Add(dim_B);
            obj.PropiedadesCadena.Add(parteC);

            return DataManager.SetCollarBK(obj);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(plano.Text) & !string.IsNullOrEmpty(parte.Text) & !string.IsNullOrEmpty(dimA.Text) & !string.IsNullOrEmpty(dimB.Text)
                & !string.IsNullOrEmpty(dimA_unidades.Text) & !string.IsNullOrEmpty(dimB_unidades.Text))
                return true;
            else
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
