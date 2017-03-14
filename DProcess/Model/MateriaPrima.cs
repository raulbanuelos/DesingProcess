using System.Collections.ObjectModel;
namespace Model
{
    public class MateriaPrima : Arquetipo
    {
        #region Propiedades

        private PropiedadCadena especificacion;
        /// <summary>
        /// Propiedad que representa la especificación de la materia prima.
        /// </summary>
        /// <example>
        /// SPR-128,MF012-S,etc.
        /// </example>
        public PropiedadCadena Especificacion
        {
            get
            {
                return especificacion;
            }
            set
            {
                especificacion = value;
                TipoDeMaterial = DataManager.GetTipoMaterial(especificacion.Valor);
            }
        }

        /// <summary>
        /// Cadena que representa el tipo de material.
        /// </summary>
        /// <example>
        /// HIERRO GRIS, ACERO AL CARBON, ETC.
        /// </example>
        public string TipoDeMaterial { get; set; }

        /// <summary>
        /// Lista de propiedades que contiene la materia prima.
        /// </summary>
        public ObservableCollection<Propiedad> Propiedades { get; set; }

        /// <summary>
        /// Double que representa la cantidad utilizable en cada operación.
        /// </summary>
        public double Cantidad { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Measurement { get; set; }

        #endregion

        #region Construcotres

        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public MateriaPrima()
        {
            Especificacion = new PropiedadCadena();
            Propiedades = new ObservableCollection<Propiedad>();
            Cantidad = 0;
            Measurement = string.Empty;
        }
        #endregion

        #region Métodos
        #endregion
    }
}
