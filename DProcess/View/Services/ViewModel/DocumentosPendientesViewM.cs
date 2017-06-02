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
    class DocumentosPendientesViewM : INotifyPropertyChanged
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

        #region propiedades
        public Usuario usuario;

        public ObservableCollection<Documento> ListaDocumentosValidar { get; set; }


        private bool _gridUsuario;
        public bool GridUsuario
        {
            get
            {
                return _gridUsuario;
            }
            set
            {
                _gridUsuario = value;
                NotifyChange("GridUsuario");
            }
        }

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

        private Documento documento;
        #endregion

        #region constructor

        public DocumentosPendientesViewM(Usuario user)
        {
            usuario = user;
         
            inicializa();
        }
        #endregion

        #region commands
        public ICommand AbrirDocumento
        {
            get
            {
                return new RelayCommand(o => modificarDocumento());
            }
        }

        private void modificarDocumento()
        {
            if (selectedDocumento != null)
            {
                documento = DataManagerControlDocumentos.GetDocumento(SelectedDocumento.id_documento, SelectedDocumento.version.no_version);

                DocumentoViewModel viewM = new DocumentoViewModel(documento,false);

                FrmDocumento frm = new FrmDocumento();

                frm.DataContext = viewM;

                frm.ShowDialog();

                inicializa();
            }
        }

        #endregion

        public void inicializa()
        {
            ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentosPendientes(usuario.NombreUsuario);
        }

    }
}
