using System;
using System.IO;
using System.Xml;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Model;
using Model.Interfaces;
using View.Forms.Modals;
using View.Forms.UserControls;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using System.Collections.Generic;
using View.Forms.Routing;

using Model.ControlDocumentos;
using View.Forms.ControlDocumentos;
using System.Data;
using Encriptar;
using System.Globalization;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using PdfSharp;

namespace View.Services.ViewModel
{
    public class AnilloViewModel : INotifyPropertyChanged
    {

        #region Attributes
        private Anillo ModelAnillo;
        private CalculaMateriaPrima calcularMateriaPrima;
        DialogService dialogService;
        #endregion

        #region Properties
        public ObservableCollection<string> ListaEspecificacionesMateriaPrima { get; set; }

        public ObservableCollection<Cliente> ListaClientes { get; set; }

        public ObservableCollection<string> ListaTreatment { get; set; }

        private ObservableCollection<NumericEntry> propiedadesOD;
        public ObservableCollection<NumericEntry> PropiedadesOD
        {
            get { return propiedadesOD; }
            set { propiedadesOD = value; NotifyChange("PropiedadesOD"); }
        }

        private ObservableCollection<NumericEntry> propiedadesPuntas;
        public ObservableCollection<NumericEntry> PropiedadesPuntas
        {
            get { return propiedadesPuntas; }
            set { propiedadesPuntas = value; NotifyChange("PropiedadesPuntas"); }
        }

        private ObservableCollection<NumericEntry> propiedadesID;
        public ObservableCollection<NumericEntry> PropiedadesID
        {
            get { return propiedadesID; }
            set { propiedadesID = value; NotifyChange("PropiedadesID"); }
        }

