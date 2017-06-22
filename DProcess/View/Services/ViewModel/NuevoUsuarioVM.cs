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
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Model;
using System.Collections;

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
        public Usuario User;

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

        private ObservableCollection<Model.ControlDocumentos.Rol> _listaRol = new ObservableCollection<Model.ControlDocumentos.Rol>();
        public ObservableCollection<Model.ControlDocumentos.Rol> ListaRol
        {
            get
            {
                return _listaRol;
            }
            set
            {
                _listaRol = value;
                NotifyChange("ListaRol");
            }
        }


        #endregion
        #region Constructor

        public NuevoUsuarioVM(Usuario ModelUsuario )
        {
            User = ModelUsuario;
            ListaRol= DataManagerControlDocumentos.GetRol_Usuario(User.NombreUsuario);
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
                if (Validar() & ValidarSelected())
                {
                    //Declaramos un objeto con el cual se realiza la encriptación
                    Encriptacion encriptar = new Encriptacion();
                    //Declaramos un objeto de tipo usuarios
                    Usuarios objUsuario = new Usuarios();

                    //Asignamos los valores al objeto
                    objUsuario.usuario = encriptar.encript(_usuario);
                    objUsuario.nombre = _nombre;
                    objUsuario.APaterno = _aPaterno;
                    objUsuario.AMaterno = _aMaterno;
                    objUsuario.password = encriptar.encript(_contraseña);

                    //Valida que el nombre de usuario no se repita
                   string validate = DataManagerControlDocumentos.ValidateUsuario(objUsuario);

                    //si no se repite
                    if (validate == null)
                    {
                        //si las contraseñas son iguales
                        if (_contraseña.Contains(_confirmarContraseña)) {

                            //ejecutamos el método para insertar un registro a la tabla
                            string usuario = DataManagerControlDocumentos.SetUsuario(objUsuario);

                            Usuario _usuario = new Usuario();
                            _usuario.NombreUsuario = usuario;

                            //si el usuario es diferente de vacío
                            if (usuario != string.Empty)
                            {
                                //Recorremos la lista de roles
                                foreach (var item in _listaRol)
                                {
                                    //si el rol fue seleccionado
                                    if (item.selected == true)
                                    {
                                        Model.ControlDocumentos.Rol objRol = new Model.ControlDocumentos.Rol();

                                        objRol.id_rol = item.id_rol;
                                        objRol.id_usuario = usuario;

                                        //Agregamos el rol de cada usuario
                                        int id_rolUsuario = DataManagerControlDocumentos.SetRol_Usuario(objRol);
                                    }
                                }

                                //Obtenemos los roles del usuario nuevo
                               IList Roles= DataManager.GetRoles(_usuario.NombreUsuario);
                                _usuario.Roles = new List<Model.Rol>();
                                foreach (var item in Roles)
                                {
                                    System.Type tipo = item.GetType();
                                    Model.Rol rol = new Model.Rol();
                                    rol.idRol = (int)tipo.GetProperty("ID_ROL").GetValue(item, null);
                                    rol.NombreRol = (string)tipo.GetProperty("NOMBRE_ROL").GetValue(item, null);

                                    //los agregamos a la propiedad de roles
                                    _usuario.Roles.Add(rol);
                                }

                                //Si el usuario agregado tiene rol de administrador
                                if (Module.UsuarioIsRol(_usuario.Roles, 1))
                                {
                                    //Es administrador
                                    _usuario.PerfilCIT = true;
                                    _usuario.PerfilData = true;
                                    _usuario.PerfilHelp = true;
                                    _usuario.PerfilQuotes = true;
                                    _usuario.PerfilRawMaterial = true;
                                    _usuario.PerfilStandarTime = true;
                                    _usuario.PerfilTooling = true;
                                    _usuario.PerfilUserProfile = true;
                                    _usuario.PerfilRGP = true;

                                    //Agreamos el perfil y privilegios
                                    DataManager.Set_PerfilUsuario(_usuario);
                                    DataManager.Set_PrivilegiosUsuario(_usuario);

                                }//Si el usuario agregado tiene rol de dueño de documento o CIT
                                else if(Module.UsuarioIsRol(_usuario.Roles, 2) || Module.UsuarioIsRol(_usuario.Roles, 3))
                                {
                                    //usuario es admin del cit o dueño del documento
                                    _usuario.PerfilCIT = true;
                                    _usuario.PerfilData = false;
                                    _usuario.PerfilHelp = false;
                                    _usuario.PerfilQuotes = false;
                                    _usuario.PerfilRawMaterial = false;
                                    _usuario.PerfilStandarTime = false;
                                    _usuario.PerfilTooling = false;
                                    _usuario.PerfilUserProfile = true;
                                    _usuario.PerfilRGP = false;

                                    DataManager.Set_PerfilUsuario(_usuario);
                                    DataManager.Set_PrivilegiosUsuario(_usuario);
                                }
                                else if(Module.UsuarioIsRol(_usuario.Roles, 4) || Module.UsuarioIsRol(_usuario.Roles, 5) || Module.UsuarioIsRol(_usuario.Roles, 6) || Module.UsuarioIsRol(_usuario.Roles, 7))
                                {
                                    //usuario tiene rol de ingeniero

                                    _usuario.PerfilCIT = false;
                                    _usuario.PerfilUserProfile = true;
                                    _usuario.PerfilData = true;
                                    _usuario.PerfilHelp = false;
                                    _usuario.PerfilQuotes = true;
                                    _usuario.PerfilRawMaterial = true;
                                    _usuario.PerfilStandarTime = true;
                                    _usuario.PerfilTooling = true;
                                    _usuario.PerfilRGP = true;

                                    DataManager.Set_PerfilUsuario(_usuario);
                                    DataManager.Set_PrivilegiosUsuario(_usuario);
                                }


                                 //se muestra un mensaje de cambios realizados.
                                    await dialog.SendMessage("Información", "Usuario dado de alta..");

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
                            await dialog.SendMessage("Alerta", "La contraseña no coincide.");
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
        /// <summary>
        /// Método para guardar text de passwordBox para confirmar contraseña
        /// </summary>
        public ICommand PasswordChanged
        {
            get
            {
                return new RelayCommand(parametro => changed((object)parametro));
            }
        }

        public void changed(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            _confirmarContraseña = passwordBox.Password;
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
            _contraseña = passwordBox.Password;
        }
        #endregion

        #region Métodos

        private bool Validar()
        {
            if (string.IsNullOrEmpty(_usuario) & string.IsNullOrEmpty(_nombre) & string.IsNullOrEmpty(_aPaterno) & string.IsNullOrEmpty(_aMaterno) & string.IsNullOrEmpty(_contraseña) & string.IsNullOrEmpty(_confirmarContraseña))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidarSelected()
        {
            int aux=0;
            foreach (var item in ListaRol)
            {
                if (item.selected)
                {
                    aux++;
                }
                   
            }

            if (aux == 0)
                return false;

            return true;
        }
        #endregion
    }
}
