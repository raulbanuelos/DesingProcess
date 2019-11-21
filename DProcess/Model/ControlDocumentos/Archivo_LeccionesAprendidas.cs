using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class Archivo_LeccionesAprendidas
    {
        public int ID_ARCHIVO_LECCIONES { get; set; }
        public int ID_LECCIONES_APRENDIDAS { get; set; }
        public byte[] ARCHIVO { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public string rutaIcono { get; set; }
        public string EXT { get; set; } 
        public string rutaArchivo { get; set; }
    }
}
