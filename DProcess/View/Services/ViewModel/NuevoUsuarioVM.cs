using Encriptar;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace View.Services.ViewModel
{
   public class NuevoUsuarioVM : INotifyPropertyChanged
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

        private string _usuario;
        public string Usuario
        {
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

        private string _aPaterno;
        public string APaterno
        {
            get
            {
                return _aPaterno;
            }
            set
            {
                _aPaterno = value;
                NotifyChange("APaterno");
            }
        }

        private string _aMaterno;
        public string AMaterno
        {
            get
            {
                return _aMaterno;
            }
            set
            {
                _aMaterno = value;
                NotifyChange("AMaterno");
            }
        }

        private string _contraseña;
        public string Contraseña
        {
            get
            {
                return _contraseña;
            }
            set
            {
                _contraseña = value;
                NotifyChange("Contraseña");
            }
        }

        private string _confirmarContraseña;
        public string ConfirmarContraseña
        {
            get
            {
                return _confirmarContraseña;
            }
            set
            {
                _confirmarContraseña = value;
                NotifyChange("ConfirmarContraseña");
            }
        }

        

        #endregion

        #region comandos

        /// <summary>
        /// Método para guardar un usuario nuevo
        /// </summary>
        public ICommand GuardarUsuario
        {
            get
            {
                return new RelayCommand(o => guardarUsuario());
            }
        }
        public async void guardarUsuario()
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
                //Valida que los campos no estén vacíos.
                if (Validar())
                {
                    //Declaramos un objeto con el cual se realiza la encriptación
                    Encriptacion encriptar = new Encriptacion();
                    //Declaramos un objeto de tipo usuarios
                    Usuarios objUsuario = new Usuarios();

                    objUsuario.usuario = encriptar.encript(_usuario);
                    objUsuario.nombre = _nombre;
                    objUsuario.APaterno = _aPaterno;
                    objUsuario.AMaterno = _aMaterno;
                    objUsuario.password = encriptar.encript(_contraseña);

                    string validate = DataManagerControlDocumentos.ValidateUsuario(objUsuario);

                    if (validate == null)
                    {
                        //ejecutamos el método para insertar un registro a la tabla
                        string usuario = DataManagerControlDocumentos.SetUsuario(objUsuario);

                        //si el usuario es diferente de vacío
                        if (usuario != string.Empty)
                        {
                            //se muestra un mensaje de cambios realizados.
                            await dialog.SendMessage("Información", "Los cambios fueron guardados exitosamente..");
                            //Obtenemos la ventana actual.
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
                            await dialog.SendMessage("Alerta", "Error al registar el usuario.");
                        }

                    }
                    else
                    {
                        await dialog.SendMessage("Alerta", "Error el usuario ya existe.");
                    }
                }
                else
                {
                    await dialog.SendMessage("RGP: Alerta", "Se debe llenar todos los campos");
                }
            }
        }

        public ICommand PasswordChanged
        {
            get
            {
                return new RelayCommand(parametro => changed(parametro));
            }
        }

        public void changed(object pass)
        {
          //  _confirmarContraseña = pass.ToString();
        }
        #endregion

        #region Métodos

        private bool Validar()
        {
            if (string.IsNullOrEmpty(_usuario) & string.IsNullOrEmpty(_nombre) & string.IsNullOrEmpty(_aPaterno) & string.IsNullOrEmpty(_aMaterno) & string.IsNullOrEmpty(_contraseña))
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
