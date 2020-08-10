using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class DO_Solicitud_Control_Documentos
    {
        public int ID_SOLICITUD_CONTROL_DOCUMENTOS { get; set; }
        public int ID_VERSION { get; set; }
        public string ACCION { get; set; }
        public DateTime FECHA_SOLICITUD { get; set; }
        public bool BAN_EJECUTADA { get; set; }
        public DateTime? FECHA_EJECUCION { get; set; }
        public string COMENTARIO { get; set; }
    }
}
