//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.Perfiles
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAT_PROPIEDAD_OPCIONAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAT_PROPIEDAD_OPCIONAL()
        {
            this.CAT_OPCION_PROPIEDAD_OPCIONAL = new HashSet<CAT_OPCION_PROPIEDAD_OPCIONAL>();
            this.CAT_TABLA_PROPIEDAD_OPCIONAL = new HashSet<CAT_TABLA_PROPIEDAD_OPCIONAL>();
            this.TR_PROPIEDAD_OPCIONAL_PERFIL = new HashSet<TR_PROPIEDAD_OPCIONAL_PERFIL>();
            this.TBL_ARQUETIPO_PROPIEDADES_OPCIONAL = new HashSet<TBL_ARQUETIPO_PROPIEDADES_OPCIONAL>();
        }
    
        public int ID_PROPIEDAD_OPCIONAL { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION_LARGA { get; set; }
        public string DESCRIPCION_CORTA { get; set; }
        public byte[] IMAGEN { get; set; }
        public Nullable<int> SOURCE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAT_OPCION_PROPIEDAD_OPCIONAL> CAT_OPCION_PROPIEDAD_OPCIONAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAT_TABLA_PROPIEDAD_OPCIONAL> CAT_TABLA_PROPIEDAD_OPCIONAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TR_PROPIEDAD_OPCIONAL_PERFIL> TR_PROPIEDAD_OPCIONAL_PERFIL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ARQUETIPO_PROPIEDADES_OPCIONAL> TBL_ARQUETIPO_PROPIEDADES_OPCIONAL { get; set; }
    }
}
