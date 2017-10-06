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

namespace View.Services.ViewModel
{
    public class UretanoSplitterVM : INotifyPropertyChanged
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

        private DataTable _ListaSplitter;
        public DataTable ListaSplitter
        {
            get { return _ListaSplitter; }
            set { _ListaSplitter = value; NotifyChange("ListaSplitter"); }
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

        private string _titulo;
        public string Titulo { get { return _titulo; } set { _titulo = value; NotifyChange("Titulo"); } }
        #endregion

        #region Commands

        #endregion

        #region Methods
        /// <summary>
        /// Comando que obtiene los registros buscados
        /// </summary>
        public ICommand BusquedaCutter
        {
            get
            {
                return new RelayCommand(parametro => buscaUretanoSplitter((string)parametro));
            }
        }

        /// <summary>
        /// Comando que buscar las coincidencias de acuerdo al diametro
        /// </summary>
        public ICommand BuscarOptimos
        {
            get
            {
                return new RelayCommand(o => obtieneUretanoSplitter());
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="texto"></param>
        public void buscaUretanoSplitter(string texto)
        {
            ListaSplitter = DataManager.GetAllUretano(texto);
        }
        /// <summary>
        /// 
        /// </summary>
        public async void obtieneUretanoSplitter()
        {
            ListaMejores.Clear();
            ListaOptimos.Clear();

            if (diam !=0)
            {              
                ObservableCollection<Herramental> ListAux = new ObservableCollection<Herramental>();
                //Obtenemos el herramental
                Herramental uretano = DataManager.GetUretanoSplitter(diam);
                //Agregamos a la lista auxiliar elherramental que se obtuvo
                ListAux.Add(uretano);

                //Convierte la lista a Datatable, para mostrarla en pantalla
                ListaOptimos = DataManager.ConverToObservableCollectionHerramental_DataSet(ListAux, "Uretano_Splitter");
                ListaMejores = ListaOptimos;

                if (uretano.Codigo == null)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage("Alerta", "No se encontró herramental con estas características..");
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage("Alerta", "Se debe llenar todos los campos...");
        }
        #endregion

        #region Constructor

        public UretanoSplitterVM()
        {
            buscaUretanoSplitter(string.Empty);
            dialog = new DialogService();
            Titulo = "Uretano Splitter";
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
        }
        #endregion
    }
}
