using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class Bloqueo
    {
        #region Propiedades
        public int id_bloqueo { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public int estado { get; set; }
        public string observaciones { get; set; }


        #endregion

    }
}
