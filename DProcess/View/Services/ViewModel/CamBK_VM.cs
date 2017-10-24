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
    public class CamBK_VM : INotifyPropertyChanged
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
        public DataTable ListaBK
        {
            get
            {
                return _ListaBK;
            }
            set
            {
                _ListaBK = value;
                NotifyChange("ListaBK");
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

        #endregion

        #region Commands
        /// <summary>
        /// Comando que obtiene los registros buscados
        /// </summary>
        public ICommand BusquedaBK
        {
            get
            {
                return new RelayCommand(param => busquedaBK((string)param));
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
        private void busquedaBK(string texto)
        {
            ListaBK = DataManager.GetAllCamBK(texto);
        }

        /// <summary>
        /// Método que busca los herramentales más óptimos de acuerdo al diam y gap
        /// </summary>
        private async void buscarOptimos()
        {
            //Limpiamos las listas
            ListaMejores.Clear();
            ListaOptimos.Clear();

            //Si las variables son diferentes de cero
            if (diam != 0 & gap != 0)
            {
                //Ejecutamos el método para buscar los collarines optimos.
               

                //Ejecutamos el método para seleccionar la mejor opción de collarines.
           

                //Verificamos que la cantidad de mejores herramentales sea mayor a cero.
                if (ListaOptimos.Rows.Count == 0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage("Alerta", "No se encontró herramental con estas características..");
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage("Alerta", "Se debe llenar todos los campos...");
        }
        #endregion

        #region Constructor

        public CamBK_VM()
        {
            dialog = new DialogService();
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
            busquedaBK(string.Empty);
        }
        #endregion
    }
}
