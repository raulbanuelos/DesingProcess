//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.Usuario
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAT_MOTIVO_CAMBIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAT_MOTIVO_CAMBIO()
        {
            this.TR_LECCIONES_APRENDIDAS_MOTIVO_CAMBIO = new HashSet<TR_LECCIONES_APRENDIDAS_MOTIVO_CAMBIO>();
        }
    
        public int ID_MOTIVO_CAMBIO { get; set; }
        public string MOTIVO_CAMBIO { get; set; }
        public string DESCRIPCION_CAMBIO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TR_LECCIONES_APRENDIDAS_MOTIVO_CAMBIO> TR_LECCIONES_APRENDIDAS_MOTIVO_CAMBIO { get; set; }
    }
}
