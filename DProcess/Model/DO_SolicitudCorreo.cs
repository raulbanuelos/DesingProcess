using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DO_SolicitudCorreo
    {
        public int IdSolicitudCorreo { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Recipients { get; set; }
        public bool banEjecutada { get; set; }
        public DateTime FechaEjecutada { get; set; }
        public string Origen { get; set; }
        public int idArchivo { get; set; }
    }
}
