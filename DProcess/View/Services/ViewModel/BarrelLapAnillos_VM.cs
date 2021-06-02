using System.ComponentModel;
using System.Data;
using Model;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace View.Services.ViewModel
{
    public class BarrelLapAnillos_VM : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Methods
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

        #region Properties
        private DataTable _ListaHerramentalesBarrelLapAnillo;
        public DataTable ListaHerramentalesBarrelLapAnillo
        {
            get
            {
                return _ListaHerramentalesBarrelLapAnillo;
            }
            set
            {
                _ListaHerramentalesBarrelLapAnillo = value;
                NotifyChange("ListaHerramentalesBarrelLapAnillo");
            }
        }

        private DataTable _ListaOptimos;
        public DataTable ListaOptimos
        {
            get
            {
                return _ListaOptimos;
            }
            set
            {
                _ListaOptimos = value;
                NotifyChange("ListaOptimos");
            }
        }

        private DataTable _ListaMejores;
        public DataTable ListaMejores
        {
            get
            {
                return _ListaMejores;
            }
            set
            {
                _ListaMejores = value;
                NotifyChange("ListaMejores");
            }
        }

        private string _MedidaNominal;
        public string MedidaNominal
        {
            get { return _MedidaNominal; }
            set { _MedidaNominal = value; NotifyChange("MedidaNominal"); }
        }
        #endregion

        #region Commands

        public ICommand BusquedaBarrel
        {
            get
            {
                return new RelayCommand(param => Busqueda((string)param));
            }
        }

        #endregion

        #region Constructor
        public BarrelLapAnillos_VM()
        {
            Busqueda(string.Empty);
        }
        #endregion

        #region Methods
        public void Busqueda(string TextoBuscar)
        {
            ObservableCollection<Herramental> aux = new ObservableCollection<Herramental>();
            ListaHerramentalesBarrelLapAnillo = DataManager.GetALLBarrelLapAnillos_(TextoBuscar, out aux);
        }
        #endregion
    }
}