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
using System.Globalization;
using View.Forms.LeccionesAprendidas;
using View.Resources;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;

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

        private HamburgerMenuItemCollection _menuItems;
        public HamburgerMenuItemCollection MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                if (Equals(value, _menuItems)) return;
                _menuItems = value;
                //OnPropertyChanged();
                NotifyChange("MenuItems");
            }
        }

        private HamburgerMenuItemCollection _menuOptionItems;
        public HamburgerMenuItemCollection MenuOptionItems
        {
            get
            {
                return _menuOptionItems;
            }
            set
            {
                if (Equals(value, _menuOptionItems)) return;
                _menuOptionItems = value;
                //OnPropertyChanged();
                NotifyChange("MenuOptionItems");
            }
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
            CreateMenuItems();
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
        /// Comando para mostrar la ventana de todos los documentos que están en estátus de pendientes por corregir.
        /// </summary>
        public ICommand irDocumentosPendientesCorregir
        {
            get
            {
                return new RelayCommand(o => irTodosDocumentosPendientesCorregir());
            }
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
        /// Comando para abrir ventana de búsqueda de documentos
        /// </summary>
        public ICommand IrBusquedaDocumento
        {
            get{
                return new RelayCommand(o => irBusquedaDocumento());
            }
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
        /// Comando para abrir la ventana de historial
        /// </summary>
        public ICommand IrHistorial
        {
            get
            {
                return new RelayCommand(o => irHistorial());
            }
        }

        /// <summary>
        ///  Comando para abrir la ventana de historial filtrado.
        /// </summary>
        public ICommand IrHistorialFiltrado
        {
            get
            {
                return new RelayCommand(o => irHistorialFiltrado());
            }
        }

        /// <summary>
        /// Comando para abrir la ventana de documentos eliminados
        /// </summary>
        public ICommand irDocEliminado
        {
            get
            {
                return new RelayCommand(o =>irEliminados());
            }
        }


        #endregion

        #region Methods
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
        /// Método que muestra la ventada de documentos pendientes por corregir de un usuario
        /// </summary>
        private void irDocumentosPendientes()
        {
            FrmDocumentosValidar frm = new FrmDocumentosValidar();

            DocumentosPendientesViewM context = new DocumentosPendientesViewM(usuario, "pendientes");

            frm.DataContext = context;
            frm.ShowDialog();
            initSnack();
        }

        /// <summary>
        /// Método que muestra la ventana con todos los documentos en estátus de Pendiente po corregir.
        /// </summary>
        private void irTodosDocumentosPendientesCorregir()
        {
            //Declaramos la pantalla en la que se mostraran los resultados.
            FrmDocumentosValidar frm = new FrmDocumentosValidar();

            //Declaramos la clase ViewModel que será el contexto de la pantalla.
            DocumentosPendientesViewM context = new DocumentosPendientesViewM(usuario, "todosPendientes");

            //Asignamos el contexto a la pantalla.
            frm.DataContext = context;

            //Mostramos la pantalla.
            frm.ShowDialog();


            initSnack();
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
        /// Método que muestra la ventana de todos los documentos 
        /// </summary>
        private void irBusquedaDocumento()
        {
            FrmBusqueda_Documentos frmBusqueda = new FrmBusqueda_Documentos();
            Buscar_DocumentoVM context = new Buscar_DocumentoVM();
            frmBusqueda.DataContext = context;

            frmBusqueda.ShowDialog();
            TextoBuscar = string.Empty;
            GetDataGrid(string.Empty);
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
        /// Método que muestra la ventana de historial de los documentos
        /// </summary>
        private void irHistorial()
        {
            FrmHistorial frm = new FrmHistorial();
            HistorialVM context = new HistorialVM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// Método que muestra la ventana del historial de documentos filtrado.
        /// </summary>
        private void irHistorialFiltrado()
        {
            FrmHistorial_Filtrado frm = new FrmHistorial_Filtrado();
            HistorialFiltrado_VM context = new HistorialFiltrado_VM();
            frm.DataContext = context;
            frm.ShowDialog();

        }

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
        /// Método que muestra todos los documentos eliminados con su archivo
        /// </summary>
        private void irEliminados()
        {
            FrmDocumentos_Eliminados frm = new FrmDocumentos_Eliminados();
            Documento_EliminadoVM context = new Documento_EliminadoVM();
            frm.DataContext = context;
            frm.ShowDialog();
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
            DialogService dialogService = new DialogService();
            bool isAdministratorCIT = Module.UsuarioIsRol(usuario.Roles, 2);

            //Método que obtiene un registro si se encuentra activo
            obj = DataManagerControlDocumentos.GetBloqueo();

            //Si el sistema no está bloqueado
            //2 es administrador del CIT, sólo los administradores pueden crear un documento cuando el sistema esté bloqueado
            if (obj.id_bloqueo == 0 || Module.UsuarioIsRol(usuario.Roles, 2))
            {
                //Obtenermos la cantidad de números de documentosque tiene el usuario sin versión.
                int num_documentos = DataManagerControlDocumentos.GetDocumento_SinVersion(usuario.NombreUsuario).Count;

                //Si el número de documento es menor que cero
                if (num_documentos > 0)
                {
                    //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendrá el mensaje modal.
                    MetroDialogSettings setting = new MetroDialogSettings();
                    setting.NegativeButtonText = StringResources.msgCrearDocumento;
                    setting.AffirmativeButtonText = StringResources.msgEditarDocumento;

                    MessageDialogResult result;

                    if (isAdministratorCIT)
                    {
                        //Creamos la opción del botón para generar un número de documento de forma manual.
                        setting.FirstAuxiliaryButtonText = StringResources.msgNumeroFijo;

                        //Ejecutamos el método para mostrar el mensaje. El resultado lo guardamos en una variable local.
                        result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgDocumentoDisponible, setting, MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary);
                    }
                    else
                        //Ejecutamos el método para mostrar el mensaje. El resultado lo guardamos en una variable local.
                        result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgDocumentoDisponible, setting, MessageDialogStyle.AffirmativeAndNegative);


                    switch (result)
                    {
                        case MessageDialogResult.Negative:
                            //Ventana para generar un nuevo número de documento
                            FrmGenerador_Numero frmGenerador = new FrmGenerador_Numero();
                            GeneradorViewModel Datacontext = new GeneradorViewModel { ModelUsuario = usuario };
                            frmGenerador.DataContext = Datacontext;
                            frmGenerador.ShowDialog();
                            break;
                        case MessageDialogResult.Affirmative:
                            //Creamos un objeto de la ventana
                            FrmDocumento frm = new FrmDocumento();
                            DocumentoViewModel context = new DocumentoViewModel(usuario);
                            frm.DataContext = context;
                            //Mostramos la ventana
                            frm.ShowDialog();
                            break;
                        case MessageDialogResult.FirstAuxiliary:
                            //Mostramos la ventana para crear un documento con el número fijo.
                            FrmCrear_Numero frmCrear = new FrmCrear_Numero();

                            GeneradorViewModel generadorViewModel = new GeneradorViewModel { ModelUsuario = usuario };

                            frmCrear.DataContext = generadorViewModel;

                            frmCrear.DataContext = generadorViewModel;

                            frmCrear.ShowDialog();

                            break;


                        case MessageDialogResult.SecondAuxiliary:
                            break;
                        default:
                            break;
                    }

                    TextoBuscar = string.Empty;
                    GetDataGrid(string.Empty);
                    initSnack();
                }
                else
                {
                    //El usuario no tiene documentos sin verisón 

                    //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendrá el mensaje modal.
                    MetroDialogSettings setting = new MetroDialogSettings();
                    setting.AffirmativeButtonText = StringResources.msgCrearDocumento;
                    setting.NegativeButtonText = StringResources.msgCancelar;

                    MessageDialogResult result;

                    if (isAdministratorCIT)
                    {
                        //Creamos la opción del botón para generar un número de documento de forma manual.
                        setting.FirstAuxiliaryButtonText = StringResources.msgNumeroFijo;

                        //Ejecutamos el método para mostrar el mensaje. El resultado lo guardamos en una variable local.
                        result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgDocumentoDisponible, setting, MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary);
                    }
                    else
                        //Ejecutamos el método para mostrar el mensaje. El resultado lo guardamos en una variable local.
                        result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgNumeroDocumentoDisp, setting, MessageDialogStyle.AffirmativeAndNegative);

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

                            //Mostramos la ventana para crear un documento con el número fijo.
                            FrmCrear_Numero frmCrear = new FrmCrear_Numero();

                            GeneradorViewModel generadorViewModel = new GeneradorViewModel { ModelUsuario = usuario };

                            frmCrear.DataContext = generadorViewModel;

                            frmCrear.DataContext = generadorViewModel;

                            frmCrear.ShowDialog();

                            break;

                        case MessageDialogResult.SecondAuxiliary:
                            break;
                        default:
                            break;
                    }
                    initSnack();
                }
            }
            else
            {
                //Si el sistema está bloqueado
                DialogService dialog = new DialogService();
                await dialog.SendMessage(StringResources.msgSistemaBloqueado, obj.observaciones);
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
                await dialog.SendMessage(StringResources.msgSistemaBloqueado, obj.observaciones);
            }
        }

        /// <summary>
        /// Método que crea un DataSet de los documentos listados, manda a llamar a la función para crear un nuevo archivo
        /// </summary>
        private async void ExportarExcel()
        {

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
                Progress = await dialog.SendProgressAsync(StringResources.msgDoingOperation, StringResources.msgGenerandoExcell);

                //Se añade las columnas, se especifíca el tipo fecha para dar formato a la columna
                //Se tien que especificar el tipo, si no la fecha se escribe mal en Excel
                table.Columns.Add("Numero de Documento");
                table.Columns.Add("Descripción");
                table.Columns.Add("Version");
                table.Columns.Add("Fecha de Emision", typeof(DateTime));
                table.Columns.Add("Fecha de Revision", typeof(DateTime));
                table.Columns.Add("Área");
                table.Columns.Add("Copias");

                //Iteramos la lista de documentos
                foreach (var item in Lista)
                {
                    //Se crea una nueva fila
                    DataRow newRow = table.NewRow();

                    //Se añaden los valores a las columnas
                    newRow["Numero de Documento"] = item.nombre;
                    newRow["Descripción"] = item.descripcion;
                    newRow["Version"] = item.version.no_version;
                    newRow["Fecha de Emision"] = item.fecha_emision;
                    newRow["Fecha de Revision"] = item.fecha_actualizacion;
                    newRow["Área"] = item.Departamento;
                    newRow["Copias"] = item.version.no_copias;

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
                    await dialog.SendMessage(StringResources.msgError, StringResources.msgGenerandoExcell);
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
                DocumentoViewModel context = new DocumentoViewModel(selectedDocumento, true, usuario);

                frm.DataContext = context;

                frm.ShowDialog();

                //initControlDocumentos();
                TextoBuscar = string.Empty;
                GetDataGrid(string.Empty);
                initSnack();
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
        /// Inicializa las alertas de los documentos
        /// </summary>
        private void initSnack()
        {
            int num_validar,num_aprobados,pendientes_liberar,documentos_vencidos;
            
            //id_rol=2 mostrar todos los snackbar
            //id_rol=3 solo mostrar pendientes por corregir 
           
            //Si el usuaruio es administador del CIT, puede visualizar todos los avisos
            if (Module.UsuarioIsRol(usuario.Roles, 2)) {

                //Método para obtener todos los documentos que están pendientes por validar
                num_validar = DataManagerControlDocumentos.GetDocumentosValidar(usuario.NombreUsuario,"").Count;

                //Método para obetener los documentos que tiene pendientes  por corregir del usuario. 
                num_pendientes = DataManagerControlDocumentos.GetDocumentos_PendientesCorregir(usuario.NombreUsuario,"").Count;

                //Método para obtener todos los documentos que están aprobados pero están pendientes por liberar
                num_aprobados = DataManagerControlDocumentos.GetDocumentos_PendientesLiberar("").Count;

                //Obtiene los documentos pendientes por liberar, y que el usuario debe entregar en físico
                pendientes_liberar = DataManagerControlDocumentos.GetPendientes_Liberar(usuario.NombreUsuario).Count;

                //Método para obtener los documentos vencidos de un usuario
                documentos_vencidos = DataManagerControlDocumentos.GetDocumentos_Vencidos(usuario.NombreUsuario).Count;

                //Muestra los snackBar si hay documentos en la lista
                if (num_validar > 0)
                {
                    EnabledValidar = true;
                    DocumentosValidar = " " + num_validar + " " +StringResources.msgDocumentosValidar;
                }
                else
                {
                    //Si no hay documentos en la lista, no muestra el snackbar
                    EnabledValidar = false;
                    DocumentosValidar = string.Empty;
                }
                if (num_pendientes > 0)
                {
                    //documentos por corregir
                    EnabledCorregir = true;
                    DocumentosCorregir = " " + num_pendientes + " " +StringResources.msgDocumentosCorregir;
                }
                else
                {
                    EnabledCorregir = false;
                    DocumentosCorregir = string.Empty;
                }
                if (num_aprobados > 0)
                {
                    //documentos aprobados
                    EnabledAprobados = true;
                    DocumentosAprobados = " " + num_aprobados + " " +StringResources.msgDocumentosLiberar;
                }
                else
                {
                    EnabledAprobados = false;
                    DocumentosAprobados = string.Empty;
                }
                if(pendientes_liberar >0 ){
                    //documentos pendientes por liberar
                    EnabledPendientes_Liberar = true;
                    PendientesLiberar= " " + pendientes_liberar + " "+ StringResources.msgDocumentosEntregar;
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
                num_pendientes = DataManagerControlDocumentos.GetDocumentos_PendientesCorregir(usuario.NombreUsuario,"").Count;
                //Método para obetener los documentos que tiene pendientes  por liberar del usuario. 
                pendientes_liberar = DataManagerControlDocumentos.GetPendientes_Liberar(usuario.NombreUsuario).Count;
                 //Método para obtener los documentos vencidos de un usuario
                documentos_vencidos = DataManagerControlDocumentos.GetDocumentos_Vencidos(usuario.NombreUsuario).Count;

                //Si hay documentos pendientes, muestra snackbar
                if (num_pendientes > 0)
                {
                    EnabledCorregir = true;
                    DocumentosCorregir = " " + num_pendientes + " " +StringResources.msgDocumentosCorregir;
                }
                else
                {
                    EnabledCorregir = false;
                    DocumentosCorregir = string.Empty;
                }
                if (pendientes_liberar > 0)
                {
                    EnabledPendientes_Liberar = true;
                    PendientesLiberar = pendientes_liberar + " " +StringResources.msgDocumentosEntregar;
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

        /// <summary>
        /// Método para generar el Hamburger menú
        /// </summary>
        public void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();

            //verificamos que el usuario sea administrador
            if (BttnEnabled == true)
            {
                //si es administrador se agregan todos los botones
                //Nuevo Documento
                this.MenuItems.Add(
                     new HamburgerMenuIconItem()
                     {
                         Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.File },
                         Label = StringResources.lblNuevoDocumento,
                         Command = IrNuevoDocumento,
                     }
                    );
                //Exportar a excel
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FileExcel},
                        Label = StringResources.lblExportar,
                        Command = Exportar,
                    }
                    );
                //Buscar Documentos
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FileFind },
                        Label = StringResources.lblBuscarDocumento,
                        Command = IrBusquedaDocumento,
                    }
                    );
                //Agrgar tipo de documento
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FolderPlus },
                        Label = StringResources.lblAgregarTipo,
                        Command = AgregarTipo,
                    }
                    );
                //Agregar departamento
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.PlusBox },
                        Label = StringResources.lblAgregarDepartamento,
                        Command = AgregarDepartamento,
                    }
                    );
                //Bloquear sistema
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.TimerOff },
                        Label = StringResources.lblBloquearSistema,
                        Command = IrBloquear,
                    }
                    );
                //Ver documentos rechazados
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FileRestore },
                        Label = StringResources.lblViewDocumentosRechazados,
                        Command = irDocumentosPendientesCorregir,
                    }
                    );
                //Ver recursos o formatos para elaborar los documentos
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.LinkVariant },
                        Label = StringResources.lblVerRecurso,
                        Command = IrRecursos,
                    }
                    );
                //Ver historial filtrado
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.History },
                        Label = StringResources.lblVerHistorial,
                        Command = IrHistorial,
                    }
                    );
                //Ver historial sin filtrar
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FilterVariant },
                        Label = StringResources.lblHistorial,
                        Command = IrHistorialFiltrado,
                    }
                    );
                //ver documentos eliminados
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.DeleteVariant },
                        Label = StringResources.lblDocumentosEliminados,
                        Command = irDocEliminado,
                    }
                    );
            }
            else
            {
                //si el usuario no es administrador solo se muestran los siguientes botones
                this.MenuItems.Add(
                    //Nuevo documento
                    new HamburgerMenuIconItem()
                    {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.File },
                    Label = StringResources.lblNuevoDocumento,
                    Command = IrNuevoDocumento,
                    }
                );
                //Exportar excel
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FileExcel },
                        Label = StringResources.lblExportar,
                        Command = Exportar,
                    }
                );
                //Ver recursos
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.LinkVariant },
                        Label = StringResources.lblVerRecurso,
                        Command = IrRecursos,
                    }
                );
                //Ver historial filtrado
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.History },
                        Label = StringResources.lblVerHistorial,
                        Command = IrHistorial,
                    }
                );
                //Ver historial sin filtrar
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FilterVariant },
                        Label = StringResources.lblHistorial,
                        Command = IrHistorialFiltrado,
                    }
                );
                //Ver documentos eliminados
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.DeleteVariant },
                        Label = StringResources.lblDocumentosEliminados,
                        Command = irDocEliminado,
                    }
                );
            }
        }
        #endregion
    }
}
