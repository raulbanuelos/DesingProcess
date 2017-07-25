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
    public class VersionesVM : INotifyPropertyChanged
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

        private ObservableCollection<Model.ControlDocumentos.Version> _Lista;
        public ObservableCollection<Model.ControlDocumentos.Version> Lista
        {
            get
            {
                return _Lista;
            }
            set
            {
                _Lista = value;
                NotifyChange("Lista");
            }
        }


        #endregion
        #region Constructor
        public VersionesVM(Documento objDocument)
        {
            Lista = DataManagerControlDocumentos.GetVersiones_Documento(objDocument.id_documento);


        }
        #endregion

        #region Metodos

        /// <summary>
        /// Comando para visualizar el archivo especificado
        /// </summary>
        public ICommand verArchivo
        {
            get
            {
               return new RelayCommand(o => abrirArchivo());
            }
        }

        /// <summary>
        /// Método que abre el archivo de una versión
        /// </summary>
        private void abrirArchivo()
        {

        }
        #endregion
    }
}
