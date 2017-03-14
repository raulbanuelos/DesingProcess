using System.ComponentModel;
using System.Windows.Input;
using Model;
using System.Windows.Controls;
using View.Forms.Routing;

namespace View.Services.ViewModel
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        public Usuario ModelUsuario;

        #region Propiedades de Usuario
        private string _Nombre;
        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                _Nombre = value;
                NotifyChange("Nombre");
            }
        }

        private string _ApellidoPaterno;
        public string ApellidoPaterno
        {
            get
            {
                return _ApellidoPaterno;
            }
            set
            {
                _ApellidoPaterno = value;
                NotifyChange("ApellidoPaterno");
            }
        }

        private string _ApellidoMaterno;
        public string ApellidoMaterno
        {
            get
            {
                return _ApellidoMaterno;
            }
            set
            {
                _ApellidoMaterno = value;
                NotifyChange("ApellidoMaterno");
            }
        }

        private string _NombreUsuario;
        public string NombreUsuario
        {
            get
            {
                return _NombreUsuario;
            }
            set
            {
                _NombreUsuario = value;
                NotifyChange("NombreUsuario");
            }
        }

        private bool _Block;
        public bool Block
        {
            get
            {
                return _Block;
            }
            set
            {
                _Block = value;
                NotifyChange("Block");
            }
        }

        private bool _Conectado;
        public bool Conectado
        {
            get
            {
                return _Conectado;
            }
            set
            {
                _Conectado = value;
                NotifyChange("Conectado");
            }
        }
        #endregion

        #region Propiedades de navegación
        private Page pagina;
        public Page Pagina
        {
            get { return pagina; }
            set
            {
                pagina = value;
                NotifyChange("Pagina");
            }
        }
        #endregion

        #region Constructores
        public UsuarioViewModel()
        {

        }
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands

        public ICommand IrRouting
        {
            get
            {
                return new RelayCommand(o => irRouting());
            }
        }

        #endregion

        #region Métodos
        #region Métodos de navegación
        private void irRouting()
        {
            PRouting pantallaPlano = new PRouting();
            Pagina = pantallaPlano;
            pantallaPlano.DataContext = this;
        }
        #endregion
        #endregion
    }
}
