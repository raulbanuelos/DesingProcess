using Model.ControlDocumentos;
using Encriptar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using System.Windows.Controls;
using Model;
using System.Collections;
using MahApps.Metro.Controls;
using System.Windows;

namespace View.Services.ViewModel
{
    public class ModificarDatosUsuario
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
        private ObservableCollection<Usuarios> _ListaUsuarios = new ObservableCollection<Usuarios>();
        public ObservableCollection<Usuarios> ListaUsuarios
        {
            get
            {
                return _ListaUsuarios;
            }
            set
            {
                _ListaUsuarios = value;
                NotifyChange("ListaUsuarios");
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

        private ObservableCollection<Model.ControlDocumentos.Rol> _listaTodosRoles = new ObservableCollection<Model.ControlDocumentos.Rol>();
        public ObservableCollection<Model.ControlDocumentos.Rol> ListaTotdosRoles
        {
            get
            {
                return _listaTodosRoles;
            }
            set
            {
                _listaTodosRoles = value;
                NotifyChange("ListaRol2");
            }
        }

        private string _usuario;
        public string usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                NotifyChange("usuario");
            }
        }

        private string _nombre;
        public string nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
                NotifyChange("nombre");
            }
        }

        private string _Apaterno;
        public string Apaterno
        {
            get
            {
                return _Apaterno;
            }
            set
            {
                _Apaterno = value;
                NotifyChange("Apaterno");
            }
        }

        private string _Amaterno;
        public string Amaterno
        {
            get
            {
                return _Amaterno;
            }
            set
            {
                _Amaterno = value;
                NotifyChange("Amaterno");
            }
        }

        private string _correo;
        public string correo
        {
            get
            {
                return _correo;
            }
            set
            {
                _correo = value;
                NotifyChange("correo");
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

        private string _patnsf;
        public string patnsf
        {
            get
            {
                return _patnsf;
            }
            set
            {
                _patnsf = value;
                NotifyChange("patnsf");
            }
        }
        #endregion

        #region Constructor
        public ModificarDatosUsuario(Usuarios SelectedItem)
        {
            //Asiganmos los valores para que se muestren 
            if (SelectedItem !=null)
            {
                usuario = SelectedItem.usuario;
                nombre = SelectedItem.nombre;
                Contraseña = SelectedItem.password;
                Apaterno = SelectedItem.APaterno;
                Amaterno = SelectedItem.AMaterno;
                correo = SelectedItem.Correo;

                if (string.IsNullOrEmpty(SelectedItem.Pathnsf))
                {
                    patnsf = "";
                }
                else
                {
                    patnsf = SelectedItem.Pathnsf;
                }

                ListaTotdosRoles = DataManagerControlDocumentos.GetRol();

                Encriptacion encriptar = new Encriptacion();
                string user = encriptar.encript(SelectedItem.usuario);
                ListaRol = DataManagerControlDocumentos.GetRol_Usuario(user);

                //muestra los roles que el usuario tenga
                foreach (var item in ListaTotdosRoles)
                {
                    if (existe(item.id_rol,ListaRol))
                    {
                        item.selected = true;
                    }
                }               
            }
        }
        #endregion

        #region Funciones
        /// <summary>
        /// funcion que obtiene los roles que tiene un usuario
        /// </summary>
        /// <param name="idRol"></param>
        /// <param name="lista"></param>
        /// <returns></returns>
        private bool existe(int idRol, ObservableCollection<Model.ControlDocumentos.Rol> lista)
        {
            bool respuesta = false;
            foreach (var item in lista)
            {
                if (item.id_rol ==idRol)
                {
                    respuesta = true;
                }
            }
            return respuesta;
        }
        #endregion

        #region Comandos
        /// <summary>
        /// comando para verificar la contraseña
        /// </summary>
        /// 
        public ICommand PasswordChanged
        {
            get
            {
                return new RelayCommand(parametro => changed((object)parametro));
            }
        }
        /// <summary>
        /// coamdno para verificar la contraseña
        /// </summary>
        public ICommand PasswordChanged1
        {
            get
            {
                return new RelayCommand(parametro => changedPass((object)parametro));
            }
        }
        /// <summary>
        /// comando para adjuntar ruta
        /// </summary>
        public ICommand _AdjuntarRuta
        {
            get
            {
                return new RelayCommand(o => adjuntarruta());
            }
        }
        /// <summary>
        /// comando para guardar los datos del usuario
        /// </summary>
        public ICommand GuardarUsuario
        {
            get
            {
                return new RelayCommand(o => guardardatos());
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// metodo para verificar el campo de confirmar contraseña
        /// </summary>
        /// <param name="parametro"></param>
        public void changed(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            _confirmarContraseña = passwordBox.Password;
        }

        /// <summary>
        /// metodo para verificar el campo de contraseña
        /// </summary>
        /// <param name="parametro"></param>
        public void changedPass(object parametro)
        {
            var passwordBox = parametro as PasswordBox;
            _contraseña = passwordBox.Password;
        }

        /// <summary>
        /// metodo para adjuntar una ruta
        /// </summary>
        private async void adjuntarruta()
        {
            DialogService dialog = new DialogService();

            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //filtramos los tipos de archivos que se pueden adjuntar
            dlg.Filter = "LOTUS Files (.nsf) | *.nsf";
            Nullable<bool> result = dlg.ShowDialog();
            Usuarios obj = new Usuarios();

            if (result == true)
            {
                try
                {
                    string ruta = (dlg.FileName);
                    //obtenemos la RUTA del archivo, solo nos interesa la ruta
                    string filename = dlg.FileName;
                    obj.extencion = System.IO.Path.GetExtension(filename);
                    obj.Pathnsf = dlg.FileName;

                    //volvemos a verificar que la extencion sea .nsf
                    if (obj.extencion ==".nsf")
                    {
                        patnsf = ruta;
                    }
                    else
                    {
                        await dialog.SendMessage("Atención", "Solo se permiten rutas con extención .nsf");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Metodo para guardar los cambios hechos a los datos del usuario
        /// </summary>
        private async void guardardatos()
        {
            DialogService dialog = new DialogService();
            MetroDialogSettings botones = new MetroDialogSettings();

            botones.AffirmativeButtonText = "SI";
            botones.NegativeButtonText = "NO";

            MessageDialogResult result = await dialog.SendMessage("Atencion", "Desea Guardar los Cambios", botones , MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                //si no se modifico el campo de contraseña pasaremos a guardar los demas datos
                //pero si se modifico haremos las validaciones correspondientes
                if (string.IsNullOrEmpty(_contraseña) || string.IsNullOrEmpty(_confirmarContraseña))
                {
                    //declaramos el objeto para encriptar el usuario
                    Encriptacion encriptar = new Encriptacion();
                    //declaramos un objeto de tipo usuarios
                    Usuarios nuevosdatos = new Usuarios();

                    //pasamos los valores
                    nuevosdatos.usuario = encriptar.encript(_usuario);
                    nuevosdatos.nombre = _nombre;
                    nuevosdatos.password = Contraseña;
                    nuevosdatos.APaterno = _Apaterno;
                    nuevosdatos.AMaterno = _Amaterno;
                    nuevosdatos.Correo = _correo;
                    nuevosdatos.Pathnsf = _patnsf;

                    //insertamos los cambios a la BD
                    int DatosUsuarios = DataManagerControlDocumentos.UpdateUsuario(nuevosdatos);

                    //obtenemos los roles que tiene un usuario
                    Usuario Dusuario = new Usuario();
                    Dusuario.NombreUsuario = nuevosdatos.usuario;

                    //borramos todos los roles que tenga un usuario
                    int borrar = DataManagerControlDocumentos.DeleteRol_Usuario(nuevosdatos.usuario);

                    //agregamos los roles
                    foreach (var item in ListaTotdosRoles)
                    {
                        if (item.selected==true)
                        {
                            Model.ControlDocumentos.Rol ObjRol = new Model.ControlDocumentos.Rol();

                            //asignamos los valores
                            ObjRol.id_rol = item.id_rol;
                            ObjRol.id_usuario = nuevosdatos.usuario;

                            //insertamos los cambios en la BD
                            int id_rolusuario = DataManagerControlDocumentos.SetRol_Usuario(ObjRol);
                        }
                    }

                    IList RolesUsuario = DataManager.GetRoles(Dusuario.NombreUsuario);
                    Dusuario.Roles = new List<Model.Rol>();

                    foreach (var item in RolesUsuario)
                    {
                        System.Type tipo = item.GetType();
                        Model.Rol rol = new Model.Rol();
                        rol.idRol = (int)tipo.GetProperty("ID_ROL").GetValue(item, null);
                        rol.NombreRol = (string)tipo.GetProperty("NOMBRE_ROL").GetValue(item, null);

                        //los agregamos a la propiedad de roles
                        Dusuario.Roles.Add(rol);
                    }

                    //eliminar todos los registros de perfil y privilegio.
                    DataManager.DeLete_PerfilUsuario(encriptar.encript(_usuario));
                    DataManager.DeletePrivilegiosUsuario(encriptar.encript(_usuario));

                    //si el usuario tiene rol de dueño de documento o administrador de CIT
                    if (Module.UsuarioIsRol(Dusuario.Roles, 2) || Module.UsuarioIsRol(Dusuario.Roles, 3))
                    {
                        Dusuario.PerfilCIT = true;
                    }
                    //si el usuario tiene rol de ingeniero
                    if (Module.UsuarioIsRol(Dusuario.Roles, 4) || Module.UsuarioIsRol(Dusuario.Roles, 5) || Module.UsuarioIsRol(Dusuario.Roles, 6) || Module.UsuarioIsRol(Dusuario.Roles, 7))
                    {
                        Dusuario.PerfilData = true;
                        Dusuario.PerfilQuotes = true;
                        Dusuario.PerfilRawMaterial = true;
                        Dusuario.PerfilStandarTime = true;
                        Dusuario.PerfilTooling = true;
                        Dusuario.PerfilUserProfile = true;
                        Dusuario.PerfilRGP = true;
                    }
                    //si el usuario tiene rol de administrador de sistema
                    if (Module.UsuarioIsRol(Dusuario.Roles, 1))
                    {
                        Dusuario.PerfilCIT = true;
                        Dusuario.PerfilData = true;
                        Dusuario.PerfilHelp = true;
                        Dusuario.PerfilQuotes = true;
                        Dusuario.PerfilRawMaterial = true;
                        Dusuario.PerfilStandarTime = true;
                        Dusuario.PerfilTooling = true;
                        Dusuario.PerfilUserProfile = true;
                        Dusuario.PerfilRGP = true; 
                    }

                    //agregamos los perfiles y privilegios correspondientes
                    DataManager.Set_PerfilUsuario(Dusuario);
                    DataManager.Set_PrivilegiosUsuario(Dusuario);

                    await dialog.SendMessage("Información", "Datos Modificados Correctamente");

                    //obtenemos la ventana anterior
                    var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                    if (window != null)
                    {
                        //Cerramos la pantalla actual
                        window.Close();
                    }
                }
                else
                {
                    //verificamos que la longitud de la contraseña sea  mayor o igual que 6
                    if (_contraseña.Length>=6)
                    {
                        //verificamos que los campos de contraseña y confirmar contraseña sean iguales
                        if (_contraseña.Equals(_confirmarContraseña))
                        {

                            //declaramos el objeto con el cual se encripta la contraseña
                            Encriptacion encriptar = new Encriptacion();

                            //declaramos un objeto de tipo usuarios
                            Usuarios nuevosdatos = new Usuarios();

                            //pasaremos los valores
                            nuevosdatos.usuario = encriptar.encript(_usuario);
                            nuevosdatos.nombre = _nombre;
                            nuevosdatos.APaterno = _Apaterno;
                            nuevosdatos.AMaterno = _Amaterno;
                            nuevosdatos.Correo = _correo;
                            nuevosdatos.password = encriptar.encript(_contraseña);
                            nuevosdatos.Pathnsf = _patnsf;

                            //insertamos los datos a la BD
                            int datosusuario = DataManagerControlDocumentos.UpdateUsuario(nuevosdatos);

                            Usuario Dusuario = new Usuario();
                            Dusuario.NombreUsuario = nuevosdatos.usuario;

                            //borramos todos los roles que tenga un usuario
                            int borrar = DataManagerControlDocumentos.DeleteRol_Usuario(nuevosdatos.usuario);

                            foreach (var item in ListaTotdosRoles)
                            {
                                if (item.selected == true)
                                {
                                    Model.ControlDocumentos.Rol ObjRol = new Model.ControlDocumentos.Rol();

                                    ObjRol.id_rol = item.id_rol;
                                    ObjRol.id_usuario = nuevosdatos.usuario;

                                    //le agregamos el rol a cada usuario
                                    int id_RolUsuario = DataManagerControlDocumentos.SetRol_Usuario(ObjRol);
                                }
                            }

                            //obtenemos los roles del usuario
                            IList RolesUsuario = DataManager.GetRoles(Dusuario.NombreUsuario);
                            Dusuario.Roles = new List<Model.Rol>();

                            foreach (var item in RolesUsuario)
                            {
                                System.Type tipo = item.GetType();
                                Model.Rol rol = new Model.Rol();
                                rol.idRol = (int)tipo.GetProperty("ID_ROL").GetValue(item, null);
                                rol.NombreRol = (string)tipo.GetProperty("NOMBRE_ROL").GetValue(item, null);

                                //los agregamos a la propiedad de roles
                                Dusuario.Roles.Add(rol);
                            }
                            //borramos el perfil y los privilegios que tenga el usuario
                            DataManager.DeLete_PerfilUsuario(encriptar.encript(_usuario));
                            DataManager.DeletePrivilegiosUsuario(encriptar.encript(_usuario));

                            //si el usuario tiene rol de dueño de documento o administrador de CIT
                            if (Module.UsuarioIsRol(Dusuario.Roles, 2) || Module.UsuarioIsRol(Dusuario.Roles, 3))
                            {
                                Dusuario.PerfilCIT = true;
                            }
                            //si el usuario tiene rol de ingeniero
                            if (Module.UsuarioIsRol(Dusuario.Roles, 4) || Module.UsuarioIsRol(Dusuario.Roles, 5) || Module.UsuarioIsRol(Dusuario.Roles, 6) || Module.UsuarioIsRol(Dusuario.Roles, 7))
                            {
                                Dusuario.PerfilData = true;
                                Dusuario.PerfilQuotes = true;
                                Dusuario.PerfilRawMaterial = true;
                                Dusuario.PerfilStandarTime = true;
                                Dusuario.PerfilTooling = true;
                                Dusuario.PerfilUserProfile = true;
                                Dusuario.PerfilRGP = true;
                            }
                            //si el usuario tiene rol de administrador
                            if (Module.UsuarioIsRol(Dusuario.Roles, 1))
                            {
                                Dusuario.PerfilCIT = true;
                                Dusuario.PerfilData = true;
                                Dusuario.PerfilHelp = true;
                                Dusuario.PerfilQuotes = true;
                                Dusuario.PerfilRawMaterial = true;
                                Dusuario.PerfilStandarTime = true;
                                Dusuario.PerfilTooling = true;
                                Dusuario.PerfilUserProfile = true;
                                Dusuario.PerfilRGP = true;
                            }

                            //asignamos los perfiles y los privilegios correspondientes
                            DataManager.Set_PerfilUsuario(Dusuario);
                            DataManager.Set_PrivilegiosUsuario(Dusuario);

                            await dialog.SendMessage("Información","Datos Modificados Correctamente");

                            //obtenemos la ventana anterior
                            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                            if (window != null)
                            {
                                //Cerramos la pantalla actual
                                window.Close();
                            }
                        }
                        else
                        {
                            await dialog.SendMessage("Atencion","Las contraseñas no son iguales");
                        }
                    }else
                    {
                        await dialog.SendMessage("Atencion", "La contraseña debe de tener mas de 6 caracteres");
                    }
                }
            }
        }
        #endregion
    }
}
