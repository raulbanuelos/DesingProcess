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
        /// Cadena que representa la concatecacion del nombre con la unidad.
        /// </summary>
        public string TextoPresentacion {
            get {
                return DescripcionCorta + " : " + Unidad;
            }
        }

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

        #region Métodos

        public override string ToString()
        {
            //Retornamos una cadena con el valor de la propiedad con el formato 0.00000
            return Nombre;
        }
        #endregion
    }
}
