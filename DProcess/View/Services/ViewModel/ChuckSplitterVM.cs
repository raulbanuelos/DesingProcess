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
using View.Resources;

namespace View.Services.ViewModel
{
    public class ChuckSplitterVM : INotifyPropertyChanged
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

        /// <summary>
        /// Comando que obtiene los registros buscados
        /// </summary>
        public ICommand BusquedaCutter
        {
            get
            {
                return new RelayCommand(parametro => buscaChuckSplitter((string)parametro));
            }
        }

        /// <summary>
        /// Comando que buscar las coincidencias de acuerdo al diametro
        /// </summary>
        public ICommand BuscarOptimos
        {
            get
            {
                return new RelayCommand(o => obtieneChuckSplitter());
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Método que obtiene la lista que coincidan con el texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        private void buscaChuckSplitter(string texto)
        {
            ListaSplitter = DataManager.GetAllChuckSplitter(texto);
        }

        /// <summary>
        /// Método que busca el mejor herramental de acuerdo con el width
        /// </summary>
        private async void obtieneChuckSplitter()
        {
            ListaMejores.Clear();
            ListaOptimos.Clear();

            if (diam !=0)
            {
                ObservableCollection<Herramental> ListAux = new ObservableCollection<Herramental>();
                //Obtenemos el herramental
                Herramental chuck = DataManager.GetChuckSplitter(diam);
                //Agregamos a la lista auxiliar elherramental que se obtuvo
                ListAux.Add(chuck);

                //Convierte la lista a Datatable, para mostrarla en pantalla
                ListaOptimos = DataManager.ConverToObservableCollectionHerramental_DataSet(ListAux, "Chuck_Splitter");
                ListaMejores = ListaOptimos;

                if (chuck.Codigo == null)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHerramental);
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
        }
        #endregion

        #region Constructor

        public ChuckSplitterVM()
        {
            buscaChuckSplitter(string.Empty);
            dialog = new DialogService();
            Titulo = "Chuck Splitter";
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
        }
        #endregion
    }
}
