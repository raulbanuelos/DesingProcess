﻿using MahApps.Metro.Controls;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using View.Forms.ControlDocumentos;
using View.Resources;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MahApps.Metro.IconPacks;
using System.Reflection;
using System.Drawing.Imaging;
using System.Drawing;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Threading;
using System.Collections.Generic;

namespace View.Services.ViewModel
{
    public class DocumentoViewModel : INotifyPropertyChanged
    {
        #region Attributes

        UsuariosViewModel vmUsuarios;
        private int idVersion;
        // variables auxiliar, guarda la información cuando se genera una nueva versión
        private string auxversion, auxUsuario, auxUsuario_Autorizo, auxDescripcion;
        private DateTime auxFecha;
        public Usuario User;
        Archivo ArchivoTemporal;
        string codeValidation = string.Empty;
        Archivo archivoFirmado;
        string ipServidor = "";

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

        public bool IsSelectedGrupos;
        public bool IsSelectedUsuarios;
        public Archivo SignedFile;

        private static int id_recurso;

        public Documento DatosDocumento = new Documento();

        public bool DocumentoNuevo = false;

        public bool VersionGenerada = false;

        public string VentanaProcedencia = string.Empty;

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

        private DateTime fecharev = DataManagerControlDocumentos.Get_DateTime();
        public DateTime Fecharev
        {
            get
            {
                return this.fecharev;
            }
            set
            {
                this.fecharev = value;
                NotifyChange("Fecharev");
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
        public int id_dep
        {
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
        public ObservableCollection<Departamento> ListaDepartamento
        {
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

        private ObservableCollection<objUsuario> _ListaUsuarios = new ObservableCollection<objUsuario>();
        public ObservableCollection<objUsuario> ListaUsuarios
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

        private ObservableCollection<objUsuario> _ListaUsuariosCorreo;
        public ObservableCollection<objUsuario> ListaUsuariosCorreo
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
        public ObservableCollection<Documento> ListaNumeroDocumento
        {
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
        public Documento SelectedDocumento
        {
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

        private DO_Grupos model;

        private DialogService dialogService;

        private bool _bttnArchivos;
        public bool BttnArchivos
        {
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
        public bool BttnEliminar
        {
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

        private bool _banAlertaCorreo = false;
        public bool banAlertaCorreo
        {
            get
            {
                return _banAlertaCorreo;
            }
            set
            {
                _banAlertaCorreo = value;
                NotifyChange("banAlertaCorreo");
            }
        }

        private bool _banButtonNotificar;
        public bool banButtonNotificar
        {
            get { return _banButtonNotificar; }
            set { _banButtonNotificar = value; NotifyChange("banButtonNotificar"); }
        }

        private bool nombreEnabled = false;
        public bool NombreEnabled
        {
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
        public int NoCopias
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
        public bool BttnCancelar
        {
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

        private bool _isopen;
        public bool isopen
        {
            get
            {
                return _isopen;
            }
            set
            {
                _isopen = value;
                NotifyChange("isopen");
            }
        }

        private bool _visibilitycheck_suscripcion;
        public bool visibilitycheck_suscripcion
        {
            get
            {
                return _visibilitycheck_suscripcion;
            }
            set
            {
                _visibilitycheck_suscripcion = value;
                NotifyChange("visibilitycheck_suscripcion");
            }
        }

        private bool _visibilitylabelsuscripcion;
        public bool visibilitylabelsuscripcion
        {
            get
            {
                return _visibilitylabelsuscripcion;
            }
            set
            {
                _visibilitylabelsuscripcion = value;
                NotifyChange("visibilitylabelsuscripcion");
            }
        }

        private bool _ActivarSuscripcion;
        public bool ActivarSuscripcion
        {
            get
            {
                return _ActivarSuscripcion;
            }
            set
            {
                _ActivarSuscripcion = value;
                NotifyChange("ActivarSuscripcion");

                // Validamos el estado del check, si está seleccionado o no
                if(value == false)
                {
                    // Si no está seleccionado, se elimina registro de suscripción al documento
                    DataManagerControlDocumentos.Delete_SuscriptorDoc(User.NombreUsuario, id_documento);
                }
                else
                {
                    // Si está seleccionado, se inserta registro de suscripción
                    DataManagerControlDocumentos.Insert_SuscriptorDoc(User.NombreUsuario, id_documento);
                }
            }
        }

        public int idgrupo
        {
            get
            {
                return model.idgrupo;
            }
            set
            {
                model.idgrupo = value;
                NotifyChange("idgrupo");
            }
        }

        private DO_Grupos _GrupoSeleccionado;
        public DO_Grupos GrupoSeleccionado
        {
            get
            {
                return _GrupoSeleccionado;
            }
            set
            {
                _GrupoSeleccionado = value;
                NotifyChange("GrupoSeleccionado");
            }
        }

        private ObservableCollection<DO_INTEGRANTES_GRUPO> _ListaIntegrantes_Grupo;
        public ObservableCollection<DO_INTEGRANTES_GRUPO> ListaIntegrantes_Grupo
        {
            get
            {
                return _ListaIntegrantes_Grupo;
            }
            set
            {
                _ListaIntegrantes_Grupo = value;
                NotifyChange("ListaIntegrantes_Grupo");
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
        public DateTime FechaFin
        {
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
                //OnPropertyChanged();
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
                //OnPropertyChanged();
                NotifyChange("MenuOptionItems");
            }
        }

        private bool _AdjuntarDocumento = false;
        public bool AdjuntarDocumento
        {
            get
            {
                return _AdjuntarDocumento;
            }
            set
            {
                _AdjuntarDocumento = value;
                NotifyChange("AdjuntarDocumento");
            }
        }

        private ObservableCollection<DO_Grupos> _ListaGrupos;
        public ObservableCollection<DO_Grupos> ListaGrupos
        {
            get
            {
                return _ListaGrupos;
            }
            set
            {
                _ListaGrupos = value;
                NotifyChange("ListaGrupos");
            }
        }

        private string _TituloCheckGrupos = StringResources.lblSeleccionarTodosGrupos;
        public string TituloCheckGrupos
        {
            get
            {
                return _TituloCheckGrupos;
            }
            set
            {
                _TituloCheckGrupos = value;
                NotifyChange("TituloCheckGrupos");
            }
        }

        private string _TituloCheckUsuarios = StringResources.lblSeleccionarTodosUsuarios;
        public string TituloCheckUsuarios
        {
            get
            {
                return _TituloCheckUsuarios;
            }
            set
            {
                _TituloCheckUsuarios = value;
                NotifyChange("TituloCheckUsuarios");
            }
        }

        private string _TituloBotonNotificar = StringResources.lblNotificar;
        public string TituloBotonNotificar
        {
            get
            {
                return _TituloBotonNotificar;
            }
            set
            {
                _TituloBotonNotificar = value;
                NotifyChange("TituloBotonNotificar");
            }
        }

        private ObservableCollection<DO_UsuarioSuscrito> _ListaUsuariosSuscritos;
        public ObservableCollection<DO_UsuarioSuscrito> ListaUsuariosSuscritos
        {
            get
            {
                return _ListaUsuariosSuscritos;
            }
            set
            {
                _ListaUsuariosSuscritos = value;
                NotifyChange("ListaUsuariosSuscritos");
            }
        }

        private ObservableCollection<DO_UsuarioSuscrito> _ListaUsuariosSuscritosParaEliminar;
        public ObservableCollection<DO_UsuarioSuscrito> ListaUsuariosSuscritosParaEliminar
        {
            get
            {
                return _ListaUsuariosSuscritosParaEliminar;
            }
            set
            {
                _ListaUsuariosSuscritosParaEliminar = value;
                NotifyChange("ListaUsuariosSuscritosParaEliminar");
            }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor para generar una versión, o modificar el documento
        /// </summary>
        /// <param name="selectedDocumento"></param>
        /// <param name="band"></param>
        /// <param name="ModelUsuario"></param>
        public DocumentoViewModel(Documento selectedDocumento, bool band, Usuario ModelUsuario)
        {
            ipServidor = System.Configuration.ConfigurationManager.AppSettings["ipNodeServer"];
            //Variables auxiliar guarda la información para cuando se genera una versión nueva
            User = ModelUsuario;
            //Inicializa los combobox
            Inicializar();

            #region Cargar Usuarios a Notificar

            //Cargamos la lista de usuarios seleccionados a notificar, si es que existen.
            ObservableCollection<DO_USUARIO_NOTIFICACION_VERSION> listaUsuariosNotificarGuardados = DataManagerControlDocumentos.GetAllUsuariosNotificacionVersion(selectedDocumento.version.id_version);
            if (listaUsuariosNotificarGuardados.Count > 0)
            {
                if (ListaGrupos.Count > 0)
                {
                    foreach (var grupo in ListaGrupos)
                    {
                        ObservableCollection<DO_INTEGRANTES_GRUPO> listaUsuariosGrupo = DataManagerControlDocumentos.GetAllIntegrantesGrupo(grupo.idgrupo);
                        int integrantesGrupo = listaUsuariosGrupo.Count;
                        int c = 0;
                        foreach (var usuarioGrupo in listaUsuariosGrupo)
                        {
                            foreach (var usuarioGuardado in listaUsuariosNotificarGuardados)
                            {
                                if (usuarioGrupo.idusuariointegrante == usuarioGuardado.id_usuario)
                                {
                                    c += 1;
                                }
                            }
                        }
                        if (c == integrantesGrupo)
                        {
                            grupo.IsSelected = true;
                        }
                    }


                    foreach (var item in listaUsuariosNotificarGuardados)
                    {
                        foreach (var item2 in ListaUsuariosCorreo)
                        {
                            if (item.id_usuario == item2.usuario)
                            {
                                item2.IsSelected = true;
                            }
                        }
                    }

                    foreach (var grupoSeleccionado in ListaGrupos.Where(x => x.IsSelected).ToList())
                    {
                        ObservableCollection<DO_INTEGRANTES_GRUPO> listaUsuariosGrupo = DataManagerControlDocumentos.GetAllIntegrantesGrupo(grupoSeleccionado.idgrupo);
                        foreach (var usuarioGrupoSeleccionado in listaUsuariosGrupo)
                        {
                            foreach (var usuarioCorreo in ListaUsuariosCorreo)
                            {
                                if (usuarioGrupoSeleccionado.idusuariointegrante == usuarioCorreo.usuario)
                                {
                                    usuarioCorreo.IsSelected = false;
                                }
                            }
                        }
                    }

                }
                else
                {
                    foreach (var item in listaUsuariosNotificarGuardados)
                    {
                        foreach (var item2 in ListaUsuariosCorreo)
                        {
                            if (item.id_usuario == item2.usuario)
                            {
                                item2.IsSelected = true;
                            }
                        }
                    }
                }
            }
            #endregion

            //Asiganmos los valores para que se muestren
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
            BttnArchivos = false;

            //Si es ventana para generar una nueva versión, band = true
            BttnVersion = band;

            //si band contiene true, significa que el documento esta liberado, caso contrario es por que esta en pendiente por corregir
            if (band == true)
            {
                //mandamos llamar el menú que lo construye
                //CreateMenuItems(Ventana);
                AdjuntarDocumento = true;

                // Mostramos check de suscripción a documentos
                visibilitycheck_suscripcion = true;
                visibilitylabelsuscripcion = true;
            }
            else
            {
                //Ocultamos el mensaje en la ventana pendiente por liberar
                banAlertaCorreo = true;
                banButtonNotificar = true;
            }

            //si es personal del CIT y es ventana para generar una nueva versión
            if (Module.UsuarioIsRol(User.Roles, 2))
            {
                BttnModificar = true;
                BttnEliminar = band;
                IsEnabled = true;
                EnabledEliminar = true;
                EnabledFecha = true;
                BttnArchivos = true;
                VersionEnabled = true;
                if (id_tipo == 1015)
                    BttnGuardar = false;
            }
            VentanaProcedencia = "DocumentoLiberado";
            CreateMenuItems(VentanaProcedencia);

            //si es ventana para corregir documento con estatus pendiente por corregir.
            if (band == false)
            {
                IsEnabled = true;
                BttnModificar = true;
                EnabledEliminar = true;
                BttnArchivos = true;
                EnabledFecha = false;
                Fecha = DataManagerControlDocumentos.Get_DateTime();
                VentanaProcedencia = "DocumentoPorCorregir";
                CreateMenuItems(VentanaProcedencia);
                //Si es administrador del CIT muestra la fecha.
                if (Module.UsuarioIsRol(User.Roles, 2))
                    EnabledFecha = true;
                if (id_tipo == 1015 || id_tipo == 2 || id_tipo == 1002 || id_tipo == 1004 || id_tipo == 1002)
                    BttnModificar = false;

                banAlertaCorreo = ListaGrupos.Where(x => x.IsSelected).ToList().Count > 0 || ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count > 0 ? false : true;
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

            // Asignamos a la variable, si el usuario ya está inscrito o no al documento
            bool inscrito = DataManagerControlDocumentos.Get_RegisrosSuscritos(User.NombreUsuario, id_documento);

            // Validamos
            if (inscrito == true)
            {
                // Si ya está inscrito, el botón inicia seleccionado
                _ActivarSuscripcion = true;
            }
            else
            {
                // Si no está suscrito, el botón permanece deseleccionado
                _ActivarSuscripcion = false;
            }
        }

        /// <summary>
        /// Constructor para crear un nuevo documento
        /// </summary>
        /// <param name="ModelUsuario"></param>
        public DocumentoViewModel(Usuario ModelUsuario)
        {
            ipServidor = System.Configuration.ConfigurationManager.AppSettings["ipNodeServer"];
            BotonGuardar = StringResources.ttlGuardar;
            BttnGuardar = true;
            BttnArchivos = true;
            EnabledEliminar = true;
            NombreEnabled = true;
            banAlertaCorreo = true;
            banButtonNotificar = true;
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
            Inicializar();
            VentanaProcedencia = "DocumentoNuevo";
            CreateMenuItems(VentanaProcedencia);
            DocumentoNuevo = true;
        }

        /// <summary>
        /// Constructor para liberar documentos
        /// </summary>
        /// <param name="selectedDocumento"></param>
        /// <param name="ModelUsuario"></param>
        public DocumentoViewModel(Documento selectedDocumento, Usuario ModelUsuario)
        {
            ipServidor = System.Configuration.ConfigurationManager.AppSettings["ipNodeServer"];
            User = ModelUsuario;
            Encriptacion des = new Encriptacion();

            //si es personal del CIT, la campo de fecha es editable
            if (Module.UsuarioIsRol(User.Roles, 2))
            {
                EnabledFecha = true;
                VersionEnabled = true;
            }


            //Inicializa los combobox
            Inicializar();
            IsEnabled = false;
            EnabledEliminar = false;
            BttnArchivos = false;
            BttnLiberar = true;
            WidthButton = 155;

            //Ocultamos el mensaje en la ventana pendiente por liberar
            banAlertaCorreo = false;
            banButtonNotificar = false;

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

            //Obtenemos si el usuario adjunto el documento firmado.
            SignedFile = DataManagerControlDocumentos.GetDocumentoFirmado(idVersion);

            //Obtenemos el tipo de documento
            id_tipo = DataManagerControlDocumentos.GetTipoDocumento(id_documento);

            //Si es personal del CIT, el campo de usuario elaboro es editable.
            if (Module.UsuarioIsRol(User.Roles, 2))
                VersionEnabled = true;

            //Inicializamos la lista de Areas del sistema frames.
            ListaAreasSealed = new ObservableCollection<FO_Item>();

            //Llenamos la lista de las áreas y si es una versión superior a 1, obtenemos el área a la cual esta asignada en el sistema frames.
            //switch (id_tipo)
            //{
            //    case 1003:
            //    case 1013:
            //        ListaAreasSealed = DataManagerControlDocumentos.GetAllAreasOHSAS();
            //        if (!Module.IsNumeric(Version) || (Module.IsNumeric(Version) && Convert.ToInt32(Version) > 1))
            //        {
            //            id_areasealed = DataManagerControlDocumentos.GetIdAreaOHSAS(Nombre);
            //        }
            //        else
            //        {
            //            id_areasealed = "0";
            //        }
            //        break;

            //    case 1005:
            //    case 1012:
            //    case 1011:
            //        ListaAreasSealed = DataManagerControlDocumentos.GetAllAreasEspecifico();
            //        if (!Module.IsNumeric(Version) || (Module.IsNumeric(Version) && Convert.ToInt32(Version) > 1))
            //        {
            //            id_areasealed = DataManagerControlDocumentos.GetIdAreaEspecifico(Nombre);
            //        }
            //        else
            //        {
            //            id_areasealed = "0";
            //        }
            //        break;

            //    case 1006:
            //    case 1014:
            //        ListaAreasSealed = DataManagerControlDocumentos.GetAllAreasISO();
            //        if (!Module.IsNumeric(Version) || (Module.IsNumeric(Version) && Convert.ToInt32(Version) > 1))
            //        {
            //            id_areasealed = DataManagerControlDocumentos.GetIdAreaISO(Nombre);
            //        }
            //        else
            //        {
            //            id_areasealed = "0";
            //        }
            //        break;

            //    default:
            //        break;
            //}

            string mensaje = string.Empty;

            if (id_tipo == 1003 || id_tipo == 1013 || id_tipo == 1005 || id_tipo == 1012 || id_tipo == 1011 || id_tipo == 1006 || id_tipo == 1014)
            {
                // Comprobamos que la lista venga en 0, de ser así mandará un mensaje de error.
                if (ListaAreasSealed.Count == 0)
                {
                    mensaje = StringResources.ttlAlerta + StringResources.msgErrorListaFrame;
                }
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

            #region CARGA DE USUARIOS PARA NOTIFICAR

            //obtenemos la lista de los usuarios
            ListaUsuariosCorreo = DataManagerControlDocumentos.GetUsuarios();

            //iteramos la lista
            //para seleciconar los usuarios a notificar al momento de abrirse la ventana
            foreach (var item in ListaUsuariosCorreo)
            {
                //seleccionamos el administrado del sistema para notificar
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

            // Asignamos nueva lista con todos los usuarios de la tabla TR_USUARIOS_NOTIFICACION_VERSION
            ObservableCollection<DO_USUARIO_NOTIFICACION_VERSION> ListaUsuariosCorreoCompleta = DataManagerControlDocumentos.GetAllUsuariosNotificacionVersion(idVersion);

            // Declaramos nueva lista para almacenar los usuarios suscritos a documentos
            ListaUsuariosSuscritos = DataManagerControlDocumentos.Get_UserSuscripDoc(id_documento);

            // Iteramos ambas listas, y nos aseguramos de que todos los usuarios necesarios estén seleccionados
            foreach (var UserTotales in ListaUsuariosCorreoCompleta)
            {
                foreach (var User in ListaUsuariosCorreo)
                {
                    if (UserTotales.id_usuario == User.usuario)
                    {
                        User.IsSelected = true;
                    }
                }
            }

            // Iteramos la lista con usuarios suscritos y los cargamos a la lista general para notificar
            foreach (var UserSuscritos in ListaUsuariosSuscritos)
            {
                foreach (var User in ListaUsuariosCorreo)
                {
                    if (UserSuscritos.id_usuariosuscrito == User.usuario)
                    {
                        User.IsSelected = true;
                    }
                }
            }

            #endregion

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

            archivoFirmado = DataManagerControlDocumentos.GetDocumentoFirmado(selectedDocumento.version.id_version);

            //establecemos el tipo de ventana para saber que opciones del menú se van a mostrar
            VentanaProcedencia = "PendienteLiberar";
            //mandamos llamar el método que construye el menú
            CreateMenuItems(VentanaProcedencia);
        }

        public DocumentoViewModel(DO_Grupos Model)
        {
            ipServidor = System.Configuration.ConfigurationManager.AppSettings["ipNodeServer"];
            //Mapeamos el valor del modelo recibido al atributo de la clase.
            model = Model;
            dialogService = new DialogService();
        }

        public DocumentoViewModel()
        {
            ipServidor = System.Configuration.ConfigurationManager.AppSettings["ipNodeServer"];
            dialogService = new DialogService();
            NotifyChange("ListaIntegrantes_Grupo");
        }

        #endregion

        #region Commands

        public ICommand DownLoadFiles
        {
            get
            {
                return new RelayCommand(o => bajarArchivos());
            }
        }

        public ICommand DownLoadFileScanned
        {
            get
            {
                return new RelayCommand(o => downLoadFileScanned());
            }
        }

        /// <summary>
        /// Comando que valida usuarios seleccionados para notificar
        /// </summary>
        public ICommand ValidarSeleccionados
        {
            get
            {
                return new RelayCommand(o => RecorrerListas());
            }
        }

        /// <summary>
        /// Comando que genera el archivo automáticamente.
        /// </summary>
        public ICommand GenerarArchivo
        {
            get
            {
                return new RelayCommand(o => generarArchivo());
            }
        }

        /// <summary>
        /// Comando para guardar un registro de documento
        /// </summary>
        public ICommand GuardarControl
        {
            get
            {
                return new RelayCommand(o => guardarControl(string.Empty));
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
        /// Comando que manda a regresar por corregir
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
                return new RelayCommand(o => liberarDocumento());
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

        /// <summary>
        /// Comando para poner un documento liberado en pendiente por corregir y borrar el documento que contiene
        /// </summary>
        public ICommand EliminarDocumntoSellado
        {
            get
            {
                return new RelayCommand(o => EliminarDocumentoSellado());
            }
        }

        /// <summary>
        /// Comando para abrir o cerras flyout
        /// </summary>
        public ICommand IrFlyOut
        {
            get
            {
                return new RelayCommand(o => abrircerrarFlyout());
            }
        }

        /// <summary>
        /// Comando para abrir grupo y ver usuarios integrantes
        /// </summary>
        public ICommand AbrirGrupo
        {
            get
            {
                return new RelayCommand(a => abrirgrupo());
            }
        }

        /// <summary>
        /// Comando para ir a la ventana de crear grupos
        /// </summary>
        public ICommand IrCrearGrupo
        {
            get
            {
                return new RelayCommand(a => ircreargrupo());
            }
        }

        /// <summary>
        /// Comando para eliminar grupo
        /// </summary>
        public ICommand EliminarGrupo
        {
            get
            {
                return new RelayCommand(a => eliminargrupo());
            }
        }

        /// <summary>
        /// Comando que llama al método para seleccionar o deseleccionar todos los grupos
        /// </summary>
        public ICommand SelecDeselecGrupos
        {
            get
            {
                return new RelayCommand(a => _SelecDeselecGrupos());
            }
        }

        /// <summary>
        /// Comando que llama al métido para seleccionar o deseleccionar todos los usuarios
        /// </summary>
        public ICommand SelecDeselecUsuarios
        {
            get
            {
                return new RelayCommand(a => _SelecDeselecUsuarios());
            }
        }

        #endregion

        #region Methods

        private async void downLoadFileScanned()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();
            if (archivoFirmado != null)
            {
                try
                {
                    //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                    string filename = GetPathTempFile(archivoFirmado);

                    //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                    File.WriteAllBytes(filename, archivoFirmado.archivo);
                    //Se inicializa el programa para visualizar el archivo.
                    Process.Start(filename);
                }
                catch (Exception)
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorAbrir);
                }
            }
        }

        private int validarArchivo(out string mensaje)
        {
            // 4 posibles salidas para validar
            int validacion = 2;

            if (id_tipo == 1002)
            {
                validacion = validarHII(out mensaje);
                return validacion;
            }
            if (id_tipo == 1004)
            {
                validacion = validarAVY(out mensaje);
                return validacion;
            }

            if (id_tipo == 2)
            {
                validacion = validarHOE(out mensaje);
                return validacion;
            }

            validacion = validarJES(out mensaje);
            return validacion;
        }

        /// <summary>
        /// 1 = Archivo correcto / 2 = Archivo incorrecto (errores de datos) / 3 = Ocurrió un error / 4 = El archivo adjuntado no pertenece al tipo de formato
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        private int validarHOE(out string mensaje)
        {
            try
            {
                object unknownType = Type.Missing;
                int ban = 1;
                string CodigoFormato = "Código de formato:  : F-3571-R18-es";

                foreach (Archivo archivo in ListaDocumentos)
                {
                    mensaje = string.Empty;
                    string pathExcel = GetPathTempFile(archivo);

                    Archivo archivoPDF = archivo;
                    archivoPDF.ext = ".pdf";
                    archivo.ruta = @"/Images/p.png";
                    string pathPDF = GetPathTempFile(archivoPDF);

                    //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                    File.WriteAllBytes(pathExcel, archivo.archivo);

                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(pathExcel, true);

                    Microsoft.Office.Interop.Excel.Worksheet sheet1;
                    sheet1 = ExcelWork.Sheets[1];

                    if (sheet1.Name != "HOE")
                    {
                        mensaje += StringResources.msgLaHoja1 + "HOE";

                        ExcelApp.Visible = false;

                        if (ExcelWork != null)
                            ExcelWork.Close(unknownType, unknownType, unknownType);

                        if (ExcelApp != null)
                            ExcelApp.Quit();

                        return 4;
                    }

                    for (int i = 1; i <= ExcelWork.Sheets.Count; i++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Worksheet sheet;
                            sheet = ExcelWork.Sheets[i];

                            if (sheet.PageSetup.Pages.Count > 1)
                            {
                                mensaje += "\n" + "La Hoja " + ExcelWork.Sheets[i].Name + " " + " tiene " + sheet.PageSetup.Pages.Count + " páginas. " + StringResources.lblOnlyOnePage;
                                ban = 2;
                            }

                            bool ValidacionNumeracion = true;
                            //Evaluamos la numeración de las hojas
                            string Numeracion = Convert.ToString(sheet.Range["X7"].Value);

                            if (Numeracion != "Hoja " + i + " de " + ExcelWork.Sheets.Count)
                            {
                                ValidacionNumeracion = false;
                            }

                            if (ValidacionNumeracion == false)
                            {
                                mensaje += "\n" + StringResources.msgNumeracionIncorrecta + ExcelWork.Sheets[i].Name + " " + StringResources.msgDBCr + "Hoja " + i + " de " + ExcelWork.Sheets.Count;
                                ban = 2;
                            }

                            int VRevisar = 10;
                            string VersionRevisar = "VERSION_10";
                            string FechaRevisar = "FECHA_A10";
                            string UsuarioRevisar = "USUARIO_A10";

                            if (Convert.ToInt32(Version) <= VRevisar)
                            {
                                VersionRevisar = "VERSION_" + Version;
                                FechaRevisar = "FECHA_A" + Version;
                                UsuarioRevisar = "USUARIO_A" + Version;
                            }

                            string fecha = Convert.ToString(sheet.Range["FECHA_LIBERACION"].Value);
                            string elaboro = Convert.ToString(sheet.Range["ELABORO"].Value);
                            string aprobo = Convert.ToString(sheet.Range["APROBO"].Value);
                            string reviso = Convert.ToString(sheet.Range["REVISO"].Value);
                            string codigo = Convert.ToString(sheet.Range["CODIGO"].Value);
                            string departamento = Convert.ToString(sheet.Range["PROCESO"].Value);
                            string codformato = Convert.ToString(sheet.Range["CODFORMATO"].Value);
                            string no_version = Convert.ToString(sheet.Range[VersionRevisar].Value);
                            string UsuarioRev = Convert.ToString(sheet.Range[UsuarioRevisar].Value);
                            string FechaRev = Convert.ToString(sheet.Range[FechaRevisar].Value);

                            //Validar fecha elaboración
                            DateTime date = Convert.ToDateTime(fecha);
                            if (date.Year != FechaFin.Year || date.Month != FechaFin.Month || date.Day != FechaFin.Day)
                            {
                                string mes = "";
                                mes = FechaFin.Month < 10 ? "0" + FechaFin.Month : "" + FechaFin.Month;

                                string dia = "";
                                dia = FechaFin.Day < 10 ? "0" + FechaFin.Day : "" + FechaFin.Day;

                                mensaje += "\n" + StringResources.msgFElaboraIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + FechaFin.Year + "-" + mes + "-" + dia;
                                ban = 2;
                            }

                            //Validar fecha revisión
                            DateTime date1 = Convert.ToDateTime(FechaRev);
                            if (date1.Year != FechaFin.Year || date1.Month != FechaFin.Month || date1.Day != FechaFin.Day)
                            {
                                string mes = "";
                                mes = FechaFin.Month < 10 ? "0" + FechaFin.Month : "" + FechaFin.Month;

                                string dia = "";
                                dia = FechaFin.Day < 10 ? "0" + FechaFin.Day : "" + FechaFin.Day;

                                mensaje += "\n" + StringResources.msgFReviIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + FechaFin.Year + "-" + mes + "-" + dia;
                                ban = 2;
                            }
                            if (elaboro != ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto)
                            {
                                mensaje += "\n" + StringResources.msgUElabIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto;
                                ban = 2;
                            }
                            if (aprobo != ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto)
                            {
                                mensaje += "\n" + StringResources.msgUAproIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto;
                                ban = 2;
                            }

                            string UsuariosPermitido = NombreUsuarioAut.Replace(" ", "");
                            if (UsuariosPermitido == "SISTEMA")
                            {
                                mensaje += "\n" + StringResources.msgUNotSISTEMA;
                                ban = 2;
                            }
                            if (reviso != ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto)
                            {
                                mensaje += "\n" + StringResources.msgUAutoIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto;
                                ban = 2;
                            }
                            if (codigo != SelectedDocumento.nombre)
                            {
                                mensaje += "\n" + StringResources.msgCodIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + SelectedDocumento.nombre;
                                ban = 2;
                            }
                            if (no_version != Version)
                            {
                                mensaje += "\n" + StringResources.msgVersIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + Version;
                                ban = 2;
                            }

                            string NombreAbreviado = ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().nombre.Substring(0, 1) + "." + ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().APaterno;
                            if (UsuarioRev != NombreAbreviado)
                            {
                                mensaje += "\n" + StringResources.msgUElabIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + NombreAbreviado;
                                ban = 2;
                            }
                            if (codformato != CodigoFormato)
                            {
                                // Cerrar Excel cuando se adjunta archivo que no corresponde al formato
                                ExcelApp.Visible = false;

                                if (ExcelWork != null)
                                    ExcelWork.Close(unknownType, unknownType, unknownType);

                                if (ExcelApp != null)
                                    ExcelApp.Quit();
                                // Retornar cuando el archivo no pertenece al formato

                                mensaje += "\n" + StringResources.ttlAlerta + StringResources.msgDocDifFormato;
                                return 4;
                            }
                        }
                        catch (Exception er)
                        {
                            mensaje = StringResources.msgFileIncorrect + er.Message;
                            // Cerrar Excel cuando se adjunta archivo que no corresponde al formato
                            ExcelApp.Visible = false;

                            if (ExcelWork != null)
                                ExcelWork.Close(unknownType, unknownType, unknownType);

                            if (ExcelApp != null)
                                ExcelApp.Quit();

                            // Retornar cuando el archivo no pertenece al formato
                            return 4;
                        }
                    }

                    if (ban != 2)
                    {
                        ExcelWork.Save();

                        short resPDF = excel2Pdf(pathExcel, pathPDF);

                        QuitarExcelPonerPDF(archivo, pathPDF);

                        if (resPDF != 0)
                        {
                            mensaje = StringResources.msgErrorConvertFile;
                            return 2;
                        }
                    }

                    ExcelWork.Save();
                    ExcelApp.Visible = false;

                    if (ExcelWork != null)
                        ExcelWork.Close(unknownType, unknownType, unknownType);

                    if (ExcelApp != null)
                        ExcelApp.Quit();

                    if (ban == 2)
                        return 2;

                }
                mensaje = StringResources.msgCorrectFile;
                return 1;
            }
            catch (Exception)
            {
                mensaje = StringResources.msgOcurrioError;
                return 3;
            }
        }

        /// <summary>
        /// 1 = Archivo correcto / 2 = Archivo incorrecto (errores de datos) / 3 = Ocurrió un error / 4 = El archivo adjuntado no pertenece al tipo de formato
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        private int validarJES(out string mensaje)
        {
            try
            {
                object unknownType = Type.Missing;
                int ban = 1;
                string CodigoFormato = "Código de formato:  : F-3571-R19-es";

                foreach (Archivo archivo in ListaDocumentos)
                {
                    mensaje = string.Empty;
                    string pathExcel = GetPathTempFile(archivo);

                    Archivo archivoPDF = archivo;
                    archivoPDF.ext = ".pdf";
                    archivo.ruta = @"/Images/p.png";
                    string pathPDF = GetPathTempFile(archivoPDF);

                    //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                    File.WriteAllBytes(pathExcel, archivo.archivo);

                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(pathExcel, true);

                    Microsoft.Office.Interop.Excel.Worksheet sheet1;
                    sheet1 = ExcelWork.Sheets[1];

                    if (sheet1.Name != "JES")
                    {
                        mensaje += StringResources.msgLaHoja1 + "JES";

                        ExcelApp.Visible = false;

                        if (ExcelWork != null)
                            ExcelWork.Close(unknownType, unknownType, unknownType);

                        if (ExcelApp != null)
                            ExcelApp.Quit();

                        return 4;
                    }

                    for (int i = 1; i <= ExcelWork.Sheets.Count; i++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Worksheet sheet;
                            sheet = ExcelWork.Sheets[i];

                            if (sheet.PageSetup.Pages.Count > 1)
                            {
                                mensaje += "\n" + "La Hoja " + ExcelWork.Sheets[i].Name + " " + " tiene " + sheet.PageSetup.Pages.Count + " páginas. " + StringResources.lblOnlyOnePage;
                                ban = 2;
                            }

                            bool ValidacionNumeracion = true;
                            //Evaluamos la numeración de las hojas
                            string Numeracion = Convert.ToString(sheet.Range["X7"].Value);

                            if (Numeracion != "Hoja " + i + " de " + ExcelWork.Sheets.Count)
                            {
                                ValidacionNumeracion = false;
                            }

                            if (ValidacionNumeracion == false)
                            {
                                mensaje += "\n" + StringResources.msgNumeracionIncorrecta + ExcelWork.Sheets[i].Name + " " + StringResources.msgDBCr + "Hoja " + i + " de " + ExcelWork.Sheets.Count;
                                ban = 2;
                            }

                            int VRevisar = 11;
                            string VersionRevisar = "VERSION_11";
                            string FechaRevisar = "FECHA_A11";
                            string UsuarioRevisar = "USUARIO_A11";

                            if (Convert.ToInt32(Version) <= VRevisar)
                            {
                                VersionRevisar = "VERSION_" + Version;
                                FechaRevisar = "FECHA_A" + Version;
                                UsuarioRevisar = "USUARIO_A" + Version;
                            }

                            string fecha = Convert.ToString(sheet.Range["FECHA_LIBERACION"].Value);
                            string descripcion = Convert.ToString(sheet.Range["DESCRIPCION"].Value);
                            string elaboro = Convert.ToString(sheet.Range["ELABORO"].Value);
                            string aprobo = Convert.ToString(sheet.Range["APROBO"].Value);
                            string reviso = Convert.ToString(sheet.Range["REVISO"].Value);
                            string codigo = Convert.ToString(sheet.Range["CODIGO"].Value);
                            string departamento = Convert.ToString(sheet.Range["PROCESO"].Value);
                            string codformato = Convert.ToString(sheet.Range["CODFORMATO"].Value);
                            string no_version = Convert.ToString(sheet.Range[VersionRevisar].Value);
                            string UsuarioRev = Convert.ToString(sheet.Range[UsuarioRevisar].Value);
                            string FechaRev = Convert.ToString(sheet.Range[FechaRevisar].Value);

                            //Validar fecha elaboración
                            DateTime date = Convert.ToDateTime(fecha);
                            if (date.Year != FechaFin.Year || date.Month != FechaFin.Month || date.Day != FechaFin.Day)
                            {
                                string mes = "";
                                mes = FechaFin.Month < 10 ? "0" + FechaFin.Month : "" + FechaFin.Month;

                                string dia = "";
                                dia = FechaFin.Day < 10 ? "0" + FechaFin.Day : "" + FechaFin.Day;

                                mensaje += "\n" + StringResources.msgFElaboraIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + FechaFin.Year + "-" + mes + "-" + dia;
                                ban = 2;
                            }

                            //Validar fecha revisión
                            DateTime date1 = Convert.ToDateTime(FechaRev);
                            if (date1.Year != FechaFin.Year || date1.Month != FechaFin.Month || date1.Day != FechaFin.Day)
                            {
                                string mes = "";
                                mes = FechaFin.Month < 10 ? "0" + FechaFin.Month : "" + FechaFin.Month;

                                string dia = "";
                                dia = FechaFin.Day < 10 ? "0" + FechaFin.Day : "" + FechaFin.Day;

                                mensaje += "\n" + StringResources.msgFReviIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + FechaFin.Year + "-" + mes + "-" + dia;
                                ban = 2;
                            }

                            string CadenaEvaluar = descripcion.Replace(" ", "");
                            if (!Regex.IsMatch(CadenaEvaluar, "^[a-zA-Z0-9-_,;.()áÁéÉíÍóÓúÚÜüñÑ]*$"))
                            {
                                mensaje += "\n" + StringResources.msgDescNotEspeciales;
                                ban = 2;
                            }
                            if (descripcion != Descripcion)
                            {
                                mensaje += "\n" + StringResources.msgDescIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + Descripcion;
                                ban = 2;
                            }
                            if (elaboro != ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto)
                            {
                                mensaje += "\n" + StringResources.msgUElabIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto;
                                ban = 2;
                            }
                            if (aprobo != ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto)
                            {
                                mensaje += "\n" + StringResources.msgUAproIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto;
                                ban = 2;
                            }

                            string UsuariosPermitido = NombreUsuarioAut.Replace(" ", "");
                            if (UsuariosPermitido == "SISTEMA")
                            {
                                mensaje += "\n" + StringResources.msgUNotSISTEMA;
                                ban = 2;
                            }
                            if (reviso != ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto)
                            {
                                mensaje += "\n" + StringResources.msgUAutoIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto;
                                ban = 2;
                            }
                            if (codigo != SelectedDocumento.nombre)
                            {
                                mensaje += "\n" + StringResources.msgCodIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + SelectedDocumento.nombre;
                                ban = 2;
                            }
                            if (no_version != Version)
                            {
                                mensaje += "\n" + StringResources.msgVersIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + Version;
                                ban = 2;
                            }

                            string NombreAbreviado = ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().nombre.Substring(0, 1) + "." + ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().APaterno;
                            if (UsuarioRev != NombreAbreviado)
                            {
                                mensaje += "\n" + StringResources.msgUElabIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + NombreAbreviado;
                                ban = 2;
                            }
                            if (codformato != CodigoFormato)
                            {
                                // Cerrar Excel cuando se adjunta archivo que no corresponde al formato
                                ExcelApp.Visible = false;

                                if (ExcelWork != null)
                                    ExcelWork.Close(unknownType, unknownType, unknownType);

                                if (ExcelApp != null)
                                    ExcelApp.Quit();

                                // Retornar cuando el archivo no pertenece al formato
                                mensaje += "\n" + StringResources.ttlAlerta + StringResources.msgDocDifFormato;
                                return 4;
                            }
                        }
                        catch (Exception er)
                        {
                            mensaje = StringResources.msgFileIncorrect + er.Message;
                            // Cerrar Excel cuando se adjunta archivo que no corresponde al formato
                            ExcelApp.Visible = false;

                            if (ExcelWork != null)
                                ExcelWork.Close(unknownType, unknownType, unknownType);

                            if (ExcelApp != null)
                                ExcelApp.Quit();

                            // Retornar cuando el archivo no pertenece al formato
                            return 4;
                        }
                    }

                    //
                    if (ban != 2)
                    {
                        //string qrFinal = GetPathTempFile(new Archivo { nombre = "tempOuputQR", numero = 1, ext = ".PNG" });
                        //generateQRCode(qrFinal);

                        //for (int i = 1; i <= ExcelWork.Sheets.Count; i++)
                        //{
                        //    Microsoft.Office.Interop.Excel.Worksheet sheet;
                        //    sheet = ExcelWork.Sheets[i];
                        //    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[2, 25];
                        //    float Left = (float)((double)oRange.Left);
                        //    float Top = (float)((double)oRange.Top);
                        //    const float ImageSize = 128;
                        //    sheet.Shapes.AddPicture(qrFinal, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                        //}

                        ExcelWork.Save();

                        short resPDF = excel2Pdf(pathExcel, pathPDF);

                        QuitarExcelPonerPDF(archivo, pathPDF);

                        if (resPDF != 0)
                        {
                            mensaje = StringResources.msgErrorConvertFile;
                            return 2;
                        }
                    }

                    ExcelWork.Save();
                    ExcelApp.Visible = false;

                    if (ExcelWork != null)
                        ExcelWork.Close(unknownType, unknownType, unknownType);

                    if (ExcelApp != null)
                        ExcelApp.Quit();

                    if (ban == 2)
                        return 2;

                }
                //if (ListaDocumentos.Count == 0)
                //{
                //    mensaje = StringResources.msgNotFoundFile;
                //    return 2;
                //}
                mensaje = StringResources.msgCorrectFile;
                return 1;
            }
            catch (Exception)
            {
                mensaje = StringResources.msgOcurrioError;
                ListaDocumentos.Clear();
                return 3;
            }

        }

        /// <summary>
        /// 1 = Archivo correcto / 2 = Archivo incorrecto (errores de datos) / 3 = Ocurrió un error / 4 = El archivo adjuntado no pertenece al tipo de formato
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        private int validarHII(out string mensaje)
        {
            try
            {
                object unknownType = Type.Missing;
                int ban = 1;
                string CodigoFormato = "F-3571-01HII-es\nVersión 03";

                foreach (Archivo archivo in ListaDocumentos)
                {
                    mensaje = string.Empty;
                    string pathExcel = GetPathTempFile(archivo);

                    Archivo archivoPDF = archivo;
                    archivoPDF.ext = ".pdf";
                    archivo.ruta = @"/Images/p.png";
                    string pathPDF = GetPathTempFile(archivoPDF);

                    //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                    File.WriteAllBytes(pathExcel, archivo.archivo);

                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(pathExcel, true);

                    Microsoft.Office.Interop.Excel.Worksheet sheet1;
                    sheet1 = ExcelWork.Sheets[1];

                    if (sheet1.Name != "HII")
                    {
                        mensaje += StringResources.msgLaHoja1 + "HII";

                        ExcelApp.Visible = false;

                        if (ExcelWork != null)
                            ExcelWork.Close(unknownType, unknownType, unknownType);

                        if (ExcelApp != null)
                            ExcelApp.Quit();

                        return 4;
                    }

                    for (int i = 1; i <= ExcelWork.Sheets.Count; i++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Worksheet sheet;
                            sheet = ExcelWork.Sheets[i];

                            if (sheet.PageSetup.Pages.Count > 1)
                            {
                                mensaje += "\n" + "La Hoja " + ExcelWork.Sheets[i].Name + " " + " tiene " + sheet.PageSetup.Pages.Count + " páginas. " + StringResources.lblOnlyOnePage;
                                ban = 2;
                            }

                            bool ValidacionNumeracion = true;
                            //Evaluamos la numeración de las hojas
                            string Numeracion = Convert.ToString(sheet.Range["HOJAS"].Value);

                            if (Numeracion != "Hoja " + i + " de " + ExcelWork.Sheets.Count)
                            {
                                ValidacionNumeracion = false;
                            }

                            if (ValidacionNumeracion == false)
                            {
                                mensaje += "\n" + StringResources.msgNumeracionIncorrecta + ExcelWork.Sheets[i].Name + " " + StringResources.msgDBCr + "Hoja " + i + " de " + ExcelWork.Sheets.Count;
                                ban = 2;
                            }

                            string fecharevision = Convert.ToString(sheet.Range["FECHA_ACTUAL"].Value);
                            string fechaemision = Convert.ToString(sheet.Range["FECHA_V1"].Value);
                            string descripcion = Convert.ToString(sheet.Range["DESCRIPCION"].Value);
                            string elaboro = Convert.ToString(sheet.Range["NOMBRE_ELABORO"].Value);
                            string reviso = Convert.ToString(sheet.Range["NOMBRE_REVISO"].Value);
                            string codigo = Convert.ToString(sheet.Range["CODIGO"].Value);
                            string departamento = Convert.ToString(sheet.Range["NOMBRE_DEPARTAMENTO"].Value);
                            string no_version = Convert.ToString(sheet.Range["VERSION_ACTUAL"].Value);
                            string codformato = Convert.ToString(sheet.Range["CODFORMATO"].Value);

                            //Validar fecha revisión
                            DateTime date = Convert.ToDateTime(fecharevision);
                            if (date.Year != FechaFin.Year || date.Month != FechaFin.Month || date.Day != FechaFin.Day)
                            {
                                string mes = "";
                                mes = FechaFin.Month < 10 ? "0" + FechaFin.Month : "" + FechaFin.Month;

                                string dia = "";
                                dia = FechaFin.Day < 10 ? "0" + FechaFin.Day : "" + FechaFin.Day;

                                mensaje += "\n" + StringResources.msgFReviIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + FechaFin.Year + "-" + mes + "-" + dia;
                                ban = 2;
                            }
                            //Validar fecha emisión
                            DateTime date1 = Convert.ToDateTime(fechaemision);
                            string FechaPrimerVersion = string.Empty;

                            if (id_documento != 0)
                            {
                                FechaPrimerVersion = DataManagerControlDocumentos.GetFechaPrimeraVersion(id_documento);
                            }
                            else
                            {
                                string mes = "";
                                mes = FechaFin.Month < 10 ? "0" + FechaFin.Month : "" + FechaFin.Month;

                                string dia = "";
                                dia = FechaFin.Day < 10 ? "0" + FechaFin.Day : "" + FechaFin.Day;

                                FechaPrimerVersion = FechaFin.Year + "-" + mes + "-" + dia;
                            }

                            DateTime fechaElaboracion = Convert.ToDateTime(FechaPrimerVersion);

                            if (date1.Year != fechaElaboracion.Year || date1.Month != fechaElaboracion.Month || date1.Day != fechaElaboracion.Day)
                            {
                                string mes = "";
                                mes = fechaElaboracion.Month < 10 ? "0" + fechaElaboracion.Month : "" + fechaElaboracion.Month;

                                string dia = "";
                                dia = fechaElaboracion.Day < 10 ? "0" + fechaElaboracion.Day : "" + fechaElaboracion.Day;

                                mensaje += "\n" + StringResources.msgFEmisIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + fechaElaboracion.Year + "-" + mes + "-" + dia;
                                ban = 2;
                            }

                            string CadenaEvaluar = descripcion.Replace(" ", "");
                            if (!Regex.IsMatch(CadenaEvaluar, "^[a-zA-Z0-9-_,;.()áÁéÉíÍóÓúÚÜüñÑ]*$"))
                            {
                                mensaje += "\n" + StringResources.msgDescNotEspeciales;
                                ban = 2;
                            }
                            if (descripcion != Descripcion)
                            {
                                mensaje += "\n" + StringResources.msgDescIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + Descripcion;
                                ban = 2;
                            }

                            if (elaboro != ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto)
                            {
                                mensaje += "\n" + StringResources.msgUElabIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto;
                                ban = 2;
                            }

                            string UsuariosPermitido = NombreUsuarioAut.Replace(" ", "");
                            if (UsuariosPermitido == "SISTEMA")
                            {
                                mensaje += "\n" + StringResources.msgUNotSISTEMA;
                                ban = 2;
                            }
                            if (reviso != ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto)
                            {
                                mensaje += "\n" + StringResources.msgUAutoIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto;
                                ban = 2;
                            }

                            if (codigo != SelectedDocumento.nombre)
                            {
                                mensaje += "\n" + StringResources.msgCodIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + SelectedDocumento.nombre;
                                ban = 2;
                            }
                            if (no_version != Version)
                            {
                                mensaje += "\n" + StringResources.msgVersIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + Version;
                                ban = 2;
                            }
                            if (codformato != CodigoFormato)
                            {
                                // Cerrar Excel cuando se adjunta archivo que no corresponde al formato
                                ExcelApp.Visible = false;

                                if (ExcelWork != null)
                                    ExcelWork.Close(unknownType, unknownType, unknownType);

                                if (ExcelApp != null)
                                    ExcelApp.Quit();

                                // Retornar cuando el archivo no pertenece al formato
                                //mensaje += "\n" + StringResources.ttlAlerta + StringResources.msgDocDifFormato;
                                return 4;
                            }
                        }
                        catch (Exception er)
                        {
                            mensaje = StringResources.msgFileIncorrect + er.Message;
                            // Cerrar Excel cuando se adjunta archivo que no corresponde al formato
                            ExcelApp.Visible = false;

                            if (ExcelWork != null)
                                ExcelWork.Close(unknownType, unknownType, unknownType);

                            if (ExcelApp != null)
                                ExcelApp.Quit();

                            // Retornar cuando el archivo no pertenece al formato
                            return 4;
                        }
                    }

                    ExcelApp.Visible = false;

                    if (ExcelWork != null)
                        ExcelWork.Close(unknownType, unknownType, unknownType);

                    if (ExcelApp != null)
                        ExcelApp.Quit();

                    if (ban == 2)
                        return 2;

                    short resPDF = excel2Pdf(pathExcel, pathPDF);

                    if (resPDF == 0)
                    {
                        string pdfFinal = GetPathTempFile(new Archivo { nombre = "tempOuputPdf", numero = 1 });
                        string qrFinal = GetPathTempFile(new Archivo { nombre = "tempOuputQR", numero = 1 });
                        //generateQRCode(qrFinal);

                        //insertQR(pathPDF, pdfFinal, qrFinal);

                        QuitarExcelPonerPDF(archivo, pathPDF);
                    }
                    else
                    {
                        mensaje = StringResources.msgErrorConvertFile;
                        return 2;
                    }
                }

                if (ListaDocumentos.Count == 0)
                {
                    mensaje = StringResources.msgNotFoundFile;
                    return 2;
                }
                mensaje = StringResources.msgCorrectFile;
                return 1;
            }
            catch (Exception)
            {
                mensaje = StringResources.msgOcurrioError;
                return 3;
            }
        }

        /// <summary>
        /// 1 = Archivo correcto / 2 = Archivo incorrecto (errores de datos) / 3 = Ocurrió un error / 4 = El archivo adjuntado no pertenece al tipo de formato
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        private int validarAVY(out string mensaje)
        {
            try
            {
                object unknownType = Type.Missing;
                int ban = 1;

                foreach (Archivo archivo in ListaDocumentos)
                {
                    mensaje = string.Empty;
                    string pathExcel = GetPathTempFile(archivo);

                    Archivo archivoPDF = archivo;
                    archivoPDF.ext = ".pdf";
                    archivo.ruta = @"/Images/p.png";
                    string pathPDF = GetPathTempFile(archivoPDF);

                    // Crea una archivo temporal, escribe en él los bytes extraídos de la BD
                    File.WriteAllBytes(pathExcel, archivo.archivo);

                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(pathExcel, true);

                    Microsoft.Office.Interop.Excel.Worksheet sheet1;
                    sheet1 = ExcelWork.Sheets[1];

                    if (sheet1.Name != "AYUDA_VISUAL")
                    {
                        mensaje += StringResources.msgLaHoja1 + "AYUDA_VISUAL";

                        ExcelApp.Visible = false;

                        if (ExcelWork != null)
                            ExcelWork.Close(unknownType, unknownType, unknownType);

                        if (ExcelApp != null)
                            ExcelApp.Quit();

                        return 4;
                    }

                    for (int i = 1; i <= ExcelWork.Sheets.Count; i++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Worksheet sheet;
                            sheet = ExcelWork.Sheets[i];

                            if (sheet.PageSetup.Pages.Count > 1)
                            {
                                mensaje += "\n" + "La Hoja " + ExcelWork.Sheets[i].Name + " " + " tiene " + sheet.PageSetup.Pages.Count + " páginas. " + StringResources.lblOnlyOnePage;
                                ban = 2;
                            }

                            bool ValidacionNumeracion = true;
                            //Evaluamos la numeración de las hojas
                            string Numeracion = Convert.ToString(sheet.Range["NUMERACION"].Value);

                            if (Numeracion != "Hoja:(" + i + "/" + ExcelWork.Sheets.Count + ")")
                            {
                                ValidacionNumeracion = false;
                            }

                            if (ValidacionNumeracion == false)
                            {
                                mensaje += "\n" + StringResources.msgNumeracionIncorrecta + ExcelWork.Sheets[i].Name + " " + StringResources.msgDBCr + "Hoja:(" + i + "/" + ExcelWork.Sheets.Count + ")";
                                ban = 2;
                            }

                            string codigo = Convert.ToString(sheet.Range["CODIGO"].Value);
                            string no_version = Convert.ToString(sheet.Range["Version"].Value);
                            string fechaelaboracion = Convert.ToString(sheet.Range["FECHA_ELABORACION"].Value);
                            string fecharevision = Convert.ToString(sheet.Range["FECHA_REVISION"].Value);
                            string elaboro = Convert.ToString(sheet.Range["ELABORO"].Value);
                            string aprobo = Convert.ToString(sheet.Range["APROBO"].Value);
                            string departamento = Convert.ToString(sheet.Range["NOMBRE_DEPARTAMENTO"].Value);

                            //Validar fecha revisión
                            DateTime date = Convert.ToDateTime(fecharevision);
                            if (date.Year != FechaFin.Year || date.Month != FechaFin.Month || date.Day != FechaFin.Day)
                            {
                                string mes = "";
                                mes = FechaFin.Month < 10 ? "0" + FechaFin.Month : "" + FechaFin.Month;

                                string dia = "";
                                dia = FechaFin.Day < 10 ? "0" + FechaFin.Day : "" + FechaFin.Day;

                                mensaje += "\n" + StringResources.msgFReviIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + FechaFin.Year + "-" + mes + "-" + dia;
                                ban = 2;
                            }

                            //Validar fecha elaboración
                            DateTime date1 = Convert.ToDateTime(fechaelaboracion);
                            string FechaPrimerVersion = string.Empty;

                            if (id_documento != 0)
                            {
                                FechaPrimerVersion = DataManagerControlDocumentos.GetFechaPrimeraVersion(id_documento);
                            }
                            else
                            {
                                string mes = "";
                                mes = FechaFin.Month < 10 ? "0" + FechaFin.Month : "" + FechaFin.Month;

                                string dia = "";
                                dia = FechaFin.Day < 10 ? "0" + FechaFin.Day : "" + FechaFin.Day;

                                FechaPrimerVersion = FechaFin.Year + "-" + mes + "-" + dia;
                            }

                            DateTime fechaElaboracion = Convert.ToDateTime(FechaPrimerVersion);

                            if (date1.Year != fechaElaboracion.Year || date1.Month != fechaElaboracion.Month || date1.Day != fechaElaboracion.Day)
                            {
                                string mes = "";
                                mes = fechaElaboracion.Month < 10 ? "0" + fechaElaboracion.Month : "" + fechaElaboracion.Month;

                                string dia = "";
                                dia = fechaElaboracion.Day < 10 ? "0" + fechaElaboracion.Day : "" + fechaElaboracion.Day;

                                mensaje += "\n" + StringResources.msgFElaboraIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + fechaElaboracion.Year + "-" + mes + "-" + dia;
                                ban = 2;
                            }

                            if (elaboro != ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto)
                            {
                                mensaje += "\n" + StringResources.msgUElabIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto;
                                ban = 2;
                            }
                            if (aprobo != ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto)
                            {
                                mensaje += "\n" + StringResources.msgUAproIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto;
                                ban = 2;
                            }
                            if (codigo != SelectedDocumento.nombre)
                            {
                                mensaje += "\n" + StringResources.msgCodIncorrecto + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + SelectedDocumento.nombre;
                                ban = 2;
                            }
                            if (no_version != Version)
                            {
                                mensaje += "\n" + StringResources.msgVersIncorrecta + ExcelWork.Sheets[i].Name + StringResources.msgDBCr + Version;
                                ban = 2;
                            }
                        }
                        catch (Exception er)
                        {
                            mensaje = StringResources.msgFileIncorrect + er.Message;
                            // Cerrar Excel cuando se adjunta archivo que no corresponde al formato
                            ExcelApp.Visible = false;

                            if (ExcelWork != null)
                                ExcelWork.Close(unknownType, unknownType, unknownType);

                            if (ExcelApp != null)
                                ExcelApp.Quit();

                            // Retornar cuando el archivo no pertenece al formato
                            return 4;
                        }
                    }

                    ExcelApp.Visible = false;

                    if (ExcelWork != null)
                        ExcelWork.Close(unknownType, unknownType, unknownType);

                    if (ExcelApp != null)
                        ExcelApp.Quit();

                    if (ban == 2)
                        return 2;

                    short resPDF = excel2Pdf(pathExcel, pathPDF);

                    if (resPDF == 0)
                    {
                        string pdfFinal = GetPathTempFile(new Archivo { nombre = "tempOuputPdf", numero = 1 });
                        string qrFinal = GetPathTempFile(new Archivo { nombre = "tempOuputQR", numero = 1 });
                        //generateQRCode(qrFinal);

                        //insertQR(pathPDF, pdfFinal, qrFinal);

                        QuitarExcelPonerPDF(archivo, pathPDF);
                    }
                    else
                    {
                        mensaje = StringResources.msgErrorConvertFile;
                        return 2;
                    }
                }

                if (ListaDocumentos.Count == 0)
                {
                    mensaje = StringResources.msgNotFoundFile;
                    return 2;
                }
                mensaje = StringResources.msgCorrectFile;
                return 1;
            }
            catch (Exception)
            {
                mensaje = StringResources.msgOcurrioError;
                return 3;
            }
        }

        //private void generateQRCode(string path)
        //{
        //    string codigo = string.Empty;

        //    bool ban = true;

        //    while (ban)
        //    {
        //        codeValidation = Module.GetRandomString(8);
        //        ban = DataManagerControlDocumentos.ExistsCodeValidation(codeValidation);
        //    }

        //    // codigo = SelectedDocumento.nombre + " " + Version + " " + codeValidation + " ";
        //    codigo = codeValidation + "*";

        //    //Encriptamos el codigo.
        //    string codigoEncriptado = Seguridad.Encriptar(codigo);

        //    var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
        //    var qrCode = qrEncoder.Encode(codigoEncriptado);

        //    var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
        //    using (var stream = new FileStream(path, FileMode.Create))
        //        renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
        //}

        //private bool insertQR(string pathPDF, string pathPDFOuput, string imgCode)
        //{
        //    using (Stream inputPdfStream = new FileStream(pathPDF, FileMode.Open, FileAccess.Read, FileShare.Read))
        //    using (Stream outputPdfStream = new FileStream(pathPDFOuput, FileMode.Create, FileAccess.Write, FileShare.None))
        //    {
        //        var reader = new PdfReader(inputPdfStream);
        //        using (var stamper = new PdfStamper(reader, outputPdfStream))
        //        {
        //            var pages = reader.NumberOfPages;

        //            for (int i = 1; i <= pages; i++)
        //            {
        //                var pdfContentByte = stamper.GetOverContent(i);
        //                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(new FileStream(imgCode, FileMode.Open, FileAccess.Read, FileShare.Read));
        //                image.ScaleAbsoluteHeight(40);
        //                image.ScaleAbsoluteWidth(40);

        //                switch (id_tipo)
        //                {
        //                    //Posición Código QR en documento
        //                    //JES
        //                    case 1015:
        //                        image.SetAbsolutePosition(940, 723);
        //                        break;
        //                    //HOE
        //                    case 2:
        //                        image.SetAbsolutePosition(940, 728);
        //                        break;
        //                    //HII
        //                    case 1002:
        //                        image.SetAbsolutePosition(860, 732);
        //                        break;
        //                    //AVY
        //                    case 1004:
        //                        // Vertical
        //                        if (id_recurso == 1054)
        //                        {
        //                            image.SetAbsolutePosition(40, 730);
        //                        }
        //                        // Horizontal
        //                        else
        //                        {
        //                            image.SetAbsolutePosition(50, 532);
        //                        }
        //                        break;
        //                }
        //                if (id_tipo != 1004)
        //                    pdfContentByte.AddImage(image);
        //            }
        //            stamper.Close();
        //        }
        //    }

        //    return true;
        //}

        public short excel2Pdf(string originalXlsPath, string pdfPath)
        {
            Console.WriteLine("Class: " + GetType() + " Method: " + MethodBase.GetCurrentMethod().Name + " Started ");
            short convertExcel2PdfResult = -1;

            // Create COM Objects
            Microsoft.Office.Interop.Excel.Application excelApplication = null;
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = null;
            object unknownType = Type.Missing;

            // Create new instance of Excel
            try
            {
                //open excel application
                excelApplication = new Microsoft.Office.Interop.Excel.Application
                {
                    ScreenUpdating = false,
                    DisplayAlerts = false
                };

                //open excel sheet
                if (excelApplication != null)

                    excelWorkbook = excelApplication.Workbooks.Open(originalXlsPath, unknownType, unknownType,
                    unknownType, unknownType, unknownType,
                    unknownType, unknownType, unknownType,
                    unknownType, unknownType, unknownType,
                    unknownType, unknownType, unknownType);

                if (excelWorkbook != null)
                {

                    // Call Excel's native export function (valid in Office 2007 and Office 2010, AFAIK)
                    excelWorkbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF,
                    pdfPath,
                    unknownType, unknownType, unknownType, unknownType, unknownType,
                    unknownType, unknownType);

                    convertExcel2PdfResult = 0;
                }
                else
                {
                    Console.WriteLine("Error occured for conversion of office excel to PDF ");
                    convertExcel2PdfResult = 504;
                }
            }

            catch (Exception exExcel2Pdf)
            {
                Console.WriteLine("Error occured for conversion of office excel to PDF, Exception: ", exExcel2Pdf);
                convertExcel2PdfResult = 504;
            }
            finally
            {
                // Close the workbook, quit the Excel, and clean up regardless of the results...
                if (excelWorkbook != null)
                    excelWorkbook.Close(unknownType, unknownType, unknownType);

                if (excelApplication != null) excelApplication.Quit();

                //Util.releaseObject(excelWorkbook);
                //Util.releaseObject(excelApplication);
            }

            Console.WriteLine("Class: " + GetType() + " Method: " + MethodBase.GetCurrentMethod().Name + " Ended ");
            return convertExcel2PdfResult;
        }

        private async void generarArchivo()
        {
            DialogService dialog = new DialogService();
            if (!string.IsNullOrEmpty(nombre))
            {
                if (id_tipo == 1015 || id_tipo == 2 || id_tipo == 1002 || id_tipo == 1004)
                {
                    if (VentanaProcedencia == "DocumentoLiberado")
                    {
                        if (VersionGenerada)
                        {
                            if (!string.IsNullOrEmpty(Descripcion))
                            {
                                if (ValidarNombreDescripcion())
                                {
                                    if (ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault() != null)
                                    {
                                        ObservableCollection<Archivo> recursos = new ObservableCollection<Archivo>();

                                        switch (id_tipo)
                                        {
                                            case 1015:
                                                //Si es de tipo JES se trae el formato correspondiente
                                                recursos = DataManagerControlDocumentos.GetRecursosTipoDocumento(1015);
                                                break;
                                            case 2:
                                                //si es de tipo HOE se trae el formato correspondiente
                                                recursos = DataManagerControlDocumentos.GetRecursosTipoDocumento(2);
                                                break;
                                            case 1002:
                                                //si es de tipo HII se trae el formato correspondiente
                                                recursos = DataManagerControlDocumentos.GetRecursosTipoDocumento(1002);
                                                break;
                                            case 1004:
                                                {
                                                    //Inicializamos los servicios de dialog
                                                    DialogService dialogService = new DialogService();

                                                    //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                                                    MetroDialogSettings setting = new MetroDialogSettings();
                                                    setting.AffirmativeButtonText = StringResources.lblVertical;
                                                    setting.NegativeButtonText = StringResources.lblHorizontal;

                                                    //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local
                                                    MessageDialogResult result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgElegirFormato, setting, MessageDialogStyle.AffirmativeAndNegative);

                                                    //Vertical
                                                    if (result == MessageDialogResult.Affirmative)
                                                    {
                                                        id_recurso = 1054;
                                                    }
                                                    //Horizontal
                                                    else
                                                    {
                                                        id_recurso = 1055;
                                                    }

                                                    //Si es de tipo AVY se trae el formato correspondiente
                                                    recursos = DataManagerControlDocumentos.GetRecursoByIdRecurso(id_recurso);
                                                }
                                                break;
                                        }

                                        Archivo TipoFormato = recursos[0];
                                        string path = GetPathTempFile(TipoFormato);
                                        File.WriteAllBytes(path, TipoFormato.archivo);
                                        string NombreAbreviadoPersonaCreo = ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().nombre.Substring(0, 1) + "." + ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().APaterno;

                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFormatoGCorrect);

                                        if (id_tipo == 1002)
                                        {
                                            ImportExcel.ExportFormatoHII(path, FechaFin, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                        }
                                        if (id_tipo == 1004)
                                        {
                                            ImportExcel.ExportFormatoAVY(path, FechaFin, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                        }
                                        if (id_tipo == 2)
                                        {
                                            ImportExcel.ExportFormatoHOE(path, FechaFin, NombreAbreviadoPersonaCreo, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                        }
                                        if (id_tipo == 1015)
                                        {
                                            ImportExcel.ExportFormatoJES(path, FechaFin, NombreAbreviadoPersonaCreo, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                        }
                                    }
                                    else
                                    {
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSelectUserAuto);
                                    }

                                }
                                else
                                {
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgNombreDescripcionNoIgual);
                                }
                            }
                            else
                            {
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInDescDoc);
                            }
                        }
                        else
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgGenerar1roVers);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Descripcion))
                        {
                            if (ValidarNombreDescripcion())
                            {
                                if (ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault() != null)
                                {
                                    ObservableCollection<Archivo> recursos = new ObservableCollection<Archivo>();

                                    switch (id_tipo)
                                    {
                                        case 1015:
                                            //Si es de tipo JES se trae el formato correspondiente
                                            recursos = DataManagerControlDocumentos.GetRecursosTipoDocumento(1015);
                                            break;
                                        case 2:
                                            //si es de tipo HOE se trae el formato correspondiente
                                            recursos = DataManagerControlDocumentos.GetRecursosTipoDocumento(2);
                                            break;
                                        case 1002:
                                            //si es de tipo hii se trae el formato correspondiente
                                            recursos = DataManagerControlDocumentos.GetRecursosTipoDocumento(1002);
                                            break;
                                        case 1004:
                                            {
                                                //Inicializamos los servicios de dialog
                                                DialogService dialogService = new DialogService();

                                                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                                                MetroDialogSettings setting = new MetroDialogSettings();
                                                setting.AffirmativeButtonText = StringResources.lblVertical;
                                                setting.NegativeButtonText = StringResources.lblHorizontal;

                                                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                                                MessageDialogResult result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgElegirFormato, setting, MessageDialogStyle.AffirmativeAndNegative);

                                                //Vertical
                                                if (result == MessageDialogResult.Affirmative)
                                                {
                                                    id_recurso = 1054;
                                                }
                                                //Horizontal
                                                else
                                                {
                                                    id_recurso = 1055;
                                                }

                                                //Si es de tipo AVY se trae el formato correspondiente
                                                recursos = DataManagerControlDocumentos.GetRecursoByIdRecurso(id_recurso);
                                            }
                                            break;
                                    }

                                    Archivo formato = recursos[0];
                                    string path = GetPathTempFile(formato);
                                    File.WriteAllBytes(path, formato.archivo);
                                    string NombreAbreviadoPersonaCreo = ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().nombre.Substring(0, 1) + "." + ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().APaterno;

                                    if (id_tipo == 1002)
                                    {
                                        ImportExcel.ExportFormatoHII(path, FechaFin, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                    }
                                    if (id_tipo == 1004)
                                    {
                                        ImportExcel.ExportFormatoAVY(path, FechaFin, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                    }
                                    if (id_tipo == 2)
                                    {
                                        ImportExcel.ExportFormatoHOE(path, FechaFin, NombreAbreviadoPersonaCreo, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                    }
                                    if (id_tipo == 1015)
                                    {
                                        ImportExcel.ExportFormatoJES(path, FechaFin, NombreAbreviadoPersonaCreo, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                    }
                                }
                                else
                                {
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSelectUserAuto);
                                }
                            }
                            else
                            {
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgNombreDescripcionNoIgual);
                            }
                        }
                        else
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInDescDoc);
                        }
                    }
                }
                else
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSoloFormatos);
                }
            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSelectNumDoc);
            }

        }

        /// <summary>
        /// Método que obtiene el nombre completo del usuario de acuerdo al id
        /// Se guarda el nombre completo para mostrar en mensaje de la información del documento
        /// </summary>
        private void getUsuarioAutorizo()
        {
            if (usuarioAutorizo != null)
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
        /// Método para cambiar el estatus de un archivo a pendiente por corregir
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
                        objVersion.fecha_version = Fecha;
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
                    objVersion.fecha_version = Fecha;
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
                    foreach (var item in ListaDocumentos)
                    {
                        //Manda a la función para eliminar los archivos
                        int n = DataManagerControlDocumentos.DeleteArchivo(item);
                    }

                    Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
                    //Se asigna el id
                    objVersion.id_version = idVersion;
                    objVersion.no_version = version;

                    //string mensaje = "Se elimina versión " + Version + ",Se regresa a versión anterior";
                    string mensaje = StringResources.msgEliminaVers + Version + StringResources.msgRegresaVers;

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
        /// Método que llena la lista para visualizar los archivos de la versión y permite adjuntar un documento automáticamente cuando ya se corrigió el archivo
        /// </summary>
        private async void AdjuntarArchivo(string filename = "")
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController AsyncProgress;

            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //si no se ha seleccionado un documento se manda un mensaje indicando que se tiene que seleccionar un documento
            //Si no se ha seleccionado un documento, usuario autorizo o la descripción esta vacía, se manda un mensaje indicando que se tienen que llenar todos los campos
            if (_selectedDocumento == null || string.IsNullOrEmpty(Descripcion) || usuarioAutorizo == null)
            {
                //await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSeleccioneArchivo);
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgLlenaTodosCampos);
            }
            else
            {
                //Validamos que la descripción no sea igual al nombre del documento.
                //Si es igual, impide adjuntar archivos
                if (ValidarNombreDescripcion())
                {
                    //Si la lista no tiene otro archivo adjunto
                    if (ListaDocumentos.Count == 0)
                    {

                        //Abre la ventana de explorador de archivos
                        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                        //Filtar los documentos por extensión
                        if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014 || id_tipo == 1011)
                            dlg.Filter = "Word (97-2003)|*.doc";
                        else
                        {
                            if (id_tipo == 1015 || id_tipo == 2 || id_tipo == 1002 || id_tipo == 1004)
                            {
                                dlg.Filter = "Excel Files (.xlsm, .xlsx)|*.xlsm; *.xlsx";
                            }
                            else
                            {
                                dlg.Filter = "PDF Files (.pdf)|*.pdf";
                            }
                        }

                        Nullable<bool> result;

                        //Permite decidir si se debe o no, mostrar el explorador de archivos
                        if (string.IsNullOrEmpty(filename))
                        {
                            // Mostrar el explorador de archivos
                            result = dlg.ShowDialog();
                        }
                        else
                        {
                            result = true;
                            dlg.FileName = filename;
                        }

                        //Se crea el objeto de tipo archivo
                        Archivo obj = new Archivo();

                        // Si fue seleccionado un documento
                        if (result == true)
                        {
                            try
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

                                    //Sumamos 1 al contador que tenga la lista de documentos
                                    obj.numero = ListaDocumentos.Count + 1;

                                    //Si el archivo tiene extensión pdf
                                    if (obj.ext == ".pdf")
                                    {
                                        //asigna la imagen del pdf al objeto
                                        obj.ruta = @"/Images/p.png";
                                    }
                                    else
                                    {
                                        if (obj.ext == ".xlsm" || obj.ext == ".xlsx")
                                        {
                                            obj.ruta = @"/Images/E.jpg";
                                        }
                                        else
                                        {
                                            //Si es archivo de word asigna la imagen correspondiente.
                                            obj.ruta = @"/Images/w.png";
                                        }
                                    }

                                    //Verificamos de nuevo que se hayan insertado los tipos de archivos correspondiente al tipo de documento
                                    if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014 || id_tipo == 1011)
                                    {
                                        //si el archivo es igual a cualquiera de los id anteriores se comprueba que sea un archivo .doc
                                        if (obj.ext == ".doc")
                                        {
                                            //si se agrego el archivo correspondiente lo agregamos a la lista temporal
                                            ListaDocumentos.Add(obj);
                                            //deshabilitamos el boton de agregar archivos
                                            BttnArchivos = false;
                                        }//si no es archivo .doc se manda un mensaje y se elimina
                                        else
                                        {
                                            //si se llegara a insertar un tipo de archivo que no corresponde se manda un mensaje indicando que tipo de archivo se debe adjuntar
                                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertarTipoArchivoDOC);
                                            //se borra de la lista temporal
                                            Lista.Clear();
                                            //se habilita el boton para adjuntar archivos
                                            BttnArchivos = true;
                                        }
                                    }
                                    else
                                    {
                                        //Verificamos de nuevo que se hayan insertado los tipos de archivos correspondiente al tipo de documento
                                        if (id_tipo == 1015 || id_tipo == 2 || id_tipo == 1002 || id_tipo == 1004)
                                        {
                                            //Se comprueba que se haya insertado una hoja de calculo
                                            if (obj.ext == ".xlsm" || obj.ext == ".xlsx")
                                            {
                                                ListaDocumentos.Add(obj);

                                                string mensaje;

                                                //Mandamos llamar al método que revisa que el documento este correcto
                                                int r = validarArchivo(out mensaje);

                                                //Si el archivo esta incorrecto, se corrige automáticamente
                                                if (r == 2)
                                                {
                                                    if (mensaje.Contains(StringResources.lblOnlyOnePage))
                                                    {
                                                        //se elimina de la lista temporal
                                                        ListaDocumentos.Clear();
                                                        //se habilita el boton para poder adjuntar un archivo
                                                        BttnArchivos = true;

                                                        setting.AffirmativeButtonText = StringResources.lblManualmente;

                                                        //Modo de corregir un documento
                                                        MessageDialogResult resp = await dialog.SendMessage(StringResources.ttlAlerta, mensaje + "\n\n" + StringResources.msgModoCorregir, setting, MessageDialogStyle.Affirmative);

                                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCorrigeArchivo);
                                                    }
                                                    else
                                                    {
                                                        //se elimina de la lista temporal
                                                        ListaDocumentos.Clear();
                                                        //se habilita el boton para poder adjuntar un archivo
                                                        BttnArchivos = true;

                                                        setting.AffirmativeButtonText = StringResources.lblAutomaticamente;
                                                        setting.NegativeButtonText = StringResources.lblManualmente;

                                                        //Modo de corregir un documento
                                                        MessageDialogResult resp = await dialog.SendMessage(StringResources.ttlAlerta, mensaje + "\n\n" + StringResources.msgModoCorregir, setting, MessageDialogStyle.AffirmativeAndNegative);

                                                        string nfilename = dlg.FileName;
                                                        string path = nfilename;
                                                        string NombreAbreviadoPersonaCreo = ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().nombre.Substring(0, 1) + "." + ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().APaterno;

                                                        //Métodos para corregir los archivos según el tipo
                                                        if (resp == MessageDialogResult.Affirmative)
                                                        {
                                                            if (id_tipo == 1004)
                                                            {
                                                                ImportExcel.ExportAVYCorrecto(path, FechaFin, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                                                AdjuntarArchivo(path);
                                                            }
                                                            if (id_tipo == 2)
                                                            {
                                                                ImportExcel.ExportHOECorrecto(path, FechaFin, NombreAbreviadoPersonaCreo, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                                                AdjuntarArchivo(path);
                                                            }
                                                            if (id_tipo == 1015)
                                                            {
                                                                ImportExcel.ExportJESCorrecto(path, FechaFin, NombreAbreviadoPersonaCreo, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                                                AdjuntarArchivo(path);
                                                            }
                                                            if (id_tipo == 1002)
                                                            {
                                                                ImportExcel.ExportHIICorrecto(path, FechaFin, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                                                AdjuntarArchivo(path);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCorrigeArchivo);
                                                        }
                                                    }
                                                }
                                                else //si el archivo insertado corresponde con el que se pide entramos al else
                                                {   // Si el archivo es correcto...
                                                    if (r == 1)
                                                    {
                                                        if (AdjuntarDocumento || DocumentoNuevo)
                                                        {
                                                            //si el registro ya esta liberado y se busca hacer una nueva version entramos aqui
                                                            guardarControl("FORMATO DEL SISTEMA");
                                                        }
                                                        else
                                                        {
                                                            //si estamos en la ventana de pendiente por corregir entramos aqui
                                                            CreadoPendienteXLiberar();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //se elimina de la lista temporal
                                                        ListaDocumentos.Clear();
                                                        //se habilita el boton para poder adjuntar un archivo
                                                        BttnArchivos = true;

                                                        // Si ocurrió un error
                                                        if (r == 3)
                                                        {
                                                            // Mandar mensaje de por favor intente otra vez adjuntar el archivo cuando ocurre error
                                                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgOcurrioError);
                                                        }
                                                        // Si el archivo no pertenece al formato
                                                        else
                                                        {
                                                            // Mandar mensaje de que se adjunto un documento que no pertenece al formato
                                                            await dialog.SendMessage(StringResources.ttlAlerta, mensaje + "\n\n" + StringResources.msgDocDifFormato + "\n" + StringResources.msgCorrigeArchivo);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //si se llegara a insertar un tipo de archivo que no corresponde se manda un mensaje indicando que tipo de archivo se debe adjuntar
                                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSoloExcel);
                                                //se borra de la lista temporal
                                                ListaDocumentos.Clear();
                                                //habilitamos el boton de adjuntar archivos
                                                BttnArchivos = true;
                                            }
                                        }
                                        else
                                        {
                                            //cualquier id que sea diferente de los anteriores tiene que ser un archivo .pdf.
                                            //Verificamos de nuevo que no se haya insertado un archivo que no corresponde
                                            if (obj.ext == ".pdf")
                                            {
                                                //si se agrego el archivo correspondiente lo agregamos a la lista temporal
                                                ListaDocumentos.Add(obj);
                                                //deshabilitamos el boton de agregar archivos
                                                BttnArchivos = false;
                                            }
                                            else
                                            {
                                                //si se llegara a insertar un tipo de archivo que no corresponde se manda un mensaje indicando que tipo de archivo se debe adjuntar
                                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertarTipoArchivoPDF);
                                                //se borra de la lista temporal
                                                Lista.Clear();
                                                //habilitamos el boton de adjuntar archivos
                                                BttnArchivos = true;
                                            }
                                        }
                                    }
                                    //Ejecutamos el método para cerrar el mensaje de espera.
                                    await AsyncProgress.CloseAsync();
                                }
                                else
                                {
                                    //Si el archivo está abierto mandamos un mensaje indicando que se debe cerrar el archivo para poder adjuntarlo
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                                }
                            }
                            catch (IOException)
                            {
                                //Si el archivo está abierto mandamos un mensaje indicando que se debe cerrar el archivo para poder adjuntarlo
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                            }
                        }//TERMINA IF DEL RESULT
                    }
                    else
                    {
                        //Entramos a este else si la lista de documento ya tiene un documento y se quiere agregar uno nuevo
                        //primero verificamos que la ventana sea SOLO de los archivos pendientes por corregir
                        if (AdjuntarDocumento != true)
                        {
                            //si la ventana es pendiente por corregir, solo se elimina el archivo y se sustituye por el nuevo seleccionado
                            MessageDialogResult Respuesta = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSustituirArchivo, setting, MessageDialogStyle.AffirmativeAndNegative);

                            if (Respuesta == MessageDialogResult.Affirmative)
                            {
                                //Abre la ventana de explorador de archivos
                                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                                //Filtar los documentos por extensión
                                if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014 || id_tipo == 1011)
                                    dlg.Filter = "Word (97-2003)|*.doc";
                                else
                                {
                                    if (id_tipo == 1015 || id_tipo == 2 || id_tipo == 1002 || id_tipo == 1004)
                                    {
                                        dlg.Filter = "Excel Files (.xlsm, .xlsx)|*.xlsm; *.xlsx";
                                    }
                                    else
                                    {
                                        dlg.Filter = "PDF Files (.pdf)|*.pdf";
                                    }
                                }

                                //mostramos la ventana para poder seleccionar el archivo
                                Nullable<bool> result = dlg.ShowDialog();

                                // Si fue seleccionado un documento
                                if (result == true)
                                {
                                    try
                                    {
                                        //Se obtiene el nombre del documento
                                        filename = dlg.FileName;

                                        //Si el archivo no está en uso
                                        if (!Module.IsFileInUse(filename))
                                        {
                                            //Ejecutamos el método para enviar un mensaje de espera mientras se comprueban los datos.
                                            AsyncProgress = await dialog.SendProgressAsync(StringResources.msgEspera, StringResources.msgInsertando);

                                            //inicializamos la variable global para poder insertar el archivo en la base de datos
                                            ArchivoTemporal = new Archivo();

                                            //Se convierte el archvio a tipo byte y se le asigna al objeto
                                            ArchivoTemporal.archivo = await Task.Run(() => File.ReadAllBytes(filename));

                                            //Obtiene la extensión del documento y se le asigna al objeto
                                            ArchivoTemporal.ext = System.IO.Path.GetExtension(filename);

                                            //Se obtiene sólo el nombre, sin extensión.
                                            ArchivoTemporal.nombre = System.IO.Path.GetFileNameWithoutExtension(filename);
                                            //se obtiene el id_version del documento
                                            ArchivoTemporal.id_version = idVersion;

                                            //Si el archivo tiene extensión pdf
                                            if (ArchivoTemporal.ext == ".pdf")
                                            {
                                                //asigna la imagen del pdf al objeto
                                                ArchivoTemporal.ruta = @"/Images/p.png";
                                            }
                                            else
                                            {
                                                if (ArchivoTemporal.ext == ".xlsm" || ArchivoTemporal.ext == ".xlsx")
                                                {
                                                    ArchivoTemporal.ruta = @"/Images/E.jpg";
                                                }
                                                else
                                                {
                                                    //Si es archivo de word asigna la imagen correspondiente.
                                                    ArchivoTemporal.ruta = @"/Images/w.png";
                                                }
                                            }
                                            //despues de que el usuario haya seleccionado el archivo a insertar
                                            //consultamos de que tipo es el archivo
                                            if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014 || id_tipo == 1011)
                                            {
                                                //si el archivo es igual a cualquiera de los id anteriores se comprueba que sea un archivo .doc
                                                if (ArchivoTemporal.ext == ".doc")
                                                {
                                                    //BORRAMOS EL ARCHIVO DE LA BASE DE DATOS, EL ARCHIVO QUE SE BORRA ES EL QUE SE ENCUENTRA EN LA LISTA DE DOCUMENTOS
                                                    int n = DataManagerControlDocumentos.DeleteArchivo(ListaDocumentos[0]);

                                                    //LIMPIAMOS LA LISTA QUE CONTIENE EL ARCHIVO ANTERIOR
                                                    ListaDocumentos.Clear();
                                                    // Toma tiempo para que se elimine y reemplace el PDF que ya está generado al adjuntar (no genere error de numeración)
                                                    Thread.Sleep(1500);

                                                    //AGREGAMOS EL NUEVO ARCHIVO A LA LISTA TEMPORAL QUE SELECCIONO EL USUARIO
                                                    ListaDocumentos.Add(ArchivoTemporal);
                                                }
                                                else
                                                {
                                                    //si el archivo no es del tipo correspondiente se manda un mensaje indicando el tipo de archivo que se puede insertar
                                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertarTipoArchivoDOC);
                                                    //limpiamos la lista temporal
                                                    Lista.Clear();
                                                }
                                            }
                                            else
                                            {
                                                if (id_tipo == 1015 || id_tipo == 2 || id_tipo == 1002 || id_tipo == 1004)
                                                {
                                                    //si el archivo es igual a cualquiera de los id anteriores se comprueba que sea un archivo .xlsx
                                                    if (ArchivoTemporal.ext == ".xlsm" || ArchivoTemporal.ext == ".xlsx")
                                                    {
                                                        //BORRAMOS EL ARCHIVO DE LA BASE DE DATOS, EL ARCHIVO QUE SE BORRA ES EL QUE SE ENCUENTRA EN LA LISTA DE DOCUMENTOS
                                                        int n = DataManagerControlDocumentos.DeleteArchivo(ListaDocumentos[0]);

                                                        //LIMPIAMOS LA LISTA QUE CONTIENE EL ARCHIVO ANTERIOR
                                                        ListaDocumentos.Clear();
                                                        // Toma tiempo para que se elimine y reemplace el PDF que ya está generado al adjuntar (no genere error de numeración)
                                                        Thread.Sleep(1500);

                                                        //AGREGAMOS EL NUEVO ARCHIVO A LA LISTA TEMPORAL QUE SELECCIONO EL USUARIO
                                                        ListaDocumentos.Add(ArchivoTemporal);

                                                        string mensaje;

                                                        int r = validarArchivo(out mensaje);
                                                        // Si el archivo es incorrecto se corrige automáticamente
                                                        if (r == 2)
                                                        {
                                                            //si se agrego el archivo correspondiente lo agregamos a la lista temporal
                                                            ListaDocumentos.Clear();
                                                            //deshabilitamos el boton de agregar archivos
                                                            BttnArchivos = true;

                                                            setting.AffirmativeButtonText = StringResources.lblAutomaticamente;
                                                            setting.NegativeButtonText = StringResources.lblManualmente;

                                                            //Modo de corregir un documento
                                                            MessageDialogResult resp = await dialog.SendMessage(StringResources.ttlAlerta, mensaje + "\n\n" + StringResources.msgModoCorregir, setting, MessageDialogStyle.AffirmativeAndNegative);

                                                            string nfilename = dlg.FileName;
                                                            string path = nfilename;
                                                            string NombreAbreviadoPersonaCreo = ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().nombre.Substring(0, 1) + "." + ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().APaterno;

                                                            //Métodos para corregir los archivos
                                                            if (resp == MessageDialogResult.Affirmative)
                                                            {
                                                                if (id_tipo == 1004)
                                                                {
                                                                    ImportExcel.ExportAVYCorrecto(path, FechaFin, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                                                    AdjuntarArchivo(path);
                                                                }
                                                                if (id_tipo == 2)
                                                                {
                                                                    ImportExcel.ExportHOECorrecto(path, FechaFin, NombreAbreviadoPersonaCreo, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                                                    AdjuntarArchivo(path);
                                                                }
                                                                if (id_tipo == 1015)
                                                                {
                                                                    ImportExcel.ExportJESCorrecto(path, FechaFin, NombreAbreviadoPersonaCreo, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                                                    AdjuntarArchivo(path);
                                                                }
                                                                if (id_tipo == 1002)
                                                                {
                                                                    ImportExcel.ExportHIICorrecto(path, FechaFin, ListaUsuarios.Where(x => x.usuario == usuario).FirstOrDefault().NombreCorto, ListaUsuarios.Where(x => x.usuario == usuarioAutorizo).FirstOrDefault().NombreCorto, Descripcion, SelectedDocumento.nombre, ListaDepartamento.Where(x => x.id_dep == id_dep).FirstOrDefault().nombre_dep, Convert.ToInt32(Version), ID_documento);
                                                                    AdjuntarArchivo(path);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCorrigeArchivo);
                                                            }
                                                        }
                                                        else
                                                        {   // Si el archivo es correcto...
                                                            if (r == 1)
                                                            {
                                                                if (DocumentoNuevo)
                                                                {
                                                                    guardarControl("FORMATO DEL SISTEMA");
                                                                }
                                                                else
                                                                {
                                                                    CreadoPendienteXLiberar();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //se elimina de la lista temporal
                                                                ListaDocumentos.Clear();
                                                                //se habilita el boton para poder adjuntar un archivo
                                                                BttnArchivos = true;

                                                                // Si ocurrió un error
                                                                if (r == 3)
                                                                {
                                                                    // Mandar mensaje de por favor intente otra vez adjuntar el archivo cuando ocurre error
                                                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgOcurrioError);
                                                                }
                                                                // Si el archivo no pertenece al formato
                                                                else
                                                                {   // Mandar mensaje de que se adjunto un documento que no pertenece al formato
                                                                    await dialog.SendMessage(StringResources.ttlAlerta, mensaje + "\n\n" + StringResources.msgDocDifFormato + "\n" + StringResources.msgCorrigeArchivo);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //si el archivo no es del tipo correspondiente se manda un mensaje indicando el tipo de archivo que se puede insertar
                                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertarTipoArchivoPDF);
                                                        //limpiamos la lista temporal
                                                        Lista.Clear();
                                                    }
                                                }
                                                else
                                                {
                                                    //cualquier id que sea diferente de los anteriores tiene que ser un archivo .pdf
                                                    if (ArchivoTemporal.ext == ".pdf")
                                                    {
                                                        //BORRAMOS EL ARCHIVO DE LA BASE DE DATOS, EL ARCHIVO QUE SE BORRA ES EL QUE SE ENCUENTRA EN LA LISTA DE DOCUMENTOS
                                                        int n = DataManagerControlDocumentos.DeleteArchivo(ListaDocumentos[0]);

                                                        //LIMPIAMOS LA LISTA QUE CONTIENE EL ARCHIVO ANTERIOR
                                                        ListaDocumentos.Clear();

                                                        //AGREGAMOS EL NUEVO ARCHIVO A LA LISTA TEMPORAL QUE SELECCIONO EL USUARIO
                                                        ListaDocumentos.Add(ArchivoTemporal);
                                                    }
                                                    else
                                                    {
                                                        //si el archivo no es del tipo correspondiente se manda un mensaje indicando el tipo de archivo que se puede insertar
                                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertarTipoArchivoPDF);
                                                        //limpiamos la lista temporal
                                                        Lista.Clear();
                                                    }
                                                    //Ejecutamos el método para cerrar el mensaje de espera.
                                                    //await AsyncProgress.CloseAsync();
                                                }
                                            }
                                            await AsyncProgress.CloseAsync();
                                        }
                                        else
                                        {
                                            //Si el archivo está abierto le indicamos al usuario que lo tiene que cerrar para poder insetarlo
                                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                                        }
                                    }
                                    catch (IOException)
                                    {
                                        //Si el archivo está abierto le indicamos al usuario que lo tiene que cerrar para poder insetarlo
                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Si el documento ya esta liberado y se quiere agregar un nuevo archivo se mostrara el mensaje que indique que solo se puede ag
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgInsertarUnoSolo);
                        }
                    }
                }
                else
                {
                    //Si la descripción es igual al nombre del documento mandamos mensaje
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgDescripcionNoIgual);
                }
            }
        }

