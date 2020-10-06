using System.Collections.ObjectModel;
using Model.Interfaces;
namespace Model
{
    public class Anillo : Arquetipo
    {
        #region Propiedades
        /// <summary>
        /// Perfil que representa el diámetro exterior del anillo.
        /// </summary>
        public Perfil PerfilOD { get; set; }

        /// <summary>
        /// Perfil que representa el diámetro interior del anillo.
        /// </summary>
        public Perfil PerfilID { get; set; }

        /// <summary>
        /// Perfil que representa la cara lateral del anillo.
        /// </summary>
        public Perfil PerfilLateral { get; set; }

        /// <summary>
        /// Perfil que representa las puntas del anillo.
        /// </summary>
        public Perfil PerfilPuntas { get; set; }

        /// <summary>
        /// Propiedad que representa el diámetro del anillo.
        /// </summary>
        public Propiedad D1 { get; set; }

        /// <summary>
        /// Propiedad que representa el width del anillo.
        /// </summary>
        public Propiedad H1 { get; set; }

        /// <summary>
        /// Propiedad que representa el FreeGap del anillo.
        /// </summary>
        public Propiedad FreeGap { get; set; }

        /// <summary>
        /// Propiedad que representa el peso del anillo.
        /// </summary>
        public Propiedad Mass { get; set; }

        /// <summary>
        /// Propiedad que representa la tensión del anillo.
        /// </summary>
        public Propiedad Tension { get; set; }

        /// <summary>
        /// Propiedad que representa la tolerancia de la tensión del anillo.
        /// </summary>
        public Propiedad TensionTol { get; set; }

        /// <summary>
        /// Propiedad que representa la ovalidad mínima del anillo.
        /// </summary>
        public Propiedad OvalityMin { get; set; }

        /// <summary>
        /// Propiedad que representa la ovalidad máxima del anillo.
        /// </summary>
        public Propiedad OvalityMax { get; set; }

        /// <summary>
        /// Double que representa la dureza máxima del anillo.
        /// </summary>
        public Propiedad HardnessMin { get; set; }

        /// <summary>
        /// Double que representa la dureza mínima del anillo.
        /// </summary>
        public Propiedad HardnessMax { get; set; }

        /// <summary>
        /// Materia prima que representa el material base del anillo.
        /// </summary>
        public MateriaPrima MaterialBase { get; set; }

        /// <summary>
        /// Cadena que representa el número de plano del anillo.
        /// </summary>
        public string NoPlano { get; set; }

        /// <summary>
        /// Cadena que representa el número de parte del cliente.
        /// </summary>
        public string CustomerPartNumber { get; set; }

        /// <summary>
        /// Cadena que representa el nivel de revisión del cliente.
        /// </summary>
        public string CustomerRevisionLevel { get; set; }

        /// <summary>
        /// Cadena que representa la sobre medida del plano.
        /// </summary>
        /// <example>
        /// STD: Estandar.
        /// +0.030 : Sobre medida.
        /// </example>
        public string Size { get; set; }

        /// <summary>
        /// Cadena que representa el tipo de anillo.
        /// </summary>
        public string TipoAnillo { get; set; }

        /// <summary>
        /// Cadena que representa el numero de documento del cliente.
        /// </summary>
        public string CustomerDocNo { get; set; }

        /// <summary>
        /// Cadena que representa el tratamiento que tiene el anillo.
        /// </summary>
        public string Treatment { get; set; }

        /// <summary>
        /// Cadena que representa la especificación de tratamiento que tiene el anillo.
        /// </summary>
        public string EspecTreatment { get; set; }

        /// <summary>
        /// Cadena que representa el texto con la información del anillo general. Esto para sistema ERP.
        /// </summary>
        public string Caratula { get; set; }

        /// <summary>
        /// Cliente que representa a cual pertenece el anillo.
        /// </summary>
        public Cliente cliente { get; set; }

        /// <summary>
        /// Empaquetado que representa las condiciones de empaque para inspección final.
        /// </summary>
        public Empaquetado CondicionesDeEmpaque { get; set; }

        /// <summary>
        /// Revisión del plano.
        /// </summary>
        public Revision NivelRevicion { get; set; }

        /// <summary>
        /// Colección de tipo propiedad la cual contiene las propiedades que son adquiridas por el anillo durante su proceso.
        /// </summary>
        public ObservableCollection<Propiedad> PropiedadesAdquiridasProceso { get; set; }

        /// <summary>
        /// Colección de tipo PropiedadBool la cual contiene las propiedades que son adquiridas por el anillo durante su proceso.
        /// </summary>
        public ObservableCollection<PropiedadBool> PropiedadesBoolAdquiridasProceso { get; set; }

        /// <summary>
        /// Colección de tipo PropiedadCadena la cual contiene las propiedades que son adquiridas por el anillo durante su proceso.
        /// </summary>
        public ObservableCollection<PropiedadCadena> PropiedadesCadenaAdquiridasProceso { get; set; }
        
        /// <summary>
        /// Colección de tipo IOPeracion la cual contiene todas las operaciones que se necesitan para procesar el anillo.
        /// </summary>
        public ObservableCollection<IOperacion> Operaciones { get; set; }

        /// <summary>
        /// Colección de tipo PinturaAnillo la cual contiene todas las franjas de pintura que tiene el anillo.
        /// </summary>
        public ObservableCollection<PinturaAnillo> FranjasPintura { get; set; } //Falta agregar la tabla para ir guardando los datos
        
        public ObservableCollection<DO_Norma> ListaNormas { get; set; }

        public ObservableCollection<DO_Norma> ListaAllNormas { get; set; }
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public Anillo()
        {
            //Asignamos los valores por default a todas las propiedades.
            PerfilID = new Perfil();
            PerfilLateral = new Perfil();
            PerfilOD = new Perfil();
            PerfilPuntas = new Perfil();
            D1 = new Propiedad();
            H1 = new Propiedad();
            FreeGap = new Propiedad();
            Mass = new Propiedad();
            Tension = new Propiedad();
            TensionTol = new Propiedad();
            MaterialBase = new MateriaPrima();
            NoPlano = string.Empty;
            CustomerPartNumber = string.Empty;
            CustomerRevisionLevel = string.Empty;
            Size = string.Empty;
            TipoAnillo = string.Empty;
            HardnessMin = new Propiedad();
            HardnessMax = new Propiedad();
            CustomerDocNo = string.Empty;
            Treatment = string.Empty;
            EspecTreatment = string.Empty;
            Caratula = string.Empty;
            cliente = new Cliente();
            CondicionesDeEmpaque = new Empaquetado();
            NivelRevicion = new Revision();
            PropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            PropiedadesBoolAdquiridasProceso = new ObservableCollection<PropiedadBool>();
            PropiedadesCadenaAdquiridasProceso = new ObservableCollection<PropiedadCadena>();
            Operaciones = new ObservableCollection<IOperacion>();
            FranjasPintura = new ObservableCollection<PinturaAnillo>();
            OvalityMax = new Propiedad();
            OvalityMin = new Propiedad();
            ListaNormas = new ObservableCollection<DO_Norma>();
            ListaAllNormas = new ObservableCollection<DO_Norma>();
        }
        #endregion

        #region Métodos

        public override string ToString()
        {
            return Codigo + "       " + DescripcionGeneral;
        }

        #endregion
    }
}
