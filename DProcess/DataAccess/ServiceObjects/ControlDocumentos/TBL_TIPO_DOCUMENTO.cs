//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_TIPO_DOCUMENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_TIPO_DOCUMENTO()
        {
            this.TBL_CONF_DOCUMENTO = new HashSet<TBL_CONF_DOCUMENTO>();
            this.TBL_DOCUMENTO = new HashSet<TBL_DOCUMENTO>();
            this.TBL_RECURSO_TIPO_DOCUMENTO = new HashSet<TBL_RECURSO_TIPO_DOCUMENTO>();
            this.TR_VALIDACION_TIPO_DOCUMENTO = new HashSet<TR_VALIDACION_TIPO_DOCUMENTO>();
        }
    
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string ABREBIATURA { get; set; }
        public string NUMERO_MATRIZ { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ACTUALIZACION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_CONF_DOCUMENTO> TBL_CONF_DOCUMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_DOCUMENTO> TBL_DOCUMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_RECURSO_TIPO_DOCUMENTO> TBL_RECURSO_TIPO_DOCUMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TR_VALIDACION_TIPO_DOCUMENTO> TR_VALIDACION_TIPO_DOCUMENTO { get; set; }
    }
}
