namespace Model
{
    public class Cliente
    {
        #region Propiedades
        /// <summary>
        /// Entero que representa el id del cliente.
        /// </summary>
        public int IdCliente { get; set; }

        /// <summary>
        /// Cadena que representa el nombre del cliente.
        /// </summary>
        public string NombreCliente { get; set; }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por default. Inicializa todas las propiedades con valores por default.
        /// </summary>
        public Cliente()
        {
            //Asignamos a las propiedades los valores por default.
            IdCliente = 0;
            NombreCliente = string.Empty;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            //Retornamos el nombre del cliente.
            return NombreCliente;
        }

        #endregion
    }
}
