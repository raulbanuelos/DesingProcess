using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class Archivo
    {
        #region Propiedades
        public int id_archivo { get; set; }
        public int id_version { get; set; }
        public byte[] archivo{ get; set; }
        public string ext { get; set; }
        public string nombre { get; set; }
        //int id_archivo,int id_version,byte[] archivo,string ext
        public string ruta { get; set; }
        public int numero { get; set; }
        #endregion

        #region Constructor

        #endregion
    }
}
