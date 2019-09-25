using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using Model;
using Model.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using View.Forms.Routing;

namespace View.Services.ViewModel
{
    public class CalculateDimensionsViewModel : Arquetipo, INotifyPropertyChanged
    {

        #region Attributes
        DialogService dialogService;
        #endregion

        #region Properties
        #region Dimensiones Materia Prima
        private double _DiametroMateriaPrima;
        public double DiametroMateriaPrima
        {
            get { return _DiametroMateriaPrima; }
            set { _DiametroMateriaPrima = value; NotifyChange("DiametroMateriaPrima"); }
        }

        private double _WidthMateriaPrima;
        public double WidthMateriaPrima
        {
            get { return _WidthMateriaPrima; }
            set { _WidthMateriaPrima = value; NotifyChange("WidthMateriaPrima"); }
        }

        private double _ThicknessMateriaPrima;
        public double ThicknessMateriaPrima
        {
            get { return _ThicknessMateriaPrima; }
            set { _ThicknessMateriaPrima = value; NotifyChange("ThicknessMateriaPrima"); }
        }
        #endregion

        #region Dimensiones Del anillo
        private double _DiametroAnillo;
        public double DiametroAnillo
        {
            get { return _DiametroAnillo; }
            set { _DiametroAnillo = value; NotifyChange("DiametroAnillo"); }
        }

        private double _WidthAnillo;
        public double WidthAnillo
        {
            get { return _WidthAnillo; }
            set { _WidthAnillo = value; NotifyChange("WidthAnillo"); }
        }

        private double _ThicknessAnillo;
        public double ThicknessAnillo
        {
            get { return _ThicknessAnillo; }
            set { _ThicknessAnillo = value; NotifyChange("ThicknessAnillo"); }
        }

        private double _GapAnillo;
        public double GapAnillo
        {
            get { return _GapAnillo; }
            set { _GapAnillo = value; NotifyChange("GapAnillo"); }
        }

        #endregion
        
        private ObservableCollection<IOperacion> _Operaciones = new ObservableCollection<IOperacion>();
        public ObservableCollection<IOperacion> Operaciones
        {
            get
            {
                return _Operaciones;
            }
            set
            {
                _Operaciones = value;
                NotifyChange("Operaciones");
            }
        }

        private ObservableCollection<IOperacion> _ListaOperacionesOpcionales;
        public ObservableCollection<IOperacion> ListaOperacionesOpcionales
        {
            get { return _ListaOperacionesOpcionales; }
            set { _ListaOperacionesOpcionales = value; NotifyChange("ListaOperacionesOpcionales"); }
        }

        private IOperacion _OperacionSeleccionadaOpcional;
        public IOperacion OperacionSeleccionadaOpcional
        {
            get { return _OperacionSeleccionadaOpcional; }
            set { _OperacionSeleccionadaOpcional = value; NotifyChange("OperacionSeleccionadaOpcional"); }
        }

        private IOperacion _OperacionAntesAddOperacion;
        public IOperacion OperacionAntesAddOperacion
        {
            get { return _OperacionAntesAddOperacion; }
            set { _OperacionAntesAddOperacion = value; NotifyChange("OperacionAntesAddOperacion"); }
        }

        private IOperacion _OperationRouteSelected;
        public IOperacion OperationRouteSelected
        {
            get { return _OperationRouteSelected; }
            set { _OperationRouteSelected = value; NotifyChange("OperationRouteSelected"); }
        }

        public ObservableCollection<Arquetipo> ListaComponentes { get; set; }

        private Arquetipo _ComponenteSeleccionado;
        public Arquetipo ComponenteSeleccionado
        {
            get { return _ComponenteSeleccionado; }
            set { _ComponenteSeleccionado = value; NotifyChange("ComponenteSeleccionado"); }
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
                NotifyChange("MenuOptionItems");
            }
        }

        #region Flyouts
        private bool _IsOpenSave;
        public bool IsOpenSave
        {
            get { return _IsOpenSave; }
            set { _IsOpenSave = value; NotifyChange("IsOpenSave"); }
        }

        private bool _IsOpenAddOperation;
        public bool IsOpenAddOperation
        {
            get { return _IsOpenAddOperation; }
            set { _IsOpenAddOperation = value; NotifyChange("IsOpenAddOperation"); }
        }


        #endregion

        #endregion

