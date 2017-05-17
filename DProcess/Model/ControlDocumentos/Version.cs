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
        public Archivo archivo { get; set; }
        public string id_usuario_autorizo { get; set; }
        #endregion

        #region Constructores

        public Version()
        {
            id_version = 0;
            id_usuario = string.Empty;
            id_documento = 0;
            no_version = string.Empty;
            fecha_version = new DateTime();
            no_copias = 0;
            archivo = new Archivo();
            id_usuario_autorizo = string.Empty;
        }
        #endregion

        //int id_version,string id_usuario,int id_documento,string no_version,DateTime fecha,int no_copias
    }
}
