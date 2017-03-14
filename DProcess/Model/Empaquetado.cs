namespace Model
{
    public class Empaquetado
    {
        #region Propiedades
        /// <summary>
        /// Entero que representa la cantidad de piezas por rollo que se indicarán en la operación de Inspección Final.
        /// </summary>
        public int PzasXRollo { get; set; }

        /// <summary>
        /// Entero que representa la cantidad de rollos que se colocaran en una caja, esto se indicará en la operación de Inspección Final.
        /// </summary>
        public int RollosXCaja { get; set; }

        /// <summary>
        /// Cadena que representa el número de caja que se indicará en la operación Inspección Final.
        /// </summary>
        public string CajaNo { get; set; }

        /// <summary>
        /// Cadena que representa el papel que se utilizará en la operación Inspección Final.
        /// </summary>
        public string PapelTipo { get; set; }

        /// <summary>
        /// Cadena que representa la nota número uno que se indicará en la operación Inspección Final.
        /// </summary>
        public string Nota1 { get; set; }

        /// <summary>
        /// Cadena que representa la nota número dos que se indicará en la operación Inspección Final.
        /// </summary>
        public string Nota2 { get; set; }

        /// <summary>
        /// Cadena que representa el tipo de aceite que se utilizará en la operación Inspección Final.
        /// </summary>
        public string AceiteTipo { get; set; }

        /// <summary>
        /// Entero que representa el número de paso que se indicará en la operación Inspección Final.
        /// </summary>
        public int CantidadPasos { get; set; }

        /// <summary>
        /// Cadena que representa la nota general que se indicará en la operación Inspección Final.
        /// </summary>
        public string NotaGeneral { get; set; }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public Empaquetado()
        {
            //Asignamos los valores por default a las propiedades.
            PzasXRollo = 0;
            RollosXCaja = 0;
            CajaNo = string.Empty;
            PapelTipo = string.Empty;
            Nota1 = string.Empty;
            Nota2 = string.Empty;
            AceiteTipo = string.Empty;
            CantidadPasos = 0;
            NotaGeneral = string.Empty;
        }
        #endregion

        #region Métodos

        #endregion
    }
}
