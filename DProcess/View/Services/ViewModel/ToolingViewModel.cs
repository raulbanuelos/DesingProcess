using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.Tooling;

namespace View.Services.ViewModel
{
    public class ToolingViewModel : INotifyPropertyChanged
    {
        #region Attributtes

        #endregion

        #region Properties
        private ObservableCollection<Herramental> maestroHerramental;
        public ObservableCollection<Herramental> MaestroHerramentales {
            get
            {
                return maestroHerramental;
            }
            set
            {
                maestroHerramental = value;
                NotifyChange("MaestroHerramentales");
            }
        }

        string textoBusqueda;
        public string TextoBusqueda {
            get
            {
                return textoBusqueda;
            }
            set
            {
                textoBusqueda = value;
                NotifyChange("TextoBusqueda");
            }
        }
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors

        public ToolingViewModel()
        {
            maestroHerramental = new ObservableCollection<Herramental>();
            MaestroHerramentales = DataManager.GetMaestroHerramental(TextoBusqueda);
        }
        #endregion

        #region Commands
        public ICommand BuscarTooling
        {
            get
            {
                return new RelayCommand(param => buscarTooling((string)param));
            }
        }

        public ICommand IrCollarBK
        {
            get
            {
                return new RelayCommand(o => irCollarBK());
            }
        }
        #endregion

        #region Methods
        private void buscarTooling(string busqueda)
        {
            MaestroHerramentales = DataManager.GetMaestroHerramental(busqueda);
        }

        private void irCollarBK()
        {
            WCollarBK wCollar = new WCollarBK();

            CollarAutoFinTurnViewModel vm = new CollarAutoFinTurnViewModel();

            wCollar.DataContext = vm;

            wCollar.ShowDialog();
        }
        #endregion
    }
}
