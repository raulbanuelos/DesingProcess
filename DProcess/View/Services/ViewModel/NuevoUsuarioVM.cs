using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Model;
using System.Collections;
using View.Resources;
using View.Forms.Tooling;

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

        private string _Correo;
        public string Correo
        {
            get { return _Correo; }
            set { _Correo = value; NotifyChange("Correo"); }
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

        public NuevoUsuarioVM(Usuario ModelUsuario)
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

        #endregion

        #region Métodos

        public async void guardarUsuario()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgConfirmacion, setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                //Valida que los campos no estén vacíos.
                if (Validar() & ValidarSelected())
                {
                    // Asignamos el valor de la constraseña random
                    _contraseña = this.GenerarPasswordAleatoria();
                    
                    if (_contraseña.Length >= 6 )
                    {
                        //Declaramos un objeto con el cual se realiza la encriptación
                        Encriptacion encriptar = new Encriptacion();
                        //Declaramos un objeto de tipo usuarios
                        objUsuario objUsuario = new objUsuario();

                        //Asignamos los valores al objeto
                        objUsuario.usuario = encriptar.encript(_usuario);
                        objUsuario.nombre = _nombre;
                        objUsuario.APaterno = _aPaterno;
                        objUsuario.AMaterno = _aMaterno;
                        objUsuario.password = encriptar.encript(_contraseña);
                        objUsuario.Correo = Correo;
                        objUsuario.Pathnsf = "";
                        
                        //datos por default
                        objUsuario.usql = "´©³¶´¦³";
                        objUsuario.psql = "´‰“sqrr";

                        //Valida que el nombre de usuario no se repita
                        string validate = DataManagerControlDocumentos.ValidateUsuario(objUsuario);

                        //si no se repite
                        if (validate == null)
                        {
                            // Nos aseguramos que sean iguales
                            _confirmarContraseña = _contraseña;

                            //si las contraseñas son iguales
                            if (_contraseña.Equals(_confirmarContraseña))
                            {
                                //ejecutamos el método para insertar un registro a la tabla
                                string usuario = DataManagerControlDocumentos.SetUsuario(objUsuario);

                                // Declaramos la ruta para asignarle una foto de usuario por default
                                string url_foto = @"\\MXAGSQLSRV01\documents__\ESPECIFICOS\img\defaultuser.jpg";

                                // Declaramos valor para el campo
                                bool is_available_email = true;
                                bool temporal_password = true;

                                // Ejecutamos el método para insertar los registros a la tabla TBL_USER_DETAILS
                                DataManagerControlDocumentos.Insert_UserDetail(objUsuario.usuario, url_foto, is_available_email, temporal_password);

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
                                    IList Roles = DataManager.GetRoles(_usuario.NombreUsuario);
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
                                    //si el usuario tiene rol de administrador de CIT o dueño de documento
                                     if (Module.UsuarioIsRol(_usuario.Roles, 2) || Module.UsuarioIsRol(_usuario.Roles, 3))
                                    {
                                        //usuario es admin del cit o dueño del documento
                                        _usuario.PerfilCIT = true;
                                    }
                                    //si el usuario tiene rol de ingeniero
                                    if (Module.UsuarioIsRol(_usuario.Roles, 4) || Module.UsuarioIsRol(_usuario.Roles, 5) || Module.UsuarioIsRol(_usuario.Roles, 6) || Module.UsuarioIsRol(_usuario.Roles, 7))
                                    {
                                        //usuario tiene rol de ingeniero

                                        _usuario.PerfilUserProfile = true;
                                        _usuario.PerfilData = true;
                                        _usuario.PerfilQuotes = true;
                                        _usuario.PerfilRawMaterial = true;
                                        _usuario.PerfilStandarTime = true;
                                        _usuario.PerfilTooling = true;
                                        _usuario.PerfilRGP = true;   
                                    }
                                    //si el usuario tiene rol de administrador del sistema
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
                                    }

                                    //agregamos los perfiles y privilegios correspondientes
                                    DataManager.Set_PerfilUsuario(_usuario);
                                    DataManager.Set_PrivilegiosUsuario(_usuario);

                                    //se muestra un mensaje de cambios realizados.
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgUsuarioAlta);

                                    //Obtenemos la ventana actual.
                                    var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                                    //Verificamos que la pantalla sea diferente de nulo.
                                    if (window != null)
                                    {
                                        //Cerramos la pantalla
                                        window.Close();
                                    }

                                    // Declaramos el cuerpo y título del correo
                                    string title = "Tu usuario ha sido creado";
                                    string body = "";

                                    // Declaramos lista vacía para el parámetro
                                    ObservableCollection<Archivo> ListaVacia = new ObservableCollection<Archivo>();
                                
                                    // Declaramos el objeto
                                    Usuario UserCreated = new Usuario();

                                    // Obtenemos el nuevo usuario creado
                                    UserCreated = DataManager.GetUsuario(objUsuario.usuario);

                                    // Declaramos lista para guardar el nuevo usuario
                                    List<objUsuario> ListaUserCreated = new List<objUsuario>();
                                    objUsuario userCreado = new objUsuario();
                                    // Igualamos valores
                                    userCreado.Correo = UserCreated.Correo;
                                    userCreado.nombre = UserCreated.Nombre;
                                    userCreado.APaterno = UserCreated.ApellidoPaterno;
                                    userCreado.AMaterno = UserCreated.ApellidoMaterno;
                                    userCreado.Details = UserCreated.Details;
                                    ListaUserCreated.Add(userCreado);

                                    // Cargamos el cuerpo del correo
                                    body = "<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Le envío su usuario y contraseña:</P>";
                                    body += "<P><STRONG>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Usuario:</STRONG> " + encriptar.desencript(_usuario.NombreUsuario) + "</P>";
                                    body += "<P><STRONG>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Contraseña:</STRONG> " + _contraseña + "</P>";
                                    body += "<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Favor de respetar minúsculas y mayúsculas.</P>";
                                    body += "<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para el acceso a la plataforma, favor de ingresar a la siguiente ruta:</P>";
                                    body += "<P><STRONG>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TodosP/R@aul/Deploy</STRONG></P>";
                                    body += "<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;En dicha carpeta se encuentra un archivo llamado View.exe. Favor de ejecutarlo.</P>";

                                    // Abrimos ventana para notificar
                                    NotificarAViewModel vmNotificar = new NotificarAViewModel(User, body, ListaVacia, ListaUserCreated, title);
                                    WNotificarA ventanaCorreo = new WNotificarA();
                                    ventanaCorreo.DataContext = vmNotificar;
                                    ventanaCorreo.ShowDialog();
                                }
                                else
                                {
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgUsuarioAltaError);
                                }
                            }
                            else
                            {
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgContraseñaDiferente);
                            }

                        }
                        else
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgUsuarioExistente);
                        }
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgContraseñaCorta);
                    }
                }
                else
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
                }
            }
        }

        public void changed(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            _confirmarContraseña = passwordBox.Password;
        }

        public void changedPass(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            _contraseña = passwordBox.Password;
        }

        private bool Validar()
        {
            if (string.IsNullOrEmpty(_usuario) & string.IsNullOrEmpty(_nombre) & string.IsNullOrEmpty(_aPaterno) & string.IsNullOrEmpty(_aMaterno) /*& string.IsNullOrEmpty(_contraseña) & string.IsNullOrEmpty(_confirmarContraseña)*/ & string.IsNullOrEmpty(Correo))
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

        /// <summary>
        /// Método para constraseñas aleatorias
        /// </summary>
        /// <returns></returns>
        public string GenerarPasswordAleatoria()
        {
            Random random = new Random();

            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";        
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 7;
            string contraseniaAleatoria = string.Empty;

            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[random.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }

            // Retornamos la contraseña generada
            return contraseniaAleatoria;
        }

    #endregion
}
}
