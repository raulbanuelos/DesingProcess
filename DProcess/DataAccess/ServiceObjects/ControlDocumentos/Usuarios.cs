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
    
    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            this.TBL_DOCUMENTO = new HashSet<TBL_DOCUMENTO>();
            this.TBL_VERSION = new HashSet<TBL_VERSION>();
        }
    
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public Nullable<int> Estado { get; set; }
        public string Usql { get; set; }
        public string Psql { get; set; }
        public Nullable<bool> Bloqueado { get; set; }
        public Nullable<int> Id_Departamento { get; set; }
    
        public virtual TBL_DEPARTAMENTO TBL_DEPARTAMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_DOCUMENTO> TBL_DOCUMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_VERSION> TBL_VERSION { get; set; }
    }
}
