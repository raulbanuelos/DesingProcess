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
        public UsuariosViewModel()
        {
            ListaUsuariosCorreo = DataManagerControlDocumentos.GetUsuarios("");
        } 
        #endregion
    }
}
