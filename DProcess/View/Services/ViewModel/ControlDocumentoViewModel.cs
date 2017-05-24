using Model.ControlDocumentos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System;
using View.Forms.ControlDocumentos;
using System.Data;
using Model;
using MahApps.Metro.Controls.Dialogs;

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
        public Usuario usuario;
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

        public ICommand Exportar
        {
            get
            {
                return new RelayCommand(o => ExportarExcel());
            }
        }

        public ICommand Generador
        {
            get
            {
                return new RelayCommand(o => GenerarNumero());
            }
        }

        private void irNuevoDocumento()
        {

            FrmDocumento frm = new FrmDocumento();

            DocumentoViewModel context = new DocumentoViewModel(usuario);

            frm.DataContext = context;

            frm.ShowDialog();

            initControlDocumentos();
        }

        private void GenerarNumero()
        {
            FrmGenerador_Numero frmGenerador = new FrmGenerador_Numero();

            GeneradorViewModel context = new GeneradorViewModel { ModelUsuario = usuario };

            frmGenerador.DataContext = context;

            frmGenerador.ShowDialog();
        }

        private async void ExportarExcel(){

           DataSet ds = new DataSet();
           DataTable table = new DataTable();
          
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            if (Lista.Count != 0)
            {
                table.Columns.Add("Numero de Documento");
                table.Columns.Add("Nombre de Documento");
                table.Columns.Add("Version");
                table.Columns.Add("Copias");
                table.Columns.Add("Responsable");
                table.Columns.Add("Fecha de Emision");
                table.Columns.Add("Fecha de Revision");

                foreach (var item in Lista)
                {
                    DataRow newRow = table.NewRow();

                    newRow["Numero de Documento"] = item.nombre;
                    newRow["Nombre de Documento"] = item.descripcion;
                    newRow["Version"] = item.version.no_version;
                    newRow["Copias"] = item.version.no_copias;
                    newRow["Responsable"] = item.Departamento;
                    newRow["Fecha de Emision"] = item.fecha_emision.ToShortDateString();
                    newRow["Fecha de Revision"] = item.fecha_actualizacion.ToShortDateString();

                    table.Rows.Add(newRow);
                }

                ds.Tables.Add(table);

                string e = ExportToExcel.Export(ds);

                if (e != null)
                {
                    await dialog.SendMessage("Alerta", e);
                }
            }
            
        }
        private void editarDocumento()
        {
            if (selectedDocumento != null)
            {
                FrmDocumento frm = new FrmDocumento();
                DocumentoViewModel context = new DocumentoViewModel(selectedDocumento.nombre, selectedDocumento.version.no_version, Convert.ToString(selectedDocumento.version.no_copias), selectedDocumento.descripcion, selectedDocumento.id_documento, selectedDocumento.id_dep, selectedDocumento.version.id_version, selectedDocumento.fecha_actualizacion);

                frm.DataContext = context;

                frm.ShowDialog();

                initControlDocumentos();
            }
        }
        #endregion

        #region Methods

        private void initControlDocumentos()
        {
            _ListaTipoDocumento = DataManagerControlDocumentos.GetTipo();

            if (_ListaTipoDocumento.Count > 0)
            {
                SelectedTipoDocumento = _ListaTipoDocumento[0];
            }
            GetDataGrid(string.Empty);
        }

        private void GetDataGrid(string TextoBusqueda)
        {
            Lista = DataManagerControlDocumentos.GetDataGrid(SelectedTipoDocumento.id_tipo, TextoBusqueda);
        } 

        #endregion

        #region Constructors
        public ControlDocumentoViewModel()
        {
            initControlDocumentos();
        }
        #endregion

    }
}
