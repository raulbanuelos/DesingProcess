using Model;
using System.ComponentModel;

namespace View.Services.ViewModel
{
    public class PropiedadViewModel : INotifyPropertyChanged
    {
        #region Atributos
        private Propiedad model; 
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
        /// Cadena que representa la unidad de la proiedad.
        /// </summary>
        /// <example>
        /// degree(°),Inch (in),PSI
        /// </example>
        public string Unidad {
            get {
                return model.Unidad;
            }
            set {
                model.Unidad = value;
                NotifyChange("Unidad");
            }
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
