using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
   public class HistorialVersion
    {
        #region Propiedades
        public int id_historial { get; set; }
        public string nombre_documento { get; set; }
        public string no_version { get; set; }
        public DateTime fecha { get; set; }
        public string descripcion { get; set; }
        public string  Nombre_usuario { get; set; }
        public int cantidad { get; set; }
        #endregion
    }
}
