using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class DO_Grupos
    {
        public int idgrupo { get; set; }
        public string nombre { get; set; }
        public string idusuariodueno { get; set; }
        public bool IsSelected { get; set; }
    }
}
