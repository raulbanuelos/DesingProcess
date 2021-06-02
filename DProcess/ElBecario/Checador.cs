using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Services;

namespace ElBecario
{
    public class Checador
    {
        public void iniciarProceso()
        {
            List<DO_SolicitudCorreo> solicitudes = DataManager.GetSolicitudCorreoPendientes();
            ServiceEmail ServiceEmail = new ServiceEmail();

            if (solicitudes.Count > 0)
            {
                Console.WriteLine("There are " + solicitudes.Count + " awaiting request: " + DateTime.Now.ToLongTimeString());
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Attending...");
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                Console.WriteLine("There are not awaiting request: " + DateTime.Now.ToLongTimeString());
            }

            foreach (var solicitud in solicitudes)
            {
                string[] recipients = solicitud.Recipients.Split(',');

                List<string> attachments = new List<string>();

                if (solicitud.Origen == "LECCIONES_APRENDIDAS")
                {
                    ObservableCollection<Archivo_LeccionesAprendidas> ListaArchivosLecciones = DataManagerControlDocumentos.GetArchivosLecciones(solicitud.idArchivo);

                    List<Archivo> ListaArchivos = new List<Archivo>();

                    foreach (var archivoLecciones in ListaArchivosLecciones)
                    {
                        Archivo archivo = new Archivo();

                        archivo.archivo = File.ReadAllBytes(archivoLecciones.NOMBRE_ARCHIVO);
                        archivo.nombre = archivoLecciones.NOMBRE_ARCHIVO;
                        archivo.ext = archivoLecciones.EXT;
                        archivo.numero = 666;

                        string pathFile = saveFileTemp(archivo);

                        attachments.Add(pathFile);
                    }
                }
                else if(solicitud.Origen == "CONTROL_DOCUMENTOS")
                {
                    //TODO: Get files of documents control
                    attachments = new List<string>();
                }

                bool banSent = ServiceEmail.SendEmailOutlook(recipients, solicitud.Title, solicitud.Body, attachments);

                if (banSent)
                {
                    DataManager.SetEjecutadaSolicitudCorreo(solicitud.IdSolicitudCorreo);
                }
            }
        }

        private string saveFileTemp(Archivo archivo)
        {
            if (archivo != null)
            {
                try
                {
                    //Get temp path
                    string filename = GetPathTempFile(archivo);

                    //Save file
                    File.WriteAllBytes(filename, archivo.archivo);

                    return filename;

                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
            else return string.Empty;
        }

        private string GetPathTempFile(Archivo item)
        {
            var tempFolder = Path.GetTempPath();
            string filename = string.Empty;

            do
            {
                string aleatorio = Module.GetRandomString(5);

                filename = Path.Combine(tempFolder, item.nombre + item.numero + "_" + aleatorio + item.ext);
            } while (File.Exists(filename));

            return filename;
        }
    }
}
