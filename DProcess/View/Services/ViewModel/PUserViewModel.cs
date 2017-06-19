using Encriptar;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.ControlDocumentos;

namespace View.Services.ViewModel
{
    class PUserViewModel : INotifyPropertyChanged
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
        public Usuario user;
        Encriptacion encriptar = new Encriptacion();

        private string _usuario;
        public string Usuario {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                NotifyChange("Usuario");

            }
        }

        private string _password;
        public string Password {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                NotifyChange("Password");
            }
        }

        private string _nombre;
        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
                NotifyChange("Nombre");
            }
        }

        private string _apellidoPaterno;
        public string ApellidPaterno
        {
            get
            {
               return _apellidoPaterno;
            }
            set
            {
                _apellidoPaterno = value;
                NotifyChange("ApellidoPaterno");
            }
        }
        private string _apellidoMaterno;
        public string ApellidMaterno
        {
            get
            {
                return _apellidoMaterno;
            }
            set
            {
                _apellidoMaterno = value;
                NotifyChange("ApellidoMaterno");
            }
        }
        #endregion

        #region Contructor
        public PUserViewModel(Usuario ModelUsuario)
        {
            user = ModelUsuario;
            Usuario = encriptar.desencript(user.NombreUsuario);
            Nombre = user.Nombre;
            ApellidPaterno = user.ApellidoPaterno;
            ApellidMaterno = user.ApellidoMaterno;

        }
        #endregion

        public ICommand AgregarUsuario
        {
            get
            {
                return new RelayCommand(o => agregarUsuario());
            }
        }
        private void agregarUsuario()
        {
            FrmNuevoUsuario frm = new FrmNuevoUsuario();
            NuevoUsuarioVM context = new NuevoUsuarioVM(user);

            frm.DataContext = context;
            frm.ShowDialog();
        }

    }
}
