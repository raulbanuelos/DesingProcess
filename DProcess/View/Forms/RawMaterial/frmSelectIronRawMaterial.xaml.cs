using MahApps.Metro.Controls;
namespace View.Forms.RawMaterial
{
    /// <summary>
    /// Lógica de interacción para frmSelectIronRawMaterial.xaml
    /// </summary>
    public partial class frmSelectIronRawMaterial : MetroWindow
    {
        public frmSelectIronRawMaterial()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
