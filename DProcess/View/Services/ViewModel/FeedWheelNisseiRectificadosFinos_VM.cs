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
    public class FeedWheelNisseiRectificadosFinos_VM : INotifyPropertyChanged
    {
        #region Attributtes
        DialogService dialog;
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

        #region Propiedades

        private DataTable _ListaHerramentales;
        public DataTable ListaHerramentales
        {
            get
            {
                return _ListaHerramentales;
            }
            set
            {
                _ListaHerramentales = value;
                NotifyChange("ListaHerramentales");
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

        private double _diam;
        public double Diam
        {
            get { return _diam; }
            set { _diam = value; NotifyChange("Diam"); }
        }

        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; NotifyChange("Titulo"); }
        }

        private string _texto;
        public string Texto
        {
            get { return _texto; }
            set { _texto = value; NotifyChange("Texto"); }
        }

        #endregion

        #region Constructor
        public FeedWheelNisseiRectificadosFinos_VM()
        {
            busquedaherramental(string.Empty);
        }
        #endregion

        #region Comandos
        
        public ICommand BusquedaHerramental
        {
            get
            {
                return new RelayCommand(a => busquedaherramental((string)a));
            }
        }


        #endregion

        #region Métodos

        /// <summary>
        /// Método para buscar un registro
        /// </summary>
        /// <param name="TextoBuscar"></param>
        public void busquedaherramental(string TextoBuscar)
        {
            ListaHerramentales = DataManager.GetAllNisseiRectificadosFinos(TextoBuscar);
        }

        #endregion
    }
}
