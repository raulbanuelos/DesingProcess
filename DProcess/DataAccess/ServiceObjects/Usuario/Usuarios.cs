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
    
    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            this.PerfilUsuario = new HashSet<PerfilUsuario>();
            this.PrivilegioUsuario = new HashSet<PrivilegioUsuario>();
            this.TBL_LECCIONES_APRENDIDAS = new HashSet<TBL_LECCIONES_APRENDIDAS>();
            this.TBL_LECCIONES_APRENDIDAS1 = new HashSet<TBL_LECCIONES_APRENDIDAS>();
            this.TBL_USERS_DETAILS = new HashSet<TBL_USERS_DETAILS>();
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
        public string Correo { get; set; }
        public string Pathnsf { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PerfilUsuario> PerfilUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrivilegioUsuario> PrivilegioUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_LECCIONES_APRENDIDAS> TBL_LECCIONES_APRENDIDAS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_LECCIONES_APRENDIDAS> TBL_LECCIONES_APRENDIDAS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_USERS_DETAILS> TBL_USERS_DETAILS { get; set; }
    }
}
