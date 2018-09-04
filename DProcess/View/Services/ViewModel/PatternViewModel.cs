using System.ComponentModel;
using Model;
using System.Windows.Input;
using System;
using System.IO;
using System.Collections.Generic;
using View.Forms.Shared;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model.Interfaces;
using View.Services.Operaciones.Fundicion;
using View.Forms.Routing;
using MahApps.Metro.Controls.Dialogs;

namespace View.Services.ViewModel
{
    public class PatternViewModel : INotifyPropertyChanged
    {
        #region Atributos
        private Pattern model;
        private Anillo ModelAnillo;
        private bool calculoOk;
        private string NombreUsuario;
        bool IsPatternNew;
        #endregion

        #region Propierties

        #region Propiedades Pattern

        public String Codigo
        {
            get
            {
                return model.Codigo;
            }
            set
            {
                model.Codigo = value;
                ModelAnillo.Codigo = value;
                NotifyChange("Codigo");
            }
        }

        public Cliente customer
        {
            get
            {
                return model.customer;
            }
            set
            {
                model.customer = value;
                NotifyChange("customer");
            }
        }

        public Propiedad medida {
            get
            {
                return model.medida;
            }
            set
            {
                model.medida = value;
                NotifyChange("medida");
            }
        }

        public Propiedad diametro {
            get
            {
                return model.diametro;
            }
            set
            {
                model.diametro = value;
                NotifyChange("diametro");
            }
        }

        public Propiedad mounting {
            get { return model.mounting; }
            set { model.mounting = value; NotifyChange("mounting"); }
        }

        public PropiedadCadena on_14_rd_gate {
            get { return model.on_14_rd_gate; }
            set { model.on_14_rd_gate = value; NotifyChange("on_14_rd_gate"); }
        }

        public PropiedadCadena button {
            get { return model.button; }
            set { model.button = value; NotifyChange("button"); }
        }

        public PropiedadCadena cone {
            get { return model.cone; }
            set { model.cone = value; NotifyChange("cone"); }
        }

        public PropiedadCadena M_Circle {
            get { return model.M_Circle; }
            set { model.M_Circle = value; NotifyChange("M_Circle"); }
        }

        public Propiedad ring_w_min {
            get { return model.ring_w_min; }
            set { model.ring_w_min = value; NotifyChange("ring_w_min"); }
        }

        public Propiedad ring_w_max {
            get { return model.ring_w_max; }
            set { model.ring_w_max = value; NotifyChange("ring_w_max"); }
        }

        public PropiedadCadena date_ordered {
            get { return model.date_ordered; }
            set { model.date_ordered = value; NotifyChange("date_ordered"); }
        }

        public Propiedad B_Dia {
            get { return model.B_Dia; }
            set { model.B_Dia = value; NotifyChange("B_Dia"); }
        }

        public Propiedad fin_Dia
        {
            get { return model.fin_Dia; }
            set { model.fin_Dia = value; NotifyChange("fin_Dia"); }
        }

        public Propiedad turn_allow
        {
            get { return model.turn_allow; }
            set { model.turn_allow = value; NotifyChange("turn_allow"); }
        }

        public Propiedad cstg_sm_od
        {
            get { return model.cstg_sm_od; }
            set { model.cstg_sm_od = value; NotifyChange("cstg_sm_od"); }
        }

        public Propiedad shrink_allow
        {
            get { return model.shrink_allow; }
            set { model.shrink_allow = value; NotifyChange("shrink_allow"); }
        }

        public Propiedad patt_sm_od
        {
            get { return model.patt_sm_od; }
            set { model.patt_sm_od = value; NotifyChange("patt_sm_od"); }
        }

        public Propiedad piece_in_patt
        {
            get { return model.piece_in_patt; }
            set { model.piece_in_patt = value; NotifyChange("piece_in_patt"); }
        }

        public Propiedad bore_allow
        {
            get { return model.bore_allow; }
            set { model.bore_allow = value; NotifyChange("bore_allow"); }
        }

        public Propiedad patt_sm_id
        {
            get { return model.patt_sm_id; }
            set { model.patt_sm_id = value; NotifyChange("patt_sm_id"); }
        }
   
        public Propiedad patt_thickness {
            get { return model.patt_thickness; }
            set { model.patt_thickness = value; NotifyChange("patt_thickness"); }
        }

        public PropiedadCadena joint {
            get { return model.joint; }
            set { model.joint = value; NotifyChange("joint"); }
        }

        public PropiedadCadena nick {
            get { return model.nick; }
            set { model.nick = value; NotifyChange("nick"); }
        }

        public PropiedadCadena nick_draf {
            get { return model.nick_draf; }
            set { model.nick_draf = value; NotifyChange("nick_draf"); }
        }

        public PropiedadCadena nick_depth
        {
            get { return model.nick_depth; }
            set { model.nick_depth = value; NotifyChange("nick_depth"); }
        }
       
        public PropiedadCadena side_relief {
            get { return model.side_relief; }
            set { model.side_relief = value; NotifyChange("side_relief"); }
        }

        public Propiedad cam {
            get { return model.cam; }
            set { model.cam = value; NotifyChange("cam"); }
        }

        public Propiedad cam_roll
        {
            get { return model.cam_roll; }
            set { model.cam_roll = value; NotifyChange("cam_roll"); }
        }
        
        public Propiedad rise {
            get { return model.rise; }
            set { model.rise = value; NotifyChange("rise"); }
        }

        public Propiedad OD
        {
            get { return model.OD; }
            set { model.OD = value; NotifyChange("OD"); }
        }

        public Propiedad ID
        {
            get { return model.ID; }
            set { model.ID = value; NotifyChange("ID"); }
        }

        public Propiedad diff
        {
            get { return model.diff; }
            set { model.diff = value; NotifyChange("diff"); }
        }
        
        public FO_Item TipoMateriaPrima
        {
            get
            {
                return model.TipoMateriaPrima;
            }
            set
            {
                model.TipoMateriaPrima = value;
                NotifyChange("TipoMateriaPrima");
            }
        }
        
        public PropiedadCadena mounted
        {
            get { return model.mounted; }
            set { model.mounted = value; NotifyChange("mounted"); }
        }

        public PropiedadCadena ordered
        {
            get { return model.ordered; }
            set { model.ordered = value; NotifyChange("ordered"); }
        }

        public PropiedadCadena Checked
        {
            get { return model.Checked; }
            set { model.Checked = value; NotifyChange("Checked"); }
        }

        public PropiedadCadena date_checked
        {
            get { return model.date_checked; }
            set { model.date_checked = value; NotifyChange("date_checked"); }
        }

        public PropiedadCadena esp_inst
        {
            get { return model.esp_inst; }
            set { model.esp_inst = value; NotifyChange("esp_inst"); }
        }
       
        public Propiedad factor_k {
            get { return model.factor_k; }
            set { model.factor_k = value; NotifyChange("factor_k"); }
        }

