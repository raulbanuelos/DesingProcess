using Model;
using Model.ControlDocumentos;
using System;
using Encriptar;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using System.Diagnostics;
using MahApps.Metro.Controls;
using System.Windows;
using View.Resources;
using View.Forms.LeccionesAprendidas;
using MahApps.Metro.IconPacks;

namespace View.Services.ViewModel
{
    public class ModificarLeccionVM
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

        private ObservableCollection<Usuarios> _ListaUsuarios;
        public ObservableCollection<Usuarios> ListaUsuarios
        {
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

        private ObservableCollection<Archivo_LeccionesAprendidas> _ListaDocumentos;
        public ObservableCollection<Archivo_LeccionesAprendidas> ListaDocumentos
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

        private ObservableCollection<CentrosTrabajo> _ListaCentroTrabajoLeccion;
        public ObservableCollection<CentrosTrabajo> ListaCentroTrabajoLeccion
        {
            get
            {
                return _ListaCentroTrabajoLeccion;
            }
            set
            {
                _ListaCentroTrabajoLeccion = value;
                NotifyChange("ListaCentroTrabajoLeccion");
            }
        }

        private ObservableCollection<TIPOCAMBIO> _ListaTipoCambioLeccion;
        public ObservableCollection<TIPOCAMBIO> ListaTipoCambioLeccion
        {
            get
            {
                return _ListaTipoCambioLeccion;
            }
            set
            {
                _ListaTipoCambioLeccion = value;
                NotifyChange("ListaTipoCambioLeccion");
            }
        }

        private ObservableCollection<TIPOCAMBIO> _ListaNivelesDeCambio;
        public ObservableCollection<TIPOCAMBIO> ListaNivelesDeCambio
        {
            get
            {
                return _ListaNivelesDeCambio;
            }
            set
            {
                _ListaNivelesDeCambio = value;
                NotifyChange("ListaNivelesDeCambio");
            }
        }

        private ObservableCollection<CentrosTrabajo> _ListaCentrosDeTrabajo;
        public ObservableCollection<CentrosTrabajo> ListaCentrosDeTrabajo
        {
            get
            {
                return _ListaCentrosDeTrabajo;
            }
            set
            {
                _ListaCentrosDeTrabajo = value;
                NotifyChange("ListaCentrosDeTrabajo");
            }
        }

        private Archivo_LeccionesAprendidas _SelectedItem;
        public Archivo_LeccionesAprendidas SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                NotifyChange("SelectedItem");
            }
        }

        private HamburgerMenuItemCollection _menuItems;
        public HamburgerMenuItemCollection MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                if (Equals(value, _menuItems)) return;
                _menuItems = value;
                NotifyChange("MenuItems");
            }
        }

        private HamburgerMenuItemCollection _menuOptionItems;
        public HamburgerMenuItemCollection MenuOptionItems
        {
            get
            {
                return _menuOptionItems;
            }
            set
            {
                if (Equals(value, _menuOptionItems)) return;
                _menuOptionItems = value;
                NotifyChange("MenuOptionItems");
            }
        }

        private DateTime _FECHA_ACTUALIZACION;
        public DateTime FECHA_ACTUALIZACION
        {
            get
            {

                return _FECHA_ACTUALIZACION;
            }
            set
            {
                _FECHA_ACTUALIZACION = value;
                NotifyChange("FECHA_ACTUALIZACION");
            }
        }

        private DateTime _FECHA_ULTIMO_CAMBIO;
        public DateTime FECHA_ULTIMO_CAMBIO
        {
            get
            {
                return _FECHA_ULTIMO_CAMBIO;
            }
            set
            {
                _FECHA_ULTIMO_CAMBIO = value;
                NotifyChange("FECHA_ULTIMO_CAMBIO");
            }
        }

        private string _COMPONENTE;
        public string COMPONENTE
        {
            get
            {
                return _COMPONENTE;
            }
            set
            {
                _COMPONENTE = value;
                NotifyChange("value");
            }
        }

        private string _CAMBIO_REQUERIDO;
        public string CAMBIO_REQUERIDO
        {
            get
            {
                return _CAMBIO_REQUERIDO;
            }
            set
            {
                _CAMBIO_REQUERIDO = value;
                NotifyChange("CAMBIO_REQUERIDO");
            }
        }

        private string _DESCRIPCION_PROBLEMA;
        public string DESCRIPCION_PROBLEMA
        {
            get
            {
                return _DESCRIPCION_PROBLEMA;
            }
            set
            {
                _DESCRIPCION_PROBLEMA = value;
                NotifyChange("DESCRIPCION_PROBLEMA");
            }
        }

        private string _REPORTADO_POR;
        public string REPORTADO_POR
        {
            get
            {
                return _REPORTADO_POR;
            }
            set
            {
                _REPORTADO_POR = value;
                NotifyChange("REPORTADO_POR");
            }
        }

        private string _SOLICITUD_TRABAJO_DE_ING;
        public string SOLICITUD_TRABAJO_DE_ING
        {
            get
            {
                return _SOLICITUD_TRABAJO_DE_ING;
            }
            set
            {
                _SOLICITUD_TRABAJO_DE_ING = value;
                NotifyChange("SOLICITUD_TRABAJO_DE_ING");
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

        private string _AuxUsuarioAutorizo;
        public string AuxUsuarioAutorizo
        {
            get
            {
                return _AuxUsuarioAutorizo;
            }
            set
            {
                _AuxUsuarioAutorizo = value;
                NotifyChange("AuxUsuarioAutorizo");
            }
        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get
            {
                return _IsEnabled;
            }
            set
            {
                _IsEnabled = value;
                NotifyChange("IsEnabled");
            }
        }

        private bool bttnEnabled = false;
        public bool BttnEnabled
        {
            get
            {
                return bttnEnabled;
            }
            set
            {
                bttnEnabled = value;
                NotifyChange("BttnEnabled");
            }
        }

        public int id_leccion;
        public Usuario User;

        private ObservableCollection<CentrosTrabajo> _ListaCentrosDeTrabajoSeleccionados;
        public ObservableCollection<CentrosTrabajo> ListaCentrosDeTrabajoSeleccionados
        {
            get
            {
                return _ListaCentrosDeTrabajoSeleccionados;
            }
            set
            {
                _ListaCentrosDeTrabajoSeleccionados = value;
                NotifyChange("ListaCentrosDeTrabajoSeleccionados");
            }
        }

        public LeccionesAprendidas AuxLeccionSeleccionada;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor para eliminar o modificar una lección
        /// </summary>
        /// <param name="SelectedLeccion"></param>
        public ModificarLeccionVM(LeccionesAprendidas SelectedLeccion,Usuario ModelUsuario)
        {
            AuxLeccionSeleccionada = SelectedLeccion;
            User = ModelUsuario;

            //verificamos que se haya seleccionado una leccion
            if (SelectedLeccion != null)
            {
                if (Module.UsuarioIsRol(User.Roles, 2))
                {
                    BttnEnabled = true;
                }

                CreateMenuItems();
                ListaCentrosDeTrabajoSeleccionados = new ObservableCollection<CentrosTrabajo>();
                User = ModelUsuario;
                //asignamos los valores a los botones
                IsEnabled = false;
                Encriptacion ecr = new Encriptacion();

                //obtenemos los usuarios
                ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();

                //Obtenemos los centros de trabajo que esten relacionados con la lección aprendida
                ListaCentrosDeTrabajoSeleccionados = DataManagerControlDocumentos.GetCentrosDetrabajoLecciones(SelectedLeccion.ID_LECCIONES_APRENDIDAS);

                //Obtenemos los tipos de cambio que esten relacionados con la leccion aprendida
                ListaTipoCambioLeccion = DataManagerControlDocumentos.GetTipoCambioLecciones(SelectedLeccion.ID_LECCIONES_APRENDIDAS);

                //Obtenemos todos los centros de trabajo
                ListaCentrosDeTrabajo = DataManagerControlDocumentos.GetCentrosDeTrabajo("");


                //Obtenemos todos los tipos de cambio
                ListaNivelesDeCambio = DataManagerControlDocumentos.GetNivelesDeCambio();

                //
                foreach (var item in ListaCentrosDeTrabajo)
                {
                    if (ExisteCentroDeTrabajo(item.CentroTrabajo, ListaCentrosDeTrabajoSeleccionados))
                    {
                        item.IsSelected = true;
                    }
                }

                //
                foreach (var item in ListaNivelesDeCambio)
                {
                    if (ExisteNivelDeCambio(item.ID_TIPOCAMBIO, ListaTipoCambioLeccion))
                    {
                        item.IsSelected = true;
                    }
                }



                //verificamos que el usuario sea administrador del sistema
                if (Module.UsuarioIsRol(User.Roles, 2))
                {
                    IsEnabled = true;
                }

                //mostramos los valores de la leccion 
                id_leccion = SelectedLeccion.ID_LECCIONES_APRENDIDAS;
                usuarioAutorizo = ecr.encript(SelectedLeccion.ID_USUARIO);
                AuxUsuarioAutorizo = ModelUsuario.NombreUsuario;
                CAMBIO_REQUERIDO = SelectedLeccion.CAMBIO_REQUERIDO;
                COMPONENTE = SelectedLeccion.COMPONENTE;
                REPORTADO_POR = SelectedLeccion.REPORTADO_POR;
                FECHA_ULTIMO_CAMBIO = SelectedLeccion.FECHA_ULTIMO_CAMBIO;
                FECHA_ACTUALIZACION = SelectedLeccion.FECHA_ACTUALIZACION;
                DESCRIPCION_PROBLEMA = SelectedLeccion.DESCRIPCION_PROBLEMA;
                SOLICITUD_TRABAJO_DE_ING = SelectedLeccion.SOLICITUD_DE_TRABAJO;


                //obtenemos la lista de las lecciones aprendidas
                ListaDocumentos = DataManagerControlDocumentos.GetArchivosLecciones(SelectedLeccion.ID_LECCIONES_APRENDIDAS);
                //asignamos una imagen dependiendo de la extencion del archivo
                foreach (var item in ListaDocumentos)
                {
                    if (item.EXT == ".pdf")
                    {
                        item.rutaIcono = @"/Images/p.png";
                    }
                    else if (item.EXT == ".doc" || item.EXT == ".DOC")
                    {
                        item.rutaIcono = @"/Images/w.png";
                    }
                    else if (item.EXT == ".xlsx")
                    {
                        item.rutaIcono = @"/Images/E.jpg";
                    }
                    else if (item.EXT == ".pptx")
                    {
                        item.rutaIcono = @"/Images/PP.png";
                    }
                    else
                    {
                        item.rutaIcono = @"/Images/I.png";
                    }
                }
            }
        }

        #endregion

        #region Comandos

        /// <summary>
        /// Comando para guardar los datos de una leccion aprendida modificada
        /// </summary>
        public ICommand GuardarLeccion
        {
            get
            {
                return new RelayCommand(o => guardarLeccion(User));
            }
        }

        /// <summary>
        /// Método para eliminar una Leccion
        /// </summary>
        public ICommand EliminarLeccion
        {
            get
            {
                return new RelayCommand(o => eliminarLeccion());
            }
        } 

        /// <summary>
        /// Comando para eliminar un archivo
        /// de las lecciones aprendidas
        /// </summary>
        public ICommand EliminarItem
        {
            get
            {
                return new RelayCommand(o => eliminarItem(SelectedItem));
            }
        }

        /// <summary>
        /// Comando para ver una archivo
        /// </summary>
        public ICommand VerArchivo
        {
            get
            {
                return new RelayCommand(o => verArchivo(SelectedItem));
            }
        }

        public ICommand IrElegirCentroTrabajo
        {
            get
            {
                return new RelayCommand(a => ElegirCentroTrabajo());
            }
        }

        public ICommand IrElegirNivelCambio
        {
            get
            {
                return new RelayCommand(a => ElegirNivelCambio());
            }
        }

        public ICommand CerrarVentana
        {
            get
            {
                return new RelayCommand(a => cerrarVentana());
            }
        }

        public ICommand _AdjuntarArchivo
        {
            get
            {
                return new RelayCommand(o => AdjuntarArchivo());
            }
        }

        public ICommand BuscarCentroTrabajo
        {
            get
            {
                return new RelayCommand(a => EncontrarCentroTrabajo((string)a));
            }
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Método para guardar los cambios de una LeCCION
        /// </summary>
        private async void guardarLeccion(Usuario ModelUsuario)
        {
            User = ModelUsuario;
            //declaramos un objeto de tipo dialogservice
            DialogService service = new DialogService();
            MetroDialogSettings button = new MetroDialogSettings();

            //asignamos los valores de los botones
            button.AffirmativeButtonText = StringResources.lblYes;
            button.NegativeButtonText = StringResources.lblNo;

            MessageDialogResult result = await service.SendMessage(StringResources.ttlAlerta, StringResources.msgConfirmacion, button, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                InsertarNuevosCentrosDeTrabajo();
                InsertarNuevosTiposDeCambio();
                bool SaveData = GuardarCambios();

                //verificamos que ningun campo se encuentre vacio
                if (SaveData == true)
                {
                    //declaramos un objeto del tipo lecciones aprendidas
                    LeccionesAprendidas NewData = new LeccionesAprendidas();

                    //asigamos los valores
                    if (Module.UsuarioIsRol(User.Roles, 2))
                    {
                        NewData.ID_USUARIO = _usuarioAutorizo;
                    }else
                    {
                        NewData.ID_USUARIO = AuxUsuarioAutorizo;
                    }
                    NewData.COMPONENTE = _COMPONENTE;
                    NewData.CAMBIO_REQUERIDO = _CAMBIO_REQUERIDO;
                    NewData.DESCRIPCION_PROBLEMA = _DESCRIPCION_PROBLEMA;
                    NewData.FECHA_ULTIMO_CAMBIO = _FECHA_ULTIMO_CAMBIO;
                    NewData.FECHA_ACTUALIZACION = _FECHA_ACTUALIZACION;
                    NewData.REPORTADO_POR = _REPORTADO_POR;
                    NewData.SOLICITUD_DE_TRABAJO = _SOLICITUD_TRABAJO_DE_ING;

                    //los insertamos a la base de datos
                    int i = DataManagerControlDocumentos.UpdateLecccion(id_leccion,
                                                                        NewData.ID_USUARIO,
                                                                        NewData.COMPONENTE,
                                                                        NewData.NIVEL_DE_CAMBIO,
                                                                        NewData.CENTRO_DE_TRABAJO,
                                                                        NewData.OPERACION,
                                                                        NewData.DESCRIPCION_PROBLEMA,
                                                                        NewData.FECHA_ULTIMO_CAMBIO,
                                                                        NewData.FECHA_ACTUALIZACION,
                                                                        NewData.REPORTADO_POR,
                                                                        NewData.SOLICITUD_DE_TRABAJO);


                    int BorrarArchivos = DataManagerControlDocumentos.Delete_Archivo_Lecciones(id_leccion);

                    if (BorrarArchivos != 0)
                    {
                        foreach (var item in ListaDocumentos)
                        {
                            int e = await DataManagerControlDocumentos.SetArchivo_Lecciones(item.ARCHIVO, item.EXT, item.NOMBRE_ARCHIVO, id_leccion);
                        }
                    }

                    //verificamos que se hayan insertado los valores correctamente
                    if (i > 0)
                    {
                        await service.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosRealizados);

                        //obtenemos la ventana actual
                        var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                        if (window != null)
                        {
                            //cerramos la ventana 
                            window.Close();
                        }
                    }
                    else
                    {
                        await service.SendMessage(StringResources.msgError, StringResources.msgErrorGeneral);
                    }
                }
                else
                {
                    await service.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
                }                       
            }
        }

        /// <summary>
        /// Método para eliminar una leccion
        /// </summary>
        private async void eliminarLeccion()
        {
            //declaramos un objeto del tipo dialos service
            DialogService diaog = new DialogService();
            //declaramos los botones
            MetroDialogSettings button = new MetroDialogSettings();

            //asigamos los valores a los botones
            button.AffirmativeButtonText = StringResources.lblYes;
            button.NegativeButtonText = StringResources.lblNo;

            //preguntamos si se desea eliminar la leccion aprendida
            MessageDialogResult result = await diaog.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminarLeccion, button, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                //borramos la leccion de la base de datos
                int delete = DataManagerControlDocumentos.Delete_Lecciones(id_leccion);
                //verificamos que se haya eliminado la leccion
                if (delete>0)
                {
                    await diaog.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminacionCorrectaLeccion);

                    //obtenemos la ventana actual
                    var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                    if (window != null)
                    {
                        //cerramos la ventana 
                        window.Close();
                    }
                }else
                {
                    await diaog.SendMessage(StringResources.msgError, StringResources.msgErrorGeneral);
                }
            }
        }

        /// <summary>
        /// Método para adjuntar archivos a las lecciones aprendidas
        /// </summary>
        public async void AdjuntarArchivo()
        {
            //Inicializamos los servicios de dialog
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController AsyncProgress;

            //Inicializamos los servicios de metrodialog
            MetroDialogSettings settings = new MetroDialogSettings();

            //Declaramos las posibles respuestas del metrodialog
            settings.AffirmativeButtonText = StringResources.lblYes;
            settings.NegativeButtonText = StringResources.lblNo;

            //abrimos la ventana para buscar el archivo a insertar
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //obtenemos el archivo seleccionado
            Nullable<bool> result = dlg.ShowDialog();

            //Inicializamos una variable de tipo archivo_lecciones aprendidas que nos ayudara a guardar los datos del archivo a insertar
            Archivo_LeccionesAprendidas Archivo = new Archivo_LeccionesAprendidas();

            //Verificamos que se haya insertado un archivo
            if (result == true)
            {
                try
                {
                    //Obtenemos el nombre del archivo
                    string filename = dlg.FileName;

                    //Verificamos que no se encuentre abierto el archivo,si el archivo no esta entra en la condicion para agregar el archivo
                    if (!Module.IsFileInUse(filename))
                    {
                        //Mandamos un mensaje de espera mientras se inserta el archivo a la lista de documentos
                        AsyncProgress = await dialog.SendProgressAsync(StringResources.msgEspera, StringResources.msgInsertando);

                        //Obtenemos los datos del archivo que vayamos a insertar
                        Archivo.ARCHIVO = await Task.Run(() => File.ReadAllBytes(filename));
                        Archivo.EXT = System.IO.Path.GetExtension(filename);
                        Archivo.NOMBRE_ARCHIVO = System.IO.Path.GetFileNameWithoutExtension(filename);

                        //dependiendo la extensíon del archivo se agrega la ruta de imagen para visualisarla
                        switch (Archivo.EXT)
                        {
                            case ".doc":
                                Archivo.rutaIcono = @"/Images/w.png";

                                break;
                            case ".docx":
                                Archivo.rutaIcono = @"/Images/w.png";

                                break;
                            case ".pdf":
                                Archivo.rutaIcono = @"/Images/p.png";

                                break;
                            case ".xls":
                                Archivo.rutaIcono = @"/Images/E.jpg";

                                break;
                            case ".xlsx":
                                Archivo.rutaIcono = @"/Images/E.jpg";

                                break;
                            case "ppt":
                                Archivo.rutaIcono = @"/Images/PP.png";

                                break;
                            //todos los archivos que no tengan alguna de las extenciones antes mencionadas entraran aqui y se les pondra una imagen general
                            default:
                                Archivo.rutaIcono = @"/Images/I.png";
                                break;
                        }
                        //Agregamos el archivo a la lista, SOLO SE AGREGA LOCALMENTE, AUN NO LO INSERTAMOS EN LA BASE DE DATOS
                        ListaDocumentos.Add(Archivo);

                        //Cerramos el mensaje de espera
                        await AsyncProgress.CloseAsync();

                    }
                    else
                    {
                        //Si el archivo esta en uso se manda un mensaje para que lo cierre
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                    }
                }
                catch (Exception)
                {
                    //Si no se pudo cargar el archivo se manda un mensaje de error
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                }
            }
        }

        /// <summary>
        /// Método para eliminar un archivo de  las lecciones aprendidas.
        /// </summary>
        /// <param name="item"></param>
        /// 
        private async void eliminarItem(Archivo_LeccionesAprendidas item)
        {
            //Incializamos los servicios de dialog.
            DialogService dialogService = new DialogService();

            if (item != null)
            {
                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgDelArchivo, setting, MessageDialogStyle.AffirmativeAndNegative);

                if (item != null & result == MessageDialogResult.Affirmative)
                {
                    //Se elimina el item seleccionado de la listaDocumentos.
                    ListaDocumentos.Remove(item);

                    //Comparamos si el archivo se debe eliminar de la base de datos.
                    if (item.ID_ARCHIVO_LECCIONES != 0 && item.ID_LECCIONES_APRENDIDAS != 0)
                    {
                        //Se elimina de la base de datos.
                        int n = DataManagerControlDocumentos.Delete_Archivo_Lecciones(item.ID_ARCHIVO_LECCIONES);

                        if (n > 0)
                        {
                            await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgArchivoEliminadoCorrectamente);
                        }
                        else
                        {
                            await dialogService.SendMessage(StringResources.msgError, StringResources.msgArchivoEliminadoFallido);
                        }
                    }else
                    {
                        await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgArchivoEliminadoCorrectamente);
                    }
                }
            }
        }

        /// <summary>
        /// Método para visualizar un archivo seleccionado
        /// </summary>
        /// <param name="item"></param>
        private async void verArchivo(Archivo_LeccionesAprendidas item)
        {
            DialogService dialog = new DialogService();

            if (item!=null)
            {
                try
                {
                    //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                    string filename = GetPathTempFile(item);

                    //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                    File.WriteAllBytes(filename, item.ARCHIVO);
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
        /// Método que verifica si un archivo esta en uso
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
        /// Método que genera una cadena para cargar un archivo en la carpeta temporal del sistema
        /// para poder visualizarlo
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetPathTempFile(Archivo_LeccionesAprendidas item)
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
                filename = Path.Combine(tempFolder, item.NOMBRE_ARCHIVO  + aleatorio + item.EXT);
            } while (File.Exists(filename));

            //retornamos el nombre que se generó
            return filename;
        }

        /// <summary>
        /// Método que muestra la ventana para agregar o quitar centros de trabajo a la leccion aprendida
        /// </summary>
        public void ElegirCentroTrabajo()
        {
            CentrosDeTrabajo Form = new CentrosDeTrabajo();
            ListaCentrosDeTrabajoSeleccionados.Clear();

            Form.DataContext = this;
            Form.ShowDialog();

            //ListaCentrosDeTrabajoSeleccionados.Clear();

            foreach (var item in ListaCentrosDeTrabajo)
            {
                if (item.IsSelected)
                {
                    //Mostramos en la lista los Centros de trabajo que se hayan seleccionado en la vista anterior
                    if (ListaCentrosDeTrabajoSeleccionados.Where(x => x.CentroTrabajo == item.CentroTrabajo).ToList().Count == 0)
                    {
                        ListaCentrosDeTrabajoSeleccionados.Add(item);
                    }
                }
            }

            foreach (var item in ListaCentrosDeTrabajo)
            {
                if (!item.IsSelected)
                {
                    if (ListaCentrosDeTrabajoSeleccionados.Where(x => x.CentroTrabajo == item.CentroTrabajo).ToList().Count > 0)
                    {
                        CentrosTrabajo ct = ListaCentrosDeTrabajoSeleccionados.Where(x => x.CentroTrabajo == item.CentroTrabajo).FirstOrDefault();
                        ListaCentrosDeTrabajoSeleccionados.Remove(ct);
                    }
                }
            }


            ListaCentrosDeTrabajo = DataManagerControlDocumentos.GetCentrosDeTrabajo("");

            foreach (var item in ListaCentrosDeTrabajo)
            {
                if (ListaCentrosDeTrabajoSeleccionados.Where(x => x.CentroTrabajo == item.CentroTrabajo).ToList().Count > 0)
                {
                    item.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// Método que muestra la ventana para agregar o quitar niveles de cambio a la leccion aprendida
        /// </summary>
        public void ElegirNivelCambio()
        {
            TiposDeCambio Form = new TiposDeCambio();
            Form.DataContext = this;
            Form.ShowDialog();

            ListaTipoCambioLeccion.Clear();
            foreach (var item in ListaNivelesDeCambio)
            {
                if (item.IsSelected)
                {
                    ListaTipoCambioLeccion.Add(item);
                }
            }
        }

        /// <summary>
        /// Método para cerrar las ventanas de elegir centro de trabajo y elegir niveles de cambio
        /// </summary>
        public void cerrarVentana()
        {
            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
            var frm = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            //Verificamos que la pantalla sea diferente de nulo.
            if (frm != null)
            {
                //Ocultamos la pantalla
                frm.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();

            if (BttnEnabled == true)
            {
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        //Icono del Menú para guardar los cambios hechos a la lección aprendida
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.ContentSaveAll },
                        Label = StringResources.ttlGuardar,
                        Command = GuardarLeccion,
                        Tag = StringResources.ttlGuardar,
                    }
                );
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                    //Icono del menu para eliminar la leccion aprendida seleccionada
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Delete },
                    Label = StringResources.lblEliminar,
                    Command = EliminarLeccion,
                    Tag = StringResources.lblEliminar,
                    }
                );
            }
        }

        /// <summary>
        /// Método para insertar los nuevos centros de trabajo
        /// </summary>
        /// <returns></returns>
        public bool InsertarNuevosCentrosDeTrabajo()
        {
            bool DatosInsertados = false;
            int BorrarCentrosTrabajo = DataManagerControlDocumentos.DeleteCentrosDeTrabajoLeccion(id_leccion);

            if (BorrarCentrosTrabajo != 0)
            {
                foreach (var item in ListaCentrosDeTrabajoSeleccionados)
                {
                    int f = DataManagerControlDocumentos.InsertLeccionesCentroDeTrabajo(item.CentroTrabajo, id_leccion);
                    if (f != 0)
                    {
                        DatosInsertados = true;
                    }
                }
            }
            return DatosInsertados;
        }

        /// <summary>
        /// Método para insertar los nuevos tipos de cambio
        /// </summary>
        /// <returns></returns>
        public bool InsertarNuevosTiposDeCambio()
        {
            bool DatosInsertados = false;

            int BorrarTiposCambio = DataManagerControlDocumentos.DeleteTiposDeCambioLeccion(id_leccion);

            if (BorrarTiposCambio != 0)
            {
                foreach (var item in ListaTipoCambioLeccion)
                {
                    int a = DataManagerControlDocumentos.InsertLeccionesNivelCambio(item.ID_TIPOCAMBIO, id_leccion);
                    if (a != 0)
                    {
                        DatosInsertados = true;
                    }
                }
            }
            return DatosInsertados;
        }

        /// <summary>
        /// Método que busca los centros de trabajo
        /// </summary>
        /// <param name="TextoBuscar"></param>
        public void EncontrarCentroTrabajo(string TextoBuscar)
        {
            foreach (var item in ListaCentrosDeTrabajo)
            {
                if (item.IsSelected)
                {
                    //Mostramos en la lista los niveles de cambio que se hayan seleccionado en la vista anterior
                    if (ListaCentrosDeTrabajoSeleccionados.Where(x => x.CentroTrabajo == item.CentroTrabajo).ToList().Count == 0)
                    {
                        ListaCentrosDeTrabajoSeleccionados.Add(item);
                    }
                }
            }

            //ListaCentrosDeTrabajo.Clear();
            ObservableCollection<CentrosTrabajo> Aux = new ObservableCollection<CentrosTrabajo>();

            //ListaCentrosDeTrabajo = DataManagerControlDocumentos.GetCentrosDeTrabajo(TextoBuscar);

            Aux = DataManagerControlDocumentos.GetCentrosDeTrabajo(TextoBuscar);

            ListaCentrosDeTrabajo.Clear();

            foreach (var item in Aux)
            {
                ListaCentrosDeTrabajo.Add(item);
            }

            foreach (var item in ListaCentrosDeTrabajo)
            {
                if (ListaCentrosDeTrabajoSeleccionados.Where(x => x.CentroTrabajo == item.CentroTrabajo).ToList().Count > 0)
                {
                    item.IsSelected = true;
                }
            }
        }
        #endregion

        #region Funciones

        /// <summary>
        /// Funcion que obtiene los centros de trabajo que tiene una leccion
        /// </summary>
        /// <param name="idRol"></param>
        /// <param name="lista"></param>
        /// <returns></returns>
        private bool ExisteCentroDeTrabajo(string Id_CentroTrabajo, ObservableCollection<CentrosTrabajo> ListaCentrosTrabajo)
        {
            bool respuesta = false;

            foreach (var item in ListaCentrosTrabajo)
            {
                if (item.CentroTrabajo == Id_CentroTrabajo)
                {
                    respuesta = true;
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Funcion que obtiene los tipos de cambio que tiene una lección
        /// </summary>
        /// <param name="Id_NivelCambio"></param>
        /// <param name="ListaTipoCambio"></param>
        /// <returns></returns>
        private bool ExisteNivelDeCambio(int Id_NivelCambio, ObservableCollection<TIPOCAMBIO> ListaTipoCambio)
        {
            bool Respuesta = false;
            foreach (var item in ListaTipoCambio)
            {
                if (item.ID_TIPOCAMBIO == Id_NivelCambio)
                {
                    Respuesta = true;
                }
            }
            return Respuesta;
        }

        /// <summary>
        /// Función para ver que no se encuentre ningun campo vacio antes de guardar la leccion aprendida
        /// </summary>
        /// <returns></returns>
        private bool GuardarCambios()
        {
            bool DatosCompletos = false;

            if (!string.IsNullOrEmpty(_usuarioAutorizo) && !string.IsNullOrEmpty(_COMPONENTE) && 
                    !string.IsNullOrEmpty(_DESCRIPCION_PROBLEMA) && !string.IsNullOrEmpty(_REPORTADO_POR) && !string.IsNullOrEmpty(_SOLICITUD_TRABAJO_DE_ING) &&
                    _FECHA_ULTIMO_CAMBIO != null && (_FECHA_ACTUALIZACION) != null )
            {
                DatosCompletos = true;
            }
            return DatosCompletos;
        }

        #endregion
    }
}
