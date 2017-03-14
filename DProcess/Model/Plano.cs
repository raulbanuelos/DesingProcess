using System;

namespace Model
{
    public class Plano
    {
        #region Propiedades
        public int idPlano { get; set; }
        public string NoPlano { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        #endregion

        #region Constructores
        public Plano()
        {
        }
        #endregion

        #region Métodos
        #endregion
    }
}
