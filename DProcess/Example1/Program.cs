using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ninject;
using Model.ControlDocumentos;
using System.Collections.ObjectModel;
using System.IO;

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            saveFilesControlDocumentos();
            //saveFilesLeccionesAprendidas();
            //getPathFilesLearnedLessons();
            //IKernel kernel = new StandardKernel(new Bindings("Outlook"));

            //IMailSender mailSender = kernel.Get<IMailSender>();

            //mailSender.Send("raul.banuelos@mahle.com", "Testing sending email");
            //Console.ReadLine();

            //List<string> colors = new List<string>();
            //int length = 100;
            //int i = 0;
            //while(i < length)
            //{
            //    var random = new Random();
            //    var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"

            //    if (colors.Where(x => x.Equals(color)).ToList().Count == 0 )
            //    {
            //        colors.Add(color);
            //        i++;
            //    }
            //}

            //foreach (var color in colors)
            //{
            //    Console.WriteLine(color);
            //}

            Console.WriteLine("Process finished");
            Console.WriteLine("Press any key to finish...");
            Console.ReadLine();
        }

        static void getPathFilesLearnedLessons()
        {
            try
            {
                // Set a variable to the My Documents path.
                string docPath = @"C:\files_lecciones\";

                var txtFiles = Directory.EnumerateFiles(docPath, "*.*", SearchOption.AllDirectories);

                foreach (var item in txtFiles)
                {
                    Console.WriteLine(item);
                }
            }
            catch (UnauthorizedAccessException uAEx)
            {
                Console.WriteLine(uAEx.Message);
            }
            catch (PathTooLongException pathEx)
            {
                Console.WriteLine(pathEx.Message);
            }
        }

        static void saveFilesControlDocumentos()
        {
            try
            {
                Console.WriteLine("Starting...");

                int idMin = 10331;
                int idMax = idMin + 10;

                while (idMin <= 25350)
                {
                    Console.WriteLine("Get 10 next documents...");
                    ObservableCollection<Archivo> lista = DataManagerControlDocumentos.GetArchivo(idMin, idMax);
                    Console.WriteLine("The list has " + lista.Count + " files.");
                    Console.WriteLine("Starting to create folders...");
                    foreach (var archivo in lista)
                    {
                        string pathFile = getFolderControlDocumentos(archivo);
                        File.WriteAllBytes(pathFile, archivo.archivo);
                        Console.WriteLine("File saved: " + archivo.nombre + archivo.ext);
                    }
                    idMin += 11;
                    idMax = idMin + 10;
                    lista = null;
                }
            }
            catch (Exception er)
            {
                Console.WriteLine("Error" + er.Message);
            }
        }

        static string getFolderControlDocumentos(Archivo archivo)
        {
            string path = @"C:\files_control_documentos\" + archivo.id_version;

            if (!File.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                Console.WriteLine("Folder created : " + archivo.id_version);
            }

            path = path + @"\" + archivo.nombre + archivo.ext;

            return path;
        }

        static void saveFilesLeccionesAprendidas()
        {
            Console.WriteLine("Starting");
            ObservableCollection<Archivo_LeccionesAprendidas> lista = DataManagerControlDocumentos.GetArchivosLecciones();
            Console.WriteLine("The list has " + lista.Count + " files.");
            Console.WriteLine("Starting to create folders...");
            foreach (var archivo in lista)
            {
                string pathFile = getFolderLeccionesAprendidas(archivo);
                File.WriteAllBytes(pathFile, archivo.ARCHIVO);
                Console.WriteLine("File saved: " + archivo.NOMBRE_ARCHIVO + archivo.EXT);
            }

            Console.WriteLine("Finish");
        }

        static string getFolderLeccionesAprendidas(Archivo_LeccionesAprendidas archivo)
        {
            string path = @"C:\files_lecciones\" + archivo.ID_LECCIONES_APRENDIDAS;

            if (!File.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                Console.WriteLine("Folder created : " + archivo.ID_LECCIONES_APRENDIDAS);
            }

            path = path + @"\" + archivo.NOMBRE_ARCHIVO + archivo.EXT;

            return path;
        }
    }
}
