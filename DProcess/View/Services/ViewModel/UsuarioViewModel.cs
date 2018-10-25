using System.ComponentModel;
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
using System.Threading;
using TableDependency.SqlClient;
using Notifications.Wpf;
using View.Resources;
using MahApps.Metro.Controls.Dialogs;

namespace View.Services.ViewModel
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        public Usuario ModelUsuario;

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

            //if (Module.UsuarioIsRol(ModelUsuario.Roles, 2))
            //    SetDocumentsRejected();

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
            AnilloViewModel contexto = new AnilloViewModel(Nombre + " " + ApellidoPaterno + " " + ApellidoMaterno);

            //Asignamos el contexto a la pantalla del plano.
            pantallaPlano.DataContext = contexto;

            //Asignamos a la propiedad Pagina la nueva pantalla que debe de mostrar.
            Pagina = pantallaPlano;
        }

        private void irDataBase()
        {
            //PDataBase pantallaData = new PDataBase();

            //pantallaData.DataContext = this;

            //Pagina = pantallaData;

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
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

        }

        private void irRawMaterial()
        {
            PPattern pantallaPattern = new PPattern();

            PatternViewModel context = new PatternViewModel();

            pantallaPattern.DataContext = context;
            Pagina = pantallaPattern;
        }
        #endregion

        private async void SetDocumentsRejected()
        {
            List<DO_DocumentosRechazados> ListaDocumentos = Model.ControlDocumentos.DataManagerControlDocumentos.GetDocumentosAprobadosNoRecibidos();

            if (ListaDocumentos.Count > 0)
            {
                //Incializamos los servicios de dialog.
                DialogService dialog = new DialogService();

                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, "Existen archivos que tienen mas de dos dias sin entregar. \n¿Deseas rechazar los documentos? \n Si rechazas los documentos se notificará al dueño del documento vía correo. ", setting, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    //Rechazar el documento.
                    foreach (DO_DocumentosRechazados documentoRezadado in ListaDocumentos)
                    {
                        //Obtenemos el ID de la version.
                        int idVersion = Model.ControlDocumentos.DataManagerControlDocumentos.GetIdVersion(documentoRezadado.NombreDocumento, documentoRezadado.NoVersion);

                        ////Rechazamos el documento
                        Model.ControlDocumentos.DataManagerControlDocumentos.SetRechazarVersion(idVersion);

                        Model.ControlDocumentos.DataManagerControlDocumentos.InsertHistorialVersion(idVersion, ModelUsuario.Nombre + " " + ModelUsuario.ApellidoPaterno + " " + ModelUsuario.ApellidoMaterno, documentoRezadado.NombreDocumento, documentoRezadado.NoVersion, "Se cambia el estatus a: PENDIENTE POR CORREGIR");
                        
                        ServiceEmail serviceEmail = new ServiceEmail();

                        string[] correos = new string[2];

                        correos[0] = documentoRezadado.Correo;

                        //CAMBIAR AL USUARIO ACTIVO.
                        correos[1] = ModelUsuario.Correo;

                        string body = "<HTML>";
                        body += "<head>";
                        body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
                        body += "</head>";
                        body += "<body text=\"white\">";
                        body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
                        body += "<ul>";
                        body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que el documento con el número <b> " + documentoRezadado.NombreDocumento + "</b> versión <b> " + documentoRezadado.NoVersion + ".0" + " </b> fué rechazado y puesto en estatus Pendiente por corregir debido a que no se entrego a tiempo</font> </li>";
                        body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizó la fecha de la versión.</font></li>";
                        body += "<br/>";
                        body += "</ul>";
                        body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
                        body += "<p><font font=\"verdana\" size=\"3\" color=\"black\"></font> </p>";
                        body += "<br/>";
                        body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor no responda.</font> </p>";
                        body += "<br/>";
                        body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
                        body += "<ul>";
                        body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + ModelUsuario.Nombre + " " + ModelUsuario.ApellidoPaterno + "</font> </li>";
                        body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
                        body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
                        body += "<li></li>";
                        body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
                        body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
                        body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + ModelUsuario.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
                        body += "</ul>";
                        body += "</body>";
                        body += "</HTML>";
                        
                        serviceEmail.SendEmailLotusCustom(ModelUsuario.Pathnsf, correos, "Documento rechazado - " + documentoRezadado.NombreDocumento, body);

                    }
                    //Construimos en mensaje.
                }
            }
        }

        private string definirSaludo()
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
        private void TableDependencyAdmin_OnChanged(object sender, TableDependency.EventArgs.RecordChangedEventArgs<DO_Historial_Documento> e)
        {
            if (e.ChangeType == TableDependency.Enums.ChangeType.Insert)
            {
                var chagedEntity = e.Entity;
                NotificationType notification = NotificationType.Information;

                if (chagedEntity.DESCRIPCION.Contains("Se crea la versión"))
                {
                    var notificationManager = new NotificationManager();
                    notificationManager.Show(new NotificationContent
                    {
                        Title = StringResources.ttlNuevoDocumentoValidar,
                        Message = StringResources.ttlUsuario +" "+ chagedEntity.NOMBRE_USUARIO + "\n"+StringResources.ttlCreadoDocumento+"\n" + chagedEntity.NOMBRE_DOCUMENTO,
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

        private void TableDependencyAdmin_OnError(object sender, TableDependency.EventArgs.ErrorEventArgs e)
        {
            Exception ex = e.Error;
            throw ex;
        }

        private void TableDependency_OnChanged(object sender, TableDependency.EventArgs.RecordChangedEventArgs<DO_Notification> e)
        {
            if (e.ChangeType == TableDependency.Enums.ChangeType.Insert)
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

        private void TableDependency_OnError(object sender, TableDependency.EventArgs.ErrorEventArgs e)
        {
            Exception ex = e.Error;
            throw ex;
        }
        
        #endregion
    }
}