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
    public class FrontRearAnillosVM : INotifyPropertyChanged
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

        private DataTable _ListaFrontRearCollarAnillos;
        public DataTable ListaFrontRearCollarAnillos
        {
            get
            {
                return _ListaFrontRearCollarAnillos;
            }
            set
            {
                _ListaFrontRearCollarAnillos = value;
                NotifyChange("ListaFrontRearCollarAnillos");
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

        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; NotifyChange("Descripcion"); }
        }

        private string _MedidaNominal;
        public string MedidaNominal
        {
            get { return _MedidaNominal; }
            set { _MedidaNominal = value; NotifyChange("MedidaNominal"); }
        }

        private string _Notas;
        public string Notas
        {
            get { return _Notas; }
            set { _Notas = value; NotifyChange("Notas"); }
        }

        private string _Parte;
        public string Parte
        {
            get { return _Parte; }
            set { _Parte = value; NotifyChange("Parte"); }
        }


        public ICommand BusquedaFrontRearAnillos
        {
            get
            {
                return new RelayCommand(param => Busqueda((string)param));
            }
        }

        public FrontRearAnillosVM()
        {
            Busqueda(string.Empty);
        }

        public void Busqueda(string TextoBuscar)
        {
            ListaFrontRearCollarAnillos = DataManager.GetAllFrontRearCollarAnillos(TextoBuscar);
        }

    }
}
