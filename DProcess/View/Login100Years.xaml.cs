using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using View.Forms.DashBoard;
using View.Forms.Index;
using View.Forms.Shared;
using View.Forms.User;
using View.Resources;
using View.Services;
using View.Services.ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for Login100Years.xaml
    /// </summary>
    public partial class Login100Years : MetroWindow
    {
        public Login100Years()
        {
            Thread ht1 = new Thread(new ThreadStart(checkConnection));
            ht1.Start();

            InitializeComponent();
            cargarVideo();
            lblVersion.Content = StringResources.lblVersion + " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            ht1.Join();
        }

        async void checkConnection()
        {
            string respuesta = await DataManager.GetStatusConetionSQLServer();

            if (respuesta == "Error")
            {
                Uri resource = new Uri("/Images/circle_red.png", UriKind.Relative);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    btn_ingresar.IsEnabled = false;
                    imgEnLinea.Source = new BitmapImage(resource);
                    lblEstatus.Content = "Offline";
                }));
            }
            else
            {
                Uri resource = new Uri("/Images/circle_green.png", UriKind.Relative);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    imgEnLinea.Source = new BitmapImage(resource);
                    lblEstatus.Content = "on-Line";
                }));
            }
        }

        private void cargarVideo()
        {
            animar();

            MoveTitle(grdTitle, 550, 200);
        }

        private void animar()
        {
            var videoPath = System.Environment.CurrentDirectory;

            myMediaElement.Source = new Uri(videoPath + @"\intro100Years.mp4", UriKind.Absolute);

            myMediaElement.Play();
        }

        private static void MoveTitle(Grid target, double newX, double newY)
        {
            var top = Canvas.GetTop(target);
            var left = Canvas.GetLeft(target);

            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;

            DoubleAnimation anim1 = new DoubleAnimation(0, newX, TimeSpan.FromSeconds(1));
            anim1.BeginTime = TimeSpan.FromSeconds(5.5);
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);

        }

        //Método para cambiar el idioma del sistema a español
        async void Btn_Espanol(object sender, RoutedEventArgs e)
        {
            DialogService dialog = new DialogService();
            await dialog.SendMessage("Atención", "El idioma del sistema se ha cambiado a Español");
            //Idioma en español
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
        }

        //Método para cambiar el idioma del sistema a ingles
        async void Btn_Ingles(object sender, RoutedEventArgs e)
        {
            DialogService dialog = new DialogService();
            await dialog.SendMessage("Attention", "The language of the system has been changed to English");
            //Establecemos el idioma por default es el ingles.
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
        }

        async void Btn_ingresar_Click(object sender, RoutedEventArgs e)
        {
            //Abrimos el mensaje modal para que el usuario ingrese sus credenciales, el resultado lo guardamos en una variable local.
            LoginDialogData result = await this.ShowLoginAsync(StringResources.ttlAuthentication, StringResources.lblEnterCredentials, new LoginDialogSettings { ColorScheme = MetroDialogOptions.ColorScheme, InitialUsername = "", AffirmativeButtonText = StringResources.lblBtnLogin, UsernameWatermark = StringResources.lblTxtUserName, PasswordWatermark = StringResources.lblTxtContrasena });

            //Comparamos si el resultado es distinto de nulo. Si es igual a nulo quiere decir que el usuario cancelo la captura o cerró inesperadamente la pantalla.
            if (result != null)
            {
                //Incializamos los servicios de dialog.
                DialogService dialog = new DialogService();

                //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
                ProgressDialogController AsyncProgress;

                //Ejecutamos el método para enviar un mensaje de espera mientras se comprueban los datos.
                AsyncProgress = await dialog.SendProgressAsync(StringResources.lblLogIn, "");

                //Declaramos un objeto con el cual se realiza la encriptación
                Services.Encriptacion encriptar = new Services.Encriptacion();

                //Ejecutamos el método para encriptar tanto el usuario como la contraseña y los guardamos en variables locales respectivamente.
                string usuario = encriptar.encript(result.Username);
                string contrasena = encriptar.encript(result.Password);

                //Ejecutamos el método para verificar las credenciales, el resultado lo asignamos a un objeto local de tipo Usuario.
                Usuario usuarioConectado = await DataManager.GetLogin(usuario, contrasena);

                //Verificamos el resultado, si es direfente de nulo quiere decir que el logueo fué correcto, si es igual a nulo quiere decir que el usuario no existe con las credenciales proporcionadas.
                if (usuarioConectado != null)
                {

                    //Ejecutamos el método para cerrar el mensaje de espera.
                    await AsyncProgress.CloseAsync();

                    //Verificamos si el usuario no esta bloqueado.
                    if (usuarioConectado.Block)
                    {

                        //Enviamos un mensaje para indicar que el usuario está bloqueado.
                        MessageDialogResult message = await this.ShowMessageAsync(StringResources.lblInformation, StringResources.lblUserNotActive);
                    }
                    else
                    {

                        SpeechSynthesizer _SS = new SpeechSynthesizer();
                        _SS.Volume = 100;
                        _SS.Rate = 1;

                        _SS.SpeakAsync("Welcome, " + usuarioConectado.Nombre + ", To Process Design Engineering Program");

                        //Enviamos un mensaje de bienvenida al usuario.
                        MessageDialogResult message = await this.ShowMessageAsync(StringResources.lblWelcome, usuarioConectado.Nombre);

                        //Obtenemos la fecha del servidor
                        DateTime date_now = DataManagerControlDocumentos.Get_DateTime();
                        //Ejecutamos el método para desbloquear el sistema, si se venció la fecha final
                        DataManagerControlDocumentos.DesbloquearSistema(date_now);

                        //Obtenemos los detalles del usuario logueado.
                        usuarioConectado.Details = DataManager.GetUserDetails(usuarioConectado.NombreUsuario);

                        //Verificamos si esta cargada la foto, sino asignamos una por default.
                        if (string.IsNullOrEmpty(usuarioConectado.Details.PathPhoto))
                            usuarioConectado.Details.PathPhoto = System.Configuration.ConfigurationManager.AppSettings["PathDefaultImage"];

                        //Insertamos el ingreso a la bitácora.
                        DataManager.InserIngresoBitacora(Environment.MachineName, usuarioConectado.Nombre + " " + usuarioConectado.ApellidoPaterno + " " + usuarioConectado.ApellidoMaterno);

                        //Validamos si el usuario nuevo tiene la contraseña random
                        if (usuarioConectado.Details.Temporal_Password == true)
                        {
                            //Cargamnos las vista de ModificarContrasena
                            ModificarContrasena vistacontrasena = new ModificarContrasena();
                            //Cargamnos el modelo de CambiarContraseniaViewModel
                            CambiarContraseniaViewModel vmcambiarconatraseña = new CambiarContraseniaViewModel(usuarioConectado);

                            //Asingamos el DataContext.
                            vistacontrasena.DataContext = vmcambiarconatraseña;

                            //Mostramos la ventana de modificacion de contraseña
                            vistacontrasena.ShowDialog();

                            //Verificamos el valor de la variable CierrePantalla, si en la View Model de CambiarContrasenia la variable es false, dejamos continual el proceso
                            if (vmcambiarconatraseña.CierrePantalla == false)
                            {
                                return;
                            }
                        }

                        #region Configuración del correo electrónico

                        //Verificamos si esta configurado el correo electrónico en la plataforma.
                        if (!usuarioConectado.Details.IsAvailableEmail || !System.IO.File.Exists(usuarioConectado.Pathnsf))
                        {
                            //Configuramos las opciones del mesaje de pregunta.
                            MetroDialogSettings settings = new MetroDialogSettings();
                            settings.AffirmativeButtonText = StringResources.lblYes;
                            settings.NegativeButtonText = StringResources.lblNo;

                            //Preguntamos al usuario si lo quiere configurar en estos momentos.
                            MessageDialogResult resultMSG = await this.ShowMessageAsync(StringResources.ttlAtencion + usuarioConectado.Nombre, StringResources.msgConfiguracionCorreo, MessageDialogStyle.AffirmativeAndNegative, settings);

                            //Verificamos la respuesta del usuario, si es afirmativa iniciamos el proceso de configuración.
                            if (resultMSG == MessageDialogResult.Affirmative)
                            {
                                settings = new MetroDialogSettings();
                                settings.AffirmativeButtonText = StringResources.ttlOkEntiendo;

                                await this.ShowMessageAsync(usuarioConectado.Nombre + StringResources.msgParaTuInf, StringResources.msgProcesoConfiguracion, MessageDialogStyle.Affirmative, settings);

                                ProgressDialogController AsyncProgressConfigEmail;

                                AsyncProgressConfigEmail = await dialog.SendProgressAsync(StringResources.ttlEspereUnMomento + usuarioConectado.Nombre + "...", StringResources.msgEstamosConfigurando);

                                ConfigEmailViewModel configEmail = new ConfigEmailViewModel(usuarioConectado);

                                // Se reciben valores de las 2 propiedades del objeto
                                DO_PathMail respuestaConfigEmail = await configEmail.setEmail();

                                await AsyncProgressConfigEmail.CloseAsync();

                                if (respuestaConfigEmail.respuesta)
                                {
                                    // Actualizamos el path de usuario en la misma sesión
                                    usuarioConectado.Pathnsf = respuestaConfigEmail.rutamail;

                                    settings.AffirmativeButtonText = StringResources.ttlGenial;
                                    await this.ShowMessageAsync(StringResources.msgPerfecto + usuarioConectado.Nombre, StringResources.msgCuentaConfigurada, MessageDialogStyle.Affirmative, settings);
                                }
                                else
                                {
                                    await this.ShowMessageAsync(StringResources.ttlOcurrioError, StringResources.msgErrorVincular);
                                }
                            }
                        }

                        #endregion

                        if (Module.UsuarioIsRol(usuarioConectado.Roles, 2))
                        {
                            DashboardViewModel context;
                            FDashBoard pDashBoard = new FDashBoard();
                            context = new DashboardViewModel(usuarioConectado);
                            context.ModelUsuario = usuarioConectado;

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
                        else
                        {
                            Home PantallaHome = new Home(usuarioConectado.NombreUsuario);

                            //Creamos un objeto UsuarioViewModel, y le asignamos los valores correspondientes, a la propiedad Pagina se le asigna la pantalla inicial de Home.
                            UsuarioViewModel context = new UsuarioViewModel(usuarioConectado, PantallaHome);
                            context.ModelUsuario = usuarioConectado;

                            //NOTA IMPORTANTE: Se hizo una redundancia al asignarle en la propiedad página su misma pantalla. Solo es por ser la primeva vez y tenernos en donde descanzar la primera pantalla.
                            context.Pagina = PantallaHome;

                            //Asignamos al DataContext de la PantallaHome el context creado anteriormente.
                            PantallaHome.DataContext = context;

                            //Declaramos la pantalla en la que descanzan todas las páginas.
                            Layout masterPage = new Layout();

                            //Asingamos el DataContext.
                            masterPage.DataContext = context;

                            //Ejecutamos el método el cual despliega la pantalla.
                            masterPage.ShowDialog();
                        }

                        //Una vez que el usuario hizo clic en aceptar el mensaje de bienvenida, se procede con la codificación de la presentación de la pantalla inicial.
                        //Creamos un objeto de tipo Home, la cual es la pantalla inicial del sistema.
                        //Home PantallaHome = new Home(usuarioConectado.NombreUsuario);

                        //Creamos un objeto UsuarioViewModel, y le asignamos los valores correspondientes, a la propiedad Pagina se le asigna la pantalla inicial de Home.
                        //UsuarioViewModel context = new UsuarioViewModel { ModelUsuario = usuarioConectado, Pagina = PantallaHome };
                        //UsuarioViewModel context = new UsuarioViewModel(usuarioConectado, PantallaHome);
                        //context.ModelUsuario = usuarioConectado;

                        ////NOTA IMPORTANTE: Se hizo una redundancia al asignarle en la propiedad página su misma pantalla. Solo es por ser la primeva vez y tenernos en donde descanzar la primera pantalla.
                        //context.Pagina = PantallaHome;

                        ////Asignamos al DataContext de la PantallaHome el context creado anteriormente.
                        //PantallaHome.DataContext = context;

                        ////Declaramos la pantalla en la que descanzan todas las páginas.
                        //Layout masterPage = new Layout();

                        ////Asingamos el DataContext.
                        //masterPage.DataContext = context;

                        ////Ejecutamos el método el cual despliega la pantalla.
                        //masterPage.ShowDialog();


                        ////Si el usuario es administrador, le mostramos la pantalla de Dashboard.
                        //if (Module.UsuarioIsRol(usuarioConectado.Roles, 2))
                        //{
                        //    FDashBoard pDashBoard = new FDashBoard();

                        //    DashboardViewModel wm = new DashboardViewModel(pDashBoard, usuarioConectado);
                        //    pDashBoard.DataContext = wm;

                        //    //Declaramos la pantalla en la que descanzan todas las páginas.
                        //    Layout masterPage1 = new Layout();

                        //    //Asingamos el DataContext.
                        //    masterPage1.DataContext = wm;

                        //    //Ejecutamos el método el cual despliega la pantalla.
                        //    masterPage1.ShowDialog();

                        //}
                        //else
                        //{

                        //}
                    }
                }
                else
                {
                    //Ejecutamos el método para cerrar el mensaje de espera.
                    await AsyncProgress.CloseAsync();

                    //Enviamos un mensaje indicando que las credenciales escritas son incorrectas.
                    MessageDialogResult message = await this.ShowMessageAsync(StringResources.ttlAlerta, StringResources.lblPasswordIncorrect);
                }
            }
        }

        private async void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            DialogService dialog1 = new DialogService();
            ProgressDialogController AsyncProgressConfigEmail;

            AsyncProgressConfigEmail = await dialog1.SendProgressAsync(StringResources.ttlAtencion, StringResources.ttlEspereUnMomento);
            string url = System.Configuration.ConfigurationManager.AppSettings["URLNodeServer"];
            bool respuestaNode = await DataManager.GetStatusConetionNodeServer(url);

            await AsyncProgressConfigEmail.CloseAsync();

            if (!respuestaNode)
            {
                DialogService dialog = new DialogService();

                await dialog.SendMessage("Atención", "Por el momento el servicio no esta disponible, por favor intente más tarde ó contacte al administrador del sistema.");
            }
            else
            {
                var window = System.Windows.Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                MetroDialogSettings settings = new MetroDialogSettings();
                settings.AffirmativeButtonText = "Ok";
                settings.NegativeButtonText = "Cancelar";

                string correo = await window.ShowInputAsync("Atención", "Por favor ingresa tu usuario ó tu correo", null);
                Usuario user = new Usuario();

                if (!string.IsNullOrEmpty(correo))
                {
                    Model.Encriptacion encrip = new Model.Encriptacion();
                    string usuarioEncriptado = encrip.encript(correo);

                    user = DataManager.GetUsuario(usuarioEncriptado);

                    DialogService dialog = new DialogService();

                    if (!string.IsNullOrEmpty(user.IdUsuario))
                    {
                        if (iniciarProcesoRecuperarContrasena(user))
                            await dialog.SendMessage("Atención", "En los próximos minutos recibira un correo con las instrucciones necesarias para recuperar su contraseña.");
                        else
                            await dialog.SendMessage("Atención", "Hubo un error, por favor intente mas tarde.");
                    }
                    else
                    {
                        user = DataManager.GetUserByCorreo(correo);
                        if (!string.IsNullOrEmpty(user.IdUsuario))
                        {
                            if (iniciarProcesoRecuperarContrasena(user))
                                await dialog.SendMessage("Atención", "En los próximos minutos recibira un correo con las instrucciones necesarias para recuperar su contraseña.");
                            else
                                await dialog.SendMessage("Atención", "Hubo un error, por favor intente mas tarde.");
                        }
                        else
                            await dialog.SendMessage("Atención", "No hay registros del usuario ó correo ingresado, por favor revisa los datos.");
                    }
                }
            }
        }

        private bool iniciarProcesoRecuperarContrasena(Usuario usuario)
        {
            bool respuesta = false;
            Model.Encriptacion encrip = new Model.Encriptacion();
            string temporalPassword = Module.GetRandomString(8);
            string escriptPassword = encrip.encript(temporalPassword);

            if (DataManager.SetTemporalPassword(usuario.IdUsuario, escriptPassword) > 0)
            {
                respuesta = true;
                ServiceEmail SO_Email = new ServiceEmail();
                string body = string.Empty;
                string[] correos = new string[2];
                correos[0] = "raul.banuelos@mahle.com";
                correos[1] = usuario.Correo;

                body = "<HTML>";
                body += "<head>";
                body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
                body += "</head>";
                body += "<body text=\"white\">";
                body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
                body += "<ul>";
                body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Se ha recibido una solicitud de restrablecimiento de su contraseña para el sistema Diseño del proceso.</font> </li>";
                body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Su contraseña temporal es la siguiente:</font></li>";
                body += "<br/>";
                body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Contraseña : <b>" + temporalPassword + "</b></font></li>";

                //body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Área del Frames en donde se inserto : <b>" + AreaFrames + "</b></font></li>";
                body += "</ul>";
                body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Favor de respetar mayúsculas y minúsculas</font> </p>";
                body += "<br/>";
                body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor no responda.</font> </p>";
                body += "<br/>";
                body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
                body += "<ul>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Raúl Bañuelos</font> </li>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
                body += "<li></li>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">raul.banuelos@mahle.com</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
                body += "</ul>";
                body += "</body>";
                body += "</HTML>";

                respuesta = SO_Email.SendEmailLotusCustom("", correos, "Solicitud para restablecer tu contraseña", body, "SISTEMA", 0);
            }

            return respuesta;
        }

        /// <summary>
        /// Método que define si es "Buenos días" o "Buenas tardes" dependiendo la hora.
        /// </summary>
        /// <returns></returns>
        private string definirSaludo()
        {
            DateTime d = DateTime.Now;
            string saludo = string.Empty;

            return d.Hour <= 11 ? "Buenos días;" : "Buenas tardes;";
        }
    }
}