        public Propiedad rise_built
        {
            get { return model.rise_built; }
            set { model.rise_built = value; NotifyChange("rise_built"); }
        }

        public Propiedad ring_th_min
        {
            get { return model.ring_th_min; }
            set { model.ring_th_min = value; NotifyChange("ring_th_min"); }
        }

        public Propiedad ring_th_max
        {
            get { return model.ring_th_max; }
            set { model.ring_th_max = value; NotifyChange("ring_th_max"); }
        }
 
        public PropiedadBool estado {
            get { return model.estado; }
            set { model.estado = value; NotifyChange("estado"); }
        }

        public Propiedad plato
        {
            get { return model.plato; }
            set { model.plato = value; NotifyChange("plato"); }
        }

        public PropiedadCadena detalle
        {
            get { return model.detalle; }
            set { model.detalle = value; NotifyChange("detalle"); }
        }

        public Propiedad peso_cstg
        {
            get { return model.peso_cstg; }
            set { model.peso_cstg = value; NotifyChange("peso_cstg"); }
        }

        public Propiedad cam_lever
        {
            get { return model.cam_lever; }
            set { model.cam_lever = value; NotifyChange("cam_lever"); }
        }

        public Propiedad patt_width
        {
            get { return model.patt_width; }
            set { model.patt_width = value; NotifyChange("patt_width"); }
        }

        public PropiedadCadena Hardness {
            get
            {
                return model.Hardness;
            }
            set
            {
                model.Hardness = value;
                NotifyChange("Hardness");
            }
        }

        public Propiedad HardnessMin {
            get
            {
                return model.HardnessMin;
            }
            set
            {
                model.HardnessMin = value;
                ModelAnillo.HardnessMin = value;
                NotifyChange("HardnessMin");
            }
        }

        public Propiedad HardnessMax {
            get
            {
                return model.HardnessMax;
            }
            set
            {
                model.HardnessMax = value;
                ModelAnillo.HardnessMax = value;
                NotifyChange("HardnessMax");
            }
        }

        public PropiedadCadena Proceso {
            get
            {
                return model.Proceso;
            }
            set
            {
                model.Proceso = value;
                NotifyChange("Proceso");
            }
        }

        public PropiedadCadena EspecMaterialAnillo {
            get
            {
                return model.EspecMaterialAnillo;
            }
            set
            {
                model.EspecMaterialAnillo = value;
                NotifyChange("EspecMaterialAnillo");
            }
        }
        
        public PropiedadCadena TipoAnillo {
            get
            {
                return model.TipoAnillo;
            }
            set
            {
                model.TipoAnillo = value;
                NotifyChange("TipoAnillo");
            }
        }

        public PropiedadBool diseno
        {
            get
            {
                return model.diseno;
            }
            set
            {
                model.diseno = value;
                NotifyChange("diseno");
                _IsRedondo = !model.diseno.Valor;
            }
        }

        #endregion
        
        #region Properties of anillo
        /// <summary>
        /// Cadena que representa la descripci�n general del elemento existente en sistema ERP.
        /// </summary>
        public string DescripcionGeneral
        {
            get
            {
                return ModelAnillo.DescripcionGeneral;
            }
            set
            {
                ModelAnillo.DescripcionGeneral = value;
                NotifyChange("DescripcionGeneral");
            }
        }

        /// <summary>
        /// Arreglo de Bytes que representa una imagen correspondiente al elemento.
        /// </summary>
        public byte[] Imagen
        {
            get
            {
                return ModelAnillo.Imagen;
            }
            set
            {
                ModelAnillo.Imagen = value;
                NotifyChange("Imagen");
            }
        }

        /// <summary>
        /// Booleano que representa si el elemento esta activo: true, o baja: false.
        /// </summary>
        public bool Activo
        {
            get
            {
                return ModelAnillo.Activo;
            }
            set
            {
                ModelAnillo.Activo = value;
                NotifyChange("Activo");
            }
        }

        /// <summary>
        /// Perfil que representa el di�metro exterior del anillo.
        /// </summary>
        public Perfil PerfilOD
        {
            get
            {
                return ModelAnillo.PerfilOD;
            }
            set
            {
                ModelAnillo.PerfilOD = value;
                NotifyChange("PerfilOD");
            }
        }

        /// <summary>
        /// Perfil que representa el di�metro interior del anillo.
        /// </summary>
        public Perfil PerfilID
        {
            get
            {
                return ModelAnillo.PerfilID;
            }
            set
            {
                ModelAnillo.PerfilID = value;
                NotifyChange("PerfilID");
            }
        }

        /// <summary>
        /// Perfil que representa la cara lateral del anillo.
        /// </summary>
        public Perfil PerfilLateral
        {
            get
            {
                return ModelAnillo.PerfilLateral;
            }
            set
            {
                ModelAnillo.PerfilLateral = value;
                NotifyChange("PerfilLateral");
            }
        }

        /// <summary>
        /// Perfil que representa las puntas del anillo.
        /// </summary>
        public Perfil PerfilPuntas
        {
            get
            {
                return ModelAnillo.PerfilPuntas;
            }
            set
            {
                ModelAnillo.PerfilPuntas = value;
                NotifyChange("PerfilPuntas");
            }
        }

        /// <summary>
        /// Propiedad que representa el di�metro del anillo.
        /// </summary>
        public Propiedad D1
        {
            get
            {
                return ModelAnillo.D1;
            }
            set
            {
                ModelAnillo.D1 = value;
                NotifyChange("D1");
            }
        }

        /// <summary>
        /// Propiedad que representa el width del anillo.
        /// </summary>
        public Propiedad H1
        {
            get
            {
                return ModelAnillo.H1;
            }
            set
            {
                ModelAnillo.H1 = value;
                NotifyChange("H1");
            }
        }

        /// <summary>
        /// Propiedad que representa el FreeGap del anillo.
        /// </summary>
        public Propiedad FreeGap
        {
            get
            {
                return ModelAnillo.FreeGap;
            }
            set
            {
                ModelAnillo.FreeGap = value;
                NotifyChange("FreeGap");
            }
        }

        /// <summary>
        /// Propiedad que representa el peso del anillo.
        /// </summary>
        public Propiedad Mass
        {
            get
            {
                return ModelAnillo.Mass;
            }
            set
            {
                ModelAnillo.Mass = value;
                NotifyChange("Mass");
            }
        }

        /// <summary>
        /// Propiedad que representa la tensi�n del anillo.
        /// </summary>
        public Propiedad Tension
        {
            get
            {
                return ModelAnillo.Tension;
            }
            set
            {
                ModelAnillo.Tension = value;
                NotifyChange("Tension");
            }
        }

        /// <summary>
        /// Propiedad que representa la tolerancia de la tensi�n del anillo.
        /// </summary>
        public Propiedad TensionTol
        {
            get
            {
                return ModelAnillo.TensionTol;
            }
            set
            {
                ModelAnillo.TensionTol = value;
                NotifyChange("TensionTol");
            }
        }

