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
   public class Buscar_DocumentoVM : INotifyPropertyChanged
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
        private ObservableCollection<Documento> _ListaDocumentos;
        public ObservableCollection<Documento> ListaDocumentos
        {
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
        #endregion

        #region Constructor

        public Buscar_DocumentoVM()
        {
            //Inicializa la lista de documentos
            GetGrid(string.Empty);
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Función que obtiene todos los documentos liberados, los asigna a la lista para mostrar en el dataGrid
        /// </summary>
        /// <param name="texto"></param>
        private void GetGrid(string texto)
        {
            ListaDocumentos = DataManagerControlDocumentos.GetGridDocumentos(texto);
        }

        /// <summary>
        /// Comando que busca un archivo de la lista
        /// Recibe como parámetro la palabra a buscar
        /// </summary>
        public ICommand BuscarDocumentos
        {
            get
            {
                return new RelayCommand(param => GetGrid((string)param));
            }
        }
        #endregion
    }
}
