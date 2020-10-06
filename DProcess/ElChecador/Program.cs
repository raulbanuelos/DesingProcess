using System;
using System.Timers;
using System.Speech.Synthesis;
namespace ElChecador
{
    class Program
    {
        static void Main(string[] args)
        {
            //Timer aTimer = new Timer();
            //aTimer.Elapsed += ATimer_Elapsed;
            //aTimer.Interval = 1000;
            //aTimer.Enabled = true;

            Encriptar encriptar = new Encriptar();

            Console.WriteLine(encriptar.desencript("Ž¢©­¦nqs"));

            //SpeechSynthesizer _SS = new SpeechSynthesizer();
            //foreach (var item in _SS.GetInstalledVoices())
            //{
            //    var voice = (InstalledVoice)item;
            //    Console.WriteLine(voice.VoiceInfo.Name);

            //}

            //_SS.Volume = 100;
            //_SS.Rate = 1;

            //_SS.SpeakAsync("Welcome, Jorge Humberto, To Process Design Engineering Program");


            Console.WriteLine("Press \'q\' to quit the sample");
            while (Console.Read() != 'q');
        }

        private static void ATimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Clear();
            string mes = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string dia = DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            string hora = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            string minuto = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            string segundo = DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString();

            Console.WriteLine("Hora Actual:" + DateTime.Now.Year + "-" + mes + "-" + dia + " " + hora + ":" + minuto + ":" + segundo);
        }
    }
}