        /// <summary>
        /// Propiedad que representa la ovalidad m�nima del anillo.
        /// </summary>
        public Propiedad OvalityMin
        {
            get
            {
                return ModelAnillo.OvalityMin;
            }
            set
            {
                ModelAnillo.OvalityMin = value;
                NotifyChange("OvalityMin");
            }
        }

        /// <summary>
        /// Propiedad que representa la ovalidad m�xima del anillo.
        /// </summary>
        public Propiedad OvalityMax
        {
            get
            {
                return ModelAnillo.OvalityMax;
            }
            set
            {
                ModelAnillo.OvalityMax = value;
                NotifyChange("OvalityMax");
            }
        }

        /// <summary>
        /// Materia prima que representa el material base del anillo.
        /// </summary>
        public MateriaPrima MaterialBase
        {
            get
            {
                return ModelAnillo.MaterialBase;
            }
            set
            {
                ModelAnillo.MaterialBase = value;
                NotifyChange("MaterialBase");
            }
        }

        /// <summary>
        /// Cadena que representa el n�mero de plano del anillo.
        /// </summary>
        public string NoPlano
        {
            get
            {
                return ModelAnillo.NoPlano;
            }
            set
            {
                ModelAnillo.NoPlano = value;
                NotifyChange("NoPlano");
            }
        }

        /// <summary>
        /// Cadena que representa el n�mero de parte del cliente.
        /// </summary>
        public string CustomerPartNumber
        {
            get
            {
                return ModelAnillo.CustomerPartNumber;
            }
            set
            {
                ModelAnillo.CustomerPartNumber = value;
                NotifyChange("CustomerPartNumber");
            }
        }

        /// <summary>
        /// Cadena que representa el nivel de revisi�n del cliente.
        /// </summary>
        public string CustomerRevisionLevel
        {
            get
            {
                return ModelAnillo.CustomerRevisionLevel;
            }
            set
            {
                ModelAnillo.CustomerRevisionLevel = value;
                NotifyChange("CustomerRevisionLevel");
            }
        }

        /// <summary>
        /// Cadena que representa la sobre medida del plano.
        /// </summary>
        /// <example>
        /// STD: Estandar.
        /// +0.030 : Sobre medida.
        /// </example>
        public string Size
        {
            get
            {
                return ModelAnillo.Size;
            }
            set
            {
                ModelAnillo.Size = value;
                NotifyChange("Size");
            }
        }

        /// <summary>
        /// Cadena que representa el numero de documento del cliente.
        /// </summary>
        public string CustomerDocNo
        {
            get
            {
                return ModelAnillo.CustomerDocNo;
            }
            set
            {
                ModelAnillo.CustomerDocNo = value;
                NotifyChange("CustomerDocNo");
            }
        }

        /// <summary>
        /// Cadena que representa el tratamiento que tiene el anillo.
        /// </summary>
        public string Treatment
        {
            get
            {
                return ModelAnillo.Treatment;
            }
            set
            {
                ModelAnillo.Treatment = value;
                NotifyChange("Treatment");
            }
        }

        /// <summary>
        /// Cadena que representa la especificaci�n de tratamiento que tiene el anillo.
        /// </summary>
        public string EspecTreatment
        {
            get
            {
                return ModelAnillo.EspecTreatment;
            }
            set
            {
                ModelAnillo.EspecTreatment = value;
                NotifyChange("EspecTreatment");
            }
        }

        /// <summary>
        /// Cadena que representa el texto con la informaci�n del anillo general. Esto para sistema ERP.
        /// </summary>
        public string Caratula
        {
            get
            {
                return ModelAnillo.Caratula;
            }
            set
            {
                ModelAnillo.Caratula = value;
                NotifyChange("Caratula");
            }
        }

        /// <summary>
        /// Cliente que representa a cual pertenece el anillo.
        /// </summary>
        public Cliente cliente
        {
            get
            {
                return ModelAnillo.cliente;
            }
            set
            {
                ModelAnillo.cliente = value;
                NotifyChange("cliente");
            }
        }

        /// <summary>
        /// Empaquetado que representa las condiciones de empaque para inspecci�n final.
        /// </summary>
        public Empaquetado CondicionesDeEmpaque
        {
            get
            {
                return ModelAnillo.CondicionesDeEmpaque;
            }
            set
            {
                ModelAnillo.CondicionesDeEmpaque = value;
                NotifyChange("CondicionesDeEmpaque");
            }
        }

        /// <summary>
        /// Revisi�n del plano.
        /// </summary>
        public Revision NivelRevicion
        {
            get
            {
                return ModelAnillo.NivelRevicion;
            }
            set
            {
                ModelAnillo.NivelRevicion = value;
                NotifyChange("NivelRevicion");
            }
        }

        /// <summary>
        /// Colecci�n de tipo propiedad la cual contiene las propiedades que son adquiridas por el anillo durante su proceso.
        /// </summary>
        public ObservableCollection<Propiedad> PropiedadesAdquiridasProceso
        {
            get
            {
                return ModelAnillo.PropiedadesAdquiridasProceso;
            }
            set
            {
                ModelAnillo.PropiedadesAdquiridasProceso = value;
                NotifyChange("PropiedadesAdquiridasProceso");
            }
        }

        /// <summary>
        /// Colecci�n de tipo PropiedadBool la cual contiene las propiedades que son adquiridas por el anillo durante su proceso.
        /// </summary>
        public ObservableCollection<PropiedadBool> PropiedadesBoolAdquiridasProceso
        {
            get
            {
                return ModelAnillo.PropiedadesBoolAdquiridasProceso;
            }
            set
            {
                ModelAnillo.PropiedadesBoolAdquiridasProceso = value;
                NotifyChange("PropiedadesBoolAdquiridasProceso");
            }
        }

        /// <summary>
        /// Colecci�n de tipo PropiedadCadena la cual contiene las propiedades que son adquiridas por el anillo durante su proceso.
        /// </summary>
        public ObservableCollection<PropiedadCadena> PropiedadesCadenaAdquiridasProceso
        {
            get
            {
                return ModelAnillo.PropiedadesCadenaAdquiridasProceso;
            }
            set
            {
                ModelAnillo.PropiedadesCadenaAdquiridasProceso = value;
                NotifyChange("PropiedadesCadenaAdquiridasProceso");
            }
        }

        /// <summary>
        /// Colecci�n de tipo IOPeracion la cual contiene todas las operaciones que se necesitan para procesar el anillo.
        /// </summary>
        public ObservableCollection<IOperacion> Operaciones
        {
            get
            {
                return ModelAnillo.Operaciones;
            }
            set
            {
                ModelAnillo.Operaciones = value;
                NotifyChange("Operaciones");
            }
        }

