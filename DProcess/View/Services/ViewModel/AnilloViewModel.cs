﻿using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Linq;
using System.Windows.Input;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Model.Interfaces;
using View.Resources;
using View.Forms.Modals;
using View.Forms.Routing;
using View.Forms.RawMaterial;
using View.Forms.UserControls;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using MahApps.Metro.Controls.Dialogs;

namespace View.Services.ViewModel
{
    public class AnilloViewModel : INotifyPropertyChanged
    {
        public enum UnidadDistance
        {
            [Description("Inch (in)")]
            Inch,

            [Description("Millimeter (mm)")]
            Milimeter,

            [Description("centimeter(cm)")]
            Centimeter
        }

        #region Attributes
        private Anillo ModelAnillo;
        private CalculaMateriaPrima calcularMateriaPrima;
        DialogService dialogService;
        private string NombreUsuario;
        private Usuario User;
        #endregion

        #region Properties

        private string _NotaColorSelected;

        public string NotaColorSelected
        {
            get { return _NotaColorSelected; }
            set { _NotaColorSelected = value; NotifyChange("NotaColorSelected"); }
        }

        private bool _banFreeGap;
        public bool banFreeGap
        {
            get
            {
                return _banFreeGap;
            }
            set
            {
                _banFreeGap = value;
                NotifyChange("banFreeGap");
            }
        }

        private double _WidthFranjaSelected;
        public double WidthFranjaSelected
        {
            get { return _WidthFranjaSelected; }
            set { _WidthFranjaSelected = value; NotifyChange("WidthFranjaSelected"); }
        }

        public ObservableCollection<UbicacionPintura> ListaUbicaciones { get; set; }

        private UbicacionPintura _UbicacionSelected;
        public UbicacionPintura UbicacionSelected
        {
            get { return _UbicacionSelected; }
            set { _UbicacionSelected = value; NotifyChange("UbicacionSelected"); }
        }

        public ObservableCollection<string> ListaColores { get; set; }

        private string _ColorSelected;
        public string ColorSelected
        {
            get { return _ColorSelected; }
            set { _ColorSelected = value; NotifyChange("ColorSelected"); }
        }

        public ObservableCollection<string> ListaEspecificacionesMateriaPrima { get; set; }

        private ObservableCollection<IOperacion> _ListaOperacionesOpcionales;
        public ObservableCollection<IOperacion> ListaOperacionesOpcionales
        {
            get { return _ListaOperacionesOpcionales; }
            set { _ListaOperacionesOpcionales = value; NotifyChange("ListaOperacionesOpcionales"); }
        }

        private IOperacion _OperacionAntesAddOperacion;
        public IOperacion OperacionAntesAddOperacion
        {
            get { return _OperacionAntesAddOperacion; }
            set { _OperacionAntesAddOperacion = value; NotifyChange("OperacionAntesAddOperacion"); }
        }

        private IOperacion _OperacionSeleccionadaOpcional;
        public IOperacion OperacionSeleccionadaOpcional
        {
            get { return _OperacionSeleccionadaOpcional; }
            set { _OperacionSeleccionadaOpcional = value; NotifyChange("OperacionSeleccionadaOpcional"); }
        }

        private ObservableCollection<MateriaPrimaRolado> _ListaIronRawMaterial;
        public ObservableCollection<MateriaPrimaRolado> ListaIronRawMaterial
        {
            get { return _ListaIronRawMaterial; }
            set { _ListaIronRawMaterial = value; NotifyChange("ListaIronRawMaterial"); }
        }

        private ObservableCollection<MateriaPrimaAceros> _ListaIronRailRawMaterial;
        public ObservableCollection<MateriaPrimaAceros> ListaIronRailRawMaterial
        {
            get { return _ListaIronRailRawMaterial; }
            set { _ListaIronRailRawMaterial = value; }
        }

        public ObservableCollection<Cliente> ListaClientes { get; set; }

        public ObservableCollection<string> ListaTreatment { get; set; }

        private ObservableCollection<NumericEntry> propiedadesOD;
        public ObservableCollection<NumericEntry> PropiedadesOD
        {
            get { return propiedadesOD; }
            set { propiedadesOD = value; NotifyChange("PropiedadesOD"); }
        }

        private ObservableCollection<StringEntry> propiedadesCadenaOD;
        public ObservableCollection<StringEntry> PropiedadesCadenaOD
        {
            get { return propiedadesCadenaOD; }
            set { propiedadesCadenaOD = value; NotifyChange("PropiedadesCadenaOD"); }
        }

        private ObservableCollection<BoolEntry> propiedadesBoolOD;
        public ObservableCollection<BoolEntry> PropiedadesBoolOD
        {
            get { return propiedadesBoolOD; }
            set { propiedadesBoolOD = value; NotifyChange("PropiedadesBoolOD"); }
        }

        private ObservableCollection<OptionalEntry> propiedadesOptionalOD;
        public ObservableCollection<OptionalEntry> PropiedadesOptionalOD
        {
            get { return propiedadesOptionalOD; }
            set { propiedadesOptionalOD = value; NotifyChange("PropiedadesOptionalOD"); }
        }

        private ObservableCollection<NumericEntry> propiedadesPuntas;
        public ObservableCollection<NumericEntry> PropiedadesPuntas
        {
            get { return propiedadesPuntas; }
            set { propiedadesPuntas = value; NotifyChange("PropiedadesPuntas"); }
        }

        private ObservableCollection<StringEntry> propiedadesCadenaPuntas;
        public ObservableCollection<StringEntry> PropiedadesCadenaPuntas
        {
            get { return propiedadesCadenaPuntas; }
            set { propiedadesCadenaPuntas = value; NotifyChange("PropiedadesCadenaPuntas"); }
        }

        private ObservableCollection<BoolEntry> propiedadesBoolPuntas;
        public ObservableCollection<BoolEntry> PropiedadesBoolPuntas
        {
            get { return propiedadesBoolPuntas; }
            set { propiedadesBoolPuntas = value; NotifyChange("PropiedadesBoolPuntas"); }
        }

        private ObservableCollection<OptionalEntry> propiedadesOptionalPuntas;
        public ObservableCollection<OptionalEntry> PropiedadesOptionalPuntas
        {
            get { return  propiedadesOptionalPuntas; }
            set {  propiedadesOptionalPuntas = value; NotifyChange("PropiedadesOptionalPuntas"); }
        }

        private ObservableCollection<NumericEntry> propiedadesID;
        public ObservableCollection<NumericEntry> PropiedadesID
        {
            get { return propiedadesID; }
            set { propiedadesID = value; NotifyChange("PropiedadesID"); }
        }

        private ObservableCollection<StringEntry> propiedadesCadenaID;
        public ObservableCollection<StringEntry> PropiedadesCadenaID
        {
            get { return propiedadesCadenaID; }
            set { propiedadesCadenaID = value; NotifyChange("PropiedadesCadenaID"); }
        }

        private ObservableCollection<BoolEntry> propiedadesBoolID;
        public ObservableCollection<BoolEntry> PropiedadesBoolID
        {
            get { return propiedadesBoolID; }
            set { propiedadesBoolID = value; NotifyChange("PropiedadesBoolID"); }
        }

        private ObservableCollection<OptionalEntry> propiedadesOptionalID;
        public ObservableCollection<OptionalEntry> PropiedadesOptionalID
        {
            get { return propiedadesOptionalID; }
            set { propiedadesOptionalID = value; NotifyChange("PropiedadesOptionalID"); }
        }

        private ObservableCollection<NumericEntry> propiedadesLateral;
        public ObservableCollection<NumericEntry> PropiedadesLateral
        {
            get { return propiedadesLateral; }
            set { propiedadesLateral = value; NotifyChange("PropiedadesLateral"); }
        }

        private ObservableCollection<StringEntry> propiedadesCadenaLateral;
        public ObservableCollection<StringEntry> PropiedadesCadenaLateral
        {
            get { return propiedadesCadenaLateral; }
            set { propiedadesCadenaLateral = value; NotifyChange("PropiedadesCadenaLateral"); }
        }

        private ObservableCollection<BoolEntry> propiedadesBoolLateral;
        public ObservableCollection<BoolEntry> PropiedadesBoolLateral
        {
            get { return propiedadesBoolLateral; }
            set { propiedadesBoolLateral = value; NotifyChange("PropiedadesBoolLateral"); }
        }

