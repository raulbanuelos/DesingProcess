using Model.ControlDocumentos;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace View.Services.ViewModel
{
    public class UsuariosViewModel : INotifyPropertyChanged
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

        private ObservableCollection<Model.ControlDocumentos.objUsuario> _ListaUsuariosCorreo;
        public ObservableCollection<Model.ControlDocumentos.objUsuario> ListaUsuariosCorreo
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

        #endregion

        #region Constructors

        /// <summary>
        /// Contructor para sellar copias documentos
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="usuarioautorizo"></param>
        public UsuariosViewModel(string usuario, string usuarioautorizo)
        {
            ListaUsuariosCorreo = DataManagerControlDocumentos.GetUsuarios();

            // iteramos la lista para seleccionar a los usuarios a notificar al momento de abrir la ventana.
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
                if (item.usuario == usuarioautorizo)
                {
                    item.IsSelected = true;
                }
            }            
        } 

        /// <summary>
        /// Constructor para cargar usuarios suscritos al eliminar documento
        /// </summary>
        /// <param name="ListaUsuariosSuscritosParaEliminar"></param>
        /// <param name="usuario"></param>
        /// <param name="usuarioautorizo"></param>
        public UsuariosViewModel(ObservableCollection<DO_UsuarioSuscrito> ListaUsuariosSuscritosParaEliminar, string usuario, string usuarioautorizo)
        {          
            ListaUsuariosCorreo = DataManagerControlDocumentos.GetUsuarios();

            // iteramos la lista para seleccionar a los usuarios a notificar al momento de abrir la ventana.
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
                if (item.usuario == usuarioautorizo)
                {
                    item.IsSelected = true;
                }
            }

            // Iteramos la lista con usuarios suscritos y los cargamos a la lista general para notificar
            foreach (var UserSuscritos in ListaUsuariosSuscritosParaEliminar)
            {
                foreach (var User in ListaUsuariosCorreo)
                {
                    if (UserSuscritos.id_usuariosuscrito == User.usuario)
                    {
                        User.IsSelected = true;
                    }
                }
            }
        }

        #endregion
    }
}
