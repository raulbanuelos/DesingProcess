using MahApps.Metro.Controls;
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
using System.Windows;
using System.Windows.Input;
using View.Forms.ControlDocumentos;

namespace View.Services.ViewModel
{
    public class DocumentoViewModel : INotifyPropertyChanged
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
        public Usuario User;

        private string nombre;
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
                NotifyChange("Nombre");
            }
        }

        private string version;
        public string Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
                NotifyChange("Version");
            }
        }

        private DateTime fecha = DateTime.Now;
        public DateTime Fecha
        {
            get
            {

                return this.fecha;
            }
            set
            {
                this.fecha = value;
                NotifyChange("Fecha");
            }
        }

        private string copias;
        public string Copias
        {
            get
            {
                return this.copias;
            }
            set
            {
                this.copias = value;
                NotifyChange("Copias");
            }
        }

        private string descripcion;
        public string Descripcion
        {
            get
            {
                return this.descripcion;
            }
            set
            {
                this.descripcion = value;
                NotifyChange("Descripcion");
            }
        }

        private int id_documento;
        public int ID_documento
        {
            get
            {
                return id_documento;
            }
            set
            {
                id_documento = value;
                NotifyChange("ID_documento");
            }
        }

        private int _id_tipo;
        public int id_tipo
        {
            get
            {
                return _id_tipo;
            }
            set
            {
                _id_tipo = value;
                NotifyChange("id_tipo");
            }
        }

        private string botonGuardar;
        public string BotonGuardar
        {
            get
            {
                return botonGuardar;
            }
            set
            {
                botonGuardar = value;
                NotifyChange("BotonGuardar");
            }
        }

        private int _id_dep;
        public int id_dep {
            get
            {
                return _id_dep;
            }

            set
            {
                _id_dep = value;
                NotifyChange("id_dep");
            }

        }

        private string _usuario;
        public string usuario { get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                NotifyChange("usuario");
            }
        }

        private string _usuarioAutorizo;
        public string usuarioAutorizo
        {
            get
            {
                return _usuarioAutorizo;
            }
            set
            {
                _usuarioAutorizo = value;
                NotifyChange("usuarioAutorizo");
            }
        }

        private int idVersion;

        private string auxcopias, auxversion, auxUsuario,auxUsuario_Autorizo;

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

        private ObservableCollection<TipoDocumento> _ListaTipo;
        public ObservableCollection<TipoDocumento> ListaTipo
        {
            get
            {
                return _ListaTipo;
            }
            set
            {
                _ListaTipo = value;
                NotifyChange("ListaTipo");
            }
        }

        private ObservableCollection<Departamento> _ListaDepartamento;
        public ObservableCollection<Departamento> ListaDepartamento {
            get
            {
                return _ListaDepartamento;
            }
        set
            {
                _ListaDepartamento = value;
                NotifyChange("ListaDepartamento");
            }

        }

        private ObservableCollection<Usuarios> _ListaUsuarios;
        public ObservableCollection<Usuarios> ListaUsuarios {
            get
            {
                return _ListaUsuarios;
            }
            set
            {
                _ListaUsuarios = value;
                NotifyChange("ListaUsuarios");
            }
        }

        private ObservableCollection<Model.ControlDocumentos.Version> ListaVersiones = new ObservableCollection<Model.ControlDocumentos.Version>();
        private ObservableCollection<Archivo> ListaArchivo = new ObservableCollection<Archivo>();

        private ObservableCollection<Documento> _ListaNumeroDocumento;
        public ObservableCollection<Documento> ListaNumeroDocumento {
            get
            {
                return _ListaNumeroDocumento;
            }
            set
            {
                _ListaNumeroDocumento = value;
                NotifyChange("ListaNumeroDocumento");
            }
        }
       
        private Documento _selectedDocumento;
        public Documento SelectedDocumento {
            get
            {
                return _selectedDocumento;
            }
            set
            {
                _selectedDocumento = value;
                NotifyChange("SelectedDocumento");
            }
        }

        private Archivo _selectedItem;
        public Archivo SelectedItem {
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

        private bool _bttnEliminar;
        public bool BttnEliminar {
            get
            {
                return _bttnEliminar;
            }
            set
            {
                _bttnEliminar = value;
                NotifyChange("BttnEliminar");
            }
        }

        private bool _bttnModificar;
        public bool BttnModificar
        {
            get
            {
                return _bttnModificar;
            }
            set
            {
                _bttnModificar = value;
                NotifyChange("BttnModificar");
            }
        }

        private bool _bttnVersion;
        public bool BttnVersion
        {
            get
            {
                return _bttnVersion;
            }
            set
            {
                _bttnVersion = value;
                NotifyChange("BttnVersion");
            }
        }

        private bool _bttnGuardar;
        public bool BttnGuardar { get
            {
                return _bttnGuardar;
            }
            set
            {
                _bttnGuardar = value;
                NotifyChange("BttnGuardar");
            }
        }

        private bool nombreEnabled;
        public bool NombreEnabled {
            get
            {
                return nombreEnabled;
            }
            set
            {
                nombreEnabled = value;
                NotifyChange("NombreEnabled");
            }
        }

        private bool versionEnabled = false;
        public bool VersionEnabled
        {
            get
            {
                return versionEnabled;
            }
            set
            {
                versionEnabled = value;
                NotifyChange("VersionEnabled");
            }
        }

        private bool tipoEnabled = true;
        public bool TipoEnabled
        {
            get
            {
                return tipoEnabled;
            }
            set
            {
                tipoEnabled = value;
                NotifyChange("TipoEnabled");
            }
        }

        private bool departamentoEnabled = true;
        public bool DepartamentoEnabled
        {
            get
            {
                return departamentoEnabled; ;
            }
            set
            {
                departamentoEnabled = value;
                NotifyChange("DepartamentoEnabled");
            }
        }

        private bool _bttnCancelar;
        public bool BttnCancelar {
            get
            {
                return _bttnCancelar;
            }
            set
            {
                _bttnCancelar = value;
                NotifyChange("BttnCancelar");
            }
        }

        #endregion

        #region Constructor
        public DocumentoViewModel(string _nombre, string _version, string _copias, string _descripcion, int _id_documento,int _idDep,int _id_version,DateTime fecha_revision)
        {
            Inicializar();
            Nombre = _nombre;
            User = new Usuario();
            Version = _version;
            Fecha = fecha_revision;
            auxversion = _version;
            auxcopias = _copias;
            Copias = _copias;
            Descripcion = _descripcion;
            id_documento = _id_documento;
            BotonGuardar = "Guardar";
            BttnGuardar = false;
            BttnEliminar = true;
            BttnModificar = true;
            BttnVersion = true;
            NombreEnabled = false;
            idVersion = _id_version;
            id_dep = _idDep;
            id_tipo = DataManagerControlDocumentos.GetTipoDocumento(_id_documento);

            ObservableCollection<Model.ControlDocumentos.Version> ListaUsuario = DataManagerControlDocumentos.GetIdUsuario(_id_version);
            foreach (var item in ListaUsuario)
            {
                usuario = item.id_usuario;
                auxUsuario = usuario;
                usuarioAutorizo = item.id_usuario_autorizo;
                auxUsuario_Autorizo = usuarioAutorizo;
            }
          
            ObservableCollection<Documento> Lista = DataManagerControlDocumentos.GetTipo(_id_documento,idVersion);

            foreach (var item in Lista)
            {
                Archivo objArchivo = new Archivo();

                objArchivo.nombre = item.nombre;
                objArchivo.id_archivo = item.version.archivo.id_archivo;
                objArchivo.archivo = item.version.archivo.archivo;
                objArchivo.ext = item.version.archivo.ext;

                if (objArchivo.ext == ".pdf")
                {
                    //asigna la imagen del pdf al objeto
                    objArchivo.ruta = @"/Images/p.png";
                }
                else
                {
                    //Si es archivo de word asigna la imagen correspondiente.
                    objArchivo.ruta = @"/Images/w.png";
                }
                ListaDocumentos.Add(objArchivo);
            }

            
        }

        public DocumentoViewModel(Usuario usuario)
        {
            BotonGuardar = "Guardar";
            BttnGuardar = true;
            Version = "1";
            User = usuario;
            NombreEnabled = true;
            TipoEnabled = false;
            DepartamentoEnabled = false;
            ListaNumeroDocumento = DataManagerControlDocumentos.GetDocumento_SinVersion(User.NombreUsuario);
            Inicializar();
        }

        #endregion

        #region Commands
        /// <summary>
        /// Comando para guardar un registro de documento
        /// </summary>
        public ICommand GuardarControl
        {
            get
            {
                return new RelayCommand(o => guardarControl());
            }
        }
        private async void guardarControl()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController controllerProgressAsync;

            //Verificamos que el botón contenga la leyenda Guardar, esto indica que el registro es nuevo.
            if (BotonGuardar == "Guardar")
            {
                //Ejecutamos el método para verificar que todos los campos contengan valores.
                if (ValidarValores())
                {
                    //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
                    controllerProgressAsync = await dialog.SendProgressAsync("Por favor espere", "Guardando el documento...");

                    //Declaramos un objeto de tipo documento.
                    Documento obj = new Documento();
                    
                    //Declaramos un objeto de tipo Version.
                    Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

                    //Mapeamos los valores al objeto declarado.
                    obj.id_documento = _selectedDocumento.id_documento;
                    obj.nombre =nombre;
                    obj.id_tipo_documento = _id_tipo;
                    obj.id_dep = _id_dep;
                    obj.descripcion = descripcion;
                    obj.fecha_creacion = DateTime.Now;
                    obj.fecha_actualizacion = fecha;
                    obj.fecha_emision = fecha;
                    obj.id_estatus =2;
                    
                    //Ejecutamos el método para guardar el documento. El resultado lo guardamos en una variable local.
                   // id_documento = DataManagerControlDocumentos.SetDocumento(obj);
                    id_documento = DataManagerControlDocumentos.UpdateDocumento(obj);

                    //si se guardo el registro en la tabla documento
                    if (id_documento!=0)
                    {         
                        //Mapeamos los valores al objeto de versión.
                        objVersion.no_version = version;
                        objVersion.no_copias = Convert.ToInt32(copias);
                       // objVersion.id_documento = id_documento;
                        objVersion.id_documento = _selectedDocumento.id_documento;
                        objVersion.id_usuario = _usuario;
                        objVersion.id_usuario_autorizo = _usuarioAutorizo;
                        objVersion.fecha_version = fecha;

                        //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                        int id_version = DataManagerControlDocumentos.SetVersion(objVersion);

                        //si se guardó correctamente el registro en la tabla versión.
                        if (id_version!=0)
                        {
                            //obj.id_documento = id_documento;      
                            obj.version_actual = Convert.ToString(id_version);

                            //Se ejecuta el método para modificar sólo la versión actual del documento, con el id de la versión que se guardó anteriormente.
                            int update = DataManagerControlDocumentos.UpdateVersionActual(obj);
                            
                            //Iteramos la lista de documentos.
                            foreach (var item in _ListaDocumentos)
                            {
                                //Declaramos un objeto de tipo Archivo.
                                Archivo objArchivo = new Archivo();

                                //Mapeamos los valores al objeto creado.
                                objArchivo.id_version = id_version;
                                objArchivo.archivo = item.archivo;
                                objArchivo.ext = item.ext;
                                objArchivo.nombre = item.nombre;
                                //Ejecutamos el método para guardar el documento iterado, el resultado lo guardamos en una variable local.
                                int n = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                                
                            }

                            //Ejecutamos el método para cerrar el mensaje de espera.
                            await controllerProgressAsync.CloseAsync();

                            //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
                            await dialog.SendMessage("Información", "Los cambios fueron guardados exitosamente..");

                            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                            //Verificamos que la pantalla sea diferente de nulo.
                            if (window!=null)
                            {
                                //Cerramos la pantalla
                                window.Close();
                            }
                        }
                        else
                        {
                            await dialog.SendMessage("Alerta", "Error al registrar la versión..");
                        }
                    }
                    else
                    {
                        //Si no se hizo la alta.
                        //Ejecutamos el método para enviar un mensaje de alerta al usuario.
                        await dialog.SendMessage("Alerta", "Error al registrar el documento..");
                    }
                    
                }
                else
                {
                    //Si no cumple con la validación.
                    //Ejecutamos el método para enviar un mensaje de alerta al usuario.
                    await dialog.SendMessage("Alerta", "Se debe llenar todos los campos.");
                }
            }
            else
            {
                //Si es una actualización.
                //Ejecutamos el método para valirdar los valores.

                if (ValidarValores())
                {

                    //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
                    controllerProgressAsync = await dialog.SendProgressAsync("Por favor espere", "Guardando el documento...");

                    //Declaramos un objeto de tipo Version.
                    Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

                    //Mapeamos los valores al objeto de versión.
                    objVersion.no_version = version;
                    objVersion.no_copias = Convert.ToInt32(copias);
                    objVersion.id_documento = id_documento;
                    objVersion.id_usuario = _usuario;
                    objVersion.id_usuario_autorizo = _usuarioAutorizo;
                    objVersion.fecha_version = fecha;

                    //valida que la version en el documento no se repita
                    int validacion = DataManagerControlDocumentos.ValidateVersion(objVersion);

                    if (validacion == 0)
                    {
                        //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                        int id_version = DataManagerControlDocumentos.SetVersion(objVersion);

                        //si se realizo la alta
                        if (id_version != 0)
                        {
                            //Creamos un objeto de tipo Documento
                            Documento obj = new Documento();

                            //Mapeamos los valores al objeto.
                            obj.id_documento = id_documento;
                            obj.version_actual = Convert.ToString(id_version);
                            //Se ejecuta el método para modificar sólo la versión actual del documento, con el id de la versión que se guardó anteriormente.
                            int update = DataManagerControlDocumentos.UpdateVersionActual(obj);

                            //Iteramos la lista de documentos.
                            foreach (var item in _ListaDocumentos)
                            {
                                //Mapeamos los valores al objeto creado.
                                Archivo objArchivo = new Archivo();

                                //Mapeamos los valores al objeto creado.
                                objArchivo.id_version = id_version;
                                objArchivo.archivo = item.archivo;
                                objArchivo.ext = item.ext;
                                objArchivo.nombre = item.nombre;

                                //Ejecutamos el método para guardar el documento iterado, el resultado lo guardamos en una variable local.
                                int id_archivo = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                            }

                            //Asignamos el valore de Guardar a la etiqueta del botón.
                            BotonGuardar = "Guardar";

                            //Ejecutamos el método para cerrar el mensaje de espera.
                            await controllerProgressAsync.CloseAsync();

                            //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
                            await dialog.SendMessage("Información", "Los cambios fueron guardados exitosamente..");

                            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                            //Verificamos que la pantalla sea diferente de nulo.
                            if (window != null)
                            {
                                //Cerramos la pantalla
                                window.Close();
                            }
                        }
                        else
                        {
                            //si hubo algún error en la alta, manda mensaje se error.
                            await dialog.SendMessage("Alerta", "Error al registrar la versión..");
                        }
                    }
                    else
                    {
                        await dialog.SendMessage("Información", "La versión ya existe para este documento");
                        await controllerProgressAsync.CloseAsync();
                    }
                }
                else
                {
                    //Si no cumple con la validación.
                    //Ejecutamos el método para enviar un mensaje de alerta al usuario.
                    await dialog.SendMessage("Alerta", "Se debe llenar todos los campos.");
                }

            }
        }

        /// <summary>
        /// Comando para agregar un documento a la lista, desde el explorador de archivos
        /// </summary>
        public ICommand LlenarLista
        {
            get
            {
                return new RelayCommand(o => llenarLista());
            }
        }
        private void llenarLista()
        {
            //Abre la ventana de explorador de archivos
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //Filtar los dpocumentos por extensión 
            dlg.Filter = "Word (97-2003)|*.doc|PDF Files (.pdf)|*.pdf";

            // Mostrar el explorador de archivos
            Nullable<bool> result = dlg.ShowDialog();

            // Si fue seleccionado un documento 
            if (result == true)
            {
                //Se obtiene el nombre del documento
                string filename = dlg.FileName;

                //Se crea el objeto de tipo archivo
                Archivo obj = new Archivo();

                //Se convierte el archvio a tipo byte y se le asigna al objeto
                obj.archivo = File.ReadAllBytes(filename);

                //Obtiene la extensión del documento y se le asigna al objeto
                obj.ext = System.IO.Path.GetExtension(filename);

                //Se obtiene sólo el nombre, sin extensión.
                obj.nombre = System.IO.Path.GetFileNameWithoutExtension(filename);

                obj.numero = ListaDocumentos.Count+1;
                if (BotonGuardar=="Guardar") {
                    //El nombre del archivo lo asigna al textbox del nombre
                   // Nombre = obj.nombre;
                }

                //Si el archivo tiene extensión pdf
                if (obj.ext == ".pdf")
                {
                    //asigna la imagen del pdf al objeto
                    obj.ruta = @"/Images/p.png";
                }
                else
                {
                    //Si es archivo de word asigna la imagen correspondiente.
                    obj.ruta = @"/Images/w.png";
                }
                //Se agrega el objeto a la lista
                ListaDocumentos.Add(obj);
            }
        }

        /// <summary>
        /// Comando para cancelar
        /// </summary>
        public ICommand Cancelar
        {
            get
            {
                return new RelayCommand(o => cancelar());
            }
        }
        private void cancelar()
        {
            Copias = auxcopias;
            usuario = auxUsuario;
            usuarioAutorizo = auxUsuario_Autorizo;
            Version = auxversion;
            BotonGuardar = "Guardar";
            BttnEliminar = true;
            BttnModificar = true;
            BttnVersion = true;
            BttnGuardar = false;
            NombreEnabled = false;
            BttnCancelar = false;

            ListaDocumentos.Clear();

            ObservableCollection<Documento> Lista = DataManagerControlDocumentos.GetTipo(id_documento, idVersion);

            foreach (var item in Lista)
            {
                Archivo objArchivo = new Archivo();

                objArchivo.nombre = item.nombre;
                objArchivo.id_archivo = item.version.archivo.id_archivo;
                objArchivo.archivo = item.version.archivo.archivo;
                objArchivo.ext = item.version.archivo.ext;

                if (objArchivo.ext == ".pdf")
                {
                    //asigna la imagen del pdf al objeto
                    objArchivo.ruta = @"/Images/p.png";
                }
                else
                {
                    //Si es archivo de word asigna la imagen correspondiente.
                    objArchivo.ruta = @"/Images/w.png";
                }
                ListaDocumentos.Add(objArchivo);
            }
        }

        /// <summary>
        /// Comando para eliminar un registro
        /// </summary>
        public ICommand Eliminar
        {
            get
            {
                return new RelayCommand(o => eliminar());
            }
        }
        private async void eliminar()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Deseas eliminar el registro?", setting, MessageDialogStyle.AffirmativeAndNegative);

            //Si el id es diferente de cero
            if (id_documento != 0 & result==MessageDialogResult.Affirmative)
            {
                //Elimina los documentos de la lista 
                foreach (var item in _ListaDocumentos)
                {

                  int n = DataManagerControlDocumentos.DeleteArchivo(item);
                }

                Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
                //Se asigna el id 
                objVersion.id_version = idVersion;
                //Mandamos a llamar a la  función para eliminar la versión.
                int version = DataManagerControlDocumentos.DeleteVersion(objVersion);

                //Obetenemos las versiones del documento
                ListaVersiones = DataManagerControlDocumentos.GetVersiones(id_documento);

                //iteramos la lista de las versiones
                foreach (var item in ListaVersiones)
                {
                    //De cada versión obetemos los correspondientes archivos.
                    ListaArchivo = DataManagerControlDocumentos.GetArchivos(item.id_version);

                    //Iteramos la lista de archivos
                    foreach (var archivo in ListaArchivo)
                    {
                        //Eliminamos el archivo
                        int a = DataManagerControlDocumentos.DeleteArchivo(archivo);
                    }
                    //Mandamos a llamar la funcion para eliminar la version.
                   int v = DataManagerControlDocumentos.DeleteVersion(item);
                }

                if (version != 0)
                {
                    Documento obj = new Documento();

                    //se le asigna el id al objeto
                    obj.id_documento = id_documento;

                    //Se manda a llamar a la función.
                    int n = DataManagerControlDocumentos.DeleteDocumento(obj);
                   
                }
                else
                {
                    await dialog.SendMessage("Alert", "No se puedo eliminar la versión");
                }

                     await dialog.SendMessage("", "Registro eliminado!");
                    //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                    var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                    //Verificamos que la pantalla sea diferente de nulo.
                    if (window != null)
                    {
                        //Cerramos la pantalla
                        window.Close();
                    }
            }
        }

        /// <summary>
        /// Comando para modificar un registro
        /// </summary>
        public ICommand Modificar
        {
            get
            {
                return new RelayCommand(o => modificar());
            }
        }
        private async void modificar()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Deseas guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                //Ejecutamos el método para valirdar los valores.
                if (ValidarValores())
                {
                    //Se crea un objeto de tipo Documento.
                    Documento obj = new Documento();

                    //Se asignan los valores.
                    obj.id_documento = id_documento;
                    obj.id_dep = _id_dep;
                    obj.id_tipo_documento = _id_tipo;
                    obj.descripcion = Descripcion;
                    obj.version_actual = Convert.ToString(idVersion);
                    obj.fecha_actualizacion = fecha;

                    //Ejecuta el método para modificar un registro 
                    int n = DataManagerControlDocumentos.UpdateDocumento(obj);

                    //Ejecutamos el método para modificar un documento             
                    if (n != 0)
                    {
                        Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
                        objVersion.id_version = idVersion;
                        objVersion.no_version = version;
                        objVersion.no_copias = Convert.ToInt32(copias);
                        objVersion.id_documento = id_documento;
                        objVersion.id_usuario = _usuario;
                        objVersion.id_usuario_autorizo = _usuarioAutorizo;
                        objVersion.fecha_version = fecha;

                        //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                        int id_version = DataManagerControlDocumentos.UpdateVersion(objVersion);

                        if (id_version != 0)
                        {

                            foreach (var item in _ListaDocumentos)
                            {
                                //Declaramos un objeto de tipo Archivo.
                                Archivo objArchivo = new Archivo();

                                //Mapeamos los valores al objeto creado.
                                objArchivo.id_version = idVersion;
                                objArchivo.archivo = item.archivo;
                                objArchivo.ext = item.ext;
                                objArchivo.nombre = item.nombre;

                                //si el archivo no existe 
                                if (item.id_archivo == 0)
                                {
                                    //Ejecutamos el método para guardar el documento iterado, el resultado lo guardamos en una variable local.
                                    int a = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                                }
                            }
                            await dialog.SendMessage("Información", "Cambios realizados..");
                            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                            //Verificamos que la pantalla sea diferente de nulo.
                            if (window != null)
                            {
                                //Cerramos la pantalla
                                window.Close();
                            }

                        }
                        else
                        {
                            await dialog.SendMessage("Alerta", "No se pudieron realizar los cambios en la versión..");
                        }
                    }
                    else
                    {
                        await dialog.SendMessage("Alerta", "No se pudieron realizar los cambios en documento..");
                    }

                }
                else
                {
                    //Si los campos están vacíos, manda un mensaje.
                    await dialog.SendMessage("RGP: Alerta", "Se debe llenar todos los campos");
                }
            }
        }
        
        /// <summary>
        /// Comando para generar una nueva versión a un documento
        /// </summary>
        public ICommand GenerarVersion
        {
            get
            {
                return new RelayCommand(o => generarVersion());
            }
        }
        private async void generarVersion()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Obtiene la últuma version del documento.
            Version = DataManagerControlDocumentos.GetLastVersion(id_documento);

            //Manda un mensaje al usuario, donde muestra la versión nueva.
            await dialog.SendMessage("Alerta", "La nueva versión del documento es la número "+Version);

            //Limpiamos todos lo textbox, y se cambia el content del botón de guardar.
            Fecha = DateTime.Now;
            Copias=string.Empty;
            usuario = null;
            usuarioAutorizo = null;
            ListaDocumentos.Clear();
            BotonGuardar = "Guardar Version";
            BttnGuardar = true;
            BttnEliminar = false;
            BttnModificar = false;
            BttnVersion = false;
            NombreEnabled = false;
            BttnCancelar = true;

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
        private  void verArchivo(Archivo item)
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
            do
            {
                string aleatorio = Module.GetRandomString(5);

                filename = Path.Combine(tempFolder, item.nombre + item.numero + "_" + aleatorio + item.ext);
            } while (File.Exists(filename));

            return filename;
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
                MessageDialogResult result = await dialogService.SendMessage("Attention", "¿Deseas eliminar el archivo?", setting, MessageDialogStyle.AffirmativeAndNegative);

                if (item != null & result == MessageDialogResult.Affirmative)
                {
                    //Se elimina el item seleccionado de la listaDocumentos.
                    ListaDocumentos.Remove(item);
                    //Se elimina de la base de datos.
                    int n = DataManagerControlDocumentos.DeleteArchivo(item);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand AgregarUsuario
        {
            get
            {
                return new RelayCommand(o => agregarUsuario());
            }
        }
        private void agregarUsuario()
        {
            FrmNuevoUsuario frm = new FrmNuevoUsuario();
            NuevoUsuarioVM context = new NuevoUsuarioVM();

            frm.DataContext = context;
            frm.ShowDialog();
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand AgregarDepartamento
        {
            get
            {
                return new RelayCommand(o => agregarDepartamento());
            }
        }
        private  void agregarDepartamento()
        {
            FrmNuevo_Departamento frm = new FrmNuevo_Departamento();
            NuevoDepartamentoVM context = new NuevoDepartamentoVM();
            frm.DataContext =context;
            frm.ShowDialog();
            ListaDepartamento = DataManagerControlDocumentos.GetDepartamento();
        }

        /// <summary>
        /// Mátodo para agregar tipo de Documento
        /// </summary>
        public ICommand AgregarTipo
        {
            get
            {
                return new RelayCommand(o => agregarTipo());
            }
        }
        private void agregarTipo()
        {
            FrmNuevoTipo frmTipo = new FrmNuevoTipo();
            NuevoTipoDocumentoVM context = new NuevoTipoDocumentoVM();

            frmTipo.DataContext = context;

            frmTipo.ShowDialog();
            ListaTipo = DataManagerControlDocumentos.GetTipo();
        }

        public ICommand CambiarCombo
        {
            get
            {
                return new RelayCommand(o => cambiarCombo());
            }
        }
        private void cambiarCombo()
        {
            if (_selectedDocumento !=null)
            {
                id_dep = _selectedDocumento.id_dep;
                id_tipo = _selectedDocumento.id_tipo_documento;
                nombre = _selectedDocumento.nombre;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Método que retorna un true si todos los campos contienen un valor.
        /// </summary>
        /// <returns></returns>
        private bool ValidarValores()
        {
            if (nombre != null & version != null & fecha != null & copias != null & descripcion != null & id_tipo != 0 & _ListaDocumentos.Count != 0 & _usuario!=null & _id_dep!=0 & _usuarioAutorizo!=null)
                return true;
            else 
                return false;
        }

        private void Inicializar()
        {
            ListaDepartamento= DataManagerControlDocumentos.GetDepartamento();
            ListaTipo = DataManagerControlDocumentos.GetTipo();
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();
        }
        #endregion
    }
}
