using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        # region Propiedades
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
                return _password;
            }
            set
            {
                _password = value;
                NotifyChange("Password");
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
        #endregion

        #region Contructor

        #endregion

    }
}
