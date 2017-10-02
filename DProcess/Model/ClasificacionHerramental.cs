using System.Collections.ObjectModel;

namespace Model
{
    public class ClasificacionHerramental
    {
        #region Propiedades
        /// <summary>
        /// Entero que presenta el id de clasificación.
        /// </summary>
        public int IdClasificacion { get; set; }

        /// <summary>
        /// Cadena que representa la descripción de la clasificación
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Cadena que representa la unidad de medida de la clasificación.
        /// </summary>
        public string UnidadMedida { get; set; }

        /// <summary>
        /// Double que representa el costo que tiene la clasificación.
        /// </summary>
        public double Costo { get; set; }

        /// <summary>
        /// Entero que representa la cantidad de herramental a utilizar.
        /// </summary>
        public int CantidadUtilizar { get; set; }

        /// <summary>
        /// Entero que representa la cantidad de piezas que dura siendo útil el herramental.
        /// </summary>
        public int VidaUtil { get; set; }

        /// <summary>
        /// Boolenano que indica si el herramental es tomado en cuenta para la verificación anual de herramental.
        /// </summary>
        public bool VerificacionAnual { get; set; }

        /// <summary>
        /// Colección de tipo cadena que representa las cotas mas importantes que re revizan.
        /// </summary>
        public ObservableCollection<string> ListaCotasRevizar { get; set; }

        public string objetoXML { get; set; }
        #endregion

        #region Constructores 
        /// <summary>
        /// Constructor por default. Inicializa los valores de todas las propiedades por default.
        /// </summary>
        public ClasificacionHerramental()
        {
            //Asignamos los valores por default a todas las propiedades.
            IdClasificacion = 0;
            Descripcion = string.Empty;
            UnidadMedida = string.Empty;
            Costo = 0;
            CantidadUtilizar = 0;
            VidaUtil = 0;
            VerificacionAnual = false;
            ListaCotasRevizar = new ObservableCollection<string>();
        }
        #endregion

        #region Métodos
        #endregion
    }
}
