using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Resources;

namespace View.Services.ViewModel
{
    public class CatMateriaPrimaRoladoVM
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

        private ObservableCollection<Material> _ListaMateriaPrima;
        public ObservableCollection<Material> ListaMateriaPrima
        {
            get
            {
                return _ListaMateriaPrima;
            }
            set
            {
                _ListaMateriaPrima = value;
                NotifyChange("ListaMateriaPrima");
            }
        }

        private ObservableCollection<MateriaPrimaRolado> _ListaCatMateriaRolado;
        public ObservableCollection<MateriaPrimaRolado> ListaCatMateriaRolado
        {
            get
            {
                return _ListaCatMateriaRolado;
            }
            set
            {
                _ListaCatMateriaRolado = value;
                NotifyChange("ListaCatMateriaRolado");
            }
        }

        private string _Descripcion;
        public string Descripcion { get { return _Descripcion; } set { _Descripcion = value; NotifyChange("Descripcion"); } }

        private string _UM;
        public string UM { get { return _UM; } set { _UM = value; NotifyChange("UM"); } }

        private string _Width;
        public string Width { get { return _Width; } set { _Width = value; NotifyChange("Width"); } }

        private string _Groove;
        public string Groove { get { return _Groove; } set { _Groove = value; NotifyChange("Groove"); } }

        private string _Thickness;
        public string Thickness { get { return _Thickness; } set { _Thickness = value; NotifyChange("Thickness"); } }

        private string _Ubicacion;
        public string Ubicacion { get { return _Ubicacion; } set { _Ubicacion = value; NotifyChange("Ubicacion"); } }

        private string _Especificacion_Perfil;
        public string Especificacion_Perfil { get { return _Especificacion_Perfil; } set { _Especificacion_Perfil = value; NotifyChange("Especificacion_Perfil"); } }

        #endregion

        #region Constructor
        public CatMateriaPrimaRoladoVM()
        {
            ListaMateriaPrima = DataManager.GetAllMaterialID();
            ListaCatMateriaRolado = DataManager.GetAllCatMateriaPrimaRolado(string.Empty);
            CreateMenuItems();
        }
        #endregion

        #region Comandos

        public ICommand NuevoMateriaPrima
        {
            get
            {
                return new RelayCommand(o => _NuevoMateriaPrima());
            }
        }

        public ICommand BajaMateriaPrima
        {
            get
            {
                return new RelayCommand(o => _BajaMateriaPrima());
            }
        }

        public ICommand CambioMateriaPrima
        {
            get
            {
                return new RelayCommand(o => _CambioMateriaPrima());
            }
        }

        public ICommand GuardarMateriaPrima
        {
            get
            {
                return new RelayCommand(o => _GuardarMateriaPrima());
            }
        }
        #endregion

        #region Métodos

        public void _NuevoMateriaPrima()
        {

        }

        public void _BajaMateriaPrima()
        {

        }

        public void _CambioMateriaPrima()
        {

        }

        public void _GuardarMateriaPrima()
        {

        }

        public void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();

            this.MenuItems.Add(
                 new HamburgerMenuIconItem()
                 {
                     Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Upload },
                     Label = StringResources.lblAlta,
                     Command = NuevoMateriaPrima,
                     Tag = StringResources.lblNuevo,
                 }
            );

            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.ContentSave },
                    Label = StringResources.lblGuardar,
                    Command = GuardarMateriaPrima,
                    Tag = StringResources.lblGuardar,
            });

            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.DeleteCircle},
                    Label = StringResources.lblEliminar,
                    Command = BajaMateriaPrima,
                    Tag = StringResources.lblEliminar,
                }
            );

            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Update },
                    Label = StringResources.lblCambios,
                    Command = CambioMateriaPrima,
                    Tag = StringResources.lblCambios,
                }
           );
        }
        #endregion
    }

}