        /// <summary>
        /// Método que genera un versión
        /// Limpia los datos de la versión anterior
        /// </summary>
        private async void generarVersion()
        {
            DialogService dialog1 = new DialogService();

            ProgressDialogController AsyncProgressConfigEmail;

            AsyncProgressConfigEmail = await dialog1.SendProgressAsync(StringResources.ttlAtencion, StringResources.ttlEspereUnMomento);

            string url = System.Configuration.ConfigurationManager.AppSettings["URLNodeServer"];

            bool banStatusNodeServer = await DataManager.GetStatusConetionNodeServer(url);

            await AsyncProgressConfigEmail.CloseAsync();

            if (banStatusNodeServer)
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
                            usuarioAutorizo = null;
                            banAlertaCorreo = true;
                            banButtonNotificar = true;
                            VersionGenerada = true;
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
                            await dialog.SendMessage(StringResources.msgErrorCrearVersion, StringResources.msgNumeroVersion + " " + obj.no_version + " " + " " + StringResources.msgEstado + " " + obj.estatus);
                        }
                    }
                }
                else
                {
                    //El sistema se encuentra bloqueado
                    await dialog.SendMessage(StringResources.msgSistemaBloqueado, objBloqueo.observaciones);
                }
            }
            else
            {
                await dialog1.SendMessage(StringResources.ttlAtencion, StringResources.lblModeReadOnly);
            }
        }

        /// <summary>
        /// Método que retorna un true si todos los campos contienen un valor
        /// </summary>
        /// <returns></returns>
        private bool ValidarValores()
        {
            if (nombre != null & version != null & fecha != null & !string.IsNullOrEmpty(descripcion) & id_tipo != 0 & ListaDocumentos.Count != 0 & _usuario != null & _id_dep != 0 & usuarioAutorizo != null & !string.IsNullOrWhiteSpace(descripcion))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Método que retorna un true si el nombre del documento y la descripción son diferentes
        /// </summary>
        /// <returns></returns>
        private bool ValidarNombreDescripcion()
        {
            //Hacemos mayúsculas ambos campos para poder hacer la validación correctamente
            if (nombre.ToUpper() != descripcion.ToUpper())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Método para liberar un documento, modifica el número de copias del documento y el estatus de la versión.
        /// </summary>
        private async void liberarDocumento()
        {
            DialogService dialog = new DialogService();

            //Obtenemos la ventana actual
            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            //TO DO
            id_areasealed = "";
            //comprobamos que se haya seleccionado un area frames para poder insertarlo
            if (id_areasealed == "0" && (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014 || id_tipo == 1011))
            {
                //si no se selecciono el area, no se libera el documento
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblInsertarAreaFrames);
            }
            else
            {
                //Formulario para ingresar el número de copias,
                //string num_copias = await window.ShowInputAsync(StringResources.msgIngNumeroCopias, StringResources.msgNumeroCopias, null);
                string num_copias = "0";
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
                                            //int r = InsertDocumentoSealed();
                                            //Se modifica esta línea debido a que cambiaron la contraseña del servidor del frames.
                                            //int r = 1;
                                            //string confirmacionFrames = r > 0 ? StringResources.msgFramesExito : StringResources.msgFramesIncorrecto;
                                            string confirmacionFrames = StringResources.msgFramesIncorrecto;
                                            string confirmacionCorreo = string.Empty;

                                            //Se verifica si el dueño de documento adjunto al archivo firmado, si lo adjunto lo guardamos en la ruta específica.
                                            if (SignedFile.IsSignedFile)
                                                SaveSignedFile();

                                            //Verificamos si son documentos Procedimientos y Formatos
                                            if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014 || id_tipo == 1011)
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

                                            // Llamamos el método para eliminar los registros de la tabla TR_USUARIO_NOTIFICACION_VERSION por ID_VERSION, una vez que el documento sea liberado
                                            DataManagerControlDocumentos.EliminarRegistroVersion(idVersion);

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
                                            //si falla al momento de liberar el documento se regresa el estatus del documento a pendiente por aprobar
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
                                            //int r = UpdateDocumentoSealed();
                                            //Se modifica esta línea debido a que cambiaron la contraseña del servidor del frames.
                                            //int r = 1;

                                            //Se verifica si el dueño de documento adjunto al archivo firmado, si lo adjunto lo guardamos en la ruta específica.
                                            if (SignedFile.IsSignedFile)
                                                SaveSignedFile();

                                            //Si los registros fueron guardados exitosamente el archivo que queda como obsoleto se pasa a la carpeta de respaldo y se elimina de la base de datos
                                            _LiberarEspacioBD(last_id);

                                            //string confirmacionFrames = r > 0 ? StringResources.msgFramesExito : StringResources.msgFramesIncorrecto;
                                            string confirmacionFrames = StringResources.msgFramesIncorrecto;

                                            string confirmacionCorreo = string.Empty;

                                            if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014 || id_tipo == 1011)
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

                                            // Llamamos el método para eliminar los registros de la tabla TR_USUARIO_NOTIFICACION_VERSION por ID_VERSION, una vez que el documento sea liberado.
                                            DataManagerControlDocumentos.EliminarRegistroVersion(idVersion);

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

                                            //si falla al momento de liberar el documento se regresa el estatus de la version a aprobado, pendiente por liberar.
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
            foreach (objUsuario item in ListaUsuariosCorreo)
            {
                if (item.IsSelected)
                {
                    correos[i] = item.Correo;
                    i += 1;
                }
            }
            // Eliminamos correos duplicados
            correos = Module.EliminarCorreosDuplicados(correos);

            string path = User.Pathnsf;
            string title = "Actualización de documento - " + Nombre;
            string body = string.Empty;
            string tipo_documento = string.Empty;
            //string AreaFrames = string.Empty;

            //switch (id_tipo)
            //{
            //    case 1003:
            //    case 1013:
            //        AreaFrames = DataManagerControlDocumentos.GetNombreAreaOHSAS(Convert.ToInt32(id_areasealed));
            //        break;
            //    case 1014:
            //    case 1006:
            //        AreaFrames = DataManagerControlDocumentos.GetNombreAreaISO(Convert.ToInt32(id_areasealed));
            //        break;
            //    case 1005:
            //    case 1012:
            //        AreaFrames = DataManagerControlDocumentos.GetNombreAreaESPECIFICOS(Convert.ToInt32(id_areasealed));
            //        break;
            //    default:
            //        break;
            //}

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
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + Version + ".0" + "</b></font></li>";
            //body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Área del Frames en donde se inserto : <b>" + AreaFrames + "</b></font></li>";
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

            bool respuesta = SO_Email.SendEmailLotusCustom(path, correos, title, body, "CONTROL_DOCUMENTOS",0);

            return respuesta;
        }

        private async void bajarArchivos()
        {
            List<string> lista = new List<string>();
            lista.Add("4.10-1.7.1");
            lista.Add("4.11-1.42");
            lista.Add("4.11-1.43");
            lista.Add("4.11-1.70");
            lista.Add("4.11-1.71");
            lista.Add("4.11-1.78");
            lista.Add("4.11-1.79");
            lista.Add("4.14-1.3");
            lista.Add("4.9-001.11");
            lista.Add("4.9-001.15");
            lista.Add("4.9-001.18");
            lista.Add("4.9-001.19");
            lista.Add("4.9-001.6");
            lista.Add("4.9-001.8");
            lista.Add("4.9-002.4");
            lista.Add("4.9-005.1");
            lista.Add("4.9-009.04");
            lista.Add("4.9-009.1");
            lista.Add("4.9-009.6");
            lista.Add("4.9-1.14");
            lista.Add("4.9-1.16.1");
            lista.Add("4.9-1.16.2");
            lista.Add("4.9-1.17");
            lista.Add("4.9-1.17.1");
            lista.Add("4.9-1.17.2");
            lista.Add("4.9-1.19.1");
            lista.Add("4.9-1.19.2");
            lista.Add("4.9-1.2");
            lista.Add("4.9-1.2.1");
            lista.Add("4.9-1.2.2");
            lista.Add("4.9-1.321");
            lista.Add("4.9-1.33");
            lista.Add("4.9-1.342");
            lista.Add("4.9-1.433");
            lista.Add("4.9-1.45.1");
            lista.Add("4.9-1.477");
            lista.Add("4.9-1.5.3");
            lista.Add("4.9-1.52");
            lista.Add("4.9-1.67");
            lista.Add("4.9-1.9.2");
            lista.Add("4.9-1.9.3");
            lista.Add("4.9-2.1");
            lista.Add("4.9-2.105");
            lista.Add("4.9-2.109");
            lista.Add("4.9-2.120");
            lista.Add("4.9-2.24");
            lista.Add("4.9-2.31");
            lista.Add("4.9-2.50");
            lista.Add("4.9-2.52");
            lista.Add("4.9-2.89");
            lista.Add("4.9-2.92");
            lista.Add("4.9-2.93");
            lista.Add("4.9-2.94");
            lista.Add("4.9-2.95");
            lista.Add("4.9-2.97");
            lista.Add("4.9-3.15");
            lista.Add("4.9-3.16");
            lista.Add("4.9-3.20");
            lista.Add("4.9-3.22");
            lista.Add("4.9-4.7.1");
            lista.Add("4.9-4.8.2");
            lista.Add("4.9-4.8.4");
            lista.Add("AVBB-001");
            lista.Add("AVBB-002");
            lista.Add("AVBB-003");
            lista.Add("AVBB-004");
            lista.Add("AVBG-001");
            lista.Add("AVBK-005");
            lista.Add("AVBK-007");
            lista.Add("AVBK-008");
            lista.Add("AVBK-017");
            lista.Add("AVBK-018");
            lista.Add("AVBK-019");
            lista.Add("AVBK-020");
            lista.Add("AVBK-022");
            lista.Add("AVBK-028");
            lista.Add("AVBK-034");
            lista.Add("AVBK-042");
            lista.Add("AVBK-043");
            lista.Add("AVBK-045");
            lista.Add("AVBK-046");
            lista.Add("AVBK-047");
            lista.Add("AVCA-001");
            lista.Add("AVCA-002");
            lista.Add("AVCA-003");
            lista.Add("AVCA-004");
            lista.Add("AVCA-005");
            lista.Add("AVCA-006");
            lista.Add("AVCA-007");
            lista.Add("AVCA-008");
            lista.Add("AVCA-009");
            lista.Add("AVCT-001");
            lista.Add("AVCT-002");
            lista.Add("AVCT-003");
            lista.Add("AVE8-002");
            lista.Add("AVE8-003");
            lista.Add("AVE8-004");
            lista.Add("AVE8-005");
            lista.Add("AVE8-006");
            lista.Add("AVE8-007");
            lista.Add("AVE8-008");
            lista.Add("AVE8-009");
            lista.Add("AVE8-010");
            lista.Add("AVE8-012");
            lista.Add("AVE8-013");
            lista.Add("AVE8-014");
            lista.Add("AVE8-015");
            lista.Add("AVFO-003");
            lista.Add("AVFO-004");
            lista.Add("AVFO-005");
            lista.Add("AVFO-006");
            lista.Add("AVFO-007");
            lista.Add("AVKA-001");
            lista.Add("AVKO-001");
            lista.Add("AVMO-001");
            lista.Add("AVPE-001");
            lista.Add("AVSM-001");
            lista.Add("AVSM-002");
            lista.Add("HIFM-0001");
            lista.Add("PIAV-002");
            lista.Add("SD 0038");
            lista.Add("SD 0046");
            lista.Add("SD 0047");
            lista.Add("SD 0053");
            lista.Add("SD 0053.1");
            lista.Add("SD 0054");
            lista.Add("SD 0055");
            lista.Add("SD 0056");
            lista.Add("SD 0057");
            lista.Add("W-3571-A4473-es");

            foreach (var noDocumento in lista)
            {
                Documento documento = DataManagerControlDocumentos.GetDocumento(noDocumento);
                ObservableCollection<Model.ControlDocumentos.Version> listaVersiones = DataManagerControlDocumentos.GetVersiones(documento.id_documento);
                Model.ControlDocumentos.Version versionLiberada = listaVersiones.Where(o => o.id_estatus_version == 1).FirstOrDefault();
                ObservableCollection<Archivo> archivos =  DataManagerControlDocumentos.GetArchivos(versionLiberada.id_version);
                int c = 1;
                foreach (var archivo in archivos)
                {
                    string root = @"\\agufileserv2\TODOSP\R@ul\DepuracionControlDocumentos\Documentos";
                    string fileName = Path.Combine(root, documento.nombre + "_" + c + archivo.ext);
                    File.WriteAllBytes(fileName, archivo.archivo);
                    c++;
                }
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

        /// <summary>
        /// Metodo que notifica vía Correo que un documento ya esta disponible para descarga con sello electónico.
        /// </summary>
        /// <returns></returns>
        private bool NotificarDocumentoDisponibleConSello()
        {
            ServiceEmail serviceMail = new ServiceEmail();

            string[] correos = new string[ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count];

            int i = 0;
            foreach (objUsuario item in ListaUsuariosCorreo)
            {
                if (item.IsSelected)
                {
                    correos[i] = item.Correo;
                    i += 1;
                }
            }
            // Eliminamos correos duplicados
            correos = Module.EliminarCorreosDuplicados(correos);

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

            bool respuesta = serviceMail.SendEmailLotusCustom(path, correos, title, body, "CONTROL_DOCUMENTOS",0);

            return respuesta;
        }

        /// <summary>
        /// Método que notifica via correo que un documento que ya se encuentra en la matríz fue sellado correctamente
        /// </summary>
        /// <returns></returns>
        private bool NotificarDocumentoExistenteConSello()
        {
            ServiceEmail Correo = new ServiceEmail();

            string[] CorreosUsuarios = new string[vmUsuarios.ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count];

            int i = 0;
            foreach (objUsuario item in vmUsuarios.ListaUsuariosCorreo)
            {
                if (item.IsSelected)
                {
                    CorreosUsuarios[i] = item.Correo;
                    i += 1;
                }
            }
            // Eliminamos correos duplicados
            CorreosUsuarios = Module.EliminarCorreosDuplicados(CorreosUsuarios);

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

            bool respuesta = Correo.SendEmailLotusCustom(path, CorreosUsuarios, title, body, "CONTROL_DOCUMENTOS",0);

            return respuesta;
        }

        /// <summary>
        /// Método que notifica vía correo el alta de un documento.
        /// </summary>
        /// <returns></returns>
        private bool NotificarNuevaVersion()
        {
            ServiceEmail SO_Email = new ServiceEmail();
            //string AreaFrames = string.Empty;

            //obtenemos los correos de la vista FRMListaDocumentos
            string[] correos = new string[ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count];

            //obtenemos los correos de los usuarios seleccionados
            int i = 0;
            foreach (objUsuario item in ListaUsuariosCorreo)
            {
                if (item.IsSelected)
                {
                    correos[i] = item.Correo;
                    i += 1;
                }
            }
            // Eliminamos correos duplicados
            correos = Module.EliminarCorreosDuplicados(correos);

            string path = User.Pathnsf;
            string title = "Alta de documento - " + Nombre;
            string body = string.Empty;
            string tipo_documento = string.Empty;

            //obtenemos el tipo de documento
            //switch (id_tipo)
            //{
            //    case 1003:
            //    case 1013:
            //        AreaFrames = DataManagerControlDocumentos.GetNombreAreaOHSAS(Convert.ToInt32(id_areasealed));
            //        break;
            //    case 1014:
            //    case 1006:
            //        AreaFrames = DataManagerControlDocumentos.GetNombreAreaISO(Convert.ToInt32(id_areasealed));
            //        break;
            //    case 1005:
            //    case 1012:
            //        AreaFrames = DataManagerControlDocumentos.GetNombreAreaESPECIFICOS(Convert.ToInt32(id_areasealed));
            //        break;
            //    default:
            //        break;
            //}
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
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + Version + ".0" + "</b></font></li>";
            //body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Área del Frames en donde se inserto : <b>" + AreaFrames + "</b></font></li>";
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
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a> </li>";
            body += "</ul>";
            body += "</body>";
            body += "</HTML>";

            //Ejecutamos el método para notificar por correo
            bool respuesta = SO_Email.SendEmailLotusCustom(path, correos, title, body, "CONTROL_DOCUMENTOS", 0);

            return respuesta;
        }

        /// <summary>
        /// Método que guarda un nuevo documento, con su respectiva versión y los archivos
        /// O guarda una nueva versión con sus archivos
        /// </summary>
        private async void guardarControl(string TipoDocumento)
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
                //Verifa que el nombre del documento y la descripción no sean iguales
                if (ValidarNombreDescripcion())
                {
                    //Quitamos los espacios en blanco de la descripcion para poder verificar que no se encuentre ningún caracter especial
                    string CadenaEvaluar = descripcion.Replace(" ", "");

                    //verifica que la descripcion no contenga ningun caracter especial
                    if (Regex.IsMatch(CadenaEvaluar, "^[a-zA-Z0-9-_,;.()áÁéÉíÍóÓúÚÜüñÑ]*$"))
                    {
                        //Quitamos los espacios en blanco del Nombre del usuario que lo autorizo
                        string UsuariosPermitido = NombreUsuarioAut.Replace(" ", "");

                        //Solo se puede guardar el documento si el nombre es diferente de sistema
                        if (UsuariosPermitido != "SISTEMA")
                        {
                            MessageDialogResult result;

                            if (TipoDocumento == "FORMATO DEL SISTEMA")
                            {
                                result = await dialog.SendMessage(StringResources.msgGuardarDocumento, mensaje, setting, MessageDialogStyle.Affirmative);
                            }
                            else
                            {
                                result = await dialog.SendMessage(StringResources.msgGuardarDocumento, mensaje, setting, MessageDialogStyle.AffirmativeAndNegative);
                            }
                            //Ejecutamos el método para mostrar el mensaje con la información que el usuario capturó.El resultado lo asignamos a una variable local.

                            //Verificamos que el botón contenga la leyenda Guardar, esto indica que el registro es nuevo.
                            if (BotonGuardar == StringResources.ttlGuardar)
                            {
                                //Si la respuesta es afirmativa
                                if (result == MessageDialogResult.Affirmative)
                                {
                                    //Valída si existe documentos que sean iguales al documento a subir, el resultado se guarda en una variable local.
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
                                            //si el archivo es una JES, se pone el estatus de documento en pendiente por liberar
                                            if (TipoDocumento == "FORMATO DEL SISTEMA")
                                            {
                                                obj.id_estatus = 4;
                                            }
                                            else
                                            {
                                                obj.id_estatus = 2;
                                            }

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
                                                //si es una JES, se pone el estatus de version en pendiente por liberar
                                                if (TipoDocumento == "FORMATO DEL SISTEMA")
                                                {
                                                    objVersion.id_estatus_version = 5;
                                                }
                                                else
                                                {
                                                    objVersion.id_estatus_version = 3;
                                                }
                                                objVersion.no_copias = 0;
                                                objVersion.descripcion_v = Descripcion;

                                                //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                                                int id_version = DataManagerControlDocumentos.SetVersion(objVersion, obj.nombre);

                                                DataManagerControlDocumentos.InsertHistorialVersion(id_version, NombreUsuarioElaboro, obj.nombre, objVersion.no_version, "Se cambia el estatus a: APROBADO, PENDIENTE POR LIBERAR");

                                                UnirTodosIntegrantes(id_version);

                                                //Actualizamos el código generado en la tabla TBL_VERSION.
                                                DataManagerControlDocumentos.UpdateCodeValidation(id_version, codeValidation);
                                                codeValidation = string.Empty;

                                                //si se guardó correctamente el registro en la tabla versión.
                                                if (id_version != 0)
                                                {
                                                    objVersion.id_version = id_version;

                                                    bool banOk = true;
                                                    //Iteramos la lista de documentos.
                                                    foreach (var item in ListaDocumentos)
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

                                                        if (nombre == 0)
                                                        {
                                                            banOk = false;
                                                            objVersion.id_estatus_version = 4;

                                                            //Rechazamos el documento.
                                                            DataManagerControlDocumentos.UpdateVersion(objVersion, User, objArchivo.nombre);

                                                            //await dialog.SendMessage(StringResources.ttlAlerta, "Hubo un error al adjuntar el documento, por favor intente mas tarde.");
                                                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHuboError);
                                                        }else
                                                        {
                                                            //Si es formato del sistema, enviamos un correo al que autorizará/Rechazará el documento.
                                                            if (TipoDocumento == "FORMATO DEL SISTEMA")
                                                            {
                                                                ObservableCollection<Archivo> archivosTem = DataManagerControlDocumentos.GetArchivos(id_version);
                                                                //string[] files = new string[archivosTem.Count];
                                                                //int c = 0;
                                                                //foreach (var item2 in archivosTem)
                                                                //{
                                                                //    string pathTemp = GetPathTempFile(item2);
                                                                //    File.WriteAllBytes(pathTemp, item2.archivo);
                                                                //    files[c] = pathTemp;
                                                                //    c++;
                                                                //}

                                                                bool confirmacionEnviarCorreo = enviarCorreoAprobarRechazar(_selectedDocumento.nombre, objVersion.no_version, objVersion.id_version,objVersion.id_usuario_autorizo);
                                                            }
                                                        }
                                                    }

                                                    //Ejecutamos el método para cerrar el mensaje de espera.
                                                    await controllerProgressAsync.CloseAsync();

                                                    if (banOk)
                                                        //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
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

                                    //Si no hay documentos con la misma descripción.
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

                                            //si es archivo JES, se pasa automaticamente a pendiente por liberar
                                            if (TipoDocumento == "FORMATO DEL SISTEMA")
                                            {
                                                objVersion.id_estatus_version = 5;
                                            }
                                            else //si no es JES, se pasa a pendiente por validar
                                            {
                                                objVersion.id_estatus_version = 3;
                                            }

                                            objVersion.no_copias = 0;
                                            objVersion.descripcion_v = Descripcion;

                                            //valida que la version en el documento no se repita
                                            int validacion = DataManagerControlDocumentos.ValidateVersion(objVersion);

                                            if (validacion == 0)
                                            {
                                                //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                                                int id_version = DataManagerControlDocumentos.SetVersion(objVersion, nombre);
                                                // Llamamos el método para insertar en la tabla todos usuarios a notificar
                                                UnirTodosIntegrantes(id_version);

                                                objVersion.id_version = id_version;

                                                DataManagerControlDocumentos.UpdateCodeValidation(id_version, codeValidation);

                                                //si se realizo guardo la versión
                                                if (id_version != 0)
                                                {
                                                    bool banOk = true;
                                                    //Iteramos la lista de documentos.
                                                    foreach (var item in ListaDocumentos)
                                                    {
                                                        Archivo objArchivo = new Archivo();
                                                        //Mapeamos los valores al objeto creado, se guarda el archivo con el nombre del documento y la versión
                                                        objArchivo.id_version = id_version;
                                                        objArchivo.archivo = item.archivo;
                                                        objArchivo.ext = item.ext;
                                                        objArchivo.nombre = string.Concat(nombre, version);

                                                        //Ejecutamos el método para guardar el documento iterado, el resultado lo guardamos en una variable local.
                                                        int id_archivo = await DataManagerControlDocumentos.SetArchivo(objArchivo);

                                                        if (id_archivo == 0)
                                                        {
                                                            banOk = false;
                                                            objVersion.id_estatus_version = 4;

                                                            //Rechazamos el documento.
                                                            DataManagerControlDocumentos.UpdateVersion(objVersion, User, objArchivo.nombre);

                                                            //await dialog.SendMessage(StringResources.ttlAlerta, "Hubo un error al adjuntar el documento, por favor intente mas tarde.");
                                                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHuboError);
                                                        }else
                                                        {
                                                            //Si es formato del sistema, enviamos un correo al que autorizará/Rechazará el documento.
                                                            if (TipoDocumento == "FORMATO DEL SISTEMA")
                                                            {
                                                                ObservableCollection<Archivo> archivosTem = DataManagerControlDocumentos.GetArchivos(id_version);
                                                                string[] files = new string[archivosTem.Count];
                                                                int c = 0;
                                                                foreach (var item2 in archivosTem)
                                                                {
                                                                    string pathTemp = GetPathTempFile(item2);
                                                                    File.WriteAllBytes(pathTemp, item2.archivo);
                                                                    files[c] = pathTemp;
                                                                    c++;
                                                                }

                                                                bool confirmacionEnviarCorreo = enviarCorreoAprobarRechazar(_selectedDocumento.nombre, objVersion.no_version, objVersion.id_version, objVersion.id_usuario_autorizo);
                                                            }
                                                        }

                                                    }

                                                    //Asignamos el valor de Guardar a la etiqueta del botón.
                                                    BotonGuardar = StringResources.ttlGuardar;

                                                    //Ejecutamos el método para cerrar el mensaje de espera.
                                                    await controllerProgressAsync.CloseAsync();

                                                    //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
                                                    if (banOk)
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
                            //Mandamos mensaje que el usuario autorizo no puede ser sistema
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblUsuarioPermitido);
                        }
                        //CIERRA IF DE VALIDAR CARACTERES ESPECIALES
                    }
                    else
                    {
                        //Mandamos mensaje que la descripcion no puede tener caracteres especiales
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblCaracteresEspeciales);
                    }
                }
                else
                {
                    //Mandamos mensaje que el nombre del documento y la descripción no pueden ser iguales
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiaDescripcion);
                }
            }
            else
            {
                //Mandamos mensaje de que no puede haber campos vacios
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
                foreach (objUsuario item in vmUsuarios.ListaUsuariosCorreo)
                {
                    if (item.IsSelected)
                    {
                        correos[i] = item.Correo;
                        i += 1;
                    }
                }
                // Eliminamos duplicados
                correos = Module.EliminarCorreosDuplicados(correos);

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
                body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + Version + ".0" + "</b></font></li>";
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

                bool respuesta = SO_Email.SendEmailLotusCustom(path, correos, title, body, "CONTROL_DOCUMENTOS", 0);

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
        private int DeleteDocumentoSealed(out bool banEliminarFrames)
        {
            int r = 0;
            banEliminarFrames = false;

            switch (id_tipo)
            {
                case 1003:
                case 1013:
                    r = DataManagerControlDocumentos.DeleteDocumentoOHSAS(SelectedDocumento.nombre);
                    banEliminarFrames = true;
                    break;

                case 1005:
                case 1012:
                case 1011:
                    r = DataManagerControlDocumentos.DeleteDocumentoEspecifico(SelectedDocumento.nombre);
                    banEliminarFrames = true;
                    break;

                case 1006:
                case 1014:
                    r = DataManagerControlDocumentos.DeleteDocumentoISO(SelectedDocumento.nombre);
                    banEliminarFrames = true;
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
                case 1011:
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
                case 1011:
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

        private void SaveSignedFile()
        {
            string path = @"\\agufileserv2\ingenieria\resprutas\NUEVO SOFTWARE RUTAS\DocumentosFirmados";
            string folder = string.Empty;
            switch (id_tipo) {
                case 2:
                    folder = @"\HOE\";
                    break;
                case 1002:
                    folder = @"\HII\";
                    break;
                case 1003:
                    folder = @"\PROCEDIMIENTO_OHSAS\";
                    break;
                case 1004:
                    folder = @"\AYUDA_VISUAL\";
                    break;
                case 1005:
                    folder = @"\PROCEDIMIENTO_TS_46949\";
                    break;
                case 1006:
                    folder = @"\PROCEDIMIENTO_ISO_14001\";
                    break;
                case 1007:
                    folder = @"\HMTE\";
                    break;
                case 1010:
                    folder = @"\HVA\";
                    break;
                case 1011:
                    folder = @"\MIE\";
                    break;
                case 1012:
                    folder = @"\FORMATO_TS46949\";
                    break;
                case 1013:
                    folder = @"\FORMATO_OHSAS\";
                    break;
                case 1014:
                    folder = @"\FORMATO_ISO_140001\";
                    break;
                case 1015:
                    folder = @"\JES\";
                    break;
            }

            path = path + folder + nombre + @"\";

            if (!Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            path = path + nombre + "_" + version + SignedFile.ext;

            File.WriteAllBytes(path, SignedFile.archivo);

        }

        /// <summary>
        /// Método que guarda el archivo de tipo OHSAS, ESPECIFICOS, ISO14001 en sealed//documents__
        /// </summary>
        private string SaveFile()
        {
            string nombre_tipo;
            try
            {   //Si es documneto de tipo especifico o formato
                if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014 || id_tipo == 1011)
                {
                    string path = @"\\MXAGSQLSRV01\documents__";
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
                        case 1011:
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
            if (id_tipo != 1003 && id_tipo != 1005 && id_tipo != 1006 && id_tipo != 1012 && id_tipo != 1013 && id_tipo != 1014 && id_tipo != 1011)
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
                        string waterMarkText = "MAHLE CONTROL DE DOCUMENTOS / DOCUMENTO LIBERADO ELECTRÓNICAMENTE Y TIENE VALIDEZ SIN FIRMA." + " DISPOSICIÓN: " + fecha;
                        string waterMarkText2 = "ÚNICAMENTE TIENE VALIDEZ EL DOCUMENTO DISPONIBLE EN INTRANET.";
                        string waterMarkText3 = "LAS COPIAS NO ESTÁN SUJETAS A NINGÚN SERVICIO DE ACTUALIZACIÓN.";

                        byte[] newarchivo = AddWatermark(item.archivo, bfTimes, waterMarkText, waterMarkText2, waterMarkText3, id_recurso);

                        item.archivo = newarchivo;

                        int r = DataManagerControlDocumentos.UpdateArchivo(item);

                        res = r == 0 ? false : true;
                    }
                }

            }
            return res;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="baseFont"></param>
        /// <param name="watermarkText"></param>
        /// <param name="waterMarkText2"></param>
        /// <param name="waterMarkText3"></param>
        /// <returns></returns>
        public static byte[] AddWatermark(byte[] bytes, BaseFont baseFont, string watermarkText, string waterMarkText2, string waterMarkText3, int recurso)
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

                        iTextSharp.text.Rectangle realPageSize = reader.GetPageSizeWithRotation(i);

                        if (recurso != 0 && (recurso == 1054 || recurso == 1055))
                        {
                            if (recurso == 1054)
                            {
                                //Vertical
                                AddWaterMarkText2(dc, watermarkText, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 28), Convert.ToInt32(realPageSize.Bottom + 350));
                                AddWaterMarkText2(dc, waterMarkText2, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 34), Convert.ToInt32(realPageSize.Bottom + 265));
                                AddWaterMarkText2(dc, waterMarkText3, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 40), Convert.ToInt32(realPageSize.Bottom + 265));
                            }
                            else
                            {
                                //Horizontal
                                AddWaterMarkText2(dc, watermarkText, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 28), Convert.ToInt32(realPageSize.Bottom + 285));
                                AddWaterMarkText2(dc, waterMarkText2, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 34), Convert.ToInt32(realPageSize.Bottom + 210));
                                AddWaterMarkText2(dc, waterMarkText3, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 40), Convert.ToInt32(realPageSize.Bottom + 210));
                            }
                        }
                        else
                        {
                            AddWaterMarkText2(dc, watermarkText, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 6), Convert.ToInt32(realPageSize.Bottom + 245));
                            AddWaterMarkText2(dc, waterMarkText2, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 12), Convert.ToInt32(realPageSize.Bottom + 160));
                            AddWaterMarkText2(dc, waterMarkText3, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 18), Convert.ToInt32(realPageSize.Bottom + 160));
                        }
                    }
                    stamper.Close();
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pdfData"></param>
        /// <param name="watermarkText"></param>
        /// <param name="font"></param>
        /// <param name="fontSize"></param>
        /// <param name="angle"></param>
        /// <param name="color"></param>
        /// <param name="pos_x"></param>
        /// <param name="pos_y"></param>
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
            ListaDepartamento = DataManagerControlDocumentos.GetDepartamento();
            ListaTipo = DataManagerControlDocumentos.GetTipo();
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();
            ListaUsuariosCorreo = DataManagerControlDocumentos.GetUsuarios();
            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(User.NombreUsuario);
        }

        /// <summary>
        /// Método para modificar el contenido
        /// </summary>
        private async void modificar()
        {
            DialogService dialog1 = new DialogService();

            ProgressDialogController AsyncProgressConfigEmail;

            AsyncProgressConfigEmail = await dialog1.SendProgressAsync(StringResources.ttlAtencion, StringResources.ttlEspereUnMomento);

            string url = System.Configuration.ConfigurationManager.AppSettings["URLNodeServer"];

            bool banStatusNodeServer = await DataManager.GetStatusConetionNodeServer(url);

            await AsyncProgressConfigEmail.CloseAsync();

            if (banStatusNodeServer)
            {
                //Incializamos los servicios de dialog.
                DialogService dialog = new DialogService();

                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                //mostramos el mensaje que indica los datos que se enviaran al administrador del sistema.
                string mensaje = StringResources.lblNombre + ":" + " " + nombre +
                    "\n" + StringResources.lblVersion + ":" + " " + version +
                    "\n" + StringResources.lblFecha + ":" + " " + fecha.ToShortDateString() +
                    "\n" + StringResources.lblDescripcion + ":" + " " + descripcion +
                    "\n" + StringResources.lblTipoDocumento + ":" + " " + NombreTipo +
                    "\n" + StringResources.lblNombreDepartamento + ":" + " " + NombreDepto +
                    "\n" + StringResources.lblUsuarioElaboro + ":" + " " + NombreUsuarioElaboro +
                    "\n" + StringResources.lblUsuarioAutorizo + ":" + " " + NombreUsuarioAut;

                //declaramos una variable de tipo bloqueo que nos ayudara a saber si el sistema se encuentra bloqueado
                Bloqueo objBloqueo = new Bloqueo();

                //Método que obtiene un registro si se encuentra activo
                objBloqueo = DataManagerControlDocumentos.GetBloqueo();

                if (objBloqueo.id_bloqueo == 0 || Module.UsuarioIsRol(User.Roles, 2))
                {
                    if (ValidarValores())
                    {
                        string CadenaEvaluar = descripcion.Replace(" ", "");
                        if (Regex.IsMatch(CadenaEvaluar, "^[a-zA-Z0-9-_,;.()áÁéÉíÍóÓúÚÜüñÑ]*$"))
                        {
                            string UsuariosPermitido = NombreUsuarioAut.Replace(" ", "");
                            if (UsuariosPermitido != "SISTEMA")
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
                                        //Mandamos llamar el metodo que obtiene el id de la version si es que es una version mayor
                                        //y nos indica si tenemos registro de la version anterior
                                        int last_id = DataManagerControlDocumentos.GetID_LastVersion(id_documento, idVersion);

                                        //Si es la primer versión del documento se modifica los campos de la tabla de documento en la base de datos
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
                                                int update_version = modificaVersion("DOC");

                                                //si se modifico correctamente
                                                if (update_version != 0)
                                                {
                                                    bool banOk = true;

                                                    //obtenemos los datos que se habian guardado localmente en el metodo de adjuntar archivo
                                                    foreach (var item in ListaDocumentos)
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

                                                            if (a == 0)
                                                            {
                                                                banOk = false;
                                                                rechazaVersion();
                                                                //await dialog.SendMessage(StringResources.ttlAlerta, "Hubo un error al adjuntar el documento, por favor intente mas tarde.");
                                                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHuboError);
                                                                break;
                                                            }

                                                        }
                                                    }

                                                    if (banOk)
                                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosGuardadosExito);

                                                    CerrarVentanaActual();

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
                                            //Entramos a este else si el documento tiene mas versiones y se cuenta con registro de ellas. Aqui solo se modifican los datos de la
                                            //tabla de version en la base de datos

                                            //mandamos llamar el metodo que modifica los datos de la version
                                            int update_version = modificaVersion("DOC");
                                            if (update_version != 0)
                                            {
                                                bool banOk = true;

                                                //Iteramos la lista de los archivos de la versión
                                                foreach (var item in ListaDocumentos)
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

                                                        if (a == 0)
                                                        {
                                                            rechazaVersion();
                                                            banOk = false;

                                                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHuboError);
                                                            break;
                                                        }
                                                    }
                                                }

                                                if (banOk)
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
                                //Mandamos mensaje de que el usuario autorizo no puede ser SISTEMA
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblUsuarioPermitido);
                            }
                            //cierre
                        }
                        else
                        {
                            //Mandamos mensaje de que la descripcion no puede tener caracteres especiales
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblCaracteresEspeciales);
                        }
                    }
                    else
                    {
                        //Mandamos mensaje de que no puede haber campos vacios
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
                    }
                }
                else
                {
                    //El sistema se encuentra bloqueado
                    await dialog.SendMessage(StringResources.msgSistemaBloqueado, objBloqueo.observaciones);
                }
            }
            else
            {
                await dialog1.SendMessage(StringResources.ttlAtencion, StringResources.lblModeReadOnly);
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
                // Declaramos nueva lista, para asignarle el total de registros por id_documento
                ListaUsuariosSuscritosParaEliminar = DataManagerControlDocumentos.Get_UserSuscripDoc(id_documento);
                // Mandamos llamar el constructor que recibe como parámetro  la lista anterior
                vmUsuarios = new UsuariosViewModel(ListaUsuariosSuscritosParaEliminar, auxUsuario, auxUsuario_Autorizo);
                FrmListaUsuarios frmListaUsuarios = new FrmListaUsuarios();
                frmListaUsuarios.DataContext = vmUsuarios;

                frmListaUsuarios.ShowDialog();

                //Verficamos que el usuario seleccionó al menos una persona para noticarle la baja del documento. Si no selecciona a ninguna, no se permite la baja.
                if (vmUsuarios.ListaUsuariosCorreo.Where(x => x.IsSelected).ToList().Count > 0)
                {
                    Documento objDoc_Eliminado = new Documento();
                    //Elimina los documentos de la lista
                    foreach (var item in ListaDocumentos)
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

                            //Eliminamos el archivo de la BD
                            BorrarArchivosRegistrosEliminados(objDoc_Eliminado.nombre, objDoc_Eliminado.version.no_version);

                            bool banEliminarFrames = false;
                            int eliminoFrames = 0;

                            //Mandamos llamar el metodo que elimina el registro de la base de datos
                            //eliminoFrames = DeleteDocumentoSealed(out banEliminarFrames);
                            banEliminarFrames = true;

                            string confirmacionFrames = string.Empty;

                            if (banEliminarFrames)
                                confirmacionFrames = eliminoFrames > 0 ? StringResources.msgEliminarDocumentoFrames : StringResources.msgEliminarDocumentoFramesFallida + "http://sealed/frames.htm";

                            string confirmacionCorreo = string.Empty;
                            confirmacionCorreo = NotificarBajaDocumento() ? StringResources.msgNotificacionCorreo : StringResources.msgNotificacionCorreoFallida;

                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgRegistroEliminado + "\n" + confirmacionCorreo + "\n" + confirmacionFrames);
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

            //Declaramos un objeto al cual le asignamos las propiedades que contendra el mensaje.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgActNoCopias, setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
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

                        //ejecutamos el metodo para actualizar el numero de copias
                        int act_cop = DataManagerControlDocumentos.UpdateNoCopias(idVersion, nuevo_copias);

                        //comprobamos que se hayan guardado los cambios con exito, y mandamos un mensaje segun sea el caso
                        if (act_cop != 0)
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosRealizados);
                            //despues de haber dado aceptar, se inicia el metodo para actualizar el campo de numero de copias.
                            NoCopias = DataManagerControlDocumentos.GetCopias(idVersion);
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
        }

        /// <summary>
        /// Método que modifica la versión
        /// </summary>
        /// <returns></returns>
        private int modificaVersion(string TipoDocumento)
        {
            if (TipoDocumento == "FORMATO DEL SISTEMA")
            {
                //SI ES JES, SE PASA A PENDIENTE POR LIBERAR
                Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
                objVersion.id_version = idVersion;
                objVersion.no_version = version;
                objVersion.id_documento = id_documento;
                objVersion.id_usuario = _usuario;
                objVersion.id_usuario_autorizo = _usuarioAutorizo;
                objVersion.fecha_version = fecha;
                objVersion.id_estatus_version = 5;
                objVersion.no_copias = 0;
                objVersion.descripcion_v = Descripcion;

                //Ejecutamos el método para guardar la versión. El resultado lo retornamos
                return DataManagerControlDocumentos.UpdateVersion(objVersion, User, nombre);
            }
            else
            {
                //SI ES CUALQUIER OTRO MÉTODO SOLO SE PASA A PENDIENTE POR VALIDAR
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

        }

        /// <summary>
        /// Método que guarda la versión y pone el estatus como pendiente por corregir.
        /// </summary>
        /// <returns></returns>
        private int rechazaVersion()
        {
            Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
            objVersion.id_version = idVersion;
            objVersion.no_version = version;
            objVersion.id_documento = id_documento;
            objVersion.id_usuario = _usuario;
            objVersion.id_usuario_autorizo = _usuarioAutorizo;
            objVersion.fecha_version = fecha;
            objVersion.id_estatus_version = 4;
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
            if (ListaDocumentos.Count > 1)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Método que valída si existen documentos con similar descripción del que se va a dar de alta.
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<Documento> ValidaSimilares()
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
            if (ListaSimilares.Count > 0)
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
                //Consultamos los archivos que tenga el registro, si no contiene ningun archivo y el documento esta liberado no se permite salir de la ventana
                if (DataManagerControlDocumentos.GetArchivoFiltrado(idVersion).Count == 0 && AdjuntarDocumento)
                {
                    //await dialog.SendMessage(StringResources.ttlAlerta, "No se ha adjuntado ningun archivo");
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgNoAdjFile);
                }
                else
                {
                    //si no esta liberado el documento o ya cuenta con un archivo se le permite al usuario salir de la pestaña
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
                }
                else
                {
                    //se notifica si el usuario no selecciono a mas de un usuario para notificar
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSelectNotify);
                }

            }
        }

        /// <summary>
        /// Método para eliminar el documento con sello electronico cuando se cambie el estado
        /// a pendiente por corregir
        /// </summary>
        private async void EliminarDocumentoSellado()
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
                        objVersion.fecha_version = Fecha;
                        objVersion.id_estatus_version = 4;
                        objVersion.no_copias = 0;
                        objVersion.descripcion_v = Descripcion;

                        //Eliminamos el archivo que contiene el sello electronico
                        int EliminarSello = DataManagerControlDocumentos.EliminarDocumentoSellado(objVersion.id_version);

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
                    //eliminamos el archivo que contiene el sello electronico

                    objVersion.id_version = idVersion;
                    objVersion.no_version = version;
                    objVersion.id_documento = id_documento;
                    objVersion.id_usuario = _usuario;
                    objVersion.id_usuario_autorizo = _usuarioAutorizo;
                    objVersion.fecha_version = Fecha;
                    objVersion.id_estatus_version = 4;
                    objVersion.no_copias = 0;
                    objVersion.descripcion_v = Descripcion;

                    //eliminamos el archivo que contiene el sello electronico
                    int EliminarSello = DataManagerControlDocumentos.EliminarDocumentoSellado(objVersion.id_version);

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
        /// Método que genera el Menú
        /// </summary>
        /// <param name="r"></param>
        /// <param name="Habilitado"></param>
        public void CreateMenuItems(string Ventana)
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();

            switch (Ventana)
            {
                case "PendienteLiberar":
                    //Libera un documento
                    this.MenuItems.Add(
                         new HamburgerMenuIconItem()
                         {
                             Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AirplaneTakeoff },
                             Label = StringResources.lblLiberar,
                             Command = LiberarDocumento,
                             Tag = StringResources.lblLiberar,
                         }
                        );

                    this.MenuItems.Add(
                        new HamburgerMenuIconItem()
                        {
                            Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.KeyboardReturn },
                            Label = StringResources.lblPendienteCorregir,
                            Command = RegresarCorregir,
                            Tag = StringResources.lblPendienteCorregir,
                        }
                    );

                    this.MenuItems.Add(
                        new HamburgerMenuIconItem() {
                            Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Scanner},
                            Label = "Descargar documento firmado",
                            Command = DownLoadFileScanned,
                            Tag = "Descargar documento firmado"
                        });

                    this.MenuItems.Add(
                        new HamburgerMenuIconItem()
                        {
                            Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Scanner },
                            Label = "Bajar documentos",
                            Command = DownLoadFiles,
                            Tag = "Bajar documentos"
                        });

                    break;

                case "DocumentoLiberado":

                    this.MenuItems.Add(
                            new HamburgerMenuIconItem()
                            {
                                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FileExcel },
                                Label = "Crear Archivo",
                                Command = GenerarArchivo,
                                Tag = "Crear Archivo",
                            }
                        );
                    if (Module.UsuarioIsRol(User.Roles, 2))
                    {
                        //Regresa la versión anterior de un documento si es que tiene
                        this.MenuItems.Add(
                            new HamburgerMenuIconItem()
                            {
                                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FormatRotate90 },
                                Label = StringResources.lblRegresarVersionAnterior,
                                Command = RegresarVersion,
                                Tag = StringResources.lblRegresarVersionAnterior,
                            }
                        );
                        //Elimina el registro del documento
                        this.MenuItems.Add(
                            new HamburgerMenuIconItem()
                            {
                                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Delete },
                                Label = StringResources.lblEliminar,
                                Command = Eliminar,
                                Tag = StringResources.lblEliminar,
                            }
                        );
                        //Verificamos si el documento ya esta liberado o no
                        //si ya esta liberado se elimina el documento con el sello electronico y se modifica el estatus a pendiente por corregir
                        this.MenuItems.Add(
                            new HamburgerMenuIconItem()
                            {
                                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.KeyboardReturn },
                                Label = StringResources.lblPendienteCorregir,
                                Command = EliminarDocumntoSellado,
                                Tag = StringResources.lblPendienteCorregir
                            }
                        );
                        //Sella electronicamente un documento
                        this.MenuItems.Add(
                            new HamburgerMenuIconItem()
                            {
                                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Seal },
                                Label = StringResources.ttlSellar,
                                Command = SellarDocumento,
                                Tag = StringResources.ttlSellar,
                            }
                            );
                        //Sella electronicamente un documento
                        this.MenuItems.Add(
                            new HamburgerMenuIconItem()
                            {
                                Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Update },
                                Label = StringResources.lblActualizarCopias,
                                Command = ActNoCopias,
                                Tag = StringResources.lblActualizarCopias
                            }
                            );
                    }
                    break;
                case "DocumentoPorCorregir":
                    this.MenuItems.Add(
                        new HamburgerMenuIconItem()
                        {
                            Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FileExcel },
                            Label = "Crear  archivo",
                            Command = GenerarArchivo,
                            Tag = "Crear archivo"
                        }
                        );
                    break;
                case "DocumentoNuevo":
                    this.MenuItems.Add(
                        new HamburgerMenuIconItem()
                        {
                            Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FileExcel },
                            Label = "Crear Archivo",
                            Command = GenerarArchivo,
                            Tag = "Crear Archivo"
                        }
                        );
                    break;
            }
        }

        private void CerrarVentanaActual()
        {
            #region Cerrar la ventana
            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            //Verificamos que la pantalla sea diferente de nulo.
            if (window != null)
            {
                //Cerramos la pantalla
                window.Close();
            }
            #endregion
        }

        /// <summary>
        /// Función para quitar el Excel y poner el pdf
        /// </summary>
        public async void QuitarExcelPonerPDF(Archivo Arc, string PathPdf)
        {
            //Se convierte el archvio a tipo byte y se le asigna al objeto
            Arc.archivo = await Task.Run(() => File.ReadAllBytes(PathPdf));

            ListaDocumentos.Clear();

            ListaDocumentos.Add(Arc);

        }

        /// <summary>
        /// Método que cuando un archivo se crea mediante los formatos que da el sistema
        /// los pone en pendiente por liberar automaticamente
        /// </summary>
        /// <returns></returns>
        public async void CreadoPendienteXLiberar()
        {
            DialogService dialog1 = new DialogService();

            ProgressDialogController AsyncProgressConfigEmail1;

            AsyncProgressConfigEmail1 = await dialog1.SendProgressAsync(StringResources.ttlAtencion, StringResources.ttlEspereUnMomento);

            string url = System.Configuration.ConfigurationManager.AppSettings["URLNodeServer"];

            bool banStatusNodeServer = await DataManager.GetStatusConetionNodeServer(url);

            await AsyncProgressConfigEmail1.CloseAsync();

            if (banStatusNodeServer)
            {
                //Incializamos los servicios de dialog.
                DialogService dialog = new DialogService();

                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                //mostramos el mensaje que indica los datos que se enviaran al administrador del sistema.
                string mensaje = StringResources.lblNombre + ":" + " " + nombre +
                    "\n" + StringResources.lblVersion + ":" + " " + version +
                    "\n" + StringResources.lblFecha + ":" + " " + fecha.ToShortDateString() +
                    "\n" + StringResources.lblDescripcion + ":" + " " + descripcion +
                    "\n" + StringResources.lblTipoDocumento + ":" + " " + NombreTipo +
                    "\n" + StringResources.lblNombreDepartamento + ":" + " " + NombreDepto +
                    "\n" + StringResources.lblUsuarioElaboro + ":" + " " + NombreUsuarioElaboro +
                    "\n" + StringResources.lblUsuarioAutorizo + ":" + " " + NombreUsuarioAut;

                //declaramos una variable de tipo bloqueo que nos ayudara a saber si el sistema se encuentra bloqueado
                Bloqueo objBloqueo = new Bloqueo();

                //Método que obtiene un registro si se encuentra activo
                objBloqueo = DataManagerControlDocumentos.GetBloqueo();

                if (objBloqueo.id_bloqueo == 0 || Module.UsuarioIsRol(User.Roles, 2))
                {
                    if (ValidarValores())
                    {
                        //Ejecutamos el método para mostrar el mensaje con la información que el usuario capturó.El resultado lo asignamos a una variable local.
                        MessageDialogResult result = await dialog.SendMessage(StringResources.msgGuardarDocumento, mensaje, setting, MessageDialogStyle.Affirmative);

                        if (result == MessageDialogResult.Affirmative)
                        {
                            //Valída si existe documentos que se aprecezcan al documento a subir, el resultado se guarda en una variable local.
                            ObservableCollection<Documento> ListDocSimilares = ValidaSimilares();

                            ListDocSimilares = null;

                            if (ListDocSimilares == null)
                            {
                                //Mandamos llamar el metodo que obtiene el id de la version si es que es una version mayor
                                //y nos indica si tenemos registro de la version anterior
                                int last_id = DataManagerControlDocumentos.GetID_LastVersion(id_documento, idVersion);


                                //Si es la primer versión del documento se modifica los campos de la tabla de documento en la base de datos
                                if (last_id == 0)
                                {
                                    //Se crea un objeto de tipo Documento.
                                    Documento obj = new Documento();

                                    obj.id_documento = id_documento;
                                    obj.id_dep = _id_dep;
                                    obj.id_tipo_documento = _id_tipo;
                                    obj.fecha_emision = fecha;
                                    obj.fecha_actualizacion = _FechaFin;
                                    //SE ASIGNA A PENDIENTE POR LIBERAR
                                    obj.id_estatus = 4;
                                    obj.usuario = usuario;

                                    //Ejecuta el método para modificar el documento actual
                                    int n = DataManagerControlDocumentos.UpdateDocumento(obj);
                                    //Si se realizo la modificacion

                                    if (n != 0)
                                    {
                                        //Se ejecuta el metodo que modifica la version actual, el resultado lo guardamos en una variable local
                                        int update_version = modificaVersion("FORMATO DEL SISTEMA");

                                        if (update_version != 0)
                                        {
                                            bool banOk = true;

                                            //obtenemos los datos que se habian guardado localmente en el metodo de adjuntar archivo
                                            foreach (var item in ListaDocumentos)
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

                                                    if (a == 0)
                                                    {
                                                        banOk = false;
                                                        rechazaVersion();
                                                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHuboError);
                                                        break;
                                                    }

                                                    // Llamamos el método para insertar en la tabla todos usuarios a notificar
                                                    UnirTodosIntegrantes(idVersion);
                                                }
                                            }
                                            if (banOk)
                                            {
                                                string idUsuarioAutorizo = DataManagerControlDocumentos.GetVersion(idVersion).id_usuario_autorizo;

                                                //ObservableCollection<Archivo> archivosTem = DataManagerControlDocumentos.GetArchivos(idVersion);
                                                //string[] files = new string[archivosTem.Count];
                                                //int c = 0;
                                                //foreach (var item in archivosTem)
                                                //{
                                                //    string pathTemp = GetPathTempFile(item);
                                                //    File.WriteAllBytes(pathTemp, item.archivo);
                                                //    files[c] = pathTemp;
                                                //    c++;
                                                //}

                                                if (!User.Details.IsAvailableEmail || !File.Exists(User.Pathnsf))
                                                {
                                                    //Configurar Correo.
                                                    await dialog.SendMessage(StringResources.ttlAtencion, "Su cuenta de correo electrónico aún no esta configurada con la plataforma Diseño del proceso. \n\n A continuación iniciará el proceso de configuración.");

                                                    await dialog.SendProgressAsync(User.Nombre + StringResources.msgParaTuInf, StringResources.msgProcesoConfiguracion);

                                                    ProgressDialogController AsyncProgressConfigEmail;

                                                    AsyncProgressConfigEmail = await dialog.SendProgressAsync(StringResources.ttlEspereUnMomento + User.Nombre + "...", StringResources.msgEstamosConfigurando);

                                                    ConfigEmailViewModel configEmail = new ConfigEmailViewModel(User);

                                                    // Se reciben valores de las 2 propiedades del objeto
                                                    DO_PathMail respuestaConfigEmail = await configEmail.setEmail();

                                                    await AsyncProgressConfigEmail.CloseAsync();

                                                    if (respuestaConfigEmail.respuesta)
                                                    {
                                                        // Actualizamos el path de usuario en la misma sesión
                                                        User.Pathnsf = respuestaConfigEmail.rutamail;

                                                        await dialog.SendProgressAsync(StringResources.msgPerfecto + User.Nombre, StringResources.msgCuentaConfigurada);
                                                    }
                                                    else
                                                    {
                                                        await dialog.SendMessage(StringResources.ttlOcurrioError, "Ocurrio un error al vincular su cuenta de correo electrónico. \nSu documento lo tendrá que entregar firmado al administrador de Control de documentos");
                                                    }
                                                }
                                                else
                                                {
                                                    bool confirmacionEnviarCorreo = enviarCorreoAprobarRechazar(nombre, version, idVersion, idUsuarioAutorizo);
                                                }

                                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosRealizados);
                                            }


                                            CerrarVentanaActual();
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
                                    //Entramos a este else si el documento tiene mas versiones y se cuenta con registro de ellas.Aqui solo se modifican los datos de la
                                    //tabla de version en la base de datos
                                    int update_version = modificaVersion("FORMATO DEL SISTEMA");

                                    if (update_version != 0)
                                    {
                                        bool banOk = true;

                                        //Iteramos la lista de los archivos de la versión
                                        foreach (var item in ListaDocumentos)
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

                                                if (a == 0)
                                                {
                                                    rechazaVersion();
                                                    banOk = false;
                                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHuboError);
                                                    break;
                                                }

                                                // Llamamos el método para insertar en la tabla todos usuarios a notificar
                                                UnirTodosIntegrantes(idVersion);
                                            }
                                        }

                                        if (banOk)
                                        {

                                            string idUsuarioAutorizo = DataManagerControlDocumentos.GetVersion(idVersion).id_usuario_autorizo;

                                            //ObservableCollection<Archivo> archivosTem = DataManagerControlDocumentos.GetArchivos(idVersion);
                                            //string[] files = new string[archivosTem.Count];
                                            //int c = 0;
                                            //foreach (var item in archivosTem)
                                            //{
                                            //    string pathTemp = GetPathTempFile(item);
                                            //    File.WriteAllBytes(pathTemp, item.archivo);
                                            //    files[c] = pathTemp;
                                            //    c++;
                                            //}

                                            if (!User.Details.IsAvailableEmail || !File.Exists(User.Pathnsf))
                                            {
                                                //Configurar Correo.
                                                await dialog.SendMessage(StringResources.ttlAtencion, "Su cuenta de correo electrónico aún no esta configurada con la plataforma Diseño del proceso. \n\n A continuación iniciará el proceso de configuración.");

                                                await dialog.SendProgressAsync(User.Nombre + StringResources.msgParaTuInf, StringResources.msgProcesoConfiguracion);

                                                ProgressDialogController AsyncProgressConfigEmail;

                                                AsyncProgressConfigEmail = await dialog.SendProgressAsync(StringResources.ttlEspereUnMomento + User.Nombre + "...", StringResources.msgEstamosConfigurando);

                                                ConfigEmailViewModel configEmail = new ConfigEmailViewModel(User);

                                                // Se reciben valores de las 2 propiedades del objeto
                                                DO_PathMail respuestaConfigEmail = await configEmail.setEmail();

                                                await AsyncProgressConfigEmail.CloseAsync();

                                                if (respuestaConfigEmail.respuesta)
                                                {
                                                    // Actualizamos el path de usuario en la misma sesión
                                                    User.Pathnsf = respuestaConfigEmail.rutamail;

                                                    await dialog.SendProgressAsync(StringResources.msgPerfecto + User.Nombre, StringResources.msgCuentaConfigurada);
                                                }
                                                else
                                                {
                                                    await dialog.SendMessage(StringResources.ttlOcurrioError, "Ocurrio un error al vincular su cuenta de correo electrónico. \nSu documento lo tendrá que entregar firmado al administrador de Control de documentos");
                                                }

                                            }
                                            else
                                            {
                                                bool confirmacionEnviarCorreo = enviarCorreoAprobarRechazar(nombre, version, idVersion, idUsuarioAutorizo);
                                            }

                                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosRealizados);
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
                                        //Mandamos Mensaje si hubo error al guardar los cambios de la versión
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
                        //Mandamos mensaje de que no puede haber campos vacios
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
                    }
                }
                else
                {
                    //El sistema se encuentra bloqueado
                    await dialog.SendMessage(StringResources.msgSistemaBloqueado, objBloqueo.observaciones);
                }
            }
            else
            {
                await dialog1.SendMessage(StringResources.ttlAtencion, StringResources.lblModeReadOnly);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="codigoDocumento"></param>
        /// <param name="noVersion"></param>
        /// <param name="idVersion"></param>
        /// <param name="emailUsuario"></param>
        /// <returns></returns>
        private bool enviarCorreoAprobarRechazar(string codigoDocumento, string noVersion, int idVersion, string idUsuarioAutorizo)
        {
            string email = DataManagerControlDocumentos.GetCorreoUsuario(idUsuarioAutorizo);
            Model.ControlDocumentos.Version version = DataManagerControlDocumentos.GetVersion(idVersion);
            Usuario usuarioCreo = DataManager.GetUsuario(version.id_usuario);

            ServiceEmail serviceEmail = new ServiceEmail();
            string body;
            body = "<HTML>";
            body += "<head>";
            body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
            body += "</head>";
            body += "<body text=\"white\">";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\"></font> </p>";
            body += "<ul>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que el usuario <b>" + usuarioCreo.Nombre + " " + usuarioCreo.ApellidoPaterno + " </b> ha dado de alta una nueva versión del documento <b>" + codigoDocumento + "</b> versión <b> " + noVersion + ".0" + " </b> para lo cual requiero su autorización para poderlo liberar en el sistema </font> </li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">El documento está adjunto a este correo. </font> </li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para <b> APROBAR</b> el documento favor de dar click en el siguiente link:</font> <a href=\"http://" + ipServidor + ":3000/api/aprobardocumento/id:" + idVersion + " \">Aprobar</a></li>";
            body += "<br/><br/>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para <b> RECHAZAR</b> el documento favor de dar click en el siguiente link:</font> <a href=\"http://" + ipServidor + ":3000/api/viewnoaprobar/id:" + idVersion + " \">No Aprobar</a> </li>";
            body += "<br/>";
            body += "</ul>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\"></font> </p>";
            body += "<br/>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\"></font> </p>";
            body += "<br/>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Nombre + " " + User.ApellidoPaterno + "</font> </p>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></p>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </p>";
            body += "<p></p>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </p>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </p>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </p>";

            body += "</body>";
            body += "</HTML>";

            string[] recepents = new string[2];
            recepents[0] = "raul.banuelos@mx.mahle.com";
            recepents[1] = email;

            recepents = Module.EliminarCorreosDuplicados(recepents);

            return serviceEmail.SendEmailLotusCustom(User.Pathnsf, recepents, "Control de documentos -  Solicitud de aprobación de documento: " + codigoDocumento, body, "CONTROL_DOCUMENTOS", idVersion);

            //List<string> attachments = new List<string>();

            //foreach (var item in files)
            //{
            //    attachments.Add(item);
            //}
            //return serviceEmail.SendEmailOutlook(recepents, "Control de documentos -  Solicitud de aprobación de documento: " + codigoDocumento, body, attachments);
        }

        /// <summary>
        /// Método que copia los archivos que tengan estatus obsoleto a una carpeta de respaldo
        /// </summary>
        public string _LiberarEspacioBD(int IdVersionEliminar)
        {
            try
            {
                ObservableCollection<Documento> data = DataManagerControlDocumentos.GetDocumentosObsoletos(IdVersionEliminar);

                foreach (var item in data)
                {
                    string NombreFolder = string.Empty;

                    switch (item.id_tipo_documento)
                    {
                        case 2:
                            //asignamos la ruta donde se va a crear el nuevo folder mas el nombre del folder
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\HOJA DE OPERACION ESTANDAR\" + item.nombre;
                            break;
                        case 1002:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\HOJA DE INSTRUCCION DE INSPECCION\" + item.nombre;
                            break;
                        case 1003:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\PROCEDIMIENTO OHSAS\" + item.nombre;
                            break;
                        case 1004:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\AYUDAS VISUALES\" + item.nombre;
                            break;
                        case 1005:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\PROCEDIMIENTO ESPECIFICO\" + item.nombre;
                            break;
                        case 1006:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\PROCEDIMIENTO ISO\" + item.nombre;
                            break;
                        case 1007:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\HOJA DE METODO DE TRABAJO ESTANDAR\" + item.nombre;
                            break;
                        case 1011:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\METODO DE INSPECCION ESTANDARIZADO\" + item.nombre;
                            break;
                        case 1012:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\FORMATO ESPECIFICO\" + item.nombre;
                            break;
                        case 1013:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\FORMATO OHSAS\" + item.nombre;
                            break;
                        case 1014:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\FORMATO ISO\" + item.nombre;
                            break;
                        case 1015:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\JES\" + item.nombre;
                            break;
                    }

                    if (!System.IO.Directory.Exists(NombreFolder))
                    {
                        //creamos el folder
                        System.IO.Directory.CreateDirectory(NombreFolder);
                    }

                    //Asignamos el nombre del archivo, concatenamos el nombre y el número de la version.
                    string NombreArchivo = item.nombre + "_" + item.version.no_version + item.version.archivo.ext;

                    //Creamos la ruta donde se pondran los archivos
                    string pathString = System.IO.Path.Combine(NombreFolder, NombreArchivo);

                    //Obtenemos el arreglo de bytes que representan el archivo
                    byte[] file = item.version.archivo.archivo;

                    //Lo copiamos a la carpeta
                    System.IO.File.WriteAllBytes(pathString, file);

                }

                EliminarDocumentos(data);

                return "Ok";
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// Método que elimina los archivos que esten en estatus obsoleto
        /// </summary>
        /// <param name="data"></param>
        public void EliminarDocumentos(ObservableCollection<Documento> data)
        {
            foreach (var item in data)
            {
                DataManagerControlDocumentos.DeleteArchivo(item.version.archivo);
            }
        }

        /// <summary>
        /// Método que copia los archivos de los registros eliminados a una carpeta de respaldo
        /// </summary>
        /// <returns></returns>
        public string BorrarArchivosRegistrosEliminados(string Nombre, string No_Version)
        {
            try
            {
                ObservableCollection<Documento> ListaDocumentosEliminados = DataManagerControlDocumentos.GetDocumentoEliminar(Nombre, No_Version);

                foreach (var item in ListaDocumentosEliminados)
                {
                    string NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\DOCUMENTOS ELIMINADOS\";

                    //Asignamos el nombre del archivo, concatenamos el nombre y el número de la version.
                    string NombreArchivo = item.nombre + "_" + item.version.no_version + item.version.archivo.ext;

                    //Creamos la ruta donde se pondran los archivos
                    string pathString = System.IO.Path.Combine(NombreFolder, NombreArchivo);

                    //Obtenemos el arreglo de bytes que representan el archivo
                    byte[] file = item.version.archivo.archivo;

                    //Lo copiamos a la carpeta
                    System.IO.File.WriteAllBytes(pathString, file);

                }

                EliminarArchivoDocEliminados(ListaDocumentosEliminados);

                return "Ok";
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// Método para eliminar los archivos que esten en la tabla de documentos eliminados
        /// </summary>
        public void EliminarArchivoDocEliminados(ObservableCollection<Documento> data)
        {
            foreach (var item in data)
            {
                item.version.archivo.archivo = new byte[0];
                DataManagerControlDocumentos.UpdateDocumentoEliminado(item.id_documento, item.version.archivo.archivo);
            }
        }

        /// <summary>
        /// Método para abrir y cerrar el flyout de la ventana documentos, muestra la lista de grupos por usuario
        /// </summary>
        public void abrircerrarFlyout()
        {
            if (isopen == true)
            {
                isopen = false;
                TituloBotonNotificar = StringResources.lblNotificar;
                // Se manda llamar método que valida los seleccionados, después que el flyuot se cierra
                RecorrerListas();
            }
            else
            {
                isopen = true;
                TituloBotonNotificar = StringResources.lblCerrar;
            }
        }

        /// <summary>
        /// Método para abrir o ver registros de un grupo.
        /// </summary>
        public void abrirgrupo()
        {
            if (GrupoSeleccionado.idgrupo != 0)
            {
                FrmVerIntegrantesGrupo Form = new FrmVerIntegrantesGrupo();
                GruposViewModel Data = new GruposViewModel(GrupoSeleccionado.idgrupo, User);

                Form.DataContext = Data;
                Form.ShowDialog();
            }
        }

        /// <summary>
        /// Método para abrir ventana cuando se quiere crear nuevo grupo
        /// </summary>
        public void ircreargrupo()
        {
            FrmCrearGrupo Form = new FrmCrearGrupo();

            GruposViewModel Data = new GruposViewModel(User);

            Form.DataContext = Data;
            Form.ShowDialog();

            // Cargamos de nuevo la lista de grupos, para que se actualice al momento de crear nuevo grupo
            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(User.NombreUsuario);
            ListaGrupos = ListaGrupos;
            NotifyChange("ListaGrupos");
        }

        /// <summary>
        /// Método para eliminar un grupo.
        /// </summary>
        public async void eliminargrupo()
        {
            foreach (var grupo in ListaGrupos)
            {
                if (grupo.IsSelected)
                {
                    DialogService dialog = new DialogService();
                    MetroDialogSettings settings = new MetroDialogSettings();

                    settings.AffirmativeButtonText = StringResources.lblYes;
                    settings.NegativeButtonText = StringResources.lblNo;

                    MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminarRegistro, settings, MessageDialogStyle.AffirmativeAndNegative);

                    // Se asegura que el grupo es existente
                    if (grupo.idgrupo != 0)
                    {
                        if (MessageDialogResult.Affirmative == result)
                        {
                            // Generamos lista con integrantes del grupo a eliminar
                            ObservableCollection<DO_INTEGRANTES_GRUPO> ListaIntegrantes = DataManagerControlDocumentos.GetAllIntegrantesGrupo(grupo.idgrupo);

                            // Recorremos y eliminamos integrantes del grupo
                            foreach (var usuariointegrante in ListaIntegrantes)
                            {
                                DataManagerControlDocumentos.eliminarintegrantes(grupo.idgrupo, usuariointegrante.idusuariointegrante);
                            }

                            // Eliminamos grupo ya vacío
                            DataManagerControlDocumentos.eliminarGrupos(grupo.idgrupo);

                            // Cargamos lista de grupos para que se actualice al momento de eliminar alguno
                            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(User.NombreUsuario);

                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.ttlDone);

                            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();
                        }
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                    }
                }
            }
        }

        /// <summary>
        /// Método para juntar en una sola lista los usuarios de todos los grupos y usuarios individuales
        /// </summary>
        public void UnirTodosIntegrantes(int ID_VERSION)
        {
            DataManagerControlDocumentos.EliminarRegistroVersion(ID_VERSION);

            ObservableCollection<DO_INTEGRANTES_GRUPO> listaFinal = new ObservableCollection<DO_INTEGRANTES_GRUPO>();

            foreach (var Grupo in ListaGrupos.Where(x => x.IsSelected).ToList())
            {
                ListaIntegrantes_Grupo = DataManagerControlDocumentos.GetAllIntegrantesGrupo(Grupo.idgrupo);

                foreach (var Integrante in ListaIntegrantes_Grupo)
                {
                    int r = listaFinal.Where(x => x.idusuariointegrante == Integrante.idusuariointegrante).ToList().Count;
                    if (r == 0)
                    {
                        listaFinal.Add(new DO_INTEGRANTES_GRUPO { idusuariointegrante = Integrante.idusuariointegrante });
                    }
                }
            }

            foreach (var Usuario in ListaUsuariosCorreo.Where(x => x.IsSelected).ToList())
            {
                int r = listaFinal.Where(x => x.idusuariointegrante == Usuario.usuario).ToList().Count;
                if (r == 0)
                {
                    listaFinal.Add(new DO_INTEGRANTES_GRUPO { idusuariointegrante = Usuario.usuario });
                }
            }

            foreach (var item in listaFinal)
            {
                DataManagerControlDocumentos.InsertUserNotifyVersion(item.idusuariointegrante, ID_VERSION);
            }
        }

        #region Métodos para seleccionar-deseleccionar grupos y usuarios

        /// <summary>
        /// Método que selecciona o deselecciona grupos segun sea el caso
        /// </summary>
        public void _SelecDeselecGrupos()
        {
            if (IsSelectedGrupos)
            {
                TituloCheckGrupos = StringResources.lblSeleccionarTodosGrupos;
                _DeseleccionarTodosGrupos();
                IsSelectedGrupos = false;
            }
            else
            {
                TituloCheckGrupos = StringResources.lblDeseleccionarTodosGrupos;
                _SeleccionarTodosGrupos();
                IsSelectedGrupos = true;
            }
        }

        /// <summary>
        /// Método que selecciona o deselecciona usuarios segun sea el caso
        /// </summary>
        public void _SelecDeselecUsuarios()
        {
            if (IsSelectedUsuarios)
            {
                TituloCheckUsuarios = StringResources.lblSeleccionarTodosUsuarios;
                _DeseleccionarTodosUsuarios();
                IsSelectedUsuarios = false;
            }
            else
            {
                TituloCheckUsuarios = StringResources.lblDeseleccionarTodosUsuarios;
                _SeleccionarTodosUsuarios();
                IsSelectedUsuarios = true;
            }
        }

        #endregion

        #region SelectDeselect grupos

        /// <summary>
        /// Método que selecciona todos los grupos
        /// </summary>
        public void _SeleccionarTodosGrupos()
        {
            ObservableCollection<DO_Grupos> Aux = new ObservableCollection<DO_Grupos>();

            foreach (var grupo in ListaGrupos)
            {
                grupo.IsSelected = true;
                Aux.Add(grupo);
            }

            ListaGrupos.Clear();

            foreach (var item in Aux)
            {
                ListaGrupos.Add(item);
            }
        }

        /// <summary>
        /// Método que deselecciona todos los grupos
        /// </summary>
        public void _DeseleccionarTodosGrupos()
        {
            ObservableCollection<DO_Grupos> Aux = new ObservableCollection<DO_Grupos>();

            foreach (var grupo in ListaGrupos)
            {
                grupo.IsSelected = false;
                Aux.Add(grupo);
            }

            ListaGrupos.Clear();

            foreach (var item in Aux)
            {
                ListaGrupos.Add(item);
            }
        }

        #endregion

        #region SelectDeselect usuarios

        /// <summary>
        /// Método que selecciona todos los usuarios
        /// </summary>
        public void _SeleccionarTodosUsuarios()
        {
            ObservableCollection<objUsuario> Aux = new ObservableCollection<objUsuario>();

            foreach (var usuario in ListaUsuariosCorreo)
            {
                usuario.IsSelected = true;
                Aux.Add(usuario);
            }

            ListaUsuariosCorreo.Clear();

            foreach (var item in Aux)
            {
                ListaUsuariosCorreo.Add(item);
            }
        }

        /// <summary>
        /// Método que deselecciona todos los usuarios
        /// </summary>
        public void _DeseleccionarTodosUsuarios()
        {

            ObservableCollection<objUsuario> Aux = new ObservableCollection<objUsuario>();

            foreach (var usuario in ListaUsuariosCorreo)
            {
                usuario.IsSelected = false;
                Aux.Add(usuario);
            }

            ListaUsuariosCorreo.Clear();

            foreach (var item in Aux)
            {
                ListaUsuariosCorreo.Add(item);
            }
        }

        #endregion

        /// <summary>
        /// Método para mostrar mensaje de alerta cuando no hay usuarios seleccionados
        /// </summary>
        public void RecorrerListas()
        {
            banAlertaCorreo = ListaUsuariosCorreo.Where(a => a.IsSelected == true).ToList().Count > 0 || ListaGrupos.Where(b => b.IsSelected == true).ToList().Count > 0 ? false : true;
        }

        #endregion
    }
}