using Encriptar;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemonDocumentsControl
{
    public class Login
    {
        public Usuario User;

        public Login()
        {
           
        }
        
        public async Task<bool> login()
        {
            Console.WriteLine("Escribe tu usuario:");
            string usuario = Console.ReadLine();

            Console.WriteLine("Escribe tu contraseña:");
            string contrasena = Console.ReadLine();

            Encriptacion encriptar = new Encriptacion();

            usuario = encriptar.encript(usuario);
            contrasena = encriptar.encript(contrasena);

            //Ejecutamos el método para verificar las credenciales, el resultado lo asignamos a un objeto local de tipo Usuario.
            Usuario usuarioConectado = await DataManager.GetLogin(usuario, contrasena);
           

            if (usuarioConectado != null)
            {
                if (usuarioConectado.Block)
                {
                    Console.WriteLine("Tu estas bloqueado, no vas a poder accesar.");
                    return false;
                }
                else
                {
                    //Obtenemos la fecha del servidor
                    DateTime date_now = DataManagerControlDocumentos.Get_DateTime();

                    //Ejecutamos el método para desbloquear el sistema, si se venció la fecha final
                    DataManagerControlDocumentos.DesbloquearSistema(date_now);

                    //Obtenemos los detalles del usuario logueado.
                    usuarioConectado.Details = DataManager.GetUserDetails(usuarioConectado.NombreUsuario);

                    //Insertamos el ingreso a la bitácora.
                    DataManager.InserIngresoBitacora(Environment.MachineName, usuarioConectado.Nombre + " " + usuarioConectado.ApellidoPaterno + " " + usuarioConectado.ApellidoMaterno);

                    Console.WriteLine("Hola, soy el Demonio que realizará el trabajo aburrido.\n");
                    System.Threading.Thread.Sleep(4000);

                    Console.WriteLine("Todo esto con la intención de hacerte felíz\n");
                    System.Threading.Thread.Sleep(3000);

                    User = usuarioConectado;

                    return true;
                }
            }
            else
            {
                Console.WriteLine("Uusario y/O Contraseña incorrecta");
                return false;
            }
        }
    }
}
