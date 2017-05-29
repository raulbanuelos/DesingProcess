using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.ControlDocumentos;

namespace View.Services.ViewModel
{
    public class DocumentoValidarViewModel : INotifyPropertyChanged
    {
        #region Properties
        public ObservableCollection<Documento> ListaDocumentosValidar { get; set; }

        private Documento selectedDocumento;
        public Documento SelectedDocumento
        {
            get
            {
                return selectedDocumento;
            }
            set
            {
                selectedDocumento = value;
                NotifyChange("SelectedDocumento");
            }
        }
        #endregion

        #region Attributes
        public Usuario usuario; 
        #endregion

        #region Constructors
        public DocumentoValidarViewModel(Usuario _usuario)
        {
            usuario = _usuario;
            init();
            
        }
        #endregion

        #region Methods
        private void abrirDocumento()
        {
            
            if (selectedDocumento != null)
            {
                SelectedDocumento = DataManagerControlDocumentos.GetDocumento(SelectedDocumento.id_documento, SelectedDocumento.version.no_version);
                
                FrmValidarDocumento p = new FrmValidarDocumento();

                p.DataContext = this;

                p.ShowDialog();

                init();

            }
        }

        private void init()
        {
            ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentosValidar(usuario.NombreUsuario);
        }
        #endregion

        #region Commands

        public ICommand AbrirDocumento
        {
            get
            {
                return new RelayCommand(o => abrirDocumento());
            }
        } 
        #endregion

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
    }
}