        /// <summary>
        /// Colecci�n de tipo PinturaAnillo la cual contiene todas las franjas de pintura que tiene el anillo.
        /// </summary>
        public ObservableCollection<PinturaAnillo> FranjasPintura
        {
            get
            {
                return ModelAnillo.FranjasPintura;
            }
            set
            {
                ModelAnillo.FranjasPintura = value;
                NotifyChange("FranjasPintura");
            }
        } //Falta agregar la tabla para ir guardando los datos	
        #endregion

        private bool _IsRedondo;
        public bool IsRedondo
        {
            get
            {
                return _IsRedondo;
            }
            set
            {
                _IsRedondo = value;
                NotifyChange("IsRedondo");
            }
        }

        private bool _ReadOnlyFactorK;
        public bool ReadOnlyFactorK
        {
            get { return _ReadOnlyFactorK; }
            set { _ReadOnlyFactorK = value; NotifyChange("ReadOnlyFactorK"); }
        }

        private bool _ReadOnlyCamLever;
        public bool ReadOnlyCamLever
        {
            get { return _ReadOnlyCamLever; }
            set { _ReadOnlyCamLever = value; NotifyChange("ReadOnlyCamLever"); }
        }

        private ObservableCollection<Pattern> listaPattern;
        public ObservableCollection<Pattern> ListaPattern
        {
            get { return listaPattern; }
            set { listaPattern = value; NotifyChange("ListaPattern"); }
        }

        private ObservableCollection<Cliente> customersList;
        public ObservableCollection<Cliente> CustomersList
        {
            get { return customersList; }
            set { customersList = value; NotifyChange("CustomersList"); }
        }

        private ObservableCollection<FO_Item> _TipoMPList;
        public ObservableCollection<FO_Item> TipoMPList
        {
            get { return _TipoMPList; }
            set { _TipoMPList = value; NotifyChange("TipoMPList"); }
        }


        private FO_Item tipoMateriaPrimaList;
        public FO_Item TipoMateriaPrimaList
        {
            get { return tipoMateriaPrimaList; }
            set { tipoMateriaPrimaList = value; NotifyChange("TipoMateriaPrimaList"); }
        }


        private Pattern selectedPattern;
        public Pattern SelectedPattern
        {
            get { return selectedPattern; }
            set { selectedPattern = value; NotifyChange("SelectedPattern"); }
        }

        #endregion

        #region INotifyPropertyChanged M�todos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor que se utiliza para cuando no se encuentra placa modelo para un anillo.
        /// </summary>
        /// <param name="modelPattern"></param>
        /// <param name="nombreUsuario"></param>
        public PatternViewModel(Pattern modelPattern, string nombreUsuario)
        {
            NombreUsuario = nombreUsuario;

            //Inicializamos el objeto anillo que representa nuestro modelo.
            ModelAnillo = new Anillo();

            //Inicializamos el objeto anillo que representa nuestro modelo.
            Inicializar();

            model = modelPattern;
        }

        /// <summary>
        /// Constructor por default.
        /// </summary>
        public PatternViewModel()
        {
            //Inicializamos el objeto anillo que representa nuestro modelo.
            ModelAnillo = new Anillo();

            ListaPattern = DataManager.GetAllPattern();

            CustomersList = DataManager.GetAllClientes();

            TipoMPList = DataManager.GetAllTipoMateriaPrima();

            Inicializar();

        }
        #endregion

        #region Commands

        public ICommand AltaPattern
        {
            get
            {
                return new RelayCommand(o => altaPattern());
            }
        }

        public ICommand SelecccionarPlaca
        {
            get
            {
                return new RelayCommand(o => seleccionarPlaca());
            }
        }

        public ICommand AbrirPlaca
        {
            get
            {
                return new RelayCommand(o => abrirPlaca());
            }
        }

        /// <summary>
        /// Comando que responde a la petici�n de guardar una placa modelo.
        /// </summary>
        public ICommand GuardarPattern {
            get {
                return new RelayCommand(o => guardarPattern());
            }
        }

        public ICommand EliminarPattern
        {
            get
            {
                return new RelayCommand(o => eliminarPattern());
            }
        }

        /// <summary>
        /// Comando que responde a la petici�n de calcula la hoja de ruta.
        /// </summary>
        public ICommand Calcular
        {
            get{
                return new RelayCommand(o => calcularPlaca());
            }
        }

        /// <summary>
        /// Comando que responde a la petici�n de ver la hoja de ruta.
        /// </summary>
        public ICommand ViewRouting
        {
            get
            {
                return new RelayCommand( o => viewRouting());
            }
        }

        public ICommand ViewRoute
        {
            get
            {
                return new RelayCommand(o => viewRoute());
            }
        }

        #endregion

        #region Methods

        private void altaPattern()
        {
            Inicializar();
            Codigo = DataManager.GetNextCodePattern(DataManager.GetLastCodePattern());
        }

        private void abrirPlaca()
        {
            WPattern pattern = new WPattern();

            pattern.DataContext = this;

            pattern.Show();
        }

        private void seleccionarPlaca()
        {
            if (SelectedPattern == null)
            {
                SelectedPattern = new Pattern();
            }
            Codigo = SelectedPattern.Codigo;
            medida = SelectedPattern.medida;
            diametro = SelectedPattern.diametro;
            detalle = SelectedPattern.detalle;
            mounting = SelectedPattern.mounting;
            plato = SelectedPattern.plato;
            on_14_rd_gate = SelectedPattern.on_14_rd_gate;
            button = SelectedPattern.button;
            M_Circle = SelectedPattern.M_Circle;
            cone = SelectedPattern.cone;
            ring_th_min = SelectedPattern.ring_th_min;
            ring_th_max = SelectedPattern.ring_th_max;
            ring_w_min = SelectedPattern.ring_w_min;
            ring_w_max = SelectedPattern.ring_w_max;
            date_ordered = SelectedPattern.date_ordered;
            mounted = SelectedPattern.mounted;
            ordered = SelectedPattern.ordered;
            Checked = SelectedPattern.Checked;
            factor_k = SelectedPattern.factor_k;
            OD = SelectedPattern.OD;
            ID = SelectedPattern.ID;
            diff = SelectedPattern.diff;
            B_Dia = SelectedPattern.B_Dia;
            fin_Dia = SelectedPattern.fin_Dia;
            turn_allow = SelectedPattern.turn_allow;
            cstg_sm_od = SelectedPattern.cstg_sm_od;
            shrink_allow = SelectedPattern.shrink_allow;
            patt_sm_od = SelectedPattern.patt_sm_od;
            piece_in_patt = SelectedPattern.piece_in_patt;
            bore_allow = SelectedPattern.bore_allow;
            patt_thickness = SelectedPattern.patt_thickness;
            patt_sm_id = SelectedPattern.patt_sm_id;
            joint = SelectedPattern.joint;
            nick = SelectedPattern.nick;
            nick_draf = SelectedPattern.nick_draf;
            nick_depth = SelectedPattern.nick_depth;
            side_relief = SelectedPattern.side_relief;
            cam = SelectedPattern.cam;
            cam_roll = SelectedPattern.cam_roll;
            rise_built = SelectedPattern.rise_built;
            cam_lever = SelectedPattern.cam_lever;
            patt_width = SelectedPattern.patt_width;
            peso_cstg = SelectedPattern.peso_cstg;
            TipoAnillo = SelectedPattern.TipoAnillo;
            diseno = SelectedPattern.diseno;
            esp_inst = SelectedPattern.esp_inst;
            date_checked = SelectedPattern.date_checked;
            rise = SelectedPattern.rise;

            TipoMateriaPrima.id = SelectedPattern.TipoMateriaPrima.id;
            TipoMateriaPrima.ValorCadena = SelectedPattern.TipoMateriaPrima.ValorCadena;
            TipoMateriaPrima = TipoMateriaPrima;

            customer = SelectedPattern.customer;
            customer = customer;

        }

