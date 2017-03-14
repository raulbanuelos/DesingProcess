using System;

namespace Model
{
    public class Revision
    {
        #region Propiedades
        /// <summary>
        /// Cadena que representa el nivel de revisión del plano
        /// </summary>
        public string NivelRevision { get; set; }

        /// <summary>
        /// Cadena que representa la descripción de la revisión.
        /// </summary>
        public string DescripcionRevision { get; set; }

        /// <summary>
        /// Fecha que representa la fecha en la cual se realizo el cambio.
        /// </summary>
        public DateTime Fecha { get; set; }
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public Revision()
        {
            //Asignamos los valores iniciales para las propiedades.
            Fecha = DateTime.Now.Date;
        }
        #endregion

        #region Métodos
        #endregion
    }
}
