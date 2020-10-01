using Model;
using System;

namespace ElBecario
{
    class Program
    {
        static void Main(string[] args)
        {
            Usuario usuario = new Usuario();

            usuario.ApellidoMaterno = "DÍAZ";
            usuario.ApellidoPaterno = "BAÑUELOS";
            usuario.Block = false;
            usuario.Correo = "raul.banuelos@mx.mahle.com";
            usuario.IdUsuario = "¢¥®ª¯";
            usuario.Nombre = "RAÚL";
            usuario.NombreUsuario = "admin";
            usuario.Pathnsf = @"C:\Users\M0051722\AppData\Local\Lotus\Notes\Data\as_RBanuelos.nsf";

            ObserverSolicitudes observer = new ObserverSolicitudes(usuario);

            Console.WriteLine("Si quieres darme un descanzo, solo oprime cualquier tecla y me ire a dormir.\n");

            Console.SetCursorPosition(5, 10);
            
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Saliendo en...");
            Console.Write("3");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            
            Console.WriteLine("Saliendo en...");
            Console.Write("2");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();

            Console.WriteLine("Saliendo en...");
            Console.Write("1");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            
            Console.WriteLine("Adios puñetas!\n");
            System.Threading.Thread.Sleep(1000);
        }
    }
}
