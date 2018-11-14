﻿using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.LeccionesAprendidas;
using System.Collections.ObjectModel;
using Model.ControlDocumentos;
using View.Resources;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using System.Diagnostics;

namespace View.Services.ViewModel
{
    public class InsertarNuevaLeccionVW : INotifyPropertyChanged
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

        private ObservableCollection<TIPOCAMBIO> _ListaNivelesDeCambioSeleccionados;
        public ObservableCollection<TIPOCAMBIO> ListaNivelesDeCambioSeleccionados
        {
            get
            {
                return _ListaNivelesDeCambioSeleccionados;
            }
            set
            {
                _ListaNivelesDeCambioSeleccionados = value;
                NotifyChange("ListaNivelesDeCambioSeleccionados");
            }
        }

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
                NotifyChange("_ListaCentrosDeTrabajoSeleccionados");
            }
        }

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

        private ObservableCollection<LeccionesAprendidas> _ListaComponentesSimilares;
        public ObservableCollection<LeccionesAprendidas> ListaComponentesSimilares
        {
            get
            {
                return _ListaComponentesSimilares;
            }
            set
            {
                _ListaComponentesSimilares = value;
                NotifyChange("ListaComponentesSimilares");
            }
        }

        private string _Componente;
        public string Componente
        {
            get
            {
                return _Componente;
            }
            set
            {
                _Componente = value;
                NotifyChange("Componente");
            }
        }

        private  string _CambioRequerido;
        public string CambioRequerido
        {
            get
            {
                return _CambioRequerido;
            }
            set
            {
                _CambioRequerido = value;
                NotifyChange("CambioRequerido");
            }
        }

        private string _NivelCambio;
        public string NivelCambio
        {
            get
            {
                return _NivelCambio;
            }
            set
            {
                _NivelCambio = value;
                NotifyChange("NivelCambio");
            }
        }

        private string _Operacion;
        public string Operacion
        {
            get
            {
                return _Operacion;
            }
            set
            {
                _Operacion = value;
                NotifyChange("Operacion");
            }
        }

        private string _DescripcionProblema;
        public string DescripcionProblema
        {
            get
            {
                return _DescripcionProblema;
            }
            set
            {
                _DescripcionProblema = value;
                NotifyChange("DescripcionProblema");
            }
        }

        private DateTime _FechaUltimoCambio;
        public DateTime FechaUltimoCambio
        {
            get
            {
                return _FechaUltimoCambio;
            }
            set
            {
                _FechaUltimoCambio = value;
                NotifyChange("FechaUltimoCambio");
            }
        }

        private DateTime _FechaActualizacion;
        public DateTime FechaActualizacion
        {
            get
            {
                return _FechaActualizacion;
            }
            set
            {
                _FechaActualizacion = value;
                NotifyChange("FechaActualizacion");
            }
        }

        private string _ReportadoPor;
        public string ReportadoPor
        {
            get
            {
                return _ReportadoPor;
            }
            set
            {
                _ReportadoPor = value;
                NotifyChange("ReportadoPor");
            }
        }

        private string _SolicitudTrabajoIng;
        public string SolicitudTrabajoIng
        {
            get
            {
                return _SolicitudTrabajoIng;
            }
            set
            {
                _SolicitudTrabajoIng = value;
                NotifyChange("SolicitudTrabajoIng");
            }
        }

        private string _UsuarioAutorizo;
        public string UsuarioAutorizo
        {
            get
            {
                return _UsuarioAutorizo;
            }
            set
            {
                _UsuarioAutorizo = value;
                NotifyChange("UsuarioAutorizo");
            }
        }

        private string _usuario;
        public string usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                NotifyChange("usuario");
            }
        }

        private Archivo_LeccionesAprendidas _ArchivoSeleccionado;
        public Archivo_LeccionesAprendidas ArchivoSeleccionado
        {
            get
            {
                return _ArchivoSeleccionado;
            }
            set
            {
                _ArchivoSeleccionado = value;
                NotifyChange("ArchivoSeleccionado");
            }
        }

        private bool _isEnabled = false;
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

        public InsertarNuevaLeccionVW(Usuario ModelUsuario)
        {
            User = ModelUsuario;
            usuario = User.NombreUsuario;

            //Obtenemos los datos que se van a mostrar en las listas
            ListaCentrosDeTrabajo = DataManagerControlDocumentos.GetCentrosDeTrabajo();
            ListaNivelesDeCambio = DataManagerControlDocumentos.GetNivelesDeCambio();
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();

            //Inicializamos las listas para poder guardar datos 
            ListaCentrosDeTrabajoSeleccionados = new ObservableCollection<CentrosTrabajo>();
            ListaNivelesDeCambioSeleccionados = new ObservableCollection<TIPOCAMBIO>();
            ListaDocumentos = new ObservableCollection<Archivo_LeccionesAprendidas>();
            ListaComponentesSimilares = new ObservableCollection<LeccionesAprendidas>();

            //Verificamos que el usuario sea administrador del sistema
            if (Module.UsuarioIsRol(User.Roles, 2))
            {
                IsEnabled = true;
            }

            //Obtenemos la fecha del sistema
            FechaUltimoCambio = DataManagerControlDocumentos.Get_DateTime();
            FechaActualizacion = DataManagerControlDocumentos.Get_DateTime();
        }

        #endregion

        #region Comandos

        public ICommand IrPaginaDescripcion
        {
            get
            {
                return new RelayCommand(a => IrPagDescripcion());
            }
        }

        public ICommand RegresarPaginaNuevaLeccion
        {
            get
            {
                return new RelayCommand(a => RegPagNuevaLeccion());
            }
        }

        public ICommand IrPaginaInformacionCambios
        {
            get
            {
                return new RelayCommand(a => IrPagCambios());
            }
        }

        public ICommand RegresarPaginaDescripcion
        {
            get
            {
                return new RelayCommand(a => RegPagInformacionCambios());
            }
        }

        public ICommand ElegirCentroTrabajo
        {
            get
            {
                return new RelayCommand(a => IrElegirCentroTrabajo());
            }
        }

        public ICommand ElegirNivelCambio
        {
            get
            {
                return new RelayCommand(a => IrElegirNivelCambio());
            }
        }

        public ICommand CerrarVentana
        {
            get
            {
                return new RelayCommand(a => cerrarVentana());
            }
        }

        public ICommand GuardarDatos
        {
            get
            {
                return new RelayCommand(a => GuardarDatosLeccion());
            }
        }

        public ICommand _AdjuntarArchivo
        {
            get
            {
                return new RelayCommand(o => AdjuntarArchivo());
            }
        }

        public ICommand VerArchivo
        {
            get
            {
                return new RelayCommand(o => verArchivo(ArchivoSeleccionado));
            }
        }

        public ICommand EliminarArchivo
        {
            get
            {
                return new RelayCommand(a => eliminarItem(ArchivoSeleccionado));
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que estando en la ventana de InsertarNuevaLeccion te dirige a la ventana de InformacionDescripcion
        /// </summary>
        public async void IrPagDescripcion()
        {
            DialogService dialog = new DialogService();

            bool CambiarPagina = PaginaDescripcion();
            if (CambiarPagina == true)
            {
                //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                var frm = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                //Verificamos que la pantalla sea diferente de nulo.
                if (frm != null)
                {
                    //Ocultamos la pantalla
                    frm.Close();
                }

                //Obtenemos la lista de los componentes similares y la mostramos
                ListaComponentesSimilares = DataManagerControlDocumentos.GetComponentesSimilares(_Componente);

                //Mostramos la siguiente pantalla
                InformacionDescripcion Form = new InformacionDescripcion();
                Form.DataContext = this;
                Form.ShowDialog();
            }else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }

        }

        /// <summary>
        /// Método que estando en la ventana de Información Descripcion te regresa a la ventana de InsertarNuevaLeccion
        /// </summary>
        public void RegPagNuevaLeccion()
        {
            var frm = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            if (frm != null)
            {
                frm.Close();
            }

            InsertarNuevaLeccion Form = new InsertarNuevaLeccion();
            Form.DataContext = this;
            Form.ShowDialog();
        }

        /// <summary>
        /// Método que estando en la ventana de Informacion Descripcion te dirige a la ventana de Informacion Cambios
        /// </summary>
        /// <returns></returns>
        public async void IrPagCambios()
        {
            DialogService dialog = new DialogService();

            //Mandamos llamar el método para verificar que no falte ningun campo de llenar
            bool CmbiarPagina = PaginaCambios();

            if (CmbiarPagina == true)
            {
                var frm = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                if (frm != null)
                {
                    frm.Close();
                }

                InformacionCambios Form = new InformacionCambios();
                Form.DataContext = this;
                Form.ShowDialog();
            }else
            {
                //si falta algun campo manda un mensaje y no se puede avanzar a la siguiente página
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }

        }

        /// <summary>
        /// Método que estando en la ventana de InformacionCambios te regresa a la ventana de InformacionDescripcion
        /// </summary>
        public void RegPagInformacionCambios()
        {
            var frm = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            if (frm != null)
            {
                frm.Close();
            }

            InformacionDescripcion Form = new InformacionDescripcion();
            Form.DataContext = this;
            Form.ShowDialog();
        
        }

        /// <summary>
        /// Método que despliega la ventana para elegir los centros de trabajo
        /// </summary>
        public void IrElegirCentroTrabajo()
        {
            CentrosDeTrabajo Form = new CentrosDeTrabajo();

            Form.DataContext = this;
            Form.ShowDialog();

            ListaCentrosDeTrabajoSeleccionados.Clear();

            foreach (var item in ListaCentrosDeTrabajo)
            {
                if (item.IsSelected)
                {
                    //Mostramos en la lista los Centros de trabajo que se hayan seleccionado en la vista anterior
                    ListaCentrosDeTrabajoSeleccionados.Add(item);
                }
            }
        }

        /// <summary>
        /// Método que despliega la ventana para elegit los niveles de cambio
        /// </summary>
        public void IrElegirNivelCambio()
        {
            TiposDeCambio Form = new TiposDeCambio();

            Form.DataContext = this;
            Form.ShowDialog();

            ListaNivelesDeCambioSeleccionados.Clear();
            foreach (var item in ListaNivelesDeCambio)
            {
                if (item.IsSelected)
                {
                    //Mostramos en la lista los niveles de cambio que se hayan seleccionado en la vista anterior
                    ListaNivelesDeCambioSeleccionados.Add(item);
                }
            }
        }

        /// <summary>
        /// Método que obtiene un vector con los centros de trabajo seleccionados
        /// </summary>
        /// <returns></returns>
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
                                Archivo.ruta = @"/Images/w.png";
                                
                                break;
                            case ".docx":
                                Archivo.ruta = @"/Images/w.png";
                                
                                break;
                            case ".pdf":
                                Archivo.ruta = @"/Images/p.png";
                                
                                break;
                            case ".xls":
                                Archivo.ruta = @"/Images/E.jpg";
                                
                                break;
                            case ".xlsx":
                                Archivo.ruta = @"/Images/E.jpg";
                                
                                break;
                            case "ppt":
                                Archivo.ruta = @"/Images/PP.png";
                                
                                break;
                                //todos los archivos que no tengan alguna de las extenciones antes mencionadas entraran aqui y se les pondra una imagen general
                            default:
                                Archivo.ruta = @"/Images/I.png";                                
                                break;
                        }
                        //Agregamos el archivo a la lista, SOLO SE AGREGA LOCALMENTE, AUN NO LO INSERTAMOS EN LA BASE DE DATOS
                        ListaDocumentos.Add(Archivo);

                        //Cerramos el mensaje de espera
                        await AsyncProgress.CloseAsync();

                    }else
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
        /// Método para visualizar un archivo seleccionado
        /// </summary>
        /// <param name="item"></param>
        private async void verArchivo(Archivo_LeccionesAprendidas item)
        {
            DialogService dialog = new DialogService();

            if (item != null)
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

                //Ejecutamos el método para mostrar el mensaje
                MessageDialogResult result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgDelArchivo, setting, MessageDialogStyle.AffirmativeAndNegative);

                //Entramos aqui si el archivo seleccionado es diferente de nulo y si el usuario ha seleccioando la opción de si
                if (item != null & result == MessageDialogResult.Affirmative)
                {
                    //Se elimina el item seleccionado de la listaDocumentos.
                    ListaDocumentos.Remove(item);

                    //Verificamos si el archivo aun se encuentra guardado localmente o ya se encuentra en la base de datos.
                    //si ya se encuentra en la base de datos entra al IF
                    if (item.ID_ARCHIVO_LECCIONES != 0 && item.ID_LECCIONES_APRENDIDAS != 0)
                    {
                        //Lo eliminados de la base de datos
                        int n = DataManagerControlDocumentos.Delete_Archivo_Lecciones(item.ID_ARCHIVO_LECCIONES);

                        //verificamos que el archivo se haya eliminado correctamente
                        if (n > 0)
                        {
                            //si el archivo se elimino correctamente mandamos un mensaje de confirmación
                            await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgArchivoEliminadoCorrectamente);
                        }
                        else
                        {
                            //si no se elimino correctamente mandamos un mensaje notificando el error
                            await dialogService.SendMessage(StringResources.msgError, StringResources.msgArchivoEliminadoFallido);
                        }
                    }
                    //nos vamos a este ELSE si el archivo solo encontraba guardado localmente
                    else
                    {
                        //si el archivo se removio de la lista de documentos correctamente mandamos un mensaje de confirmacion
                        await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgArchivoEliminadoCorrectamente);
                    }
                }
            }
        }

        /// <summary>
        /// Método para guardar los datos de la leccion aprendida en la base de datos
        /// </summary>
        public async void GuardarDatosLeccion()
        {

            DialogService dialog = new DialogService();
            bool Guardar = GuardarInformacion();

            if (Guardar == true)
            {
                //Declaramos una variable de tipo Lecciones aprendidas que usaremos para guardar los datos
                LeccionesAprendidas ObjLec = new LeccionesAprendidas();
                Archivo_LeccionesAprendidas ObjArc = new Archivo_LeccionesAprendidas();
                LeccionesTipoCambio ObjTipo = new LeccionesTipoCambio();
                LeccionesCentroTrabajo ObjCentros = new LeccionesCentroTrabajo();
                
                //Obtenemos los datos capturados
                ObjLec.COMPONENTE = _Componente;
                ObjLec.CAMBIO_REQUERIDO = _CambioRequerido;
                ObjLec.DESCRIPCION_PROBLEMA = _DescripcionProblema;
                ObjLec.FECHA_ULTIMO_CAMBIO = _FechaUltimoCambio;
                ObjLec.FECHA_ACTUALIZACION = _FechaActualizacion;
                ObjLec.REPORTADO_POR = _ReportadoPor;
                ObjLec.SOLICITUD_DE_TRABAJO = _SolicitudTrabajoIng;
                ObjLec.ID_USUARIO = _usuario;

                //Mandamos llamar el metodo para insertar la nueva leccion. Nos regresara el id de la leccion insertada y con eso podremos insertar los centros de trabajo seleccionados y los tipos de cambios
                int Id_Leccion_Aprendida_Insertada = DataManagerControlDocumentos.InsertLeccion(ObjLec.COMPONENTE,ObjLec.CAMBIO_REQUERIDO,ObjLec.DESCRIPCION_PROBLEMA,ObjLec.FECHA_ULTIMO_CAMBIO,ObjLec.FECHA_ACTUALIZACION,ObjLec.REPORTADO_POR,ObjLec.SOLICITUD_DE_TRABAJO,ObjLec.ID_USUARIO);

                if (Id_Leccion_Aprendida_Insertada != 0)
                {
                    //Obtenemos los datos de los centros de trabajo seleccionados
                    foreach (var item in ListaCentrosDeTrabajoSeleccionados)
                    {
                        ObjCentros.ID_CENTROTRABAJO = item.CentroTrabajo;
                        ObjCentros.ID_LECCIONESAPRENDIDAS = Id_Leccion_Aprendida_Insertada;

                        //Mandamos llamar el metodo que los inserta en la base de datos
                        int i = DataManagerControlDocumentos.InsertLeccionesCentroDeTrabajo(ObjCentros.ID_CENTROTRABAJO, ObjCentros.ID_LECCIONESAPRENDIDAS);
                    }

                    //Obtenemos los datos de los tipos de cambios seleccionados
                    foreach (var item in ListaNivelesDeCambioSeleccionados)
                    {
                        ObjTipo.ID_TIPO_CAMBIO = item.ID_TIPOCAMBIO;
                        ObjTipo.ID_LECCIONAPRENDIDA = Id_Leccion_Aprendida_Insertada;

                        //Mandamos llamar el metodo que los inserta en la base de datos
                        int i = DataManagerControlDocumentos.InsertLeccionesNivelCambio(ObjTipo.ID_TIPO_CAMBIO, ObjTipo.ID_LECCIONAPRENDIDA);
                    }

                    //Obtenemos los datos de los archivos adjuntados
                    foreach (var item in ListaDocumentos)
                    {
                        ObjArc.ID_LECCIONES_APRENDIDAS = Id_Leccion_Aprendida_Insertada;
                        ObjArc.ARCHIVO = item.ARCHIVO;
                        ObjArc.NOMBRE_ARCHIVO = item.NOMBRE_ARCHIVO;
                        ObjArc.EXT = item.EXT;

                        //Mandamos llamar el metodo que inserta todos los documentos que se hayan adjuntado
                        int i = await DataManagerControlDocumentos.SetArchivo_Lecciones(ObjArc.ARCHIVO, ObjArc.EXT, ObjArc.NOMBRE_ARCHIVO, ObjArc.ID_LECCIONES_APRENDIDAS);
                    }

                    //Mandamos un mensaje de que se guardaron todos los datos correctamente
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertoExitoLeccion);

                    //Cerramos la ventana
                    var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                    if (window != null)
                    {
                        //cerramos la ventana 
                        window.Close();
                    }
                }
                else
                {
                    //Si ocurrio un error al insertar la leccion aprendida se notificara al usuario
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                }
            }
            else
            {
                //si aun faltan datos por capturar manda la notificacion de que se tienen que capturar todos
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Función que comprueba que no falte ningun dato para seguir a la siguiente páguina de InformacionDescripcion
        /// </summary>
        /// <returns></returns>
        public bool PaginaDescripcion()
        {
            if (!string.IsNullOrEmpty(_Componente) && !string.IsNullOrEmpty(_CambioRequerido) && _ListaNivelesDeCambioSeleccionados.Count > 0 && _ListaCentrosDeTrabajoSeleccionados.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Función que comprueba que no falte ningun dato para seguir a la siguiente página de InformacionCambios
        /// </summary>
        /// <returns></returns>
        public bool PaginaCambios()
        {
            if (!string.IsNullOrEmpty(_DescripcionProblema) && _FechaActualizacion != null && _FechaUltimoCambio != null) 
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Función que comprueba que no falte ningun dato para guarda los datos de la leccion aprendida
        /// </summary>
        /// <returns></returns>
        public bool GuardarInformacion()
        {
            if (!string.IsNullOrEmpty(_ReportadoPor) && !string.IsNullOrEmpty(_SolicitudTrabajoIng))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Función que genera una cadena para cargar un archivo en la carpeta temporal del sistema
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
                filename = Path.Combine(tempFolder, item.NOMBRE_ARCHIVO + aleatorio + item.EXT);
            } while (File.Exists(filename));

            //retornamos el nombre que se generó
            return filename;
        }

        #endregion
    }
}