        private ObservableCollection<OptionalEntry> propiedadesOptionalLateral;
        public ObservableCollection<OptionalEntry> PropiedadesOptionalLateral
        {
            get { return propiedadesOptionalLateral; }
            set { propiedadesOptionalLateral = value; NotifyChange("PropiedadesOptionalLateral"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesOD;
        public ObservableCollection<StackPanel> PanelPropiedadesOD
        {
            get { return panelPropiedadesOD; }
            set { panelPropiedadesOD = value; NotifyChange("PanelPropiedadesOD"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesCadenaOD;
        public ObservableCollection<StackPanel> PanelPropiedadesCadenaOD
        {
            get { return panelPropiedadesCadenaOD; }
            set { panelPropiedadesCadenaOD = value; NotifyChange("PanelPropiedadesCadenaOD"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesBoolOD;
        public ObservableCollection<StackPanel> PanelPropiedadesBoolOD
        {
            get { return panelPropiedadesBoolOD; }
            set { panelPropiedadesBoolOD = value; NotifyChange("PanelPropiedadesBoolOD"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesOpcionalesOD;
        public ObservableCollection<StackPanel> PanelPropiedadesOpcionalesOD
        {
            get { return panelPropiedadesOpcionalesOD; }
            set { panelPropiedadesOpcionalesOD = value; NotifyChange("PanelPropiedadesOpcionalesOD"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesPuntas;
        public ObservableCollection<StackPanel> PanelPropiedadesPuntas
        {
            get { return panelPropiedadesPuntas; }
            set { panelPropiedadesPuntas = value; NotifyChange("panelPropiedadesPuntas"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesCadenaPuntas;
        public ObservableCollection<StackPanel> PanelPropiedadesCadenaPuntas
        {
            get { return panelPropiedadesCadenaPuntas; }
            set { panelPropiedadesCadenaPuntas = value; NotifyChange("PanelPropiedadesCadenaPuntas"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesBoolPuntas;
        public ObservableCollection<StackPanel> PanelPropiedadesBoolPuntas
        {
            get { return panelPropiedadesBoolPuntas; }
            set { panelPropiedadesBoolPuntas = value; NotifyChange("PanelPropiedadesBoolPuntas"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesOpcionalesPuntas;
        public ObservableCollection<StackPanel> PanelPropiedadesOpcionalesPuntas
        {
            get { return panelPropiedadesOpcionalesPuntas; }
            set { panelPropiedadesOpcionalesPuntas = value; NotifyChange("PanelPropiedadesOpcionalesPuntas"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesID;
        public ObservableCollection<StackPanel> PanelPropiedadesID
        {
            get { return panelPropiedadesID; }
            set { panelPropiedadesID = value; NotifyChange("panelPropiedadesID"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesCadenaID;
        public ObservableCollection<StackPanel> PanelPropiedadesCadenaID
        {
            get { return panelPropiedadesCadenaID; }
            set { panelPropiedadesCadenaID = value; NotifyChange("PanelPropiedadesCadenaID"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesBoolID;
        public ObservableCollection<StackPanel> PanelPropiedadesBoolID
        {
            get { return panelPropiedadesBoolID; }
            set { panelPropiedadesBoolID = value; NotifyChange("PanelPropiedadesBoolID"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesOpcionalesID;
        public ObservableCollection<StackPanel> PanelPropiedadesOpcionalesID
        {
            get { return panelPropiedadesOpcionalesID; }
            set { panelPropiedadesOpcionalesID = value; NotifyChange("PanelPropiedadesOpcionalesID"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesLateral;
        public ObservableCollection<StackPanel> PanelPropiedadesLateral
        {
            get { return panelPropiedadesLateral; }
            set { panelPropiedadesLateral = value; NotifyChange("PanelPropiedadesLateral"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesCadenaLateral;
        public ObservableCollection<StackPanel> PanelPropiedadesCadenaLateral
        {
            get { return panelPropiedadesCadenaLateral; }
            set { panelPropiedadesCadenaLateral = value; NotifyChange("panelPropiedadesCadenaLateral"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesBoolLateral;
        public ObservableCollection<StackPanel> PanelPropiedadesBoolLateral
        {
            get { return panelPropiedadesBoolLateral; }
            set { panelPropiedadesBoolLateral = value; NotifyChange("PanelPropiedadesBoolLateral"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesOpcionalesLateral;
        public ObservableCollection<StackPanel> PanelPropiedadesOpcionalesLateral
        {
            get { return panelPropiedadesOpcionalesLateral; }
            set { panelPropiedadesOpcionalesLateral = value; NotifyChange("PanelPropiedadesOpcionalesLateral"); }
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

        private IOperacion operationSelected;
        public IOperacion OperationSelected
        {
            get { return operationSelected; }
            set { operationSelected = value; NotifyChange("OperationSelected"); }
        }

        private ObservableCollection<Perfil> _AllPerfilesOD;
        public ObservableCollection<Perfil> AllPerfilesOD
        {
            get { return _AllPerfilesOD; }
            set { _AllPerfilesOD = value; NotifyChange("AllPerfilesOD"); }
        }

        private Perfil _PerfilSeleccionadoOD;
        public Perfil PerfilSeleccionadoOD
        {
            get { return _PerfilSeleccionadoOD; }
            set { _PerfilSeleccionadoOD = value; NotifyChange("PerfilSeleccionadoOD"); }
        }

        private ObservableCollection<Perfil> _AllPerfilesID;
        public ObservableCollection<Perfil> AllPerfilesID
        {
            get { return _AllPerfilesID; }
            set { _AllPerfilesID = value; NotifyChange("AllPerfilesID"); }
        }

        private Perfil _PerfilSeleccionadoID;
        public Perfil PerfilSeleccionadoID
        {
            get { return _PerfilSeleccionadoID; }
            set { _PerfilSeleccionadoID = value; NotifyChange("_PerfilSeleccionadoID"); }
        }

        private ObservableCollection<Perfil> _AllPerfilesLateral;
        public ObservableCollection<Perfil> AllPerfilesLateral
        {
            get { return _AllPerfilesLateral; }
            set { _AllPerfilesLateral = value; NotifyChange("AllPerfilesLateral"); }
        }

        private Perfil _PerfilSeleccionadoLateral;
        public Perfil PerfilSeleccionadoLateral
        {
            get { return _PerfilSeleccionadoLateral; }
            set { _PerfilSeleccionadoLateral = value; NotifyChange("PerfilSeleccionadoLateral"); }
        }

        private ObservableCollection<Perfil> _AllPerfilesPuntas;
        public ObservableCollection<Perfil> AllPerfilesPuntas
        {
            get { return _AllPerfilesPuntas; }
            set { _AllPerfilesPuntas = value; NotifyChange("AllPerfilesPuntas"); }
        }

        private ObservableCollection<Arquetipo> _ListaComponentes;
        public ObservableCollection<Arquetipo> ListaComponentes {
            get
            {
                return _ListaComponentes;
            }
            set
            {
                _ListaComponentes = value;
                NotifyChange("ListaComponentes");
            }
        }

        private Arquetipo _ComponenteSeleccionado;
        public Arquetipo ComponenteSeleccionado
        {
            get { return _ComponenteSeleccionado; }
            set { _ComponenteSeleccionado = value; NotifyChange("ComponenteSeleccionado"); }
        }

        private Perfil _PerfilSeleccionadoPuntas;
        public Perfil PerfilSeleccionadoPuntas
        {
            get { return _PerfilSeleccionadoPuntas; }
            set { _PerfilSeleccionadoPuntas = value; NotifyChange("PerfilSeleccionadoPuntas"); }
        }

        private string _EspecificacionMaterialSeleccionada;
        public string EspecificacionMaterialSeleccionada
        {
            get { return _EspecificacionMaterialSeleccionada; }
            set { _EspecificacionMaterialSeleccionada = value; NotifyChange("EspecificacionMaterialSeleccionada"); }
        }

        private bool _IsMilimeter;

        public bool IsMilimeter
        {
            get { return _IsMilimeter; }
            set { _IsMilimeter = value; NotifyChange("IsMilimeter"); }
        }

        private bool _IsPackageOpen;
        public bool IsPackageOpen
        {
            get { return _IsPackageOpen; }
            set { _IsPackageOpen = value; NotifyChange("IsPackageOpen"); }
        }

        private bool _IsPinturaOpen;

        public bool IsPinturaOpen
        {
            get { return _IsPinturaOpen; }
            set { _IsPinturaOpen = value; NotifyChange("IsPinturaOpen"); }
        }

        private bool _IsNormasOpen;
        public bool IsNormasOpen
        {
            get { return _IsNormasOpen; }
            set { _IsNormasOpen = value; NotifyChange("IsNormasOpen"); }
        }

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

        public ObservableCollection<DO_Norma> ListaNormas {
            get
            {
                return ModelAnillo.ListaNormas;
            }
            set
            {
                ModelAnillo.ListaNormas = value;
                NotifyChange("ListaNormas");
            }
        }

        public ObservableCollection<DO_Norma> ListaAllNormas
        { get; set;
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

        public AnilloViewModel(string nombreUsuario, Usuario user)
        {
            //Inicializamos el objeto anillo que representa nuestro modelo.
            ModelAnillo = new Anillo();
            User = user;
            //Inicializamos los atributos
            NombreUsuario = nombreUsuario;

            ListaEspecificacionesMateriaPrima = DataManager.GetAllEspecificacionesMateriaPrima();
            ListaClientes = DataManager.GetAllClientes();
            ListaTreatment = DataManager.GetAllTreatment();
            ListaIronRawMaterial = new ObservableCollection<MateriaPrimaRolado>();

            PropiedadesOD = new ObservableCollection<NumericEntry>();
            PropiedadesCadenaOD = new ObservableCollection<StringEntry>();
            PropiedadesBoolOD = new ObservableCollection<BoolEntry>();
            PropiedadesOptionalOD = new ObservableCollection<OptionalEntry>();

            PropiedadesID = new ObservableCollection<NumericEntry>();
            PropiedadesCadenaID = new ObservableCollection<StringEntry>();
            PropiedadesBoolID = new ObservableCollection<BoolEntry>();
            PropiedadesOptionalID = new ObservableCollection<OptionalEntry>();

            PropiedadesLateral = new ObservableCollection<NumericEntry>();
            PropiedadesCadenaLateral = new ObservableCollection<StringEntry>();
            PropiedadesBoolLateral = new ObservableCollection<BoolEntry>();
            PropiedadesOptionalLateral = new ObservableCollection<OptionalEntry>();

            PropiedadesPuntas = new ObservableCollection<NumericEntry>();
            PropiedadesCadenaPuntas = new ObservableCollection<StringEntry>();
            PropiedadesBoolPuntas = new ObservableCollection<BoolEntry>();
            PropiedadesOptionalPuntas = new ObservableCollection<OptionalEntry>();

            PerfilSeleccionadoID = new Perfil();
            PerfilSeleccionadoOD = new Perfil();
            PerfilSeleccionadoLateral = new Perfil();
            PerfilSeleccionadoPuntas = new Perfil();

            //Inicializamos el plano;
            newPlano(true);

            dialogService = new DialogService();

            //Mandamos llamar el metodo que genera el Menú
            CreateMenuItems();

            AllPerfilesOD = DataManager.GetAllPerfiles("PERFIL O.D.");
            AllPerfilesID = DataManager.GetAllPerfiles("PERFIL I.D.");
            AllPerfilesLateral = DataManager.GetAllPerfiles("PERFIL CARAS LATERALES");
            AllPerfilesPuntas = DataManager.GetAllPerfiles("PERFIL PUNTAS");

            ListaAllNormas = DataManager.GetAllNormas();

            ListaColores = DataManager.GetAllColores();

            ListaUbicaciones = DataManager.GetUbicacionesSegmentos();
            FranjasPintura = new ObservableCollection<PinturaAnillo>();

        }

        /// <summary>
        /// Constructor para cuando se abre desde la pantalla de Busqueda de Casting.
        /// </summary>
        /// <param name="user"></param>
        public AnilloViewModel(Usuario user)
        {
            User = user;
            //Inicializamos el objeto anillo que representa nuestro modelo.
            ModelAnillo = new Anillo();

            //Inicializamos los atributos
            NombreUsuario = User.NombreUsuario;
            ListaEspecificacionesMateriaPrima = DataManager.GetAllEspecificacionesMateriaPrima();
            ListaClientes = DataManager.GetAllClientes();
            ListaTreatment = DataManager.GetAllTreatment();
            ListaIronRawMaterial = new ObservableCollection<MateriaPrimaRolado>();

            PropiedadesOD = new ObservableCollection<NumericEntry>();
            PropiedadesCadenaOD = new ObservableCollection<StringEntry>();
            PropiedadesBoolOD = new ObservableCollection<BoolEntry>();
            PropiedadesOptionalOD = new ObservableCollection<OptionalEntry>();

            PropiedadesID = new ObservableCollection<NumericEntry>();
            PropiedadesCadenaID = new ObservableCollection<StringEntry>();
            PropiedadesBoolID = new ObservableCollection<BoolEntry>();
            PropiedadesOptionalID = new ObservableCollection<OptionalEntry>();

            PropiedadesLateral = new ObservableCollection<NumericEntry>();
            PropiedadesCadenaLateral = new ObservableCollection<StringEntry>();
            PropiedadesBoolLateral = new ObservableCollection<BoolEntry>();
            PropiedadesOptionalLateral = new ObservableCollection<OptionalEntry>();

            PropiedadesPuntas = new ObservableCollection<NumericEntry>();
            PropiedadesCadenaPuntas = new ObservableCollection<StringEntry>();
            PropiedadesBoolPuntas = new ObservableCollection<BoolEntry>();
            PropiedadesOptionalPuntas = new ObservableCollection<OptionalEntry>();

            PerfilSeleccionadoID = new Perfil();
            PerfilSeleccionadoOD = new Perfil();
            PerfilSeleccionadoLateral = new Perfil();
            PerfilSeleccionadoPuntas = new Perfil();

            //Inicializamos el plano;
            newPlano(false);

            dialogService = new DialogService();

            //Mandamos llamar el metodo que genera el Menú
            CreateMenuItems();

            AllPerfilesOD = DataManager.GetAllPerfiles("PERFIL O.D.");
            AllPerfilesID = DataManager.GetAllPerfiles("PERFIL I.D.");
            AllPerfilesLateral = DataManager.GetAllPerfiles("PERFIL CARAS LATERALES");
            AllPerfilesPuntas = DataManager.GetAllPerfiles("PERFIL PUNTAS");
        }

        #endregion

        #region Commands

        public ICommand BuscarCasting
        {
            get
            {
                return new RelayCommand(o => buscarCasting());
            }
        }

        public ICommand CalculateDimensions {
            get {
                return new RelayCommand(o => calculateDimensions());
            }
        }

        public ICommand AddPintura {
            get
            {
                return new RelayCommand(o => addPintura());
            }
        }

        public ICommand ChangeFlyoutPackage
        {
            get
            {
                return new RelayCommand(o => changeFlyoutPackage());
            }
        }

        public ICommand ChangeFlyoutPintura
        {
            get
            {
                return new RelayCommand(o => changeFlyoutPintura());
            }
        }

        public ICommand ChangeFlyoutNormas
        {
            get
            {
                return new RelayCommand(o => changeFlyoutNormas());
            }
        }

        public ICommand VerNormas
        {
            get
            {
                return new RelayCommand(o => verNormas());
            }
        }

        public ICommand AddOperation
        {
            get
            {
                return new RelayCommand(o => addOperation());
            }
        }

        private void addOperation()
        {
            int index = (OperacionAntesAddOperacion.NoOperacion / 10);
            Operaciones.Insert(index, OperacionSeleccionadaOpcional);
        }

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

        public ICommand NewPlano
        {
            get
            {
                return new RelayCommand(o => newPlano(true));
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

        public ICommand SavePlano
        {
            get
            {
                return new RelayCommand(o => savePlano());
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

        public ICommand ReCalcularRuta
        {
            get
            {
                return new RelayCommand(o => calcularRuta(false));
            }
        }

        public ICommand ViewRoute
        {
            get
            {
                return new RelayCommand(o => viewRoute());
            }
        }

        public ICommand ViewRouting
        {
            get
            {
                return new RelayCommand(o => viewRouting());
            }
        }

        public ICommand ConversionFTaFD
        {
            get
            {
                return new RelayCommand(o => ConversionDeFTaFD());
            }
        }

        public ICommand SetMaterialRemover
        {
            get
            {
                return new RelayCommand(o => setMaterialRemover());
            }
        }

        public ICommand CreateRing
        {
            get
            {
                return new RelayCommand(o => createRing());
            }
        }

        public ICommand GoCalculateFreeGap
        {
            get
            {
                return new RelayCommand(o => calculateFreeGap());
            }
        }

        public ICommand BuscarComponentes {
            get
            {
                return new RelayCommand(param => buscarComponente((string)param));
            }
        }

        public ICommand HideUnhideFreeGap
        {
            get
            {
                return new RelayCommand(param => hideUnhidePanelFreeGap());
            }
        }
        #endregion

        #region Methods

        private void hideUnhidePanelFreeGap() {
            string tipoMaterial = DataManager.GetTipoMaterial(EspecificacionMaterialSeleccionada);

            banFreeGap = tipoMaterial == "ACERO INOXIDABLE" || tipoMaterial == "ACERO AL CARBON" ? false : true;
        }

        private void buscarComponente(string parametro)
        {
            ListaComponentes = DataManager.GetAllArquetipo("");
            List<Arquetipo> lista = ListaComponentes.Where(x => x.Codigo.Contains(parametro)).ToList();
            List<Arquetipo> listaDescripcion = ListaComponentes.Where(x => x.DescripcionGeneral.Contains(parametro)).ToList();
            ListaComponentes = new ObservableCollection<Arquetipo>();
            foreach (var item in lista)
                ListaComponentes.Add(item);

            foreach (var item in listaDescripcion)
            {
                if (ListaComponentes.Where(x => x.Codigo == item.Codigo).ToList().Count==0)
                {
                    ListaComponentes.Add(item);
                }
            }
        }

        private void calculateFreeGap()
        {
            WCalculoFreeGap ventana = new WCalculoFreeGap();
            ventana.DataContext = new CalculoFreeGapViewModel();

            ventana.ShowDialog();
        }

        private void calculateDimensions()
        {
            WCalculateDimensions frm = new WCalculateDimensions();

            CalculateDimensionsViewModel viewmodel = new CalculateDimensionsViewModel();

            frm.DataContext = viewmodel;

            frm.ShowDialog();
        }

        private void addPintura()
        {
            PinturaAnillo objPintura = new PinturaAnillo();

            objPintura.AnchoPintura = WidthFranjaSelected;
            objPintura.Color = ColorSelected;
            objPintura.Nota = NotaColorSelected;
            objPintura.UbicacionFranja = UbicacionSelected;

            FranjasPintura.Add(objPintura);
            FranjasPintura = FranjasPintura;

            WidthFranjaSelected = 0;
            ColorSelected = string.Empty;
            NotaColorSelected = string.Empty;
            UbicacionSelected = new UbicacionPintura();

        }

        private void changeFlyoutPackage()
        {
            IsPackageOpen = IsPackageOpen ? false : true;

            if (IsPackageOpen)
            {
                CondicionesDeEmpaque = DataManager.GetCondicionesEmpaqueSegmentos(Module.ConvertTo(ModelAnillo.D1.TipoDato, ModelAnillo.D1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), ModelAnillo.D1.Valor), Module.ConvertTo(ModelAnillo.H1.TipoDato, ModelAnillo.H1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), ModelAnillo.H1.Valor));
            }
        }

        private void changeFlyoutPintura()
        {
            IsPinturaOpen = IsPinturaOpen ? false : true;
        }

        private void changeFlyoutNormas()
        {
            IsNormasOpen = IsNormasOpen ? false : true;
        }

        private void buscarCasting()
        {
            calcularMateriaPrima = new CalculaMateriaPrima(ModelAnillo);
            MaterialBase = calcularMateriaPrima.CalcularPlacaModelo();
        }

        /// <summary>
        /// Método que calcula las operaciones.
        /// </summary>
        /// <param name="banCalcularOperaciones">Si es false, solo re calcularan las medidas (Thickness, width, diámetro) con las mismas operaciones que previamente se definieron.</param>
        private async void calcularRuta(bool banCalcularOperaciones = true)
        {
            #region Simulacion anillo HIERRO GRIS
            ////Comenzamos a simular el anillo

            //D1 = new Propiedad { DescripcionCorta = "D1", DescripcionLarga = "Diámetro nominal", Imagen = null, Nombre = "D1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 4.32 };
            //H1 = new Propiedad { DescripcionCorta = "H1", DescripcionLarga = "Width nominal", Imagen = null, Nombre = "H1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.0780 };

            //Codigo = "RBT10-004";

            //PropiedadCadena especificacion = new PropiedadCadena();
            //especificacion.DescripcionCorta = "MATERIAL";
            //especificacion.DescripcionLarga = "Especificación de material";
            //especificacion.Imagen = null;
            //especificacion.Nombre = "Material MAHLE";
            //especificacion.Valor = "SPR-128";

            //MaterialBase = new MateriaPrima { Especificacion = especificacion };
            //FreeGap = new Propiedad { DescripcionCorta = "Free Gap", DescripcionLarga = "Free Gap", Imagen = null, Nombre = "Total Free Gap Max", TipoDato = "Distance", Unidad = "Inch (in)", Valor = .500 };

            //Propiedad Thickness = new Propiedad { DescripcionCorta = "Thickness", DescripcionLarga = "Thickness", Imagen = null, Nombre = "a1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.196 };
            //Propiedad ThicknessMin = new Propiedad { DescripcionCorta = "Thickness Min", DescripcionLarga = "Thickness Min", Imagen = null, Nombre = "a1 Tol Min", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.005 };
            //Propiedad ThicknessMax = new Propiedad { DescripcionCorta = "Thickness Max", DescripcionLarga = "Thickness Max", Imagen = null, Nombre = "a1 Tol Max", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.005 };

            //PerfilID.Propiedades.Add(Thickness);
            //PerfilID.Propiedades.Add(ThicknessMin);
            //PerfilID.Propiedades.Add(ThicknessMax);

            //Propiedad h1 = new Propiedad { DescripcionCorta = "h1", DescripcionLarga = "Width", Imagen = null, Nombre = "h1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.0775 };
            //Propiedad h1Tol = new Propiedad { DescripcionCorta = "h1 Tol", DescripcionLarga = "Width", Imagen = null, Nombre = "h1 Tol", TipoDato = "Distance", Unidad = "Inch (in)", Valor = .0005 };

            //PerfilLateral.Propiedades.Add(h1);
            //PerfilLateral.Propiedades.Add(h1Tol);

            //PerfilOD.PropiedadesCadena.Add(new PropiedadCadena { DescripcionCorta = "Proceso", DescripcionLarga = "Proceso", Imagen = null, Nombre = "Proceso", Valor = "Doble" });
            //PerfilOD.Propiedades.Add(new Propiedad { DescripcionCorta = "CLOSING STRESS", DescripcionLarga = "CLOSING STRESS", Imagen = null, Nombre = "CLOSING STRESS", Valor = 33400 });
            //PerfilOD.PropiedadesCadena.Add(new PropiedadCadena { DescripcionCorta = "RingShape", DescripcionLarga = "RingShape", Imagen = null, Nombre = "RingShape", Valor = "#3" });

            //PerfilPuntas.Propiedades.Add(new Propiedad { DescripcionCorta = "Gap", DescripcionLarga = "Gap del anillo", Imagen = null, Nombre = "s1", Valor = 0.016, TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch) });
            //PerfilPuntas.Propiedades.Add(new Propiedad { DescripcionCorta = "Gap Tol Max", DescripcionLarga = "Tolerancia máxima en gap del anillo", Imagen = null, Nombre = "s1 Tol Max", Valor = 0.005, TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch) });
            //PerfilPuntas.Propiedades.Add(new Propiedad { DescripcionCorta = "Gap Tol Min", DescripcionLarga = "Tolerancia mínima en gap del anillo", Imagen = null, Nombre = "s1 Tol Min", Valor = 0.005, TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch) });

            //MaterialBase.Especificacion = new PropiedadCadena { DescripcionCorta = "MATERIAL:", DescripcionLarga = "MATERIAL BASE DEL ANILLO", Imagen = null, Nombre = "Material MAHLE", Valor = "SPR-128" };
            //HardnessMax = new Propiedad { DescripcionCorta = "Hardness Max", DescripcionLarga = "Hardness Max", Imagen = null, Nombre = "HardnessMax", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Dureza), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDureza.RB), Valor = 106 };
            //HardnessMin = new Propiedad { DescripcionCorta = "Hardness Min", DescripcionLarga = "Hardness Min", Imagen = null, Nombre = "HardnessMin", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Dureza), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDureza.RB), Valor = 95 };

            //TipoAnillo = "RBT10";

            //cliente = new Cliente { NombreCliente = "MAHLE", IdCliente = 12 };

            ////Terminamos de simular el anillo
            #endregion

            #region Simulacion Anillo ACERO AL CARBON (ROLADOS)
            //PropiedadCadena especificacion = new PropiedadCadena();
            //especificacion.DescripcionCorta = "MATERIAL";
            //especificacion.DescripcionLarga = "Especificación de material";
            //especificacion.Imagen = null;
            //especificacion.Nombre = "Material MAHLE";
            //especificacion.Valor = "MS064-1";

            //ModelAnillo.MaterialBase.Especificacion = especificacion;

            //H1 = new Propiedad { DescripcionCorta = "H1", DescripcionLarga = "Width nominal", Imagen = null, Nombre = "H1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.05866 };
            //D1 = new Propiedad { DescripcionCorta = "D1", DescripcionLarga = "Diámetro nominal", Imagen = null, Nombre = "D1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 4.4055 };

            //FreeGap = new Propiedad { DescripcionCorta = "Free Gap", DescripcionLarga = "Free Gap", Imagen = null, Nombre = "Total Free Gap Max", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.302 };

            //Propiedad h1 = new Propiedad { DescripcionCorta = "h1", DescripcionLarga = "Width", Imagen = null, Nombre = "h1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.05866 };
            //Propiedad h1Tol = new Propiedad { DescripcionCorta = "h1 Tol", DescripcionLarga = "Width", Imagen = null, Nombre = "h1 Tol", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.00078 };
            //PerfilLateral.Propiedades.Add(h1);
            //PerfilLateral.Propiedades.Add(h1Tol);

            //Propiedad Thickness = new Propiedad { DescripcionCorta = "Thickness", DescripcionLarga = "Thickness", Imagen = null, Nombre = "a1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.131 };
            //Propiedad ThicknessMin = new Propiedad { DescripcionCorta = "Thickness Min", DescripcionLarga = "Thickness Min", Imagen = null, Nombre = "a1 Tol Min", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.005 };
            //Propiedad ThicknessMax = new Propiedad { DescripcionCorta = "Thickness Max", DescripcionLarga = "Thickness Max", Imagen = null, Nombre = "a1 Tol Max", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0.005 };

            //PerfilID.Propiedades.Add(Thickness);
            //PerfilID.Propiedades.Add(ThicknessMin);
            //PerfilID.Propiedades.Add(ThicknessMax);

            #endregion

            double espesorRadialMP = 0.0;
            double espesorAxialMP = 0.0;

            Anillo anilloProcesado = new Anillo();
            DescripcionGeneral = string.Format("{0:0.00000}", Module.ConvertTo(D1.TipoDato, D1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), D1.Valor)) + " X " + string.Format("{0:0.00000}", Module.ConvertTo(H1.TipoDato, H1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), H1.Valor)) + " " + TipoAnillo;
            MaterialBase.Especificacion = EspecificacionMaterialSeleccionada;

            //Agregamos los valores iniciales del anillo. Esto se utiliza para el calculo de tiempos estandar.
            anilloProcesado.PropiedadesAdquiridasProceso.Add(D1);
            anilloProcesado.PropiedadesAdquiridasProceso.Add(H1);
            anilloProcesado.PropiedadesCadenaAdquiridasProceso.Add(new PropiedadCadena { Nombre = "ESPEC_MATERIAL", Valor = MaterialBase.Especificacion });
            anilloProcesado.PropiedadesCadenaAdquiridasProceso.Add(new PropiedadCadena { Nombre = "TIPO_MATERIAL", Valor = MaterialBase.TipoDeMaterial });

            double currentWidth = 0.0;
            double currentThickness = 0.0;
            double currentDiameter = 0.0;
            int noOperacion = 10;

            string clasificacionProducto = GetClasificacionProducto();

            if (clasificacionProducto == "Gasolina")
            {
                if (MaterialBase.TipoDeMaterial == "HIERRO GRIS")
                {
                    //Ingresar calculo de placa modelo.
                    calcularMateriaPrima = new CalculaMateriaPrima(ModelAnillo);
                    MaterialBase = calcularMateriaPrima.CalcularPlacaModelo();

                    if (banCalcularOperaciones)
                        Operaciones = Router.CalcularHierroGris(ModelAnillo);

                    if (MaterialBase.Codigo.Equals("CODIFICAR"))
                    {
                        MetroDialogSettings setting = new MetroDialogSettings();
                        setting.AffirmativeButtonText = "SI";
                        setting.NegativeButtonText = "NO";
                        dialogService = new DialogService();

                        MessageDialogResult result = await dialogService.SendMessage("Atención", "No se encontró ninguna placa modelo para el componente ingresado ¿Desea generar una placa modelo nueva?", setting, MessageDialogStyle.AffirmativeAndNegative, "Desing Process");

                        switch (result)
                        {
                            case MessageDialogResult.Negative:
                                break;
                            case MessageDialogResult.Affirmative:
                                WPattern pattern = new WPattern();

                                Pattern nuevaPlaca = new Pattern();

                                nuevaPlaca.customer = cliente;

                                nuevaPlaca.diametro = D1;
                                nuevaPlaca.medida = H1;

                                nuevaPlaca.ring_th_max = Module.GetPropiedad("a1", PerfilID.Propiedades, "Max");
                                nuevaPlaca.ring_th_min = Module.GetPropiedad("a1", PerfilOD.Propiedades, "Min");

                                //Begin
                                double widthMin1 = Module.GetValorPropiedadMin("h1", PerfilLateral.Propiedades, true);
                                double widthMax1 = Module.GetValorPropiedadMax("h1", PerfilLateral.Propiedades, true);

                                Propiedad WidthMin = new Propiedad { Valor = widthMin1, DescripcionCorta = "WidthMin", DescripcionLarga = "WidthMin", Imagen = null, Nombre = "WidthMin", TipoDato = "Distance", Unidad = "Inch (in)" };
                                Propiedad WidthMax = new Propiedad { Valor = widthMax1, DescripcionCorta = "WidthMax", DescripcionLarga = "WidthMax", Imagen = null, Nombre = "WidthMax", TipoDato = "Distance", Unidad = "Inch (in)" };
                                //End

                                nuevaPlaca.ring_w_max = WidthMax;
                                nuevaPlaca.ring_w_min = WidthMin;

                                nuevaPlaca.piece_in_patt = new Propiedad { DescripcionCorta = "Piece", DescripcionLarga = "Piece", Imagen = null, Nombre = "Piece", TipoDato = "Distance", Unidad = "Inch (in)", Valor = calcularMateriaPrima.Piece };
                                nuevaPlaca.esp_inst = new PropiedadCadena { DescripcionCorta = "Especial Instruccions", DescripcionLarga = "Especial Instruccions", Imagen = null, Nombre = "EspecInst", Valor = Codigo + Environment.NewLine + "MATERIAL: " + ModelAnillo.MaterialBase.Especificacion };

                                //Falta agregar material
                                //nuevaPlaca.Material

                                nuevaPlaca.Hardness = new PropiedadCadena { DescripcionCorta = "Hardness", DescripcionLarga = "Hardness", Imagen = null, Nombre = "Hardness", Valor = EnumEx.GetEnumDescription(DataManager.UnidadDureza.RB) };
                                nuevaPlaca.HardnessMin = HardnessMin;
                                nuevaPlaca.HardnessMax = HardnessMax;

                                nuevaPlaca.turn_allow = new Propiedad { DescripcionCorta = "", DescripcionLarga = "", Imagen = null, Nombre = "", TipoDato = "", Unidad = "", Valor = calcularMateriaPrima.TS };
                                nuevaPlaca.bore_allow = new Propiedad { DescripcionCorta = "", DescripcionLarga = "", Imagen = null, Nombre = "", TipoDato = "", Unidad = "", Valor = calcularMateriaPrima.BS };
                                //DataManager.GetCamTurnConstant()
                                double factork = calcularMateriaPrima.CamTurnConstant * 0.0001;

                                nuevaPlaca.factor_k = new Propiedad { DescripcionCorta = "Factor K", DescripcionLarga = "Factor K", Imagen = null, Nombre = "FactorK", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), Valor = factork };
                                nuevaPlaca.EspecMaterialAnillo = MaterialBase.Especificacion;

                                nuevaPlaca.Proceso = Module.GetPropiedadCadena("Proceso", PerfilOD.PropiedadesCadena);
                                nuevaPlaca.TipoAnillo = new PropiedadCadena { DescripcionCorta = "", DescripcionLarga = "", Imagen = null, Nombre = "TipoAnillo", Valor = TipoAnillo };
                                nuevaPlaca.TipoMateriaPrima = new FO_Item { id = 1, IsSelected = false, Nombre = "TipoMaterial", ValorCadena = "GASOLINA" };

                                PatternViewModel vm = new PatternViewModel(nuevaPlaca, NombreUsuario);
                                pattern.DataContext = vm;
                                pattern.Show();

                                break;
                            case MessageDialogResult.FirstAuxiliary:
                                break;
                            case MessageDialogResult.SecondAuxiliary:
                                break;
                            default:
                                break;
                        }
                    }
                    anilloProcesado.PropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
                    anilloProcesado.PropiedadesBoolAdquiridasProceso = new ObservableCollection<PropiedadBool>();
                    anilloProcesado.PropiedadesCadenaAdquiridasProceso = new ObservableCollection<PropiedadCadena>();

                    anilloProcesado.PropiedadesAdquiridasProceso.Add(new Propiedad { TipoDato = "Distance", DescripcionCorta = "Piece", DescripcionLarga = "Piece", Imagen = null, Nombre = "Piece", Unidad = "Inch (in)", Valor = calcularMateriaPrima.Piece });

                    calcularDimenciones();
                }
                else
                {
                    if (ModelAnillo.MaterialBase.TipoDeMaterial == "ACERO AL CARBON")
                    {
                        //Obtenemos la ventana actual para mandar un mensaje de capturar la especificación del perfil
                        var window = System.Windows.Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                        //Formulario para ingresar el número de copias,
                        string _especPerfil = await window.ShowInputAsync(StringResources.lblAlertas, "Por favor ingrese la especificación del perfil del anillo.", null);

                        ModelAnillo.PropiedadesCadenaAdquiridasProceso = new ObservableCollection<PropiedadCadena>();
                        PropiedadCadena especPerfil = new PropiedadCadena { Nombre = "especPerfil", Valor = _especPerfil };
                        ModelAnillo.PropiedadesCadenaAdquiridasProceso.Add(especPerfil);

                        //Se define la ruta primero antes de que se calcule la materia prima.
                        if (banCalcularOperaciones)
                            Operaciones = Router.CalcularAceroRolado(ModelAnillo);

                        int c = 0;
                        foreach (var operacion in Operaciones)
                        {
                            if (operacion is IObserverWidth)
                                ((IObserverWidth)operacion).setMaterialRemover(Operaciones, c, ModelAnillo);

                            if (operacion is IObserverThickness)
                                ((IObserverThickness)operacion).setMaterialRemover(Operaciones, c);
                            c++;
                        }

                        calcularMateriaPrima = new CalculaMateriaPrima(ModelAnillo);

                        List<MateriaPrimaRolado> listaOpcionales = calcularMateriaPrima.CalcularAceroAlCarbon();
                        ListaIronRawMaterial.Clear();

                        foreach (var opcion in listaOpcionales)
                            ListaIronRawMaterial.Add(opcion);

                        //Mostramos la ventana para que el usuario seleccione la materia prima.
                        frmSelectIronRawMaterial wOpciones = new frmSelectIronRawMaterial();
                        wOpciones.DataContext = this;
                        wOpciones.ShowDialog();

                        //Si el usuario hizo clic en Aceptar continuamos con el calculo.
                        if (wOpciones.DialogResult.HasValue && wOpciones.DialogResult.Value)
                        {
                            if (ListaIronRawMaterial.Where(x => x.IsSelected).ToList().Count == 1)
                            {
                                MateriaPrimaRolado mpSeleccionada = ListaIronRawMaterial.Where(x => x.IsSelected).FirstOrDefault();
                                Operaciones[0].ListaMateriaPrima.Add(mpSeleccionada);

                                currentWidth = mpSeleccionada._Width;
                                currentThickness = mpSeleccionada.Thickness;
                                int nCortesWith = mpSeleccionada.nCortesWidth;

                                calcularDimenciones(nCortesWith);

                                MaterialBase = mpSeleccionada;
                            }
                            else
                            {
                                //Enviamos el mensaje que debe de serleccionar una materia prima
                                await dialogService.SendMessage("Atención", "Debe seleccionar una materia prima");

                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                if (clasificacionProducto == "Segmento")
                {
                    if (MaterialBase.TipoDeMaterial == "ACERO INOXIDABLE")
                    {
                        calcularMateriaPrima = new CalculaMateriaPrima(ModelAnillo);
                        ListaIronRailRawMaterial = calcularMateriaPrima.CalcularMateriaPrimaAceroSegmento();

                        frmSelectIronRailsRawMaterial wOpciones = new frmSelectIronRailsRawMaterial();
                        wOpciones.DataContext = this;
                        wOpciones.ShowDialog();

                        if (wOpciones.DialogResult.HasValue && wOpciones.DialogResult.Value)
                        {
                            if (ListaIronRailRawMaterial.Where(x => x.IsSelected).ToList().Count == 1)
                            {
                                MateriaPrimaAceros mpSeleccionada = new MateriaPrimaAceros();
                                mpSeleccionada = ListaIronRailRawMaterial.Where(x => x.IsSelected).FirstOrDefault();
                                mpSeleccionada.Propiedades.Add(new Propiedad { Nombre = "espesorAxialMP", Valor = mpSeleccionada.ESP_AXIAL, TipoDato = "Distance", Unidad = "Inch (in)" });
                                mpSeleccionada.Propiedades.Add(new Propiedad { Nombre = "espesorRadialMP", Valor = mpSeleccionada.ESP_RADIAL, TipoDato = "Distance", Unidad = "Inch (in)" });
                                MaterialBase = mpSeleccionada;
                                espesorAxialMP = mpSeleccionada.ESP_AXIAL;
                                espesorRadialMP = mpSeleccionada.ESP_RADIAL;
                            }
                        }

                        if (banCalcularOperaciones)
                            Operaciones = Router.CalcularAceroSegmentosPVD(ModelAnillo);
                    }
                }
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
            anilloProcesado.FreeGap = ModelAnillo.FreeGap;

            #region Cálculo de Operaciones
            bool ban = true;
            Anillo aProcesado = new Anillo();

            dialogService = new DialogService();
            var Controller = await dialogService.SendProgressAsync(Resources.StringResources.ttlEspereUnMomento, string.Empty);

            int totalOperaciones = Operaciones.Count;
            int count = 0;

            foreach (IOperacion element in Operaciones)
            {
                element.NoOperacion = noOperacion;
                bool IsMaking = false;
                if (element is IObserverWidth)
                {
                    IObserverWidth auxWidth = (IObserverWidth)element;
                    currentWidth = auxWidth.WidthOperacion;
                    IsMaking = true;
                }

                if (element is IObserverDiametro)
                {
                    IObserverDiametro auxDiametro = (IObserverDiametro)element;
                    currentDiameter = auxDiametro.Diameter;
                    IsMaking = true;
                }

                if (element is IObserverThickness)
                {
                    IObserverThickness auxThickness = (IObserverThickness)element;
                    currentThickness = auxThickness.Thickness;
                    IsMaking = true;
                }

                string mensaje = "                                                                                                                    " + noOperacion / 10 + "/" + Operaciones.Count + Environment.NewLine +
                    Resources.StringResources.msgDoingOperation + " " + noOperacion + "    " + element.NombreOperacion +
                                Environment.NewLine + Environment.NewLine + Resources.StringResources.lblWidth + ": " + currentWidth +
                                "    " + Resources.StringResources.lblThickness + ": " + currentThickness +
                                "    " + Resources.StringResources.lblDiameter + ": " + currentDiameter;

                Controller.SetMessage(mensaje);

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

                if (IsMaking)
                    await Task.Delay(500);
                else
                    await Task.Delay(500);

                noOperacion += 10;
                count += count;
            }
            #endregion

            #region Definición de Carátula
            ModelAnillo.Caratula = DescripcionGeneral + Environment.NewLine;
            ModelAnillo.Caratula += "FECHA REV.       " + Module.GetDate() + Environment.NewLine;

            if (clasificacionProducto == "Segmento")
            {
                #region Carátula para los segmentos
                Propiedad h11Min = Module.GetPropiedad("h11 Min", PerfilLateral.Propiedades);
                Propiedad h11Max = Module.GetPropiedad("h11 Max", PerfilLateral.Propiedades);
                Propiedad a1Min = Module.GetPropiedad("a1 Min", PerfilID.Propiedades);
                Propiedad a1Max = Module.GetPropiedad("a1 Max", PerfilID.Propiedades);

                h11Min.Valor = Module.ConvertTo(h11Min.TipoDato, h11Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), h11Min.Valor);
                h11Min.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                h11Max.Valor = Module.ConvertTo(h11Max.TipoDato, h11Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), h11Max.Valor);
                h11Max.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                a1Min.Valor = Module.ConvertTo(a1Min.TipoDato, a1Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), a1Min.Valor);
                a1Min.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                a1Max.Valor = Module.ConvertTo(a1Max.TipoDato, a1Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), a1Max.Valor);
                a1Max.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                ModelAnillo.Caratula += "ESP. AXIAL       " + h11Min.Valor + " - " + h11Max.Valor + "" + Environment.NewLine; //  <-- Width
                ModelAnillo.Caratula += "ESP RADIAL       " + a1Min.Valor + " - " + a1Max.Valor + "" + Environment.NewLine; // <-- Thickness

                Propiedad s1Min = Module.GetPropiedad("s1 Min", PerfilPuntas.Propiedades);
                Propiedad s1Max = Module.GetPropiedad("s1 Max", PerfilPuntas.Propiedades);
                Propiedad freeGapMin = Module.GetPropiedad("freeGapMin", PerfilPuntas.Propiedades);
                Propiedad freeGapMax = Module.GetPropiedad("freeGapMax", PerfilPuntas.Propiedades);

                s1Min.Valor = Module.ConvertTo(s1Min.TipoDato, s1Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), s1Min.Valor);
                s1Min.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                s1Max.Valor = Module.ConvertTo(s1Max.TipoDato, s1Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), s1Max.Valor);
                s1Max.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                freeGapMin.Valor = Module.ConvertTo(freeGapMin.TipoDato, freeGapMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), freeGapMin.Valor);
                freeGapMin.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                freeGapMax.Valor = Module.ConvertTo(freeGapMax.TipoDato, freeGapMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), freeGapMax.Valor);
                freeGapMax.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);


                ModelAnillo.Caratula += "ABERT.TRAB.      " + s1Min.Valor + " - " + s1Max.Valor + "" + Environment.NewLine;
                ModelAnillo.Caratula += "ABERT.LIBRE      " + freeGapMin.Valor + " - " + freeGapMax.Valor + "" + Environment.NewLine;
                ModelAnillo.Caratula += "MATERIAL         " + MaterialBase.Especificacion + Environment.NewLine;
                ModelAnillo.Caratula += "CHAFLAN DIA. EXT 0.002 - 0.0078" + Environment.NewLine;

                if (Module.HasPropiedadOptional("ESPEC_NITRURADO", PerfilOD.PropiedadesOpcionales))
                {
                    PropiedadOptional especNitrurado = PerfilOD.PropiedadesOpcionales.Where(o => o.lblTitle == "ESPEC_NITRURADO").FirstOrDefault();
                    DO_DataGasNitridingRails dataNitrurado = DataManager.GetDataGasNitriding(especNitrurado.ElementSelected.ValorCadena);
                    ModelAnillo.Caratula += "NITRURADO        " + dataNitrurado.ThicknessMin + " - " + dataNitrurado.ThicknessMax + Environment.NewLine;
                }

                if (Module.HasPropiedadOptional("ESPEC_PVD", PerfilOD.PropiedadesOpcionales))
                {
                    PropiedadOptional propiedadEspec = PerfilOD.PropiedadesOpcionales.Where(o => o.lblTitle == "ESPEC_PVD").FirstOrDefault();
                    DO_DataPVD dataPVD = DataManager.GetDataPVD(propiedadEspec.ElementSelected.ValorCadena);
                    ModelAnillo.Caratula += "PVD MIP 230" + "       " + dataPVD.ThicknessMin + " - " + dataPVD.ThicknessMax + Environment.NewLine;
                }

                ModelAnillo.Caratula += "NOTA:" + Environment.NewLine;
                ModelAnillo.Caratula += "DIMENSION PARA REGIÓN INTERNA 0.0152 - 0.0169" + Environment.NewLine;
                ModelAnillo.Caratula += "" + Environment.NewLine;
                ModelAnillo.Caratula += "MAT. PRIMA" + Environment.NewLine;
                ModelAnillo.Caratula += "PROV.HITACHI" + Environment.NewLine;
                ModelAnillo.Caratula += "DIM ALAMBRE  " + espesorRadialMP + " X " + espesorAxialMP + Environment.NewLine;
                ModelAnillo.Caratula += "ACERO 10% CROMO" + Environment.NewLine;
                ModelAnillo.Caratula += "" + Environment.NewLine;

                #endregion

            }else if(clasificacionProducto == "Gasolina" && MaterialBase.TipoDeMaterial == "ACERO AL CARBON")
            {
                Propiedad h1Min = Module.GetPropiedad("h1 Min", PerfilLateral.Propiedades);
                h1Min.Valor = Module.ConvertTo(h1Min.TipoDato, h1Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), h1Min.Valor);
                h1Min.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                Propiedad h1Max = Module.GetPropiedad("h1 Max", PerfilLateral.Propiedades);
                h1Max.Valor = Module.ConvertTo(h1Max.TipoDato, h1Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), h1Max.Valor);
                h1Max.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                Propiedad a1Min = Module.GetPropiedad("a1 Min", PerfilID.Propiedades);
                a1Min.Valor = Module.ConvertTo(a1Min.TipoDato, a1Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), a1Min.Valor);
                a1Min.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                Propiedad a1Max = Module.GetPropiedad("a1 Max", PerfilID.Propiedades);
                a1Max.Valor = Module.ConvertTo(a1Max.TipoDato, a1Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), a1Max.Valor);
                a1Max.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                Propiedad s1Min = Module.GetPropiedad("s1 Min", PerfilPuntas.Propiedades);
                s1Min.Valor = Module.ConvertTo(s1Min.TipoDato, s1Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), s1Min.Valor);
                s1Min.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                Propiedad s1Max = Module.GetPropiedad("s1 Max", PerfilPuntas.Propiedades);
                s1Max.Valor = Module.ConvertTo(s1Max.TipoDato, s1Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), s1Max.Valor);
                s1Max.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

                ModelAnillo.Caratula += "WIDTH      " + h1Min.Valor + " - " + h1Max.Valor + "" + Environment.NewLine;
                ModelAnillo.Caratula += "THICKNESS  " + a1Min.Valor + "  - " + a1Max.Valor + "" + Environment.NewLine;
                ModelAnillo.Caratula += "TENSION    " + ModelAnillo.Tension.Valor + "   +- " + ModelAnillo.TensionTol.Valor + " " + ModelAnillo.Tension.Unidad + " FT" + Environment.NewLine;
                ModelAnillo.Caratula += "WKG. GAP   " + s1Min.Valor + "   - " + s1Max.Valor + "" + Environment.NewLine;
                ModelAnillo.Caratula += "MATERIAL   " + MaterialBase.Especificacion + "" + Environment.NewLine;
                ModelAnillo.Caratula += "HARDNESS   " + ModelAnillo.HardnessMax.Unidad + " " + ModelAnillo.HardnessMin.Valor + " - " + ModelAnillo.HardnessMax.Valor + "" + Environment.NewLine;
                ModelAnillo.Caratula += "COATING    " + ModelAnillo.Treatment + " " + ModelAnillo.EspecTreatment + Environment.NewLine;
                ModelAnillo.Caratula += "PC.        " + Environment.NewLine;
                ModelAnillo.Caratula += "FOUNDRY" + Environment.NewLine;
                ModelAnillo.Caratula += "" + Environment.NewLine;
                ModelAnillo.Caratula += "WIRE    " + ((MateriaPrimaRolado)MaterialBase)._Width + " X " + ((MateriaPrimaRolado)MaterialBase).Thickness + " S= " + ((MateriaPrimaRolado)MaterialBase).Groove + "" + Environment.NewLine;
                ModelAnillo.Caratula += "CSTG. SIZE" + Environment.NewLine;
                ModelAnillo.Caratula += "WTH" + Environment.NewLine;
                ModelAnillo.Caratula += "NICK" + Environment.NewLine;
                ModelAnillo.Caratula += "T.S." + Environment.NewLine;
                ModelAnillo.Caratula += "B.S." + Environment.NewLine;
                ModelAnillo.Caratula += "INV." + Environment.NewLine;
                ModelAnillo.Caratula += "" + Environment.NewLine;
            }

            ModelAnillo.Caratula += "*IDENTIFICACION" + Environment.NewLine;
            ModelAnillo.Caratula += "CLIENTE: " + cliente.NombreCliente + Environment.NewLine;
            ModelAnillo.Caratula += "PTE. CLTE. " + CustomerPartNumber + " REV. " + CustomerRevisionLevel + "" + Environment.NewLine;

            if (clasificacionProducto != "Segmento")
            {
                ModelAnillo.Caratula += "MEDIDA " + Module.ConvertTo(D1.TipoDato, D1.Unidad, EnumEx.GetEnumDescription(UnidadDistance.Inch), D1.Valor) + " X " + Module.ConvertTo(H1.TipoDato, H1.Unidad, EnumEx.GetEnumDescription(UnidadDistance.Inch), H1.Valor) + Environment.NewLine;
            }
            ModelAnillo.Caratula += "" + Environment.NewLine;
            ModelAnillo.Caratula += "REVISADO " + User.Nombre + " " + User.ApellidoPaterno + Environment.NewLine;
            ModelAnillo.Caratula += "        " + Module.GetDate() + Environment.NewLine;
            ModelAnillo.Caratula += "" + Environment.NewLine;
            ModelAnillo.Caratula += "NOTAS:" + Environment.NewLine;
            #endregion

            await Controller.CloseAsync();
            await dialogService.SendMessage(Resources.StringResources.ttlDone, Resources.StringResources.msgRoutingReady);

        }

        /// <summary>
        /// Método que retorna la clasificación de anillo que es. La clasificación puede ser: "Gasolina", "Segmento", "Expansor", "Large Bore."
        /// </summary>
        /// <returns></returns>
        private string GetClasificacionProducto()
        {
            if (ModelAnillo.TipoAnillo == "THM17" || ModelAnillo.TipoAnillo == "THM13")
                return "Gasolina";

            if (ModelAnillo.TipoAnillo == "MD52SNCV")
                return "Segmento";

            return "No disponible";

        }

        private void calcularDimenciones(int nCortesWith)
        {
            int nPasosNISSEI = Operaciones.Where(x => x.NombreOperacion == "FINISH GRIND (NISSEI)").ToList().Count;

            int[] cortesXPaso = Module.GetCortesByPaso(nCortesWith, nPasosNISSEI);

            double sum = 0.0;
            foreach (int totalCortes in cortesXPaso)
            {
                double t = totalCortes * .0005;
                sum += t;
            }

            PasoNISSEI[] pasosNISSEI = new PasoNISSEI[nPasosNISSEI];

            for (int j = 0; j < pasosNISSEI.Length; j++)
            {
                pasosNISSEI[j] = new PasoNISSEI();
                pasosNISSEI[j].NumPaso = j + 1;
                pasosNISSEI[j].Cortes = new CorteNISSEI[cortesXPaso[j]];
                for (int k = 0; k < pasosNISSEI[j].Cortes.Length ; k++)
                {
                    pasosNISSEI[j].Cortes[k] = new CorteNISSEI();
                }
            }

            #region Relgas (Constantes)
            /*Reglas:
            1.- Primer corte de primer paso siempre debe ser 0.0008 .
            2.- Todos los cortes del último paso deben ser 0.0004 .
            3.- Todos los últimos cortes de cada paso debe ser 0.0004 .
            4.- Todos los primeros cortes de pasos intermedios deben ser 0.0006
            Pasos intermedio : Pasos que NO SON el primer paso ni el último paso.
            */

            //Preparamos Regla #1
            pasosNISSEI[0].Cortes[0].MatRemover = .0008;
            pasosNISSEI[0].Cortes[0].WasAssigned = true;

            //Preparamos Regla #2
            for (int j = 0; j < pasosNISSEI[nPasosNISSEI - 1].Cortes.Length; j++)
            {
                pasosNISSEI[nPasosNISSEI - 1].Cortes[j].MatRemover = 0.0004;
                pasosNISSEI[nPasosNISSEI - 1].Cortes[j].WasAssigned = true;
            }

            //Preparamos Regla #3
            for (int j = 0; j < pasosNISSEI.Length; j++)
            {
                pasosNISSEI[j].Cortes[pasosNISSEI[j].Cortes.Length - 1].MatRemover = .0004;
                pasosNISSEI[j].Cortes[pasosNISSEI[j].Cortes.Length - 1].WasAssigned = true;
            }

            //Preparamos Regla #4
            for (int j = 1; j < pasosNISSEI.Length - 1; j++)
            {
                pasosNISSEI[j].Cortes[0].MatRemover = .0006;
                pasosNISSEI[j].Cortes[0].WasAssigned = true;
            }
            #endregion

            //Asignamos el número de corte a todos los pasos.
            for (int j = 0; j < pasosNISSEI.Length; j++)
            {
                for (int k = 0; k < pasosNISSEI[j].Cortes.Length; k++)
                {
                    pasosNISSEI[j].Cortes[k].NumCorte = k + 1;
                }
            }

            //Contamos el material a remover que llevamos hasta el momento. (Se lleva hasta el momento solo las cantidades constantes.)
            double sumConstante = 0.0;
            for (int j = 0; j < pasosNISSEI.Length; j++)
            {
                for (int k = 0; k < pasosNISSEI[j].Cortes.Length; k++)
                {
                    sumConstante += pasosNISSEI[j].Cortes[k].MatRemover;
                }
            }

            double faltante = sum - sumConstante;

            int num0006 = Convert.ToInt32(faltante / 0.0006);

            double v = faltante - (num0006 * .0006);
            if (v < .0004)
                num0006--;

            v = faltante -  (num0006 * .0006);

            int num0004 = Convert.ToInt32(v / .0006);

            if (num0006 > 0)
            {
                for (int j = 0; j < pasosNISSEI.Length; j++)
                {
                    for (int k = 0; k < pasosNISSEI[j].Cortes.Length; k++)
                    {
                        if (!pasosNISSEI[j].Cortes[k].WasAssigned && num0006 > 0)
                        {
                            pasosNISSEI[j].Cortes[k].MatRemover = 0.0006;
                            pasosNISSEI[j].Cortes[k].WasAssigned = true;
                            num0006--;
                        }
                    }
                }
            }

            if (num0004 > 0)
            {
                for (int j = 0; j < pasosNISSEI.Length; j++)
                {
                    for (int k = 0; k < pasosNISSEI[j].Cortes.Length; k++)
                    {
                        if (!pasosNISSEI[j].Cortes[k].WasAssigned && num0004 > 0)
                        {
                            pasosNISSEI[j].Cortes[k].MatRemover = 0.0004;
                            pasosNISSEI[j].Cortes[k].WasAssigned = true;
                            num0004--;
                        }
                    }
                }
            }

            #region Calculo de width
            int i = Operaciones.Count - 1;
            int c = 0;
            double widthMin = Module.GetValorPropiedadMin("h1", PerfilLateral.Propiedades, true);
            double widthMax = Module.GetValorPropiedadMax("h1", PerfilLateral.Propiedades, true);
            double widthFinal = (widthMin + widthMax) / 2;

            SubjectWidth subjectWidth = new SubjectWidth();
            bool banUltimaOperacionWidth = true;

            //Contador de operaciones NISSEI
            int cNissei = cortesXPaso.Length - 1;

            while (i >= 0)
            {
                if (Operaciones[i] is IObserverWidth)
                {
                    if (banUltimaOperacionWidth)
                    {
                        IObserverWidth ope = (Operaciones[i] as IObserverWidth);
                        if (Operaciones[i].NombreOperacion == "FINISH GRIND (NISSEI)")
                        {
                            ope.CortesOPasadas = cortesXPaso[cNissei];
                            ope.PasoNISSEI = pasosNISSEI[cNissei];
                            ope.MatRemoverWidth = GetMatRemoverPASONissei(pasosNISSEI[cNissei]);
                            cNissei--;
                        }

                        subjectWidth.Subscribe(ope, widthFinal);
                        banUltimaOperacionWidth = false;
                    }
                    else
                    {
                        IObserverWidth ope = (Operaciones[i] as IObserverWidth);
                        if (Operaciones[i].NombreOperacion == "FINISH GRIND (NISSEI)")
                        {
                            ope.CortesOPasadas = cortesXPaso[cNissei];
                            ope.PasoNISSEI = pasosNISSEI[cNissei];
                            ope.MatRemoverWidth = GetMatRemoverPASONissei(pasosNISSEI[cNissei]);
                            cNissei--;
                        }
                        subjectWidth.Subscribe(ope);
                        subjectWidth.Notify(c);
                    }
                    c += 1;
                }
                i = i - 1;
            }
            #endregion

            #region Calculo de Diámetro
            i = Operaciones.Count - 1;
            c = 0;
            SubjectDiametro subjectDiametro = new SubjectDiametro();
            bool banUltimaOperacionDiametro = true;

            double gapMinimo = Module.GetValorPropiedadMin("s1", PerfilPuntas.Propiedades, true);
            double gapMaximo = Module.GetValorPropiedadMax("s1", PerfilPuntas.Propiedades, true);
            double mediaGap = Math.Round((gapMinimo + gapMaximo) / 2, 3);

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
                        subjectDiametro.Subscribe(operacion);
                        subjectDiametro.Notify(c);
                    }
                    c += 1;
                }
                i = i - 1;
            }
            #endregion

            #region Calculo de thickness

            i = Operaciones.Count - 1;
            c = 0;

            double thicknessMin = Module.GetValorPropiedadMin("a1", PerfilID.Propiedades, true);
            double thicknessMax = Module.GetValorPropiedadMax("a1", PerfilID.Propiedades, true);

            double mediaThickness = Math.Round((thicknessMin + thicknessMax) / 2, 5);

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
            #endregion
        }

        private double GetMatRemoverPASONissei(PasoNISSEI paso)
        {
            double matRemover = 0.0;
            for (int i = 0; i < paso.Cortes.Length; i++)
            {
                matRemover += paso.Cortes[i].MatRemover;
            }

            return matRemover;
        }

        private void calcularDimenciones()
        {
            #region Calculo de width
            int i = Operaciones.Count - 1;
            int c = 0;
            double widthMin = Module.GetValorPropiedadMin("h1", PerfilLateral.Propiedades, true);
            double widthMax = Module.GetValorPropiedadMax("h1", PerfilLateral.Propiedades, true);
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
            #endregion

            #region Calculo de Diámetro

            i = Operaciones.Count - 1;
            c = 0;
            SubjectDiametro subjectDiametro = new SubjectDiametro();
            bool banUltimaOperacionDiametro = true;

            double gapMinimo = Module.GetValorPropiedadMin("s1", PerfilPuntas.Propiedades, true);
            double gapMaximo = Module.GetValorPropiedadMax("s1", PerfilPuntas.Propiedades, true);
            double mediaGap = Math.Round((gapMinimo + gapMaximo)/2, 3);

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
                        subjectDiametro.Subscribe(operacion);
                        subjectDiametro.Notify(c);
                    }
                    c += 1;
                }
                i = i - 1;
            }
            #endregion

            #region Calculo de thickness

            i = Operaciones.Count - 1;
            c = 0;

            double thicknessMin = Module.GetValorPropiedadMin("a1", PerfilID.Propiedades, true);
            double thicknessMax = Module.GetValorPropiedadMax("a1", PerfilID.Propiedades, true);

            double mediaThickness = Math.Round((thicknessMin + thicknessMax) / 2, 5);

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
            #endregion
        }

        private void abrirPlano()
        {

        }

        /// <summary>
        /// Método que inicializa todas las propiedades para generar un nuevo plano.
        /// </summary>
        /// <param name="banUnidadDefinida">Si es True se muestra un mensaje en la pantalla para preguntar que unidad desea, si es false por defautl la unidad sera Inch.</param>
        private async void newPlano(bool banUnidadDefinida)
        {
            //Inicializamos los servicios de dialog.
            DialogService dialogService = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendrá el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblMilimetros;
            setting.NegativeButtonText = StringResources.lblPulgadas;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo guardamos en una variable local.
            MessageDialogResult result;

            result = banUnidadDefinida ? await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgSeleccionaFormato, setting, MessageDialogStyle.AffirmativeAndNegative) : MessageDialogResult.Negative;

            //Para cada resultado realizamos una acción.
            switch (result)
            {
                case MessageDialogResult.Negative:

                    //Inicializamos el nuevo modelo.
                    ModelAnillo = new Anillo();

                    //Establecemos a todas las propiedades del modelo anillo los valores por default.

                    SetUnidadesDefault(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch),
                                        EnumEx.GetEnumDescription(DataManager.TipoDato.Force), EnumEx.GetEnumDescription(DataManager.UnidadForce.LBS),
                                        EnumEx.GetEnumDescription(DataManager.TipoDato.Dureza), EnumEx.GetEnumDescription(DataManager.UnidadDureza.RB),
                                        EnumEx.GetEnumDescription(DataManager.TipoDato.Mass), EnumEx.GetEnumDescription(DataManager.UnidadMass.Gram));

                    break;
                case MessageDialogResult.Affirmative:

                    //Inicializamos el nuevo modelo.
                    ModelAnillo = new Anillo();

                    //Establecemos a todas las propiedades del modelo anillo los valores por default.
                    SetUnidadesDefault(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), EnumEx.GetEnumDescription(DataManager.UnidadDistance.Milimeter),
                                        EnumEx.GetEnumDescription(DataManager.TipoDato.Force), EnumEx.GetEnumDescription(DataManager.UnidadForce.LBS),
                                        EnumEx.GetEnumDescription(DataManager.TipoDato.Dureza), EnumEx.GetEnumDescription(DataManager.UnidadDureza.RB),
                                        EnumEx.GetEnumDescription(DataManager.TipoDato.Mass), EnumEx.GetEnumDescription(DataManager.UnidadMass.Gram));

                    break;
                case MessageDialogResult.FirstAuxiliary:
                    break;
                case MessageDialogResult.SecondAuxiliary:
                    break;
                default:
                    break;
            }
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

                            //Enviamos un mensaje para informar que se importo el plano correctamente.
                            await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgCargaPlano + Codigo);
                        }
                        else
                        {
                            //En caso de que el archivo no contenga elementos, enviamos un mensaje de alerta.
                            await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgPlanoIncorrecto);
                        }
                    }
                    else
                    {
                        //En caso de que el usuario no seleccione un archivo .xml, enviamos un mensaje indicando formato no soportado.
                        await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgPlanoIncorrecto);
                    }
                }
                else
                {
                    //En caso de que no exista el archivo, enviamos un mensaje de alerta.
                    await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgPlanoIncorrecto);
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

        private ObservableCollection<StackPanel> SetOptionalEntryToStackPanel(ObservableCollection<OptionalEntry> CollectionText, ObservableCollection<PropiedadOptional> propiedadesOpcionales)
        {
            ObservableCollection<StackPanel> CollectionPanel = new ObservableCollection<StackPanel>();

            if (CollectionText.Count == propiedadesOpcionales.Count)
            {
                int c = 0;

                while (c < CollectionText.Count)
                {
                    StackPanel panel = new StackPanel();
                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    panel.Children.Add(CollectionText[c]);

                    CollectionPanel.Add(panel);

                    c += 1;
                }
            }

            return CollectionPanel;
        }

        private ObservableCollection<StackPanel> SetStringEntryToStackPanel(ObservableCollection<StringEntry> CollectionText, ObservableCollection<PropiedadCadena> CollectionPropiedadesCadena)
        {
            ObservableCollection<StackPanel> CollectionPanel = new ObservableCollection<StackPanel>();

            if (CollectionText.Count == CollectionPropiedadesCadena.Count)
            {
                int c = 0;

                while (c < CollectionText.Count)
                {
                    StackPanel panel = new StackPanel();

                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    panel.Children.Add(CollectionText[c]);

                    CollectionPanel.Add(panel);

                    c += 1;
                }
            }

            return CollectionPanel;
        }

        private ObservableCollection<StackPanel> SetBoolEntryToStackPanel(ObservableCollection<BoolEntry> CollectionText, ObservableCollection<PropiedadBool> CollectionPropiedadesBool)
        {
            ObservableCollection<StackPanel> CollectionPanel = new ObservableCollection<StackPanel>();

            if (CollectionText.Count == CollectionPropiedadesBool.Count)
            {
                int c = 0;

                while (c < CollectionText.Count)
                {
                    StackPanel panel = new StackPanel();

                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    panel.Children.Add(CollectionText[c]);

                    CollectionPanel.Add(panel);

                    c += 1;
                }
            }

            return CollectionPanel;
        }

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
                    StackPanel panel = new StackPanel();
                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;

                    panel.Children.Add(CollectionNumeric[c]);

                    CollectionPanel.Add(panel);

                    c += 1;
                }
            }

            //Retornamos la colección creada.
            return CollectionPanel;
        }

        /// <summary>
        /// Método que abre un plano guardado en la base de datos.
        /// </summary>
        private async void openPlano()
        {
            ListaComponentes =  DataManager.GetAllArquetipo("");

            WSelectComponent wSelectComponente = new WSelectComponent();
            wSelectComponente.DataContext = this;
            wSelectComponente.ShowDialog();

            if (wSelectComponente.DialogResult.HasValue && wSelectComponente.DialogResult.Value)
            {
                if (ComponenteSeleccionado != null)
                {
                    dialogService = new DialogService();
                    var Controller = await dialogService.SendProgressAsync(Resources.StringResources.ttlEspereUnMomento, "Estoy realizando su petición...");

                    string codigo = ComponenteSeleccionado.Codigo;

                    ModelAnillo = DataManager.GetAnillo(codigo);

                    int[] vecPerfiles = DataManager.GetPerfilByComponente(codigo);

                    int c = 0;
                    while (c < vecPerfiles.Length)
                    {
                        Perfil miPerfil = DataManager.GetPerfilByID(vecPerfiles[c]);

                        switch (miPerfil.TipoPerfil)
                        {
                            case "PERFIL O.D.":
                                PerfilOD = miPerfil;
                                NotifyChange("PerfilOD");
                                break;
                            case "PERFIL I.D.":
                                PerfilID = miPerfil;
                                NotifyChange("PerfilID");
                                break;
                            case "PERFIL CARAS LATERALES":
                                PerfilLateral = miPerfil;
                                NotifyChange("PerfilLateral");
                                break;
                            case "PERFIL PUNTAS":
                                PerfilPuntas = miPerfil;
                                NotifyChange("PerfilPuntas");
                                break;

                            default:
                                break;
                        }
                        c += 1;
                    }

                    ObservableCollection<Propiedad> ListaTotalesPropiedades = DataManager.GetPropiedadSaved(Codigo);
                    ObservableCollection<PropiedadCadena> ListaTotalesPropiedadesCadena = DataManager.GetPropiedadCadenaSaved(Codigo);
                    ObservableCollection<PropiedadBool> ListaTotalesPropiedadesBool = DataManager.GetPropiedadBoolSaved(Codigo);
                    ObservableCollection<PropiedadOptional> ListaTotalesPropiedadesOpcionales = DataManager.GetPropiedadOptionalSaved(Codigo);

                    PerfilOD.Propiedades = Module.ConvertListToObservableCollectionPropiedad(ListaTotalesPropiedades.Where(x => x.TipoPerfil == "PERFIL O.D.").ToList());
                    PerfilOD.PropiedadesCadena = Module.ConvertListToObservableCollectionPropiedadCadena(ListaTotalesPropiedadesCadena.Where(x => x.TipoPerfil == "PERFIL O.D.").ToList());
                    PerfilOD.PropiedadesBool = Module.ConvertListToObservableCollectionPropiedadBool(ListaTotalesPropiedadesBool.Where(x => x.TipoPerfil == "PERFIL O.D.").ToList());
                    PerfilOD.PropiedadesOpcionales = Module.ConvertListToObservableCollectionPropiedadOptional(ListaTotalesPropiedadesOpcionales.Where(x => x.TipoPerfil == "PERFIL O.D.").ToList());

                    PerfilID.Propiedades = Module.ConvertListToObservableCollectionPropiedad(ListaTotalesPropiedades.Where(x => x.TipoPerfil == "PERFIL I.D.").ToList());
                    PerfilID.PropiedadesCadena = Module.ConvertListToObservableCollectionPropiedadCadena(ListaTotalesPropiedadesCadena.Where(x => x.TipoPerfil == "PERFIL I.D.").ToList());
                    PerfilID.PropiedadesBool = Module.ConvertListToObservableCollectionPropiedadBool(ListaTotalesPropiedadesBool.Where(x => x.TipoPerfil == "PERFIL I.D.").ToList());
                    PerfilID.PropiedadesOpcionales = Module.ConvertListToObservableCollectionPropiedadOptional(ListaTotalesPropiedadesOpcionales.Where(x => x.TipoPerfil == "PERFIL I.D.").ToList());

                    PerfilLateral.Propiedades = Module.ConvertListToObservableCollectionPropiedad(ListaTotalesPropiedades.Where(x => x.TipoPerfil == "PERFIL CARAS LATERALES").ToList());
                    PerfilLateral.PropiedadesCadena = Module.ConvertListToObservableCollectionPropiedadCadena(ListaTotalesPropiedadesCadena.Where(x => x.TipoPerfil == "PERFIL CARAS LATERALES").ToList());
                    PerfilLateral.PropiedadesBool = Module.ConvertListToObservableCollectionPropiedadBool(ListaTotalesPropiedadesBool.Where(x => x.TipoPerfil == "PERFIL CARAS LATERALES").ToList());
                    PerfilLateral.PropiedadesOpcionales = Module.ConvertListToObservableCollectionPropiedadOptional(ListaTotalesPropiedadesOpcionales.Where(x => x.TipoPerfil == "PERFIL CARAS LATERALES").ToList());

                    PerfilPuntas.Propiedades = Module.ConvertListToObservableCollectionPropiedad(ListaTotalesPropiedades.Where(x => x.TipoPerfil == "PERFIL PUNTAS").ToList());
                    PerfilPuntas.PropiedadesCadena = Module.ConvertListToObservableCollectionPropiedadCadena(ListaTotalesPropiedadesCadena.Where(x => x.TipoPerfil == "PERFIL PUNTAS").ToList());
                    PerfilPuntas.PropiedadesBool = Module.ConvertListToObservableCollectionPropiedadBool(ListaTotalesPropiedadesBool.Where(x => x.TipoPerfil == "PERFIL PUNTAS").ToList());
                    PerfilPuntas.PropiedadesOpcionales = Module.ConvertListToObservableCollectionPropiedadOptional(ListaTotalesPropiedadesOpcionales.Where(x => x.TipoPerfil == "PERFIL PUNTAS").ToList());

                    NotifyChange("Codigo");
                    NotifyChange("PerfilOD");
                    NotifyChange("PerfilID");
                    NotifyChange("PerfilLateral");
                    NotifyChange("PerfilPuntas");

                    ArquetipoRing arquetipoRing = DataManager.GetArquetipoRing(Codigo);

                    D1 = arquetipoRing.D1;
                    H1 = arquetipoRing.H1;
                    FreeGap = arquetipoRing.FreeGap;
                    Mass = arquetipoRing.Mass;
                    Tension = arquetipoRing.Tension;
                    TensionTol = arquetipoRing.TensionTol;
                    NoPlano = arquetipoRing.NoPlano;
                    CustomerPartNumber = arquetipoRing.CustomerPartNumber;
                    CustomerRevisionLevel = arquetipoRing.CustomerRevisionLevel;
                    Size = arquetipoRing.Size1;
                    TipoAnillo = arquetipoRing.TipoAnillo;
                    CustomerDocNo = arquetipoRing.CustomerDocNo;
                    Treatment = arquetipoRing.Treatment;
                    EspecTreatment = arquetipoRing.EspecTreatment;
                    HardnessMax = arquetipoRing.HardnessMax;
                    HardnessMin = arquetipoRing.HardnessMin;
                    EspecificacionMaterialSeleccionada = arquetipoRing.EspecMaterialBase;
                    OvalityMax = arquetipoRing.OvalityMax;
                    OvalityMin = arquetipoRing.OvalityMin;

                    createNumericEntry();
                    createTextEntry();
                    createBoolEntry();
                    createOptionalEntry();

                    ListaNormas = DataManager.GetNormaByArquetipo(Codigo);

                    await Controller.CloseAsync();
                    await dialogService.SendMessage(Resources.StringResources.ttlDone, "El componente está listo.");

                    ////-------------------Perfil OD-------------------
                    //PerfilOD.Propiedades = new ObservableCollection<Propiedad>();
                    //PerfilOD.Propiedades.Add(new Propiedad { Nombre = "S1 MIN", DescripcionCorta = "S1 MIN", DescripcionLarga = "DIÁMETRO NOMINAL DEL ANILLO", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });

                    //PerfilOD.Propiedades = SortObservableCollectionPropiedad(PerfilOD.Propiedades);

                    //PropiedadesOD.Clear();

                    //foreach (var item in PerfilOD.Propiedades)
                    //{
                    //    NumericEntry uc = new NumericEntry();
                    //    PropiedadViewModel mvm = new PropiedadViewModel(item);
                    //    uc.DataContext = mvm;
                    //    PropiedadesOD.Add(uc);
                    //}

                    //PanelPropiedadesOD = SetNumericEntryToStackPanel(PropiedadesOD, PerfilOD.Propiedades);
                    ////-------------------Perfil OD-------------------

                    ////-------------------Perfil Puntas-------------------
                    //PerfilPuntas.Propiedades = new ObservableCollection<Propiedad>();
                    //PerfilPuntas.Propiedades.Add(new Propiedad { Nombre = "Q1 MIN", DescripcionCorta = "Q1 MIN", DescripcionLarga = "Q1 MIN", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });
                    //PerfilPuntas.Propiedades.Add(new Propiedad { Nombre = "Q1 MAX", DescripcionCorta = "Q1 MAX", DescripcionLarga = "Q1 MAX", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });
                    //PerfilPuntas.Propiedades.Add(new Propiedad { Nombre = "A1", DescripcionCorta = "A1", DescripcionLarga = "A1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });
                    //PerfilPuntas.Propiedades.Add(new Propiedad { Nombre = "Y6", DescripcionCorta = "Y6", DescripcionLarga = "Y6", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });

                    //PerfilPuntas.Propiedades = SortObservableCollectionPropiedad(PerfilPuntas.Propiedades);

                    //PropiedadesPuntas.Clear();

                    //foreach (var item in PerfilPuntas.Propiedades)
                    //{
                    //    NumericEntry uc = new NumericEntry();
                    //    PropiedadViewModel mvm = new PropiedadViewModel(item);
                    //    uc.DataContext = mvm;
                    //    PropiedadesPuntas.Add(uc);
                    //}
                    //PanelPropiedadesPuntas = SetNumericEntryToStackPanel(PropiedadesPuntas, PerfilPuntas.Propiedades);
                    ////-------------------Perfil Puntas-------------------

                    ////-------------------Perfil ID-------------------
                    //PerfilID.Propiedades = new ObservableCollection<Propiedad>();
                    //PerfilID.Propiedades.Add(new Propiedad { Nombre = "P1", DescripcionCorta = "P1", DescripcionLarga = "P1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });

                    //PerfilID.Propiedades = SortObservableCollectionPropiedad(PerfilID.Propiedades);

                    //PropiedadesID.Clear();

                    //foreach (var item in PerfilID.Propiedades)
                    //{
                    //    NumericEntry uc = new NumericEntry();
                    //    PropiedadViewModel mvm = new PropiedadViewModel(item);
                    //    uc.DataContext = mvm;
                    //    PropiedadesID.Add(uc);
                    //}
                    //panelPropiedadesID = SetNumericEntryToStackPanel(PropiedadesID, PerfilID.Propiedades);
                    ////-------------------Perfil ID-------------------

                    ////-------------------Perfil Lateral-------------------
                    //PerfilLateral.Propiedades = new ObservableCollection<Propiedad>();
                    //PerfilLateral.Propiedades.Add(new Propiedad { Nombre = "T1", DescripcionCorta = "T1", DescripcionLarga = "T1", TipoDato = "Distance", Unidad = "Inch (in)", Valor = 0, Imagen = null });

                    //PerfilLateral.Propiedades = SortObservableCollectionPropiedad(PerfilLateral.Propiedades);

                    //PropiedadesLateral.Clear();

                    //foreach (var item in PerfilLateral.Propiedades)
                    //{
                    //    NumericEntry uc = new NumericEntry();
                    //    PropiedadViewModel mvm = new PropiedadViewModel(item);
                    //    uc.DataContext = mvm;
                    //    PropiedadesLateral.Add(uc);
                    //}
                    //PanelPropiedadesLateral = SetNumericEntryToStackPanel(PropiedadesLateral, PerfilLateral.Propiedades);
                    ////-------------------Perfil Lateral-------------------
                }
                else
                {
                    //No se seleccionó ningun componente.
                }
            }
        }

        /// <summary>
        /// Método que guarda el componente.
        /// </summary>
        private async void savePlano()
        {
            dialogService = new DialogService();
            var Controller = await dialogService.SendProgressAsync(Resources.StringResources.ttlEspereUnMomento, "Guardando componente...");

            DescripcionGeneral = string.Format("{0:0.00000}", Module.ConvertTo(D1.TipoDato,D1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), D1.Valor)) + " X " + string.Format("{0:0.00000}", Module.ConvertTo(H1.TipoDato, H1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), H1.Valor)) + " " + TipoAnillo;

            if (DataManager.ExistArquetipo(Codigo))
            {
                int confirmacionUpdateArquetipo = DataManager.UpdateArquetipo(Codigo, DescripcionGeneral, null, true);

                DataManager.UpdateArquetipoRing(Codigo, D1.Valor, D1.Unidad, H1.Valor, H1.Unidad, FreeGap.Valor, FreeGap.Unidad, Mass.Valor, Mass.Unidad, Tension.Valor, Tension.Unidad, TensionTol.Valor, TensionTol.Unidad, NoPlano, CustomerPartNumber, CustomerRevisionLevel, Size, TipoAnillo, CustomerDocNo, Treatment, EspecTreatment, HardnessMin.Valor, HardnessMin.Unidad, HardnessMax.Valor, HardnessMax.Unidad, EspecificacionMaterialSeleccionada, OvalityMin.Valor, OvalityMin.Unidad, OvalityMax.Valor, OvalityMax.Unidad);

                #region Update Propiedades Perfil OD
                foreach (Propiedad propiedad in PerfilOD.Propiedades)
                {
                    DataManager.UpdateArquetipoPropiedades(Codigo, propiedad.idPropiedad, propiedad.Unidad, propiedad.Valor);
                }

                foreach (PropiedadCadena propiedad in PerfilOD.PropiedadesCadena)
                {
                    DataManager.UpdateArquetipoPropiedadesCadena(Codigo, propiedad.idPropiedad, propiedad.Valor);
                }

                foreach (PropiedadBool propiedad in PerfilOD.PropiedadesBool)
                {
                    DataManager.UpdateArquetipoPropiedadesBool(Codigo, propiedad.idPropiedad, propiedad.Valor);
                }

                foreach (PropiedadOptional propiedad in PerfilOD.PropiedadesOpcionales)
                {
                    DataManager.UpdateArquetipoPropiedadesOpcionales(Codigo, propiedad.idPropiedadOpcional, propiedad.ElementSelected.ValorCadena);
                }
                #endregion

                #region Update Propiedades Perfil ID
                foreach (Propiedad propiedad in PerfilID.Propiedades)
                {
                    DataManager.UpdateArquetipoPropiedades(Codigo, propiedad.idPropiedad, propiedad.Unidad, propiedad.Valor);
                }

                foreach (PropiedadCadena propiedad in PerfilID.PropiedadesCadena)
                {
                    DataManager.UpdateArquetipoPropiedadesCadena(Codigo, propiedad.idPropiedad, propiedad.Valor);
                }

                foreach (PropiedadBool propiedad in PerfilID.PropiedadesBool)
                {
                    DataManager.UpdateArquetipoPropiedadesBool(Codigo, propiedad.idPropiedad, propiedad.Valor);
                }

                foreach (PropiedadOptional propiedad in PerfilID.PropiedadesOpcionales)
                {
                    DataManager.UpdateArquetipoPropiedadesOpcionales(Codigo, propiedad.idPropiedadOpcional, propiedad.ElementSelected.ValorCadena);
                }
                #endregion

                #region Update Propiedades Perfil Lateral
                foreach (Propiedad propiedad in PerfilLateral.Propiedades)
                {
                    DataManager.UpdateArquetipoPropiedades(Codigo, propiedad.idPropiedad, propiedad.Unidad, propiedad.Valor);
                }

                foreach (PropiedadCadena propiedad in PerfilLateral.PropiedadesCadena)
                {
                    DataManager.UpdateArquetipoPropiedadesCadena(Codigo, propiedad.idPropiedad, propiedad.Valor);
                }

                foreach (PropiedadBool propiedad in PerfilLateral.PropiedadesBool)
                {
                    DataManager.UpdateArquetipoPropiedadesBool(Codigo, propiedad.idPropiedad, propiedad.Valor);
                }

                foreach (PropiedadOptional propiedad in PerfilLateral.PropiedadesOpcionales)
                {
                    DataManager.UpdateArquetipoPropiedadesOpcionales(Codigo, propiedad.idPropiedadOpcional, propiedad.ElementSelected.ValorCadena);
                }
                #endregion

                #region Update Propiedades Perfil Puntas
                foreach (Propiedad propiedad in PerfilPuntas.Propiedades)
                {
                    DataManager.UpdateArquetipoPropiedades(Codigo, propiedad.idPropiedad, propiedad.Unidad, propiedad.Valor);
                }

                foreach (PropiedadCadena propiedad in PerfilPuntas.PropiedadesCadena)
                {
                    DataManager.UpdateArquetipoPropiedadesCadena(Codigo, propiedad.idPropiedad, propiedad.Valor);
                }

                foreach (PropiedadBool propiedad in PerfilPuntas.PropiedadesBool)
                {
                    DataManager.UpdateArquetipoPropiedadesBool(Codigo, propiedad.idPropiedad, propiedad.Valor);
                }

                foreach (PropiedadOptional propiedad in PerfilPuntas.PropiedadesOpcionales)
                {
                    DataManager.UpdateArquetipoPropiedadesOpcionales(Codigo, propiedad.idPropiedadOpcional, propiedad.ElementSelected.ValorCadena);
                }
                #endregion

                #region Update Normas

                DataManager.DeleteArquetipoNorma(Codigo);

                foreach (var item in ListaNormas)
                {
                    DataManager.InsertArquetipoNorma(Codigo, item.idNorma);
                }
                #endregion

            }
            else
            {
                int confirmacionSaveArquetipo = DataManager.InsertArquetipo(Codigo, DescripcionGeneral, null, true);

                if (confirmacionSaveArquetipo > 0)
                {

                    //Insert arquetipo.
                    DataManager.InsertArquetipoRing(Codigo, D1.Valor, D1.Unidad, H1.Valor, H1.Unidad, FreeGap.Valor, FreeGap.Unidad, Mass.Valor, Mass.Unidad, Tension.Valor, Tension.Unidad, TensionTol.Valor, TensionTol.Unidad, NoPlano, CustomerPartNumber, CustomerRevisionLevel, Size, TipoAnillo, CustomerDocNo, Treatment, EspecTreatment, HardnessMin.Valor, HardnessMin.Unidad, HardnessMax.Valor, HardnessMax.Unidad, EspecificacionMaterialSeleccionada, OvalityMin.Valor, OvalityMin.Unidad, OvalityMax.Valor, OvalityMax.Unidad);

                    #region Insert Perfil OD
                    DataManager.InsertPerfilArquetipo(Codigo, PerfilOD.idPerfil);
                    foreach (Propiedad propiedad in PerfilOD.Propiedades)
                    {
                        DataManager.InsertArquetipoPropiedades(Codigo, propiedad.idPropiedad, propiedad.Unidad, propiedad.Valor);
                    }

                    foreach (PropiedadCadena propiedad in PerfilOD.PropiedadesCadena)
                    {
                        DataManager.InsertArquetipoPropiedadesCadena(Codigo, propiedad.idPropiedad, propiedad.Valor);
                    }

                    foreach (PropiedadBool propiedad in PerfilOD.PropiedadesBool)
                    {
                        DataManager.InsertArquetipoPropiedadesBool(Codigo, propiedad.idPropiedad, propiedad.Valor);
                    }

                    foreach (PropiedadOptional propiedad in PerfilOD.PropiedadesOpcionales)
                    {
                        DataManager.InsertArquetipoPropiedadesOpcionales(Codigo, propiedad.idPropiedadOpcional, propiedad.ElementSelected.ValorCadena);
                    }
                    #endregion

                    #region Insert Perfil Lateral
                    DataManager.InsertPerfilArquetipo(Codigo, PerfilLateral.idPerfil);
                    foreach (Propiedad propiedad in PerfilLateral.Propiedades)
                    {
                        DataManager.InsertArquetipoPropiedades(Codigo, propiedad.idPropiedad, propiedad.Unidad, propiedad.Valor);
                    }

                    foreach (PropiedadCadena propiedad in PerfilLateral.PropiedadesCadena)
                    {
                        DataManager.InsertArquetipoPropiedadesCadena(Codigo, propiedad.idPropiedad, propiedad.Valor);
                    }

                    foreach (PropiedadBool propiedad in PerfilLateral.PropiedadesBool)
                    {
                        DataManager.InsertArquetipoPropiedadesBool(Codigo, propiedad.idPropiedad, propiedad.Valor);
                    }

                    foreach (PropiedadOptional propiedad in PerfilLateral.PropiedadesOpcionales)
                    {
                        DataManager.InsertArquetipoPropiedadesOpcionales(Codigo, propiedad.idPropiedadOpcional, propiedad.ElementSelected.ValorCadena);
                    }
                    #endregion

                    #region Insert Perfil ID
                    DataManager.InsertPerfilArquetipo(Codigo, PerfilID.idPerfil);
                    foreach (Propiedad propiedad in PerfilID.Propiedades)
                    {
                        DataManager.InsertArquetipoPropiedades(Codigo, propiedad.idPropiedad, propiedad.Unidad, propiedad.Valor);
                    }

                    foreach (PropiedadCadena propiedad in PerfilID.PropiedadesCadena)
                    {
                        DataManager.InsertArquetipoPropiedadesCadena(Codigo, propiedad.idPropiedad, propiedad.Valor);
                    }

                    foreach (PropiedadBool propiedad in PerfilID.PropiedadesBool)
                    {
                        DataManager.InsertArquetipoPropiedadesBool(Codigo, propiedad.idPropiedad, propiedad.Valor);
                    }

                    foreach (PropiedadOptional propiedad in PerfilID.PropiedadesOpcionales)
                    {
                        DataManager.InsertArquetipoPropiedadesOpcionales(Codigo, propiedad.idPropiedadOpcional, propiedad.ElementSelected.ValorCadena);
                    }
                    #endregion

                    #region Insert Perfil Puntas
                    DataManager.InsertPerfilArquetipo(Codigo, PerfilPuntas.idPerfil);
                    foreach (Propiedad propiedad in PerfilPuntas.Propiedades)
                    {
                        DataManager.InsertArquetipoPropiedades(Codigo, propiedad.idPropiedad, propiedad.Unidad, propiedad.Valor);
                    }

                    foreach (PropiedadCadena propiedad in PerfilPuntas.PropiedadesCadena)
                    {
                        DataManager.InsertArquetipoPropiedadesCadena(Codigo, propiedad.idPropiedad, propiedad.Valor);
                    }

                    foreach (PropiedadBool propiedad in PerfilPuntas.PropiedadesBool)
                    {
                        DataManager.InsertArquetipoPropiedadesBool(Codigo, propiedad.idPropiedad, propiedad.Valor);
                    }

                    foreach (PropiedadOptional propiedad in PerfilPuntas.PropiedadesOpcionales)
                    {
                        DataManager.InsertArquetipoPropiedadesOpcionales(Codigo, propiedad.idPropiedadOpcional, propiedad.ElementSelected.ValorCadena);
                    }
                    #endregion

                    #region Insert Normas
                    foreach (var item in ListaNormas)
                    {
                        DataManager.InsertArquetipoNorma(Codigo, item.idNorma);
                    }
                    #endregion

                }
                else
                {
                    //Notificar que ocurrio un error.
                }
            }

            await Controller.CloseAsync();
            await dialogService.SendMessage(Resources.StringResources.ttlDone, "Componente Guardado");

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
                    MaterialBase.Especificacion = obj.value;
                    MaterialBase = MaterialBase;
                    break;
                case "Mass Calculated":
                    Mass.Valor = Convert.ToDouble(obj.value);
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

            int c = 0;
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

            c = 0;
            while (c < PerfilID.PropiedadesBool.Count && !ban)
            {
                if (PerfilID.PropiedadesBool[c].Nombre == obj.name)
                {
                    PerfilID.PropiedadesBool[c].Valor = Convert.ToBoolean(obj.value);
                    PerfilID.PropiedadesBool[c] = PerfilID.PropiedadesBool[c];
                }
                c += 1;
            }


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

            c = 0;
            while (c < PerfilLateral.PropiedadesBool.Count && !ban)
            {
                if (PerfilLateral.PropiedadesBool[c].Nombre == obj.name)
                {
                    PerfilLateral.PropiedadesBool[c].Valor = Convert.ToBoolean(obj.value);
                    PerfilLateral.PropiedadesBool[c] = PerfilLateral.PropiedadesBool[c];
                }
                c += 1;
            }

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

            c = 0;
            while (c < PerfilOD.PropiedadesBool.Count && !ban)
            {
                if (PerfilOD.PropiedadesBool[c].Nombre == obj.name)
                {
                    PerfilOD.PropiedadesBool[c].Valor = Convert.ToBoolean(obj.value);
                    PerfilOD.PropiedadesBool[c] = PerfilOD.PropiedadesBool[c];
                }
                c += 1;
            }

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

            c = 0;
            while (c < PerfilPuntas.PropiedadesBool.Count && !ban)
            {
                if (PerfilPuntas.PropiedadesBool[c].Nombre == obj.name)
                {
                    PerfilPuntas.PropiedadesBool[c].Valor = Convert.ToBoolean(obj.value);
                    PerfilPuntas.PropiedadesBool[c] = PerfilPuntas.PropiedadesBool[c];
                }
                c += 1;
            }
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
            if (EnumEx.GetEnumDescription(DataManager.UnidadDistance.Milimeter) == unidaddistance)
                IsMilimeter = true;

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
            ModelAnillo.HardnessMin.DescripcionCorta = "Hardness Min MP";
            ModelAnillo.HardnessMin.DescripcionLarga = "Dureza mínima de Materia Prima";
            HardnessMin = ModelAnillo.HardnessMin;

            ModelAnillo.HardnessMax.Nombre = "HardnessMax";
            ModelAnillo.HardnessMax.TipoDato = tipodatodureza;
            ModelAnillo.HardnessMax.Unidad = unidaddureza;
            ModelAnillo.HardnessMax.DescripcionCorta = "Hardness Max MP";
            ModelAnillo.HardnessMax.DescripcionLarga = "Dureza máxima de Materia Prima";
            HardnessMax = ModelAnillo.HardnessMax;

            ModelAnillo.Mass.Nombre = "MassAnillo";
            ModelAnillo.Mass.TipoDato = tipodatoMass;
            ModelAnillo.Mass.Unidad = unidadMass;
            ModelAnillo.Mass.DescripcionCorta = "Mass";
            ModelAnillo.Mass.DescripcionLarga = "Peso del Anillo";
            Mass = ModelAnillo.Mass;
        }

        /// <summary>
        /// Método para generar un PDF de la ruta
        /// </summary>
        private void viewRoute()
        {
            GenerarPDF.Traveler(ModelAnillo);
        }

        /// <summary>
        ///
        /// </summary>
        private void viewRouting()
        {
            //Declaramos un objeto el cual es la pantalla.
            WRouting wRouting = new WRouting();
            if (Operaciones != null && Operaciones.Count > 0)
            {
                OperationSelected = Operaciones[0];

                ListaOperacionesOpcionales = DataManager.GetAllOperaciones();

                //Establecemos el DataContext.
                wRouting.DataContext = this;

                //Desplegamos la pantalla-
                wRouting.ShowDialog();
            }
            else
            {
                //Enviar mensaje de no existen operaciones.
            }
        }

        /// <summary>
        /// Método para abrir la ventana donde se hacen las conversiones de FT a FD
        /// </summary>
        private void ConversionDeFTaFD()
        {
            ConversionFTFD Conversion = new ConversionFTFD();
            ConversionFTFDViewModel ConversionFTDF = new ConversionFTFDViewModel();

            Conversion.DataContext = ConversionFTDF;

            Conversion.ShowDialog();


            //CalculoPlacaModelo Conversion = new CalculoPlacaModelo();
            //CalculoPlacaModeloViewModel ConversionFTDF = new CalculoPlacaModeloViewModel();

            //Conversion.DataContext = ConversionFTDF;

            //Conversion.ShowDialog();
        }

        /// <summary>
        /// Método para generar el Hamburger Menú
        /// </summary>
        public void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();

            //Boton para agregar un nuevo plano
            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.File },
                    Label = StringResources.lblNuevo,
                    Command = NewPlano,
                    Tag = StringResources.lblNuevo,
                });
            //Boton para abrir un plano exixtente
            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Folder },
                    Label = StringResources.lblAbrir,
                    Command = OpenPlano,
                    Tag = StringResources.lblAbrir,
                });
            //Boton para importar un archivo xml existente
            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FileExcelBox },
                    Label = StringResources.lblImportarXML,
                    Command = ImportXML,
                    Tag = StringResources.lblImportarXML,
                });
            //Boton para guardar los planos(sin comando)
            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.ContentSave },
                    Label = StringResources.lblGuardar,
                    Command = SavePlano,
                    Tag = StringResources.lblGuardar,
                });
            //Boton para calcular la ruta
            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.PlayCircle },
                    Label = StringResources.lblCorrer,
                    Command = CalcularRuta,
                    Tag = StringResources.lblCorrer,
                });
            //Boton para ver las operaciones de la ruta en pdf
            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FilePdf },
                    Label = StringResources.lblVerRuta,
                    Command = ViewRoute,
                    Tag = StringResources.lblVerRuta,
                });

            //Boton para ver la ruta y calcular dimenciones.
            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Sigma },
                    Label = StringResources.lblCalcularDimensiones,
                    Command = ViewRouting,
                    Tag = StringResources.lblCalcularDimensiones,
                });
            //Boton para acceder a la pestaña de conversion Ft a Fd
            this.MenuItems.Add(
                new HamburgerMenuIconItem()
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.TooltipEdit },
                    Label = StringResources.lblConvertir,
                    Command = ConversionFTaFD,
                    Tag = StringResources.lblConvertir,
                });

            this.MenuItems.Add(
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AccessPoint },
                    Label = "Create a ring",
                    Command = CreateRing,
                    Tag = "Crea un anillo."
                });

            this.MenuItems.Add(
                new HamburgerMenuIconItem {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AccountKey },
                    Label = "Calculate dimensions",
                    Command = CalculateDimensions,
                    Tag = "Calculate dimension"
                });

            this.MenuItems.Add(
                new HamburgerMenuIconItem {
                    Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.CropFree },
                    Label = "Calcular free gap",
                    Command = GoCalculateFreeGap,
                    Tag = "Calculate free gap",
                });
        }

        private void setMaterialRemover()
        {
            int c = 0;

            while (c < Operaciones.Count)
            {
                if (OperationSelected.NoOperacion == Operaciones[c].NoOperacion)
                {
                    Operaciones[c] = OperationSelected;
                    OperationSelected = Operaciones[c];
                    NotifyChange("OperationSelected");
                    break;
                }
                c++;
            }
        }

        /// <summary>
        /// Método que muestra la pantalla para que el usuario seleccione los perfiles del anillo.
        /// </summary>
        private void createRing()
        {
            WCreateRing createRing = new WCreateRing();

            createRing.DataContext = this;
            createRing.ShowDialog();

            PerfilOD = PerfilSeleccionadoOD;
            PerfilID = PerfilSeleccionadoID;
            PerfilLateral = PerfilSeleccionadoLateral;
            PerfilPuntas = PerfilSeleccionadoPuntas;

            PerfilOD.Propiedades = DataManager.GetAllPropiedadesByPerfil(PerfilOD.idPerfil,IsMilimeter);
            PerfilOD.PropiedadesCadena = DataManager.GetAllPropiedadesCadenaByPerfil(PerfilOD.idPerfil);
            PerfilOD.PropiedadesBool = DataManager.GetallPropiedadesBoolByPerfil(PerfilOD.idPerfil);
            PerfilOD.PropiedadesOpcionales = DataManager.GetAllPropiedadesOpcionalesByPerfil(PerfilOD.idPerfil);

            PerfilID.Propiedades = DataManager.GetAllPropiedadesByPerfil(PerfilID.idPerfil,IsMilimeter);
            PerfilID.PropiedadesCadena = DataManager.GetAllPropiedadesCadenaByPerfil(PerfilID.idPerfil);
            PerfilID.PropiedadesBool = DataManager.GetallPropiedadesBoolByPerfil(PerfilID.idPerfil);
            PerfilID.PropiedadesOpcionales = DataManager.GetAllPropiedadesOpcionalesByPerfil(PerfilID.idPerfil);

            PerfilLateral.Propiedades = DataManager.GetAllPropiedadesByPerfil(PerfilLateral.idPerfil,IsMilimeter);
            PerfilLateral.PropiedadesCadena = DataManager.GetAllPropiedadesCadenaByPerfil(PerfilLateral.idPerfil);
            PerfilLateral.PropiedadesBool = DataManager.GetallPropiedadesBoolByPerfil(PerfilLateral.idPerfil);
            PerfilLateral.PropiedadesOpcionales = DataManager.GetAllPropiedadesOpcionalesByPerfil(PerfilLateral.idPerfil);

            PerfilPuntas.Propiedades = DataManager.GetAllPropiedadesByPerfil(PerfilPuntas.idPerfil,IsMilimeter);
            PerfilPuntas.PropiedadesCadena = DataManager.GetAllPropiedadesCadenaByPerfil(PerfilPuntas.idPerfil);
            PerfilPuntas.PropiedadesBool = DataManager.GetallPropiedadesBoolByPerfil(PerfilPuntas.idPerfil);
            PerfilPuntas.PropiedadesOpcionales = DataManager.GetAllPropiedadesOpcionalesByPerfil(PerfilPuntas.idPerfil);

            createNumericEntry();

            createTextEntry();

            createBoolEntry();

            createOptionalEntry();
        }

        private void createOptionalEntry()
        {
            #region OD
            foreach (PropiedadOptional propiedad in PerfilOD.PropiedadesOpcionales)
            {
                OptionalEntry optionalEntry = new OptionalEntry();

                PropiedadOptionalViewModel vm = new PropiedadOptionalViewModel(propiedad);
                optionalEntry.DataContext = vm;

                PropiedadesOptionalOD.Add(optionalEntry);
            }

            PanelPropiedadesOpcionalesOD = SetOptionalEntryToStackPanel(PropiedadesOptionalOD, PerfilOD.PropiedadesOpcionales);
            #endregion

            #region ID
            foreach (PropiedadOptional propiedad in PerfilID.PropiedadesOpcionales)
            {
                OptionalEntry optionalEntry = new OptionalEntry();

                PropiedadOptionalViewModel vm = new PropiedadOptionalViewModel(propiedad);
                optionalEntry.DataContext = vm;

                PropiedadesOptionalID.Add(optionalEntry);
            }

            PanelPropiedadesOpcionalesID = SetOptionalEntryToStackPanel(PropiedadesOptionalID, PerfilID.PropiedadesOpcionales);
            #endregion

            #region Lateral
            foreach (PropiedadOptional propiedad in PerfilLateral.PropiedadesOpcionales)
            {
                OptionalEntry optionalEntry = new OptionalEntry();

                PropiedadOptionalViewModel vm = new PropiedadOptionalViewModel(propiedad);
                optionalEntry.DataContext = vm;

                PropiedadesOptionalLateral.Add(optionalEntry);

            }

            PanelPropiedadesOpcionalesLateral = SetOptionalEntryToStackPanel(PropiedadesOptionalLateral, PerfilLateral.PropiedadesOpcionales);
            #endregion

            #region Puntas
            foreach (PropiedadOptional propiedad in PerfilPuntas.PropiedadesOpcionales)
            {
                OptionalEntry optionalEntry = new OptionalEntry();

                PropiedadOptionalViewModel vm = new PropiedadOptionalViewModel(propiedad);
                optionalEntry.DataContext = vm;
                PropiedadesOptionalPuntas.Add(optionalEntry);
            }

            PanelPropiedadesOpcionalesPuntas = SetOptionalEntryToStackPanel(PropiedadesOptionalPuntas, PerfilPuntas.PropiedadesOpcionales);
            #endregion
        }

        private void createBoolEntry()
        {
            #region OD
            foreach (PropiedadBool propiedad in PerfilOD.PropiedadesBool)
            {
                BoolEntry boolEntry = new BoolEntry();

                PropiedadBoolViewModel propiedadViewModel = new PropiedadBoolViewModel(propiedad);
                boolEntry.DataContext = propiedadViewModel;

                PropiedadesBoolOD.Add(boolEntry);
            }
            PanelPropiedadesBoolOD = SetBoolEntryToStackPanel(PropiedadesBoolOD, PerfilOD.PropiedadesBool);
            #endregion

            #region ID
            foreach (PropiedadBool propiedad in PerfilID.PropiedadesBool)
            {
                BoolEntry boolEntry = new BoolEntry();

                PropiedadBoolViewModel propiedadViewModel = new PropiedadBoolViewModel(propiedad);
                boolEntry.DataContext = propiedadViewModel;

                PropiedadesBoolID.Add(boolEntry);
            }
            PanelPropiedadesBoolID = SetBoolEntryToStackPanel(PropiedadesBoolID, PerfilID.PropiedadesBool);
            #endregion

            #region Lateral
            foreach (PropiedadBool propiedad in PerfilLateral.PropiedadesBool)
            {
                BoolEntry boolEntry = new BoolEntry();

                PropiedadBoolViewModel propiedadViewModel = new PropiedadBoolViewModel(propiedad);
                boolEntry.DataContext = propiedadViewModel;

                PropiedadesBoolLateral.Add(boolEntry);
            }
            PanelPropiedadesBoolLateral = SetBoolEntryToStackPanel(PropiedadesBoolLateral, PerfilLateral.PropiedadesBool);
            #endregion

            #region Puntas
            foreach (PropiedadBool propiedad in PerfilPuntas.PropiedadesBool)
            {
                BoolEntry boolEntry = new BoolEntry();

                PropiedadBoolViewModel propiedadViewModel = new PropiedadBoolViewModel(propiedad);
                boolEntry.DataContext = propiedadViewModel;

                PropiedadesBoolPuntas.Add(boolEntry);
            }
            PanelPropiedadesBoolPuntas = SetBoolEntryToStackPanel(PropiedadesBoolPuntas, PerfilPuntas.PropiedadesBool);
            #endregion

        }

        private void createTextEntry()
        {
            #region OD
            foreach (PropiedadCadena propiedad in PerfilOD.PropiedadesCadena)
            {
                StringEntry textEntry = new StringEntry();

                PropiedadCadenaViewModel propiedadViewModel = new PropiedadCadenaViewModel(propiedad);
                textEntry.DataContext = propiedadViewModel;

                PropiedadesCadenaOD.Add(textEntry);
            }
            PanelPropiedadesCadenaOD = SetStringEntryToStackPanel(PropiedadesCadenaOD, PerfilOD.PropiedadesCadena);
            #endregion

            #region ID
            foreach (PropiedadCadena propiedad in PerfilID.PropiedadesCadena)
            {
                StringEntry textEntry = new StringEntry();

                PropiedadCadenaViewModel propiedadViewModel = new PropiedadCadenaViewModel(propiedad);
                textEntry.DataContext = propiedadViewModel;

                PropiedadesCadenaID.Add(textEntry);

            }
            PanelPropiedadesCadenaID = SetStringEntryToStackPanel(PropiedadesCadenaID, PerfilID.PropiedadesCadena);
            #endregion

            #region Lateral
            foreach (PropiedadCadena propiedad in PerfilLateral.PropiedadesCadena)
            {
                StringEntry textEntry = new StringEntry();

                PropiedadCadenaViewModel propiedadViewModel = new PropiedadCadenaViewModel(propiedad);
                textEntry.DataContext = propiedadViewModel;

                PropiedadesCadenaLateral.Add(textEntry);
            }
            PanelPropiedadesCadenaLateral = SetStringEntryToStackPanel(PropiedadesCadenaLateral, PerfilLateral.PropiedadesCadena);
            #endregion

            #region Puntas
            foreach (PropiedadCadena propiedad in PerfilPuntas.PropiedadesCadena)
            {
                StringEntry textEntry = new StringEntry();

                PropiedadCadenaViewModel propiedadViewModel = new PropiedadCadenaViewModel(propiedad);
                textEntry.DataContext = propiedadViewModel;

                PropiedadesCadenaPuntas.Add(textEntry);
            }
            PanelPropiedadesCadenaPuntas = SetStringEntryToStackPanel(PropiedadesCadenaPuntas, PerfilPuntas.PropiedadesCadena);
            #endregion
        }

        private void createNumericEntry()
        {
            #region OD
            foreach (Propiedad propiedad in PerfilOD.Propiedades)
            {
                NumericEntry numeric = new NumericEntry();

                PropiedadViewModel propiedadViewModel = new PropiedadViewModel(propiedad);
                numeric.DataContext = propiedadViewModel;

                PropiedadesOD.Add(numeric);
            }
            PanelPropiedadesOD = SetNumericEntryToStackPanel(PropiedadesOD, PerfilOD.Propiedades);
            #endregion

            #region ID
            foreach (Propiedad propiedad in PerfilID.Propiedades)
            {
                NumericEntry numeric = new NumericEntry();

                PropiedadViewModel propiedadViewModel = new PropiedadViewModel(propiedad);
                numeric.DataContext = propiedadViewModel;

                PropiedadesID.Add(numeric);
            }
            PanelPropiedadesID = SetNumericEntryToStackPanel(PropiedadesID, PerfilID.Propiedades);
            #endregion

            #region Lateral
            foreach (Propiedad propiedad in PerfilLateral.Propiedades)
            {
                NumericEntry numeric = new NumericEntry();

                PropiedadViewModel propiedadViewModel = new PropiedadViewModel(propiedad);
                numeric.DataContext = propiedadViewModel;

                PropiedadesLateral.Add(numeric);
            }
            PanelPropiedadesLateral = SetNumericEntryToStackPanel(PropiedadesLateral, PerfilLateral.Propiedades);
            #endregion

            #region Puntas
            foreach (Propiedad propiedad in PerfilPuntas.Propiedades)
            {
                NumericEntry numeric = new NumericEntry();

                PropiedadViewModel propiedadViewModel = new PropiedadViewModel(propiedad);
                numeric.DataContext = propiedadViewModel;

                PropiedadesPuntas.Add(numeric);
            }
            PanelPropiedadesPuntas = SetNumericEntryToStackPanel(PropiedadesPuntas, PerfilPuntas.Propiedades);
            #endregion
        }

        private void verNormas()
        {
            int x = ListaAllNormas.Count;

            WSelectNorma ventana = new WSelectNorma();

            ventana.DataContext = this;

            ventana.ShowDialog();

            ListaNormas = new ObservableCollection<DO_Norma>();

            foreach (var norma in ListaAllNormas)
            {
                if (norma.IsSelected)
                    ListaNormas.Add(norma);
            }

            ListaNormas = ListaNormas;

        }
        #endregion
    }
}