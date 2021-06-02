using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.ControlDocumentos;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using View.Forms.ControlDocumentos;
using MahApps.Metro.Controls;
using View.Forms.User;
using View.Resources;
using MahApps.Metro.IconPacks;

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
        //public objUsuario usuario;
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

        private bool _Consultar;
        public bool Consultar
        {
            get
            {
                return _Consultar;
            }
            set
            {
                _Consultar = value;
                NotifyChange("Consultar");
            }
        }

        private HamburgerMenuItemCollection _menuItems;
        public HamburgerMenuItemCollection MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                if (Equals(value, _menuItems)) return;
                _menuItems = value;
                //OnPropertyChanged();
                NotifyChange("MenuItems");
            }
        }

        private HamburgerMenuItemCollection _menuOptionItems;
        public HamburgerMenuItemCollection MenuOptionItems
        {
            get
            {
                return _menuOptionItems;
            }
            set
            {
                if (Equals(value, _menuOptionItems)) return;
                _menuOptionItems = value;
                //OnPropertyChanged();
                NotifyChange("MenuOptionItems");
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

            if (Module.UsuarioIsRol(user.Roles,2))
            {
                Consultar = true;
            }
            CreateMenuItems();
        }
        #endregion

        #region Comandos

        /// <summary>
        /// comando para agregar usuarios al sitema
        /// </summary>
        public ICommand AgregarUsuario
        {
            get
            {
                return new RelayCommand(o => agregarUsuario());
            }
        }

        /// <summary>
        /// comando para consultar los datos de los usuarios
        /// </summary>
        public ICommand ConsultarUsuarios
        {
            get
            {
                return new RelayCommand(o => consultarusuarios());
            }
        }

        /// <summary>
        /// comando para verificar la contraseña
        /// </summary>
        public ICommand PasswordChanged1
        {
            get
            {
                return new RelayCommand(parametro => changedPass((object)parametro));
            }
        }

        /// <summary>
        /// Comando para verificar la contraseña cambiada
        /// </summary>
        public ICommand ChangedConfirmPass
        {
            get
            {
                return new RelayCommand(parametro => changedConfirmPass((object)parametro));
            }
        }

        /// <summary>
        /// comando para modificar la nueva contraseña
        /// </summary>
        public ICommand ChangedNewPass
        {
            get
            {
                return new RelayCommand(parametro => changedNewPass((object)parametro));
            }
        }

        /// <summary>
        /// comando para modificar la contraseña
        /// </summary>
        public ICommand ModificarPass
        {
            get
            {
                return new RelayCommand(parametro => modificar());
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// metodo para agregar un nuevo usuario
        /// </summary>
        private void agregarUsuario()
        {
            //declaramos la vista donde se ingresaran los datos del nuevo usuario
            FrmNuevoUsuario frm = new FrmNuevoUsuario();
            //declaramos un objeto del ViewModel de la vista
            NuevoUsuarioVM context = new NuevoUsuarioVM(user);

            //abrimos la ventana
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// metodo para consultar los datos de los usuarios
        /// y asi poder administrarlos
        /// </summary>
        private void consultarusuarios()
        {
            //declaramos la vista donde se consultara la informacion de todos los usuarios
            FrmConsultaDatosUsuario frm = new FrmConsultaDatosUsuario();
            //declaramos un objeto del viewmodel de la vista
            ConsultarDatosUsuariosVM context = new ConsultarDatosUsuariosVM();

            //abrimos la ventana
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parametro"></param>
        public void changedPass(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            _contraseñaActual = passwordBox.Password;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parametro"></param>
        public void changedNewPass(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            nuevaContraseña = passwordBox.Password;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parametro"></param>
        public void changedConfirmPass(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            confimarContraseña = passwordBox.Password;
        }

        /// <summary>
        /// metodo para validar que ningun campo de contraseña este vacio
        /// </summary>
        /// <returns></returns>
        public bool Valida()
        {
            if (string.IsNullOrEmpty(confimarContraseña) || string.IsNullOrEmpty(_contraseñaActual) || string.IsNullOrEmpty(nuevaContraseña))
                return false;
            return true;
        }

        /// <summary>
        /// metodo para modificar la contraseña
        /// </summary>
        private async void modificar()
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
                //mandamos llamar el metodo que valida que ningun campo este vacio
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
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgContraseñaModificada);
                                }
                                else
                                {
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgContraseñaModificadaError);
                                }
                            }
                            else
                            {
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgContraseñaDiferente);
                            }
                        }
                        else
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgContraseñaActualDiferente);
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

        /// <summary>
        /// Método para general el Menú de hamburguesa
        /// </summary>
        public void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();

            if (Consultar == true)
            {
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AccountCheck},
                        Label = StringResources.lblAgregarUsuario,
                        Command = AgregarUsuario,
                        Tag = StringResources.lblAgregarUsuario,
                    }
                    );
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Account},
                        Label = StringResources.ttlAdministrarUsuarios,
                        Command = ConsultarUsuarios,
                        Tag = StringResources.ttlAdministrarUsuarios,
                    }
                    );
            }else
            {
                this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AccountCheck },
                        Label = StringResources.lblAgregarUsuario,
                        Command = AgregarUsuario,
                        Tag = StringResources.lblAgregarUsuario,
                    }
                );
            }
        }
        #endregion
    }
}
