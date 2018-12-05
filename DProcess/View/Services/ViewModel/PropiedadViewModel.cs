using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Model;
using View.Forms.Modals;
using MahApps.Metro.Controls.Dialogs;
using View.Resources;
using System;
using View.Forms.Routing;
using System.Threading.Tasks;
using System.IO;

namespace View.Services.ViewModel
{
    public class PropiedadViewModel : INotifyPropertyChanged
    {
        #region Atributos

        public Propiedad model;

        private ObservableCollection<string> _allTipoUnidad;

        private DialogService dialogService;

        WPropiedadNumeric ventanaNumeric;

        #endregion

        #region Properties

        public ObservableCollection<string> AllTipoUnidad
        {
            get { return _allTipoUnidad; }
            set { _allTipoUnidad = value; NotifyChange("AllTipoUnidad"); }
        }

        private ObservableCollection<Propiedad> listaPropiedades;

        public ObservableCollection<Propiedad> ListaPropiedades
        {
            get { return listaPropiedades; }
            set { listaPropiedades = value; NotifyChange("ListaPropiedades"); }
        }

        private Propiedad propiedadSeleccionada;
        public Propiedad PropiedadSeleccionada
        {
            get { return propiedadSeleccionada; }
            set { propiedadSeleccionada = value; NotifyChange("PropiedadSeleccionada"); }
        }

        private bool _enabledEliminar;
        public bool EnabledEliminar
        {
            get
            {
                return _enabledEliminar;
            }
            set
            {
                _enabledEliminar = value;
                NotifyChange("EnabledEliminar");
            }
        }

        #region Properties of Model

        public int idPropiedad
        {
            get
            {
                return model.idPropiedad;
            }
            set {
                model.idPropiedad = value;
                NotifyChange("idPropiedad");
            }
        }

        /// <summary>
        /// Cadena que representa el nombre de la propiedad.
        /// </summary>
        public string Nombre
        {
            get
            {
                return model.Nombre;
            }
            set
            {
                model.Nombre = value;
                NotifyChange("Nombre");
                NotifyChange("TextoPresentacion");
            }
        }

        /// <summary>
        /// Cadena que representa la concatecacion del nombre con la unidad.
        /// </summary>
        public string TextoPresentacion
        {
            get
            {
                return model.DescripcionCorta + ":" + model.Unidad;
            }
        }

        /// <summary>
        /// Cadena que representa una descripción larga de la propiedad.
        /// </summary>
        public string DescripcionLarga
        {
            get
            {
                return model.DescripcionLarga;
            }
            set
            {
                model.DescripcionLarga = value;
                NotifyChange("DescripcionLarga");
            }
        }

        /// <summary>
        /// Cadena que representa una descripción corta de la propiedad.
        /// </summary>
        public string DescripcionCorta
        {
            get
            {
                return model.DescripcionCorta;
            }
            set
            {
                model.DescripcionCorta = value;
                NotifyChange("DescripcionCorta");
            }
        }

        /// <summary>
        /// Cadena que representa el tipo de dato de la propiedad.
        /// </summary>
        /// <example>
        /// Angle,Distance,Presion, etc.
        /// </example>
        public string TipoDato
        {
            get
            {
                return model.TipoDato;
            }
            set
            {
                model.TipoDato = value;
                NotifyChange("TipoDato");
            }
        }

        /// <summary>
        /// Cadena que representa la unidad de la propiedad.
        /// </summary>
        /// <example>
        /// degree(°),Inch (in),PSI
        /// </example>
        public string Unidad
        {
            get
            {
                return model.Unidad;
            }
            set
            {
                if (model.Unidad != value)
                {
                    SetNewValor(value);
                }
            }
        }


        /// <summary>
        /// Double que representa el valor de la propiedad.
        /// </summary>
        public double Valor
        {
            get
            {
                return model.Valor;
            }
            set
            {
                model.Valor = value;
                NotifyChange("Valor");
            }
        }

