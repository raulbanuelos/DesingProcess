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

        private string _titulo;
        public string Titulo
        {
            get
            {
                return _titulo;
            }
            set
            {
                _titulo = value;
                NotifyChange("Titulo");
            }
        }
        #endregion

        #region Attributes
        public Usuario usuario; 
        #endregion

        #region Constructors
        /// <summary>
        /// ViewModel de ventana que muestra la lista de los documentos que el adminsitrador debe de validar
        /// </summary>
        /// <param name="_usuario"></param>
        public DocumentoValidarViewModel(Usuario _usuario)
        {
            //ViewModel de la ventana que muestra la lista de los documentos que el administrador debe de validar
            usuario = _usuario;
            init();
            
            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Método que muestra la ventana con la información del documento seleccionado
        /// Se muestra ventana para validar el documento
        /// </summary>
        private void abrirDocumento()
        {
            //Si se ha seleccionado un documento
            if (selectedDocumento != null)
            {
                //Obtiene la información de un documento y la versión
                SelectedDocumento = DataManagerControlDocumentos.GetDocumento(SelectedDocumento.id_documento, SelectedDocumento.version.no_version);
                
                FrmValidarDocumento p = new FrmValidarDocumento();

                ValidarDocumentoViewM context = new ValidarDocumentoViewM(SelectedDocumento,usuario);

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
            //Ejecutamos el método para obtener la lista de documentos que el administrador tiene pendientes por validar
            ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentosValidar(usuario.NombreUsuario,"");
            _titulo = "DOCUMENTOS PENDIENTES POR VALIDAR";
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

        /// <summary>
        /// comando para buscar documentos
        /// </summary>
        public ICommand BuscarDocumento
        {
            get
            {
                return new RelayCommand(param => GetDocument((string)param));
            }
        }
        #endregion

        /// <summary>
        /// metodo que obtiene el documento
        /// </summary>
        /// <param name="txt_buscar"></param>
        private void GetDocument(string txt_buscar)
        {
            ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentosValidar(usuario.NombreUsuario, txt_buscar);
        }

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
