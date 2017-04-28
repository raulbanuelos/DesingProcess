using MahApps.Metro.Controls;
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
        }

        
    }
}