        #region Constructors
        public CalculateDimensionsViewModel()
        {
            //HardCode
            Operaciones = Router.GetExample();
            SetNumberOperation();
            DiametroAnillo = 3.4;
            GapAnillo = 0.013;
            WidthAnillo = 0.078;
            ThicknessAnillo = 0.155;
            //Termina HardCode

            ListaOperacionesOpcionales = DataManager.GetAllOperaciones();
            crearMenuItems();
        }

        public CalculateDimensionsViewModel(ObservableCollection<IOperacion> ListaOperaciones)
        {
            Operaciones = ListaOperaciones;
            SetNumberOperation();
            ListaOperacionesOpcionales = DataManager.GetAllOperaciones();
            crearMenuItems();
        }

        #endregion

        #region Methods

        #region Flyouts
        private void openCloseFlyoutSave()
        {
            IsOpenSave = IsOpenSave ? false : true;
        }

        private void openCloseFlyoutAddOperation()
        {
            SetNumberOperation();
            IsOpenAddOperation = IsOpenAddOperation ? false : true;
        }
        #endregion

        private void calcular()
        {
            //Declaramos una lista de operaciones la cual contendrá la información de la lista original.
            ObservableCollection<IOperacion> auxOperaciones = Operaciones;

            #region Calculo de Width
            int i = auxOperaciones.Count - 1;
            int c = 0;
            double widthFinal = WidthAnillo;
            bool banUltimaOperacionWidth = true;
            SubjectWidth subjectWidth = new SubjectWidth();

            while (i >= 0)
            {
                if (auxOperaciones[i] is IObserverWidth)
                {
                    if (banUltimaOperacionWidth)
                    {
                        IObserverWidth ope = (auxOperaciones[i] as IObserverWidth);
                        //Conciderar que falta el if si es de nissei, tomar en cuenta el codigo de la clase AnilloViewModel Linea 1871
                        subjectWidth.Subscribe(ope, widthFinal);
                        banUltimaOperacionWidth = false;
                    }
                    else
                    {
                        IObserverWidth ope = (auxOperaciones[i] as IObserverWidth);
                        //Conciderar que falta el if si es de nissei, tomar en cuenta el codigo de la clase AnilloViewModel Linea 1886.
                        subjectWidth.Subscribe(ope);
                        subjectWidth.Notify(c);
                    }
                    c += 1;
                }
                i = i - 1;
            }
            #endregion

            #region Calculo de diámetro
            i = auxOperaciones.Count - 1;
            c = 0;
            SubjectDiametro subjectDiametro = new SubjectDiametro();
            bool banUltimaOperacionDiametro = true;


            double mediaGap = GapAnillo;

            while (i >= 0)
            {
                if (auxOperaciones[i] is IObserverDiametro)
                {
                    if (banUltimaOperacionDiametro)
                    {
                        var operacion = (IObserverDiametro)auxOperaciones[i];
                        if (!operacion.GapFijo)
                            operacion.Gap = mediaGap;
                        
                        subjectDiametro.Subscribe(operacion, DiametroAnillo);
                        banUltimaOperacionDiametro = false;
                    }
                    else
                    {
                        var operacion = (IObserverDiametro)auxOperaciones[i];
                        if (!operacion.GapFijo)
                            operacion.Gap = mediaGap;
                        
                        subjectDiametro.Subscribe(operacion);
                        subjectDiametro.Notify(c);
                    }
                    c += 1;
                }
                i = i - 1;
            }
            #endregion

            #region Calculo de Thickness
            i = auxOperaciones.Count - 1;
            c = 0;
            double mediaThickness = ThicknessAnillo;

            SubjectThickness subjectThickness = new SubjectThickness();
            bool banUltimaOperacionThickness = true;
            while (i >= 0)
            {
                if (auxOperaciones[i] is IObserverThickness)
                {
                    if (banUltimaOperacionThickness)
                    {
                        subjectThickness.Subscribe(auxOperaciones[i] as IObserverThickness, mediaThickness);
                        banUltimaOperacionThickness = false;
                    }
                    else
                    {
                        subjectThickness.Subscribe(auxOperaciones[i] as IObserverThickness);
                        subjectThickness.Notify(c);
                    }
                    c += 1;
                }

                i = i - 1;
            }
            #endregion

            //Asignamos el número de operación.
            SetNumberOperation();

            //Psamos la información de la lista de operaciones en la que se trabajó a la lista de operaciones original.
            Operaciones = new ObservableCollection<IOperacion>();
            Operaciones = auxOperaciones;
            NotifyChange("Operaciones");
        }

