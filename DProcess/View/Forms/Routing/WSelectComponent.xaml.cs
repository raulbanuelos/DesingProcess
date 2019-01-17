using MahApps.Metro.Controls;

namespace View.Forms.Routing
{
    /// <summary>
    /// Lógica de interacción para WSelectComponent.xaml
    /// </summary>
    public partial class WSelectComponent : MetroWindow
    {
        public WSelectComponent()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
