using MahApps.Metro.Controls;
using System.Windows;

namespace View.Forms.Modals
{
    /// <summary>
    /// Lógica de interacción para frmViewUnidades.xaml
    /// </summary>
    public partial class frmViewUnidades : MetroWindow
    {
        public frmViewUnidades()
        {
            InitializeComponent();
        }

        private void btn_aceptar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