        private ObservableCollection<NumericEntry> propiedadesLateral;
        public ObservableCollection<NumericEntry> PropiedadesLateral
        {
            get { return propiedadesLateral; }
            set { propiedadesLateral = value; NotifyChange("PropiedadesLateral"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesOD;
        public ObservableCollection<StackPanel> PanelPropiedadesOD
        {
            get { return panelPropiedadesOD; }
            set { panelPropiedadesOD = value; NotifyChange("PanelPropiedadesOD"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesPuntas;
        public ObservableCollection<StackPanel> PanelPropiedadesPuntas
        {
            get { return panelPropiedadesPuntas; }
            set { panelPropiedadesPuntas = value; NotifyChange("panelPropiedadesPuntas"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesID;
        public ObservableCollection<StackPanel> PanelPropiedadesID
        {
            get { return panelPropiedadesID; }
            set { panelPropiedadesID = value; NotifyChange("panelPropiedadesID"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesLateral;
        public ObservableCollection<StackPanel> PanelPropiedadesLateral
        {
            get { return panelPropiedadesLateral; }
            set { panelPropiedadesLateral = value; NotifyChange("PanelPropiedadesLateral"); }
        }

        private bool isOpededToogle;
        public bool IsOpenedToogle {
            get
            {
                return isOpededToogle;
            }
            set
            {
                isOpededToogle = value;
                NotifyChange("IsOpenedToogle");
            }
        }

        public ObservableCollection<string> MenuItems { get; set; }
        #endregion

        #region Propiedades del Modelo Anillo

        /// <summary>
		/// Cadena que representa el código general de algún elemento existente en sistema ERP.
		/// </summary>
		public string Codigo {
            get
            {
                return ModelAnillo.Codigo;
            }
            set
            {
                ModelAnillo.Codigo = value;
                NotifyChange("Codigo");
            }
        }

        /// <summary>
        /// Cadena que representa la descripción general del elemento existente en sistema ERP.
        /// </summary>
        public string DescripcionGeneral {
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
        public byte[] Imagen {
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
        public bool Activo {
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
        /// Double que representa la dureza máxima del anillo.
        /// </summary>
        public Propiedad HardnessMin {
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
        public Propiedad HardnessMax {
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

            //Inicializamos los atributos
            ListaEspecificacionesMateriaPrima = DataManager.GetAllEspecificacionesMateriaPrima();
            ListaClientes = DataManager.GetAllClientes();
            ListaTreatment = DataManager.GetAllTreatment();
            MenuItems = new ObservableCollection<string>();
            PropiedadesOD = new ObservableCollection<NumericEntry>();
            PropiedadesID = new ObservableCollection<NumericEntry>();
            PropiedadesLateral = new ObservableCollection<NumericEntry>();
            PropiedadesPuntas = new ObservableCollection<NumericEntry>();

            MenuItems.Add("New");
            MenuItems.Add("Open");
            MenuItems.Add("Import File");
            MenuItems.Add("Save");
            MenuItems.Add("SAP");

            //Inicializamos el plano;
            newPlano();

            dialogService = new DialogService();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Comando que reponde a la acción de consultar/Modificar la unidad de la propiedad D1(Diámetro nominal del anillo).
        /// </summary>
        public ICommand VerUnidadesD1
        {
            get
            {
                return new RelayCommand(o => verListaUnidades(D1));
            }
        }

        /// <summary>
        /// Comando que reponde a la acción de consultar/Modificar la unidad de la propiedad H1(Width del anillo).
        /// </summary>
        public ICommand VerUnidadesH1
        {
            get
            {
                return new RelayCommand(o => verListaUnidades(H1));
            }
        }

        /// <summary>
        /// Comando que reponde a la acción de consultar/Modificar la unidad de la propiedad FreeGap.
        /// </summary>
        public ICommand VerUnidadesFreeGap
        {
            get
            {
                return new RelayCommand(o => verListaUnidades(FreeGap));
            }
        }

        /// <summary>
        /// Comando que reponde a la acción de consultar/Modificar la unidad de la propiedad TensionTol.
        /// </summary>
        public ICommand VerUnidadesTensionTol
        {
            get
            {
                return new RelayCommand(o => verListaUnidades(TensionTol));
            }
        }

        /// <summary>
        /// Comando que reponde a la acción de consultar/Modificar la unidad de la propiedad Tension.
        /// </summary>
        public ICommand VerUnidadesTension
        {
            get
            {
                return new RelayCommand(o => verListaUnidades(Tension));
            }
        }

        /// <summary>
        /// Comando que reponde a la acción de consultar/Modificar la unidad de la propiedad OvalityMax.
        /// </summary>
        public ICommand VerUnidadesOvalityMax
        {
            get
            {
                return new RelayCommand(o => verListaUnidades(OvalityMax));
            }
        }

        /// <summary>
        /// Comando que reponde a la acción de consultar/Modificar la unidad de la propiedad OvalityMin.
        /// </summary>
        public ICommand VerUnidadesOvalityMin
        {
            get
            {
                return new RelayCommand(o => verListaUnidades(OvalityMin));
            }
        }

        public ICommand VerUnidadesHardnessMin
        {
            get {
                return new RelayCommand(o => verListaUnidades(HardnessMin));
            }
        }

        public ICommand VerUnidadesHardnessMax
        {
            get
            {
                return new RelayCommand(o => verListaUnidades(HardnessMax));
            }
        }

        public ICommand VerUnidadesMass
        {
            get
            {
                return new RelayCommand(o => verListaUnidades(Mass));
            }
        }

        public ICommand AbrirToogle
        {
            get
            {
                return new RelayCommand(o => abrirToogle());
            }
        }

        public ICommand CerrarToogle
        {
            get
            {
                return new RelayCommand(o => cerrarToogle());
            }
        }

        public ICommand NewPlano
        {
            get
            {
                return new RelayCommand(o => newPlano());
            }
        }

        public ICommand ImportXML
        {
            get
            {
                return new RelayCommand(o => importXML());
            }
        }

        public ICommand OpenPlano
        {
            get
            {
                return new RelayCommand(o => openPlano());
            }
        }

        public ICommand OpenCalculateDimencions
        {
            get
            {
                return new RelayCommand(o => openCalculateDimensions());
            }
        }

        public ICommand AbrirPerfiles
        {
            get
            {
                return new RelayCommand(o => abrirPlano());
            }
        }

        public ICommand CalcularRuta
        {
            get
            {
                return new RelayCommand(o => calcularRuta());
            }
        }

        public ICommand ViewRoute
        {
            get
            {
                return new RelayCommand(o => viewRoute());
            }
        }

        private void viewRoute()
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = ModelAnillo.DescripcionGeneral;

            PdfPage pdfPage = pdf.AddPage();
            pdfPage.Size = PageSize.A4;

            XGraphics graph = XGraphics.FromPdfPage(pdfPage);

            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            graph.DrawString("This is my first PDF document", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.Center);

            string pdfFilename = "firstpageacfgh.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);


        }
        #endregion

        #region Methods

        private async void calcularRuta()
        {

            //Comenzamos a simular el anillo

            ModelAnillo.D1 = new Propiedad { DescripcionCorta = "D1", DescripcionLarga = "Diámetro nominal", Imagen = null, Nombre = "D1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 4.32 };
            ModelAnillo.H1 = new Propiedad { DescripcionCorta = "H1", DescripcionLarga = "Width nominal", Imagen = null, Nombre = "H1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.0780 };

            PropiedadCadena especificacion = new PropiedadCadena();
            especificacion.DescripcionCorta = "MATERIAL";
            especificacion.DescripcionLarga = "Especificación de material";
            especificacion.Imagen = null;
            especificacion.Nombre = "Material MAHLE";
            especificacion.Valor = "SPR-128";

            ModelAnillo.MaterialBase = new MateriaPrima {Especificacion = especificacion};
            ModelAnillo.FreeGap = new Propiedad { DescripcionCorta = "Free Gap", DescripcionLarga = "Free Gap", Imagen = null, Nombre = "Total Free Gap Max", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.400 };

            ModelAnillo.PerfilID.Propiedades.Add(new Propiedad { DescripcionCorta = "Thickness", DescripcionLarga = "Thickness", Imagen = null, Nombre = "a1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.196 });
            ModelAnillo.PerfilID.Propiedades.Add(new Propiedad { DescripcionCorta = "Thickness Min", DescripcionLarga = "Thickness Min", Imagen = null, Nombre = "a1 Tol Min", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.005 });
            ModelAnillo.PerfilID.Propiedades.Add(new Propiedad { DescripcionCorta = "Thickness Max", DescripcionLarga = "Thickness Max", Imagen = null, Nombre = "a1 Tol Max", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.005 });

            ModelAnillo.PerfilLateral.Propiedades.Add(new Propiedad { DescripcionCorta = "h1", DescripcionLarga = "Width", Imagen = null, Nombre = "h1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.0775 });
            ModelAnillo.PerfilLateral.Propiedades.Add(new Propiedad { DescripcionCorta = "h1 Tol", DescripcionLarga = "Width", Imagen = null, Nombre = "h1 Tol", TipoDato = "Distance", Unidad = "Inch (in)", Valor = .0005 });

            ModelAnillo.PerfilOD.PropiedadesCadena.Add(new PropiedadCadena { DescripcionCorta = "Proceso",DescripcionLarga = "Proceso", Imagen = null, Nombre = "Proceso", Valor = "Doble"});
            ModelAnillo.PerfilOD.Propiedades.Add(new Propiedad { DescripcionCorta = "CLOSING STRESS", DescripcionLarga = "CLOSING STRESS", Imagen = null, Nombre = "CLOSING STRESS", Valor = 33400});
            ModelAnillo.PerfilOD.PropiedadesCadena.Add(new PropiedadCadena { DescripcionCorta = "RingShape", DescripcionLarga = "RingShape", Imagen = null, Nombre = "RingShape", Valor = "#3" });

            ModelAnillo.PerfilPuntas.Propiedades.Add(new Propiedad { DescripcionCorta = "GapMin", DescripcionLarga = "Gap Min", Imagen = null, Nombre = "GapMin", Valor = 0.011 });
            ModelAnillo.PerfilPuntas.Propiedades.Add(new Propiedad { DescripcionCorta = "GapMax", DescripcionLarga = "Gap Max", Imagen = null, Nombre = "GapMax", Valor = 0.021 });


            ModelAnillo.MaterialBase.Especificacion = new PropiedadCadena { DescripcionCorta = "MATERIAL:", DescripcionLarga = "MATERIAL BASE DEL ANILLO", Imagen = null, Nombre = "Material MAHLE", Valor = "SPR-128" };

            ModelAnillo.TipoAnillo = "RBT10";

            //Terminamos de simular el anillo

            Anillo anilloProcesado = new Anillo();
            DescripcionGeneral = string.Format("{0:0.00000}", D1.Valor) + " X " + string.Format("{0:0.00000}", H1.Valor) + " " + TipoAnillo;

            if (ModelAnillo.MaterialBase.TipoDeMaterial == "HIERRO GRIS")
            {
                Operaciones = Router.CalcularHierroGris(ModelAnillo);
                //Ingresar calculo de placa modelo.
                calcularMateriaPrima = new CalculaMateriaPrima(ModelAnillo);
                ModelAnillo.MaterialBase = calcularMateriaPrima.CalcularPlacaModelo();
                if (ModelAnillo.MaterialBase.Codigo.Equals("CODIFICAR"))
                {
                    MetroDialogSettings setting = new MetroDialogSettings();
                    setting.AffirmativeButtonText = "SI";
                    setting.NegativeButtonText = "NO";
                    dialogService = new DialogService();

                    MessageDialogResult result = await dialogService.SendMessage("Atención", "No se encontró ninguna placa modelo para el componente ingresado  ¿Desea generar una placa modelo nueva?", setting, MessageDialogStyle.AffirmativeAndNegative,"Desing Process");

                    switch (result)
                    {
                        case MessageDialogResult.Negative:
                            break;
                        case MessageDialogResult.Affirmative:
                            WPattern pattern = new WPattern();
                            pattern.ShowDialog();
                            break;
                        case MessageDialogResult.FirstAuxiliary:
                            break;
                        case MessageDialogResult.SecondAuxiliary:
                            break;
                        default:
                            break;
                    }


                }
                anilloProcesado.PropiedadesAdquiridasProceso.Add(new Propiedad{ TipoDato = "Distance", DescripcionCorta = "Piece", DescripcionLarga = "Piece", Imagen = null, Nombre = "Piece", Unidad = "Inch (in)", Valor = calcularMateriaPrima.Piece });
            }

            //Empieza cálculo de width
            int i = Operaciones.Count - 1;
            int c = 0;
            double widthMin = Module.GetValorPropiedadMin("h1", ModelAnillo.PerfilLateral.Propiedades,true);
            double widthMax = Module.GetValorPropiedadMax("h1", ModelAnillo.PerfilLateral.Propiedades,true);
            double widthFinal = (widthMin + widthMax) / 2;

            SubjectWidth subjectWidth = new SubjectWidth();
            bool banUltimaOperacionWidth = true;
            while (i >= 0)
            {
                if (Operaciones[i] is IObserverWidth)
                {
                    if (banUltimaOperacionWidth)
                    {
                        subjectWidth.Subscribe(Operaciones[i] as IObserverWidth, widthFinal);
                        banUltimaOperacionWidth = false;
                    }
                    else
                    {
                        subjectWidth.Subscribe(Operaciones[i] as IObserverWidth);
                        subjectWidth.Notify(c);
                    }
                    c += 1;
                }
                i = i - 1;
            }

            //Termina cálculo de width

            /*Cálculo de diámetro*/

            i = Operaciones.Count - 1;
            c = 0;
            SubjectDiametro subjectDiametro = new SubjectDiametro();
            bool banUltimaOperacionDiametro = true;
            double mediaGap = Math.Round((Module.GetValorPropiedad("GapMin", ModelAnillo.PerfilPuntas.Propiedades) + Module.GetValorPropiedad("GapMax", ModelAnillo.PerfilPuntas.Propiedades))/2, 3);
            while (i >= 0)
            {
                if (Operaciones[i] is IObserverDiametro)
                {
                    if (banUltimaOperacionDiametro)
                    {
                        var operacion = (IObserverDiametro)Operaciones[i];
                        operacion.Gap = mediaGap;
                        subjectDiametro.Subscribe(operacion, D1.Valor);
                        banUltimaOperacionDiametro = false;
                    }
                    else
                    {
                        var operacion = (IObserverDiametro)Operaciones[i];
                        operacion.Gap = mediaGap;
                        //subjectDiametro.Subscribe(Operaciones[i] as IObserverDiametro);
                        subjectDiametro.Subscribe(operacion);
                        subjectDiametro.Notify(c);
                    }
                    c += 1;
                }
                i = i - 1;
            }


            /* Cálculo de thickness */

            i = Operaciones.Count - 1;
            c = 0;
            double mediaThickness = Math.Round((Module.GetValorPropiedad("ThicknessMin", PerfilID.Propiedades) + Module.GetValorPropiedad("ThicknessMax", PerfilID.Propiedades)) / 2,4);
            SubjectThickness subjectThickness = new SubjectThickness();
            bool banUltimaOperacionThickness = true;
            while (i >= 0)
            {
                if (Operaciones[i] is IObserverThickness)
                {
                    if (banUltimaOperacionThickness)
                    {
                        subjectThickness.Subscribe(Operaciones[i] as IObserverThickness, mediaThickness);
                        banUltimaOperacionThickness = false;
                    }
                    else
                    {
                        subjectThickness.Subscribe(Operaciones[i] as IObserverThickness);
                        subjectThickness.Notify(c);
                    }
                    c += 1;
                }

                i = i - 1;
            }
            

            anilloProcesado.Activo = ModelAnillo.Activo;
            anilloProcesado.cliente = ModelAnillo.cliente;
            anilloProcesado.Codigo = ModelAnillo.Codigo;
            anilloProcesado.CondicionesDeEmpaque = ModelAnillo.CondicionesDeEmpaque;
            anilloProcesado.CustomerDocNo = ModelAnillo.CustomerDocNo;
            anilloProcesado.CustomerPartNumber = ModelAnillo.CustomerPartNumber;
            anilloProcesado.CustomerRevisionLevel = ModelAnillo.CustomerRevisionLevel;
            anilloProcesado.DescripcionGeneral = ModelAnillo.DescripcionGeneral;
            anilloProcesado.HardnessMax = ModelAnillo.HardnessMax;
            anilloProcesado.HardnessMin = ModelAnillo.HardnessMin;
            anilloProcesado.Imagen = ModelAnillo.Imagen;
            anilloProcesado.Mass = ModelAnillo.Mass;
            anilloProcesado.MaterialBase = ModelAnillo.MaterialBase;
            anilloProcesado.NivelRevicion = ModelAnillo.NivelRevicion;
            anilloProcesado.NoPlano = ModelAnillo.NoPlano;
            anilloProcesado.Operaciones = ModelAnillo.Operaciones;
            anilloProcesado.PerfilID = new Perfil();
            anilloProcesado.PerfilOD = new Perfil();
            anilloProcesado.PerfilPuntas = new Perfil();
            anilloProcesado.PerfilLateral = new Perfil();
            anilloProcesado.Size = ModelAnillo.Size;
            anilloProcesado.Tension = ModelAnillo.Tension;
            anilloProcesado.TensionTol = ModelAnillo.TensionTol;
            anilloProcesado.TipoAnillo = ModelAnillo.TipoAnillo;
            anilloProcesado.PropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            anilloProcesado.PropiedadesBoolAdquiridasProceso = new ObservableCollection<PropiedadBool>();
            anilloProcesado.PropiedadesCadenaAdquiridasProceso = new ObservableCollection<PropiedadCadena>();

            //Realizamos las operaciones
            bool ban = true;
            Anillo aProcesado = new Anillo();
            foreach (IOperacion element in Operaciones)
            {
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
            }
        }

        private void abrirPlano()
        {
            
        }

        /// <summary>
        /// Método que inicializa todas las propiedades para generar un nuevo plano.
        /// </summary>
        private async void newPlano()
        {
            //Inicializamos los servicios de dialog.
            DialogService dialogService = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendrá el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "Format MAHLE";
            setting.NegativeButtonText = "Old Format";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo guardamos en una variable local.
            MessageDialogResult result = await dialogService.SendMessage("Attention", "Select the format of the plane:", setting, MessageDialogStyle.AffirmativeAndNegative);

            //Para cada resultado realizamos una acción.
            switch (result)
            {
                case MessageDialogResult.Negative:

                    //Inicializamos el nuevo modelo.
                    ModelAnillo = new Anillo();

                    //Establecemos a todas las propiedades del modelo anillo los valores por default.
                    SetUnidadesDefault("Distance", "Inch (in)", "Force", "LBS", "Dureza", "HRC", "Mass", "Gram (g)");
                    break;
                case MessageDialogResult.Affirmative:

                    //Inicializamos el nuevo modelo.
                    ModelAnillo = new Anillo();

                    //Establecemos a todas las propiedades del modelo anillo los valores por default.
                    SetUnidadesDefault("Distance", "Millimeter (mm)", "Force", "LBS", "Dureza", "HRC", "Mass", "Gram (g)");
                    break;
                case MessageDialogResult.FirstAuxiliary:
                    break;
                case MessageDialogResult.SecondAuxiliary:
                    break;
                default:
                    break;
            }

            //Cerramos el menu lateral derecho.
            cerrarToogle();
        }

        /// <summary>
        /// Método que importa un archivo .xml con la estructura del plano.
        /// </summary>
        private async void importXML()
        {
            //Declaramos los servicios de dialogo.
            DialogService dialogService = new DialogService();

            //Declaramos una ventana para poder seleccionar el archivo.
            OpenFileDialog dialog = new OpenFileDialog();

            //Establecemos las propiuedades del objeto dialog.
            dialog.Title = "Open xml file.";
            dialog.Filter = "XML files|*.xml";
            dialog.InitialDirectory = @"C:\";

            //Ejecutamos el método para abrir la ventana.
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //Asignamos el nombre del archivo seleccionado a una variable local.
                string path = dialog.FileName;

                //Comprobamos que el archivo exista.
                if (File.Exists(path))
                {
                    //Obtenemos el nombre de la extensión y lo asignamos a una variable local.
                    string ext = Path.GetExtension(path);

                    //Comporbamos que en efecto sea un archivo xml.
                    if (ext == ".xml")
                    {
                        //Declaramos un objeto el cual contendra la informacion de archivo xml.
                        XmlDocument doc = new XmlDocument();

                        //Ejecutamos el método para cargar la informaicón del archivo seleccionado al objeto creado.
                        doc.Load(path);

                        //Obtenemos todos los nodos que contiene el archivo xml con la siguiente estructura. "/itens/item"
                        XmlNodeList Nodes = doc.DocumentElement.SelectNodes("/itens/item");

                        //Comprobamos que exista mas de un nodo.
                        if (Nodes.Count > 0)
                        {
                            //Iteramos la lista obtenida de nodos.
                            foreach (XmlNode node in Nodes)
                            {
                                //Creamos un objeto de tipo itens el cual contendra la inforamción del registro iterado.
                                Itens obj = new Itens();

                                //Mapeamos los valores de cada atributo en las respectivas propiedades del objeto.
                                obj.id = node.Attributes["id"].Value;
                                obj.name = node.Attributes["name"].Value;
                                obj.value = node.Attributes["value"].Value;

                                //Ejecutamos el método el cual se encarga de asignar los valores a cada propiedad del plano.
                                mapearPropiedad(obj);
                            }

                            //Cerramos el menú.
                            IsOpenedToogle = false;

                            //Enviamos un mensaje para informar que se importo el plano correctamente.
                            await dialogService.SendMessage("Information", "Se cargo el plano del componente :" + Codigo);
                        }
                        else
                        {
                            //En caso de que el archivo no contenga elementos, enviamos un mensaje de alerta.
                            await dialogService.SendMessage("Attention", "No has seleccionado un archivo válido");
                        }
                    }
                    else
                    {
                        //En caso de que el usuario no seleccione un archivo .xml, enviamos un mensaje indicando formato no soportado.
                        await dialogService.SendMessage("Attention", "No has seleccionado un archivo válido");
                    }
                }
                else
                {
                    //En caso de que no exista el archivo, enviamos un mensaje de alerta.
                    await dialogService.SendMessage("Attention", "No has seleccionado un archivo válido");
                }
            }
        }

        /// <summary>
        /// Método que ordena de manera alfanumérica una colección observable de tipo Propiedad
        /// </summary>
        /// <param name="Collection">Colección que se requiere ordenar.</param>
        /// <returns>Colección observable ordenada</returns>
        private ObservableCollection<Propiedad> SortObservableCollectionPropiedad(ObservableCollection<Propiedad> Collection)
        {
            //Ordenamos la colección y le resutlado lo asignamos a una lista de tipo propiedad.
            List<Propiedad> Lista = Collection.OrderBy(x => x.Nombre).ToList();

            //Limpiamos la colección.
            Collection.Clear();

            //Iteramos la lista para ir guardando cada item en la colección.
            foreach (var laPropiedad in Lista)
            {
                //Agregamos el item iterado a la colección.
                Collection.Add(laPropiedad);
            }

            //Retornamos la colección.
            return Collection;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CollectionNumeric"></param>
        /// <returns></returns>
        private ObservableCollection<StackPanel> SetNumericEntryToStackPanel(ObservableCollection<NumericEntry> CollectionNumeric, ObservableCollection<Propiedad> CollectionPropiedades)
        {
            //Declaramos la colección que será la que retornemos en el método.
            ObservableCollection<StackPanel> CollectionPanel = new ObservableCollection<StackPanel>();

            //Verificamos si las dos colecciones contienen el mismo número de elementos.
            if (CollectionNumeric.Count == CollectionPropiedades.Count)
            {
                int c = 0;

                while (c < CollectionNumeric.Count)
                {
                    string[] separador = CollectionPropiedades[c].Nombre.Split(' ');


                    c += 1;
                }
            }


            //Retornamos la colección creada.
            return CollectionPanel;
        }

        /// <summary>
        /// Método que abre un plano guardado en la base de datos.
        /// </summary>
        private void openPlano()
        {
            //-------------------Perfil OD-------------------
            PerfilOD.Propiedades = new ObservableCollection<Propiedad>();
            PerfilOD.Propiedades.Add(new Propiedad { Nombre = "S1 MIN", DescripcionCorta = "S1 MIN", DescripcionLarga = "DIÁMETRO NOMINAL DEL ANILLO", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });

            PerfilOD.Propiedades = SortObservableCollectionPropiedad(PerfilOD.Propiedades);

            PropiedadesOD.Clear();

            foreach (var item in PerfilOD.Propiedades)
            {
                NumericEntry uc = new NumericEntry();
                PropiedadViewModel mvm = new PropiedadViewModel(item);
                uc.DataContext = mvm;
                PropiedadesOD.Add(uc);
            }

            PanelPropiedadesOD = SetNumericEntryToStackPanel(PropiedadesOD, PerfilOD.Propiedades);
            //-------------------Perfil OD-------------------

            //-------------------Perfil Puntas-------------------
            PerfilPuntas.Propiedades = new ObservableCollection<Propiedad>();
            PerfilPuntas.Propiedades.Add(new Propiedad { Nombre = "Q1 MIN", DescripcionCorta = "Q1 MIN", DescripcionLarga = "Q1 MIN", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });
            PerfilPuntas.Propiedades.Add(new Propiedad { Nombre = "Q1 MAX", DescripcionCorta = "Q1 MAX", DescripcionLarga = "Q1 MAX", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });
            PerfilPuntas.Propiedades.Add(new Propiedad { Nombre = "A1", DescripcionCorta = "A1", DescripcionLarga = "A1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });
            PerfilPuntas.Propiedades.Add(new Propiedad { Nombre = "Y6", DescripcionCorta = "Y6", DescripcionLarga = "Y6", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });

            PerfilPuntas.Propiedades = SortObservableCollectionPropiedad(PerfilPuntas.Propiedades);

            PropiedadesPuntas.Clear();

            foreach (var item in PerfilPuntas.Propiedades)
            {
                NumericEntry uc = new NumericEntry();
                PropiedadViewModel mvm = new PropiedadViewModel(item);
                uc.DataContext = mvm;
                PropiedadesPuntas.Add(uc);
            }
            PanelPropiedadesPuntas = SetNumericEntryToStackPanel(PropiedadesPuntas, PerfilPuntas.Propiedades);
            //-------------------Perfil Puntas-------------------

            //-------------------Perfil ID-------------------
            PerfilID.Propiedades = new ObservableCollection<Propiedad>();
            PerfilID.Propiedades.Add(new Propiedad { Nombre = "P1", DescripcionCorta = "P1", DescripcionLarga = "P1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });

            PerfilID.Propiedades = SortObservableCollectionPropiedad(PerfilID.Propiedades);

            PropiedadesID.Clear();

            foreach (var item in PerfilID.Propiedades)
            {
                NumericEntry uc = new NumericEntry();
                PropiedadViewModel mvm = new PropiedadViewModel(item);
                uc.DataContext = mvm;
                PropiedadesID.Add(uc);
            }
            panelPropiedadesID = SetNumericEntryToStackPanel(PropiedadesID, PerfilID.Propiedades);
            //-------------------Perfil ID-------------------

            //-------------------Perfil Lateral-------------------
            PerfilLateral.Propiedades = new ObservableCollection<Propiedad>();
            PerfilLateral.Propiedades.Add(new Propiedad { Nombre = "T1", DescripcionCorta = "T1", DescripcionLarga = "T1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });

            PerfilLateral.Propiedades = SortObservableCollectionPropiedad(PerfilLateral.Propiedades);

            PropiedadesLateral.Clear();

            foreach (var item in PerfilLateral.Propiedades)
            {
                NumericEntry uc = new NumericEntry();
                PropiedadViewModel mvm = new PropiedadViewModel(item);
                uc.DataContext = mvm;
                PropiedadesLateral.Add(uc);
            }
            PanelPropiedadesLateral = SetNumericEntryToStackPanel(PropiedadesLateral, PerfilLateral.Propiedades);
            //-------------------Perfil Lateral-------------------
            
        }

        private void openCalculateDimensions()
        {
            WDimensions p = new WDimensions();
            p.ShowDialog();
            
        }

        /// <summary>
        /// Método que asigna el iten a la propiedad correspondiente.
        /// </summary>
        /// <param name="obj"></param>
        private void mapearPropiedad(Itens obj)
        {
            //Declaramos una bandera la cual no ayudará a indicar si ya se asigno la propiedad, y no recorrer todas las propiedades.
            bool ban = true;
            switch (obj.name)
            {
                case "d1":
                    D1.Valor = Convert.ToDouble(obj.value);
                    D1 = D1;
                    break;
                case "h1":
                    H1.Valor = Convert.ToDouble(obj.value);
                    H1 = H1;
                    break;
                case "Doc No Ring":
                    NoPlano = obj.value;
                    break;
                case "Material MAHLE":
                    MaterialBase.Especificacion.Valor = obj.value;
                    MaterialBase = MaterialBase;
                    break;
                case "Mass Calculated":
                    Mass.Valor =  Convert.ToDouble(obj.value);
                    Mass = Mass;
                    break;
                case "Cust Name":
                    cliente.NombreCliente = obj.value;
                    cliente = cliente;
                    break;
                default:
                    ban = false;
                    break;
            }

            int c =  0;
            while (c < PerfilID.Propiedades.Count && !ban)
            {
                if (PerfilID.Propiedades[c].Nombre == obj.name)
                {
                    PerfilID.Propiedades[c].Valor = Convert.ToDouble(obj.value);
                    PerfilID.Propiedades[c] = PerfilID.Propiedades[c];
                    ban = true;
                }
                c += 1;
            }

            c = 0;
            while (c < PerfilID.PropiedadesCadena.Count && !ban)
            {
                if (PerfilID.PropiedadesCadena[c].Nombre == obj.name)
                {
                    PerfilID.PropiedadesCadena[c].Valor = obj.value;
                    PerfilID.PropiedadesCadena[c] = PerfilID.PropiedadesCadena[c];
                    ban = true;
                }
                c += 1;
            }

            /*
             * Falta agregar el código para mapear los valores boleanos.
             */

            c = 0;
            while (c < PerfilLateral.Propiedades.Count && !ban)
            {
                if (PerfilLateral.Propiedades[c].Nombre == obj.name)
                {
                    PerfilLateral.Propiedades[c].Valor = Convert.ToDouble(obj.value);
                    PerfilLateral.Propiedades[c] = PerfilLateral.Propiedades[c];
                    ban = true;
                }
                c += 1;
            }

            c = 0;
            while (c < PerfilLateral.PropiedadesCadena.Count && !ban)
            {
                if (PerfilLateral.PropiedadesCadena[c].Nombre == obj.name)
                {
                    PerfilLateral.PropiedadesCadena[c].Valor = obj.value;
                    PerfilLateral.PropiedadesCadena[c] = PerfilLateral.PropiedadesCadena[c];
                    ban = true;
                }
                c += 1;
            }

            /*
             * Falta agregar el código para mapear los valores booleanos.
             */

            c = 0;
            while (c < PerfilOD.Propiedades.Count && !ban)
            {
                if (PerfilOD.Propiedades[c].Nombre == obj.name)
                {
                    PerfilOD.Propiedades[c].Valor = Convert.ToDouble(obj.value);
                    PerfilOD.Propiedades[c] = PerfilOD.Propiedades[c];
                    ban = true;
                }
                c += 1;
            }

            c = 0;
            while (c < PerfilOD.PropiedadesCadena.Count && !ban)
            {
                if (PerfilOD.PropiedadesCadena[c].Nombre == obj.name)
                {
                    PerfilOD.PropiedadesCadena[c].Valor = obj.value;
                    PerfilOD.PropiedadesCadena[c] = PerfilOD.PropiedadesCadena[c];
                    ban = true;
                }
                c += 1;
            }
            /*
             * Falta agregar el código para mapear los valores booleanos.
             */

            c = 0;
            while (c < PerfilPuntas.Propiedades.Count && !ban)
            {
                if (PerfilPuntas.Propiedades[c].Nombre == obj.name)
                {
                    PerfilPuntas.Propiedades[c].Valor = Convert.ToDouble(obj.value);
                    PerfilPuntas.Propiedades[c] = PerfilPuntas.Propiedades[c];
                    ban = true;
                }
                c += 1;
            }

            c = 0;
            while (c < PerfilPuntas.PropiedadesCadena.Count && !ban)
            {
                if (PerfilPuntas.PropiedadesCadena[c].Nombre == obj.name)
                {
                    PerfilPuntas.PropiedadesCadena[c].Valor = obj.value;
                    PerfilPuntas.PropiedadesCadena[c] = PerfilPuntas.PropiedadesCadena[c];
                    ban = true;
                }
                c += 1;
            }
            /*
             * Falta agregar el código para mapear los valores booleanos.
             */

        }

        /// <summary>
        /// Método que muestra una ventana con todas las posibles unidades a mostrar.
        /// </summary>
        /// <param name="laPropiedad">Propiedad que representa el modelo de la pantalla que se muestra.</param>
        private void verListaUnidades(Propiedad laPropiedad)
        {
            //Inicializamos el contexto de Propiedad.
            PropiedadViewModel contextoUnidades = new PropiedadViewModel(laPropiedad);

            //Declaramos un objeto que representa la pantalla a mostrar.
            frmViewUnidades modal = new frmViewUnidades();

            //Asignamos el contexto a la pantalla.
            modal.DataContext = contextoUnidades;

            //Ejecutamos el método para que se muestre la pantalla.
            modal.ShowDialog();

            //Verificamos cual es la propiedad y asignamos el atributo model de la clase PropiedadViewModel para que se visualice el cambio de unidad.
            if (laPropiedad.Nombre == "H1")
            {
                H1 = contextoUnidades.model;
            }
            else {
                if (laPropiedad.Nombre == "D1")
                {
                    D1 = contextoUnidades.model;
                }
                else {
                    if (laPropiedad.Nombre == "FreeGap")
                    {
                        FreeGap = contextoUnidades.model;
                    }
                    else {
                        if (laPropiedad.Nombre == "Tension")
                        {
                            Tension = contextoUnidades.model;
                        }
                        else {
                            if (laPropiedad.Nombre == "TensionTol")
                            {
                                TensionTol = contextoUnidades.model;
                            }
                            else {
                                if (laPropiedad.Nombre == "OvalityMin")
                                {
                                    OvalityMin = contextoUnidades.model;
                                }
                                else {
                                    if (laPropiedad.Nombre == "OvalityMax")
                                    {
                                        OvalityMax = contextoUnidades.model;
                                    }
                                    else {
                                        if (laPropiedad.Nombre == "HardnessMin")
                                        {
                                            HardnessMin = contextoUnidades.model;
                                        }
                                        else {
                                            if (laPropiedad.Nombre == "HardnessMax")
                                            {
                                                HardnessMax = contextoUnidades.model;
                                            }
                                            else {
                                                if (laPropiedad.Nombre == "MassAnillo")
                                                {
                                                    Mass = contextoUnidades.model;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método que inicializa los valores por defualt de todas las propiedades del modelo Anillo.
        /// </summary>
        private void SetUnidadesDefault(string tipodatodistance, string unidaddistance, string tipodatoforce, string unidadforce, string tipodatodureza, string unidaddureza, string tipodatoMass, string unidadMass)
        {

            ModelAnillo.D1.Nombre = "D1";
            ModelAnillo.D1.TipoDato = tipodatodistance;
            ModelAnillo.D1.Unidad = unidaddistance;
            ModelAnillo.D1.DescripcionCorta = "D1";
            ModelAnillo.D1.DescripcionLarga = "Diámetro nominal del anillo.";
            D1 = ModelAnillo.D1;

            ModelAnillo.H1.Nombre = "H1";
            ModelAnillo.H1.TipoDato = tipodatodistance;
            ModelAnillo.H1.Unidad = unidaddistance;
            ModelAnillo.H1.DescripcionCorta = "H1";
            ModelAnillo.H1.DescripcionLarga = "Width nominal del anillo";
            H1 = ModelAnillo.H1;

            ModelAnillo.OvalityMin.Nombre = "OvalityMin";
            ModelAnillo.OvalityMin.TipoDato = tipodatodistance;
            ModelAnillo.OvalityMin.Unidad = unidaddistance;
            ModelAnillo.OvalityMin.DescripcionCorta = "Ovality Min";
            ModelAnillo.OvalityMin.DescripcionLarga = "Ovalidad mínima del anillo";
            OvalityMin = ModelAnillo.OvalityMin;

            ModelAnillo.OvalityMax.Nombre = "OvalityMax";
            ModelAnillo.OvalityMax.TipoDato = tipodatodistance;
            ModelAnillo.OvalityMax.Unidad = unidaddistance;
            ModelAnillo.OvalityMax.DescripcionCorta = "Ovality Max";
            ModelAnillo.OvalityMax.DescripcionLarga = "Ovalidad máxima del anillo";
            OvalityMax = ModelAnillo.OvalityMax;

            ModelAnillo.Tension.Nombre = "Tension";
            ModelAnillo.Tension.TipoDato = tipodatoforce;
            ModelAnillo.Tension.Unidad = unidadforce;
            ModelAnillo.Tension.DescripcionCorta = "Tension";
            ModelAnillo.Tension.DescripcionLarga = "Tensión del anillo";
            Tension = ModelAnillo.Tension;

            ModelAnillo.TensionTol.Nombre = "TensionTol";
            ModelAnillo.TensionTol.TipoDato = tipodatoforce;
            ModelAnillo.TensionTol.Unidad = unidadforce;
            ModelAnillo.TensionTol.DescripcionCorta = "Tension Tol";
            ModelAnillo.TensionTol.DescripcionLarga = "Tolerancia de tensión del anillo.";
            TensionTol = ModelAnillo.TensionTol;

            ModelAnillo.FreeGap.Nombre = "FreeGap";
            ModelAnillo.FreeGap.TipoDato = tipodatodistance;
            ModelAnillo.FreeGap.Unidad = unidaddistance;
            ModelAnillo.FreeGap.DescripcionCorta = "Free gap";
            ModelAnillo.FreeGap.DescripcionLarga = "Abertura libre del anillo";
            FreeGap = ModelAnillo.FreeGap;

            ModelAnillo.HardnessMin.Nombre = "HardnessMin";
            ModelAnillo.HardnessMin.TipoDato = tipodatodureza;
            ModelAnillo.HardnessMin.Unidad = unidaddureza;
            ModelAnillo.HardnessMin.DescripcionCorta = "Hardness Min";
            ModelAnillo.HardnessMin.DescripcionLarga = "Dureza mínima";
            HardnessMin = ModelAnillo.HardnessMin;

            ModelAnillo.HardnessMax.Nombre = "HardnessMax";
            ModelAnillo.HardnessMax.TipoDato = tipodatodureza;
            ModelAnillo.HardnessMax.Unidad = unidaddureza;
            ModelAnillo.HardnessMax.DescripcionCorta = "Hardness Max";
            ModelAnillo.HardnessMax.DescripcionLarga = "Dureza máxima";
            HardnessMax = ModelAnillo.HardnessMax;

            ModelAnillo.Mass.Nombre = "MassAnillo";
            ModelAnillo.Mass.TipoDato = tipodatoMass;
            ModelAnillo.Mass.Unidad = unidadMass;
            ModelAnillo.Mass.DescripcionCorta = "Mass";
            ModelAnillo.Mass.DescripcionLarga = "Peso del Anillo";
            Mass = ModelAnillo.Mass;
        }

        /// <summary>
        /// Método que abre el menú lateral derecho de la pantalla.
        /// </summary>
        private void abrirToogle()
        {
            //Asignamos a la propiedad el valor de true. Esto abrirá el menú.
            IsOpenedToogle = true;
        }

        /// <summary>
        /// Método que cierra el menú lateral derecho de la pantalla.
        /// </summary>
        private void cerrarToogle()
        {
            //Asignamos a la propiedad el valor de false. Esto cerrará el menú.
            IsOpenedToogle = false;
        }
        #endregion
    }
}
