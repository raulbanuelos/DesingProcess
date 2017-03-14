using System.Collections.ObjectModel;
namespace Model
{
    public class Herramental : Arquetipo
    {
        #region Propiedades

        /// <summary>
        /// Propiedad que representa la clasificación del herramental.
        /// </summary>
        public ClasificacionHerramental clasificacionHerramental { get; set; }

        /// <summary>
        /// Booleano que representa si el herramental existe en sistema ERP.
        /// </summary>
        public bool Encontrado { get; set; }

        /// <summary>
        /// Cadena que representa el número de plano del herramental.
        /// </summary>
        public string Plano { get; set; }

        /// <summary>
        /// Cadena que representa la descripción que se muestra en el hoja de ruta.
        /// </summary>
        public string DescripcionRuta { get; set; }

        /// <summary>
        /// Cadena que representa la descripción de las medidas por las que se busca el herramental.
        /// </summary>
        public string DescripcionMedidasBusqueda { get; set; }

        /// <summary>
        /// Colección de propiedades que representa las propiedades del herramental.
        /// </summary>
        public ObservableCollection<Propiedad> Propiedades { get; set; }
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor por default. Inicializa las propiedades con valores por default.
        /// </summary>
        public Herramental()
        {
            //Asignamos los valores por default a todas las propiedades.
            clasificacionHerramental = new ClasificacionHerramental();
            Encontrado = false;
            Plano = string.Empty;
            Propiedades = new ObservableCollection<Propiedad>();
        }

        #endregion

        #region Métodos

        #endregion
    }
}
