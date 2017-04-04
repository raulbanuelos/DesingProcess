using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
   public class TipoDocumento
    {
        #region Propiedades
        public int id_tipo { get; set; }
        public string tipo_documento { get; set; }
        public string abreviatura { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        #endregion
    }
}
