using Encriptar;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using View.Forms.ControlDocumentos;
using MahApps.Metro.Controls;

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

        private string _contraseñaActual,nuevaContraseña,confimarContraseña;
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

        /// <summary>
        /// Método para guardar text de passwordBox 
        /// </summary>
        public ICommand PasswordChanged1
        {
            get
            {
                return new RelayCommand(parametro => changedPass((object)parametro));
            }
        }

        public void changedPass(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            _contraseñaActual = passwordBox.Password;
        }

        public ICommand ChangedNewPass
        {
            get
            {
                return new RelayCommand(parametro => changedNewPass((object)parametro));
            }
        }

        public void changedNewPass(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            nuevaContraseña = passwordBox.Password;
        }

        public ICommand ChangedConfirmPass
        {
            get
            {
                return new RelayCommand(parametro => changedConfirmPass((object)parametro));
            }
        }

        public void changedConfirmPass(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            confimarContraseña = passwordBox.Password;
        }

        public ICommand ModificarPass
        {
            get
            {
                return new RelayCommand(parametro => modificar());
            }
        }

        private async void modificar()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Deseas guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                
                if (Valida())
                {
                    if (confimarContraseña.Length >=6 && _contraseñaActual.Length >=6 && nuevaContraseña.Length>=6)
                    {
                        string pass = encriptar.desencript(DataManagerControlDocumentos.GetPass(user.NombreUsuario));

                        if (pass.Equals(_contraseñaActual))
                        {
                            if (nuevaContraseña.Equals(confimarContraseña))
                            {
                                string passEncrip = encriptar.encript(nuevaContraseña);

                                int update = DataManagerControlDocumentos.UpdatePass(user.NombreUsuario, passEncrip);

                                if (update !=0)
                                {
                                    //se muestra un mensaje de cambios realizados.
                                    await dialog.SendMessage("Información", "Contraseña modificada");

                                }
                                else
                                {
                                    await dialog.SendMessage("Alerta", "Error al cambiar la contraseña");
                                }
                            }
                            else
                            {
                                await dialog.SendMessage("Alerta", "La contraseña no coincide");
                            }
                        }
                        else
                        {
                            await dialog.SendMessage("Alerta", "Error la contraseña actual no coincide");
                        }
                    }
                    else
                    {
                        await dialog.SendMessage("Alerta", "La contraseña debe tener mínimo 6 caracteres.");
                    }
                }
                else
                {
                    await dialog.SendMessage("Alerta", "Error debe llenar todos los campos");
                }
            }
        }

        #region Metodos

        public bool Valida()
        {
            if (string.IsNullOrEmpty(confimarContraseña) || string.IsNullOrEmpty(_contraseñaActual) || string.IsNullOrEmpty(nuevaContraseña))
                return false;
            return true;
        }

        #endregion
    }
}
