using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.ViewModel
{
    class DocumentosVencidosVM : INotifyPropertyChanged
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
        private ObservableCollection<Documento> _Listadocumentos;
        public ObservableCollection<Documento> ListaDocumentos
        {
            get
            {
                return _Listadocumentos;
            }
            set
            {
                _Listadocumentos = value;
                NotifyChange("ListaDocumentos");
            }
        }

        private Documento _selectedDocumento;
        public Documento SelectedDocumento
        {
            get
            {
                return _selectedDocumento;
            }
            set
            {
                _selectedDocumento = value;
                NotifyChange("SelectedDocumento");
            }

        }
        #endregion

        #region Commands 
        #endregion

        #region Methods

        #endregion
        #region Constructor

        public DocumentosVencidosVM(string usuario)
        {
            ListaDocumentos = DataManagerControlDocumentos.GetDocumentos_Vencidos(usuario);
        }
        #endregion
    }
}