        /// <summary>
        /// Arreglo de bytes que representa la imagen de la propiedad.
        /// </summary>
        public byte[] Imagen
        {
            get
            {
                return model.Imagen;
            }
            set
            {
                model.Imagen = value;
                NotifyChange("Imagen");
            }
        }
        #endregion
        
        #endregion

        #region Events INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion
        
        #region Constructores

        /// <summary>
        /// Constructor que inicializa el atributo de modelo.
        /// </summary>
        /// <param name="Model">Propiedad que se va asignar al modelo.</param>
        public PropiedadViewModel(Propiedad Model)
        {
            //Mapeamos el valor del modelo recibido al atributo de la clase.
            model = Model;

            //Ejecutamos el método para obtener la lista de unidades, asignamos el resultado a la lista de la clase.
            if(!string.IsNullOrEmpty(model.TipoDato))
                AllTipoUnidad = DataManager.GetUnidades(model.TipoDato);

            dialogService = new DialogService();
        }

        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public PropiedadViewModel()
        {
            model = new Propiedad();
            dialogService = new DialogService();

            //Ejecutamos el método para obtener la lista de unidades, asignamos el resultado a la lista de la clase.
            AllTipoUnidad = DataManager.GetUnidades(model.TipoDato);
        }

        /// <summary>
        /// Constructor que inicializa las unidades en su valor por default.
        /// </summary>
        /// <param name="_nombre">Nombre de la propiedad</param>
        /// <param name="_descripcionCorta"></param>
        /// <param name="_descripcionLarga"></param>
        /// <param name="_TipoDato">Tipo de dato. (Distance,Cantidad,Angle,Force,Mass,Presion,Tiempo)</param>
        public PropiedadViewModel(string _nombre,string _descripcionCorta, string _descripcionLarga,string _TipoDato)
        {
            dialogService = new DialogService();
            model = new Propiedad();

            Nombre = _nombre;
            DescripcionCorta = _descripcionCorta;
            DescripcionLarga = _descripcionLarga;
            Valor = 0;

            if (_TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance)))
            {
                Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);
            }
            else if (_TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Cantidad)))
            {
                Unidad = EnumEx.GetEnumDescription(DataManager.UnidadCantidad.Unidades);
            }
            else if (_TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Angle)))
            {
                Unidad = EnumEx.GetEnumDescription(DataManager.UnidadAngle.Degree);
            }
            else if (_TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Force)))
            {
                Unidad = EnumEx.GetEnumDescription(DataManager.UnidadForce.Newton);
            }
            else if(_TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Mass)))
            {
                Unidad = EnumEx.GetEnumDescription(DataManager.UnidadMass.Kilogram);
            }
            else if (_TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Presion)))
            {
                Unidad = EnumEx.GetEnumDescription(DataManager.UnidadPresion.PSI);
            }
            else if(_TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Tiempo)))
            {
                Unidad = EnumEx.GetEnumDescription(DataManager.UnidadTiempo.Second);
            }
            else if(_TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Dureza)))
            {
                Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDureza.RB);
            }
        }

        /// <summary>
        /// Constructor para cuando se requieran administrar todas las propiedades.
        /// </summary>
        /// <param name="sTl"></param>
        public PropiedadViewModel(bool sTl)
        {
            //Obtenemos todas las propiedades.
            ListaPropiedades = DataManager.GetAllPropiedades();
        }
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region Commands
        public ICommand VerUnidades
        {
            get
            {
                return new RelayCommand(o => verListaUnidades());
            }
        }

        /// <summary>
        /// Comando para visualizar la propiedad seleccionada.
        /// </summary>
        public ICommand EditarPropiedad
        {
            get
            {
                return new RelayCommand(o => editarDocumento());
            }
        }

        public ICommand GuardarPropiedad
        {
            get
            {
                return new RelayCommand(o => guardarPropiedad());
            }
        }

        public ICommand SeleccionarImagen
        {
            get
            {
                return new RelayCommand(o => seleccionarImagen());
            }
        }

        public ICommand EliminarPropiedad
        {
            get
            {
                return new RelayCommand(o => eliminarPropiedad());
            }
        }

        public ICommand NewPropiedad
        {
            get
            {
                return new RelayCommand(o => newPropiedad());
            }
        }
        #endregion

        #region Methods

        private void newPropiedad()
        {
            PropiedadViewModel wmNumeric = new PropiedadViewModel(new Propiedad());
            ventanaNumeric = new WPropiedadNumeric();
            wmNumeric.EnabledEliminar = false;
            ventanaNumeric.DataContext = wmNumeric;
            ventanaNumeric.ShowDialog();

            //Obtenemos todas las propiedades.
            ListaPropiedades = DataManager.GetAllPropiedades();
        }

        private async void eliminarPropiedad()
        {
            if (idPropiedad > 0)
            {
                //Incializamos los servicios de dialog.
                DialogService dialogService = new DialogService();

                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminarRegistro, setting, MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    DataManager.DeletePropiedad(idPropiedad);
                    await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.ttlDone);

                    //Obtenemos todas las propiedades.
                    ListaPropiedades = DataManager.GetAllPropiedades();
                }
            }
            
        }

        private async void seleccionarImagen()
        {
            //Abre la ventana de explorador de archivos
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Imagenes|*.png";
            // Mostrar el explorador de archivos
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                if (!Module.IsFileInUse(filename))
                {
                    Imagen = await Task.Run(() => File.ReadAllBytes(filename));
                }
            }

        }

        private async void guardarPropiedad()
        {
            //Incializamos los servicios de dialog.
            DialogService dialogService = new DialogService();

            if (validar())
            {
                int r = 0;

                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;
                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgConfirmacion, setting, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    if (idPropiedad > 0)
                        r = DataManager.UpdatePropiedad(model);
                    else
                        r = DataManager.SetPropiedad(model);

                    if (r > 0)
                        await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.ttlDone);
                    else
                        await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                }
            }
            else
            {
                await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }
            
        }

        private bool validar()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(DescripcionCorta) || string.IsNullOrEmpty(DescripcionLarga) || string.IsNullOrEmpty(TipoDato) || Imagen == null)
                return false;
            else
                return true;
        }

        private void editarDocumento()
        {
            if (PropiedadSeleccionada.idPropiedad > 0)
            {
                PropiedadViewModel wmNumeric = new PropiedadViewModel(PropiedadSeleccionada);
                wmNumeric.EnabledEliminar = true;
                ventanaNumeric = new WPropiedadNumeric();
                ventanaNumeric.DataContext = wmNumeric;

                ventanaNumeric.ShowDialog();

                //Obtenemos todas las propiedades.
                ListaPropiedades = DataManager.GetAllPropiedades();

            }
        }
        
        private void verListaUnidades()
        {
            frmViewUnidades modal = new frmViewUnidades();

            modal.DataContext = this;

            modal.ShowDialog();
        }

        /// <summary>
        /// Método que asigna el nuevo valor a la propiedad Unidad. Así mismo permirte convertir o mantener el valor.
        /// </summary>
        /// <param name="NewUnidad"></param>
        public async void SetNewValor(string NewUnidad)
        {
            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.ttlConvertir;
            setting.NegativeButtonText = StringResources.ttlMantener;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgMantenerConvertir + model.Unidad +" "+ StringResources.msgA +" "+ NewUnidad, setting, MessageDialogStyle.AffirmativeAndNegative);

            //Comparamos si la respuesta fué afirmativa, el usuario eligió convertir el valor.
            if (result == MessageDialogResult.Affirmative)
            {
                Valor = Module.ConvertTo(model.TipoDato, model.Unidad, NewUnidad, Valor);
                NotifyChange("Valor");
            }
            model.Unidad = NewUnidad;
            NotifyChange("Unidad");
            NotifyChange("TextoPresentacion");
        }
        #endregion
    }
}