using Model;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;
using View.Resources;

namespace View.Services.ViewModel
{
    public class CoilCenterG_VM : INotifyPropertyChanged
    {
        #region Attributtes
        DialogService dialog;
        bool banCenter;
        bool banEntrance;
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
        private DataTable listaHerramentales;
        public DataTable ListaHerramentales
        {
            get { return listaHerramentales; }
            set { listaHerramentales = value; NotifyChange("ListaHerramentales"); }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; NotifyChange("Width"); }
        }

        private double _radial;
        public double Radial
        {
            get { return _radial; }
            set { _radial = value; NotifyChange("Radial"); }
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
        /// Comando que busca los registros que coincidan con el texto de búsqueda
        /// </summary>
        public ICommand BusquedaCoil
        {
            get
            {
                return new RelayCommand(parametro => BuscarCoil_Center((string)parametro));
            }
        }

        /// <summary>
        /// Comando que buscar las coincidencias de acuerdo al width y radial
        /// </summary>
        public ICommand BuscarOptimos
        {
            get
            {
                return new RelayCommand(o => buscarOptimos());
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        ///  Método que obtiene la lista que coincidan con el texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        private void BuscarCoil_Center(string texto)
        {
            ListaHerramentales = DataManager.GetALLCOIL_CENTER_GUIDE(texto);
        }

        /// <summary>
        ///  Método que busca un registro de Cutter de acuerdo al diametro y radial
        /// </summary>
        private async void buscarOptimos()
        {
            ListaOptimos = new DataTable();
            ListaMejores = new DataTable();

           Herramental idealCenterGuide = new Herramental();

            if (_width != 0 & _radial != 0)
            {              
                //Obtiene la lista de los herramentales optimos
                ListaOptimos = DataManager.GetCOIL_CENTER_GUIDE(_width, _radial, out idealCenterGuide);
                //obtiene el mejor herramental
                ListaMejores = DataManager.SelectBestCoil(ListaOptimos);

                if (ListaMejores.Rows.Count == 0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHerramental);
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
        }
        #endregion

        #region Constructor

        public CoilCenterG_VM(bool _banCenter, bool _banEntrace)
        {
            banCenter = _banCenter;
            banEntrance = _banEntrace;
            BuscarCoil_Center(string.Empty);
            dialog = new DialogService();
        }
        #endregion
    }
}
