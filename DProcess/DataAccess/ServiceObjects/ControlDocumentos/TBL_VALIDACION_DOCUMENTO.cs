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
    
    public partial class TBL_VALIDACION_DOCUMENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_VALIDACION_DOCUMENTO()
        {
            this.TR_VALIDACION_TIPO_DOCUMENTO = new HashSet<TR_VALIDACION_TIPO_DOCUMENTO>();
            this.TBL_VALIDACION_VERSION = new HashSet<TBL_VALIDACION_VERSION>();
        }
    
        public int ID_VALIDACION_DOCUMENTO { get; set; }
        public string VALIDACION_DOCUMENTO { get; set; }
        public string VALIDACION_DESCRIPCION { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ACTUALIZACION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TR_VALIDACION_TIPO_DOCUMENTO> TR_VALIDACION_TIPO_DOCUMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_VALIDACION_VERSION> TBL_VALIDACION_VERSION { get; set; }
    }
}
