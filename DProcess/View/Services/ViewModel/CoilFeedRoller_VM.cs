using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Resources;

namespace View.Services.ViewModel
{
   public class CoilFeedRoller_VM : INotifyPropertyChanged
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
                return new RelayCommand(parametro => BuscarCoil_Feed((string)parametro));
            }
        }

        /// <summary>
        /// Comando que buscar las coincidencias de acuerdo al width
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
        private void BuscarCoil_Feed(string texto)
        {
            ListaHerramentales = DataManager.GetAllCOIL_Feed_Roller(texto);
        }

        /// <summary>
        /// Método que busca el óptimo con el width
        /// </summary>
        private async void buscarOptimos()
        {
            if (_width != 0)
            {
                ListaOptimos = new DataTable();
                ListaMejores = new DataTable();
                //Obtiene la lista de los herramentales optimos
                ListaOptimos = DataManager.GetCOIL_Feed_Roller(Width);
                //Obtiene la lista del mejor herramental
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

        public CoilFeedRoller_VM()
        {
            BuscarCoil_Feed(string.Empty);
            dialog = new DialogService();
        }
        #endregion
    }
}
