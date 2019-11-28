/*
 * Desarrollador: Edgar Raúl Bañuelos Díaz
 * Fecha: 02/03/2017
 * Hora: 07:34 a.m.
 * 
 */
using System.Windows;
using Encriptar;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model;
using View.Forms.Index;
using View.Forms.Shared;
using View.Services.ViewModel;
using Model.ControlDocumentos;
using System;
using View.Services;
using View.Resources;
using System.Threading;
using View.Forms.Routing;
using System.IO;

namespace View.Forms.LogIn
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : MetroWindow
	{
		public LogIn()
		{
			InitializeComponent();
            lblVersion.Content = StringResources.lblVersion +" "+ System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
                Encriptacion encriptar = new Encriptacion();

                //Ejecutamos el método para encriptar tanto el usuario como la contraseña y los guardamos en variables locales respectivamente.
				string usuario = encriptar.encript(result.Username);
				string contrasena = encriptar.encript(result.Password);

                //Ejecutamos el método para verificar las credenciales, el resultado lo asignamos a un objeto local de tipo Usuario.
				Usuario usuarioConectado = await DataManager.GetLogin(usuario,contrasena);
                
                //Verificamos el resultado, si es direfente de nulo quiere decir que el logueo fué correcto, si es igual a nulo quiere decir que el usuario no existe con las credenciales proporcionadas.
				if (usuarioConectado != null) {

                    //Ejecutamos el método para cerrar el mensaje de espera.
                    await AsyncProgress.CloseAsync();

                    //Verificamos si el usuario no esta bloqueado.
                    if (usuarioConectado.Block) {

                        //Enviamos un mensaje para indicar que el usuario está bloqueado.
                        MessageDialogResult message = await this.ShowMessageAsync(StringResources.lblInformation,StringResources.lblUserNotActive);
					}else{

                        //Enviamos un mensaje de bienvenida al usuario.
						MessageDialogResult message = await this.ShowMessageAsync(StringResources.lblWelcome,usuarioConectado.Nombre);

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

                        #region Configuración del correo electrónico

                        //Verificamos si esta configurado el correo electrónico en la plataforma.
                        if (!usuarioConectado.Details.IsAvailableEmail)
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
                        
                        //Una vez que el usuario hizo clic en aceptar el mensaje de bienvenida, se procede con la codificación de la presentación de la pantalla inicial.
                        //Creamos un objeto de tipo Home, la cual es la pantalla inicial del sistema.
                        Home PantallaHome = new Home(usuarioConectado.NombreUsuario);

                        //Creamos un objeto UsuarioViewModel, y le asignamos los valores correspondientes, a la propiedad Pagina se le asigna la pantalla inicial de Home.
                        //UsuarioViewModel context = new UsuarioViewModel { ModelUsuario = usuarioConectado, Pagina = PantallaHome };
                        UsuarioViewModel context = new UsuarioViewModel(usuarioConectado, PantallaHome);
                        context.ModelUsuario = usuarioConectado;
                        context.Pagina = PantallaHome;

                        //Asignamos al DataContext de la PantallaHome el context creado anteriormente.
                        //NOTA IMPORTANTE: Se hizo una redundancia al asignarle en la propiedad página su misma pantalla. Solo es por ser la primeva vez y tenernos en donde descanzar la primera pantalla.
                        PantallaHome.DataContext = context;

                        //Declaramos la pantalla en la que descanzan todas las páginas.
                        Layout masterPage = new Layout();

                        //Asingamos el DataContext.
                        masterPage.DataContext = context;

                        //Ejecutamos el método el cual despliega la pantalla.
                        masterPage.ShowDialog();
                    }
				}
				else
                {
                    //Ejecutamos el método para cerrar el mensaje de espera.
                    await AsyncProgress.CloseAsync();

                    //Enviamos un mensaje indicando que las credenciales escritas son incorrectas.
                    MessageDialogResult message = await this.ShowMessageAsync(StringResources.ttlAlerta,StringResources.lblPasswordIncorrect);
				}
			}
		}
        
        //Método para cambiar el idioma del sistema a español
        async void Btn_Espanol(object sender, RoutedEventArgs e)
        {
            DialogService dialog = new DialogService();
            await dialog.SendMessage("Atención","El idioma del sistema se ha cambiado a Español");
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
    }
}