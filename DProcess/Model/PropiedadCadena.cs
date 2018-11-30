namespace Model
{
    public class PropiedadCadena
    {
        #region Propiedades

        /// <summary>
        /// Entero que representa el id de la propiedad.
        /// </summary>
        public int idPropiedad { get; set; }

        /// <summary>
        /// Cadena que representa el nombre de la propiedad.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Cadena que representa el valor de la propiedad.
        /// </summary>
        public string Valor { get; set; }

        /// <summary>
        /// Cadena que representa una descripción corta de la propiedad.
        /// </summary>
        public string DescripcionCorta { get; set; }

        /// <summary>
        /// Cadena que representa una descripción larga de la propiedad.
        /// </summary>
        public string DescripcionLarga { get; set; }

        /// <summary>
        /// Arreglo de bytes que representa la imagen de la propiedad.
        /// </summary>
        public byte[] Imagen { get; set; }

        /// <summary>
        /// Cadena que representa el tipo de perfil al que pertenece la propiedad.
        /// </summary>
        public string TipoPerfil { get; set; }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public PropiedadCadena()
        {
            //Asignamos los valores iniciales para cada propiedad.
            Nombre = string.Empty;
            Valor = string.Empty;
            DescripcionCorta = string.Empty;
            DescripcionLarga = string.Empty;
            Imagen = null;
        }
        #endregion

        #region Métodos

        #endregion
    }
}
