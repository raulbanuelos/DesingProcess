namespace Model
{
    public class Arquetipo
    {
        /// <summary>
		/// Cadena que representa el código general de algún elemento existente en sistema ERP.
		/// </summary>
		public string Codigo { get; set; }

        /// <summary>
        /// Cadena que representa la descripción general del elemento existente en sistema ERP.
        /// </summary>
        public string DescripcionGeneral { get; set; }

        /// <summary>
        /// Arreglo de Bytes que representa una imagen correspondiente al elemento.
        /// </summary>
        public byte[] Imagen { get; set; }

        /// <summary>
        /// Booleano que representa si el elemento esta activo: true, o baja: false.
        /// </summary>
        public bool Activo { get; set; }
    }
}