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

namespace View.Services.ViewModel
{
    public class RecursoViewModel : INotifyPropertyChanged
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

        #region Properties
        private ObservableCollection<Archivo> _ListaArchivo = new ObservableCollection<Archivo>();
        public ObservableCollection<Archivo> ListaArchivo
        {
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

        private TipoDocumento selectedTipoDocumento;
        public TipoDocumento SelectedTipoDocumento
        {
            get
            {
                return selectedTipoDocumento;
            }
            set
            {
                selectedTipoDocumento = value;
                NotifyChange("SelectedTipoDocumento");
            }
        }

        private ObservableCollection<TipoDocumento> _ListaTipoDocumento;
        public ObservableCollection<TipoDocumento> ListaTipoDocumento
        {
            get
            {
                return _ListaTipoDocumento;
            }
            set
            {
                _ListaTipoDocumento = value;
                NotifyChange("ListaTipoDocumento");
            }
        }

        private bool _bttnGuardar;
        public bool BttnGuardar
        {
            get
            {
                return _bttnGuardar;
            }
            set
            {
                _bttnGuardar = value;
                NotifyChange("BttnGuardar");
            }
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

        #region Commands
        public ICommand ConsultarArchivos
        {
            get
            {
                return new RelayCommand(o => Consultar());
            }
        }

        public ICommand AgregarArchivo
        {
            get
            {
                return new RelayCommand(o => InsertArchivo());
            }
        }
        
        /// <summary>
        /// Método para eliminar un item de listBox
        /// </summary>
        public ICommand EliminarItem
        {
            get
            {
                return new RelayCommand(o => eliminarItem(SelectedItem));
            }
        }

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
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor por default.
        /// </summary>
        /// <param name="ModelUsuario"></param>
        public RecursoViewModel(Usuario ModelUsuario)
        {
            //Obtenemos la lista de tipo de documento.
            ListaTipoDocumento = DataManagerControlDocumentos.GetTipo();

            //Verificamos que los elementos de la lista sean al menos uno, y seleccionamos el primer elemeto.
            if (_ListaTipoDocumento.Count > 0)
            {
                SelectedTipoDocumento = _ListaTipoDocumento[0];
            }

            //Si es rol administrador del cit permitirmos que el usuario pueda agregar mas archivos.
            BttnGuardar = Module.UsuarioIsRol(ModelUsuario.Roles, 2) ? true : false;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Método que asigna a la lista de archivos todos los archivos correspondientes.
        /// </summary>
        private void Consultar()
        {
            ListaArchivo = DataManagerControlDocumentos.GetRecursosTipoDocumento(SelectedTipoDocumento.id_tipo);
        }

        /// <summary>
        /// Método que inserta un archivo.
        /// </summary>
        private async void InsertArchivo()
        {
            //Inicializamos los servici de Dialog.
            DialogService ServiceDialog = new DialogService();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController AsyncProgress;

            //Declaramos un objeto de tipo OpenFileDialog.
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            //Abrimos la ventana para que seleccione un archivo.
            Nullable<bool> result = dialog.ShowDialog();

            //Verificamos si el usuario eligió un documento.
            if (result == true)
            {
                try
                {
                    //Obtenemos el nombre del archivo y lo asignamos a una variable.
                    string fileName = dialog.FileName;

                    //Declaramos un objeto de tipo Archivo.
                    Archivo obj = new Archivo();
                    //Si el archivo no está en uso
                    if (!IsFileInUse(fileName)) {
                        //Ejecutamos el método para enviar un mensaje de espera mientras se comprueban los datos.
                        AsyncProgress = await ServiceDialog.SendProgressAsync("Por favor espere", "Adjuntando archivo...");

                        //Asignamos el archivo que seleccionó el usuario al objeto declarado.
                        obj.archivo = await Task.Run(() => File.ReadAllBytes(fileName));
                        obj.ext = System.IO.Path.GetExtension(fileName);
                        obj.nombre = System.IO.Path.GetFileNameWithoutExtension(fileName);

                        //Insertamos el archivo.
                        int r = DataManagerControlDocumentos.InsertRecurso(obj.archivo, obj.nombre, obj.ext, obj.nombre, SelectedTipoDocumento.id_tipo);

                        //Ejecutamos el método para cerrar el mensaje de espera.
                        await AsyncProgress.CloseAsync();

                        //Enviamos un mensaje dependiendo la respuesta.
                        if (r > 0)
                        {
                            await ServiceDialog.SendMessage("Información", "Archivo agregado correctamente");
                            Consultar();
                        }

                        else
                            await ServiceDialog.SendMessage("Alerta", "Error al guardar el archivo");
                    }
                    else
                    {
                        await ServiceDialog.SendMessage("Alerta", "Cierre el archivo para continuar..");
                    }
                }
                catch (IOException)
                {
                    await ServiceDialog.SendMessage("Alerta", "Cierre el archivo para continuar..");
                }
            }
        }

        /// <summary>
        /// Método que verifica si un archivo está siendo usado por otro programa 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsFileInUse(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("'path' cannot be null or empty.", "path");
            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) { }
            }
            catch (IOException)
            {
                //si el archivo está abierto, retorna verdadero
                return true;
            }
            //Si el archivo no está en uso retorna falso
            return false;
        }

        /// <summary>
        /// Método para ver el archivo que está en la lista de documetnos
        /// </summary>
        /// <param name="item"></param>
        private void verArchivo(Archivo item)
        {
            if (item != null)
            {
                //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                string filename = GetPathTempFile(item);

                //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                File.WriteAllBytes(filename, item.archivo);
                //Se inicializa el programa para visualizar el archivo.
                Process.Start(filename);
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

        /// <summary>
        /// Método que elimina el archvio seleccionado de la lista de Documento
        /// Elimina el archivo de la base de datos
        /// </summary>
        /// <param name="item"></param>
        private async void eliminarItem(Archivo item)
        {
            //Incializamos los servicios de dialog.
            DialogService dialogService = new DialogService();

            if (item != null)
            {
                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = "SI";
                setting.NegativeButtonText = "NO";

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialogService.SendMessage("Attention", "¿Desea eliminar el archivo?", setting, MessageDialogStyle.AffirmativeAndNegative);

                if (item != null & result == MessageDialogResult.Affirmative)
                {
                    //Se elimina el item seleccionado de la listaDocumentos.
                    ListaArchivo.Remove(item);

                    //Eliminamos el archivo y guardamos el número de registros afectados.
                    int r = DataManagerControlDocumentos.DeleteRecurso(item.id_archivo);

                    //Si el resutaldo es mayor a cero entonces confirmamos al usuario si se eliminó. Sino mandamos un mensaje de alerta.
                    if (r > 0)
                        await dialogService.SendMessage("Información", "El archivo fue eliminado correctamente");
                    else
                        await dialogService.SendMessage("Alerta", "Error al eliminar el archivo");
                }
            }
        }
        #endregion
    }
}
