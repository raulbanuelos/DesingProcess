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
    public class VersionesVM : INotifyPropertyChanged
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

        private ObservableCollection<Model.ControlDocumentos.Version> _Lista;
        public ObservableCollection<Model.ControlDocumentos.Version> Lista
        {
            get
            {
                return _Lista;
            }
            set
            {
                _Lista = value;
                NotifyChange("Lista");
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

        private Model.ControlDocumentos.Version _SelectedVersion;
        public Model.ControlDocumentos.Version SelectedVersion
        {
            get
            {
                return _SelectedVersion;
            }
            set
            {
                _SelectedVersion = value;
                NotifyChange("SelectedVersion");
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


        #endregion
        #region Constructor
        public VersionesVM(Documento objDocument)
        {
            Lista = DataManagerControlDocumentos.GetVersiones_Documento(objDocument.id_documento);


        }
        #endregion

        #region Metodos

        /// <summary>
        /// Comando para visualizar el archivo especificado
        /// </summary>
        public ICommand AbrirArchivo
        {
            get
            {
               return new RelayCommand(o => abrirArchivo());
            }
        }

        /// <summary>
        /// Método que abre el archivo de una versión
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

        private string GetPathTempFile(Archivo item)
        {
            //Se guarda la ruta del directorio temporal.
            var tempFolder = Path.GetTempPath();

            string filename = string.Empty;
            do
            {
                string aleatorio = Module.GetRandomString(5);
                filename = Path.Combine(tempFolder, item.nombre  + "_" + aleatorio + item.ext);
            } while (File.Exists(filename));

            return filename;
        }

        public ICommand verArchivos
        {
            get
            {
                return new RelayCommand(o => listarArchivos());
            }
        }

        private void listarArchivos()
        {
            if (SelectedVersion != null)
            {
                ListaArchivos = DataManagerControlDocumentos.GetArchivos(SelectedVersion.id_version);
            }
        }
        #endregion
    }
}
