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
    
    public partial class CentroTrabajo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CentroTrabajo()
        {
            this.TR_LECCIONES_CENTROSTRABAJO = new HashSet<TR_LECCIONES_CENTROSTRABAJO>();
        }
    
        public string CentroTrabajo1 { get; set; }
        public Nullable<double> TiempoSetup { get; set; }
        public string NombreOperacion { get; set; }
        public string ObjetoXML { get; set; }
        public string ObjetoXMLVista { get; set; }
        public string NombreIngles { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TR_LECCIONES_CENTROSTRABAJO> TR_LECCIONES_CENTROSTRABAJO { get; set; }
    }
}
