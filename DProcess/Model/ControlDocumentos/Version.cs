using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class Version
    {
        #region Propiedades
        public int id_version { get; set; }
        public string id_usuario { get; set; }
        public int id_documento { get; set; }
        public string no_version { get; set; }
        public DateTime fecha_version { get; set; }
        public int no_copias { get; set; }
        #endregion

        #region Constructores
       

        #endregion

        //int id_version,string id_usuario,int id_documento,string no_version,DateTime fecha,int no_copias
    }
}
