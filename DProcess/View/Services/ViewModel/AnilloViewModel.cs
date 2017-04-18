using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Model.Interfaces;
using System.Windows.Input;
using View.Forms.Modals;

namespace View.Services.ViewModel
{
    public class AnilloViewModel : INotifyPropertyChanged
    {

        #region Attributes
        private Anillo ModelAnillo;
        #endregion

        #region Propiedades del Modelo Anillo
        /// <summary>
        /// Perfil que representa el diámetro exterior del anillo.
        /// </summary>
        public Perfil PerfilOD {
            get {
                return ModelAnillo.PerfilOD;
            }
            set {
                ModelAnillo.PerfilOD = value;
                NotifyChange("PerfilOD");
            }
        }

        /// <summary>
        /// Perfil que representa el diámetro interior del anillo.
        /// </summary>
        public Perfil PerfilID {
            get {
                return ModelAnillo.PerfilID;
            }
            set {
                ModelAnillo.PerfilID = value;
                NotifyChange("PerfilID");
            }
        }

        /// <summary>
        /// Perfil que representa la cara lateral del anillo.
        /// </summary>
        public Perfil PerfilLateral {
            get {
                return ModelAnillo.PerfilLateral;
            }
            set {
                ModelAnillo.PerfilLateral = value;
                NotifyChange("PerfilLateral");
            }
        }

        /// <summary>
        /// Perfil que representa las puntas del anillo.
        /// </summary>
        public Perfil PerfilPuntas {
            get {
                return ModelAnillo.PerfilPuntas;
            }
            set {
                ModelAnillo.PerfilPuntas = value;
                NotifyChange("PerfilPuntas");
            }
        }

        /// <summary>
        /// Propiedad que representa el diámetro del anillo.
        /// </summary>
        public Propiedad D1 {
            get {
                return ModelAnillo.D1;
            }
            set {
                ModelAnillo.D1 = value;
                NotifyChange("D1");
            }
        }

        /// <summary>
        /// Propiedad que representa el width del anillo.
        /// </summary>
        public Propiedad H1 {
            get {
                return ModelAnillo.H1;
            }
            set {
                ModelAnillo.H1 = value;
                NotifyChange("H1");
            }
        }

        /// <summary>
        /// Propiedad que representa el FreeGap del anillo.
        /// </summary>
        public Propiedad FreeGap {
            get {
                return ModelAnillo.FreeGap;
            }
            set {
                ModelAnillo.FreeGap = value;
                NotifyChange("FreeGap");
            }
        }

        /// <summary>
        /// Propiedad que representa el peso del anillo.
        /// </summary>
        public Propiedad Mass {
            get {
                return ModelAnillo.Mass;
            }
            set {
                ModelAnillo.Mass = value;
                NotifyChange("Mass");
            }
        }

        /// <summary>
        /// Propiedad que representa la tensión del anillo.
        /// </summary>
        public Propiedad Tension {
            get {
                return ModelAnillo.Tension;
            }
            set {
                ModelAnillo.Tension = value;
                NotifyChange("Tension");
            }
        }

        /// <summary>
        /// Propiedad que representa la tolerancia de la tensión del anillo.
        /// </summary>
        public Propiedad TensionTol {
            get {
                return ModelAnillo.TensionTol;
            }
            set {
                ModelAnillo.TensionTol = value;
                NotifyChange("TensionTol");
            }
        }

        /// <summary>
        /// Propiedad que representa la ovalidad mínima del anillo.
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
        /// Propiedad que representa la ovalidad máxima del anillo.
        /// </summary>
        public Propiedad OvalityMax {
            get {
                return ModelAnillo.OvalityMax;
            }
            set {
                ModelAnillo.OvalityMax = value;
                NotifyChange("OvalityMax");
            }
        }

        /// <summary>
        /// Materia prima que representa el material base del anillo.
        /// </summary>
        public MateriaPrima MaterialBase {
            get {
                return ModelAnillo.MaterialBase;
            }
            set {
                ModelAnillo.MaterialBase = value;
                NotifyChange("MaterialBase");
            }
        }

