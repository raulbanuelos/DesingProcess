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
    
    public partial class TBL_DOCUMENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_DOCUMENTO()
        {
            this.TBL_VERSION = new HashSet<TBL_VERSION>();
        }
    
        public int ID_DOCUMENTO { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string ID_USUARIO { get; set; }
        public int ID_DEPARTAMENTO { get; set; }
        public Nullable<int> ID_ESTATUS_DOCUMENTO { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<System.DateTime> FECHA_EMISION { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ACTUALIZACION { get; set; }
    
        public virtual TBL_DEPARTAMENTO TBL_DEPARTAMENTO { get; set; }
        public virtual TBL_TIPO_DOCUMENTO TBL_TIPO_DOCUMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_VERSION> TBL_VERSION { get; set; }
        public virtual TBL_ESTATUS_DOCUMENTO TBL_ESTATUS_DOCUMENTO { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
