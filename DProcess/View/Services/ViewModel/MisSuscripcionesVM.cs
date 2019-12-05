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
    public class MisSuscripcionesVM : INotifyPropertyChanged
    {
        #region Events INotifyPropertyChanged

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

        #region Atributos

        Usuario User;

        #endregion

        #region Propiedades

        private ObservableCollection<Documento> _ListaDocSuscritos;
        public ObservableCollection<Documento> ListaDocSuscritos
        {
            get
            {
                return _ListaDocSuscritos;
            }
            set
            {
                _ListaDocSuscritos = value;
                NotifyChange("ListaDocSuscritos");
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

        #region Commands

        /// <summary>
        /// Comando para ir a la ventana del documento seleccionado
        /// </summary>
        public ICommand IrVerDocumento
        {
            get
            {
                return new RelayCommand(o => VerDocumento());
            }
        } 

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor para llenar lista de documento seleccionados
        /// </summary>
        /// <param name="usuario"></param>
        public MisSuscripcionesVM(Usuario usuario)
        {
            User = usuario;
            ListaDocSuscritos = DataManagerControlDocumentos.Get_DocSuscripcion(User.NombreUsuario);
            NotifyChange("ListaDocSuscritos");
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para ir a la ventana de documento
        /// </summary>
        private void VerDocumento()
        {
            //Si se seleccionó algún documento
            if (selectedDocumento != null)
            {
                FrmDocumento frm = new FrmDocumento();
                DocumentoViewModel context = new DocumentoViewModel(selectedDocumento, true, User);

                frm.DataContext = context;
                frm.ShowDialog();

                // Volvemos a cargar la lista después de desuscribirse y cerrar la ventana documento
                ListaDocSuscritos = DataManagerControlDocumentos.Get_DocSuscripcion(User.NombreUsuario);
                NotifyChange("ListaDocSuscritos");
            }
        } 

        #endregion
    }
}
