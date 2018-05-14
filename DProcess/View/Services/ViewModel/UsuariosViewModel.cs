using Model.ControlDocumentos;
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
        private ObservableCollection<Model.ControlDocumentos.Usuarios> _ListaUsuariosCorreo;
        public ObservableCollection<Model.ControlDocumentos.Usuarios> ListaUsuariosCorreo
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
        public UsuariosViewModel(string usuario, string usuarioautorizo)
        {
            ListaUsuariosCorreo = DataManagerControlDocumentos.GetUsuarios();
            //iteramos la lista
            //para 
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
        #endregion
    }
}
