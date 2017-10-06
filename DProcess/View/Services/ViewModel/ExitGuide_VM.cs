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
    public class ExitGuide_VM : INotifyPropertyChanged
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
        /// Comando que busca un registro de Coil
        /// </summary>
        public ICommand BusquedaCoil
        {
            get
            {
                return new RelayCommand(parametro => BuscarExit_Guide((string)parametro));
            }
        }

        /// <summary>
        /// Comando que busca los registros de acuerdo al diametro y radial
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
        /// Método que busca la coincidencia de los registros de coil
        /// </summary>
        /// <param name="texto"></param>
        private void BuscarExit_Guide(string texto)
        {
            ListaHerramentales = DataManager.GetAllEXIT_GUIDE(texto);
        }
        /// <summary>
        /// Método que busca coil que coincidan con los rangos de width y radial
        /// </summary>
        private async void buscarOptimos()
        {

            if (Width != 0 & Radial != 0)
            {
                ListaOptimos = new DataTable();
                ListaMejores = new DataTable();
                //Obtiene la lista de los mejores herramentales de acuerdo a width y radial
                ListaOptimos = DataManager.GetEXIT_GUIDE(_width, _radial);
                //Obtiene el mejor herramental
                ListaMejores = DataManager.SelectBestCoil(ListaOptimos);

                if (ListaMejores.Rows.Count == 0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage("Alerta", "No se encontró herramental con estas caracteristicas");
            }
             else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage("Alerta", "Se debe llenar todos los campos...");
        }
        #endregion

        #region constructor

        public ExitGuide_VM()
        {
            BuscarExit_Guide(string.Empty);
            dialog = new DialogService();
        }
        #endregion

    }
}
