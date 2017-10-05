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
   public class CutterSplitterVM : INotifyPropertyChanged
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

        private DataTable _ListaCutterSplitter;
        public DataTable ListaCutterSplitter
        {
            get { return _ListaCutterSplitter; }
            set { _ListaCutterSplitter = value; NotifyChange("ListaCutterSplitter"); }
        }

        private double diam;
        public double Diam
        {
            get { return diam; }
            set { diam = value; NotifyChange("Diam"); }
        }

        private DataTable _listaOptimos;
        public DataTable ListaOptimos
        {
            get { return _listaOptimos; }
            set { _listaOptimos = value; NotifyChange("ListaOptimos"); }
        }

        private DataTable _listaMejores;
        public DataTable ListaMejores
        {
            get { return _listaMejores; }
            set { _listaMejores = value; NotifyChange("ListaMejores"); }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Comando que obtiene los registros buscados
        /// </summary>
        public ICommand BusquedaCutter
        {
            get
            {
                return new RelayCommand(parametro => buscarCutter((string)parametro));
            }
        }

        /// <summary>
        /// Comando que buscar las coincidencias de acuerdo al diametro
        /// </summary>
        public ICommand BuscarOptimos
        {
            get
            {
                return new RelayCommand(o => obtieneCutter());
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Método que obtiene la lista que coincidan con el texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        private void buscarCutter(string texto)
        {
            //obetenemos la lista de Cutter Splitter
            ListaCutterSplitter = DataManager.GetAllCutterSplitter(texto);
        }

        /// <summary>
        /// Método que busca un registro de Cutter de acuerdo al diametro
        /// </summary>
        private void obtieneCutter()
        {
            if (diam !=0)
            {
               
            }
        }
        #endregion

        #region Constructor

        public CutterSplitterVM()
        {
            //Obtiene todos los registros
            buscarCutter(string.Empty);
            dialog = new DialogService();
        }
        #endregion
    }
}