        private void viewRoute()
        {
            GenerarPDF.Traveler(ModelAnillo);
        }

        /// <summary>
        /// M�todo que despliqega una pantalla con la informaci�n de la Ruta.
        /// </summary>
        private void viewRouting()
        {
            //Declaramos un objeto el cual es la pantalla.
            WRouting wRouting = new WRouting();

            //Declaramos el VW de la pantalla, establaciendo el objeto ModelAnillo como su base.
            RoutingViewModel routingViewModel = new RoutingViewModel(this.ModelAnillo);
            
            //Establecemos el DataContext.
            wRouting.DataContext = routingViewModel;

            //Desplegamos la pantalla-
            wRouting.ShowDialog();
        }

        /// <summary>
        /// M�todo que inicializa todas las propiedades de la clase.
        /// </summary>
        private void Inicializar()
        {
            
            model = new Pattern();
            Codigo = string.Empty;
            medida = new Propiedad();
            medida.Valor = 0;
            diametro = new Propiedad();
            diametro.Valor = 0;
            detalle = new PropiedadCadena();
            detalle.Valor = "";
            customer = new Cliente();
            customer.NombreCliente = "";
            mounting = new Propiedad { DescripcionCorta = "Mouting", DescripcionLarga = "Mouting", Imagen = null, Nombre = "MoutingCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Mass), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadMass.Gram) };
            plato = new Propiedad();
            plato.Valor = 0;
            on_14_rd_gate = new PropiedadCadena();
            on_14_rd_gate.Valor = "";
            button = new PropiedadCadena();
            button.Valor = "";
            M_Circle = new PropiedadCadena();
            M_Circle.Valor = "";
            cone = new PropiedadCadena();
            cone.Valor = "";
            ring_th_min = new Propiedad();
            ring_th_min.Valor = 0;
            ring_th_max = new Propiedad();
            ring_th_max.Valor = 0;
            ring_w_min = new Propiedad();
            ring_w_min.Valor = 0;
            ring_w_max = new Propiedad();
            ring_w_max.Valor = 0;
            date_ordered = new PropiedadCadena();
            date_ordered.Valor = "";
            mounted = new PropiedadCadena();
            mounted.Valor = "";
            ordered = new PropiedadCadena();
            ordered.Valor = "";
            Checked = new PropiedadCadena();
            Checked.Valor = "";
            factor_k = new Propiedad();
            factor_k.Valor = 0;
            OD = new Propiedad();
            OD.Valor = 0;
            ID = new Propiedad();
            ID.Valor = 0;
            diff = new Propiedad();
            diff.Valor = 0;
            B_Dia = new Propiedad();
            B_Dia.Valor = 0;
            fin_Dia = new Propiedad();
            fin_Dia.Valor = 0;
            turn_allow = new Propiedad();
            turn_allow.Valor = 0;
            cstg_sm_od = new Propiedad();
            cstg_sm_od.Valor = 0;
            shrink_allow = new Propiedad();
            shrink_allow.Valor = 0;
            patt_sm_od = new Propiedad();
            patt_sm_od.Valor = 0;
            piece_in_patt = new Propiedad();
            piece_in_patt.Valor = 0;
            bore_allow = new Propiedad();
            bore_allow.Valor = 0;
            patt_thickness = new Propiedad();
            patt_thickness.Valor = 0;
            patt_sm_id = new Propiedad();
            patt_sm_id.Valor = 0;
            joint = new PropiedadCadena();
            joint.Valor = "";
            nick = new PropiedadCadena();
            nick.Valor = "";
            nick_draf = new PropiedadCadena();
            nick_draf.Valor = "";
            nick_depth = new PropiedadCadena();
            nick_depth.Valor = "";
            side_relief = new PropiedadCadena();
            side_relief.Valor = "";
            cam = new Propiedad();
            cam.Valor = 0;
            cam_roll = new Propiedad();
            cam_roll.Valor = 0;
            rise_built = new Propiedad();
            rise_built.Valor = 0;
            cam_lever = new Propiedad();
            cam_lever.Valor = 0;
            patt_width = new Propiedad();
            patt_width.Valor = 0;
            peso_cstg = new Propiedad { DescripcionCorta = "Peso casting", DescripcionLarga = "Peso del casting", Imagen = null, Nombre = "PesoCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Mass), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadMass.Gram), Valor = 0 };
            TipoAnillo = new PropiedadCadena();
            TipoAnillo.Valor = "";
            diseno = new PropiedadBool { Valor = true };
            TipoMateriaPrima = new FO_Item();
            rise = new Propiedad();
            rise.Valor = 0;

            esp_inst = new PropiedadCadena();
            esp_inst.Valor = string.Empty;

            date_checked = new PropiedadCadena();
            date_checked.Valor = DateTime.Now.ToShortDateString();

            date_ordered = new PropiedadCadena();
            date_ordered.Valor = DateTime.Now.ToShortDateString();

            date_ordered = date_ordered;
            date_checked = date_checked;
        }

        /// <summary>
        /// M�todo para asignar nombre a las propiedades.
        /// </summary>
        private void setNameProperties()
        {
            mounting = new Propiedad { DescripcionCorta = "Mouting", DescripcionLarga = "Mouting", Imagen = null, Nombre = "MoutingCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Mass), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadMass.Gram) };
            definirPlato();
            peso_cstg = new Propiedad { DescripcionCorta = "Peso casting", DescripcionLarga = "Peso del casting", Imagen = null, Nombre = "PesoCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Mass), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadMass.Gram), Valor = 0 };
            patt_thickness = new Propiedad { DescripcionCorta = "Patt thickness", DescripcionLarga = "Patt thickness", Imagen = null, Nombre = "PattThicknessCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), Valor = 0 };
            B_Dia = new Propiedad { DescripcionCorta = "B Dia", DescripcionLarga = "B Dia", Imagen = null, Nombre = "BDiaCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), Valor = 0};
            patt_sm_od = new Propiedad { DescripcionCorta = "Patt sm od", DescripcionLarga = "Patt sm od", Imagen = null, Nombre = "PattSMOD", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch) };

            setNamePropertieWidthCasting();
            
        }

        /// <summary>
        /// M�todo que inicializa la propiedad WidthCasting
        /// </summary>
        private void setNamePropertieWidthCasting()
        {
            diametro.DescripcionCorta = "Width";
            diametro.DescripcionLarga = "Width Casting";
            diametro.Imagen = null;
            diametro.Nombre = "WidthCasting";
            diametro.TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance);
            diametro.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);
            
        }

        /// <summary>
        /// M�todo que calcula los valores de la placa modelo.
        /// </summary>
        private void calcularPlaca()
        {
            //Ejecutamos el m�todo para asignar el nombre a las propiedades. Esto para poder ser usadas en otras clases como las de los tiempos estandar
            setNameProperties();

            customer = new Cliente { IdCliente = 1, NombreCliente = "SERVICIO" };
            
            //Constante
            joint.Valor = "BUTT";
            nick.Valor = "RAD.";
            nick_draf.Valor = "4�";
            nick_depth.Valor = ".045 - .050";
            side_relief.Valor = "-----";
            cam.Valor = 8;
            cam_roll.Valor = 1.622;
            cam_lever.Valor = 0.074;
            rise_built.Valor = 0.005;

            IsPatternNew = false;

            if (turn_allow.Valor == 0 && turn_allow.Valor == 0)
            {
                IsPatternNew = true;
            }

            if (IsPatternNew)
            {
                if (diseno.Valor)
                {
                    double[] resultsTurnBore = DataManager.Get_TurnBoreAllow(TipoAnillo.Valor, EspecMaterialAnillo.Valor);
                    turn_allow.Valor = resultsTurnBore[0];
                    bore_allow.Valor = resultsTurnBore[1];
                }
                else
                {
                    turn_allow.Valor = 0.1;
                    bore_allow.Valor = 0.105;
                }
            }

            if (IsPatternNew)
            {
                if (TipoMateriaPrima.ValorCadena.Equals("GASOLINA") || TipoMateriaPrima.ValorCadena.Equals("SPR-212"))
                    patt_width.Valor = DataManager.GetIdealCastingWidth(medida.Valor, Proceso.Valor);
                else
                    patt_width.Valor = ring_w_max.Valor * 1 + 0.021;
            }
            else
            {
                patt_width = medida;
            }
            
            medida = patt_width;

            setNamePropertieWidthCasting();

            if (!factor_k.Valor.Equals(0))
            {
                if (IsPatternNew)
                {
                    Codigo = DataManager.GetNextCodePattern(DataManager.GetLastCodePattern());
                }

                //Comparamos si el tipo de material es gasolina.
                if (TipoMateriaPrima.ValorCadena.Equals("GASOLINA"))
                {
                    //Comparamos si el dise�o es normal.
                    if (diseno.Valor)
                    {
                        cam_lever.Valor = Math.Round((piece_in_patt.Valor * factor_k.Valor * 64) + 0.005, 3);
                        ReadOnlyFactorK = false;
                        ReadOnlyCamLever = true;
                    }
                    else //Si el dise�o es redondo
                    {
                        ReadOnlyFactorK = false;
                        ReadOnlyCamLever = false;
                        cam_lever.Valor = Math.Round((piece_in_patt.Valor * factor_k.Valor * 64) - 0.005, 3);
                    }
                    fin_Dia.Valor = Math.Round((((((cam_lever.Valor - 0.005) * 0.478) - piece_in_patt.Valor) / -3.1416) + medida.Valor) - (cam_lever.Valor - 0.005), 3);

                }
                else if(TipoMateriaPrima.ValorCadena.Equals("SPR-212")) //Si el tipo de material es 
                {
                    cam_lever.Valor = Math.Round((piece_in_patt.Valor * factor_k.Valor * 64) + 0.015, 3);
                    fin_Dia.Valor = Math.Round((((((cam_lever.Valor - 0.015) * 0.478) - piece_in_patt.Valor) / -3.1416) + medida.Valor) - (cam_lever.Valor - 0.015), 3);
                }
                else if(TipoMateriaPrima.ValorCadena.Equals("SUPER DUTY"))
                {
                    cam_lever.Valor = Math.Round((piece_in_patt.Valor * factor_k.Valor * 64), 3);
                    fin_Dia.Valor = Math.Round((((((cam_lever.Valor - 0.005) * 0.478) - piece_in_patt.Valor) / -3.1416) + medida.Valor) - (cam_lever.Valor - 0.005), 3);
                }
                else
                {
                    //NOTIFICAR QUE ESTE MATERIAL NO EXISTE!!!
                    return;
                }

                cstg_sm_od.Valor = Math.Round(fin_Dia.Valor + (1 * turn_allow.Valor), 3);

                if (TipoMateriaPrima.ValorCadena.Equals("GASOLINA"))
                {
                    shrink_allow.Valor = Math.Round(cstg_sm_od.Valor * 0.0104, 3);
                }
                else if (TipoMateriaPrima.ValorCadena.Equals("SPR-212"))
                {
                    shrink_allow.Valor = 0.02;
                }
                else if(TipoMateriaPrima.ValorCadena.Equals("SUPER DUTY"))
                {
                    shrink_allow.Valor = Math.Round(cstg_sm_od.Valor * 0.0094, 3);
                }

                patt_sm_od.Valor = Math.Round(cstg_sm_od.Valor + (1 * shrink_allow.Valor), 3);
                patt_thickness.Valor = Math.Round((turn_allow.Valor + (1 * bore_allow.Valor)) / 2 + (ring_th_min.Valor + (1 * ring_th_max.Valor)) / 2, 3);
                patt_sm_id.Valor = Math.Round(patt_sm_od.Valor - (patt_thickness.Valor * 2), 3);
                OD.Valor = Math.Round(cstg_sm_od.Valor + ((cam_lever.Valor - rise_built.Valor) * 2), 3);
                ID.Valor = Math.Round(patt_sm_id.Valor - (patt_sm_id.Valor * 0.015), 3);
                diff.Valor = Math.Round(OD.Valor - ID.Valor, 3);
                peso_cstg.Valor = Math.Round((((3.1416 / 4) * (Convert.ToDouble(Math.Pow(patt_sm_od.Valor , 2)) - Convert.ToDouble(Math.Pow(patt_sm_id.Valor,2)))) * diametro.Valor * 16.387 * 7.2) / 0.95, 3);
                B_Dia.Valor = Math.Round(patt_sm_od.Valor + (2 * cam_lever.Valor), 4);

                if (IsPatternNew)
                {
                    definirPlato();
                }
                
                if (calculoOk)
                {
                    detalle.Valor = DataManager.GetDetalleMoutingWidth(diametro.Valor);
                    string[] valoresMoutingDia = DataManager.GetMoutingDia(B_Dia.Valor, plato.Valor);
                    if (valoresMoutingDia.Length.Equals(5) )
                    {
                        mounting.Valor = Convert.ToDouble(valoresMoutingDia[0]);
                        on_14_rd_gate.Valor = valoresMoutingDia[1];
                        M_Circle.Valor = valoresMoutingDia[2].Equals("Aplica") ? Convert.ToString(Math.Round((patt_sm_id.Valor - .06) / 2, 3)) : M_Circle.Valor = "No Aplica";
                        button.Valor = valoresMoutingDia[3];
                        cone.Valor = valoresMoutingDia[4];

                        estado = new PropiedadBool { DescripcionCorta = "Estado", DescripcionLarga = "Estado", Imagen = null, Nombre = "Estado", Valor = true };
                    }else
                    {
                        //Notificar que no se encontraron valores de la tabla MoutingDia.
                    }

                }else
                {
                    //Notificar al usuario que no se completo el calculo.
                }
                
            }

            actualizarValores();

            ModelAnillo.PropiedadesAdquiridasProceso.Add(peso_cstg);
            ModelAnillo.PropiedadesAdquiridasProceso.Add(mounting);
            ModelAnillo.PropiedadesAdquiridasProceso.Add(patt_thickness);
            ModelAnillo.PropiedadesAdquiridasProceso.Add(diametro);
            ModelAnillo.PropiedadesAdquiridasProceso.Add(B_Dia);
            ModelAnillo.PropiedadesAdquiridasProceso.Add(patt_sm_od);
            ModelAnillo.PropiedadesBoolAdquiridasProceso = new ObservableCollection<PropiedadBool>();
            ModelAnillo.PropiedadesCadenaAdquiridasProceso = new ObservableCollection<PropiedadCadena>();
            
            calcularOperaciones();
        }

        /// <summary>
        /// M�todo que calculo la ruta de la placa modelo.
        /// </summary>
        private async void calcularOperaciones()
        {
            Operaciones.Clear();

            Caratula = "";

            ModelAnillo.DescripcionGeneral = String.Format("{0:0.00000}", diametro.Valor) + " X " + String.Format("{0:0.00000}", medida.Valor);

            double r_min, r_max, t_min, t_max;
            r_min = (medida.Valor + .010) - .005;
            r_max = (medida.Valor + .010) + .005;
            t_min = (patt_thickness.Valor + .015) - .010;
            t_max = (patt_thickness.Valor + .015) + .010;

            Caratula = DescripcionGeneral + Environment.NewLine;
            Caratula += "DATE REVIEW " + String.Format("{0:dd/MM/yyyy}",DateTime.Now) + Environment.NewLine;
            Caratula += "WIDTH      " + String.Format("{0:0.000}", r_min) + " - " + String.Format("{0:0.000}", r_max) + Environment.NewLine;
            Caratula += "THICKNESS  " + String.Format("{0:0.000}", t_min) + " - " + String.Format("{0:0.000}", t_max) + Environment.NewLine;

            //Calculo de la operaci�n fundici�n moldeo.
            double a, b, c, sec;
            a = patt_thickness.Valor * 2.54;
            b = medida.Valor * 0.254;
            c = a * b;
            sec = Math.Round(c * 1000, 2);
            if (sec < 25)
                Caratula += "CLASIFICACION FINO" + Environment.NewLine;
            else if(sec >=25 && sec <= 39.9)
                Caratula += "CLASIFICACION MEDIANO" + Environment.NewLine;
            else
                Caratula += "CLASIFICACION GRUESO" + Environment.NewLine;


            if (!IsPatternNew)
            {
                //HardCode
                EspecMaterialAnillo = new PropiedadCadena { DescripcionCorta = "Especa", DescripcionLarga = "", Imagen = null, Nombre = "EspecMateriaPrima", Valor = "SPR-128" };
            }


            string[] vecHardness = DataManager.GetHardnessIdeal(EspecMaterialAnillo.Valor, diametro.Valor);
            
            Hardness = new PropiedadCadena();
            HardnessMax = new Propiedad();
            HardnessMin = new Propiedad();

            Caratula += "IMPRESIONES " + mounting.Valor + Environment.NewLine;
            Caratula += "MATERIAL   " + EspecMaterialAnillo.Valor + Environment.NewLine;
            Caratula += "HARDNESS   " + Hardness.Valor + "    " + HardnessMin.Valor + "-" + HardnessMax.Valor + Environment.NewLine;
            Caratula += "PESO CAST " + peso_cstg.Valor + " G" + Environment.NewLine;
            Caratula += "" + Environment.NewLine;
            Caratula += "MODELO    " + Environment.NewLine;
            Caratula += Codigo + Environment.NewLine;
            Caratula += DescripcionGeneral + Environment.NewLine;
            Caratula += "" + Environment.NewLine;
            Caratula += "CUSTOMER   "  + customer.NombreCliente + Environment.NewLine;
            Caratula += "CUST. PT." + Environment.NewLine;
            Caratula += "CHK BY     ." + Environment.NewLine;
            Caratula += "REVISADO   " + NombreUsuario + Environment.NewLine;
            Caratula += String.Format("{0:dd/MM/yyyy}", DateTime.Now) + Environment.NewLine;
            Caratula += "" + Environment.NewLine;
            Caratula += "NOTAS" + Environment.NewLine;

            Anillo anilloProcesado = new Anillo();

            ModelAnillo.MaterialBase.Especificacion = EspecMaterialAnillo;

            Fusion opeFusion = new Fusion(ModelAnillo);
            Operaciones.Add(opeFusion);

            FundicionMoldeo opeMoldeo = new FundicionMoldeo(ModelAnillo);
            Operaciones.Add(opeMoldeo);

            SacudidoPulido opeSacudido = new SacudidoPulido(ModelAnillo);
            Operaciones.Add(opeSacudido);

            FundicionEsmeriladoIntExtGas opeFundicionEsmerilado = new FundicionEsmeriladoIntExtGas(ModelAnillo);
            Operaciones.Add(opeFundicionEsmerilado);

            InspeccionCasting opeInspeccion = new InspeccionCasting(ModelAnillo);
            Operaciones.Add(opeInspeccion);

            bool ban = true;
            Anillo aProcesado = new Anillo();

            DialogService dialogService = new DialogService();
            var Controller = await dialogService.SendProgressAsync(Resources.StringResources.ttlEspereUnMomento, string.Empty);

            int totalOperaciones = Operaciones.Count;
            int i = 0;

            foreach (IOperacion element in Operaciones)
            {
                Controller.SetMessage(Resources.StringResources.msgDoingOperation + element.NombreOperacion);
                if (ban)
                {
                    element.CrearOperacion(anilloProcesado, ModelAnillo);
                    aProcesado = element.anilloProcesado;
                    ban = false;
                }
                else
                {
                    element.CrearOperacion(aProcesado, ModelAnillo);
                    aProcesado = element.anilloProcesado;
                }

                await Task.Delay(3000);
                
                i += 1;
            }
            
            await Controller.CloseAsync();
            await dialogService.SendMessage(Resources.StringResources.ttlDone, Resources.StringResources.msgRoutingReady);
            
        }

        /// <summary>
        /// M�todo que despliega una pantalla en la cual el usuario puede elegir la medida de la placa modelo.
        /// </summary>
        private void definirPlato()
        {
            List<double> ListaPlato = DataManager.GetPlatoMoutingDia(B_Dia.Valor);

            ListItemViewModel context = new ListItemViewModel(ListaPlato);
            WOptionList fmr = new WOptionList();
            fmr.DataContext = context;
            if (fmr.ShowDialog().Equals(true))
            {
                plato.Valor = Convert.ToDouble(context.SelectedItem);
                calculoOk = true;
            }
        }

        /// <summary>
        /// M�todo el cual actualiza los valores. (Esto para que se vean reflejados los valores en pantalla.
        /// </summary>
        private void actualizarValores()
        {
            turn_allow = turn_allow;
            bore_allow = bore_allow;
            joint = joint;
            nick = nick;
            nick_draf = nick_draf;
            nick_depth = nick_depth;
            side_relief = side_relief;
            cam = cam;
            cam_roll = cam_roll;
            cam_lever = cam_lever;
            rise_built = rise_built;
            diseno = diseno;
            patt_width = patt_width;
            Codigo = Codigo;
            fin_Dia = fin_Dia;
            cstg_sm_od = cstg_sm_od;
            shrink_allow = shrink_allow;
            patt_sm_od = patt_sm_od;
            patt_thickness = patt_thickness;
            patt_sm_id = patt_sm_id;
            OD = OD;
            ID = ID;
            diff = diff;
            peso_cstg = peso_cstg;
            B_Dia = B_Dia;
            plato = plato;
            detalle = detalle;
            mounting = mounting;
            on_14_rd_gate = on_14_rd_gate;
            M_Circle = M_Circle;
            button = button;
            cone = cone;
            mounted = mounted;
            ordered = ordered;
            Checked = Checked;
            estado = estado;
            customer = customer;
            TipoMateriaPrima = TipoMateriaPrima;
            rise = rise;
        }
        
        /// <summary>
        /// M�todo que guarda una placa modelo.
        /// </summary>
        private async void guardarPattern()
        {
            DialogService dialog = new DialogService();
            
            if (validar())
            {
                //Si no se tiene el id del cliente, buscamos su id.
                if (model.customer.IdCliente == 0)
                    model.customer.IdCliente = DataManager.GetIDCliente(model.customer.NombreCliente);
                
                string codigoRegistrado = DataManager.SetPattern(model);
                if (!codigoRegistrado.Equals(""))
                {
                    await dialog.SendMessage(Resources.StringResources.ttlDone, Resources.StringResources.msgPatternInserted + codigoRegistrado);

                    //Inicializamos el objeto anillo que representa nuestro modelo.
                    Inicializar();
                    ListaPattern = DataManager.GetAllPattern();

                }
                else
                    await dialog.SendMessage(Resources.StringResources.ttlAlerta, Resources.StringResources.msgError);
            } 
            else
                await dialog.SendMessage(Resources.StringResources.ttlAlerta, Resources.StringResources.msgFillFlields);
        }

        /// <summary>
        /// M�todo que valida si los campos son correctos.
        /// </summary>
        /// <returns></returns>
        private bool validar()
        {
            if (mounted.Valor.Equals(string.Empty))
                return false;
            else if (Checked.Valor.Equals(string.Empty))
                return false;
            else if (ordered.Valor.Equals(string.Empty))
                return false;
            else if (string.IsNullOrEmpty(Codigo))
                return false;
            else if (medida.Valor == 0)
                return false;
            else if (diametro.Valor == 0)
                return false;
            else if (customer.IdCliente == 0)
                return false;
            else if (mounting.Valor == 0)
                return false;
            else if (string.IsNullOrEmpty(on_14_rd_gate.Valor))
                return false;
            else if (string.IsNullOrEmpty(button.Valor))
                return false;
            else if (string.IsNullOrEmpty(cone.Valor))
                return false;
            else if (string.IsNullOrEmpty(M_Circle.Valor))
                return false;
            else if (ring_w_min.Valor == 0)
                return false;
            else if (ring_w_max.Valor == 0)
                return false;
            else if (B_Dia.Valor == 0)
                return false;
            else if (fin_Dia.Valor == 0)
                return false;
            else if (turn_allow.Valor == 0)
                return false;
            else if (cstg_sm_od.Valor == 0)
                return false;
            else if (shrink_allow.Valor == 0)
                return false;
            else if (patt_sm_od.Valor == 0)
                return false;
            else if (piece_in_patt.Valor == 0)
                return false;
            else if (bore_allow.Valor == 0)
                return false;
            else if (patt_sm_id.Valor == 0)
                return false;
            else if (patt_thickness.Valor == 0)
                return false;
            else if (string.IsNullOrEmpty(joint.Valor))
                return false;
            else if (string.IsNullOrEmpty(nick.Valor))
                return false;
            else if (string.IsNullOrEmpty(nick_draf.Valor))
                return false;
            else if (string.IsNullOrEmpty(nick_depth.Valor))
                return false;
            else if (string.IsNullOrEmpty(side_relief.Valor))
                return false;
            else if (cam.Valor == 0)
                return false;
            else if (cam_roll.Valor == 0)
                return false;
            else if (rise.Valor == 0)
                return false;
            else if (OD.Valor == 0)
                return false;
            else if (ID.Valor == 0)
                return false;
            else if (diff.Valor == 0)
                return false;
            else if (TipoMateriaPrima.id == 0)
                return false;
            else if (factor_k.Valor == 0)
                return false;
            else if (rise_built.Valor == 0)
                return false;
            else if (ring_th_min.Valor == 0)
                return false;
            else if (ring_th_max.Valor == 0)
                return false;
            else if (plato.Valor == 0)
                return false;
            else if (string.IsNullOrEmpty(detalle.Valor))
                return false;
            else if (string.IsNullOrEmpty(date_checked.Valor))
                return false;

            return true;
        }

        private async void eliminarPattern()
        {
            DialogService dialog = new DialogService();
            
            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = Resources.StringResources.lblYes;
            setting.NegativeButtonText = Resources.StringResources.lblNo;

            MessageDialogResult result = await dialog.SendMessage(Resources.StringResources.ttlAlerta, Resources.StringResources.lblConfirmDeleteRecord, setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                int r = DataManager.DeletePattern(model);

                if (r > 0)
                {
                    await dialog.SendMessage(Resources.StringResources.ttlDone, Resources.StringResources.lblRecordDeleted);

                    //Inicializamos el objeto anillo que representa nuestro modelo.
                    ModelAnillo = new Anillo();

                    ListaPattern = DataManager.GetAllPattern();

                    CustomersList = DataManager.GetAllClientes();

                    TipoMPList = DataManager.GetAllTipoMateriaPrima();

                    Inicializar();

                }
                else
                {
                    await dialog.SendMessage(Resources.StringResources.ttlAlerta, Resources.StringResources.msgError);
                }
            }
        }
        #endregion
    }
}