using System;
using System.Timers;
using System.Speech.Synthesis;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace ElChecador
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello, I´m your personal assistance, I will help you in send email that other users requested");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("I will star by looking if there is any pending task");
            System.Threading.Thread.Sleep(3000);

            Checador checador = new Checador();
            checador.iniciarProceso();

            Timer timer = new Timer(60000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            Console.WriteLine("Press any Key to exit...");
            Console.ReadKey();

        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Checador checador = new Checador();
            checador.iniciarProceso();
        }
    }
}