namespace Model
{
    public class PropiedadBool
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
        /// Booleano que representa el valor de la propiedad. (true:false)
        /// </summary>
        public bool Valor { get; set; }

        /// <summary>
        /// Cadena que representa una descripción larga de la propiedad
        /// </summary>
        public string DescripcionLarga { get; set; }

        /// <summary>
        /// Cadena que representa una descripción corta de la propiedad.
        /// </summary>
        public string DescripcionCorta { get; set; }

        /// <summary>
        /// Arreglo de bytes que representa la imagen de la propiedad.
        /// </summary>
        public byte[] Imagen { get; set; }

        /// <summary>
        /// Cadena que representa el tipo de perfil.
        /// </summary>
        public string TipoPerfil { get; set; }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public PropiedadBool()
        {
            //Asignamos los valores iniciales para cada propiedad.
            Nombre = string.Empty;
            DescripcionLarga = string.Empty;
            DescripcionCorta = string.Empty;
            Valor = false;
            Imagen = null;
        }
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
