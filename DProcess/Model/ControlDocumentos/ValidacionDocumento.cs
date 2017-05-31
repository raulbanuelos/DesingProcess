using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class ValidacionDocumento
    {
        #region Propiedades
        public int id_validacion { get; set; }
        public string validacion_documento { get; set; }
        public string validacion_descripcion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public bool selected { get; set; }

        #endregion
    }
}
