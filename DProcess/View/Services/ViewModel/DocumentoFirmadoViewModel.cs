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
    public class DocumentoFirmadoViewModel : INotifyPropertyChanged
    {

        #region Attributes
        Usuario user;
        #endregion

        #region Properties
        private ObservableCollection<Documento> _ListaNumeroDocumento;
        public ObservableCollection<Documento> ListaNumeroDocumento
        {
            get { return _ListaNumeroDocumento; }
            set { _ListaNumeroDocumento = value; NotifyChange("ListaNumeroDocumento"); }
        }

        private ObservableCollection<Archivo> _ListaDocumentos = new ObservableCollection<Archivo>();
        public ObservableCollection<Archivo> ListaDocumentos
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
            get { return _SelectedDocumento; }
            set { _SelectedDocumento = value; NotifyChange("SelectedDocumento"); }
        }

        private Archivo _selectedItem;
        public Archivo SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyChange("SelectedItem");
            }

        }

        #endregion

        #region Constructor
        public DocumentoFirmadoViewModel(Usuario userConected)
        {
            user = userConected;
            ListaNumeroDocumento = DataManagerControlDocumentos.GetPendientes_Liberar(user.NombreUsuario);
            ListaDocumentos = new ObservableCollection<Archivo>();
        }

        public DocumentoFirmadoViewModel()
        {
            ListaDocumentos = new ObservableCollection<Archivo>();
        }
        #endregion

        #region Methods
        private async void AdjuntarArchivo()
        {
            ListaDocumentos.Clear();

            string filename = "";

            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();
            

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController AsyncProgress;

            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //Abre la ventana de explorador de archivos
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "PDF Files (.pdf)|*.pdf";

            Nullable<bool> result;
            // Mostrar el explorador de archivos
            result = dlg.ShowDialog();

            //Se crea el objeto de tipo archivo
            Archivo obj = new Archivo();

            if (result == true)
            {
                //Se obtiene el nombre del documento
                filename = dlg.FileName;

                //Si el archivo no está en uso
                if (!Module.IsFileInUse(filename))
                {
                    //Ejecutamos el método para enviar un mensaje de espera mientras se comprueban los datos.
                    AsyncProgress = await dialog.SendProgressAsync(StringResources.msgEspera, StringResources.msgInsertando);

                    //Se convierte el archvio a tipo byte y se le asigna al objeto
                    obj.archivo = await Task.Run(() => File.ReadAllBytes(filename));

                    //Obtiene la extensión del documento y se le asigna al objeto
                    obj.ext = System.IO.Path.GetExtension(filename);

                    //Se obtiene sólo el nombre, sin extensión.
                    obj.nombre = System.IO.Path.GetFileNameWithoutExtension(filename);

                    //asigna la imagen del pdf al objeto
                    obj.ruta = @"/Images/p.png";

                    DataManagerControlDocumentos.InsertDocumentoFirmado(obj, SelectedDocumento.version.id_version);

                    //si se agrego el archivo correspondiente lo agregamos a la lista temporal
                    ListaDocumentos.Add(obj);

                    //Ejecutamos el método para cerrar el mensaje de espera.
                    await AsyncProgress.CloseAsync();

                    await dialog.SendMessage("Genial!!!", "Tu documento ya fué enviado al administrador de CIT para su liberación.\nSi todo esta correcto recibiras un correo confirmando su liberación,\n de lo contrario recibiras un correo informandote el estátus de tu documento.");
                }
                else
                {
                    //Si el archivo está abierto mandamos un mensaje indicando que se debe cerrar el archivo para poder adjuntarlo.
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                }
            }

        }

        private void cambiarCombo()
        {
            ListaDocumentos.Clear();

            Archivo archivoGuardado = DataManagerControlDocumentos.GetDocumentoFirmado(SelectedDocumento.version.id_version);

            if (archivoGuardado.archivo != null && archivoGuardado.archivo.Length > 0)
            {
                archivoGuardado.ruta = @"/Images/p.png";
                ListaDocumentos.Add(archivoGuardado);
            }
        }


        /// <summary>
        /// Método para ver el contenido de los archivos
        /// </summary>
        /// <param name="item"></param>
        private async void verArchivo(Archivo item)
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();
            if (item != null)
            {
                try
                {
                    //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                    string filename = GetPathTempFile(item);

                    //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                    File.WriteAllBytes(filename, item.archivo);
                    //Se inicializa el programa para visualizar el archivo.
                    Process.Start(filename);
                }
                catch (Exception)
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorAbrir);
                }
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

        #region Commands

        /// <summary>
        /// Comando para ver el archivo desde la lista de documentos.
        /// </summary>
        public ICommand VerArchivo
        {
            get
            {
                return new RelayCommand(o => verArchivo(SelectedItem));
            }
        }


        /// <summary>
        /// Comando para agregar un documento a la lista, desde el explorador de archivos
        /// </summary>
        public ICommand _AdjuntarArchivo
        {
            get
            {
                return new RelayCommand(o => AdjuntarArchivo());
            }
        }

        public ICommand CambiarCombo
        {
            get
            {
                return new RelayCommand(o => cambiarCombo());
            }
        }
        #endregion

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
    }
}
