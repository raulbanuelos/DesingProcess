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
            ListaSplitter = DataManager.GetAllCutterSplitter(texto);
        }

        /// <summary>
        /// Método que busca un registro de Cutter de acuerdo con el width
        /// </summary>
        private async void obtieneCutter()
        {
            ListaMejores.Clear();
            ListaOptimos.Clear();

            if (diam !=0)
            {
                //Obtiene el herramental
                Herramental obj = DataManager.GetCutterSplitterCasting(diam);
                ObservableCollection<Herramental> ListAux = new ObservableCollection<Herramental>();
                //se agrega a una lista, para convertirlo a datatable
                ListAux.Add(obj);
                //Se convierte la lista en datatable, para mostrarla en pantalla
                ListaOptimos = DataManager.ConverToObservableCollectionHerramental_DataSet(ListAux,"Cutter Splitter");
                ListaMejores = ListaOptimos;

                if(obj.Codigo == null)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHerramental);
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
        }
        #endregion

        #region Constructor

        public CutterSplitterVM()
        {
            //Obtiene todos los registros
            buscarCutter(string.Empty);
            dialog = new DialogService();
            Titulo = "Cutter Splitter";
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
        }
        #endregion
    }
}
