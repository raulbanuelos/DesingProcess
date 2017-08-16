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

namespace View.Services.ViewModel
{
    public class Documentos_SimilaresVM : INotifyPropertyChanged
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

        private ObservableCollection<Documento> _ListaDocumentos;
        public ObservableCollection<Documento> ListaDocumentos
        {
            get
            {
                return _ListaDocumentos;
            }
            set
            {
                _ListaDocumentos = value;
                NotifyChange("ListaDocumentos");
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

        private Archivo _selectedArchivo;
        public Archivo SelectedArchivo
        {
            get
            {
                return _selectedArchivo;
            }
            set
            {
                _selectedArchivo = value;
                NotifyChange("SelectedArchivo");
            }

        }

        private ObservableCollection<Archivo> _ListaArchivos;
        public ObservableCollection<Archivo> ListaArchivos
        {
            get
            {
                return _ListaArchivos;
            }
            set
            {
                _ListaArchivos = value;
                NotifyChange("ListaArchivos");
            }
        }

        #endregion

        #region Commands
        /// <summary>
        /// Comando que muestra los archivos de un documento 
        /// </summary>
        public ICommand ListarArchivos
        {
            get
            {
                return new RelayCommand(o => verArchivos());
            }
        }
        /// <summary>
        /// método que muestra los archivos del documento seleccionado
        /// </summary>
        private void verArchivos()
        {
            //si el documento es diferente de nulo, si se seleccionó algún documento
            if (_SelectedDocumento !=null)
            {
                //Obtiene los archivos de la versión los guarda en la lista, para mostrarlos
                ListaArchivos = DataManagerControlDocumentos.GetArchivos(SelectedDocumento.version.id_version);
            }
        }
        /// <summary>
        /// Comando para abrir un archivo 
        /// </summary>
        public ICommand AbrirArchivo
        {
            get
            {
                return new RelayCommand(o => abrirArchivo());
            }
        }
        /// <summary>
        /// Método para descargar y visualizar el archivo seleccionado
        /// </summary>
        private void abrirArchivo()
        {
            if (SelectedArchivo !=null)
            {
                int id_archivo = SelectedArchivo.id_archivo;
                //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                string filename = GetPathTempFile(SelectedArchivo);

                //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                File.WriteAllBytes(filename, SelectedArchivo.archivo);

                //Se inicializa el programa para visualizar el archivo.
                Process.Start(filename);
            }
        }
         /// <summary>
         /// Comando que obtiene un nombre aleatorio para guardar el archivo temporalmente
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
                filename = Path.Combine(tempFolder, item.nombre + "_" + aleatorio + item.ext);
            } while (File.Exists(filename));

            //Retorna la ruta del archivo
            return filename;
        }

        #endregion

        #region Constructor
        public Documentos_SimilaresVM(ObservableCollection<Documento> ListaSimilares)
        {
            ListaDocumentos = ListaSimilares;
        }
        #endregion
    }
}
