using Encriptar;
using MahApps.Metro.Controls;
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
using View.Resources;

namespace View.Services.ViewModel
{
    public class CambiarContraseniaViewModel : INotifyPropertyChanged
    {
        #region Atributo

        public Usuario User;

        //Variable para evitar que se cierre la pantalla 
        public bool CierrePantalla = false;

        #endregion

        #region Constructor

        public CambiarContraseniaViewModel(Usuario ModelUsuario)
        {
            User = ModelUsuario;
        }

        #endregion

        #region Propiedades

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

        #region Commands

        /// <summary>
        /// Comando para introducir contraseña
        /// </summary>
        public ICommand ContraseniaIntroducida
        {
            get
            {
                return new RelayCommand(parametro => guardarcontrasenia((object)parametro));
            }
        }

        /// <summary>
        /// Comando para confirmar la contraseña
        /// </summary>
        public ICommand ContraseniaConfirmada
        {
            get
            {
                return new RelayCommand(parametro => confirmarcontrasenia((object)parametro));
            }
        }

        /// <summary>
        /// Comando para guardar la nueva contraseña
        /// </summary>
        public ICommand GuardarNvaContrasenia
        {
            get
            {
                return new RelayCommand(o => guardardatosnvacontrasenia());
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para tener la nueva contraseña
        /// </summary>
        /// <param name="parametro"></param>
        public void guardarcontrasenia(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            _contraseña = passwordBox.Password;
        }

        /// <summary>
        /// Método para tener la nueva contraseña confirmada
        /// </summary>
        /// <param name="parametro"></param>
        public void confirmarcontrasenia(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            _confirmarContraseña = passwordBox.Password;
        }

        /// <summary>
        /// Método para guardar la nueva información
        /// </summary>
        public async void guardardatosnvacontrasenia()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto con el cual se realiza la encriptación
            Encriptacion encriptar = new Encriptacion();

            if (!string.IsNullOrEmpty(Contraseña) && !string.IsNullOrEmpty(ConfirmarContraseña))
            {
                // Validamos la longuitud de la contraseña
                if (Contraseña.Length >= 7 && ConfirmarContraseña.Length >= 7)
                {
                    // Validamos que sean iguales
                    if (Contraseña == ConfirmarContraseña)
                    {
                        // Validamos que la contraseña nueva sea diferente a la anterior
                        if (ConfirmarContraseña != encriptar.desencript(User.Password))
                        {
                           
                            // Actualizamos el registro de la contraseña en la tabla Usuarios
                            DataManagerControlDocumentos.UpdatePass(User.NombreUsuario, encriptar.encript(Contraseña));

                            // Declaramos el valor a 0
                            bool temporal_password = false;

                            // Actualizar el valor de temporal password
                            DataManager.Update_TemporalPassword(User.NombreUsuario, temporal_password);

                            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                            var window = System.Windows.Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                            // Cambiamos la variable a verdadero 
                            CierrePantalla = true;

                            //Mensaje de que la contrase;a se guardo correctamente 
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgContraseñaGuardada);

                            //Verificamos que la pantalla sea diferente de nulo
                            if (window != null)
                            {
                                //Cerramos la pantalla
                                window.Close();
                            }
                        }
                        else
                        {
                            //Mensaje para notificar que  la contraseña no debe de ser igual a la anterior.
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgContraseñaNoValida);
                           
                        } 
                    }
                    else
                    {
                        // Mensaje para notificar que las constraseñas no coinciden.
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgContraseñaActualDiferente);
                    }
                }
                else
                {
                    // Mensaje para notificar que las constraseñas necesita más carácteres.
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorContraseña);
                    //msgErrorContraseña
                } 
            }
            else
            {
                // Mensaje para notificar que las constraseñas necesita más carácteres
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorContraseña);
                //msgErrorContraseña
            }
        }

        #endregion

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
    }
}
