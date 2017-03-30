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
		}
		
		async void Btn_ingresar_Click(object sender, RoutedEventArgs e)
		{
            //Abrimos el mensaje modal para que el usuario ingrese sus credenciales, el resultado lo guardamos en una variable local.
			LoginDialogData result = await this.ShowLoginAsync("Authentication","Enter your credentials", new LoginDialogSettings{ColorScheme=this.MetroDialogOptions.ColorScheme,InitialUsername=""});

            //Comparamos si el resultado es distinto de nulo. Si es igual a nulo quiere decir que el usuario cancelo la captura o cerró inesperadamente la pantalla.
			if(result != null)
			{
                //Declaramos un objeto con el cual se realiza la encriptación
				Encriptacion encriptar = new Encriptacion();

                //Ejecutamos el método para encriptar tanto el usuario como la contraseña y los guardamos en variables locales respectivamente.
				string usuario = encriptar.encript(result.Username);
				string contrasena = encriptar.encript(result.Password);

                //Ejecutamos el método para verificar las credenciales, el resultado lo asignamos a un objeto local de tipo Usuario.
				Usuario usuarioConectado = await DataManager.GetLogin(usuario,contrasena);

                //Verificamos el resultado, si es direfente de nulo quiere decir que el logueo fué correcto, si es igual a nulo quiere decir que el usuario no existe con las credenciales proporcionadas.
				if (usuarioConectado != null) {

                    //Verificamos si el usuario no esta bloqueado.
					if (usuarioConectado.Block) {

                        //Enviamos un mensaje para indicar que el usuario está bloqueado.
                        MessageDialogResult message = await this.ShowMessageAsync("Information","This user is not active");
					}else{

                        //Enviamos un mensaje de bienvenida al usuario.
						MessageDialogResult message = await this.ShowMessageAsync("Welcome",usuarioConectado.Nombre);

                        //Una vez que el usuario hizo clic en aceptar el mensaje de bienvenida, se procede con la codificación de la presentación de la pantalla inicial.

                        //Creamos un objeto de tipo Home, la cual es la pantalla inicial del sistema.
                        Home PantallaHome = new Home();

                        //Creamos un objeto UsuarioViewModel, y le asignamos los valores correspondientes, a la propiedad Pagina se le asgina la pantalla inicial de Home.
                        UsuarioViewModel context = new UsuarioViewModel { ModelUsuario = usuarioConectado, Pagina = PantallaHome };

                        //Asignamos al DataContext de la PantallaHome el context creado anteriormente.
                        //NOTA IMPORTANTE: Se hizo una redundancia al asignarle en la propiedad Pagina su misma pantalla. Solo es por ser la primeva vez y tenernos en donde descanzar la primera pantalla.
                        PantallaHome.DataContext = context;

                        //Declaramos la pantalla en la que descanzan todas las páginas.
                        Layout masterPage = new Layout();

                        //Asingamos el DataContext.
                        masterPage.DataContext = context;

                        //Ejecutamos el método el cual despliega la pantalla.
                        masterPage.ShowDialog();
                    }
				}
				else{

                    //Enviamos un mensaje indicando que las credenciales escritas son incorrectas.
					MessageDialogResult message = await this.ShowMessageAsync("Alert","Your user/password are incorrects");
				}
			}
		}
	}
}