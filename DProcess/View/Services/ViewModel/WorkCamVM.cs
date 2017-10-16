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
    public class WorkCamVM : INotifyPropertyChanged
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

        private DataTable _ListaCamTurn;
        public DataTable ListaCamTurn
        {
            get
            {
                return _ListaCamTurn;
            }
            set
            {
                _ListaCamTurn = value;
                NotifyChange("ListaCamTurn");
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

        private double diam;
        public double Diam
        {
            get { return diam; }
            set { diam = value; NotifyChange("Diam"); }
        }

        private double gap;
        public double Gap
        {
            get { return gap; }
            set { gap = value; NotifyChange("Gap"); }
        }

        private string titulo;
        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; NotifyChange("Titulo"); }

        }
        #endregion

        #region Commands
        /// <summary>
        /// Comando que obtiene los registros buscados
        /// </summary>
        public ICommand BusquedaCamTurn
        {
            get
            {
                return new RelayCommand(param => busquedaCamTurn((string)param));
            }
        }

        /// <summary>
        /// Comando que buscar las coincidencias de acuerdo a las dimensiones
        /// </summary>
        public ICommand BuscarOptimos
        {
            get
            {
                return new RelayCommand(o => buscarOptimos());
            }
        }


        #endregion

        #region Methods

        /// <summary>
        /// Método que obtiene la lista que coincidan con el texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        private void busquedaCamTurn(string texto)
        {
           ListaCamTurn= DataManager.GetAllWorkCam(texto);
        }

        /// <summary>
        /// Método que busca los herramentales más óptimos de acuerdo a..
        /// </summary>
        private void buscarOptimos()
        {

        }
        #endregion

        #region Constructor
        public WorkCamVM()
        {
            //Obtiene la lista de todos los registros
            busquedaCamTurn(string.Empty);
            dialog = new DialogService();
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
            Titulo = "Work Cam";
        }
        #endregion
    }

}
