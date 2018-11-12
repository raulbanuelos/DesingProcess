using System.Windows;
using MahApps.Metro.Controls;

namespace View.Forms.RawMaterial
{
    /// <summary>
    /// Lógica de interacción para frmSelectRawMaterial.xaml
    /// </summary>
    public partial class frmSelectRawMaterial : MetroWindow
    {
        public frmSelectRawMaterial()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
