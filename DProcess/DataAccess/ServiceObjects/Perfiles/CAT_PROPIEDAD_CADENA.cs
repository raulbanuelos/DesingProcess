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
    
    public partial class CAT_PROPIEDAD_CADENA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAT_PROPIEDAD_CADENA()
        {
            this.TBL_ARQUETIPO_PROPIEDADES_CADENA = new HashSet<TBL_ARQUETIPO_PROPIEDADES_CADENA>();
            this.TR_PROPIEDAD_CADENA_PERFIL = new HashSet<TR_PROPIEDAD_CADENA_PERFIL>();
        }
    
        public int ID_PROPIEDAD_CADENA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION_LARGA { get; set; }
        public string DESCRIPCION_CORTA { get; set; }
        public byte[] IMAGEN { get; set; }
        public int ID_USUARIO_CREACION { get; set; }
        public System.DateTime FECHA_CREACION { get; set; }
        public Nullable<int> ID_USUARIO_ACTUALIZACION { get; set; }
        public Nullable<System.DateTime> FECHA_ACTUALIZACION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_ARQUETIPO_PROPIEDADES_CADENA> TBL_ARQUETIPO_PROPIEDADES_CADENA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TR_PROPIEDAD_CADENA_PERFIL> TR_PROPIEDAD_CADENA_PERFIL { get; set; }
    }
}
