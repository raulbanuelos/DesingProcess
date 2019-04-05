using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class TipoError
    {
        public int ID_NOTIFICACION_ERROR { get; set; }
        public string DESCRIPCION_ERROR { get; set; }
        public bool IsSelected { get; set; }
    }
}
