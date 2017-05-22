using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class EstatusDocumento
    {
        #region Propiedades
        public int id_estatus_documento { get; set; }
        public string estatus_documento { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }

        #endregion
    }
}
