using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DO_DocumentosRechazados
    {
        public string NombreDocumento { get; set; }
        public string NoVersion { get; set; }
        public string DuenoDocumento { get; set; }
        public string Correo { get; set; }
        public string Fecha { get; set; }
        public bool IsSelected { get; set; }
    }
}
