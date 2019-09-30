using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using View.Resources;
using System.Windows;
using Model.ControlDocumentos;

namespace View.Services.ViewModel
{
    public class GruposViewModel : INotifyPropertyChanged
    {
        #region Atributos

        private int IdGrupoSeleccionado;

        Usuario user;

        public int ID_GRUPO { get; set; }

        private string _nombre;
        public string nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
                NotifyChange("nombre");
            }
        }

        #endregion

        #region Propiedades

        //private ObservableCollection<DO_Grupos> _ListaGrupos;
        //public ObservableCollection<DO_Grupos> ListaGrupos
        //{
        //    get
        //    {
        //        return _ListaGrupos;
        //    }
        //    set
        //    {
        //        _ListaGrupos = value;
        //        NotifyChange("ListaGrupos");
        //    }
        //}

        private ObservableCollection<Usuarios> _ListadeUsuarios;
        public ObservableCollection<Usuarios> ListadeUsuarios
        {
            get
            {
                return _ListadeUsuarios;
            }
            set
            {
                _ListadeUsuarios = value;
                NotifyChange("ListadeUsuarios");
            }
        }

        private DO_INTEGRANTES_GRUPO _GrupoSeleccionado;
        public DO_INTEGRANTES_GRUPO GrupoSeleccionado
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

        #endregion

        #region Commands

        public ICommand IrAgregarIntegrante
        {
            get
            {
                return new RelayCommand(a => iragregarintegrante());
            }
        }

        public ICommand GuardarNIntegrantes
        {
            get
            {
                return new RelayCommand(a => guardarnintegrantes());
            }
        }

        public ICommand CrearGrupo
        {
            get
            {
                return new RelayCommand(a => crearGrupo());
            }
        }

        #endregion

        #region Constructores

        // Constructor para visualizar grupos y ver usuarios integrantes 
        public GruposViewModel(int idGrupoSeleccionado, Usuario usuariolog)
        {
            user = usuariolog;
            IdGrupoSeleccionado = idGrupoSeleccionado;
            ListaIntegrantes_Grupo = DataManagerControlDocumentos.GetAllIntegrantesGrupo(idGrupoSeleccionado);
            ListaIntegrantes_Grupo = ListaIntegrantes_Grupo;
            NotifyChange("ListaIntegrantes_Grupo");
            ListadeUsuarios = DataManagerControlDocumentos.GetUsuarios();

            foreach (var usuariointegrante in ListaIntegrantes_Grupo)
            {
                foreach (var usuario in ListadeUsuarios)
                {
                    if (usuariointegrante.idusuariointegrante == usuario.usuario)
                    {
                        usuario.IsSelected = true;
                    }
                }
            }
        }

        // Constructor para ver todos los usuarios y seleccionar al momento de crear un grupo
        public GruposViewModel(Usuario usuariolog)
        {
            user = usuariolog;
            ListadeUsuarios = DataManagerControlDocumentos.GetUsuarios();
        }

        #endregion

        #region Events INotifyPropertyChanged
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

        #region Métodos

        /// <summary>
        /// Método para abrir y cerrar flyout, muestra la lista de usuarios para añadir al grupo.
        /// </summary>
        public void iragregarintegrante()
        {
            if (isopen == true)
            {
                isopen = false;
            }
            else
            {
                isopen = true;
            }
        }

        /// <summary>
        /// Método para obtener todos los registros de la tabla Usuarios a partir de 2 métodos.
        /// </summary>
        /// <returns></returns>
        public void ObtenerListaUsuarios()
        {
            ListadeUsuarios = DataManagerControlDocumentos.GetUsuarios();
        }

        /// <summary>
        /// Método para actualizar lista de integrantes por grupo.
        /// </summary>
        /// <returns></returns>
        public void guardarnintegrantes()
        {
            //Eliminar integrantes de la lista
            foreach (var usuariointegrante in ListaIntegrantes_Grupo)
            {
                DataManagerControlDocumentos.eliminarintegrantes(IdGrupoSeleccionado, usuariointegrante.idusuariointegrante);
            }

            // Agregar integrantes a la lista
            foreach (var usuario in ListadeUsuarios)
            {
                if (usuario.IsSelected)
                {
                    DataManagerControlDocumentos.agregarintegrante(IdGrupoSeleccionado, usuario.usuario);
                }
            }

            // Se llena la lista de integrantes por grupo
            ListaIntegrantes_Grupo = DataManagerControlDocumentos.GetAllIntegrantesGrupo(IdGrupoSeleccionado);
            ListaIntegrantes_Grupo = ListaIntegrantes_Grupo;
            NotifyChange("ListaIntegrantes_Grupo");
            iragregarintegrante();
        }

        /// <summary>
        /// Método para crear un nuevo grupo.
        /// </summary>
        public async void crearGrupo()
        {
            DialogService dialog = new DialogService();

            MetroDialogSettings settings = new MetroDialogSettings();
            settings.AffirmativeButtonText = StringResources.lblYes;
            settings.NegativeButtonText = StringResources.lblNo;

            // Validamos que se ingrese un nombre al grupo
            if (validar())
            {
                if (ListadeUsuarios.Where(a => a.IsSelected == true).ToList().Count > 1)
                {
                    MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgConfirmacion, settings, MessageDialogStyle.AffirmativeAndNegative);

                    if (result == MessageDialogResult.Affirmative)
                    {
                        // Primero se crea nuevo grupo
                        ID_GRUPO = DataManagerControlDocumentos.CrearNuevoGrupo(nombre, user.NombreUsuario);

                        foreach (var usuario in ListadeUsuarios)
                        {
                            // Se recorre la lista de usuarios y se insertan aquellos que están seleccionados
                            if (usuario.IsSelected)
                            {
                                DataManagerControlDocumentos.agregarintegrante(ID_GRUPO, usuario.usuario);
                            }
                        }

                        if (ID_GRUPO > 0)
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.ttlDone);

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
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                        }
                    }
                }
                else
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSelecciona2MasUsuarios);
                }
            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgAsignaNombreGrupo);
            }
        }

        /// <summary>
        /// Método que valida los campos vacíos al crear un nuevo grupo, se asegura de que se inserte nombre al mismo
        /// </summary>
        private bool validar()
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

    }
}
