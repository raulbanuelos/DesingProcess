using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Resources;

namespace View.Services.ViewModel
{
    class PendientesLiberarVM : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region Propiedades
        public Usuario usuario;

        private ObservableCollection<Documento> _ListaDocumentos;
        public ObservableCollection<Documento> ListaDocumentosValidar
        {
            get
            {
                return _ListaDocumentos;
            }
            set
            {
                _ListaDocumentos = value;
                NotifyChange("ListaDocumentosValidar");
            }
        }

        private Documento _SelectedDocumento;
        public Documento SelectedDocumento
        {
            get
            {
                return _SelectedDocumento;
            }
            set
            {
                _SelectedDocumento = value;
                NotifyChange("SelectedDocumento");
            }

        }
        #endregion

        #region Comandos

        /// <summary>
        /// Comando para abrir los documentos cuando
        /// estan en pendiente por liberar
        /// </summary>
        public ICommand VerArchivo
        {
            get
            {
                return new RelayCommand(param => verArchivo());
            }
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Método para ver el archivo
        /// </summary>
        /// <param name="id_archivo"></param>
        private async void verArchivo()
        {
            DialogService dialog = new DialogService();

            if (SelectedDocumento != null)
            {
                //obtenemos el id del documento
                int id_documento = SelectedDocumento.id_documento; 

                //obtenemos la version del documento
                int VersionDocumento = DataManagerControlDocumentos.GetVersionDocumento(id_documento);
                List<Archivo> lista = DataManagerControlDocumentos.GetArchivoFiltrado(VersionDocumento);

                //mostramos el archivo
                foreach (Archivo item in lista)
                {
                    //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                    string filename = GetPathTempFile(item);

                    File.WriteAllBytes(filename, item.archivo);
                    //Se inicializa el programa para visualizar el archivo.
                    Process.Start(filename);
                }

            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta,StringResources.msgSeleccioneNumero);
            }
        }

        /// <summary>
        /// Método que genera una cadena para cargar un archivo en la carpeta temporal del sistema.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetPathTempFile(Archivo item)
        {
            //Se guarda la ruta del directorio temporal.
            var tempFolder = Path.GetTempPath();
            string filename = string.Empty;
            //Realiza la acción hasta que el archivo se haya abierto
            do
            {
                //Genera un número aleatorio
                string aleatorio = Module.GetRandomString(5);
                //Crea la ruta temporal con el nombre del archivo y el número generado, y la extensión
                filename = Path.Combine(tempFolder, item.nombre + item.numero + "_" + aleatorio + item.ext);
            } while (File.Exists(filename));

            //retornamos el nombre que se generó
            return filename;
        }

        #endregion
        #region Constructor

        public PendientesLiberarVM(Usuario user)
        {
            usuario = user;
            ListaDocumentosValidar = DataManagerControlDocumentos.GetPendientes_Liberar(usuario.NombreUsuario);
        }
        #endregion
    }
}
