using Model.ControlDocumentos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System;
using View.Forms.ControlDocumentos;

namespace View.Services.ViewModel
{
    public class ControlDocumentoViewModel : INotifyPropertyChanged
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

        private ObservableCollection<Documento> _Lista;
        public ObservableCollection<Documento> Lista {
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

        private ObservableCollection<TipoDocumento> _ListaTipoDocumento;
        public ObservableCollection<TipoDocumento> ListaTipoDocumento
        {
            get
            {
                return _ListaTipoDocumento;
            }
            set
            {
                _ListaTipoDocumento = value;
                NotifyChange("ListaTipoDocumento");
            }
        }

        private TipoDocumento selectedTipoDocumento;
        public TipoDocumento SelectedTipoDocumento
        {
            get {
                return selectedTipoDocumento;
            }
            set {
                selectedTipoDocumento = value;
                NotifyChange("SelectedTipoDocumento");
            }
        }

        private Documento selectedDocumento;
        public Documento SelectedDocumento {
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
        public ICommand IrNuevoDocumento
        {
            get
            {
                return new RelayCommand(o => irNuevoDocumento());
            }
        }

        public ICommand ConsultarDocumentos
        {
            get
            {
                return new RelayCommand(o => GetDataGrid(string.Empty));
            }
        }

        public ICommand BuscarDocumentos
        {
            get
            {
                return new RelayCommand(param => GetDataGrid((string)param));
            }
        }

        public ICommand EditarDocumento
        {
            get
            {
                return new RelayCommand(o => editarDocumento());
            }
        }

        private void irNuevoDocumento()
        {
            FrmDocumento frm = new FrmDocumento();
            //DocumentoViewModel context = new DocumentoViewModel("IF00-0093", "2", "2", "Ayuda Visual Especificaciones de abertura de máquina H", 1026);
            DocumentoViewModel context = new DocumentoViewModel();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        private void editarDocumento()
        {

            FrmDocumento frm = new FrmDocumento();
            DocumentoViewModel context = new DocumentoViewModel(selectedDocumento.nombre, selectedDocumento.version.no_version, Convert.ToString( selectedDocumento.version.no_copias),selectedDocumento.descripcion, selectedDocumento.id_documento,selectedDocumento.id_dep,selectedDocumento.version.id_version);
            frm.DataContext = context;
            frm.ShowDialog();
        }
        #endregion

        private void GetDataGrid(string TextoBusqueda)
        {
           Lista = DataManagerControlDocumentos.GetDataGrid(SelectedTipoDocumento.id_tipo,TextoBusqueda);
        }

        public ControlDocumentoViewModel()
        {
            _ListaTipoDocumento = DataManagerControlDocumentos.GetTipo();

            if (_ListaTipoDocumento.Count > 0)
            {
                SelectedTipoDocumento = _ListaTipoDocumento[0];
            }
            GetDataGrid(string.Empty);
        }
    }
}
