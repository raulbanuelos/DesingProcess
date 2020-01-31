using MahApps.Metro.Controls;
using System.Windows;

namespace View.Forms.LeccionesAprendidas
{
    /// <summary>
    /// Lógica de interacción para FrmSelect.xaml
    /// </summary>
    public partial class FrmSelect : MetroWindow
    {
        public FrmSelect()
        {
            InitializeComponent();
        }

        private void btn_aceptar_Click(object sender, RoutedEventArgs e)
        {
            //Acepta el Dialog y retorna el DialogResults
            this.DialogResult = true;
        }
    }
}
