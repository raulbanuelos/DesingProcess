using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.LeccionesAprendidas;
using View.Resources;

namespace View.Services.ViewModel
{
    public class LeccionesAprendidasVM : INotifyPropertyChanged
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
        private ObservableCollection<LeccionesAprendidas> _Lista;
        public ObservableCollection<LeccionesAprendidas> Lista
        {
            get
            {
                return _Lista;
            }
            set
            {
                _Lista = value;
                NotifyChange("Lista");
            }
        }

        private ObservableCollection<Usuarios> _ListaUsuarios;
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

        private LeccionesAprendidas _SelectedLeccion;
        public LeccionesAprendidas SelectedLeccion
        {
            get
            {
                return _SelectedLeccion;
            }
            set
            {
                _SelectedLeccion = value;
                NotifyChange("SelectedLeccion");
            }
        }

        public Usuario user;

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

        #region Constructor
        public LeccionesAprendidasVM(Usuario ModelUsuario)
        {
            user = ModelUsuario;
            Constructor();
        }
        #endregion

        #region Comandos

        /// <summary>
        /// Comando para Modificar una Leccion Seleccionada
        /// </summary>
        public ICommand EditarLeccion
        {
            get
            {
                return new RelayCommand(o => editarleccion(user));
            }
        }

        /// <summary>
        /// comando para buscar una leccion
        /// </summary>
        public ICommand BuscarLeccion
        {
            get
            {
                return new RelayCommand(param => buscarleccion((string)param));
            }
        }

        public ICommand InsertNuevaLeccion
        {
            get
            {
                return new RelayCommand(a => InsertarNuevaLeccion(user));
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método que obtiene la lista de las lecciones aprendidas
        /// </summary>
        private void Constructor()
        {
            Lista = DataManagerControlDocumentos.GetLec("");
            CreateMenuItems();
        }

        /// <summary>
        /// Método para modificar una leccion seleccionada
        /// </summary>
        private void editarleccion(Usuario ModelUsuario)
        {
            if (SelectedLeccion!=null)
            {
                user = ModelUsuario;
                //declaramos un objeto de tipo ModificarLeccion
                ModificarLeccion Form = new ModificarLeccion();

                //declaramos un objeto de tipo ModificarLeccionVM
                ModificarLeccionVM context = new ModificarLeccionVM(SelectedLeccion,user);

                //abrimos la ventana
                Form.DataContext = context;
                Form.ShowDialog();
                Lista = DataManagerControlDocumentos.GetLec("");
            }
        }

        /// <summary>
        /// Método para buscar una leccion
        /// </summary>
        private void buscarleccion(string TextoBusqueda)
        {
            Lista = DataManagerControlDocumentos.GetLec(TextoBusqueda);
        }

        /// <summary>
        /// Método para generar el menu de hamburguesa
        /// </summary>
        public void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();

            //Boton para agregar una nueva lección aprendida
            this.MenuItems.Add(
                 new HamburgerMenuIconItem()
                 {
                     Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.File },
                     Label = StringResources.lblNuevaLeccion,
                     Command = InsertNuevaLeccion,
                     Tag = StringResources.lblNuevaLeccion,
                 }
            );
        }

        /// <summary>
        /// Método que muetra la pantalla para insertar una nueva lección aprendida
        /// </summary>
        private void InsertarNuevaLeccion(Usuario ModelUsuario)
        {
            InsertarNuevaLeccion Insertar = new InsertarNuevaLeccion();
            InsertarNuevaLeccionVW InsertarVW = new InsertarNuevaLeccionVW(ModelUsuario);
            Insertar.DataContext = InsertarVW;
            Insertar.ShowDialog();
            Lista = DataManagerControlDocumentos.GetLec("");
        }
        #endregion
    }
}
