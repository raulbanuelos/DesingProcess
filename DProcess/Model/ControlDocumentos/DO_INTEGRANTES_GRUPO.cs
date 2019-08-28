using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class DO_INTEGRANTES_GRUPO
    {
        public int idintegrantegrupo { get; set; }
        public int idgrupo { get; set; }
        public string idusuariointegrante { get; set; }
        public bool IsSelected { get; set; }
        public string nombrecompleto { get; set; }
    }
}
