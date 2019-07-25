using MahApps.Metro.Controls;

namespace View.Forms.RawMaterial
{
    /// <summary>
    /// Lógica de interacción para frmSelectIronRailsRawMaterial.xaml
    /// </summary>
    public partial class frmSelectIronRailsRawMaterial : MetroWindow
    {
        public frmSelectIronRailsRawMaterial()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
