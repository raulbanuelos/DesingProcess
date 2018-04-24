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
        public int id_tipo_documento { get; set; }
        public string  usuario { get; set; }
        public string usuario_autorizo { get; set; }
        public int id_estatus { get; set; }
        public int id_dep { get; set; }
        public Version no_copias { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string Departamento { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public DateTime fecha_emision { get; set; }
        public Version version { get; set; }
        public TipoDocumento tipo { get; set; }
        public ValidacionDocumento validacion { get; set; }
        //int id_documento,string id_usuario,string nombre,string descripcion,string version_actual,
        //DateTime fecha_creacion,DateTime fecha_actualizacion,DateTime fecha_emision

        #endregion

        #region Constructores
        public Documento()
        {
            Departamento = string.Empty;
            id_documento = 0;
            id_tipo_documento = 0;
            usuario = string.Empty;
            usuario_autorizo = string.Empty;
            id_estatus = 0;
            id_dep = 0;
            no_copias = new Version();
            nombre = string.Empty;
            descripcion = string.Empty;
            fecha_creacion = new DateTime();
            fecha_emision = new DateTime();
            fecha_actualizacion = new DateTime();
            version = new Version();
            tipo = new TipoDocumento();
            validacion = new ValidacionDocumento();
        }
        #endregion

    }
}
