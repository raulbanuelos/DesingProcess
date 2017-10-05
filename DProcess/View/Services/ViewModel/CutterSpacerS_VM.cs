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
    public class CutterSpacerS_VM : INotifyPropertyChanged
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

        private DataTable _ListaCutterSpacer;
        public DataTable ListaCutterSpacer
        {
            get { return _ListaCutterSpacer; }
            set { _ListaCutterSpacer = value; NotifyChange("ListaCutterSpacer"); }
        }

        private string _proceso;
        public string Proceso
        {
            get { return _proceso; }
            set { _proceso = value; NotifyChange("Proceso"); }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; NotifyChange("Width"); }
        }

        private DataTable _listaOptimos;
        public DataTable ListaOptimos
        {
            get { return _listaOptimos; }
            set { _listaOptimos = value; NotifyChange("ListaOptimos"); }
        }

        private DataTable _listaMejores;
        public DataTable ListaMejorCutter
        {
            get { return _listaMejores; }
            set { _listaMejores = value; NotifyChange("ListaMejorCutter"); }
        }

        private ObservableCollection<Herramental> ListaResultante;

        private ObservableCollection<string> _listaProcesos;
        public ObservableCollection<string> ListaProcesos { get { return _listaProcesos; } set { _listaProcesos = value; NotifyChange("ListaProcesos"); } }
        #endregion

        #region Commands
        /// <summary>
        /// Comando que obtiene los registros buscados
        /// </summary>
        public ICommand BusquedaSpacer
        {
            get
            {
                return new RelayCommand(parametro => buscarSpacer((string)parametro));
            }
        }

        /// <summary>
        /// Comando que buscar las coincidencias de acuerdo a los rangos de a y b
        /// </summary>
        public ICommand BuscarOptimos
        {
            get
            {
                return new RelayCommand(o => bestCutterSpacer());
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Obtiene los registros que coinciden con el texto
        /// </summary>
        /// <param name="texto"></param>
        private void buscarSpacer(string texto)
        {
            ListaCutterSpacer = DataManager.GetAllCutterSpacer(texto);
        }

        /// <summary>
        /// Método que busca un registro de Cutter de acuerdo a las dimesiones
        /// </summary>
        private async void bestCutterSpacer()
        {
            //Valida que los campos no estén vacíos.
            if (!string.IsNullOrEmpty(_proceso) & !string.IsNullOrWhiteSpace(_proceso) & _width!=0)
            {
                //Obtiene la lista de herramentales
                foreach (var item in DataManager.GetSpacerSplitterCastings(_proceso, _width))
                {
                    //Se agrega a una lista tipo ObservableCollection
                    ListaResultante.Add(item);
                }

                //Obtenemos el datatable, para mostrarlo en la ventana
                ListaOptimos = DataManager.ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Spacer Optimos");
                //Obtenemos el mejor herramental
                ListaMejorCutter = DataManager.SelectBestSpacer(ListaOptimos);

                //Si la lista tiene información
                if (ListaMejorCutter.Rows.Count ==0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage("Alerta", "No se encontró herramental con estas características..");
            }
            else
            {
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage("Alerta", "Se debe llenar todos los campos...");
            }
        }
        #endregion

        #region Constructor

        public CutterSpacerS_VM()
        {
            //obtiene todos los registros 
            buscarSpacer(string.Empty);
            dialog = new DialogService();
            ListaResultante = new ObservableCollection<Herramental>();
            ListaProcesos = new ObservableCollection<string>();
            //Agregamos la información de la lista de procesos
            ListaProcesos.Add("Sencillo");
            ListaProcesos.Add("Doble");
            ListaProcesos.Add("Triple");
            ListaProcesos.Add("Cuadruple");
        }
        #endregion
    }
}
