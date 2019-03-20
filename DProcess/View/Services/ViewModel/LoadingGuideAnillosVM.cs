using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    public class LoadingGuideAnillosVM : INotifyPropertyChanged
    {
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

        #region Propiedades
        private DataTable _ListaHerramentalesLoadingGuideAnillos;
        public DataTable ListaHerramentalesLoadingGuideAnillos
        {
            get
            {
                return _ListaHerramentalesLoadingGuideAnillos;
            }
            set
            {
                _ListaHerramentalesLoadingGuideAnillos = value;
                NotifyChange("ListaHerramentalesLoadingGuideAnillos");
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

        public ICommand BusquedaLoadingGuideAnillos
        {
            get
            {
                return new RelayCommand(param => Busqueda((string)param));
            }
        }

        public LoadingGuideAnillosVM()
        {
            Busqueda(string.Empty);
        }

        #region Métodos
        public void Busqueda(string TextoBuscar)
        {
            ListaHerramentalesLoadingGuideAnillos = DataManager.GetAllLoadingGuideAnillos(TextoBuscar);
        }
        #endregion
    }
}
