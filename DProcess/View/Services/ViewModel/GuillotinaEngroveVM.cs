using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Model;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    public class GuillotinaEngroveVM : INotifyPropertyChanged
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
        private DataTable _ListaHerramentalesGuillotinaEngrove;
        public DataTable ListaHerramentalesGuillotinaEngrove
        {
            get
            {
                return _ListaHerramentalesGuillotinaEngrove;
            }
            set
            {
                _ListaHerramentalesGuillotinaEngrove = value;
                NotifyChange("ListaHerramentalesGuillotinaEngrove");
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

        private double _Dimension;
        public double Dimension
        {
            get { return _Dimension; }
            set { _Dimension = value; NotifyChange("Dimension"); }
        }

        private string _Detalle;
        public string Detalle
        {
            get { return _Detalle; }
            set { _Detalle = value; NotifyChange("Detalle"); }
        }
        #endregion

        public ICommand BusquedaGuillotina
        {
            get
            {
                return new RelayCommand(param => Busqueda((string)param));
            }
        }

        #region Constructor
        public GuillotinaEngroveVM()
        {
            Busqueda(string.Empty);
        }
        #endregion

        #region Comandos
        #endregion

        #region Métodos

        public void Busqueda(string TextoBuscar)
        {
            ListaHerramentalesGuillotinaEngrove = DataManager.GetAllGuillotinaEngrave_(TextoBuscar);
        }
        #endregion
    }
}
