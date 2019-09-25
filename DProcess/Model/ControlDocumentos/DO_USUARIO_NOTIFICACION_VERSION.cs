using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class DO_USUARIO_NOTIFICACION_VERSION
    {
        public int id_usuario_notificacion_version { get; set; }
        public string id_usuario { get; set; }
        public int id_version { get; set; }
        public bool IsSelected { get; set; }
    }
}
