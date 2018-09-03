﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Encriptar;
using Model.ControlDocumentos;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using View.Forms.ControlDocumentos;
using System.Collections;
using View.Resources;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace View.Services.ViewModel
{
    public class DocumentoViewModel : INotifyPropertyChanged
    {

        #region Attributes
        UsuariosViewModel vmUsuarios;
        private int idVersion;
        // variables auxiliar, guarda la información  cuando se genera una nueva versión
        private string auxversion, auxUsuario, auxUsuario_Autorizo, auxDescripcion;
        private DateTime auxFecha;
        public Usuario User;
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

        #region Propiedades

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

        private DateTime fecha = DataManagerControlDocumentos.Get_DateTime();
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

        private string _id_areasealed;
        public string id_areasealed
        {
            get { return _id_areasealed; }
            set { _id_areasealed = value; NotifyChange("id_areasealed"); }
        }

        private string _usuario;
        public string usuario {
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

        private bool _notificar;
        public bool notificar
        {
            get
            {
                return _notificar;
            }
            set
            {
                _notificar = value;
                NotifyChange("notificar");
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

        private ObservableCollection<Usuarios> _ListaUsuariosCorreo;
        public ObservableCollection<Usuarios> ListaUsuariosCorreo
        {
            get
            {
                return _ListaUsuariosCorreo;
            }
            set
            {
                _ListaUsuariosCorreo = value;
                NotifyChange("ListaUsuariosCorreo");
            }
        }

        private ObservableCollection<FO_Item> _ListaAreasSealed;
        public ObservableCollection<FO_Item> ListaAreasSealed
        {
            get
            {
                return _ListaAreasSealed;
            }
            set
            {
                _ListaAreasSealed = value;
                NotifyChange("ListaAreasSealed");
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

        private bool nombreEnabled = false;
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

        private int _NoCopias;
        public  int NoCopias
        {
            get
            {
                return _NoCopias;
            }
            set
            {
                _NoCopias = value;
                NotifyChange("NoCopias");
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

        private bool _isEnabled = true;
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

        private bool _EnabledFecha = false;
        public bool EnabledFecha
        {
            get
            {
                return _EnabledFecha;
            }
            set
            {
                _EnabledFecha = value;
                NotifyChange("EnabledFecha");
            }
        }

        private DateTime _FechaFin;
        public DateTime FechaFin {
            get
            {
                return _FechaFin;
            }
            set
            {
                _FechaFin = value;
                NotifyChange("FechaFin");
            }
        }

        private int widthButton = 0;
        public int WidthButton { get { return widthButton; } set { widthButton = value; NotifyChange("WidthButton"); } }

        //Variables para guardar la información del documento, y mostar mensaje de confirmación
        private string NombreUsuarioElaboro, NombreUsuarioAut, NombreTipo, NombreDepto, auxNomUsElaboro;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor para generar una versión, o modificar el documento
        /// </summary>
        /// <param name="selectedDocumento"></param>
        /// <param name="band"></param>
        /// <param name="ModelUsuario"></param>
        public DocumentoViewModel(Documento selectedDocumento, bool band, Usuario ModelUsuario)
        {
            //Variables auxiliar guarda la información para cuando se genera una versión nueva 
            //Inicializa los combobox 
            Inicializar();
            //Asiganmos los valores para que se muestren 
            User = ModelUsuario;
            Nombre = selectedDocumento.nombre;
            Version = selectedDocumento.version.no_version;
            Fecha = selectedDocumento.fecha_actualizacion;
            auxFecha = Fecha;
            auxversion = selectedDocumento.version.no_version;
            Descripcion = selectedDocumento.descripcion;
            auxDescripcion = Descripcion;
            NoCopias = selectedDocumento.version.no_copias;
            id_documento = selectedDocumento.id_documento;
            idVersion = selectedDocumento.version.id_version;
            id_dep = selectedDocumento.id_dep;
            NombreDepto = selectedDocumento.Departamento;
            //Obtenemos la fecha del servidor
            FechaFin = DataManagerControlDocumentos.Get_DateTime();
            id_tipo = DataManagerControlDocumentos.GetTipoDocumento(id_documento);
            NombreTipo = DataManagerControlDocumentos.GetNombretipo(id_tipo);


            BotonGuardar = StringResources.ttlGuardar;
            BttnGuardar = false;
            EnabledEliminar = false;
            IsEnabled = false;
            BttnArchivos = true;
            //Si es ventana para generar una nueva versión, band= true
            BttnVersion = band;

            //si es personal del CIT y es ventana para generar una nueva versión
            if (Module.UsuarioIsRol(User.Roles, 2))
            {
                BttnModificar = true;
                BttnEliminar = band;
                IsEnabled = true;
                EnabledEliminar = true;
                EnabledFecha = true;
                VersionEnabled = true;
            }

            //si es ventana para corregir documento con estatus pendiente por corregir.
            if (band == false)
            {
                IsEnabled = true;
                BttnModificar = true;
                EnabledEliminar = true;
                EnabledFecha = false;
                Fecha = selectedDocumento.version.fecha_version;
                //Si es administrador del CIT muestra la fecha.
                if (Module.UsuarioIsRol(User.Roles, 2))
                    EnabledFecha = true;
            }

            //Obtiene el nombre de documento del id
            _ListaNumeroDocumento = DataManagerControlDocumentos.GetNombre_Documento(id_documento);

            if (_ListaNumeroDocumento.Count > 0)
                SelectedDocumento = _ListaNumeroDocumento[0];

            //Obtenemos el usuario que autorizó y el usuario que dio de alta
            Model.ControlDocumentos.Version UsuarioObj = DataManagerControlDocumentos.GetIdUsuario(idVersion);
            usuario = UsuarioObj.id_usuario;
            auxUsuario = usuario;
            //Variable para mostrar en mensaje de comprobación
            NombreUsuarioElaboro = UsuarioObj.nombre_usuario_elaboro;
            auxNomUsElaboro = NombreUsuarioElaboro;

            //información del usuario que autorizó, se guarda en variables auxiliar para mostrar en mensaje
            usuarioAutorizo = UsuarioObj.id_usuario_autorizo;
            auxUsuario_Autorizo = usuarioAutorizo;
            NombreUsuarioAut = UsuarioObj.nombre_usuario_autorizo;

            //Método que obtiene los archivos de un documento y de la versión
            ObservableCollection<Documento> Lista = DataManagerControlDocumentos.GetArchivos(id_documento, idVersion);

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

        /// <summary>
        /// Constructor para crear un nuevo documento
        /// </summary>
        /// <param name="ModelUsuario"></param>
        public DocumentoViewModel(Usuario ModelUsuario)
        {
            BotonGuardar = StringResources.ttlGuardar;
            BttnGuardar = true;
            BttnArchivos = true;
            EnabledEliminar = true;
            NombreEnabled = true;
            Version = "1";
            User = ModelUsuario;
            usuario = User.NombreUsuario;
            NombreUsuarioElaboro = User.Nombre + " " + User.ApellidoPaterno;
            auxUsuario = usuario;
            //Obtenemos la fecha del servidor
            FechaFin = DataManagerControlDocumentos.Get_DateTime();

            //Obetenemos la lista de los documentos sin versión del usuario
            ListaNumeroDocumento = DataManagerControlDocumentos.GetDocumento_SinVersion(User.NombreUsuario);

            //si es personal del CIT, la campo de fecha es editable
            if (Module.UsuarioIsRol(User.Roles, 2))
            {
                EnabledFecha = true;
                VersionEnabled = true;
            }

            //Mostramos el primer documento, sólo se admite un documento sin version por usuario
            //if (ListaNumeroDocumento.Count > 0)
            //    SelectedDocumento = ListaNumeroDocumento[0];

            //Inicializamos los campos de tipo de documento y departamento
            //id_dep = _selectedDocumento.id_dep;
            //id_tipo = _selectedDocumento.id_tipo_documento;
            //nombre = _selectedDocumento.nombre;
            //NombreDepto = _selectedDocumento.Departamento;
            //NombreTipo = _selectedDocumento.tipo.tipo_documento;

            Inicializar();
        }

        /// <summary>
        /// Contructor para liberar documentos
        /// </summary>
        /// <param name="selectedDocumento"></param>
        /// <param name="ModelUsuario"></param>
        public DocumentoViewModel(Documento selectedDocumento, Usuario ModelUsuario)
        {
            User = ModelUsuario;
            Encriptacion des = new Encriptacion();

            //Inicializa los combobox 
            Inicializar();
            IsEnabled = false;
            EnabledEliminar = false;
            BttnArchivos = false;
            BttnLiberar = true;
            WidthButton = 155;
            //si el usuario es administrador del sistema
            //se permite modificar el usuario autorizo
            if (Module.UsuarioIsRol(User.Roles, 2))
            {
                IsEnabled = true;
            }

            //Asiganmos los valores para que se muestren 
            Nombre = selectedDocumento.nombre;
            User = ModelUsuario;
            Version = selectedDocumento.version.no_version;
            //Obetenemos la fecha del servidor
            FechaFin = DataManagerControlDocumentos.Get_DateTime();
            Fecha = selectedDocumento.version.fecha_version;
            Descripcion = selectedDocumento.descripcion;
            id_documento = selectedDocumento.id_documento;
            idVersion = selectedDocumento.version.id_version;
            id_dep = selectedDocumento.id_dep;

            //Obtenemos el tipo de documento
            id_tipo = DataManagerControlDocumentos.GetTipoDocumento(id_documento);

            //si es personal del CIT, el campo de usuario elaboro es editable.
            if (Module.UsuarioIsRol(User.Roles, 2))
                VersionEnabled = true;

            //Inicializamos la lista de Areas del sistema frames.
            ListaAreasSealed = new ObservableCollection<FO_Item>();

            //Llenamos la lista de las áreas y si es una versión superior a 1, obtenemos el área a la cual esta asignada en el sistema frames.
            switch (id_tipo)
            {
                case 1003:
                case 1013:
                    ListaAreasSealed = DataManagerControlDocumentos.GetAllAreasOHSAS();
                    if (!Module.IsNumeric(Version) || (Module.IsNumeric(Version) && Convert.ToInt32(Version) > 1))
                    {
                        id_areasealed = DataManagerControlDocumentos.GetIdAreaOHSAS(Nombre);
                    }
                    else
                    {
                        id_areasealed = "0";
                    }
                    break;

                case 1005:
                case 1012:
                    ListaAreasSealed = DataManagerControlDocumentos.GetAllAreasEspecifico();
                    if (!Module.IsNumeric(Version) || (Module.IsNumeric(Version) && Convert.ToInt32(Version) > 1))
                    {
                        id_areasealed = DataManagerControlDocumentos.GetIdAreaEspecifico(Nombre);
                    }
                    else
                    {
                        id_areasealed = "0";
                    }
                    break;

                case 1006:
                case 1014:
                    ListaAreasSealed = DataManagerControlDocumentos.GetAllAreasISO();
                    if (!Module.IsNumeric(Version) || (Module.IsNumeric(Version) && Convert.ToInt32(Version) > 1))
                    {
                        id_areasealed = DataManagerControlDocumentos.GetIdAreaISO(Nombre);
                    }
                    else
                    {
                        id_areasealed = "0";
                    }
                    break;

                default:
                    break;
            }


            //obtenemos el nombre del documento
            _ListaNumeroDocumento = DataManagerControlDocumentos.GetNombre_Documento(id_documento);

            //Se muestra en el combobox
            if (_ListaNumeroDocumento.Count > 0)
                SelectedDocumento = _ListaNumeroDocumento[0];

            //Obtenemos el usuario que autorizó y el usuario que elabroró
            Model.ControlDocumentos.Version UsuarioObj = DataManagerControlDocumentos.GetIdUsuario(idVersion);
            usuario = UsuarioObj.id_usuario;
            usuarioAutorizo = UsuarioObj.id_usuario_autorizo;
            //obtenemos la lista de los usuarios
            ListaUsuariosCorreo = DataManagerControlDocumentos.GetUsuarios();
            
            //iteramos la lista
            //para seleciconar los usuarios a notificar al momento de abrirse la ventana
            foreach (var item in ListaUsuariosCorreo)
            {
                //sleccionamos el administrado del sistema para notificar
                if (item.usuario == "¢¥®ª¯")
                {
                    item.IsSelected = true;
                }
                //seleccionamos al usuario que elaboro
                if (item.usuario == usuario)
                {
                    item.IsSelected = true;
                }
                //seleccionamos al usuario que autorizo
                if (item.usuario == usuarioAutorizo)
                {
                    item.IsSelected = true;
                }
            }
            
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
        /// Comando para cerrar la pantalla
        /// </summary>
        public ICommand cerrar
        {
            get
            {
                return new RelayCommand(o => CerrarVentana());
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

        /// <summary>
        /// Comando para Actualizar el numero de copias de un documento
        /// </summary>
        public ICommand ActNoCopias
        {
            get
            {
                return new RelayCommand(o => ActualizarNumCopias());
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
        /// 
        /// </summary>
        public ICommand RegresarCorregir
        {
            get
            {
                return new RelayCommand(o => regresarCorregir());
            }
        }

        /// <summary>
        /// Comando para liberar un documento
        /// </summary>
        /// 
        public ICommand LiberarDocumento
        {
            get
            {
               return  new RelayCommand(o => liberarDocumento());
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
        /// Comando para obtener el nombre completo del usuario que autorizo
        /// </summary>
        public ICommand CambiarUsuario
        {
            get
            {
                return new RelayCommand(o => getUsuarioAutorizo());
            }
        }
        /// <summary>
        /// Comando para sellar electronicamente un documento las copias.
        /// 
        /// </summary>
        public ICommand SellarDocumento
        {
            get
            {
                return new RelayCommand(o => SellarCopiasDocumentos());
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Método que obtiene el nombre completo del usuario de acuerdo al id
        /// Se guarda el nombre completo para mostrar en mensaje de la información del documento
        /// </summary>
        private void getUsuarioAutorizo()
        {
            if (usuarioAutorizo !=null)
            {
                NombreUsuarioAut = DataManagerControlDocumentos.GetNombreUsuario(usuarioAutorizo);
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
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgDelArchivo, setting, MessageDialogStyle.AffirmativeAndNegative);

                if (item != null & result == MessageDialogResult.Affirmative)
                {
                    //Se elimina el item seleccionado de la listaDocumentos.
                    ListaDocumentos.Remove(item);
                    //Se elimina de la base de datos.
                    int n = DataManagerControlDocumentos.DeleteArchivo(item);
                    BttnArchivos = true;
                }
            }
        }

        /// <summary>
        /// Método para cambiar el estadus de un archivo a pendiente por corregir
        /// </summary>
        private async void regresarCorregir()
        {
            DialogService dialog = new DialogService();

            Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgRegresarCorregir, setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                //Ejecutamos el método para obtener el id de la versión anterior
                int last_version = DataManagerControlDocumentos.GetID_LastVersion(id_documento, idVersion);

                //si el documento sólo tiene una versión, se modifica el estatus del documento y la versión, se cambia el estatus a pendiente por corregir
                if (last_version == 0)
                {
                    Documento objDocumento = new Documento();
                    //Asiganmos el id del documento al objeto
                    objDocumento.id_documento = id_documento;
                    //Estatus de documento pendiente por corregir
                    objDocumento.id_estatus = 3;

                    //Ejecutamos el método para actualizar el estatus del documento.
                    int update_documento = DataManagerControlDocumentos.Update_EstatusDocumento(objDocumento);

                    if (update_documento != 0)
                    {
                        //Asigamos los valores
                        objVersion.id_version = idVersion;
                        objVersion.no_version = version;
                        objVersion.id_documento = id_documento;
                        objVersion.id_usuario = _usuario;
                        objVersion.id_usuario_autorizo = _usuarioAutorizo;
                        objVersion.fecha_version = fecha;
                        objVersion.id_estatus_version = 4;
                        objVersion.no_copias = 0;
                        objVersion.descripcion_v = Descripcion;

                        //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                        int update_version = DataManagerControlDocumentos.UpdateVersion(objVersion, User, nombre);

                        if (update_version != 0)
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusPendienteCorregir);

                            var frame = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                            //Verificamos que la pantalla sea diferente de nulo.
                            if (frame != null)
                                //Cerramos la pantalla
                                frame.Close();
                        }
                        else
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusVersion);
                        }
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusDocumento);
                    }
                }
                else
                {
                    //si el documento tiene más de un versión, sólo se modifica el estatus de la versión a pendiente por corregir

                    objVersion.id_version = idVersion;
                    objVersion.no_version = version;
                    objVersion.id_documento = id_documento;
                    objVersion.id_usuario = _usuario;
                    objVersion.id_usuario_autorizo = _usuarioAutorizo;
                    objVersion.fecha_version = fecha;
                    objVersion.id_estatus_version = 4;
                    objVersion.no_copias = 0;
                    objVersion.descripcion_v = Descripcion;

                    //Ejecutamos el método para modificar el estatus de la versión. El resultado lo guardamos en una variable local.
                    int update_version = DataManagerControlDocumentos.UpdateVersion(objVersion, User, nombre);

                    if (update_version != 0)
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusPendienteCorregir);

                        var frame = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                        //Verificamos que la pantalla sea diferente de nulo.
                        if (frame != null)
                            //Cerramos la pantalla
                            frame.Close();
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusVersion);
                    }
                }
            }
        }

        /// <summary>
        /// Método que cancela cuando se genera una nueva versión, muestra la información de la versión anterior
        /// </summary>
        private void cancelar()
        {
            usuario = auxUsuario;
            NombreUsuarioElaboro = auxNomUsElaboro;
            usuarioAutorizo = auxUsuario_Autorizo;
            Descripcion = auxDescripcion;
            Version = auxversion;
            Fecha = auxFecha;
            BotonGuardar = StringResources.ttlGuardar;
            IsEnabled = false;
            BttnArchivos = false;
            EnabledEliminar = false;
            ListaDocumentos.Clear();

            //Si es administrador del CIT
            if (Module.UsuarioIsRol(User.Roles, 2))
            {
                BttnEliminar = true;
                BttnModificar = true;
                IsEnabled = true;
                EnabledEliminar = true;
                // ListaValidaciones = DataManagerControlDocumentos.GetValidacion_Documento(id_tipo);
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
        /// Método que muestra el tipo de documento y el departamento de acuerdo al número de documento
        /// Llena la lista de validaciones de acuerdo al tipo de documento
        /// </summary>
        private void cambiarCombo()
        {
            //Si se seleccionó un documento
            if (_selectedDocumento != null)
            {
                id_dep = _selectedDocumento.id_dep;
                id_tipo = _selectedDocumento.id_tipo_documento;
                nombre = _selectedDocumento.nombre;
                NombreDepto = _selectedDocumento.Departamento;
                NombreTipo = _selectedDocumento.tipo.tipo_documento;
                //ListaValidaciones = DataManagerControlDocumentos.GetValidacion_Documento(id_tipo);
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
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgVersionAnterior, setting, MessageDialogStyle.AffirmativeAndNegative);
            //
            if (result == MessageDialogResult.Affirmative)
            {
                //Obtiene el id de la última versión

                int last_id = DataManagerControlDocumentos.GetID_LastVersion(id_documento, idVersion);
                //Si tiene una versión anterior
                if (last_id != 0)
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
                    objVersion.no_version = version;

                    string mensaje = "Se elimina versión " + Version + ",Se regresa a versión anterior";

                    //Mandamos a llamar a la  función para eliminar la versión.
                    int delete_version = DataManagerControlDocumentos.DeleteVersion(objVersion, mensaje, User, nombre);

                    if (delete_version != 0)
                    {
                        //Creamos un objeto para la versión anterior 
                        Model.ControlDocumentos.Version lastVersion = new Model.ControlDocumentos.Version();

                        //asigamos el id de la version anterior, cambiamos el estatus a liberado
                        lastVersion.id_version = last_id;
                        lastVersion.id_estatus_version = 1;
                        lastVersion.no_version = DataManagerControlDocumentos.GetNum_Version(last_id);

                        //Ejecutamos el método para actualizar el estatus de la versión.
                        int update = DataManagerControlDocumentos.Update_EstatusVersion(lastVersion, User, nombre);

                        if (update != 0)
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosRealizados);

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
                            await dialog.SendMessage(StringResources.msgError, StringResources.msgErrorBorrarDocumento);
                        }
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.msgError, StringResources.msgErrorBorrarVersion);
                    }

                }
                else
                {
                    await dialog.SendMessage(StringResources.msgError, StringResources.msgErrorVersion);
                }
            }
        }

        /// <summary>
        /// Método que llena la lista para visualizar los archivos de la versión
        /// </summary>
        private async void AdjuntarArchivo()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController AsyncProgress;


            if (_selectedDocumento == null)
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSeleccioneArchivo);
            }
            else
            {

                //Si la lista no tiene otro archivo adjunto
                if (ListaDocumentos.Count == 0)
                {
                    //Abre la ventana de explorador de archivos
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                    //Filtar los documentos por extensión 
                    //Si es procedimiento o formatos, sólo mostrar documentos word
                    if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014)
                        dlg.Filter = "Word (97-2003)|*.doc";
                    else
                        dlg.Filter = "PDF Files (.pdf)|*.pdf";
                    // Mostrar el explorador de archivos
                    Nullable<bool> result = dlg.ShowDialog();

                    //Se crea el objeto de tipo archivo
                    Archivo obj = new Archivo();

                    // Si fue seleccionado un documento 
                    if (result == true)
                    {
                        try
                        {
                            //Se obtiene el nombre del documento
                            string filename = dlg.FileName;

                            //Si el archivo no está en uso
                            if (!IsFileInUse(filename))
                            {
                                //Ejecutamos el método para enviar un mensaje de espera mientras se comprueban los datos.
                                AsyncProgress = await dialog.SendProgressAsync(StringResources.msgEspera, StringResources.msgInsertando);

                                //Se convierte el archvio a tipo byte y se le asigna al objeto
                                obj.archivo = await Task.Run(() => File.ReadAllBytes(filename));

                                //Obtiene la extensión del documento y se le asigna al objeto
                                obj.ext = System.IO.Path.GetExtension(filename);

                                //Se obtiene sólo el nombre, sin extensión.
                                obj.nombre = System.IO.Path.GetFileNameWithoutExtension(filename);

                                obj.numero = ListaDocumentos.Count + 1;

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

                                //consultamos de que tipo es el archivo
                                if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014)
                                {
                                    //si el archivo es igual a cualquiera de los id anteriores se comprueba que sea un archivo .doc
                                    if (obj.ext == ".doc")
                                    {
                                        ListaDocumentos.Add(obj);
                                        BttnArchivos = false;
                                    }
                                    //si no es archivo .doc se manda un mensaje y se elimina
                                    else
                                    {
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertarTipoArchivoDOC);
                                        Lista.Clear();
                                        BttnArchivos = true;
                                    }
                                }
                                else
                                {
                                    //cualquier id que sea diferente de los anteriores tiene que ser un archivo .pdf
                                    if (obj.ext == ".pdf")
                                    {
                                        ListaDocumentos.Add(obj);
                                        BttnArchivos = false;
                                    }
                                    else
                                    {
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertarTipoArchivoPDF);
                                        Lista.Clear();
                                        BttnArchivos = true;
                                    }
                                }

                                //Ejecutamos el método para cerrar el mensaje de espera.
                                await AsyncProgress.CloseAsync();
                            }
                            else
                            {
                                //Si el archivo está abierto
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                            }
                        }
                        catch (IOException er)
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                        }
                    }
                }
                else
                {
                    //Si tiene un archivo adjunto, se muestra el mensaje
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertarUnoSolo);
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
            if (objBloqueo.id_bloqueo == 0 || Module.UsuarioIsRol(User.Roles, 2))
            {
                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgGenerarVersion, setting, MessageDialogStyle.AffirmativeAndNegative);

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
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgNuevaVersion + " " + Version);

                        //Limpiamos todos lo textbox, y se cambia el content del botón de guardar.
                        Fecha = _FechaFin;
                        ListaDocumentos.Clear();
                        // ListaValidaciones = DataManagerControlDocumentos.GetValidacion_Documento(id_tipo);

                        //Oculta y muestra los botones
                        BotonGuardar = StringResources.ttlGuardarVersion;
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
                        NombreUsuarioElaboro = User.Nombre + " " + User.ApellidoPaterno;
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
                        await dialog.SendMessage(StringResources.msgErrorCrearVersion, StringResources.msgNumeroVersion +" "+  obj.no_version +" "+" "+ StringResources.msgEstado +" "+ obj.estatus);
                    }
                }
            }
            else
            {
                //El sistema se encuentra bloqueado
                await dialog.SendMessage(StringResources.msgSistemaBloqueado, objBloqueo.observaciones);
            }
        }

        /// <summary>
        /// Método que retorna un true si todos los campos contienen un valor.
        /// </summary>
        /// <returns></returns>
        private bool ValidarValores()
        {
            if (nombre != null & version != null & fecha != null & !string.IsNullOrEmpty(descripcion) & id_tipo != 0 & _ListaDocumentos.Count != 0 & _usuario!=null & _id_dep!=0 & usuarioAutorizo!=null & !string.IsNullOrWhiteSpace(descripcion))
                return true;
            else 
                return false;
        }

        /// <summary>
        /// Método para liberar un documento, modifica el número de copias del documento y el estatus de la versión
        /// </summary>
        private async void liberarDocumento()
        {
            DialogService dialog = new DialogService();

            //Obtenemos la ventana actual
            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            //comprobamos que se haya seleccionado un area frames para poder insertarlo
            if(id_areasealed == "0" && (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014))
            {
                //si no se selecciono el area, no se libera el documento
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblInsertarAreaFrames);
            }
            else
            { 
                //Formulario para ingresar el número de copias, 
                string num_copias = await window.ShowInputAsync(StringResources.msgIngNumeroCopias, StringResources.msgNumeroCopias, null);
                //Comprueba que el número de copias sea diferente de nulo y sólo contenga números.
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
                                    objVersion.descripcion_v = Descripcion;

                                    //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                                    int update_version = DataManagerControlDocumentos.UpdateVersion(objVersion, User, nombre);
                                    
                                    //Si la versión se actualizó correctamente.
                                    if (update_version != 0)
                                    {
                                        //Insertamos el sello electrónico a los archivos que apliquen.
                                        bool res = await SetElectronicStamp(objVersion);

                                        //Guardamos el documento si es procedimiento o formato
                                        string file = SaveFile();

                                        if (file == null)
                                        {
                                            int r = InsertDocumentoSealed();
                                            string confirmacionFrames = r > 0 ? StringResources.msgFramesExito : StringResources.msgFramesIncorrecto;
                                            string confirmacionCorreo = string.Empty;

                                            if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014)
                                            {
                                                if (NotificarNuevaVersion())
                                                    confirmacionCorreo = StringResources.msgNotificacionCorreo;
                                                else
                                                    confirmacionCorreo = StringResources.msgNotificacionCorreoFallida;

                                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgMatrizActualizada + "\n" + confirmacionFrames + "\n" + confirmacionCorreo);

                                            }
                                            else
                                            {
                                                confirmacionCorreo = string.Empty;
                                                
                                                if (NotificarDocumentoDisponibleConSello())
                                                    confirmacionCorreo = StringResources.msgNotificacionCorreo;
                                                else
                                                    confirmacionCorreo = StringResources.msgNotificacionCorreoFallida;

                                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgMatrizActualizada + "\n" + confirmacionCorreo);
                                            }

                                            //Creamos una notificación para que el usuario la pueda ver.
                                            DO_Notification notificacion = new DO_Notification();
                                            notificacion.TITLE = StringResources.msgDocumentoActualizado;
                                            notificacion.MSG = StringResources.msgDocumento + " " + Nombre + "\n Versión: " + version + "\n" + StringResources.msgNotificacionMatriz;
                                            notificacion.TYPE_NOTIFICATION = 0;
                                            notificacion.ID_USUARIO_RECEIVER = _usuario;
                                            notificacion.ID_USUARIO_SEND = StringResources.msgAdmin;

                                            DataManagerControlDocumentos.insertNotificacion(notificacion);

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
                                            //si falla al momento de liberar el documento se regres el estatus del documento a pendiente por aprobar
                                            objDocumento.id_estatus = 2;
                                            update_documento = DataManagerControlDocumentos.Update_EstatusDocumento(objDocumento);

                                            //si falla al momento de liberar el estatus de la version se regresa a aprobado, pendiente por liberar
                                            objVersion.id_estatus_version = 5;
                                            update_version = DataManagerControlDocumentos.UpdateVersion(objVersion, User, nombre);

                                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorGuardandoArchivo);
                                        }
                                    }
                                    else
                                    {

                                        objDocumento.id_estatus = 2;
                                        update_documento = DataManagerControlDocumentos.Update_EstatusDocumento(objDocumento);
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusVersionDocumento);
                                    }
                                }
                                else
                                {
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusDocumento);
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
                                objVersion.descripcion_v = Descripcion;

                                //Ejecutamos el método para modificar el estatus de la versión. El resultado lo guardamos en una variable local.
                                int update_version = DataManagerControlDocumentos.UpdateVersion(objVersion, User, nombre);

                                //si fue modificado correctamente.
                                if (update_version != 0)
                                {
                                    //Insertamos el sello electrónico a los archivos que apliquen.
                                    bool rest = await SetElectronicStamp(objVersion);

                                    //obetemos el id de la versión anterior
                                    int last_id = DataManagerControlDocumentos.GetID_LastVersion(id_documento, idVersion);

                                    //Creamos un objeto para la versión anterior 
                                    Model.ControlDocumentos.Version lastVersion = new Model.ControlDocumentos.Version();

                                    //asigamos el id y el estatus obsoleto
                                    lastVersion.id_version = last_id;
                                    lastVersion.id_estatus_version = 2;

                                    //Se obtienen el número de versión de la version anterior
                                    lastVersion.no_version = DataManagerControlDocumentos.GetNum_Version(last_id);

                                    //Ejecutamos el método para actualizar el estatus de la versión(liberamos el documento).
                                    int update = DataManagerControlDocumentos.Update_EstatusVersion(lastVersion, User, nombre);

                                    //si se actualizó correctamente
                                    if (update != 0)
                                    {
                                        //Guardamos el documento, si es procedimiento o formato
                                        string file = SaveFile();

                                        if (file == null)
                                        {
                                            int r = UpdateDocumentoSealed();
                                            string confirmacionFrames = r > 0 ? StringResources.msgFramesExito : StringResources.msgFramesIncorrecto;
                                            string confirmacionCorreo = string.Empty;

                                            if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014)
                                            {
                                                if (NotificarActualizacionVersion())
                                                    confirmacionCorreo = StringResources.msgNotificacionCorreo;
                                                else
                                                    confirmacionCorreo = StringResources.msgNotificacionCorreoFallida;

                                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgMatrizActualizada + "\n" + confirmacionFrames + "\n" + confirmacionCorreo);
                                            }
                                            else
                                            {
                                                confirmacionCorreo = string.Empty;

                                                if (NotificarDocumentoDisponibleConSello())
                                                    confirmacionCorreo = StringResources.msgNotificacionCorreo;
                                                else
                                                    confirmacionCorreo = StringResources.msgNotificacionCorreoFallida;

                                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgMatrizActualizada + "\n" + confirmacionCorreo);
                                            }

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
                                            //si falla al momento de liberar el documento se regresa el estatus de la version a liberado
                                            lastVersion.id_estatus_version = 1;
                                            update = DataManagerControlDocumentos.Update_EstatusVersion(lastVersion, User, nombre);

                                            //si falla al momneto de liberar el documento se regresa el estatus de la version a aprobado, pendiente por liberar.
                                            objVersion.id_estatus_version = 5;
                                            update_version = DataManagerControlDocumentos.UpdateVersion(objVersion, User, nombre);

                                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorGuardandoArchivo);
                                        }
                                    }
                                    else
                                    {
                                        //Si hubo error al actualizar el estatus de la última versión
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusVersion);
                                    }
                                }
                                else
                                {

                                    //Si hubo error al actualizar la última versión
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorActualizarVersion);
                                }
                            }
                        }
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCamposInvalidos);
                    }
                }
            }
        }

        /// <summary>
        /// Metodo que notifica via correo la nueva actualización a un documento
        /// </summary>
        /// <returns></returns>
        private bool NotificarActualizacionVersion()
        {
            ServiceEmail SO_Email = new ServiceEmail();
            string[] correos = new string[ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count];
            int i = 0;
            foreach (Usuarios item in ListaUsuariosCorreo)
            {
                if (item.IsSelected)
                {
                    correos[i] = item.Correo;
                    i += 1;
                }
            }
            string path = User.Pathnsf;
            string title = "Actualización de documento - " + Nombre;
            string body = string.Empty;
            string tipo_documento = string.Empty;
            string AreaFrames = string.Empty;

            switch (id_tipo)
            {
                case 1003:
                case 1013:
                    AreaFrames = DataManagerControlDocumentos.GetNombreAreaOHSAS(Convert.ToInt32(id_areasealed));
                    break;
                case 1014:
                case 1006:
                    AreaFrames = DataManagerControlDocumentos.GetNombreAreaISO(Convert.ToInt32(id_areasealed));
                    break;
                case 1005:
                case 1012:
                    AreaFrames = DataManagerControlDocumentos.GetNombreAreaESPECIFICOS(Convert.ToInt32(id_areasealed));
                    break;
                default:
                    break;
            }

            switch (id_tipo)
            {
                case 1003:
                case 1005:
                case 1006:
                    tipo_documento = "la instrucción de trabajo";
                    break;

                case 1012:
                case 1013:
                case 1014:
                    tipo_documento = "el formato";
                    break;
                default:
                    break;
            }
            body = "<HTML>";
            body += "<head>";
            body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
            body += "</head>";
            body += "<body text=\"white\">";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que " + tipo_documento + " con el número <b> " + Nombre + "</b> versión <b> " + Version + ".0" + " </b> ya se encuentra disponible en el sistema </font> <a href=\"http://sealed/frames.htm\">frames</a> </li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizo la matríz.</font></li>";
            body += "<br/>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Número : <b>" + Nombre + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Descripción : <b>" + Descripcion + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + Version +".0"+"</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Área del Frames en donde se inserto : <b>" + AreaFrames + "</b></font></li>";
            body += "</ul>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor no responda.</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Nombre + " " + User.ApellidoPaterno + "</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
            body += "<li></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
            body += "</ul>";
            body += "</body>";
            body += "</HTML>";

            bool respuesta = SO_Email.SendEmailLotusCustom(path, correos, title, body);

            return respuesta;
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

        /// <summary>
        /// Metodo que notifica vía Correo que un documento ya esta disponible para descarga con sello electónico.
        /// </summary>
        /// <returns></returns>
        private bool NotificarDocumentoDisponibleConSello()
        {
            ServiceEmail serviceMail = new ServiceEmail();

            string[] correos = new string[ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count];

            int i = 0;
            foreach (Usuarios item in ListaUsuariosCorreo)
            {
                if (item.IsSelected)
                {
                    correos[i] = item.Correo;
                    i += 1;
                }
            }

            string path = User.Pathnsf;
            string title = "Documento sellado y disponible - " + Nombre;
            string body = string.Empty;
            string tipo_documento = string.Empty;

            switch (id_tipo)
            {
                case 2:
                    tipo_documento = "la HOE";
                    break;
                case 1002:
                    tipo_documento = "la HII";
                    break;
                case 1004:
                    tipo_documento = "la ayuda visual";
                    break;
                case 1007:
                    tipo_documento = "la HMTE";
                    break;
                case 1015:
                    tipo_documento = "la JES";
                    break;
                case 1010:
                    tipo_documento = "la HVA";
                    break;
                case 1011:
                    tipo_documento = "la MIE";
                    break;
                default:
                    break;
            }
            
            body = "<HTML>";
            body += "<head>";
            body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
            body += "</head>";
            body += "<body text=\"white\">";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que " + tipo_documento + " con el número <b> " + Nombre + "</b> versión <b> " + Version + ".0" + " </b> ya se encuentra disponible en el sistema <b> Diseño del proceso </b> con el sello correspondiente. </font> </li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizo la matríz.</font></li>";
            body += "<br/>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Número : <b>" + Nombre + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Descripción : <b>" + Descripcion + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + Version + ".0" + "</b></font></li>";
            body += "</ul>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor solo responda en caso de que el documento sustituya a algún otro.</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Nombre + " " + User.ApellidoPaterno + "</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
            body += "<li></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
            body += "</ul>";
            body += "</body>";
            body += "</HTML>";

            bool respuesta = serviceMail.SendEmailLotusCustom(path, correos, title, body);

            return respuesta;

        }


        /// <summary>
        /// Método que notifica via correo que un documento que ya se encuentra en la matríz fue sellado correctamente
        /// </summary>
        /// <returns></returns>
        private bool NotificarDocumentoExistenteConSello()
        {
            ServiceEmail Correo = new ServiceEmail();

            string[] CorreosUsuarios = new string [vmUsuarios.ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count];

            int i = 0;
            foreach (Usuarios item in vmUsuarios.ListaUsuariosCorreo)
            {
                if (item.IsSelected)
                {
                    CorreosUsuarios[i] = item.Correo;
                    i += 1;
                }
            }

            string path = User.Pathnsf;
            string title = "Documento sellado y disponible - " + Nombre;
            string body = string.Empty;
            string tipo_documento = string.Empty;

            switch (id_tipo)
            {
                case 2:
                    tipo_documento = "la HOE";
                    break;
                case 1002:
                    tipo_documento = "la HII";
                    break;
                case 1004:
                    tipo_documento = "la ayuda visual";
                    break;
                case 1007:
                    tipo_documento = "la HMTE";
                    break;
                case 1015:
                    tipo_documento = "la JES";
                    break;
                case 1010:
                    tipo_documento = "la HVA";
                    break;
                case 1011:
                    tipo_documento = "la MIE";
                    break;
                default:
                    break;
            }

            body = "<HTML>";
            body += "<head>";
            body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
            body += "</head>";
            body += "<body text=\"white\">";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que " + tipo_documento + " con el número <b> " + Nombre + "</b> versión <b> " + Version + ".0" + " </b> ya se encuentra disponible en el sistema <b> Diseño del proceso </b> con el sello correspondiente. </font> </li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizo la matríz.</font></li>";
            body += "<br/>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Número : <b>" + Nombre + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Descripción : <b>" + Descripcion + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + Version + ".0" + "</b></font></li>";
            body += "</ul>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor solo responda en caso de que el documento sustituya a algún otro.</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Nombre + " " + User.ApellidoPaterno + "</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
            body += "<li></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
            body += "</ul>";
            body += "</body>";
            body += "</HTML>";

            bool respuesta = Correo.SendEmailLotusCustom(path, CorreosUsuarios, title, body);

            return respuesta;

        }

        /// <summary>
        /// Método que notifica vía correo el alta de un documento.
        /// </summary>
        /// <returns></returns>
        private bool NotificarNuevaVersion()
        {
            ServiceEmail SO_Email = new ServiceEmail();
            string AreaFrames = string.Empty;

            //obtenemos los correos de la vista FRMListaDocumentos
            string[] correos = new string[ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count];

            //obtenemos los correos de los usuarios seleccionados
            int i = 0;
            foreach (Usuarios item in ListaUsuariosCorreo)
            {
                if (item.IsSelected)
                {
                    correos[i] = item.Correo;
                    i += 1;
                }
            }

            string path = User.Pathnsf;
            string title = "Alta de documento - " + Nombre;
            string body = string.Empty;
            string tipo_documento = string.Empty;

            //obtenemos el tipo de documento
            switch (id_tipo)
            {
                case 1003:
                case 1013:
                    AreaFrames = DataManagerControlDocumentos.GetNombreAreaOHSAS(Convert.ToInt32(id_areasealed));
                    break;
                case 1014:
                case 1006:
                    AreaFrames = DataManagerControlDocumentos.GetNombreAreaISO(Convert.ToInt32(id_areasealed));
                    break;
                case 1005:
                case 1012:
                    AreaFrames = DataManagerControlDocumentos.GetNombreAreaESPECIFICOS(Convert.ToInt32(id_areasealed));
                    break;
                default:
                    break;
            }
            switch (id_tipo)
            {
                case 1003:
                case 1005:
                case 1006:
                    tipo_documento = "la instrucción de trabajo";
                    break;
                case 1012:
                case 1013:
                case 1014:
                    tipo_documento = "el formato";
                    break;
                default:
                    break;
            }

            body = "<HTML>";
            body += "<head>";
            body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
            body += "</head>";
            body += "<body text=\"white\">";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que " + tipo_documento + " con el número <b> " + Nombre + "</b> versión <b> " + Version + ".0" + " </b> ya se encuentra disponible en el sistema </font> <a href=\"http://sealed/frames.htm\">frames</a> </li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizo la matríz.</font></li>";
            body += "<br/>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Número : <b>" + Nombre + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Descripción : <b>" + Descripcion + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + Version + ".0"+ "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Área del Frames en donde se inserto : <b>" + AreaFrames + "</b></font></li>";
            body += "</ul>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">NOTA: Si este documento sustituye a algún otro, favor de notificarme para realizar la baja correspondiente.</font> </p>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor solo responda en caso de que el documento sustituya a algún otro.</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Nombre + " " + User.ApellidoPaterno + "</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
            body += "<li></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
            body += "</ul>";
            body += "</body>";
            body += "</HTML>";

            //Ejecutamos el método para notificar por correo
            bool respuesta = SO_Email.SendEmailLotusCustom(path, correos, title, body);

            return respuesta;
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

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            string mensaje = "Nombre: " + nombre + "\nVersión: " + version + "\nFecha: " + fecha.ToShortDateString() + "\nDescripción: " + descripcion +
                            "\nTipo de Documento: " + NombreTipo + "\nDepartamento: " + NombreDepto + "\nUsuario Elaboró: " + NombreUsuarioElaboro + "\nUsuario Autorizó: " + NombreUsuarioAut;

            //Verifica que todos los campos estén llenos
            if (ValidarValores())
            {
                //Ejecutamos el método para mostrar el mensaje con la información que el usuario capturó.El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialog.SendMessage(StringResources.msgGuardarDocumento, mensaje, setting, MessageDialogStyle.AffirmativeAndNegative);

                //Verificamos que el botón contenga la leyenda Guardar, esto indica que el registro es nuevo.
                if (BotonGuardar == StringResources.ttlGuardar)
                {
                    //Si la respuesta es afirmativa
                    if (result == MessageDialogResult.Affirmative)
                    {
                        //Valída si existe documentos que sena iguales al documento a subir, el resultado se guarda en una variable local.
                        ObservableCollection<Documento> ListDocIguales = ValidaDocumentosIguales();

                        if (ListDocIguales == null)
                        {
                            //Valída si existe documentos que sean similares al documento a subir, el resultado se guarda en una variable local.
                            ObservableCollection<Documento> ListDocSimilares = ValidaSimilares();

                            ListDocSimilares = null;

                            //Si la lista es igual a nulo, no existen documentos similares. Si existe archivos similares, muestra un mensaje
                            if (ListDocSimilares == null)
                            {
                                //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
                                controllerProgressAsync = await dialog.SendProgressAsync(StringResources.msgEspera, StringResources.msgEsperaGuardandoDocumento);

                                //Declaramos un objeto de tipo documento.
                                Documento obj = new Documento();

                                //Declaramos un objeto de tipo Version.
                                Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

                                //Mapeamos los valores al objeto declarado.
                                obj.id_documento = _selectedDocumento.id_documento;
                                obj.nombre = nombre;
                                obj.id_tipo_documento = _id_tipo;
                                obj.id_dep = _id_dep;
                                obj.fecha_actualizacion = _FechaFin;
                                obj.fecha_emision = fecha;
                                obj.id_estatus = 2;
                                obj.usuario = usuario;

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
                                    objVersion.descripcion_v = Descripcion;

                                    //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                                    int id_version = DataManagerControlDocumentos.SetVersion(objVersion, obj.nombre);

                                    //si se guardó correctamente el registro en la tabla versión.
                                    if (id_version != 0)
                                    {
                                        //Iteramos la lista de documentos.
                                        foreach (var item in _ListaDocumentos)
                                        {
                                            //Declaramos un objeto de tipo Archivo.
                                            Archivo objArchivo = new Archivo();

                                            //Mapeamos los valores al objeto creado, se guarda el archivo con el nombre del documento y la versión.
                                            objArchivo.id_version = id_version;
                                            objArchivo.archivo = item.archivo;
                                            objArchivo.ext = item.ext;
                                            objArchivo.nombre = string.Concat(Nombre, version);

                                            //Ejecutamos el método para guardar el documento iterado, el resultado lo guardamos en una variable local.
                                            int nombre = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                                        }
                                        //Ejecutamos el método para cerrar el mensaje de espera.
                                        await controllerProgressAsync.CloseAsync();

                                        //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosGuardadosExito);

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
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorRegistrarVersion);
                                    }
                                }
                                else
                                {
                                    //Si no se hizo la alta.
                                    //Ejecutamos el método para enviar un mensaje de alerta al usuario.
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorRegistrarDocumento);
                                }
                            }
                            else
                            {
                                // si existen archivos similares 
                                VerDocumentosSimilares(ListDocSimilares);
                            }
                        }
                        else
                        {
                            VerDocumentosSimilares(ListDocIguales);
                        }
                    }
                }
                else
                {
                    //Si se genera una nueva versión, leyenda Guardar Version
                    //Si la respuesta es afirmativa
                    if (result == MessageDialogResult.Affirmative)
                    {

                        //Valída si existe documentos que sena iguales al documento a subir, el resultado se guarda en una variable local.
                        ObservableCollection<Documento> ListDocIguales = ValidaDocumentosIguales();

                        //Si no hay documentos con la mista descripción.
                        if (ListDocIguales == null)
                        {
                            //Valída si existe documentos que se aprecezcan al documento a subir, el resultado se guarda en una variable local.
                            ObservableCollection<Documento> ListDocSimilares = ValidaSimilares();

                            ListDocSimilares = null;

                            //si no existe archivos similares, guarda el documento. Si existe archivos similares, muestra un mensaje
                            if (ListDocSimilares == null)
                            {
                                //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
                                controllerProgressAsync = await dialog.SendProgressAsync(StringResources.msgEspera, StringResources.msgEsperaGuardandoDocumento);

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
                                objVersion.descripcion_v = Descripcion;

                                //valida que la version en el documento no se repita
                                int validacion = DataManagerControlDocumentos.ValidateVersion(objVersion);

                                if (validacion == 0)
                                {
                                    //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                                    int id_version = DataManagerControlDocumentos.SetVersion(objVersion, nombre);

                                    //si se realizo guardo la versión 
                                    if (id_version != 0)
                                    {
                                        //Iteramos la lista de documentos.
                                        foreach (var item in _ListaDocumentos)
                                        {
                                            Archivo objArchivo = new Archivo();
                                            //Mapeamos los valores al objeto creado, se guarda el archivo con el nombre del documento y la versión
                                            objArchivo.id_version = id_version;
                                            objArchivo.archivo = item.archivo;
                                            objArchivo.ext = item.ext;
                                            objArchivo.nombre = string.Concat(nombre, version);

                                            //Ejecutamos el método para guardar el documento iterado, el resultado lo guardamos en una variable local.
                                            int id_archivo = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                                        }

                                        //Asignamos el valor de Guardar a la etiqueta del botón.
                                        BotonGuardar = StringResources.ttlGuardar;

                                        //Ejecutamos el método para cerrar el mensaje de espera.
                                        await controllerProgressAsync.CloseAsync();

                                        //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosGuardadosExito);

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
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorRegistrarVersion);
                                    }
                                }
                                else
                                {
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorRegistrarDocumento);
                                    await controllerProgressAsync.CloseAsync();
                                }
                            }
                            else
                            {
                                //si existen documentos similares 
                                VerDocumentosSimilares(ListDocSimilares);
                            }
                        }
                        else
                        {
                            //si existen documentos iguales. 
                            VerDocumentosSimilares(ListDocIguales);
                        }
                    }
                }
            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }

        }

        /// <summary>
        /// Método que notifica vía correo la baja de un documento
        /// </summary>
        /// <returns></returns>
        private bool NotificarBajaDocumento()
        {
            try
            {
                ServiceEmail SO_Email = new ServiceEmail();

                string[] correos = new string[vmUsuarios.ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count];

                int i = 0;
                foreach (Usuarios item in vmUsuarios.ListaUsuariosCorreo)
                {
                    if (item.IsSelected)
                    {
                        correos[i] = item.Correo;
                        i += 1;
                    }
                }

                string path = User.Pathnsf;
                string title = "Baja de documento - " + Nombre;
                string body = string.Empty;
                string tipo_documento = string.Empty;

                switch (id_tipo)
                {
                    case 1003:
                    case 1005:
                    case 1006:
                        tipo_documento = "la instrucción de trabajo";
                        break;
                    case 1012:
                    case 1013:
                    case 1014:
                        tipo_documento = "el formato";
                        break;
                    case 2:
                        tipo_documento = "la Hoja de operación estándar";
                        break;
                    case 1002:
                        tipo_documento = "la hoja de instrucción de inspección (HII)";
                        break;
                    case 1004:
                        tipo_documento = "la ayuda visual";
                        break;
                    case 1007:
                        tipo_documento = "la hoja de método de trabajo estándar";
                        break;
                    case 1010:
                        tipo_documento = "la hoja de ajuste estándar";
                        break;
                    case 1011:
                        tipo_documento = "el método de inspección estandarizado";
                        break;
                    default:
                        tipo_documento = "el documento";
                        break;
                }

                body = "<HTML>";
                body += "<head>";
                body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
                body += "</head>";
                body += "<body text=\"white\">";
                body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
                body += "<ul>";
                body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que " + tipo_documento + " con el número <b> " + Nombre + "</b> versión <b> " + Version + ".0" + " </b> fué dado de baja de la matríz del control de documentos</font> </li>";
                body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizo la matríz.</font></li>";
                body += "<br/>";
                body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Número : <b>" + Nombre + "</b></font></li>";
                body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Descripción : <b>" + Descripcion + "</b></font></li>";
                body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + Version +".0"+"</b></font></li>";
                body += "</ul>";
                body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
                body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Recordar que el dueño del documento es el responsable de eliminar todos los documentos obsoletos que están en piso.</font> </p>";
                body += "<br/>";
                body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor no responda.</font> </p>";
                body += "<br/>";
                body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
                body += "<ul>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Nombre + " " + User.ApellidoPaterno + "</font> </li>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
                body += "<li></li>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
                body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
                body += "</ul>";
                body += "</body>";
                body += "</HTML>";

                bool respuesta = SO_Email.SendEmailLotusCustom(path, correos, title, body);

                return respuesta;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Método que define si es "Buenos días" o "Buenas tardes" dependiendo la hora.
        /// </summary>
        /// <returns></returns>
        private string definirSaludo()
        {
            DateTime d = DateTime.Now;
            string saludo = string.Empty;

            return d.Hour <= 11 ? "Buenos días;" : "Buenas tardes;";
        }

        /// <summary>
        /// Metodo que elimina el documento de la base de datos del frames
        /// </summary>
        /// <returns>Retorna el número de filas afectadas. Si ocurre un error retorna un 0</returns>
        private int DeleteDocumentoSealed()
        {
            int r = 0;
            switch (id_tipo)
            {
                case 1003:
                case 1013:
                    r = DataManagerControlDocumentos.DeleteDocumentoOHSAS(SelectedDocumento.nombre);
                    break;

                case 1005:
                case 1012:
                    r = DataManagerControlDocumentos.DeleteDocumentoEspecifico(SelectedDocumento.nombre);
                    break;

                case 1006:
                case 1014:
                    r = DataManagerControlDocumentos.DeleteDocumentoISO(SelectedDocumento.nombre);
                    break;

                default:
                    break;
            }
            return r;
        }

        /// <summary>
        /// Método que inserta un documento en la base de datos del frames.
        /// </summary>
        /// <returns>Retorna el número de filas afectadas. Si ocurre un error retorna un cero.</returns>
        private int InsertDocumentoSealed()
        {
            int r = 0;
            switch (id_tipo)
            {
                case 1003:
                case 1013:
                    r = DataManagerControlDocumentos.InsertDocumentoOSHAS(Convert.ToInt32(id_areasealed), SelectedDocumento.nombre, Descripcion, Version, Module.GetFormatFechaSealed(Fecha), "CIT", 0, SelectedDocumento.nombre);
                    break;

                case 1005:
                case 1012:
                    r = DataManagerControlDocumentos.InsertDocumentoEspecifico(Convert.ToInt32(id_areasealed), SelectedDocumento.nombre, Descripcion, Version, Module.GetFormatFechaSealed(Fecha), "CIT", 0, SelectedDocumento.nombre);
                    break;

                case 1006:
                case 1014:
                    r = DataManagerControlDocumentos.InsertDocumentoISO(Convert.ToInt32(id_areasealed), SelectedDocumento.nombre, Descripcion, Version, Module.GetFormatFechaSealed(Fecha), "CIT", 0, SelectedDocumento.nombre);
                    break;

                default:
                    break;
            }

            return r;
        }

        /// <summary>
        /// Méto que actualiza un registro de la base de datos del frames.
        /// </summary>
        /// <returns>Retorna el número de filas afectadas. Si ocurre un error retorna un cero.</returns>
        private int UpdateDocumentoSealed()
        {
            int r = 0;
            switch (id_tipo)
            {
                case 1003:
                case 1013:
                    r = DataManagerControlDocumentos.UpdateDocumentoOHSAS(Convert.ToInt32(id_areasealed), SelectedDocumento.nombre, Descripcion, Version, Module.GetFormatFechaSealed(Fecha), "CIT", 0, SelectedDocumento.nombre);
                    break;

                case 1005:
                case 1012:
                    r = DataManagerControlDocumentos.UpdateDocumentoEspecifico(Convert.ToInt32(id_areasealed), SelectedDocumento.nombre, Descripcion, Version, Module.GetFormatFechaSealed(Fecha), "CIT", 0, SelectedDocumento.nombre);
                    break;

                case 1006:
                case 1014:
                    r = DataManagerControlDocumentos.UpdateDocumentoISO(Convert.ToInt32(id_areasealed), SelectedDocumento.nombre, Descripcion, Version, Module.GetFormatFechaSealed(Fecha), "CIT", 0, SelectedDocumento.nombre);
                    break;

                default:
                    break;
            }

            return r;
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
                //Si no hay error se retorna nulo
                return null;
            }
            catch (Exception er)
            {
                //Si hay error se retorna el error
                return er.ToString();
            }
        }

        /// <summary>
        /// Método que agrega una marca de agua a los documentos que no son formatos ni ayudas visuales.
        /// </summary>
        /// <param name="version"></param>
        private async Task<bool> SetElectronicStamp(Model.ControlDocumentos.Version version)
        {
            bool res = false;
            if (id_tipo != 1003 && id_tipo != 1005 && id_tipo != 1006 && id_tipo != 1012 && id_tipo != 1013 && id_tipo != 1014)
            {
                //Incializamos los servicios de dialog.
                DialogService dialog = new DialogService();
                
                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSellarDocumento, setting, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                    ObservableCollection<Archivo> archivos = DataManagerControlDocumentos.GetArchivos(version.id_version);

                    DateTime fecha_sello = DataManagerControlDocumentos.Get_DateTime();

                    string dia = fecha_sello.Day.ToString().Length == 1 ? "0" + fecha_sello.Day : fecha_sello.Day.ToString();
                    string anio = fecha_sello.Year.ToString();
                    string mes = fecha_sello.Month.ToString().Length == 1 ? "0" + fecha_sello.Month : fecha_sello.Month.ToString();

                    string fecha = dia + "/" + mes + "/" + anio;


                    foreach (Archivo item in archivos)
                    {

                        string waterMarkText = "MAHLE CONTROL DE DOCUMENTOS / DOCUMENTO LIBERADO ELECTRÓNICAMENTE Y TIENE VELIDEZ SIN FIRMA." + " DISPOSICIÓN: " + fecha;
                        string waterMarkText2 = "ÚNICAMENTE TIENE VALIDEZ EL DOCUMENTO DISPONIBLE EN INTRANET.";
                        string waterMarkText3 = "LAS COPIAS NO ESTÁN SUJETAS A NINGÚN SERVICIO DE ACTUALIZACIÓN";

                        byte[] newarchivo = AddWatermark(item.archivo, bfTimes, waterMarkText, waterMarkText2, waterMarkText3);

                        item.archivo = newarchivo;

                        int r = DataManagerControlDocumentos.UpdateArchivo(item);

                        res = r == 0 ? false : true;

                    }
                }
                    
            }

            return res;
        }

        private static byte[] AddWatermark(byte[] bytes, BaseFont baseFont, string watermarkText, string waterMarkText2,string waterMarkText3)
        {
            using (var ms = new MemoryStream(10 * 1024))
            {
                using (var reader = new PdfReader(bytes))
                using (var stamper = new PdfStamper(reader, ms))
                {
                    var pages = reader.NumberOfPages;
                    
                    for (var i = 1; i <= pages; i++)
                    {
                        var dc = stamper.GetOverContent(i);
                        //AddWaterMarkText(dc, watermarkText, baseFont, 8, 0, BaseColor.BLACK, reader.GetPageSizeWithRotation(i), 10, 315);
                        //AddWaterMarkText(dc, waterMarkText2, baseFont, 8, 0, BaseColor.BLACK, reader.GetPageSizeWithRotation(i), 20, 290);

                        Rectangle realPageSize = reader.GetPageSizeWithRotation(i);
                        
                        AddWaterMarkText2(dc, watermarkText, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 6), Convert.ToInt32(realPageSize.Bottom + 245));
                        AddWaterMarkText2(dc, waterMarkText2, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 12), Convert.ToInt32(realPageSize.Bottom + 160));
                        AddWaterMarkText2(dc, waterMarkText3, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 18), Convert.ToInt32(realPageSize.Bottom + 160));

                    }
                    stamper.Close();
                }
                return ms.ToArray();
            }
        }

        public static void AddWaterMarkText2(PdfContentByte pdfData, string watermarkText, BaseFont font, float fontSize, float angle, BaseColor color, int pos_x, int pos_y)
        {
            var gstate = new PdfGState { FillOpacity = 1.0f, StrokeOpacity = 1.0f };

            pdfData.SaveState();
            pdfData.SetGState(gstate);
            pdfData.SetColorFill(color);
            pdfData.BeginText();
            pdfData.SetFontAndSize(font, fontSize);
            var x = pos_x;
            var y = pos_y;

            pdfData.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, x, y, angle);
            pdfData.EndText();
            pdfData.RestoreState();

        }

        //public static void AddWaterMarkText(PdfContentByte pdfData, string watermarkText, BaseFont font, float fontSize, float angle, BaseColor color, Rectangle realPageSize,int res, int pos_x)
        //{
        //    var gstate = new PdfGState { FillOpacity = 1.0f, StrokeOpacity = 1.0f };
            
        //    pdfData.SaveState();
        //    pdfData.SetGState(gstate);
        //    pdfData.SetColorFill(color);
        //    pdfData.BeginText();
        //    pdfData.SetFontAndSize(font, fontSize);
        //    var x = (realPageSize.Right + realPageSize.Left) / 2;
        //    var y = (realPageSize.Bottom + realPageSize.Top) / 2;

        //    x = pos_x;
        //    y = realPageSize.Top - res;

        //    pdfData.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, x, y, angle);
        //    pdfData.EndText();
        //    pdfData.RestoreState();

        //}

        /// <summary>
        /// Método que inicializa la lista de los departamentos, tipos y usuarios
        /// </summary>
        private void Inicializar()
        {
            ListaDepartamento= DataManagerControlDocumentos.GetDepartamento();
            ListaTipo = DataManagerControlDocumentos.GetTipo();
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();
            ListaUsuariosCorreo = DataManagerControlDocumentos.GetUsuarios();
            
        }

        /// <summary>
        /// Método para modificar el contenido
        /// </summary>
        private async void modificar()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            string mensaje = StringResources.lblNombre + ":" + " " + nombre +
                "\n" + StringResources.lblVersion + ":" + " " + version +
                "\n" + StringResources.lblFecha + ":" + " " + fecha.ToShortDateString() +
                "\n" + StringResources.lblDescripcion + ":" + " " +descripcion +
                "\n" + StringResources.lblTipoDocumento + ":" + " " +NombreTipo +
                "\n" +StringResources.lblNombreDepartamento + ":" + " " +NombreDepto + 
                "\n" + StringResources.lblUsuarioElaboro + ":" + " " +NombreUsuarioElaboro +
                "\n" + StringResources.lblUsuarioAutorizo + ":" +" " +NombreUsuarioAut;
            
            Bloqueo objBloqueo = new Bloqueo();

            //Método que obtiene un registro si se encuentra activo
            objBloqueo = DataManagerControlDocumentos.GetBloqueo();

            if (objBloqueo.id_bloqueo == 0 || Module.UsuarioIsRol(User.Roles, 2))
            {
                if (ValidarValores())
                {
                    //Ejecutamos el método para mostrar el mensaje con la información que el usuario capturó.El resultado lo asignamos a una variable local.
                    MessageDialogResult result = await dialog.SendMessage(StringResources.msgGuardarDocumento, mensaje, setting, MessageDialogStyle.AffirmativeAndNegative);

                    if (result == MessageDialogResult.Affirmative)
                    {

                        //Valída si existe documentos que se aprecezcan al documento a subir, el resultado se guarda en una variable local.
                        ObservableCollection<Documento> ListDocSimilares = ValidaSimilares();

                        ListDocSimilares = null;

                        //si no existe archivos similares, guarda el documento. Si existe archivos similares, muestra un mensaje
                        if (ListDocSimilares == null)
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
                                obj.fecha_emision = fecha;
                                obj.fecha_actualizacion = _FechaFin;
                                obj.id_estatus = 2;
                                obj.usuario = usuario;

                                //Ejecuta el método para modificar el documento actual
                                int n = DataManagerControlDocumentos.UpdateDocumento(obj);
                                //Si se realizo la modificacion
                                if (n != 0)
                                {
                                    //Se ejecuta el metodo que modifica la version actual, el resultado lo guardamos en una variable local
                                    int update_version = modificaVersion();

                                    //si se modifico correctamente
                                    if (update_version != 0)
                                    {
                                        foreach (var item in _ListaDocumentos)
                                        {
                                            //Declaramos un objeto de tipo Archivo.
                                            Archivo objArchivo = new Archivo();
                                            //Mapeamos los valores al objeto creado, se guarda el archivo con el nombre del documento y la versión
                                            objArchivo.id_version = idVersion;
                                            objArchivo.archivo = item.archivo;
                                            objArchivo.ext = item.ext;
                                            objArchivo.nombre = string.Concat(nombre, version);

                                            //si el archivo no existe 
                                            if (item.id_archivo == 0)
                                            {
                                                //Ejecutamos el método para guardar el documento iterado, el resultado lo guardamos en una variable local.
                                                int a = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                                            }
                                        }
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosGuardadosExito);
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
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorCambiosVersion);
                                    }
                                }
                                else
                                {
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorCambiosDocumentos);
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
                                        //Asiganmos los valores, el nombre se guarda con el nombre de documento y versión
                                        objArchivo.id_version = idVersion;
                                        objArchivo.archivo = item.archivo;
                                        objArchivo.ext = item.ext;
                                        objArchivo.nombre = string.Concat(nombre, version);

                                        //si el archivo no existe 
                                        if (item.id_archivo == 0)
                                        {
                                            //Ejecutamos el método para guardar el documento iterado, el resultado lo guardamos en una variable local.
                                            int a = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                                        }
                                    }
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosGuardadosExito);
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
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorCambiosVersion);
                                }
                            }
                        }
                        else
                        {
                            //si existen documentos similares, ejecutamos la función para visualizar los documentos
                            VerDocumentosSimilares(ListDocSimilares);
                        }
                    }
                }
                else
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
                }
            }
            else
            {
                //El sistema se encuentra bloqueado
                await dialog.SendMessage(StringResources.msgSistemaBloqueado, objBloqueo.observaciones);
            }

            
        }

        /// <summary>
        /// Método para eliminar el registro de un documento, con todas las versiones
        /// Elimina los registros de todos las versiones de un documento
        /// </summary>
        private async void eliminar()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminarRegistro, setting, MessageDialogStyle.AffirmativeAndNegative);

            //Si el id es diferente de cero
            if (id_documento != 0 & result == MessageDialogResult.Affirmative)
            {
                vmUsuarios = new UsuariosViewModel(auxUsuario, auxUsuario_Autorizo);
                FrmListaUsuarios frmListaUsuarios = new FrmListaUsuarios();
                frmListaUsuarios.DataContext = vmUsuarios;

                frmListaUsuarios.ShowDialog();

                //Verficamos que el usuario seleccionó al menos una persona para noticarle la baja del documento. Si no selecciona a ninguna, no se permite la baja.
                if (vmUsuarios.ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count > 0)
                {
                    Documento objDoc_Eliminado = new Documento();
                    //Elimina los documentos de la lista 
                    foreach (var item in _ListaDocumentos)
                    {

                        objDoc_Eliminado.version.archivo.archivo = item.archivo;
                        objDoc_Eliminado.version.archivo.ext = item.ext;
                        int n = DataManagerControlDocumentos.DeleteArchivo(item);
                    }

                    Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
                    objVersion.id_version = idVersion;
                    objVersion.no_version = Version;
                    string mensaje_historial = StringResources.msgEliminaVersion + Version;

                    //Mandamos a llamar a la  función para eliminar la versión.
                    int Dversion = DataManagerControlDocumentos.DeleteVersion(objVersion, mensaje_historial, User, nombre);

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

                        //Mandamos a llamar la funcion para eliminar la version iterada
                        int v = DataManagerControlDocumentos.DeleteVersion(item, mensaje_historial, User, nombre);
                    }

                    //Si se elimino correctamente la versión
                    if (Dversion != 0)
                    {
                        Documento obj = new Documento();

                        //se le asigna el id al objeto
                        obj.id_documento = id_documento;

                        //Se manda a llamar a la función.
                        int n = DataManagerControlDocumentos.DeleteDocumento(obj);

                        if (n != 0)
                        {
                            objDoc_Eliminado.nombre = nombre;
                            objDoc_Eliminado.version.no_version = Version;
                            int docElim = DataManagerControlDocumentos.SetDocumento_Eliminado(objDoc_Eliminado);
                            bool banEliminarFrames = false;
                            int eliminoFrames = 0;

                            //Eliminamos el documento del sistema frames.
                            switch (id_tipo)
                            {
                                case 1003:
                                case 1013:
                                    banEliminarFrames = true;
                                    eliminoFrames = DataManagerControlDocumentos.DeleteDocumentoOHSAS(objDoc_Eliminado.nombre);
                                    break;

                                case 1005:
                                case 1012:
                                    banEliminarFrames = true;
                                    eliminoFrames = DataManagerControlDocumentos.DeleteDocumentoEspecifico(objDoc_Eliminado.nombre);
                                    break;

                                case 1006:
                                case 1014:
                                    banEliminarFrames = true;
                                    eliminoFrames = DataManagerControlDocumentos.DeleteDocumentoISO(objDoc_Eliminado.nombre);
                                    break;
                                default:
                                    banEliminarFrames = false;
                                    break;
                            }

                            string confirmacionFrames = string.Empty;

                            if (banEliminarFrames)
                                confirmacionFrames = eliminoFrames > 0 ? StringResources.msgEliminarDocumentoFrames : StringResources.msgEliminarDocumentoFramesFallida + "http://sealed/frames.htm";

                            string confirmacionCorreo = string.Empty;
                            confirmacionCorreo = NotificarBajaDocumento() ? StringResources.msgNotificacionCorreo : StringResources.msgNotificacionCorreoFallida;

                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgResgitroEliminado + "\n" + confirmacionCorreo + "\n" + confirmacionFrames);
                        }
                        else
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminarDocumentoFallido);
                        }
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminarVersionFallida);
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
                else
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgNotificarBaja);
                }

            }
        }

        /// <summary>
        /// metodo que permite actualizar el numero de copias de un documento
        /// </summary>
        private async void ActualizarNumCopias()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Obtenemos la ventana actual
            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();


            //mostramos la ventana con el campo para ingresar el nuevo numero de copias.
            string num_copias = await window.ShowInputAsync(StringResources.msgIngNumeroCopias, StringResources.msgNumeroCopias, null);

            //comprobamos que el valor que obtenemos sea diferente de nulo
            if (!string.IsNullOrEmpty(num_copias))
            {
                    //comprobamos que el campo solo contenga caracteres numericos.
                    if (Regex.IsMatch(num_copias, @"^\d+$"))
                    {
                        //lo tenemos que convertir a entero para poder ingresar el valor a la base de datos.
                        int nuevo_copias = Int32.Parse(num_copias) + _NoCopias;
                        Documento objDocumento = new Documento();
                        objDocumento.id_documento = id_documento;
                        Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

                        //Declaramos un objeto al cual le asignamos las propiedades que contendra el mensaje.
                        MetroDialogSettings setting = new MetroDialogSettings();
                        setting.AffirmativeButtonText = StringResources.lblYes;
                        setting.NegativeButtonText = StringResources.lblNo;

                        MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgConfirmacion, setting, MessageDialogStyle.AffirmativeAndNegative);

                        if (result == MessageDialogResult.Affirmative)
                        {
                            //ejecutamos el metodo para actualizar el numero de copias
                            int act_cop = DataManagerControlDocumentos.UpdateNoCopias(idVersion, nuevo_copias);

                            //comprobamos que se hayan guardado los cambios con exito, y mandamos un mensaje segun sea el caso
                            if (act_cop != 0)
                            {
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosRealizados);
                                //despues de haber dado aceptar, se inicia el metodo para actualizar el campo de numero de copias.
                                NoCopias = DataManagerControlDocumentos.GetCopias(idVersion);
                            }
                            else
                            {
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorGeneral);
                            }
                        }
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCamposInvalidos);
                    }
            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
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
            objVersion.descripcion_v = Descripcion;

            //Ejecutamos el método para guardar la versión. El resultado lo retornamos
            return DataManagerControlDocumentos.UpdateVersion(objVersion, User, nombre);
        }

        /// <summary>
        /// Método que valida el tipo de documento
        /// si es de tipo OHSAS, ESPECIFICOS O ISO, valida que en la ListaDocumentos sólo tenga un archivo
        /// </summary>
        /// <returns></returns>
        private bool ValidaTipo()
        {
                //Si la lista tiene más de un archivo, retorna falso
                if (_ListaDocumentos.Count > 1)
                    return false;
                else
                    return true;
        }

        /// <summary>
        /// Método que valída si existen documentos con similar descripción del que se va a dar de alta.
        /// </summary>
        /// <returns></returns>
        private  ObservableCollection<Documento> ValidaSimilares()
        {
            Documento ObjDocumento = new Documento();
            ObjDocumento.id_tipo_documento = _id_tipo;
            ObjDocumento.id_dep = _id_dep;
            ObjDocumento.descripcion = Descripcion;
            ObjDocumento.id_documento = id_documento;

            //Ejecutamos el método que obtiene la lista de los documentos similares
            ObservableCollection<Documento> ListaDocumentosSimilares = DataManagerControlDocumentos.ValidateDocumentosSimilares(ObjDocumento);

            //Si la lista es mayor a cero, retornamos la lista, si no encuentra documentos se retorna nulo
            if (ListaDocumentosSimilares.Count > 0)
                return ListaDocumentosSimilares;
            else
                return null;
        }

        /// <summary>
        ///  Método que valída si existen documentos con exactamente la misma descripción del que se va a dar de alta.
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<Documento> ValidaDocumentosIguales()
        {
            Documento ObjDocumento = new Documento();
            ObjDocumento.id_tipo_documento = _id_tipo;
            ObjDocumento.id_dep = _id_dep;
            ObjDocumento.descripcion = Descripcion;
            ObjDocumento.id_documento = id_documento;

            ObservableCollection<Documento> ListaIguales = DataManagerControlDocumentos.ValidateDescripcionIgual(ObjDocumento);

            //Si la lista es mayor a cero, retornamos la lista, si no encuentra documentos se retorna nulo
            if (ListaIguales.Count > 0)
                return ListaIguales;
            else
                return null;
        }

        /// <summary>
        /// Método que muestra los documentos similares
        /// </summary>
        /// <param name="ListaSimilares"></param>
        private async void VerDocumentosSimilares(ObservableCollection<Documento> ListaSimilares)
        {
            //Verificamos que existan documentos similares
            if (ListaSimilares.Count >0)
            {
                //Existen documentos similares
                DialogService dialogService = new DialogService();

                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendrá el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.ttlVerArchivo;
                setting.NegativeButtonText = StringResources.msgCancelar;

                //Ejecutamos el método para mostrar el mensaje. El resultado lo guardamos en una variable local.
                MessageDialogResult result = await dialogService.SendMessage(StringResources.msgErrorGuardarArchivo, StringResources.msgErrorDescripcionErrorArchivo, setting, MessageDialogStyle.AffirmativeAndNegative);
                switch (result)
                {
                    case MessageDialogResult.Negative:
                        break;
                    case MessageDialogResult.Affirmative:

                        //Mostramos al usuario la lista de documentos
                        Frm_DocumentosSimilares frmSimilares = new Frm_DocumentosSimilares();
                        Documentos_SimilaresVM context = new Documentos_SimilaresVM(ListaSimilares);
                        frmSimilares.DataContext = context;
                        frmSimilares.ShowDialog();
                        break;
                    case MessageDialogResult.FirstAuxiliary:
                        break;
                    case MessageDialogResult.SecondAuxiliary:
                        break;
                    default:
                        break;
                }               
            }
        }

        /// <summary>
        /// Método para cerrar la pantalla
        /// </summary>       
        private async void CerrarVentana()
        {
            DialogService dialog = new DialogService();
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblCerrarVentana, setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
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
        /// Método que sella electronicamente el documento
        /// </summary>
        private async void SellarCopiasDocumentos()
        {
            //Declaramos los servicios para mostrar los mensajes
            DialogService dialog = new DialogService();
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;
            
            if (_selectedDocumento != null)
            {
                //mandamos llamar la lista de correos para seleccionar a quien vamos a notificar
                //que ya se sello el documento electronicamente
                vmUsuarios = new UsuariosViewModel(auxUsuario, auxUsuario_Autorizo);
                FrmListaUsuarios frmListaUsuarios = new FrmListaUsuarios();
                frmListaUsuarios.DataContext = vmUsuarios;

                //mostramos la ventana
                frmListaUsuarios.ShowDialog();

                //verificamos que el usuario haya seleccionado por lo menos un usuario
                if (vmUsuarios.ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count > 0)
                {
                        
                    Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

                    //obtenemos los datos de la ultima versión del documento
                    objVersion.id_version = idVersion;
                    objVersion.no_version = version;
                    objVersion.id_documento = id_documento;
                    objVersion.id_usuario = _usuario;
                    objVersion.id_usuario_autorizo = _usuarioAutorizo;
                    objVersion.fecha_version = fecha;
                    objVersion.id_estatus_version = 1;
                    objVersion.descripcion_v = Descripcion;

                    //mandamos llamar al método que pone el sello electronicamente.
                    bool r = await SetElectronicStamp(objVersion);

                    //verfificamos que se haya sellado el documento correctamente
                    if (r == true)
                    {
                        //Mandamos llamar el metodo para notificar por correo electronico
                        string confirmacionCorreo = string.Empty;
                        confirmacionCorreo = NotificarDocumentoExistenteConSello() ? StringResources.msgNotificacionCorreo : StringResources.msgNotificacionCorreoFallida;

                        //mandamos mensaje de confirmación.
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblDocumentoSellado + "\n" + confirmacionCorreo);

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
                        //si hubo un error al sellar el documento se notifica al usuario
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblErrorSelloDocumento);
                    }
                }else
                {
                    //se notifica si el usuario no selecciono a mas de un usuario para notificar
                    await dialog.SendMessage(StringResources.ttlAlerta, "Debe Seleccionar a quien notificar");
                }
                    
            }
        }
        #endregion
    }
}
