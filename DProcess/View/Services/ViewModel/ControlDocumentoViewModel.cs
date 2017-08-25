using Model.ControlDocumentos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System;
using View.Forms.ControlDocumentos;
using System.Data;
using Model;
using MahApps.Metro.Controls.Dialogs;
using Encriptar;

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
        private bool _enabled_pendientesLiberar;
        public bool EnabledPendientes_Liberar
        {
            get
            {
                return _enabled_pendientesLiberar;
            }
            set
            {
               _enabled_pendientesLiberar = value;
                NotifyChange("EnabledPendientes_Liberar");
            }
        }

        private string _pendientesLiberar;
        public string PendientesLiberar {
            get
            {
                return _pendientesLiberar;
            }
            set
            {
                _pendientesLiberar = value;
                NotifyChange("PendientesLiberar");
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

        private bool bttnEnabled=false;
        public bool BttnEnabled
        {
            get
            {
                return bttnEnabled;
            }
            set
            {
                bttnEnabled = value;
                NotifyChange("BttnEnabled");
            }
        }

        private int num_pendientes { get; set; }

        private string _textoBuscar;
        public string TextoBuscar
        {
            get
            {
                return _textoBuscar;
            }
            set
            {
                _textoBuscar = value;
                NotifyChange("TextoBuscar");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Comando que muestra la ventana para crear un documento o generar un número de documento
        /// </summary>
        public ICommand IrNuevoDocumento
        {
            get
            {
                return new RelayCommand(o => irNuevoDocumento());
            }
        }

        /// <summary>
        /// Comando que filtra los documentos por tipo
        /// </summary>
        public ICommand ConsultarDocumentos
        {
            get
            {
                return new RelayCommand(o => GetDataGrid(string.Empty));
            }
        }

        /// <summary>
        /// Comando que al cambiar el textBox, busca un archivo de la lista
        /// Recibe como parámetro la palabra a buscar
        /// </summary>
        public ICommand BuscarDocumentos
        {
            get
            {
                return new RelayCommand(param => GetDataGrid((string)param));
            }
        }

        /// <summary>
        /// Comando para visualizar el documento seleccionado
        /// </summary>
        public ICommand EditarDocumento
        {
            get
            {
                return new RelayCommand(o => editarDocumento());
            }
        }

        /// <summary>
        /// Comando para crear un archivo excel de la lista de los documentos
        /// </summary>
        public ICommand Exportar
        {
            get
            {
                return new RelayCommand(o => ExportarExcel());
            }
        }

        /// <summary>
        /// Comando para generar un nuevo número de documento
        /// </summary>
        public ICommand Generador
        {
            get
            {
                return new RelayCommand(o => GenerarNumero());
            }
        }

        /// <summary>
        ///  Comando para mostrar la ventana de los documentos que tiene que validar el Administrador
        /// </summary>
        public ICommand IrDocumentosValidar
        {
            get
            {
                return new RelayCommand(o => irDocumentosValidar());
            }
        }

        /// <summary>
        /// Método que muestra la ventana de documentos que tiene que validar el Administrador
        /// </summary>
        private void irDocumentosValidar()
        {
            DocumentoValidarViewModel o = new DocumentoValidarViewModel(usuario);
            
            FrmDocumentosValidar p = new FrmDocumentosValidar();

            p.DataContext = o;

            p.ShowDialog();

            initSnack();
        }

        /// <summary>
        ///  Comando para mostrar la ventana de los documentos que están pendientes por corregir
        /// </summary>
        public ICommand DocumentosPendientes
        {
            get
            {
                return new RelayCommand(o => irDocumentosPendientes());
            }
        }

        /// <summary>
        /// Método que muestra la ventada de documentos pendientes por corregir de un usuario
        /// </summary>
        private void irDocumentosPendientes()
        {
            FrmDocumentosValidar frm = new FrmDocumentosValidar();

            DocumentosPendientesViewM context = new DocumentosPendientesViewM(usuario,"pendientes");

            frm.DataContext = context;
            frm.ShowDialog();
            initSnack();
        }

        /// <summary>
        ///  Comando para mostrar la ventana de los documentos que están pendientes por liberar de un usuario
        /// </summary>
        public ICommand irPendientesLiberar
        {
            get
            {
                return new RelayCommand(o => _irPendientesLiberar());
            }
        }

        /// <summary>
        /// Método que muestra la ventada de documentos pendientes por liberar de un usuario
        /// </summary>
        private void _irPendientesLiberar()
        {
            FrmPendientes_Liberar frm = new FrmPendientes_Liberar();

            PendientesLiberarVM context = new PendientesLiberarVM(usuario);

            frm.DataContext = context;

            frm.ShowDialog();
                      
            initSnack();
        }

        /// <summary>
        /// Comando para mostrar la ventana de los documentos que están pendientes por aprobar
        /// </summary>
        public ICommand IrDocumentosAprobados
        {
            get
            {
                return new RelayCommand(param => irDocumentosAprobados());
            }
        }

        /// <summary>
        /// Método que muestra la ventada de documentos liberados, pendientes por aprobar de todos los usuarios
        /// </summary>
        private void irDocumentosAprobados()
        {
            FrmDocumentosValidar frm = new FrmDocumentosValidar();

            DocumentosPendientesViewM context = new DocumentosPendientesViewM(usuario, "aprobados");

            frm.DataContext = context;

            frm.ShowDialog();

            TextoBuscar = string.Empty;
            GetDataGrid(string.Empty);
            initSnack();
        }

        /// <summary>
        /// Comando para agregar un nuevo tipo de documento
        /// </summary>
        public ICommand AgregarTipo
        {
            get
            {
                return new RelayCommand(o => agregarTipo());
            }
        }

        /// <summary>
        /// Método que muestra la ventana para agregar el tipo de documento
        /// </summary>
        private void agregarTipo()
        {
            FrmNuevoTipo frmTipo = new FrmNuevoTipo();
            NuevoTipoDocumentoVM context = new NuevoTipoDocumentoVM();
            frmTipo.DataContext = context;
            frmTipo.ShowDialog();

            _ListaTipoDocumento = DataManagerControlDocumentos.GetTipo();
            TextoBuscar = string.Empty;
            GetDataGrid(string.Empty);
        }
        /// <summary>
        /// Comando para abrir ventana de búsqueda de documentos
        /// </summary>
        public ICommand IrBusquedaDocumento
        {
            get{
                return new RelayCommand(o => irBusquedaDocumento());
            }
        }

        /// <summary>
        /// Método que muestra la ventana de todos los documentos 
        /// </summary>
        private void irBusquedaDocumento()
        {
            FrmBusqueda_Documentos frmBusqueda = new FrmBusqueda_Documentos();
            Buscar_DocumentoVM context = new Buscar_DocumentoVM();
            frmBusqueda.DataContext = context;

            frmBusqueda.ShowDialog();

        }
        /// <summary>
        /// Comando para agregar un nuevo departamento
        /// </summary>
        public ICommand AgregarDepartamento
        {
            get
            {
                return new RelayCommand(o => agregarDepartamento());
            }
        }

        /// <summary>
        /// Método que muestra la ventana para dar de alta un departamento
        /// </summary>
        private void agregarDepartamento()
        {
            FrmNuevo_Departamento frm = new FrmNuevo_Departamento();
            NuevoDepartamentoVM context = new NuevoDepartamentoVM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// Comando para visualizar la ventana de bloqueo
        /// </summary>
        public ICommand IrBloquear
        {
            get
            {
                return new RelayCommand(o => irBloquear());
            }
        }

        /// <summary>
        /// Método que muestra la ventana para agregaro o modificar un bloqueo
        /// </summary>
        private void irBloquear()
        {
            FrmBloqueo frame = new FrmBloqueo();
            BloqueoVM context = new BloqueoVM();
            frame.DataContext = context;
            frame.ShowDialog();
        }
        
        /// <summary>
        /// Comando para abrir la ventana de Validación tipo documento
        /// </summary>
        public ICommand IrValidacionTipoDocumento
        {
            get
            {
                return new RelayCommand(o => irvalidacionTipo());
            }
        }

        /// <summary>
        /// Comando para abrir la ventana de recursos
        /// </summary>
        public ICommand IrRecursos
        {
            get
            {
                return new RelayCommand(o => irRecursos());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void irRecursos()
        {
            RecursoViewModel contexto = new RecursoViewModel(usuario);

            FrmRecursosTipoDocumento p = new FrmRecursosTipoDocumento();

            p.DataContext = contexto;

            p.Show();
        }

        /// <summary>
        /// Método que muestra la ventana para agregar validación de documento
        /// </summary>
        private void irvalidacionTipo()
        {
            FrmValidacion_Tipo frame = new FrmValidacion_Tipo();
            ValidacionTipoVM context = new ValidacionTipoVM();
            frame.DataContext = context;
            frame.ShowDialog();

        }

        /// <summary>
        /// Método que muestra la ventana para agregar un documento
        /// si el usuario no tiene una versión muestra la ventana para generar versión
        /// </summary>
        private async void irNuevoDocumento()
        {
            Bloqueo obj = new Bloqueo();

            //Método que obtiene un registro si se encuentra activo
            obj = DataManagerControlDocumentos.GetBloqueo();

            //Si el sistema no está bloqueado
            //O es administrador del CIT, sólo los administradores pueden crear un documento cuando el sistema esté bloqueado
            if (obj.id_bloqueo ==0 || Module.UsuarioIsRol(usuario.Roles, 2))
            { 
            //Obtenermos la cantidad de números de documentosque tiene el usuario sin versión.
            int num_documentos = DataManagerControlDocumentos.GetDocumento_SinVersion(usuario.NombreUsuario).Count;

                //Si el número de documento es menor que cero
                if (num_documentos > 0)
                {
                    //Creamos un objeto de la ventana
                    FrmDocumento frm = new FrmDocumento();

                    DocumentoViewModel context = new DocumentoViewModel(usuario);

                    frm.DataContext = context;

                    //Mostramos la ventana
                    frm.ShowDialog();

                    initControlDocumentos();
                    initSnack();
                }
                else
                {
                    //El usuario no tiene documentos sin verisón 
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
            else
            {
                //Si el sistema está bloqueado
                   DialogService dialog = new DialogService();
                   await dialog.SendMessage("Sistema Bloqueado", obj.observaciones);
            }
        }

        /// <summary>
        /// Método que muestra la ventana para gener un número de documento
        /// </summary>
        private async void GenerarNumero()
        {
            Bloqueo obj = new Bloqueo();

            //Método que obtiene un registro si se encuentra activo
            obj = DataManagerControlDocumentos.GetBloqueo();

            //Si el sistema no está bloqueado
            //O es administrador del CIT, sólo los administradores pueden generar un número cuando el sistema esté bloqueado
            if (obj.id_bloqueo == 0 || Module.UsuarioIsRol(usuario.Roles, 2))
            {
                FrmGenerador_Numero frmGenerador = new FrmGenerador_Numero();

                GeneradorViewModel context = new GeneradorViewModel { ModelUsuario = usuario };

                frmGenerador.DataContext = context;

                frmGenerador.ShowDialog();
            }
            else
            {
                //Si el sistema está bloqueado
                DialogService dialog = new DialogService();
                await dialog.SendMessage("Sistema Bloqueado", obj.observaciones);
            }
        }

        /// <summary>
        /// Método que crea un DataSet de los documentos listados, manda a llamar a la función para crear un nuevo archivo
        /// </summary>
        private async void ExportarExcel(){

           DataSet ds = new DataSet();
           DataTable table = new DataTable();
          
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController Progress;

            //Si la lista de documentos es diferente de cero
            if (Lista.Count != 0)
            {
                //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
                Progress = await dialog.SendProgressAsync("Por favor espere", "Generando archivo excel...");

                //Se añade las columnas
                table.Columns.Add("Numero de Documento");
                table.Columns.Add("Descripción");
                table.Columns.Add("Version");
                table.Columns.Add("Copias");
                table.Columns.Add("Responsable");
                table.Columns.Add("Fecha de Emision");
                table.Columns.Add("Fecha de Revision");

                //Iteramos la lista de documentos
                foreach (var item in Lista)
                {
                    //Se crea una nueva fila
                    DataRow newRow = table.NewRow();

                    //Se añaden los valores a las columnas
                    newRow["Numero de Documento"] = item.nombre;
                    newRow["Descripción"] = item.descripcion;
                    newRow["Version"] = item.version.no_version;
                    newRow["Copias"] = item.version.no_copias;
                    newRow["Responsable"] = item.Departamento;
                    newRow["Fecha de Emision"] = item.fecha_emision.ToShortDateString();
                    newRow["Fecha de Revision"] = item.fecha_actualizacion.ToShortDateString();

                    //Agregamos la fila a la tabla
                    table.Rows.Add(newRow);
                }
                //Se agrega la tabla al dataset
                ds.Tables.Add(table);

                //Ejecutamos el método para exportar el archivo
                string e = await ExportToExcel.Export(ds);

                if (e != null)
                {
                    //Cerramos el mensaje de espera
                    await Progress.CloseAsync();

                    //Mostramos mensaje de error
                    await dialog.SendMessage("Alerta", "Error al generar el archivo excel");
                }

                //Ejecutamos el método para cerrar el mensaje de espera.
                await Progress.CloseAsync();
            }
            
        }

        /// <summary>
        /// Muestra la ventana que contiene información del documento
        /// En la ventana se puede modificar, eliminar o generar una nueva versión
        /// </summary>
        private void editarDocumento()
        {
            //Si se seleccionó algún documento
            if (selectedDocumento != null)
            {
                FrmDocumento frm = new FrmDocumento();
                DocumentoViewModel context = new DocumentoViewModel(selectedDocumento,true,usuario);

                frm.DataContext = context;

                frm.ShowDialog();

                //initControlDocumentos();
                TextoBuscar = string.Empty;
                GetDataGrid(string.Empty);
                initSnack();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// inicializa la lista de tipos de documentos, obtiene los documentos dependiendo el tipo
        /// </summary>
        private void initControlDocumentos()
        {
            _ListaTipoDocumento = DataManagerControlDocumentos.GetTipo();
        
            if (_ListaTipoDocumento.Count > 0)
            {
                SelectedTipoDocumento = _ListaTipoDocumento[0];
            }
            GetDataGrid(string.Empty);       
        }

        /// <summary>
        /// Inicializa las alertas de los documentos
        /// </summary>
        private void initSnack()
        {
            int num_validar,num_aprobados,pendientes_liberar;
            
            //id_rol=2 mostrar todos los snackbar
            //id_rol=3 solo mostrar pendientes por corregir 
           
            //Si el usuaruio es administador del CIT, puede visualizar todos los avisos
            if (Module.UsuarioIsRol(usuario.Roles, 2)) {

                //Método para obtener todos los documentos que están pendientes por validar
                num_validar = DataManagerControlDocumentos.GetDocumentosValidar(usuario.NombreUsuario).Count;

                //Método para obetener los documentos que tiene pendientes  por corregir del usuario. 
                num_pendientes = DataManagerControlDocumentos.GetDocumentos_PendientesCorregir(usuario.NombreUsuario).Count;

                //Método para obtener todos los documentos que están aprobados pero están pendientes por liberar
                num_aprobados = DataManagerControlDocumentos.GetDocumentos_PendientesLiberar().Count;

                pendientes_liberar = DataManagerControlDocumentos.GetPendientes_Liberar(usuario.NombreUsuario).Count;

                //Muestra los snackBar si hay documentos en la lista
                if (num_validar > 0)
                {
                    EnabledValidar = true;
                    DocumentosValidar = " " + num_validar + " Documento(s) pendiente(s) por validar";
                }
                else
                {
                    //Si no hay documentos en la lista, no muestra el snackbar
                    EnabledValidar = false;
                    DocumentosValidar = string.Empty;
                }
                if (num_pendientes > 0)
                {
                    EnabledCorregir = true;
                    DocumentosCorregir = " " + num_pendientes + " Documento(s) pendiente(s) por corregir";
                }
                else
                {
                    EnabledCorregir = false;
                    DocumentosCorregir = string.Empty;
                }
                if (num_aprobados > 0)
                {
                    EnabledAprobados = true;
                    DocumentosAprobados = " " + num_aprobados + " Documento(s) pendiente(s) por liberar";
                }
                else
                {
                    EnabledAprobados = false;
                    DocumentosAprobados = string.Empty;
                }
                if(pendientes_liberar >0 ){
                    EnabledPendientes_Liberar = true;
                    PendientesLiberar= "Existen  " + pendientes_liberar + " documentos que puedes entregar";
                }
                else
                {
                    EnabledPendientes_Liberar = false;
                    PendientesLiberar = string.Empty;
                }
            }

            //Si el usuario es dueño del documento
            if (Module.UsuarioIsRol(usuario.Roles, 3))
            {
                //Método para obetener los documentos que tiene pendientes  por corregir del usuario. 
                num_pendientes = DataManagerControlDocumentos.GetDocumentos_PendientesCorregir(usuario.NombreUsuario).Count;
                //Método para obetener los documentos que tiene pendientes  por liberar del usuario. 
                pendientes_liberar = DataManagerControlDocumentos.GetPendientes_Liberar(usuario.NombreUsuario).Count;

                //Si hay documentos pendientes, muestra snackbar
                if (num_pendientes > 0)
                {
                    EnabledCorregir = true;
                    DocumentosCorregir = " " + num_pendientes + " Documento(s) pendiente(s) por corregir";
                }
                else
                {
                    EnabledCorregir = false;
                    DocumentosCorregir = string.Empty;
                }
                if (pendientes_liberar > 0)
                {
                    EnabledPendientes_Liberar = true;
                    PendientesLiberar = "Existen  " + pendientes_liberar + " documentos que puedes entregar";
                }
                else
                {
                    EnabledPendientes_Liberar = false;
                    PendientesLiberar = string.Empty;
                }
            }     
        }

        /// <summary>
        /// Método que obtiene los documentos liberados, los asigna a la lista para mostrar en el datagrid
        /// </summary>
        /// <param name="TextoBusqueda"></param>
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
            //Si el usuario es administrador del CIT muestra los botones para agregar tipo de documento,
            //departamento, buscar documento y bloquear Sistema
            if (Module.UsuarioIsRol(usuario.Roles, 2))
            {
                BttnEnabled = true;
            }
        }
        #endregion
    }
}
