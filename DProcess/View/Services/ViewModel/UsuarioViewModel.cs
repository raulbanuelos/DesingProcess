﻿using System.ComponentModel;
using System.Windows.Input;
using Model;
using System.Windows.Controls;
using View.Forms.Routing;
using View.Forms.RawMaterial;
using View.Forms.ControlDocumentos;
using System.Collections.Generic;
using System;
using View.Forms.DataBase;
using View.Forms.LeccionesAprendidas;
using View.Forms.User;
using View.Forms.Tooling;
using TableDependency.SqlClient;
using Notifications.Wpf;
using View.Resources;
using View.Forms.Shared;
using View.Forms.Cotizaciones;
using View.Forms.DashBoard;
using System.Windows;
using MahApps.Metro.Controls;
using System.Linq;
using TableDependency.SqlClient.Base.EventArgs;

namespace View.Services.ViewModel
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        #region Atributtes
        public Usuario ModelUsuario;
        #endregion

        #region Propiedades de Usuario
        public string Nombre
        {
            get
            {
                return ModelUsuario.Nombre;
            }
            set
            {
                ModelUsuario.Nombre = value;
                NotifyChange("Nombre");
            }
        }

        public string ApellidoPaterno
        {
            get
            {
                return ModelUsuario.ApellidoPaterno;
            }
            set
            {
                ModelUsuario.ApellidoPaterno = value;
                NotifyChange("ApellidoPaterno");
            }
        }

        public string ApellidoMaterno
        {
            get
            {
                return ModelUsuario.ApellidoMaterno;
            }
            set
            {
                ModelUsuario.ApellidoMaterno = value;
                NotifyChange("ApellidoMaterno");
            }
        }

        public string NombreUsuario
        {
            get
            {
                return ModelUsuario.NombreUsuario;
            }
            set
            {
                ModelUsuario.NombreUsuario = value;
                NotifyChange("NombreUsuario");
            }
        }

        public bool Block
        {
            get
            {
                return ModelUsuario.Block;
            }
            set
            {
                ModelUsuario.Block = value;
                NotifyChange("Block");
            }
        }

        public bool Conectado
        {
            get
            {
                return ModelUsuario.Conectado;
            }
            set
            {
                ModelUsuario.Conectado = value;
                NotifyChange("Conectado");
            }
        }

        public List<Rol> Roles {
            get
            {
                return ModelUsuario.Roles;
            }
            set
            {
                ModelUsuario.Roles = value;
                NotifyChange("Roles");
            }
        }

        public UserDetails Details {
            get
            {
                return ModelUsuario.Details;
            }
            set
            {
                ModelUsuario.Details = value;
                NotifyChange("Details");
            }
        }
        #endregion

        #region Propiedades de navegación
        private Page pagina;
        public Page Pagina
        {
            get { return pagina; }
            set
            {
                pagina = value;
                NotifyChange("Pagina");
            }
        }

        private Page pag;
        public Page Pag
        {
            get { return pag; }
            set
            {
                pag = value;
                NotifyChange("Pag");
            }
        }

        //Perfiles
        public bool PerfilRGP
        {
            get {
                return ModelUsuario.PerfilRGP;
            }
            set {
                ModelUsuario.PerfilRGP = value;
                NotifyChange("PerfilRGP");
            }
        }

        public bool PerfilTooling
        {
            get {
                return ModelUsuario.PerfilTooling;
            }
            set {
                ModelUsuario.PerfilTooling = value;
                NotifyChange("PerfilTooling");
            }
        }

        public bool PerfilRawMaterial
        {
            get { return ModelUsuario.PerfilRawMaterial; }
            set { ModelUsuario.PerfilRawMaterial = value; NotifyChange("PerfilRawMaterial"); }
        }

        public bool PerfilStandarTime
        {
            get { return ModelUsuario.PerfilStandarTime; }
            set { ModelUsuario.PerfilStandarTime = value; NotifyChange("PerfilStandarTime"); }
        }

        public bool PerfilQuotes
        {
            get { return ModelUsuario.PerfilQuotes; }
            set { ModelUsuario.PerfilQuotes = value; NotifyChange("PerfilQuotes"); }
        }

        public bool PerfilCIT
        {
            get { return ModelUsuario.PerfilCIT; }
            set { ModelUsuario.PerfilCIT = value; NotifyChange("PerfilCIT"); }
        }

        public bool PerfilData
        {
            get { return ModelUsuario.PerfilData; }
            set { ModelUsuario.PerfilData = value; NotifyChange("PerfilData"); }
        }

        public bool PerfilUserProfile
        {
            get { return ModelUsuario.PerfilUserProfile; }
            set { ModelUsuario.PerfilUserProfile = value; NotifyChange("PerfilUserProfile"); }
        }

        public bool PerfilHelp
        {
            get { return ModelUsuario.PerfilHelp; }
            set { ModelUsuario.PerfilHelp = value; NotifyChange("PerfilHelp"); }
        }

        public bool PerfilAdministrador
        {
            get
            {
                return Module.UsuarioIsRol(ModelUsuario.Roles, 2);
            }
        }


        //Privilegios
        private bool privilegioRGP;
        public bool PrivilegioRGP
        {
            get { return privilegioRGP; }
            set { privilegioRGP = value; }
        }

        private bool privilegioTooling;
        public bool PrivilegioTooling
        {
            get { return privilegioTooling; }
            set { privilegioTooling = value; }
        }

        private bool privilegioRawMaterial;
        public bool PrivilegioRawMaterial
        {
            get { return privilegioRawMaterial; }
            set { privilegioRawMaterial = value; }
        }

        private bool privilegioStandarTime;
        public bool PrivilegioStandarTime
        {
            get { return privilegioStandarTime; }
            set { privilegioStandarTime = value; }
        }

        private bool privilegioQuotes;
        public bool PrivilegioQuotes
        {
            get { return privilegioQuotes; }
            set { privilegioQuotes = value; }
        }

        private bool privilegioCIT;
        public bool PrivilegioCIT
        {
            get { return privilegioCIT; }
            set { privilegioCIT = value; }
        }

        private bool privilegioData;
        public bool PrivilegioData
        {
            get { return privilegioData; }
            set { privilegioData = value; }
        }

        private bool privilegioUserProfile;
        public bool PrivilegioUserProfileMyProperty
        {
            get { return privilegioUserProfile; }
            set { privilegioUserProfile = value; }
        }

        private bool privilegioHelp;
        public bool PrivilegioHelp
        {
            get { return privilegioHelp; }
            set { privilegioHelp = value; }
        }

        #endregion

        #region Properties

        private string _NombreCompleto;
        public string NombreCompleto
        {
            get { return Nombre + " " + ApellidoPaterno ; }
            set { _NombreCompleto = value; NotifyChange("NombreCompleto"); }
        }


        #endregion

        #region Constructores
        public UsuarioViewModel()
        {

        }

        public UsuarioViewModel(Usuario modelUsuario, Page pagina)
        {
            ModelUsuario = modelUsuario;

            this.pagina = pagina;

            //initNotifications();
        }

        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands

        public ICommand IrRouting
        {
            get
            {
                return new RelayCommand(o => irRouting());
            }
        }

        public ICommand IrStandarTime
        {
            get
            {
                return new RelayCommand(o => irStandarTime());
            }
        }

        public ICommand IrRawMaterial
        {
            get {
                return new RelayCommand(o => irRawMaterial());
            }
        }

        public ICommand IrDataBase
        {
            get
            {
                return new RelayCommand(o => irDataBase());
            }
        }

        public ICommand IrPerfil
        {
            get
            {
                return new RelayCommand(o => irPerfil());
            }
        }

        public ICommand IrUser
        {
            get
            {
                return new RelayCommand(o => irUser());

            }
        }

        public ICommand IrTooling
        {
            get
            {
                return new RelayCommand(o => irTooling());
            }
        }

        public ICommand IrLeccion
        {
            get
            {
                return new RelayCommand(o => irLeccion());
            }
        }

        public ICommand IrDashboard
        {
            get
            {
                return new RelayCommand(o => irDashboard());
            }
        }

        /// <summary>
        ///
        /// </summary>
        public ICommand IrControlDocumentos
        {
            get
            {
                return new RelayCommand(o => irControlDocumentos());
            }
        }

        public ICommand IrPattern
        {
            get
            {
                return new RelayCommand(a => _IrPattern());
            }
        }

        public ICommand IrMateriaPrimaRolado
        {
            get
            {
                return new RelayCommand(a => _IrMateriaPrimaRolado());
            }
        }
        public ICommand IrMaterialPrimaAcero
        {
            get
            {
                return new RelayCommand(a => _IrMaterialPrimaAcero());
            }
        }

        #endregion

        #region Métodos

        #region Métodos de navegación
        /// <summary>
        /// Método el cual nos envía a la pantalla de captura del plano.
        /// </summary>
        private void irRouting()
        {
            //Declaramos un objeto de tipo PRouting, el cual contiene el formulario de entrada del plano del anillo.
            PRouting pantallaPlano = new PRouting();

            //Declaramos un objeto de tipo AnilloViewModel el cual será el contexto de la pantalla.
            //AnilloViewModel contexto = new AnilloViewModel(Nombre + " " + ApellidoPaterno + " " + ApellidoMaterno);

            AnilloViewModel contexto = new AnilloViewModel(ModelUsuario.Nombre + " " + ModelUsuario.ApellidoPaterno, ModelUsuario);

            //Asignamos el contexto a la pantalla del plano.
            pantallaPlano.DataContext = contexto;

            //Asignamos a la propiedad Pagina la nueva pantalla que debe de mostrar.
            Pagina = pantallaPlano;
        }

        private void irStandarTime()
        {
            //FrmCalculoTiemposEstandar frm = new FrmCalculoTiemposEstandar();

            CrearCotizacion frm = new CrearCotizacion();

            CrearCotizacionViewModel context = new CrearCotizacionViewModel();

            frm.DataContext = context;

            frm.ShowDialog();


            //lista.Add(new FO_Item { Nombre = "MF012-S", ValorCadena = "MF012 - S" });
            //lista.Add(new FO_Item { Nombre = "SPR-128", ValorCadena = "SPR-128" });

            //PropiedadOptionalViewModel vm = new PropiedadOptionalViewModel(lista, "Material");

            //frm.DataContext = vm;


        }

        private void irDataBase()
        {
            PDataBase pantallaData = new PDataBase();

            pantallaData.DataContext = new DataBaseViewModel();

            Pagina = pantallaData;

            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");

            //WPropiedadesNumeric ventana = new WPropiedadesNumeric();
            //ventana.DataContext = new PropiedadViewModel(true);
            //ventana.ShowDialog();

            //WPropiedadesBool Form = new WPropiedadesBool();
            //Form.DataContext = new PropiedadBoolViewModel(true);
            //Form.ShowDialog();

            //WPropiedadesCadena Form = new WPropiedadesCadena();
            //Form.DataContext = new PropiedadCadenaViewModel(true);
            //Form.ShowDialog();

            //WViewAllPerfiles ventana = new WViewAllPerfiles();
            //PerfilViewModel vmPerfil = new PerfilViewModel();
            //ventana.DataContext = vmPerfil;
            //ventana.ShowDialog();

        }

        private void irPerfil()
        {
            WPerfil p = new WPerfil();

            PerfilViewModel vmPerfil = new PerfilViewModel();

            p.DataContext = vmPerfil;

            p.Show();

        }

        private void irUser()
        {
            PUser pantallaUser = new PUser();

            PUserViewModel context = new PUserViewModel(ModelUsuario);

            pantallaUser.DataContext = context;

            Pagina = pantallaUser;
        }

        private void irTooling()
        {
            PTooling pantallaTooling = new PTooling();

            ToolingViewModel vm = new ToolingViewModel(ModelUsuario);

            pantallaTooling.DataContext = vm;

            Pagina = pantallaTooling;
        }

        private void irControlDocumentos()
        {
            ControlDocumento frm = new ControlDocumento();

            ControlDocumentoViewModel context = new ControlDocumentoViewModel(ModelUsuario);

            frm.DataContext = context;

            Pagina = frm;

        }

        private void irLeccion()
        {
            FrmLeccionesAprendidas frm = new FrmLeccionesAprendidas();
            LeccionesAprendidasVM context = new LeccionesAprendidasVM(ModelUsuario);
            frm.DataContext = context;
            Pagina = frm;

            //FDashBoard dashboard = new FDashBoard();
            //DashboardViewModel viewmodel = new DashboardViewModel();
            //dashboard.DataContext = viewmodel;
            //Pagina = dashboard;

        }

        private void irDashboard()
        {
            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            //Cerramos la pantalla.
            window.Close();

            DashboardViewModel context;
            FDashBoard pDashBoard = new FDashBoard();
            context = new DashboardViewModel(ModelUsuario);
            context.ModelUsuario = ModelUsuario;

            //NOTA IMPORTANTE: Se hizo una redundancia al asignarle en la propiedad página su misma pantalla. Solo es por ser la primeva vez y tenernos en donde descanzar la primera pantalla.
            context.Pagina = pDashBoard;

            //Asignamos al DataContext de la PantallaHome el context creado anteriormente.
            pDashBoard.DataContext = context;

            //Declaramos la pantalla en la que descanzan todas las páginas.
            Layout masterPage = new Layout();

            //Asingamos el DataContext.
            masterPage.DataContext = context;

            //Ejecutamos el método el cual despliega la pantalla.
            masterPage.ShowDialog();

        }

        private void irRawMaterial()
        {
            //PPattern pantallaPattern = new PPattern();

            //PatternViewModel context = new PatternViewModel();

            //pantallaPattern.DataContext = context;
            //Pagina = pantallaPattern;

            MateriasPrimas Form = new MateriasPrimas();
            Form.DataContext = this;
            Pagina = Form;
        }

        private void _IrPattern()
        {
            PPattern form = new PPattern();
            PatternViewModel context = new PatternViewModel();

            form.DataContext = context;

            Pag = form;

        }

        private void _IrMateriaPrimaRolado()
        {
            CatMateriaPrimaRolado form = new CatMateriaPrimaRolado();
            CatMateriaPrimaRoladoVM context = new CatMateriaPrimaRoladoVM();

            form.DataContext = context;

            Pag = form;
        }

        private void _IrMaterialPrimaAcero()
        {
            MateriaPrimaAcero form = new MateriaPrimaAcero();
            MateriaPrimaAceroVM context = new MateriaPrimaAceroVM();

            form.DataContext = context;

            Pag = form;

        }


        #endregion

        public static string definirSaludo()
        {
            DateTime d = DateTime.Now;
            string saludo = string.Empty;

            return d.Hour <= 11 ? "Buenos días;" : "Buenas tardes;";
        }

        private void initNotifications()
        {
            SqlTableDependency<DO_Notification> tableDependency;
            SqlTableDependency<DO_Historial_Documento> tableDependencyAdmin;
            var connectionString = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"];
            tableDependency = new SqlTableDependency<DO_Notification>(connectionString, "TBL_NOTIFICACIONES");
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();

            //Checamos si el usuario es administrador del CIT
            if (Module.UsuarioIsRol(ModelUsuario.Roles, 2))
            {
                tableDependencyAdmin = new SqlTableDependency<DO_Historial_Documento>(connectionString, "TBL_HISTORIAL_VERSION");
                tableDependencyAdmin.OnChanged += TableDependencyAdmin_OnChanged;
                tableDependencyAdmin.OnError += TableDependencyAdmin_OnError;
                tableDependencyAdmin.Start();
            }

        }

        /// <summary>
        /// Notificaciones para cuando se crea o corrigue un documento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TableDependencyAdmin_OnChanged(object sender, RecordChangedEventArgs<DO_Historial_Documento> e)
        {
            if (e.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Insert)
            {
                var chagedEntity = e.Entity;
                NotificationType notification = NotificationType.Information;

                if (chagedEntity.DESCRIPCION.Contains("Se crea la versión"))
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show(new NotificationContent
                    {
                        Title = StringResources.ttlNuevoDocumentoValidar,
                        Message = StringResources.ttlUsuario + " " + chagedEntity.NOMBRE_USUARIO + "\n" + StringResources.ttlCreadoDocumento + "\n" + chagedEntity.NOMBRE_DOCUMENTO,
                        Type = notification
                    });
                }

                if (chagedEntity.DESCRIPCION.Contains("Se cambia el estatus a: CREADO, PENDIENTE POR APROBACIÓN"))
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show(new NotificationContent
                    {
                        Title = StringResources.ttlNuevoDocumentoValidar,
                        Message = StringResources.ttlUsuario +" "+ chagedEntity.NOMBRE_USUARIO + "\n "+ StringResources.ttlCorrigioDocumento+"\n" + chagedEntity.NOMBRE_DOCUMENTO,
                        Type = notification
                    });
                }
            }
        }

        private void TableDependencyAdmin_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Exception ex = e.Error;
            throw ex;
        }

        private void TableDependency_OnChanged(object sender, RecordChangedEventArgs<DO_Notification> e)
        {
            if (e.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Insert)
            {
                var chagedEntity = e.Entity;

                if (chagedEntity.ID_USUARIO_RECEIVER == NombreUsuario)
                {
                    NotificationType noti = NotificationType.Information;
                    if (chagedEntity.TYPE_NOTIFICATION == 1)
                        noti = NotificationType.Success;
                    else if (chagedEntity.TYPE_NOTIFICATION == 2)
                        noti = NotificationType.Warning;
                    else if (chagedEntity.TYPE_NOTIFICATION == 3)
                        noti = NotificationType.Error;
                    else
                        noti = NotificationType.Information;

                    var notificationManager = new NotificationManager();
                    notificationManager.Show(
                        new NotificationContent
                        {
                            Title = chagedEntity.TITLE,
                            Message = chagedEntity.MSG,
                            Type = noti
                        });
                }
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Exception ex = e.Error;
            throw ex;
        }

        #endregion
    }
}