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
   public class CutterCamTurnVM : INotifyPropertyChanged
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

        private ObservableCollection<Material> _ListaMaterial;
        public ObservableCollection<Material> ListaMaterial
        {
            get { return _ListaMaterial; }
            set { _ListaMaterial = value; NotifyChange("ListaMaterial"); }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; NotifyChange("Width"); }
        }

        private Material _SelectedMaterial;
        public Material SelectedMaterial
        {
            get { return _SelectedMaterial; }
            set { _SelectedMaterial = value; NotifyChange("SelectedMaterial"); }

        }
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


        #endregion

        #region Methods

        /// <summary>
        /// Método que obtiene la lista que coincidan con el texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        private void busquedaCamTurn(string texto)
        {
           ListaCamTurn= DataManager.GetAllCutterCamTurn(texto);
        }

        /// <summary>
        /// Método que busca los herramentales más óptimos de acuerdo a..
        /// </summary>
        private async void buscarOptimos()
        {
            //Limpiamos las listas.
            ListaMejores.Clear();
            ListaOptimos.Clear();

            //Si los campos son difrentes de nulo o cero.
            if (SelectedMaterial!=null & Width!=0)
            {
                //Obtenemos la lista de los herramentales optimos.
                ListaOptimos = DataManager.GetCutterCamTurn(SelectedMaterial.id_material, _width);
                //Obtenemos la lista del mejor herramental.
                ListaMejores = DataManager.SelectBestCutterCT(ListaOptimos);

                //Si la lista no tiene información.
                if (ListaMejores.Rows.Count == 0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHerramental);
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage(StringResources.ttlAlerta ,StringResources.msgFillFlields);
        }
        #endregion

        #region Constructor

        public CutterCamTurnVM()
        {
            //Obtiene la lista de todos los registros
            busquedaCamTurn(string.Empty);
            dialog = new DialogService();
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
            ListaMaterial = DataManager.GetAllMaterial();
        }
        #endregion
    }
}
