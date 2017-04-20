using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace View.Services.ViewModel
{
    public class PropiedadViewModel : INotifyPropertyChanged
    {
        #region Atributos
        public Propiedad model;

        private ObservableCollection<string> _allTipoUnidad;

        public ObservableCollection<string> AllTipoUnidad
        {
            get { return _allTipoUnidad; }
            set { _allTipoUnidad = value; NotifyChange("AllTipoUnidad"); }
        }

        #endregion

        #region Properties

        #endregion

        #region Events INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

        #region Propiedades de Modelo
        /// <summary>
        /// Cadena que representa el nombre de la propiedad.
        /// </summary>
        public string Nombre {
            get {
                return model.Nombre;
            }
            set {
                model.Nombre = value;
                NotifyChange("Nombre");
                NotifyChange("TextoPresentacion");
            }
        }

        /// <summary>
        /// Cadena que representa la concatecacion del nombre con la unidad.
        /// </summary>
        public string TextoPresentacion {
            get {
                return model.DescripcionCorta + ":" + model.Unidad;
            }
        }

        /// <summary>
        /// Cadena que representa una descripción larga de la propiedad.
        /// </summary>
        public string DescripcionLarga {
            get
            {
                return model.DescripcionLarga;
            }
            set {
                model.DescripcionLarga = value;
                NotifyChange("DescripcionLarga");
            }
        }
        
        /// <summary>
        /// Cadena que representa una descripción corta de la propiedad.
        /// </summary>
        public string DescripcionCorta {
            get {
                return model.DescripcionCorta;
            }
            set {
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
        public string TipoDato {
            get {
                return model.TipoDato;
            }
            set {
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
        public string Unidad {
            get {
                return model.Unidad;
            }
            set {
                if (model.Unidad != value)
                {
                    SetNewValor(value);
                }
            }
        }

        /// <summary>
        /// Método que asigna el nuevo valor a la propiedad Unidad. Así mismo permirte convertir o mantener el valor.
        /// </summary>
        /// <param name="NewUnidad"></param>
        public async void SetNewValor(string NewUnidad)
        {
            //Ejecutamos el método para obtener la ventana donde se muestran las unidades.
            var metroWindow = Module.GetWindow("Unidades") as MetroWindow;

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "Convert to";
            setting.NegativeButtonText = "Keep";

            //Mostramos el mensaje en donde le indicamos al usuario que desea realizar, si mantener el mismo valor o convertirlo a la unidad que acaba de seleccionar. El resultado lo guardamos en una variable de tipo MessageDialogResult.
            MessageDialogResult result = await metroWindow.ShowMessageAsync("Attention", "What do you want to do? \n •Keep the same value \n •Convert the value from " + model.Unidad + " to " + NewUnidad, MessageDialogStyle.AffirmativeAndNegative, setting);

            //Comparamos si la respuesta fué afirmativa, el usuario eligió convertir el valor.
            if (result == MessageDialogResult.Affirmative)
            {
                Valor = Module.ConvertTo(model.TipoDato, model.Unidad, NewUnidad,Valor);
                NotifyChange("Valor");
            }
            model.Unidad = NewUnidad;
            NotifyChange("Unidad");
            NotifyChange("TextoPresentacion");
        }

        /// <summary>
        /// Double que representa el valor de la propiedad.
        /// </summary>
        public double Valor {
            get {
                return model.Valor;
            }
            set {
                model.Valor = value;
                NotifyChange("Valor");
            }
        }
        
        /// <summary>
        /// Arreglo de bytes que representa la imagen de la propiedad.
        /// </summary>
        public byte[] Imagen {
            get {
                return model.Imagen;
            }
            set {
                model.Imagen  = value;
                NotifyChange("Imagen");
            }
        }
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
            AllTipoUnidad = DataManager.GetUnidades(model.TipoDato);
        }

        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public PropiedadViewModel()
        {
            Nombre = string.Empty;
            DescripcionCorta = string.Empty;
            DescripcionLarga = string.Empty;
            TipoDato = string.Empty;
            Unidad = string.Empty;
            Valor = 0;
            Imagen = null;

            model = new Propiedad();
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
            model = new Propiedad();
            Nombre = _nombre;
            DescripcionCorta = _descripcionCorta;
            DescripcionLarga = _descripcionLarga;
            Valor = 0;
            switch (_TipoDato)
            {
                case "Distance":
                    Unidad = "Inch (in)";
                    break;
                case "Cantidad":
                    Unidad = "Unidades";
                    break;
                case "Angle":
                    Unidad = "degree(°)";
                    break;
                case "Force":
                    Unidad = "Newton";
                    break;
                case "Mass":
                    Unidad = "Kilogram(kg)";
                    break;
                case "Presion":
                    Unidad = "PSI";
                    break;
                case "Tiempo":
                    Unidad = "second ('')";
                    break;
                case "Dureza":
                    Unidad = "HRC";
                    break;
                default:
                    break;
            }

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
    }
}