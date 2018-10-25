using System.Collections.ObjectModel;
namespace Model
{
    public class Perfil
    {
        #region Propiedades

        /// <summary>
        /// Entero que representa el ID de la base de datos.
        /// </summary>
        public int idPerfil { get; set; }

        /// <summary>
        /// Cadena que representa el nombre del perfil.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Cadena que representa la descripción del perfil.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Lista de propiedades que contiene el perfil.
        /// </summary>
        public ObservableCollection<Propiedad> Propiedades { get; set; }

        /// <summary>
        /// Lista de propiedades con valores alfanuméricos que contiene el perfil.
        /// </summary>
        public ObservableCollection<PropiedadCadena> PropiedadesCadena { get; set; }

        /// <summary>
        /// Lista de propiedades con valores booleanos que contiene el perfil.
        /// </summary>
        public ObservableCollection<PropiedadBool> PropiedadesBool { get; set; }

        /// <summary>
        /// Instancia de objeto de tipo MateriaPrima que representa si el perfil cuenta con algun recubrimiento.
        /// </summary>
        public MateriaPrima Recubrimiento { get; set; }

        /// <summary>
        /// Arreglo de bytes que representa la imagen del perfil.
        /// </summary>
        public byte[] Imagen { get; set; }

        /// <summary>
        /// Cadena que representa el tipo de anillo.
        /// </summary>
        public string Tipo { get; set; }
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public Perfil()
        {
            //Inicializamos las propiedades con valores por default.
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Imagen = null;
            Tipo = string.Empty;
            Recubrimiento = new MateriaPrima();
            Propiedades = new ObservableCollection<Propiedad>();
            PropiedadesBool = new ObservableCollection<PropiedadBool>();
            PropiedadesCadena = new ObservableCollection<PropiedadCadena>();
        }
        #endregion

        #region Métodos

        #endregion
    }
}
