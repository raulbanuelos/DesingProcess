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
using View.Forms.RawMaterial;
using View.Resources;

namespace View.Services.ViewModel
{
    public class MateriaPrimaAceroVM : INotifyPropertyChanged
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
        private Page pagina;
        public Page Pagina
        {
            get { return pagina; }
            set
            {
                pagina = value;
                NotifyChange("Pagina");
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

        private ObservableCollection<MateriaPrimaAceros> _ListaCatMateriaAcero;
        public ObservableCollection<MateriaPrimaAceros> ListaCatMateriaAcero
        {
            get
            {
                return _ListaCatMateriaAcero;
            }
            set
            {
                _ListaCatMateriaAcero = value;
                NotifyChange("ListaCatMateriaAcero");
            }
        }

        private string _CodigoMateriaPrima;
        public string CodigoMateriaPrima { get { return _CodigoMateriaPrima; } set { _CodigoMateriaPrima = value; NotifyChange("CodigoMateriaPrima"); } }

        private string _IdMaterial;
        public string IdMaterial { get { return _IdMaterial; } set { _IdMaterial = value; NotifyChange("IdMaterial"); } }

        private double _ESP_AXIAL;
        public double ESP_AXIAL { get { return _ESP_AXIAL; } set { _ESP_AXIAL = value; NotifyChange("ESP_AXIAL"); } }

        private double _ESP_RADIAL;
        public double ESP_RADIAL { get { return _ESP_RADIAL; } set { _ESP_RADIAL = value; NotifyChange("ESP_RADIAL"); } }

        private string _PROVEEDOR1;
        public string PROVEEDOR1 { get { return _PROVEEDOR1; } set { _PROVEEDOR1 = value; NotifyChange("PROVEEDOR1"); } }

        private string _PROVEEDOR2;
        public string PROVEEDOR2 { get { return _PROVEEDOR2; } set { _PROVEEDOR2 = value; NotifyChange("PROVEEDOR2"); } }

        private bool _Alta = false;
        public bool Alta { get { return _Alta; } set { _Alta = value; NotifyChange("Alta"); } }

        private bool _GuardarCambio = false;
        public bool GuardarCambio { get { return _GuardarCambio; } set { _GuardarCambio = value; NotifyChange("GuardarCambio"); } }

        private MateriaPrimaAceros _SelectedMateriaPrima;
        public MateriaPrimaAceros SelectedMateriaPrima { get { return _SelectedMateriaPrima; } set { _SelectedMateriaPrima = value; NotifyChange("SelectedMateriaPrima"); } }
        #endregion

        #region Constructor
        public MateriaPrimaAceroVM()
        {
            TipoEspec = new Material();
            ListaMateriaPrima = DataManager.GetAllMaterial();
            ListaCatMateriaAcero = DataManager.GetAllMateriaPrimaAcero(string.Empty);

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

        //public ICommand CambioMateriaPrima
        //{
        //    get
        //    {
        //        return new RelayCommand(o => _CambioMateriaPrima());
        //    }
        //}
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
            ESP_AXIAL = new double();
            ESP_RADIAL = new double();
            PROVEEDOR1 = string.Empty;
            PROVEEDOR2 = string.Empty;

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
                if (!double.IsNaN(_ESP_AXIAL) || !double.IsNaN(_ESP_RADIAL) || !string.IsNullOrEmpty(_PROVEEDOR1) || !string.IsNullOrEmpty(_PROVEEDOR2))
                {
                    //Ejecutamos el método para mostrar el mensaje con la información que el usuario capturó.El resultado lo asignamos a una variable local.
                    MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, "¿Desea eliminar el registro seleccionado?", setting, MessageDialogStyle.AffirmativeAndNegative);
                    MateriaPrimaAceros obj = new MateriaPrimaAceros();

                    int i = DataManager.deletecatmateriaprimaacero(SelectedMateriaPrima.Codigo);
                    if (i != 0)
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, "Registro eliminado correctamente");
                        _NuevoMateriaPrima();
                        ListaCatMateriaAcero = DataManager.GetAllMateriaPrimaAcero(string.Empty);
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
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblSeleccionaeElemento);
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
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.DeleteCircle },
                    Label = StringResources.lblEliminar,
                    Command = BajaMateriaPrima,
                    Tag = StringResources.lblEliminar,
                }
            );
        }

        public async void _GuardarMateriaPrima()
        {
            DialogService dialog = new DialogService();
            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, "¿Desea guardar cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);
            if (MessageDialogResult.Affirmative == result)
            {
                MateriaPrimaAceros obj = new MateriaPrimaAceros();

                obj.Codigo = CodigoMateriaPrima;
                obj.Especificacion = IdMaterial;
                obj.ESP_AXIAL = ESP_AXIAL;
                obj.ESP_RADIAL = ESP_RADIAL;
                obj.PROVEEDOR = PROVEEDOR1;
                obj.PROVEEDOR2 = PROVEEDOR2;

                int MatRealizado = 0;
                
                if (GuardarCambio)
                {
                    MatRealizado = DataManager.updateacero(obj);
                }
                else
                {
                    MatRealizado = DataManager.setcatmateriaprimaacero(obj);
                }
                if (MatRealizado != 0)
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosRealizados);
                    _NuevoMateriaPrima();
                    ListaCatMateriaAcero = DataManager.GetAllMateriaPrimaAcero(string.Empty);
                }
                else
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                }
            }

        }
        public void _SelecccionarPlaca()
        {
            GuardarCambio = true;

            if (SelectedMateriaPrima == null)
            {
                SelectedMateriaPrima = new MateriaPrimaAceros();
            }
            CodigoMateriaPrima = SelectedMateriaPrima.Codigo;
            ESP_AXIAL = SelectedMateriaPrima.ESP_AXIAL;
            ESP_RADIAL = SelectedMateriaPrima.ESP_RADIAL;
            PROVEEDOR1 = SelectedMateriaPrima.PROVEEDOR;
            PROVEEDOR2 = SelectedMateriaPrima.PROVEEDOR2;

            IdMaterial = SelectedMateriaPrima.Especificacion;
        }


        #endregion
    }
}