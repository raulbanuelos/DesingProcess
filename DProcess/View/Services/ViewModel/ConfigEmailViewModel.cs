using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.ViewModel
{
    public class ConfigEmailViewModel
    {

        private Usuario user;
        private string goodPath;

        public ConfigEmailViewModel(Usuario user)
        {
            this.user = user;
        }

        /// <summary>
        /// Método para configurar cuenta de Email
        /// </summary>
        /// <returns></returns>
        public Task<DO_PathMail> setEmail()
        {
            return Task.Run(() =>
            {
                // Se inicializan los servicios
                ServiceEmail SO_Email = new ServiceEmail();
                List<string> paths = new List<string>();
                DO_PathMail respuesta = new DO_PathMail();

                int c = 0;
                string fileRule = "*.nsf";
                string[] users = new string[1];
                users[0] = user.Correo;
                string bodyTest = "<P><BR><FONT size=5><EM>Esta es una prueba</EM> de envío</FONT> de <U>correo electrónico</U> a <EM>través de la plataforma</EM> de <STRONG><U><FONT style=\"BACKGROUND - COLOR: #00ffff\">Diseño del Proceso.</FONT></U></STRONG></P>";
                bodyTest += "<P>&nbsp;<FONT size=6><FONT style=\"BACKGROUND - COLOR: #339966\">Si usted puede visualizar</FONT> este</FONT> <FONT color=#0000ff>correo en Lotus Notes</FONT>, <FONT color=#800080 size=2>significa que podrá</FONT><FONT size=7> </FONT><FONT color=#808000><FONT size=6>enviar correos a</FONT> través de la plataforma</FONT> de <STRONG><FONT style=\"BACKGROUND-COLOR: #00ffff\">Diseño del Proceso.</FONT></STRONG></P>";

                string[] directories = new string[4];

                // Se agregan rutas problables establecidas
                directories[0] = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Lotus\";
                directories[1] = @"C:\Program Files (x86)\IBM\Lotus\";
                directories[2] = @"C:\Program Files\IBM\Lotus\";
                directories[3] = @"c:\Program Files\IBM.old\Lotus\Notes\Data\";

                foreach (var path in directories)
                {
                    if (Directory.Exists(path))
                    {
                        // Realiza la búsqueda del archivo en los directorios
                        string[] files = Directory.GetFiles(path, fileRule, SearchOption.AllDirectories);
                        paths.AddRange(files);
                    }
                }

                while (c < paths.Count && !respuesta.respuesta)
                {
                    respuesta.respuesta = SO_Email.SendEmailLotusCustom(paths[c], users, "Diseño del Proceso : Correo electrónico de prueba", bodyTest);
                    goodPath = respuesta.respuesta ? paths[c] : string.Empty;
                    c++;
                }

                //Si no se obtiene respuesta, buscamos en todo el disco Local C.
                if (!respuesta.respuesta)
                {
                    paths = GetFiles(@"c:\", fileRule);
                    c = 0;

                    while (c < paths.Count && !respuesta.respuesta)
                    {
                        respuesta.respuesta = SO_Email.SendEmailLotusCustom(paths[c], users, "Diseño del Proceso : Correo electrónico de prueba", bodyTest);
                        goodPath = respuesta.respuesta ? paths[c] : string.Empty;
                        c++;
                    }
                }

                if (respuesta.respuesta)
                    actualizarPath(goodPath, user.NombreUsuario);

                // Asignamos el valor de la ruta
                respuesta.rutamail = goodPath;

                // Retornamos el objeto con los valores aseignados
                return respuesta;
            });
        }

        /// <summary>
        /// Método que obtiene todos los archivos con el patrón de busqueda deseado. Busca a partir de una ruta en especifico y todas sus subcarpetas.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static List<string> GetFiles(string path, string pattern)
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                foreach (var directory in Directory.GetDirectories(path))
                {
                    files.AddRange(GetFiles(directory, pattern));
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (PathTooLongException)
            {
            }

            return files;
        }        

        /// <summary>
        /// Método para actualizar path en tablas SQL
        /// </summary>
        /// <param name="path"></param>
        /// <param name="userName"></param>
        private void actualizarPath(string path, string userName)
        {
            DataManager.UpdatePathEmailUser(path, userName);
            DataManager.UpdateUserIsAvailableEmail(userName, true);
        }
    }
}
