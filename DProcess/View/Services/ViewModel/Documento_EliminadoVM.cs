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
    class Documento_EliminadoVM : INotifyPropertyChanged
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
        DialogService dialog = new DialogService();

        private ObservableCollection<Documento> _ListaDocumentos;
        public ObservableCollection<Documento> ListaDocumentos {
            get {
                return _ListaDocumentos;
                }
            set {
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

        private ObservableCollection<Archivo> _ListaArchivo;
        public ObservableCollection<Archivo> ListaArchivo {
            get
            {
                return _ListaArchivo;
            }
            set
            {
                _ListaArchivo = value;
                NotifyChange("ListaArchivo");
            }
        }

        private Archivo _SelectedArchivo;
        public Archivo SelectedArchivo
        {
            get
            {
                return _SelectedArchivo;
            }
            set
            {
                _SelectedArchivo = value;
                NotifyChange("SelectedArchivo");
            }
        }

        #endregion

        #region Constructor
        public Documento_EliminadoVM()
        {
            //Obtiene toda la lista de los documentos eliminados
            GetListaDocumento(string.Empty);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Comando que busca los documentos de acuerdo al parámetro recibido
        /// </summary>
        public ICommand BuscarDocumento
        {
            get
            {
                return new RelayCommand(param => GetListaDocumento((string)param));
            }
        }

        /// <summary>
        /// Comando para visualizar la lista de archivos del documento seleccionado
        /// </summary>
        public ICommand VerArchivo
        {
            get
            {
                return new RelayCommand(o => ver_archivo());
            }
        }

        /// <summary>
        /// Comando para abrir el archivo seleccionado
        /// </summary>
        public ICommand AbrirArchivo
        {
            get
            {
                return new RelayCommand(o => abrirArchivo());
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Método que obtiene la lista de los documentos de acuerdo a número de documetno
        /// </summary>
        /// <param name="texto"></param>
        private void GetListaDocumento(string texto)
        {
            //Obtiene los documentos eliminados que coinciden con el texto
            ListaDocumentos = DataManagerControlDocumentos.GetAllDocumento_Eliminado(texto);
        }

        /// <summary>
        /// Método que muestra la lista de archivos del documentos seleccionado
        /// </summary>
        private void ver_archivo()
        {
            if (SelectedDocumento !=null)
            {
                //Obtiene la lista de archivos
                ListaArchivo = DataManagerControlDocumentos.GetArchivo_DocumentoEliminado(SelectedDocumento.id_documento);
            }
        }

        /// <summary>
        /// Método que abre el archivo
        /// </summary>
        private async void abrirArchivo()
        {
            try
            {
                //Si se seleccionó un archivo
                if (SelectedArchivo != null)
                {
                    //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                    string filename = GetPathTempFile(SelectedArchivo);

                    //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                    File.WriteAllBytes(filename, SelectedArchivo.archivo);

                    //Se inicializa el programa para visualizar el archivo.
                    Process.Start(filename);
                }
            }
            catch (IOException er)
            {
                //Si hubo error al abrir el archivo muestra un mensaje 
                await dialog.SendMessage("Alerta", "Error al abrir el archivo...");
            }
        }

        /// <summary>
        /// Asigna la ruta temporal a cada archivo que se seleccione para abrir
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

        /// <summary>
        /// Método que busca el documento en la lista
        /// </summary>
        /// <param name="parametro"></param>
        private void buscarDocumento(string parametro)
        {
            //Manda a llamar al método que obtiene los documentos
            GetListaDocumento(parametro);

            //Si la lista de archivos contiene información, la limpia
            if (ListaArchivo != null)
                ListaArchivo.Clear();
        }

        #endregion


    }
}
