using MahApps.Metro.Controls;

namespace View.Forms.Shared
{
    /// <summary>
    /// Lógica de interacción para WSelectedOption.xaml
    /// </summary>
    public partial class WSelectedOption : MetroWindow
    {
        public WSelectedOption()
        {
            InitializeComponent();
        }

        private void btn_Ok_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
