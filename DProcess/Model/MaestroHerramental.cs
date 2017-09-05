using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MaestroHerramental
    {
        public string Codigo { get; set; }
        public string descripcion { get; set; }
        public string fecha_creacion { get; set; }
        public string fecha_cambio { get; set; }
        public string usuario_creacion  { get; set; }
        public string usuario_cambio { get; set; }
        public bool activo { get; set; }
        public int id_clasificacion { get; set; }
        public int id_plano { get; set; }
    }
}
