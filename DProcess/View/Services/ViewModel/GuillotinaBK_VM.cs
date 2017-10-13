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
    public class GuillotinaBK_VM: INotifyPropertyChanged
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
            get { return _ListaBK; }
            set { _ListaBK = value; NotifyChange("ListaBK"); }
        }

        private DataTable _ListaMejores;
        public DataTable ListaMejores
        {
            get { return _ListaMejores; }
            set { _ListaMejores = value; NotifyChange("ListaMejores"); }
        }

        private DataTable _ListaOptimos;
        public DataTable ListaOptimos
        {
            get { return _ListaOptimos; }
            set { _ListaOptimos = value; NotifyChange("ListaOptimos"); }
        }
        //Diametro nominal
        private double _d1;
        public double D1 {
            get { return _d1; }
            set { _d1 = value; NotifyChange("D1"); }
        }

        //Width del anillo
        private double _h1;
        public double H1 {
            get { return _h1; }
            set { _h1 = value; NotifyChange("H1"); }
        }

        private string _titulo;
        public string Titulo { get { return _titulo; } set { _titulo = value; NotifyChange("Titulo"); } }
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
        /// Comando que buscar las coincidencias de acuerdo al diámetro nominal y width del anillo.
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
        /// Método que obtiene todos los registros de GuillotinaBK.
        /// </summary>
        /// <param name="texto"></param>
        private void busquedaBK(string texto)
        {
            //Obtiene los registros que coincidan con la descripción o el código.
            ListaBK = DataManager.GetAllGuillotinaBK(texto);
        }

        /// <summary>
        /// Método que obtiene un herramental de acuerdo a las width y diametro del anillo.
        /// </summary>
        private async void buscarOptimos()
        {
            //Limpiamos las listas
            ListaMejores.Clear();
            ListaOptimos.Clear();

            //Si los parámetros son diferente de cero.
            if (_d1 !=0 & _h1!=0)
            {
                //obtenemos los herramentales más optimos con las condiciones de diametro y width.
                ListaOptimos = DataManager.GetGuillotina(_d1, _h1);
                //Se obtiene el mejor herramental.
                ListaMejores = DataManager.SelectBestBK(ListaOptimos);

                //Si la lista no contiene información.
                if(ListaMejores.Rows.Count == 0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage("Alerta", "No se encontró herramental con estas características..");
            }
            else
                //Si están vacíos muestra un mensaje en pantalla.
                await dialog.SendMessage("Alerta", "Se debe llenar todos los campos...");
        }
        #endregion

        #region MyRegion
        public GuillotinaBK_VM()
        {
            dialog = new DialogService();
            //Se obtiene todos los registros.
            busquedaBK(string.Empty);
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
            Titulo = "Guillotina BK";
        }
        #endregion
    }
}