        private void addOperation()
        {
            IOperacion operacionAux = OperacionSeleccionadaOpcional;
            int index = (OperacionAntesAddOperacion.NoOperacion / 10);
            Operaciones.Insert(index, operacionAux);
            OperacionSeleccionadaOpcional = null;
            OperacionAntesAddOperacion = null;
            IsOpenAddOperation = false;
            ListaOperacionesOpcionales = new ObservableCollection<IOperacion>();
            ListaOperacionesOpcionales = DataManager.GetAllOperaciones();
        }

        private void deleteOperation()
        {
            Operaciones.Remove(OperationRouteSelected);
        }

        private void guardar()
        {
            int r = DataManager.InsertArquetipo(Codigo, DescripcionGeneral, null, true);

            if (r > 0)
            {
                r = DataManager.InsertCalculoDetalle(Codigo, WidthAnillo, ThicknessAnillo, DiametroAnillo, GapAnillo);

                if (r > 0)
                {
                    foreach (var operacion in Operaciones)
                    {
                        double matRemoverWidth, matRemoverThickness, matRemoverDiameter;
                        bool workGap = false;
                        bool gapFixed = false;
                        
                        matRemoverDiameter = operacion is IObserverDiametro ? ((IObserverDiametro)operacion).MatRemoverDiametro : 0;
                        matRemoverThickness = operacion is IObserverThickness ? ((IObserverThickness)operacion).MatRemoverThickness : 0;
                        matRemoverWidth = operacion is IObserverWidth ? ((IObserverWidth)operacion).MatRemoverWidth : 0;

                        workGap = operacion is IObserverDiametro ? ((IObserverDiametro)operacion).RemueveGap : false;
                        gapFixed = operacion is IObserverDiametro ? ((IObserverDiametro)operacion).GapFijo : false;
                        
                        DataManager.InsertCalculoArquetipo(Codigo, operacion.IdXML, matRemoverWidth, matRemoverThickness, matRemoverDiameter, workGap, gapFixed);
                    }
                }
            }
        }

        private void abrirComponente()
        {
            ListaComponentes = DataManager.GetAllArquetipoCalculo();

            WSelectComponent wSelectComponente = new WSelectComponent();
            wSelectComponente.DataContext = this;
            wSelectComponente.ShowDialog();

            if (wSelectComponente.DialogResult.HasValue && wSelectComponente.DialogResult.Value)
            {
                if (ComponenteSeleccionado != null)
                {
                    dialogService = new DialogService();

                    string codigo = ComponenteSeleccionado.Codigo;

                }
            }
        }

        private void SetNumberOperation()
        {
            #region Asignar numeración a las operaciones
            int numero = 10;
            foreach (var operacion in Operaciones)
            {
                operacion.NoOperacion = numero;
                numero += 10;
            }
            #endregion
        }

        private void crearMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();
            MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Calculator },
                    Label = "Calcular",
                    Command = Calcular,
                    Tag = "Calcular",
                });

            MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Plus },
                    Label = "Agregar operación",
                    Command = OpenCloseFlyoutAddOperation,
                    Tag = "Agregar operación",
                });

            MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Delete },
                    Label = "Eliminar operación",
                    Command = DeleteOperation,
                    Tag = "Eliminar operación",
                });

            MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.ContentSave },
                    Label = "Guardar calculo",
                    Command = OpenCloseFlyoutSave,
                    Tag = "Guardar calculo",
                });

            MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FolderLockOpen },
                    Label = "Abrir componente",
                    Command = AbrirComponente,
                    Tag = "Abrir componente",
                });
        }

        #endregion

        #region Commands

        #region Flyouts
        public ICommand OpenCloseFlyoutSave
        {
            get
            {
                return new RelayCommand(o => openCloseFlyoutSave());
            }
        }

        public ICommand OpenCloseFlyoutAddOperation
        {
            get
            {
                return new RelayCommand(o => openCloseFlyoutAddOperation());
            }
        }
        #endregion

        public ICommand Calcular
        {
            get
            {
                return new RelayCommand(o => calcular());
            }
        }

        public ICommand AddOperation
        {
            get
            {
                return new RelayCommand(o => addOperation());
            }
        }

        public ICommand DeleteOperation
        {
            get
            {
                return new RelayCommand(o => deleteOperation());
            }
        }

        public ICommand Guardar {
            get
            {
                return new RelayCommand(o => guardar());
            }
        }

        public ICommand AbrirComponente
        {
            get
            {
                return new RelayCommand(o => abrirComponente());
            }
        }
        #endregion

        #region Events INotifyPropertyChanged
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
