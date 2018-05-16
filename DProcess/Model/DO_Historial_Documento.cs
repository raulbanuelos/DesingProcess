using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DO_Historial_Documento
    {
        public int ID_HISTORIAL_VERSION { get; set; }
        public string NOMBRE_DOCUMENTO { get; set; }
        public string NO_VERSION { get; set; }
        public string FECHA { get; set; }
        public string DESCRIPCION { get; set; }
        public string NOMBRE_USUARIO { get; set; }
    }
}
