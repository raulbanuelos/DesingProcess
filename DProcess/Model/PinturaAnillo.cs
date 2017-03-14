namespace Model
{
    public class PinturaAnillo
    {
        #region Propiedades
        /// <summary>
        /// Cadena que representa el color de la pintura.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Double que representa el ancho de la franja de pintura en el anillo.
        /// </summary>
        public double AnchoPintura { get; set; }

        /// <summary>
        /// Propiedad que representa la ubicación de la pintura en el anillo.
        /// </summary>
        public UbicacionPintura UbicacionFranja { get; set; }

        /// <summary>
        /// Cadena que representa la nota general de pintura.
        /// </summary>
        public string Nota { get; set; }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por default. Inicializa los valores por default de todas las propiedades.
        /// </summary>
        public PinturaAnillo()
        {
            //Asignamos los valores por default de todas las propiedades.
            Color = string.Empty;
            AnchoPintura = 0;
            UbicacionFranja = new UbicacionPintura();
            Nota = string.Empty;
        }
        #endregion

        #region Métodos
        #endregion
    }
}
