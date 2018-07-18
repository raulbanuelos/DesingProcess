using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
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
using View.Resources;
using View.Services;

namespace View.Forms.ControlDocumentos
{
    /// <summary>
    /// Lógica de interacción para FrmDocumento.xaml
    /// </summary>
    public partial class FrmDocumento : MetroWindow
    {
        public ObservableCollection<Archivo> ListaDocumentos;
        
        public FrmDocumento()
        {
            InitializeComponent();
            ListaDocumentos = new ObservableCollection<Archivo>();

            Closing += FrmDocumento_Closing;
        }

        private  void FrmDocumento_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string mensaje = StringResources.msgExitWithOutSaveChanges;

            MessageBoxResult result = MessageBox.Show(mensaje, "Warning", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }

        }
    }
}
