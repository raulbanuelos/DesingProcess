using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using View.Resources;

namespace View.Services.ViewModel
{
    public class CatMateriaPrimaRoladoVM : INotifyPropertyChanged
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

        private Material _TipoEspec;
        public Material TipoEspec
        {
            get
            {
                return _TipoEspec;
            }
            set
            {
                _TipoEspec = value;
                NotifyChange("TipoEspec");
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

        private string _IdMaterial;
        public string IdMaterial { get { return _IdMaterial; } set { _IdMaterial = value; NotifyChange("IdMaterial"); } }

        private string _CodigoMateriaPrima;
        public string CodigoMateriaPrima { get { return _CodigoMateriaPrima; } set { _CodigoMateriaPrima = value; NotifyChange("CodigoMateriaPrima"); } }

        private string _UM;
        public string UM { get { return _UM; } set { _UM = value; NotifyChange("UM"); } }

        private double _Width;
        public double Width { get { return _Width; } set { _Width = value; NotifyChange("Width"); } }

        private double _Groove;
        public double Groove { get { return _Groove; } set { _Groove = value; NotifyChange("Groove"); } }

        private double _Thickness;
        public double Thickness { get { return _Thickness; } set { _Thickness = value; NotifyChange("Thickness"); } }

        private string _Ubicacion;
        public string Ubicacion { get { return _Ubicacion; } set { _Ubicacion = value; NotifyChange("Ubicacion"); } }

        private string _Especificacion_Perfil;
        public string Especificacion_Perfil { get { return _Especificacion_Perfil; } set { _Especificacion_Perfil = value; NotifyChange("Especificacion_Perfil"); } }

        private bool _Alta = false;
        public bool Alta { get { return _Alta; } set { _Alta = value; NotifyChange("Alta"); } }

        private bool _GuardarCambio = false;
        public bool GuardarCambio { get { return _GuardarCambio; } set { _GuardarCambio = value; NotifyChange("GuardarCambio"); } }

        private MateriaPrimaRolado _SelectedMateriaPrima;
        public MateriaPrimaRolado SelectedMateriaPrima { get { return _SelectedMateriaPrima; } set { _SelectedMateriaPrima = value; NotifyChange("SelectedMateriaPrima"); } }

        #endregion

        #region Constructor
        public CatMateriaPrimaRoladoVM()
        {
            TipoEspec = new Material();
            ListaMateriaPrima = DataManager.GetAllMaterial();
            ListaCatMateriaRolado = DataManager.GetAllMateriaPrimaRolado(string.Empty);
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

        public ICommand SelecccionarPlaca
        {
            get
            {
                return new RelayCommand(o => _SelecccionarPlaca());
            }
        }

        public ICommand AbrirPlaca
        {
            get
            {
                return new RelayCommand(o => _AbrirPlaca());
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Inicializa los valores de los campos
        /// </summary>
        public void _NuevoMateriaPrima()
        {
            Alta = false;
            GuardarCambio = false;

            CodigoMateriaPrima = string.Empty;
            Descripcion = string.Empty;
            UM = string.Empty;
            Width = new double();
            Groove = new double();
            Thickness = new double();
            Ubicacion = string.Empty;
            Especificacion_Perfil = string.Empty;

            ListaMateriaPrima = DataManager.GetAllMaterial();

        }

        public async void _BajaMateriaPrima()
        {
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;


            if (SelectedMateriaPrima != null)
            {
                //Ejecutamos el método para mostrar el mensaje con la información que el usuario capturó.El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, "¿Desea eliminar el registro seleccionado?", setting, MessageDialogStyle.AffirmativeAndNegative);

                MateriaPrimaRolado obj = new MateriaPrimaRolado();

                int i = DataManager.DeleteMateriaPrimaRolado(SelectedMateriaPrima.Codigo);

                if (i != 0)
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, "Registro eliminado correctamente");
                    _NuevoMateriaPrima();
                    ListaCatMateriaRolado = DataManager.GetAllMateriaPrimaRolado(string.Empty);
                }
                else
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                }
            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblSeleccionaeElemento);
            }
        }

        public void _CambioMateriaPrima()
        {

        }

        public async void _GuardarMateriaPrima()
        {
            if (!string.IsNullOrEmpty(Descripcion) || !string.IsNullOrEmpty(UM) || !string.IsNullOrEmpty(Ubicacion) || !string.IsNullOrEmpty(Especificacion_Perfil))
            {
                //Incializamos los servicios de dialog.
                DialogService dialog = new DialogService();

                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                //Ejecutamos el método para mostrar el mensaje con la información que el usuario capturó.El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, "¿Desea guardar cambios?" , setting, MessageDialogStyle.AffirmativeAndNegative);

                if (MessageDialogResult.Affirmative == result)
                {
                    MateriaPrimaRolado obj = new MateriaPrimaRolado();

                    obj.Codigo = CodigoMateriaPrima;
                    obj.Especificacion = IdMaterial;
                    obj.DescripcionGeneral = Descripcion;
                    obj.UM = UM;
                    obj._Width = Width;
                    obj.Groove = Groove;
                    obj.Thickness = Thickness;
                    obj.Ubicacion = Ubicacion;
                    obj.EspecPefil = Especificacion_Perfil;

                    int MatRealizado = 0;

                    if (GuardarCambio)
                    {
                        MatRealizado = DataManager.UpdateMateriaPrimaRolado(obj);
                    }
                    else
                    {
                        MatRealizado = DataManager.SetMateriaPrimaRolado(obj);
                    }


                    if (MatRealizado != 0)
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosRealizados);
                        _NuevoMateriaPrima();
                        ListaCatMateriaRolado = DataManager.GetAllMateriaPrimaRolado(string.Empty);
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                    }   
                }
            }
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
        }

        public void _SelecccionarPlaca()
        {

            GuardarCambio = true;

            if (SelectedMateriaPrima == null)
            {
                SelectedMateriaPrima = new MateriaPrimaRolado();
            }
            CodigoMateriaPrima = SelectedMateriaPrima.Codigo;
            Descripcion = SelectedMateriaPrima.DescripcionGeneral;
            UM = SelectedMateriaPrima.UM;
            Width = SelectedMateriaPrima._Width;
            Groove = SelectedMateriaPrima.Groove;
            Thickness = SelectedMateriaPrima.Thickness;
            Ubicacion = SelectedMateriaPrima.Ubicacion;
            Especificacion_Perfil = SelectedMateriaPrima.EspecPefil;

            IdMaterial = SelectedMateriaPrima.Especificacion;
        }

        public void _AbrirPlaca()
        {

        }
        #endregion
    }

}