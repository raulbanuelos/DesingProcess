namespace Model
{
    public class UbicacionPintura
    {
        #region Propiedades
        /// <summary>
        /// Entero que representa el id de la ubicación.
        /// </summary>
        public int IDUbicacionFranja { get; set; }

        /// <summary>
        /// Cadena que representa la ubicación de la pintura.
        /// </summary>
        public string UbicacionFranjaTexto { get; set; }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public UbicacionPintura()
        {
            //Asignamos los valores por default a todas las propiedades.
            IDUbicacionFranja = 0;
            UbicacionFranjaTexto = string.Empty;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return "{" + IDUbicacionFranja + "} " + UbicacionFranjaTexto;
        }
        #endregion
    }
}
