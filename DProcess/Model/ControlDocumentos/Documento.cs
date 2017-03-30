using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
   public  class Documento
    {
        #region Propiedades
        public int id_documento { get; set; }
        public string id_usuario { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string version_actual { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public DateTime fecha_emision { get; set; }

        //int id_documento,string id_usuario,string nombre,string descripcion,string version_actual,
        //DateTime fecha_creacion,DateTime fecha_actualizacion,DateTime fecha_emision

        #endregion

        #region Constructores
        public Documento()
        {
            id_documento = 0;
            id_usuario = "";
            nombre = "";
            descripcion = "";
            version_actual = "";
            fecha_creacion = new DateTime();
            fecha_actualizacion = new DateTime();
            fecha_emision = new DateTime();
        }

        #endregion
    }
}
