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
    public class CollarSpacerVM : INotifyPropertyChanged
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

        private double _SmallOD;
        public double SmallOD
        {
            get { return _SmallOD; }
            set { _SmallOD = value; NotifyChange("SmallOD"); }
        }

        private double _pc;
        public double PC
        {
            get { return _pc; }
            set { _pc = value; NotifyChange("PC"); }
        }

        private string titulo;
        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; NotifyChange("Titulo"); }

         }

        private ObservableCollection<Herramental> ListaAux;
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


        #endregion}

        #region Methods

        /// <summary>
        /// Método que obtiene la lista que coincidan con el texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        private void busquedaCamTurn(string texto)
        {
           ListaCamTurn= DataManager.GetAllCollarSpacer(texto);
        }

        /// <summary>
        /// Método que busca los herramentales más óptimos de acuerdo al diam y gap
        /// </summary>
        private async void buscarOptimos()
        {
            //Limpiamos las listas
            ListaMejores.Clear();
            ListaOptimos.Clear();

            //Si las variables son diferentes de cero.
            if (_SmallOD !=0 & _pc !=0)
            {
                //Ejecutamos el método para buscar los collarines optimos.
                ListaAux = DataManager.GetCollarSpacer(_SmallOD,PC);
                //Convertimos la lista en datatable.
                ListaOptimos= DataManager.ConverToObservableCollectionHerramental_DataSet(ListaAux, "CollarSpacer");
                //Ejecutamos el método para seleccionar la mejor opción de collarines.
                ListaMejores = DataManager.SelectBestCollarSpacer(ListaAux);

                //Verificamos que la cantidad de mejores herramentales sea mayor a cero.
                if (ListaMejores.Rows.Count ==0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHerramental);
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
        }
        #endregion

        #region Constructor

        public CollarSpacerVM()
        {
            //Obtiene la lista de todos los registros
            busquedaCamTurn(string.Empty);
            dialog = new DialogService();
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
            Titulo = "Collar Spacer";
        }
        #endregion
    }
}
