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

        private string _DocumentosValidar;
        public string DocumentosValidar{
            get
            {
                return _DocumentosValidar;
            }
            set
            {
                _DocumentosValidar = value;
                NotifyChange("DocumentosValidar");
            }
        }

        private bool _enabledValidar;
        public bool EnabledValidar
        {
            get
            {
                return _enabledValidar;
            }
            set
            {
                _enabledValidar = value;
                NotifyChange("EnabledValidar");
            }
        }

        private string _DocumentosCorregir;
        public string DocumentosCorregir
        {
            get
            {
                return _DocumentosCorregir;
            }
            set
            {
                _DocumentosCorregir = value;
                NotifyChange("DocumentosCorregir");
            }
        }

        private bool _enabledCorregir;
        public bool EnabledCorregir
        {
            get
            {
                return _enabledCorregir;
            }
            set
            {
                _enabledCorregir = value;
                NotifyChange("EnabledCorregir");
            }
        }

        private string _DocumentosAprobados;
        public string DocumentosAprobados
        {
            get
            {
                return _DocumentosAprobados;
            }
            set
            {
                _DocumentosAprobados = value;
                NotifyChange("DocumentosAprobados");
            }
        }

        private bool _enabledAprobados;
        public bool EnabledAprobados
        {
            get
            {
                return _enabledAprobados;
            }
            set
            {
                _enabledAprobados = value;
                NotifyChange("EnabledAprobados");
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

        public ICommand IrDocumentosValidar
        {
            get
            {
                return new RelayCommand(o => irDocumentosValidar());
            }
        }

        private void irDocumentosValidar()
        {
            DocumentoValidarViewModel o = new DocumentoValidarViewModel(usuario);
            
            FrmDocumentosValidar p = new FrmDocumentosValidar();

            p.DataContext = o;

            p.ShowDialog();
        }

        public ICommand DocumentosPendientes
        {
            get
            {
                return new RelayCommand(o => irDocumentosPendientes());
            }
        }
           
        private void irDocumentosPendientes()
        {
            FrmDocumentosValidar frm = new FrmDocumentosValidar();

            DocumentosPendientesViewM context = new DocumentosPendientesViewM(usuario,"pendientes");

            frm.DataContext = context;

            frm.ShowDialog();
            
        }

        public ICommand IrDocumentosAprobados
        {
            get
            {
                return new RelayCommand(param => irDocumentosAprobados());
            }
        }

        private void irDocumentosAprobados()
        {
            FrmDocumentosValidar frm = new FrmDocumentosValidar();

            DocumentosPendientesViewM context = new DocumentosPendientesViewM(usuario, "aprobados");

            frm.DataContext = context;

            frm.ShowDialog();
        }

        private async void irNuevoDocumento()
        {


            //Obtenermos la cantidad de números de documentosque tiene el usuario sin versión.
            int num_documentos = DataManagerControlDocumentos.GetDocumento_SinVersion(usuario.NombreUsuario).Count;

            if (num_documentos > 0)
            {
                FrmDocumento frm = new FrmDocumento();

                DocumentoViewModel context = new DocumentoViewModel(usuario);

                frm.DataContext = context;

                frm.ShowDialog();

                initControlDocumentos();
            }
            else
            {

                DialogService dialogService = new DialogService();

                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendrá el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = "Crear un nuevo número";
                setting.NegativeButtonText = "Cancelar";

                //Ejecutamos el método para mostrar el mensaje. El resultado lo guardamos en una variable local.
                MessageDialogResult result = await dialogService.SendMessage("Atención", "Usted no tiene ningún número de documento disponible", setting, MessageDialogStyle.AffirmativeAndNegative);
                switch (result)
                {
                    case MessageDialogResult.Negative:
                        break;
                    case MessageDialogResult.Affirmative:
                        FrmGenerador_Numero frmGenerador = new FrmGenerador_Numero();

                        GeneradorViewModel context = new GeneradorViewModel { ModelUsuario = usuario };

                        frmGenerador.DataContext = context;

                        frmGenerador.ShowDialog();

                        break;
                    case MessageDialogResult.FirstAuxiliary:
                        break;
                    case MessageDialogResult.SecondAuxiliary:
                        break;
                    default:
                        break;
                }
            }


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
                DocumentoViewModel context = new DocumentoViewModel(selectedDocumento,true);

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

        private void initSnack()
        {

            int num_validar = DataManagerControlDocumentos.GetDocumentosValidar(usuario.NombreUsuario).Count;
            int num_pendientes= DataManagerControlDocumentos.GetDocumentosPendientes(usuario.NombreUsuario).Count;
            int num_aprobados = DataManagerControlDocumentos.GetDocumentoAprobado(usuario.NombreUsuario).Count;

            if (num_validar>0)
            {
                EnabledValidar = true;
                DocumentosValidar = " " + num_validar + " Documento(s) pendiente(s) por validar";
            }
            if (num_pendientes >0 )
            {
                EnabledCorregir = true;
                DocumentosCorregir = " " + num_pendientes + " Documento(s) pendiente(s) por corregir";
            }
            if (num_aprobados>0)
            {
                EnabledAprobados = true;
                DocumentosAprobados= " " + num_aprobados + " Documento(s) pendiente(s) por liberar";
            }
            
        }

        private void GetDataGrid(string TextoBusqueda)
        {
            Lista = DataManagerControlDocumentos.GetDataGrid(SelectedTipoDocumento.id_tipo, TextoBusqueda);
        } 

        #endregion

        #region Constructors
        public ControlDocumentoViewModel(Usuario modelUsuario)
        {
            usuario = new Usuario();
            usuario = modelUsuario;
            initControlDocumentos();
            initSnack();
            
        }
        #endregion

    }
}
