using System;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Model.Interfaces;
using System.Windows.Input;
using View.Forms.Modals;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using MahApps.Metro.Controls.Dialogs;

namespace View.Services.ViewModel
{
    public class AnilloViewModel : INotifyPropertyChanged
    {

        #region Attributes
        private Anillo ModelAnillo;
        #endregion

        #region Properties
        public ObservableCollection<string> ListaEspecificacionesMateriaPrima { get; set; }

        public ObservableCollection<Cliente> ListaClientes { get; set; }

        public ObservableCollection<string> ListaTreatment { get; set; }

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

            MenuItems.Add("New");
            MenuItems.Add("Open");
            MenuItems.Add("Import File");
            MenuItems.Add("Save");
            MenuItems.Add("SAP");

            //Inicializamos el plano;
            newPlano();
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
        #endregion

        #region Methods

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
