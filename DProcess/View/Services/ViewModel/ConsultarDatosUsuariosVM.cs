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
using View.Forms.User;
using MaterialDesignThemes.Wpf;
using MahApps.Metro.Controls.Dialogs;
using System.Collections;

namespace View.Services.ViewModel
{
    public class ConsultarDatosUsuariosVM : INotifyPropertyChanged
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

        #region Properties
        public Usuarios usuario;

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

        private Usuarios _selectedItem;
        public Usuarios SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyChange("SelectedItem");
            }

        }

        private Rol _rol;
        public Rol rol
        {
            get
            {
                return _rol;
            }
            set
            {
                _rol = value;
                NotifyChange("rol");
            }
        }

        private string _user;
        public string user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                NotifyChange("user");
            }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// metodo que llena la lista con los datos del usuario
        /// al momento de abrir la pantalla
        /// </summary>
        /// <param name="ModelUsuario"></param>
        public ConsultarDatosUsuariosVM()
        {
            ConstructorVista();
        }
        #endregion

        #region Comandos
        /// <summary>
        /// comando que busca el usuario por el nombre de usuario
        /// </summary>
        public ICommand ConsultarUsuarios
        {
            get
            {
                return new RelayCommand(param => GetUsuario((string)param));
            }
        }
        /// <summary>
        /// comando para ver la informacion del usuario y modificarla
        /// </summary>
        public ICommand ModificarUsuario
        {
            get
            {
                return new RelayCommand(o => modificardatos());
            }
        }
        /// <summary>
        /// comando para eliminar a un usuario
        /// </summary>
        public ICommand EliminarUsuario
        {
            get
            {
                return new RelayCommand(o => eliminarusuario(SelectedItem));
            }
        }

        #endregion

        #region metodos
        /// <summary>
        /// metodo que busca 
        /// </summary>
        /// <param name="buscar"></param>
        private void GetUsuario(string usuario)
        {
            Encriptacion encriptar = new Encriptacion();
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios(usuario);

            if (_ListaUsuarios.Count > 0)
            {
                foreach (var item in ListaUsuarios)
                {
                    //obtiene el id del rol que tiene cada usuario
                    ObservableCollection<Rol> listaRolUsuario = (DataManagerControlDocumentos.GetRol_Usuario(encriptar.encript(item.usuario)));
                    //mandamos llamar una funcion que nos ayudara a obtener el nombre de los roles en base al nombre del usuario
                    item.roles = tipo_rol(listaRolUsuario);
                }
            }
        }
        /// <summary>
        /// metodo que obtiene la lista de usuarios con sus respectivos roles
        /// para mostrarlos en la vista
        /// </summary>
        private void ConstructorVista()
        {
            Encriptacion encriptar = new Encriptacion();
            //obtenemos la lista de usuarios
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios("");

            if (_ListaUsuarios.Count > 0)
            {
                foreach (var item in ListaUsuarios)
                {
                    //obtiene el id del rol que tiene cada usuario
                    ObservableCollection<Rol> listaRolUsuario = (DataManagerControlDocumentos.GetRol_Usuario(encriptar.encript(item.usuario)));
                    //mandamos llamar una funcion que nos ayudara a obtener el nombre de los roles en base al nombre del usuario
                    item.roles = tipo_rol(listaRolUsuario);
                }
            }
        }
        /// <summary>
        /// metodo para ver la informacion de un usuario
        /// y modificarla
        /// </summary>
        /// <param name="id"></param>
        private void modificardatos()
        {
            if (SelectedItem != null)
            {
                ModificarUsuario er = new ModificarUsuario();
                ModificarDatosUsuario context = new ModificarDatosUsuario(SelectedItem);

                er.DataContext = context;
                er.ShowDialog();

                ConstructorVista();
            }
        }
        
        /// <summary>
        /// metodo para eliminar a un usuario
        /// </summary>
        /// <param name="id"></param>
        private async void eliminarusuario(Usuarios id_usuario)
        {
            DialogService dialog = new DialogService();
            MetroDialogSettings botones = new MetroDialogSettings();

            Encriptacion encriptar = new Encriptacion();

            botones.AffirmativeButtonText = "SI";
            botones.NegativeButtonText = "NO";

            if (id_usuario != null)
            {
                MessageDialogResult resultado = await dialog.SendMessage("Atencion", "Desea Eliminar el Usuario", botones, MessageDialogStyle.AffirmativeAndNegative);
                if (resultado ==MessageDialogResult.Affirmative)
                {
                    bool r = DataManagerControlDocumentos.ContarDocumentos(encriptar.encript(id_usuario.usuario));
                    if (SelectedItem!=null)
                    {
                        if (r == true)
                        {
                            //si el usuario tiene documentos
                            await dialog.SendMessage("Atencion", "El Usuario Tiene documentos, Solo se le Borraran los Privilegios");
                            Model.DataManager.DeletePrivilegiosUsuario(encriptar.encript(id_usuario.usuario));
                            Model.DataManager.DeLete_PerfilUsuario(encriptar.encript(id_usuario.usuario));
                            DataManagerControlDocumentos.DeleteRol_Usuario(encriptar.encript(id_usuario.usuario));
                            ConstructorVista();

                        }
                        else
                        {
                            //si el usuario no tiene documentos
                            Model.DataManager.DeletePrivilegiosUsuario(encriptar.encript(id_usuario.usuario));
                            Model.DataManager.DeLete_PerfilUsuario(encriptar.encript(id_usuario.usuario));
                            DataManagerControlDocumentos.DeleteRol_Usuario(encriptar.encript(id_usuario.usuario));
                            DataManagerControlDocumentos.DeleteUsuarios(id_usuario);
                            await dialog.SendMessage("Atencion", "El Usuario se Elimino correctamenre");
                            ConstructorVista();
                        }
                    }

                }
            }
        }
        #endregion

        #region funciones
        /// <summary>
        /// funcion que obtiene los nombres de roles
        /// </summary>
        /// <param name="listaRolUsuario"></param>
        /// <returns></returns>
        public string tipo_rol(ObservableCollection<Rol> listaRolUsuario)
        {
            string nombre_rol = "";
            foreach (Rol rol in listaRolUsuario)
            {
                nombre_rol += rol.nombre_rol + "\n";
            }
            return nombre_rol;
        }
        #endregion
    }
}
