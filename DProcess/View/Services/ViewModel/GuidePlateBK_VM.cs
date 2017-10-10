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
    public class GuidePlateBK_VM : INotifyPropertyChanged
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
        private DataTable _ListaBK;
        public DataTable ListaBK {
            get { return _ListaBK; }
            set { _ListaBK = value;  NotifyChange("ListaBK"); }
        }

        private DataTable _ListaMejores;
        public DataTable ListaMejores {
            get { return _ListaMejores; }
            set { _ListaMejores = value; NotifyChange("ListaMejores"); }
        }

        private DataTable _ListaOptimos;
        public DataTable ListaOptimos {
            get { return _ListaOptimos; }
            set { _ListaOptimos = value; NotifyChange("ListaOptimos"); }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Comando que obtiene los registros buscados.
        /// </summary>
        public ICommand BusquedaBK
        {
            get
            {
                return new RelayCommand(param => busquedaBK((string)param));
            }
        }

        /// <summary>
        /// Comando que buscar las coincidencias de acuerdo a la dimensión.
        /// </summary>
        public ICommand BuscarOptimos
        {
            get
            {
                return new RelayCommand(o => buscarOptimos());
            }
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Método que obtiene los registros.
        /// </summary>
        /// <param name="texto"></param>
        private void busquedaBK(string texto)
        {
            ListaBK = DataManager.GetAllGuidePlate(texto);
        }

        /// <summary>
        /// Método que obtiene un herramental de acuerdo a las dimensiones.
        /// </summary>
        private void buscarOptimos()
        {

        }
        #endregion

        #region Constructor
        public GuidePlateBK_VM()
        {
            dialog = new DialogService();
            busquedaBK(string.Empty);
        }
        #endregion
    }
}