        /// <summary>
        /// Cadena que representa el número de plano del anillo.
        /// </summary>
        public string NoPlano {
            get {
                return ModelAnillo.NoPlano;
            }
            set {
                ModelAnillo.NoPlano = value;
                NotifyChange("NoPlano");
            }
        }

        /// <summary>
        /// Cadena que representa el número de parte del cliente.
        /// </summary>
        public string CustomerPartNumber {
            get {
                return ModelAnillo.CustomerPartNumber;
            }
            set {
                ModelAnillo.CustomerPartNumber = value;
                NotifyChange("CustomerPartNumber");
            }
        }

        /// <summary>
        /// Cadena que representa el nivel de revisión del cliente.
        /// </summary>
        public string CustomerRevisionLevel {
            get {
                return ModelAnillo.CustomerRevisionLevel;
            }
            set {
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
        public string Size {
            get {
                return ModelAnillo.Size;
            }
            set {
                ModelAnillo.Size = value;
                NotifyChange("Size");
            }
        }

        /// <summary>
        /// Cadena que representa el tipo de anillo.
        /// </summary>
        public string TipoAnillo {
            get {
                return ModelAnillo.TipoAnillo;
            }
            set {
                ModelAnillo.TipoAnillo = value;
                NotifyChange("TipoAnillo");
            }
        }

        /// <summary>
        /// Cadena que representa la dureza del anillo.
        /// </summary>
        public string Hardness {
            get {
                return ModelAnillo.Hardness;
            }
            set {
                ModelAnillo.Hardness = value;
                NotifyChange("Hardness");
            }
        }

        /// <summary>
        /// Double que representa la dureza máxima del anillo.
        /// </summary>
        public double HardnessMin {
            get {
                return ModelAnillo.HardnessMin;
            }
            set {
                ModelAnillo.HardnessMin = value;
                NotifyChange("HardnessMin");
            }
        }

        /// <summary>
        /// Double que representa la dureza mínima del anillo.
        /// </summary>
        public double HardnessMax {
            get {
                return ModelAnillo.HardnessMax;
            }
            set {
                ModelAnillo.HardnessMax = value;
                NotifyChange("HardnessMax");
            }
        }

        /// <summary>
        /// Cadena que representa el numero de documento del cliente.
        /// </summary>
        public string CustomerDocNo {
            get {
                return ModelAnillo.CustomerDocNo;
            }
            set {
                ModelAnillo.CustomerDocNo = value;
                NotifyChange("CustomerDocNo");
            }
        }

        /// <summary>
        /// Cadena que representa el tratamiento que tiene el anillo.
        /// </summary>
        public string Treatment {
            get {
                return ModelAnillo.Treatment;
            }
            set {
                ModelAnillo.Treatment = value;
                NotifyChange("Treatment");
            }
        }

        /// <summary>
        /// Cadena que representa la especificación de tratamiento que tiene el anillo.
        /// </summary>
        public string EspecTreatment {
            get {
                return ModelAnillo.EspecTreatment;
            }
            set {
                ModelAnillo.EspecTreatment = value;
                NotifyChange("EspecTreatment");
            }
        }

        /// <summary>
        /// Cadena que representa el texto con la información del anillo general. Esto para sistema ERP.
        /// </summary>
        public string Caratula {
            get {
                return ModelAnillo.Caratula;
            }
            set {
                ModelAnillo.Caratula = value;
                NotifyChange("Caratula");
            }
        }

        /// <summary>
        /// Cliente que representa a cual pertenece el anillo.
        /// </summary>
        public Cliente cliente {
            get {
                return ModelAnillo.cliente;
            }
            set {
                ModelAnillo.cliente = value;
                NotifyChange("cliente");
            }
        }

        /// <summary>
        /// Empaquetado que representa las condiciones de empaque para inspección final.
        /// </summary>
        public Empaquetado CondicionesDeEmpaque {
            get {
                return ModelAnillo.CondicionesDeEmpaque;
            }
            set {
                ModelAnillo.CondicionesDeEmpaque = value;
                NotifyChange("CondicionesDeEmpaque");
            }
        }

        /// <summary>
        /// Revisión del plano.
        /// </summary>
        public Revision NivelRevicion {
            get {
                return ModelAnillo.NivelRevicion;
            }
            set {
                ModelAnillo.NivelRevicion = value;
                NotifyChange("NivelRevicion");
            }
        }

        /// <summary>
        /// Colección de tipo propiedad la cual contiene las propiedades que son adquiridas por el anillo durante su proceso.
        /// </summary>
        public ObservableCollection<Propiedad> PropiedadesAdquiridasProceso {
            get {
                return ModelAnillo.PropiedadesAdquiridasProceso;
            }
            set {
                ModelAnillo.PropiedadesAdquiridasProceso = value;
                NotifyChange("PropiedadesAdquiridasProceso");
            }
        }

        /// <summary>
        /// Colección de tipo PropiedadBool la cual contiene las propiedades que son adquiridas por el anillo durante su proceso.
        /// </summary>
        public ObservableCollection<PropiedadBool> PropiedadesBoolAdquiridasProceso {
            get {
                return ModelAnillo.PropiedadesBoolAdquiridasProceso;
            }
            set {
                ModelAnillo.PropiedadesBoolAdquiridasProceso = value;
                NotifyChange("PropiedadesBoolAdquiridasProceso");
            }
        }

        /// <summary>
        /// Colección de tipo PropiedadCadena la cual contiene las propiedades que son adquiridas por el anillo durante su proceso.
        /// </summary>
        public ObservableCollection<PropiedadCadena> PropiedadesCadenaAdquiridasProceso {
            get {
                return ModelAnillo.PropiedadesCadenaAdquiridasProceso;
            }
            set {
                ModelAnillo.PropiedadesCadenaAdquiridasProceso = value;
                NotifyChange("PropiedadesCadenaAdquiridasProceso");
            }
        }

        /// <summary>
        /// Colección de tipo IOPeracion la cual contiene todas las operaciones que se necesitan para procesar el anillo.
        /// </summary>
        public ObservableCollection<IOperacion> Operaciones {
            get {
                return ModelAnillo.Operaciones;
            }
            set {
                ModelAnillo.Operaciones = value;
                NotifyChange("Operaciones");
            }
        }

        /// <summary>
        /// Colección de tipo PinturaAnillo la cual contiene todas las franjas de pintura que tiene el anillo.
        /// </summary>
        public ObservableCollection<PinturaAnillo> FranjasPintura {
            get {
                return ModelAnillo.FranjasPintura;
            }
            set {
                ModelAnillo.FranjasPintura = value;
                NotifyChange("FranjasPintura");
            }
        } //Falta agregar la tabla para ir guardando los datos	
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

        #region Constructors

        public AnilloViewModel()
        {

            //Inicializamos el objeto anillo que representa nuestro modelo.
            ModelAnillo = new Anillo();

            //Establesemos a todas las propiedades del modelo anillo los valores por default.
            SetUnidesDefault();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Comando que reponde a la acción de consultar/Modificar la propiedad D1.
        /// </summary>
        public ICommand VerUnidadesD1
        {
            get
            {
                return new RelayCommand(o => verUnidadesDistancia(D1));
            }
        }

        public ICommand VerUnidadesH1
        {
            get
            {
                return new RelayCommand(o => verUnidadesDistancia(H1));
            }
        }

        public ICommand VerUnidadesFreeGap
        {
            get
            {
                return new RelayCommand(o => verUnidadesDistancia(FreeGap));
            }
        }

        public ICommand VerUnidadesTensionTol
        {
            get
            {
                return new RelayCommand(o => verUnidadesDistancia(TensionTol));
            }
        }

        public ICommand VerUnidadesTension
        {
            get
            {
                return new RelayCommand(o => verUnidadesDistancia(Tension));
            }
        }

        public ICommand VerUnidadesOvalityMax
        {
            get
            {
                return new RelayCommand(o => verUnidadesDistancia(OvalityMax));
            }
        }

        public ICommand VerUnidadesOvalityMin
        {
            get
            {
                return new RelayCommand(o => verUnidadesDistancia(OvalityMin));
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Método que muestra una ventana con todas las posibles unidades a mostrar.
        /// </summary>
        /// <param name="laPropiedad">Propiedad que representa el modelo de la pantalla que se muestra.</param>
        private void verUnidadesDistancia(Propiedad laPropiedad)
        {
            //Inicializamos el contexto de Propiedad.
            PropiedadViewModel contexto = new PropiedadViewModel(laPropiedad);

            //Declaramos un objeto que representa la pantalla a mostrar.
            frmViewUnidades modal = new frmViewUnidades();

            //Asignamos el contexto a la pantalla.
            modal.DataContext = contexto;

            //Ejecutamos el método para que se muestre la pantalla.
            modal.ShowDialog();
        }

        /// <summary>
        /// Método que inicializa los valores por defualt de todas las propiedades del modelo Anillo.
        /// </summary>
        private void SetUnidesDefault()
        {
            //Declaramos las variables que contienen los valores por default para cada tipo de dato.
            string tipodatodistance = "Distance";
            string unidaddistance = "Inch (in)";
            string tipodatoforce = "Force";
            string unidadforce = "LBS";

            ModelAnillo.D1.Nombre = "D1";
            ModelAnillo.D1.TipoDato = tipodatodistance;
            ModelAnillo.D1.Unidad = unidaddistance;
            ModelAnillo.D1.DescripcionCorta = "D1";
            ModelAnillo.D1.DescripcionLarga = "Diámetro nominal del anillo.";

            ModelAnillo.H1.Nombre = "H1";
            ModelAnillo.H1.TipoDato = tipodatodistance;
            ModelAnillo.H1.Unidad = unidaddistance;
            ModelAnillo.H1.DescripcionCorta = "H1";
            ModelAnillo.H1.DescripcionLarga = "Width nominal del anillo";

            ModelAnillo.OvalityMin.Nombre = "OvalityMin";
            ModelAnillo.OvalityMin.TipoDato = tipodatodistance;
            ModelAnillo.OvalityMin.Unidad = unidaddistance;
            ModelAnillo.OvalityMin.DescripcionCorta = "Ovality Min";
            ModelAnillo.OvalityMin.DescripcionLarga = "Ovalidad mínima del anillo";

            ModelAnillo.OvalityMax.Nombre = "OvalityMax";
            ModelAnillo.OvalityMax.TipoDato = tipodatodistance;
            ModelAnillo.OvalityMax.Unidad = unidaddistance;
            ModelAnillo.OvalityMax.DescripcionCorta = "Ovality Max";
            ModelAnillo.OvalityMax.DescripcionLarga = "Ovalidad máxima del anillo";

            ModelAnillo.Tension.Nombre = "Tension";
            ModelAnillo.Tension.TipoDato = tipodatoforce;
            ModelAnillo.Tension.Unidad = unidadforce;
            ModelAnillo.Tension.DescripcionCorta = "Tension";
            ModelAnillo.Tension.DescripcionLarga = "Tensión del anillo";

            ModelAnillo.TensionTol.Nombre = "TensionTol";
            ModelAnillo.TensionTol.TipoDato = tipodatoforce;
            ModelAnillo.TensionTol.Unidad = unidadforce;
            ModelAnillo.TensionTol.DescripcionCorta = "Tension Tol";
            ModelAnillo.TensionTol.DescripcionLarga = "Tolerancia de tensión del anillo.";

            ModelAnillo.FreeGap.Nombre = "FreeGap";
            ModelAnillo.FreeGap.TipoDato = tipodatodistance;
            ModelAnillo.FreeGap.Unidad = unidaddistance;
            ModelAnillo.FreeGap.DescripcionCorta = "Free gap";
            ModelAnillo.FreeGap.DescripcionLarga = "Abertura libre del anillo";

        }
        #endregion
    }
}
