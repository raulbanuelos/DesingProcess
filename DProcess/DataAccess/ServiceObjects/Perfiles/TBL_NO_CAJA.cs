//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.Perfiles
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_NO_CAJA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_NO_CAJA()
        {
            this.TBL_ROLLOS_CAJA_SEGMENTOS = new HashSet<TBL_ROLLOS_CAJA_SEGMENTOS>();
        }
    
        public int ID_NO_CAJA { get; set; }
        public string NO_CAJA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ROLLOS_CAJA_SEGMENTOS> TBL_ROLLOS_CAJA_SEGMENTOS { get; set; }
    }
}
