using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    public class HistorialVM : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region Propiedades
        private ObservableCollection<HistorialVersion> _ListaDocumentos;
        public ObservableCollection<HistorialVersion> ListaDocumentos {
            get
            {
                return _ListaDocumentos;
            }
            set
            {
                _ListaDocumentos = value;
                NotifyChange("ListaDocumentos");
            }
        }

        private ObservableCollection<HistorialVersion> _listaHistorial;
        public ObservableCollection<HistorialVersion> ListaHistorial
        {
            get
            {
                return _listaHistorial;
            }
            set
            {
                _listaHistorial = value;
                NotifyChange("ListaHistorial");
            }
        }

        private HistorialVersion _SelectedVersion;
        public HistorialVersion SelectedVersion {
            get
            {
                return _SelectedVersion;
            }
            set
            {
                _SelectedVersion = value;
                NotifyChange("SelectedVersion");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Comando para ver el historial de una version
        /// </summary>
        public ICommand VerHistorial
        {
            get
            {
                return new RelayCommand(o => verHistorial());
            }
        }

        /// <summary>
        /// comando para buscar el nombre de un documento
        /// </summary>
        public ICommand BuscarDocumento
        {
            get
            {
                return new RelayCommand(param => buscaDocumento((string)param));
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Funcion que llena la lista del historial de acuerdo a la version de un documento
        /// </summary>
        private void verHistorial()
        {
            //Si la version seleccionada es diferente de nulo
            if (SelectedVersion != null)
            {
                //Obtiene el historial de la version
                ListaHistorial = DataManagerControlDocumentos.GetHistorial_Version(SelectedVersion.nombre_documento, SelectedVersion.no_version);
            }
        }

        /// <summary>
        /// Método para obtener la lista de los documentos con historial, o el historial que se busca
        /// </summary>
        /// <param name="nombreDoc"></param>
        public void GetHistorial(string nombreDoc)
        {
            ListaDocumentos = DataManagerControlDocumentos.GetHistorial(nombreDoc);
            
        }
        /// <summary>
        /// Método que obtiene los documentos, y lista la lista de historial
        /// </summary>
        /// <param name="parametro"></param>
        private void buscaDocumento(string parametro)
        {
            //Obtiene coincidencias del parametro
            GetHistorial(parametro);

            if(ListaHistorial !=null)
            ListaHistorial.Clear();
        }
        #endregion

        #region Constructor

        public HistorialVM()
        {
            //Obetenemos todos los documentos
            GetHistorial(string.Empty);
        }
        #endregion
    }
}
