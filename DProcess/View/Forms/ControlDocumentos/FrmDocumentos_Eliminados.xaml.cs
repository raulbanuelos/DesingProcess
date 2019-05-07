using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View.Forms.ControlDocumentos
{
    /// <summary>
    /// Lógica de interacción para FrmDocumentos_Eliminados.xaml
    /// </summary>
    public partial class FrmDocumentos_Eliminados : MetroWindow
    {
        public FrmDocumentos_Eliminados()
        {
            InitializeComponent();
            txt_busqueda.Focus();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
