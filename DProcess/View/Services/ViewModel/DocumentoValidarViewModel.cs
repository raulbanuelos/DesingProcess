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
        private ObservableCollection<Documento> _ListaDocumentos;
        public ObservableCollection<Documento> ListaDocumentosValidar {
            get
            {
                return _ListaDocumentos;
            }
            set
            {
                _ListaDocumentos = value;
                NotifyChange("ListaDocumentosValidar");
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
        /// <summary>
        /// Método que muestra la ventana con la información del documento seleccionado
        /// 
        /// </summary>
        private void abrirDocumento()
        {
            //Si se ha seleccionado un documento
            if (selectedDocumento != null)
            {
                //Obtiene la información de un documento y la versión
                SelectedDocumento = DataManagerControlDocumentos.GetDocumento(SelectedDocumento.id_documento, SelectedDocumento.version.no_version);
                
                FrmValidarDocumento p = new FrmValidarDocumento();

                ValidarDocumentoViewM context = new ValidarDocumentoViewM(SelectedDocumento);

                p.DataContext = context;

                p.ShowDialog();

                init();

            }
        }

        /// <summary>
        /// Inicializa la lista de los documentos para mostrar en la tabla
        /// </summary>
        private void init()
        {
            //Ejecutamos el método para obtener la lista de documentos pendientes por validar
            ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentosValidar(usuario.NombreUsuario);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Comando para visualizar el documento seleccioando
        /// </summary>
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
