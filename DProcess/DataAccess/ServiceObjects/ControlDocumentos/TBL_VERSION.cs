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
    
    public partial class TBL_VERSION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_VERSION()
        {
            this.TBL_ARCHIVO = new HashSet<TBL_ARCHIVO>();
        }
    
        public int ID_VERSION { get; set; }
        public string ID_USUARIO_ELABORO { get; set; }
        public Nullable<int> ID_DOCUMENTO { get; set; }
        public string No_VERSION { get; set; }
        public Nullable<System.DateTime> FECHA_VERSION { get; set; }
        public Nullable<int> NO_COPIAS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ARCHIVO> TBL_ARCHIVO { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        public virtual TBL_DOCUMENTO TBL_DOCUMENTO { get; set; }
    }
}
