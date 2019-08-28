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
        DO_Grupos Grupos;
        
        public int ID_GRUPO { get; set; }
        
        public string nombre
        {
            get
            {
                return Grupos.nombre;
            }
            set
            {
                Grupos.nombre = value;
                NotifyChange("nombre");
            }
        }

        public int idgrupo
        {
            get
            {
                return Grupos.idgrupo;
            }
            set
            {
                Grupos.idgrupo = value;
                NotifyChange("idgrupo");
            }
        }
        #endregion

        #region Propiedades

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

        private ObservableCollection<Usuarios> _ListaUsuarios2;
        public ObservableCollection<Usuarios> ListaUsuarios2
        {
            get
            {
                return _ListaUsuarios2;
            }
            set
            {
                _ListaUsuarios2 = value;
                NotifyChange("ListaUsuarios2");
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

        public ICommand EliminarGrupo
        {
            get
            {
                return new RelayCommand(a => eliminargrupo());
            }
        }

        #endregion

        #region Constructores

        public GruposViewModel(int idGrupoSeleccionado, Usuario usuariolog)
        {
            user = usuariolog;
            IdGrupoSeleccionado = idGrupoSeleccionado;
            ListaIntegrantes_Grupo = DataManagerControlDocumentos.GetAllIntegrantesGrupo(idGrupoSeleccionado);
            ListaIntegrantes_Grupo = ListaIntegrantes_Grupo;
            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(user.IdUsuario);
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
        /// Método para obtener todos los registros de la tabla Usuarios desde 2 métodos.
        /// </summary>
        /// <returns></returns>
        public void ObtenerListaUsuarios()
        {
            ListadeUsuarios = DataManagerControlDocumentos.GetUsuarios();
        }
        #endregion

        public void guardarnintegrantes()
        {
            //Eliminar un integrante de la lista
            foreach (var usuariointegrante in ListaIntegrantes_Grupo)
            {
                DataManagerControlDocumentos.eliminarintegrantes(IdGrupoSeleccionado, usuariointegrante.idusuariointegrante);
            }

            // Agregar un integrante a la lista
            foreach (var usuario in ListadeUsuarios)
            {
                if (usuario.IsSelected)
                {
                    DataManagerControlDocumentos.agregarintegrante(IdGrupoSeleccionado, usuario.usuario);
                }

            }

            ListaIntegrantes_Grupo = DataManagerControlDocumentos.GetAllIntegrantesGrupo(IdGrupoSeleccionado);
            ListaIntegrantes_Grupo = ListaIntegrantes_Grupo;
            NotifyChange("ListaIntegrantes_Grupo");
            iragregarintegrante();
        }




        /// <summary>
        /// Método para crear un nuevo grupo.
        /// </summary>
        public void crearGrupo()
        {           
            ID_GRUPO = DataManagerControlDocumentos.CrearNuevoGrupo(nombre, user.IdUsuario);
            
            foreach (var usuario in ListaUsuarios2)
            {
                if (usuario.IsSelected)
                {
                    DataManagerControlDocumentos.agregarintegrante(ID_GRUPO, usuario.usuario);
                }
            }
            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(user.IdUsuario);            
        }


        /// <summary>
        /// Método para eliminar un grupo.
        /// </summary>
        public void eliminargrupo()
        {
            foreach (var grupos in ListaGrupos)
            {
                if (grupos.IsSelected)
                {
                    foreach (var usuariointegrante in ListaIntegrantes_Grupo)
                    {
                        DataManagerControlDocumentos.eliminarintegrantes(IdGrupoSeleccionado, usuariointegrante.idusuariointegrante);
                    }
                    DataManagerControlDocumentos.eliminarGrupos(GrupoSeleccionado.idgrupo);
                }                
            }
            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(user.IdUsuario);                 
        }
    }
}
