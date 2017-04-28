using System;
using System.Collections.Generic;
using System.IO;
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
        public int id_tipo_documento { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string version_actual { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public DateTime fecha_emision { get; set; }
        public Version version { get; set; }
        public Usuarios Usuarios { get; set; }
        public TipoDocumento tipo { get; set; }
        //int id_documento,string id_usuario,string nombre,string descripcion,string version_actual,
        //DateTime fecha_creacion,DateTime fecha_actualizacion,DateTime fecha_emision

        #endregion

        #region Constructores
        public Documento()
        {
            id_documento = 0;
            id_usuario = string.Empty;
            id_tipo_documento = 0;
            nombre = string.Empty;
            descripcion = string.Empty;
            version_actual = string.Empty;
            fecha_creacion = new DateTime();
            fecha_emision = new DateTime();
            fecha_actualizacion = new DateTime();
            version = new Version();
            Usuarios = new Usuarios();
            tipo = new TipoDocumento();
        }
        #endregion


    }
}
