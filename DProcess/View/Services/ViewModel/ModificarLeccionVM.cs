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
        private ObservableCollection<Archivo_LeccionesAprendidas> _ListaArchivosLecciones;
        public ObservableCollection<Archivo_LeccionesAprendidas> ListaArchivosLecciones
        {
            get
            {
                return _ListaArchivosLecciones;
            }
            set
            {
                _ListaArchivosLecciones = value;
                NotifyChange("ListaArchivosLecciones");
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
        private string _NIVEL_DE_CAMBIO;
        public string NIVEL_DE_CAMBIO
        {
            get
            {
                return _NIVEL_DE_CAMBIO;
            }
            set
            {
                _NIVEL_DE_CAMBIO = value;
                NotifyChange("NIVEL_DE_CAMBIO");
            }
        }
        private string _CENTRO_DE_TRABAJO;
        public string CENTRO_DE_TRABAJO
        {
            get
            {
                return _CENTRO_DE_TRABAJO;
            }
            set
            {
                _CENTRO_DE_TRABAJO = value;
                NotifyChange("CENTRO_DE_TRABAJO");
            }
        }
        private string _OPERACION;
        public string OPERACION
        {
            get
            {
                return _OPERACION;
            }
            set
            {
                _OPERACION = value;
                NotifyChange("OPERACION");
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
        private string _CRITERIO;
        public string CRITERIO
        {
            get
            {
                return _CRITERIO;
            }
            set
            {
                _CRITERIO = value;
                NotifyChange("CRITERIO");
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
        private bool _BttnGuardar;
        public bool BttnGuardar
        {
            get
            {
                return _BttnGuardar;
            }
            set
            {
                _BttnGuardar = value;
                NotifyChange("BttnGuardar");
            }
        }
        private bool _BttnInsertar;
        public bool BttnInsertar
        {
            get
            {
                return _BttnInsertar;
            }
            set
            {
                _BttnInsertar = value;
                NotifyChange("BttnInsertar");
            }
        }
        private bool _BttnEliminar;
        public bool BttnEliminar
        {
            get
            {
                return _BttnEliminar;
            }
            set
            {
                _BttnEliminar = value;
                NotifyChange("BttnEliminar");
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
        public int id_leccion;
        public Usuario User;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor para eliminar o modificar una lección
        /// </summary>
        /// <param name="SelectedLeccion"></param>
        public ModificarLeccionVM(LeccionesAprendidas SelectedLeccion,Usuario ModelUsuario)
        {
            User = ModelUsuario;
            //verificamos que se haya seleccionado una leccion
            if (SelectedLeccion != null)
            {
                User = ModelUsuario;
                //asignamos los valores a los botones
                BttnGuardar = true;
                BttnInsertar = false;
                BttnEliminar = true;
                IsEnabled = false;
                Encriptacion ecr = new Encriptacion();
                //obtenemos los usuarios
                ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();

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
                NIVEL_DE_CAMBIO = SelectedLeccion.NIVEL_DE_CAMBIO;
                FECHA_ULTIMO_CAMBIO = SelectedLeccion.FECHA_ULTIMO_CAMBIO;
                FECHA_ACTUALIZACION = SelectedLeccion.FECHA_ACTUALIZACION;
                DESCRIPCION_PROBLEMA = SelectedLeccion.DESCRIPCION_PROBLEMA;
                OPERACION = SelectedLeccion.OPERACION;
                CENTRO_DE_TRABAJO = SelectedLeccion.CENTRO_DE_TRABAJO;
                SOLICITUD_TRABAJO_DE_ING = SelectedLeccion.SOLICITUD_DE_TRABAJO;
                CRITERIO = SelectedLeccion.CRITERIO_1;

                //obtenemos la lista de las lecciones aprendidas
                ListaArchivosLecciones = DataManagerControlDocumentos.GetArchivosLecciones(SelectedLeccion.ID_LECCIONES_APRENDIDAS);
                //asignamos una imagen dependiendo de la extencion del archivo
                foreach (var item in ListaArchivosLecciones)
                {
                    if (item.EXT == ".pdf")
                    {
                        item.ruta = @"/Images/p.png";
                    }
                    else if (item.EXT == ".doc" || item.EXT == ".DOC")
                    {
                        item.ruta = @"/Images/w.png";
                    }
                    else if (item.EXT == ".xlsx")
                    {
                        item.ruta = @"/Images/E.jpg";
                    }
                    else if (item.EXT == ".pptx")
                    {
                        item.ruta = @"/Images/PP.png";
                    }
                    else
                    {
                        item.ruta = @"/Images/I.png";
                    }
                }
            }
        }

        /// <summary>
        /// Contructor para insertar una NUEVA lección.
        /// </summary>
        public ModificarLeccionVM(Usuario ModelUsuario)
        {
            User = ModelUsuario;

            IsEnabled = false;
            BttnInsertar = true;
            BttnGuardar = false;
            BttnEliminar = false;
            usuarioAutorizo = ModelUsuario.NombreUsuario;

            //verificamos que el usuario sea administrador del sistema
            if (Module.UsuarioIsRol(User.Roles, 2))
            {
                IsEnabled = true;
            }

            FECHA_ULTIMO_CAMBIO = DateTime.Today;
            FECHA_ACTUALIZACION = DateTime.Today;
            //obtenemos el usuario que este loggeado y asigamos el valor
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();
            ListaArchivosLecciones = new ObservableCollection<Archivo_LeccionesAprendidas>();
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
        /// Método para insertar una nueva leccion aprendida a la base de datos 
        /// </summary>
        public ICommand InsertarLeccion
        {
            get
            {
                return new RelayCommand(o => insertarLeccion());
            }
        }

        /// <summary>
        /// Comando para agregar archivos 
        /// </summary>
        public ICommand _AdjuntarArchivo
        {
            get
            {
                return new RelayCommand(o => InsertArchivo());
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

        #endregion

        #region Métodos

        /// <summary>
        /// Método para insertar una NUEVA leccion aprendida.
        /// </summary>
        private async void insertarLeccion()
        {
            //declaramos un pbjeto de tipo dialogservice
            DialogService service = new DialogService();
            MetroDialogSettings button = new MetroDialogSettings();

            //asignamos los valores de los botones
            button.AffirmativeButtonText = StringResources.lblYes;
            button.NegativeButtonText = StringResources.lblNo;

            MessageDialogResult result = await service.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertarLeccionAprendida, button, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                //si result es afirmativo, verificamos que ningun campo este vacio
                if (!string.IsNullOrEmpty(_usuarioAutorizo) && !string.IsNullOrEmpty(_COMPONENTE) && !string.IsNullOrEmpty(_CAMBIO_REQUERIDO) &&
                                    !string.IsNullOrEmpty(_NIVEL_DE_CAMBIO) && !string.IsNullOrEmpty(_CENTRO_DE_TRABAJO) && !string.IsNullOrEmpty(_OPERACION) &&
                                    !string.IsNullOrEmpty(_DESCRIPCION_PROBLEMA) && !string.IsNullOrEmpty(_REPORTADO_POR) && !string.IsNullOrEmpty(_SOLICITUD_TRABAJO_DE_ING) &&
                                    !string.IsNullOrEmpty(_CRITERIO) && _FECHA_ULTIMO_CAMBIO != null && (_FECHA_ACTUALIZACION) != null)
                {
                    //declaramos un objeto del tipo leccionaes aprendidas
                    LeccionesAprendidas NewData = new LeccionesAprendidas();

                    //asignamos los valores
                    NewData.ID_USUARIO = _usuarioAutorizo;
                    NewData.COMPONENTE = _COMPONENTE;
                    NewData.CAMBIO_REQUERIDO = _CAMBIO_REQUERIDO;
                    NewData.NIVEL_DE_CAMBIO = _NIVEL_DE_CAMBIO;
                    NewData.CENTRO_DE_TRABAJO = _CENTRO_DE_TRABAJO;
                    NewData.OPERACION = _OPERACION;
                    NewData.DESCRIPCION_PROBLEMA = _DESCRIPCION_PROBLEMA;
                    NewData.FECHA_ULTIMO_CAMBIO = _FECHA_ULTIMO_CAMBIO;
                    NewData.FECHA_ACTUALIZACION = _FECHA_ACTUALIZACION;
                    NewData.REPORTADO_POR = _REPORTADO_POR;
                    NewData.SOLICITUD_DE_TRABAJO = _SOLICITUD_TRABAJO_DE_ING;
                    NewData.CRITERIO_1 = _CRITERIO;

                    //insertamos los datos de la leccion aprendida a la base de datos
                    int insertar = DataManagerControlDocumentos.InsertLeccion(NewData.ID_USUARIO,NewData.COMPONENTE,NewData.CAMBIO_REQUERIDO,NewData.NIVEL_DE_CAMBIO,
                        NewData.CENTRO_DE_TRABAJO,NewData.OPERACION,NewData.DESCRIPCION_PROBLEMA,NewData.REPORTADO_POR,
                        NewData.SOLICITUD_DE_TRABAJO,NewData.CRITERIO_1, NewData.FECHA_ULTIMO_CAMBIO, NewData.FECHA_ACTUALIZACION);

                    //mandamos llamar el metodo que inserta los archivos de la leccion aprendida
                    InsertarNuevoArchivo(insertar);

                    //verificamos que se hayan insertado los datos
                    if (insertar>0)
                    {
                        await service.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertoExitoLeccion);

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
        /// Método que inserta un archivo si la leccion aprendida es NUEVA
        /// </summary>
        /// <param name="idLeccionesAprendidadas"></param>
        private async void InsertarNuevoArchivo(int idLeccionesAprendidadas)
        {
            //obtenemos todos los archivos que contenga en listbox
            foreach (Archivo_LeccionesAprendidas archivo in ListaArchivosLecciones)
            {
                //insertamos los archivos uno por uno
                int r = await DataManagerControlDocumentos.SetArchivo_Lecciones(archivo.ARCHIVO, archivo.EXT, archivo.NOMBRE_ARCHIVO, idLeccionesAprendidadas);
            }
        }

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
                //verificamos que ningun campo se encuentre vacio
                if (!string.IsNullOrEmpty(_usuarioAutorizo) && !string.IsNullOrEmpty(_COMPONENTE) && !string.IsNullOrEmpty(_CAMBIO_REQUERIDO) &&
                    !string.IsNullOrEmpty(_NIVEL_DE_CAMBIO) && !string.IsNullOrEmpty(_CENTRO_DE_TRABAJO) && !string.IsNullOrEmpty(_OPERACION) &&
                    !string.IsNullOrEmpty(_DESCRIPCION_PROBLEMA) && !string.IsNullOrEmpty(_REPORTADO_POR) && !string.IsNullOrEmpty(_SOLICITUD_TRABAJO_DE_ING) &&
                    !string.IsNullOrEmpty(_CRITERIO) && _FECHA_ULTIMO_CAMBIO != null && (_FECHA_ACTUALIZACION) != null)
                {
                    //declaramos un objeto del tipo lecciones aprendidas
                    LeccionesAprendidas NewData = new LeccionesAprendidas();

                    //asigamos los valores
                    NewData.ID_USUARIO = _AuxUsuarioAutorizo;
                    NewData.COMPONENTE = _COMPONENTE;
                    NewData.CAMBIO_REQUERIDO = _CAMBIO_REQUERIDO;
                    NewData.NIVEL_DE_CAMBIO = _NIVEL_DE_CAMBIO;
                    NewData.CENTRO_DE_TRABAJO = _CENTRO_DE_TRABAJO;
                    NewData.OPERACION = _OPERACION;
                    NewData.DESCRIPCION_PROBLEMA = _DESCRIPCION_PROBLEMA;
                    NewData.FECHA_ULTIMO_CAMBIO = _FECHA_ULTIMO_CAMBIO;
                    NewData.FECHA_ACTUALIZACION = _FECHA_ACTUALIZACION;
                    NewData.REPORTADO_POR = _REPORTADO_POR;
                    NewData.SOLICITUD_DE_TRABAJO = _SOLICITUD_TRABAJO_DE_ING;
                    NewData.CRITERIO_1 = _CRITERIO;

                    //los insertamos a la base de datos
                    int i = DataManagerControlDocumentos.UpdateLecccion(id_leccion,
                                                                        NewData.ID_USUARIO,
                                                                        NewData.COMPONENTE,
                                                                        NewData.CAMBIO_REQUERIDO,
                                                                        NewData.NIVEL_DE_CAMBIO,
                                                                        NewData.CENTRO_DE_TRABAJO,
                                                                        NewData.OPERACION,
                                                                        NewData.DESCRIPCION_PROBLEMA,
                                                                        NewData.FECHA_ULTIMO_CAMBIO,
                                                                        NewData.FECHA_ACTUALIZACION,
                                                                        NewData.REPORTADO_POR,
                                                                        NewData.SOLICITUD_DE_TRABAJO,
                                                                        NewData.CRITERIO_1);

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
        /// Metodo para Insertar un archivo en una leccion aprendida
        /// </summary>
        /// <param name="id_leccion"></param>
        private async void InsertArchivo()
        {
            //declaramos los servicios
            DialogService dialog = new DialogService();
            ProgressDialogController AsyncProgress;
            Microsoft.Win32.OpenFileDialog ventana = new Microsoft.Win32.OpenFileDialog();

            //verificamos si es una NUEVA leccion o si se va a MODIFICAR una leccion
            if (BttnInsertar)
            {
                //si es una NUEVA leccion abrimos la ventana.
                Nullable<bool> result = ventana.ShowDialog();

                //verifica si un archivo fue seleccionado
                if (result == true)
                {
                    try
                    {
                        //obtenemos el nombre del archivo
                        string nombrearchivo = ventana.FileName;

                        //declaramos un objeto de tipo archivo_leccionaes aprendidas
                        Archivo_LeccionesAprendidas archivo = new Archivo_LeccionesAprendidas();

                        //verificamos que el archivo no este en uso
                        if (!IsFileInUse(nombrearchivo))
                        {
                            AsyncProgress = await dialog.SendProgressAsync(StringResources.msgEspera, StringResources.msgInsertando);

                            //obtenemos los datos del archivo seleccionado
                            archivo.ARCHIVO = await Task.Run(() => File.ReadAllBytes(nombrearchivo));
                            archivo.EXT = System.IO.Path.GetExtension(nombrearchivo);
                            archivo.NOMBRE_ARCHIVO = System.IO.Path.GetFileNameWithoutExtension(nombrearchivo);
                            archivo.ID_LECCIONES_APRENDIDAS = id_leccion;
                            
                            //dependiendo de la extencion del archivo le agregamos una imagen
                            if (archivo.EXT == ".pdf")
                            {
                                archivo.ruta = @"/Images/p.png";
                                //lo mostramos en el listbox de la lista
                                ListaArchivosLecciones.Add(archivo);
                            }
                            else if (archivo.EXT == ".doc" || archivo.EXT == ".DOC")
                            {
                                archivo.ruta = @"/Images/w.png";
                                ListaArchivosLecciones.Add(archivo);
                            }
                            else if (archivo.EXT == ".xlsx")
                            {
                                archivo.ruta = @"/Images/E.jpg";
                                ListaArchivosLecciones.Add(archivo);
                            }
                            else if (archivo.EXT == ".pptx")
                            {
                                archivo.ruta = @"/Images/PP.png";
                                ListaArchivosLecciones.Add(archivo);
                            }
                            else
                            {
                                archivo.ruta = @"/Images/I.png";
                                ListaArchivosLecciones.Add(archivo);
                            }

                            await AsyncProgress.CloseAsync();
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgArchivoInsertado);

                        }else
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                        }
                    }
                    catch (Exception)
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorGuardandoArchivo);
                    }
                }
            }
            else
            {
                //si se va a MODIFICAR la leccion abrimos la ventana
                Nullable<bool> result = ventana.ShowDialog();

                //verificamos que se haya seleccionado un archivo
                if (result == true)
                {
                    try
                    {
                        //obtenemos el nombre del archivo
                        string nombrearchivo = ventana.FileName;

                        //declaramos un objeto de tipo archivo_leccionesaprendidas
                        Archivo_LeccionesAprendidas archivo = new Archivo_LeccionesAprendidas();

                        //verificamos que el archivo no este en uso
                        if (!IsFileInUse(nombrearchivo))
                        {
                            AsyncProgress = await dialog.SendProgressAsync(StringResources.msgEspera, StringResources.msgInsertando);

                            //obtenemos los datos del archivo seleccionado
                            archivo.ARCHIVO = await Task.Run(() => File.ReadAllBytes(nombrearchivo));
                            archivo.EXT = System.IO.Path.GetExtension(nombrearchivo);
                            archivo.NOMBRE_ARCHIVO = System.IO.Path.GetFileNameWithoutExtension(nombrearchivo);
                            archivo.ID_LECCIONES_APRENDIDAS = id_leccion;

                            //insertamos los datos del archivo a la base de datos
                            int insertar = await DataManagerControlDocumentos.SetArchivo_Lecciones(archivo.ARCHIVO, archivo.EXT, archivo.NOMBRE_ARCHIVO, id_leccion);

                            await AsyncProgress.CloseAsync();

                            //comprobamos que se hayan insertado los valores
                            if (insertar > 0)
                            {
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgArchivoInsertado);

                                //le agregamos una imagen dependiendo de la extencion
                                if (archivo.EXT == ".pdf")
                                {
                                    archivo.ruta = @"/Images/p.png";
                                    //lo mostramos en el listbox de la vista
                                    ListaArchivosLecciones.Add(archivo);
                                }
                                else if (archivo.EXT == ".doc" || archivo.EXT == ".DOC")
                                {
                                    archivo.ruta = @"/Images/w.png";
                                    ListaArchivosLecciones.Add(archivo);
                                }
                                else if (archivo.EXT == ".xlsx")
                                {
                                    archivo.ruta = @"/Images/E.jpg";
                                    ListaArchivosLecciones.Add(archivo);
                                }
                                else if (archivo.EXT == ".pptx")
                                {
                                    archivo.ruta = @"/Images/PP.png";
                                    ListaArchivosLecciones.Add(archivo);
                                }
                                else
                                {
                                    archivo.ruta = @"/Images/I.png";
                                    ListaArchivosLecciones.Add(archivo);
                                }
                            }
                            else
                            {
                                //si no se puedo insertar mandamos un mensaje de error
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorGuardandoArchivo);
                            }
                        }
                        else
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                        }
                    }
                    catch (Exception)
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorGuardandoArchivo);
                    }
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
                    ListaArchivosLecciones.Remove(item);

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

        #endregion

    }
}
