//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.Usuario
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_TIPOCAMBIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_TIPOCAMBIO()
        {
            this.TR_LECCIONES_TIPOCAMBIO = new HashSet<TR_LECCIONES_TIPOCAMBIO>();
        }
    
        public int ID_TIPOCAMBIO { get; set; }
        public string NOMBRETIPOCAMBIO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TR_LECCIONES_TIPOCAMBIO> TR_LECCIONES_TIPOCAMBIO { get; set; }
    }
}
