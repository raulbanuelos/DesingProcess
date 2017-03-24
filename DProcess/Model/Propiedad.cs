namespace Model
{
    public class Propiedad
    {
        #region Propiedades

        /// <summary>
        /// Cadena que representa el nombre de la propiedad.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Cadena que representa una descripción larga de la propiedad.
        /// </summary>
        public string DescripcionLarga { get; set; }

        /// <summary>
        /// Cadena que representa una descripción corta de la propiedad.
        /// </summary>
        public string DescripcionCorta { get; set; }

        /// <summary>
        /// Cadena que representa el tipo de dato de la propiedad.
        /// </summary>
        /// <example>
        /// Angle,Distance,Presion, etc.
        /// </example>
        public string TipoDato { get; set; }

        /// <summary>
        /// Cadena que representa la unidad de la proiedad.
        /// </summary>
        /// <example>
        /// degree(°),Inch (in),PSI
        /// </example>
        public string Unidad { get; set; }

        /// <summary>
        /// Double que representa el valor de la propiedad.
        /// </summary>
        public double Valor { get; set; }

        /// <summary>
        /// Arreglo de bytes que representa la imagen de la propiedad.
        /// </summary>
        public byte[] Imagen { get; set; }
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public Propiedad()
        {
            //Asignamos los valores iniciales para cada propiedad.
            Nombre = string.Empty;
            DescripcionCorta = string.Empty;
            DescripcionLarga = string.Empty;
            TipoDato = string.Empty;
            Unidad = string.Empty;
            Valor = 0;
            Imagen = null;

        }

        /// <summary>
        /// Constructor que inicializa las unidades en su valor por default.
        /// </summary>
        /// <param name="nombre">Nombre de la propiedad</param>
        /// <param name="descripcionCorta"></param>
        /// <param name="descripcionLarga"></param>
        /// <param name="_TipoDato">Tipo de dato. (Distance,Cantidad,Angle,Force,Mass,Presion,Tiempo)</param>
        public Propiedad(string nombre, string descripcionCorta,string descripcionLarga,string _TipoDato)
        {
            Nombre = nombre;
            DescripcionCorta = descripcionCorta;
            DescripcionLarga = descripcionLarga;

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
            Valor = 0;
        }
        #endregion

        #region Métodos

        public override string ToString()
        {
            //Retornamos una cadena con el valor de la propiedad con el formato 0.00000
            return string.Format("{0:0.00000}", Valor);
        }
        #endregion
    }
}
