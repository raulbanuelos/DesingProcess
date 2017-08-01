using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        private string auxversion, auxUsuario,auxUsuario_Autorizo;

        private DateTime auxFecha;

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
        private ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

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
        private ObservableCollection<ValidacionDocumento> _ListaValidaciones;
        public ObservableCollection<ValidacionDocumento> ListaValidaciones
        {
            get
            {
                return _ListaValidaciones;
            }
            set
            {
                _ListaValidaciones = value;
                NotifyChange("ListaValidaciones");
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

        private bool _bttnArchivos;
        public bool BttnArchivos {
            get
            {
                return _bttnArchivos;
            }
            set
            {
                _bttnArchivos = value;
                NotifyChange("BttnArchivos");
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

        private bool nombreEnabled=false;
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

        private bool _enabledEliminar;
        public bool EnabledEliminar
        {
            get
            {
                return _enabledEliminar;
            }
            set
            {
                _enabledEliminar = value;
                NotifyChange("EnabledEliminar");
            }
        }

        private bool _bttnLiberar = false;
        public bool BttnLiberar
        {
            get
            {
                return _bttnLiberar; ;
            }
            set
            {
                _bttnLiberar = value;
                NotifyChange("BttnLiberar");
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

        private bool _isEnabled=true;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                NotifyChange("IsEnabled");
            }
        }

        #endregion

        #region Constructor
        public DocumentoViewModel(Documento selectedDocumento, bool band,Usuario Modelusuario)
        {
            //Contructor para generar una versión, o modificar el documetnto
            //Inicializa los combobox 
            Inicializar();
            //Asiganmos los valores para que se muestren 
            User = Modelusuario;
            Nombre = selectedDocumento.nombre;
            Version = selectedDocumento.version.no_version;
            Fecha = selectedDocumento.fecha_actualizacion;
            auxFecha = Fecha;      
            auxversion = selectedDocumento.version.no_version;
            Descripcion = selectedDocumento.descripcion;
            id_documento = selectedDocumento.id_documento;
            idVersion = selectedDocumento.version.id_version;
            id_dep = selectedDocumento.id_dep;
            id_tipo = DataManagerControlDocumentos.GetTipoDocumento(id_documento);

            BotonGuardar = "Guardar";
            BttnGuardar = false;
            EnabledEliminar = false;
            IsEnabled = false;

            //si es personal del cit
            if (Module.UsuarioIsRol(User.Roles, 2))
            {
                BttnModificar = true;
                BttnEliminar = band;
                IsEnabled = true;
                BttnArchivos = true;
                EnabledEliminar = true;
                //Mostramos la lista de validaciones dependiento el tipo
                ListaValidaciones = DataManagerControlDocumentos.GetValidacion_Documento(id_tipo);

            } //si es ventana para corregir archivos.
            if (band==false)
            {
                IsEnabled = true;
                BttnArchivos = true;
                BttnModificar = true;
                EnabledEliminar = true;
                //Mostramos la lista de validaciones dependiento el tipo
                ListaValidaciones = DataManagerControlDocumentos.GetValidacion_Documento(id_tipo);
                Fecha = selectedDocumento.version.fecha_version;
            }

            BttnVersion = band;
      
            _ListaNumeroDocumento = DataManagerControlDocumentos.GetNombre_Documento(id_documento);

            if(_ListaNumeroDocumento.Count >0)
            SelectedDocumento = _ListaNumeroDocumento[0];

            //Obtenemos el usuario que autorizó y el usuario que dio de alta
            Model.ControlDocumentos.Version UsuarioObj = DataManagerControlDocumentos.GetIdUsuario(idVersion);
            usuario = UsuarioObj.id_usuario;
            auxUsuario = usuario;
            usuarioAutorizo = UsuarioObj.id_usuario_autorizo;
            auxUsuario_Autorizo = usuarioAutorizo;  
            
            //Método que obtiene los archivos de un documento y de la versión
            ObservableCollection<Documento> Lista = DataManagerControlDocumentos.GetArchivos(id_documento,idVersion);
            
            //Iteramos la lista
            foreach (var item in Lista)
            {
                Archivo objArchivo = new Archivo();

                //Asiganmos los valores para que se muestren 
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

        public DocumentoViewModel(Usuario Modelusuario)
        {
            //Contrusctor para crear un nuevo documento
            BotonGuardar = "Guardar";
            BttnGuardar = true;
            BttnArchivos = true;
            EnabledEliminar = true;
            Version = "1";
            User = Modelusuario;
            usuario = User.NombreUsuario;
            auxUsuario = usuario;
            NombreEnabled = true;
            ListaNumeroDocumento = DataManagerControlDocumentos.GetDocumento_SinVersion(User.NombreUsuario);
            Inicializar();
        }

        public DocumentoViewModel(Documento selectedDocumento)
        {
            //Contructor para liberar documentos
            //Inicializa los combobox 
            Inicializar();
            IsEnabled = false;
            EnabledEliminar = false;
            BttnArchivos = false;
            BttnLiberar = true;

            //Asiganmos los valores para que se muestren 
            Nombre = selectedDocumento.nombre;
            User = new Usuario();
            Version = selectedDocumento.version.no_version;
            Fecha = selectedDocumento.version.fecha_version;
            Descripcion = selectedDocumento.descripcion;
            id_documento = selectedDocumento.id_documento;
            idVersion = selectedDocumento.version.id_version;
            id_dep = selectedDocumento.id_dep;
            //Obtenemos el tipo de documento
            id_tipo = DataManagerControlDocumentos.GetTipoDocumento(id_documento);

            //obtenemos el nombre del documento
            _ListaNumeroDocumento = DataManagerControlDocumentos.GetNombre_Documento(id_documento);

            //Se muestra en el combobox
            if (_ListaNumeroDocumento.Count > 0)
                SelectedDocumento = _ListaNumeroDocumento[0];

            //Obtenemos el usuario que autorizó y el usuario que elabroró
            Model.ControlDocumentos.Version UsuarioObj = DataManagerControlDocumentos.GetIdUsuario(idVersion);
            usuario = UsuarioObj.id_usuario;
            usuarioAutorizo = UsuarioObj.id_usuario_autorizo;

            //Método que obtiene los archivos de un documento y de la versión
            Lista = DataManagerControlDocumentos.GetArchivos(id_documento, idVersion);

            //Iteramos la lista 
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
        /// <summary>
        /// Método que guarda un nuevo documento, con su respectiva versión y los archivos
        /// O guarda una nueva versión con sus archivos
        /// </summary>
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
                    if (ValidaSelected())
                    {
                        if (ValidaTipo())
                        {
                            //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
                            controllerProgressAsync = await dialog.SendProgressAsync("Por favor espere", "Guardando el documento...");

                            //Declaramos un objeto de tipo documento.
                            Documento obj = new Documento();

                            //Declaramos un objeto de tipo Version.
                            Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

                            //Mapeamos los valores al objeto declarado.
                            obj.id_documento = _selectedDocumento.id_documento;
                            obj.nombre = nombre;
                            obj.id_tipo_documento = _id_tipo;
                            obj.id_dep = _id_dep;
                            obj.descripcion = descripcion;
                            obj.fecha_actualizacion = DateTime.Now;
                            obj.fecha_emision = fecha;
                            obj.id_estatus = 2;

                            //Ejecutamos el método para guardar el documento. El resultado lo guardamos en una variable local.
                            int update = DataManagerControlDocumentos.UpdateDocumento(obj);

                            //si se guardo el registro en la tabla documento
                            if (update != 0)
                            {
                                //Mapeamos los valores al objeto de versión.
                                objVersion.no_version = version;
                                objVersion.id_documento = _selectedDocumento.id_documento;
                                objVersion.id_usuario = _usuario;
                                objVersion.id_usuario_autorizo = _usuarioAutorizo;
                                objVersion.fecha_version = fecha;
                                objVersion.id_estatus_version = 3;
                                objVersion.no_copias = 0;

                                //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                                int id_version = DataManagerControlDocumentos.SetVersion(objVersion);

                                //si se guardó correctamente el registro en la tabla versión.
                                if (id_version != 0)
                                {
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
                                        int nombre = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                                    }
                                    //Ejecutamos el método para cerrar el mensaje de espera.
                                    await controllerProgressAsync.CloseAsync();

                                    //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
                                    await dialog.SendMessage("Información", "Los cambios fueron guardados exitosamente, los archivos serán verificados por el personal del CIT..");

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
                            await dialog.SendMessage("Alerta", "Para el tipo de documento sólo se puede subir un archivo");
                        }
                    } //fin if valida select
                    else
                    {
                        //Si no cumple con la validación.
                        //Ejecutamos el método para enviar un mensaje de alerta al usuario.
                        await dialog.SendMessage("Alerta", "Verifica que el archivo  cumpla con todos los requisitos..");
                    }
                }
                else
                {
                    await dialog.SendMessage("Alerta", "Se debe llenar todos los campos.");
                }
            }
            else
            {
                //Si se genera una versión nueva
                //Ejecutamos el método para valirdar los valores.

                if (ValidarValores())
                {
                    if (ValidaSelected())
                    {
                        if (ValidaTipo())
                        {

                            //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
                            controllerProgressAsync = await dialog.SendProgressAsync("Por favor espere", "Guardando el documento...");

                            //Declaramos un objeto de tipo Version.
                            Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

                            //Mapeamos los valores al objeto de versión.
                            objVersion.no_version = version;
                            objVersion.id_documento = id_documento;
                            objVersion.id_usuario = _usuario;
                            objVersion.id_usuario_autorizo = _usuarioAutorizo;
                            objVersion.fecha_version = fecha;
                            objVersion.id_estatus_version = 3;
                            objVersion.no_copias = 0;

                            //valida que la version en el documento no se repita
                            int validacion = DataManagerControlDocumentos.ValidateVersion(objVersion);

                            if (validacion == 0)
                            {
                                //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                                int id_version = DataManagerControlDocumentos.SetVersion(objVersion);

                                //si se realizo guardo la versión 
                                if (id_version != 0)
                                {
                                    //Iteramos la lista de documentos.
                                    foreach (var item in _ListaDocumentos)
                                    {

                                        Archivo objArchivo = new Archivo();

                                        //Mapeamos los valores al objeto creado.
                                        objArchivo.id_version = id_version;
                                        objArchivo.archivo = item.archivo;
                                        objArchivo.ext = item.ext;
                                        objArchivo.nombre = item.nombre;

                                        //Ejecutamos el método para guardar el documento iterado, el resultado lo guardamos en una variable local.
                                        int id_archivo = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                                    }

                                    //Asignamos el valor de Guardar a la etiqueta del botón.
                                    BotonGuardar = "Guardar";

                                    //Ejecutamos el método para cerrar el mensaje de espera.
                                    await controllerProgressAsync.CloseAsync();

                                    //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
                                    await dialog.SendMessage("Información", "Los cambios fueron guardados exitosamente, los archivos serán verificados por el personal del CIT..");

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
                                await dialog.SendMessage("Información", "La versión ya existe para este documento..");
                                await controllerProgressAsync.CloseAsync();
                            }
                        }
                        else
                        {
                            await dialog.SendMessage("Alerta", "Para el tipo de documento sólo se puede subir un archivo");
                        }
                    } //fin ValidaSelect
                    else
                    {
                        //Si no cumple con la validación.
                        //Ejecutamos el método para enviar un mensaje de alerta al usuario.
                        await dialog.SendMessage("Alerta", "Verifica que el archivo cumpla con todos los requisitos..");
                    }
                }
                else
                {                  
                    await dialog.SendMessage("Alerta", "Se debe llenar todos los campos..");
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
        /// <summary>
        /// Método que llena la lista para visualizar los archivos de la versión
        /// </summary>
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
        /// <summary>
        /// Método que cancela cuando se genera una nueva versión, muestra la información de la versión anterior
        /// </summary>
        private void cancelar()
        {
            usuario = auxUsuario;
            usuarioAutorizo = auxUsuario_Autorizo;
            Version = auxversion;
            Fecha = auxFecha;
            BotonGuardar = "Guardar";
            IsEnabled = false;
            BttnArchivos = false;
            EnabledEliminar = false;
            ListaValidaciones.Clear();
            ListaDocumentos.Clear();

            //Si es administrador del CIT
            if (Module.UsuarioIsRol(User.Roles, 2))
            {
                BttnEliminar = true;
                BttnModificar = true;
                IsEnabled = true;
                BttnArchivos = true;
                EnabledEliminar = true;
                ListaValidaciones = DataManagerControlDocumentos.GetValidacion_Documento(id_tipo);
            }
            BttnVersion = true;
            BttnGuardar = false;
            NombreEnabled = false;
            BttnCancelar = false;

            //Obtenemos los archvios de la versión anterior
            ObservableCollection<Documento> Lista = DataManagerControlDocumentos.GetArchivos(id_documento, idVersion);
            //Iteración de la lista de archivos
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
                //Añadimos los archivos a la lista
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
        /// <summary>
        /// Método para eliminar el registro de un documento
        /// Elimina los registros de todos las versiones de un documento
        /// </summary>
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
                //Si se elimino correctamente la versión
                if (version != 0)
                {
                    Documento obj = new Documento();

                    //se le asigna el id al objeto
                    obj.id_documento = id_documento;

                    //Se manda a llamar a la función.
                    int n = DataManagerControlDocumentos.DeleteDocumento(obj);

                    await dialog.SendMessage("", "Registro eliminado!");
                }
                else
                {
                    await dialog.SendMessage("Alert", "No se puedo eliminar la versión");
                }
    
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
        /// <summary>
        /// Método que modifica el documento si tiene sólo una versión
        /// Si el documento tiene más de una versión sólo modifica la versión
        /// </summary>
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
                    //Validamos que los campos ésten seleccionados
                     if (ValidaSelected())
                     {
                        if (ValidaTipo())
                        {
                            int last_id = DataManagerControlDocumentos.GetID_LastVersion(id_documento, idVersion);
                            //Si es la primer versión del documento
                            if (last_id == 0)
                            {
                                //Se crea un objeto de tipo Documento.
                                Documento obj = new Documento();

                                //Se asignan los valores.
                                obj.id_documento = id_documento;
                                obj.id_dep = _id_dep;
                                obj.id_tipo_documento = _id_tipo;
                                obj.descripcion = Descripcion;
                                obj.fecha_emision = fecha;
                                obj.fecha_actualizacion = DateTime.Now;
                                obj.id_estatus = 5;
                                // obj.id_estatus = 2;

                                //Ejecuta el método para modificar el documento actual
                                int n = DataManagerControlDocumentos.UpdateDocumento(obj);

                                //Si se realizo la modificacion
                                if (n != 0)
                                {
                                    //Se ejecuta el metodo que modifica la version actual
                                    int update_version = modificaVersion();

                                    //si se modifico correctamente
                                    if (update_version != 0)
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
                                        await dialog.SendMessage("Información", "Cambios realizados, los archivos serán verificados por el personal del CIT..");
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
                                //modificacion de la version, cuando el documento tiene más de una versión 
                                int update_version = modificaVersion();
                                if (update_version != 0)
                                {
                                    //Iteramos la lista de los archivos de la versión
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
                                    await dialog.SendMessage("Información", "Cambios realizados, los archivos serán verificados por el personal del CIT..");
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
                        }
                        else
                        {
                            await dialog.SendMessage("Alerta", "Para el tipo de documento sólo se puede subir un archivo");
                        }
                            
                    } //fin ValidaSelected
                    else
                    {
                        //Si los campos están vacíos, manda un mensaje.
                        await dialog.SendMessage("RGP: Alerta", "Verifica que el archivo cumpla con todos los requisitos");
                    }
                    }
                    else
                    {
                        
                        await dialog.SendMessage("RGP: Alerta", "Se debe llenar todos los campos");
                }
                }
            }

        /// <summary>
        /// Método que modifica la versión
        /// </summary>
        /// <returns></returns>
        private int modificaVersion()
        {
            Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
            objVersion.id_version = idVersion;
            objVersion.no_version = version;
            objVersion.id_documento = id_documento;
            objVersion.id_usuario = _usuario;
            objVersion.id_usuario_autorizo = _usuarioAutorizo;
            objVersion.fecha_version = fecha;
            objVersion.id_estatus_version = 3;
            objVersion.no_copias = 0;

            //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
            return DataManagerControlDocumentos.UpdateVersion(objVersion);
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
        /// <summary>
        /// Método que genera un versión
        /// Limpia los datos de la versión anterior
        /// </summary>
        private async void generarVersion()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            Bloqueo objBloqueo = new Bloqueo();

            //Método que obtiene un registro si se encuentra activo
            objBloqueo = DataManagerControlDocumentos.GetBloqueo();

            //Si no encuentra ningún registro con estado bloqueado
            //O es administrador del Cit, sólo los administradores del Cit pueden generar una versión cuando el sistema se encentre bloqueado
            if (objBloqueo.id_bloqueo == 0 ||  Module.UsuarioIsRol(User.Roles, 2))
            {
                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = "SI";
                setting.NegativeButtonText = "NO";

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialog.SendMessage("Attention", "¿Deseas generar una nueva versión?", setting, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    //Ejecutamos el método que obtiene las versiones de un documentos que no están liberadas u obsoletas
                    ObservableCollection<Model.ControlDocumentos.Version> ListaEstatus = DataManagerControlDocumentos.GetStatus_Version(id_documento);

                    //Si el documento no tiene versiones pendientes 
                    if (ListaEstatus.Count == 0)
                    {
                        //Obtiene la últuma version del documento.
                        Version = DataManagerControlDocumentos.GetLastVersion(id_documento);

                        //Manda un mensaje al usuario, donde muestra la versión nueva.
                        await dialog.SendMessage("Información", "La nueva versión del documento es la número " + Version);

                        //Limpiamos todos lo textbox, y se cambia el content del botón de guardar.
                        Fecha = DateTime.Now;
                        //usuarioAutorizo = null;
                        ListaDocumentos.Clear();
                        ListaValidaciones = DataManagerControlDocumentos.GetValidacion_Documento(id_tipo);

                        //Oculta y muestra los botones
                        BotonGuardar = "Guardar Version";
                        BttnGuardar = true;
                        BttnEliminar = false;
                        BttnModificar = false;
                        BttnVersion = false;
                        NombreEnabled = false;
                        BttnArchivos = true;
                        BttnCancelar = true;
                        EnabledEliminar = true;
                        IsEnabled = true;
                        usuario = User.NombreUsuario;
                    }
                    else
                    {
                        //Si el documento tiene una versión pendiente por liberar
                        Model.ControlDocumentos.Version obj = new Model.ControlDocumentos.Version();
                        foreach (var item in ListaEstatus)
                        {
                            obj.no_version = item.no_version;
                            obj.estatus = item.estatus;
                        }
                        //Muestra mensaje 
                        await dialog.SendMessage("No se puede crear una nueva versión", " Versión número " + obj.no_version + " tiene estado: " + obj.estatus);
                    }
                }
            }
            else
            {
                //El sistema se encuentra bloqueado

                await dialog.SendMessage("Sistema Bloqueado", objBloqueo.observaciones);

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
        /// <summary>
        /// Método para ver el archivo que está en la lista de documetnos
        /// </summary>
        /// <param name="item"></param>
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
            //Realiza la acción hasta que el archivo se haya abierto
            do
            {
                //Genera un número aleatorio
                string aleatorio = Module.GetRandomString(5);
                //Crea la ruta temporal con el nombre del archivo y el número generado, y la extensión
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
        /// Comando para mostrar el departamento y tipo de acuerdo al nombre de documento
        /// </summary>
        public ICommand CambiarCombo
        {
            get
            {
                return new RelayCommand(o => cambiarCombo());
            }
        }
        /// <summary>
        /// Método que muestra el tipo de documento y el departamento de acuerdo al número de documento
        /// Llena la lista de validaciones de acuerdo al tipo de documento
        /// </summary>
        private void cambiarCombo()
        {
            if (_selectedDocumento !=null)
            {
                id_dep = _selectedDocumento.id_dep;
                id_tipo = _selectedDocumento.id_tipo_documento;
                nombre = _selectedDocumento.nombre;
                ListaValidaciones = DataManagerControlDocumentos.GetValidacion_Documento(id_tipo);
            }
        }
        /// <summary>
        /// Comando para liberar un documento
        /// </summary>
        public ICommand LiberarDocumento
        {
            get
            {
               return  new RelayCommand(o => liberarDocumento());
            }
        }
        /// <summary>
        /// Método para liberar un documento, modifica el número de copias del documento y el estatus de la versión
        /// </summary>
        private async void liberarDocumento()
        {
            DialogService dialog = new DialogService();

            //Obtenemos la ventana actual
            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            //Formulario para ingresar el número de copias, 
            string num_copias = await window.ShowInputAsync("Ingresar número de copias", "Número de Copias", null);

            //bool result= Regex.IsMatch(num_copias, @"^\d+$");
            //Comprueba que el número de copias sólo contenga números.
            if (num_copias != null)
            {
                if (Regex.IsMatch(num_copias, @"^\d+$"))
                {
                    Documento objDocumento = new Documento();
                    Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

                    //Asiganmos el id del documento al objeto
                    objDocumento.id_documento = id_documento;

                    if (!string.IsNullOrEmpty(num_copias))
                    {
                        //Ejecutamos el método para obtener el id de la versión anterior

                        int last_version = DataManagerControlDocumentos.GetID_LastVersion(id_documento, idVersion);

                        //si el documento sólo tiene una versión, se modifica el estatus del documento y la versión, se cambia el estatus a liberado
                        if (last_version == 0)
                        {
                            //Estatus de documento liberado
                            objDocumento.id_estatus = 5;

                            //Ejecutamos el método para actualizar el estatus del documento.
                            int update_documento = DataManagerControlDocumentos.Update_EstatusDocumento(objDocumento);

                            //Si se actualizó correctamente.
                            if (update_documento != 0)
                            {
                                //Asigamos los valores
                                objVersion.id_version = idVersion;
                                objVersion.no_version = version;
                                objVersion.id_documento = id_documento;
                                objVersion.id_usuario = _usuario;
                                objVersion.id_usuario_autorizo = _usuarioAutorizo;
                                objVersion.fecha_version = fecha;
                                objVersion.id_estatus_version = 1;
                                objVersion.no_copias = Convert.ToInt32(num_copias);

                                //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                                int update_version = DataManagerControlDocumentos.UpdateVersion(objVersion);

                                //Si la versión se actualizó correctamente.
                                if (update_version != 0)
                                {
                                    string file=SaveFile();

                                    if (file == null)
                                    {
                                        await dialog.SendMessage("Información", "Documento y versión liberados..");

                                        //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                                        var frame = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                                        //Verificamos que la pantalla sea diferente de nulo.
                                        if (frame != null)
                                        {
                                            //Cerramos la pantalla
                                            frame.Close();
                                        }

                                    }
                                    else
                                    {
                                        await dialog.SendMessage("Alerta", "Error al guardar el archivo");
                                    }
                                }
                                else
                                {
                                    await dialog.SendMessage("Alerta", "Error al actualizar el estatus de la versión y del documento..");
                                }
                            }
                            else
                            {
                                await dialog.SendMessage("Alerta", "Error al actualizar el estatus del documento..");
                            }
                        }
                        else
                        {
                            //si el documento tiene más de un versión, sólo se modifica el estatus de la versión a liberado
                            //la versión anterior se modifica el estatus a obsoleto
                            int fecha_actualizacion = DataManagerControlDocumentos.UpdateFecha_actualizacion(id_documento);

                            objVersion.id_version = idVersion;
                            objVersion.no_version = version;
                            objVersion.id_documento = id_documento;
                            objVersion.id_usuario = _usuario;
                            objVersion.id_usuario_autorizo = _usuarioAutorizo;
                            objVersion.fecha_version = fecha;
                            objVersion.id_estatus_version = 1;
                            objVersion.no_copias = Convert.ToInt32(num_copias);
                            
                            //Ejecutamos el método para modificar el estatus de la versión. El resultado lo guardamos en una variable local.
                            int update_version = DataManagerControlDocumentos.UpdateVersion(objVersion);

                            //si fue modificado correctamente.
                            if (update_version != 0)
                            {
                                //obetemos el id de la versión anterior
                                int last_id = DataManagerControlDocumentos.GetID_LastVersion(id_documento, idVersion);
                                //Creamos un objeto para la versión anterior 
                                Model.ControlDocumentos.Version lastVersion = new Model.ControlDocumentos.Version();

                                //asigamos el id y el estatus obsoleto
                                lastVersion.id_version = last_id;
                                lastVersion.id_estatus_version = 2;

                                //Ejecutamos el método para actualizar el estatus de la versión.
                                int update = DataManagerControlDocumentos.Update_EstatusVersion(lastVersion);

                                //si se actualizó correctamente
                                if (update != 0)
                                {
                                  string file= SaveFile();

                                    if (file == null)
                                    {
                                        await dialog.SendMessage("Información", "Versión liberada..");
                                        //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                                        var frm = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                                        //Verificamos que la pantalla sea diferente de nulo.
                                        if (frm != null)
                                        {
                                            //Cerramos la pantalla
                                            frm.Close();
                                        }
                                    }
                                    else
                                    {
                                        await dialog.SendMessage("Alerta", "Error al guardar el archivo");
                                    }
                                }
                                else
                                {
                                    //Si hubo error al actualizar el estaus de la última versión
                                    await dialog.SendMessage("Alerta", "Error al actualizar el estatus de la versión..");
                                }
                            }
                            else
                            {
                                //Si hubo error al actualizar la última versión
                                await dialog.SendMessage("Alerta", "Error al actualizar la versión..");
                            }
                        }
                    }
                }
                else
                {
                    await dialog.SendMessage("Alerta", "Campos inválidos, ingrese números..");
                }
            }
        }

        /// <summary>
        /// Método que guarda el archivo de tipo OHSAS, ESPECIFICOS, ISO14001 en sealed//documents__
        /// </summary>
        private string SaveFile()
        {
            string nombre_tipo;
            try
            {   //Si es documneto de tipo especifico o formato
                if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014)
                {
                    string path = @"\\sealed\documents__";
                    //Switch del tipo de documento
                    switch (id_tipo)
                    {
                        //Si de tipo OHSAS
                        case 1003:
                        case 1013:
                            nombre_tipo = "OHSAS";
                            path = string.Concat(path, @"\", nombre_tipo, @"\", nombre, version);
                            break;
                            //Si es de tipo específicos
                        case 1005:
                        case 1012:
                            nombre_tipo = "ESPECIFICOS";
                            path = string.Concat(path, @"\", nombre_tipo, @"\", nombre, version);
                            break;
                            //Si es de tipo ISO14001
                        case 1006:
                        case 1014:
                            nombre_tipo = "ISO14001";
                            path = string.Concat(path, @"\", nombre_tipo, @"\", nombre, version);
                            break;
                    }
                    //Iteramos la lista de archivos
                    foreach (var item in Lista)
                    {
                        //Concatenamos la ruta y la extensión
                        path = string.Concat(path, item.version.archivo.ext);
                        //Guardamos el archivo
                        File.WriteAllBytes(path, item.version.archivo.archivo);

                    }
                }
                //Si no hay erro se retorna nulo
               return null;
            } catch (Exception er)
            {
                //Si hay error se retorna el erro
                return er.ToString();
            }
        }

        /// <summary>
        /// Comando que regresa a la versión anterior
        /// elimina la versión actual
        /// </summary>
        public ICommand RegresarVersion
        {
            get
            {
                return new RelayCommand(o => regresarVersion());
            }
        }

        /// <summary>
        /// Método que regresa la versión actual a la anterior
        /// </summary>
        private async void regresarVersion()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Deseas regresar a la versión anterior?", setting, MessageDialogStyle.AffirmativeAndNegative);
             //
            if (result == MessageDialogResult.Affirmative)
            {
                //Obtiene el id de la última versión

               int last_id= DataManagerControlDocumentos.GetID_LastVersion(id_documento, idVersion);
                //Si tiene una versión anterior
                if (last_id !=0)
                {
                    //Elimina los documentos de la lista 
                    foreach (var item in _ListaDocumentos)
                    {
                        //Manda a la función para eliminar los archivos
                        int n = DataManagerControlDocumentos.DeleteArchivo(item);
                    }
                    
                    Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
                    //Se asigna el id 
                    objVersion.id_version = idVersion;

                    //Mandamos a llamar a la  función para eliminar la versión.
                    int delete_version = DataManagerControlDocumentos.DeleteVersion(objVersion);

                    if(delete_version != 0)
                    {
                        //Creamos un objeto para la versión anterior 
                        Model.ControlDocumentos.Version lastVersion = new Model.ControlDocumentos.Version();

                        //asigamos el id de la version anterior, cambiamos el estatus a liberado
                        lastVersion.id_version = last_id;
                        lastVersion.id_estatus_version = 1;

                        //Ejecutamos el método para actualizar el estatus de la versión.
                        int update = DataManagerControlDocumentos.Update_EstatusVersion(lastVersion);

                        if(update != 0)
                        {
                            await dialog.SendMessage("Información", "Cambios realizados correctamente");

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
                            await dialog.SendMessage("Error", "No se pudo eliminar el documento..");
                        }
                    }
                    else
                    {
                        await dialog.SendMessage("Error", "No se pudo eliminar la versión..");
                    }

                }
                else
                {
                    await dialog.SendMessage("Alerta", "El documento no tiene versión anterior..");
                }
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
            if (nombre != null & version != null & fecha != null & descripcion != null & id_tipo != 0 & _ListaDocumentos.Count != 0 & _usuario!=null & _id_dep!=0 & usuarioAutorizo!=null)
                return true;
            else 
                return false;
        }

        /// <summary>
        /// Método que inicializa la lista de los departamentos, tipos y usuarios
        /// </summary>
        private void Inicializar()
        {
            ListaDepartamento= DataManagerControlDocumentos.GetDepartamento();
            ListaTipo = DataManagerControlDocumentos.GetTipo();
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();
        }

        /// <summary>
        /// Método para validar que todos los checkbos de la lista de Validaciones estén seleccioandos
        /// </summary>
        /// <returns></returns>
        private bool ValidaSelected()
        {
            //Itera la lista de Validaciones
            foreach (var item in ListaValidaciones)
            {
                //Si hay un elemento que no está seleccionado, retorna falso
                if (!item.selected)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Método que valida el tipo de documento
        /// si es de tipo OHSAS, ESPECIFICOS O ISO, valida que en la ListaDocumentos sólo tenga un archivo
        /// </summary>
        /// <returns></returns>
        private bool ValidaTipo()
        {
            //Si es procedimiento o formatos
            if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014)
            {
                //Si la lista tiene más de un archivo, retorna falso
                if (_ListaDocumentos.Count > 1)
                    return false;
                else
                    return true;
            }
            else
            {
                //si no es de tipo procedimiento o formato, retorna verdadero
                return true;
            }
        }
        #endregion
    }
}
