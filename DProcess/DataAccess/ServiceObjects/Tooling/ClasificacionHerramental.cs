//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.Tooling
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClasificacionHerramental
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClasificacionHerramental()
        {
            this.MaestroHerramentales = new HashSet<MaestroHerramentales>();
        }
    
        public int idClasificacion { get; set; }
        public string Descripcion { get; set; }
        public string UnidadMedida { get; set; }
        public Nullable<double> Costo { get; set; }
        public Nullable<int> CantidadUtilizar { get; set; }
        public Nullable<int> VidaUtil { get; set; }
        public Nullable<bool> VerificacionAnual { get; set; }
        public string ListaCotasRevisar { get; set; }
        public string ObjetoXML { get; set; }
        public string TablaDetalles { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaestroHerramentales> MaestroHerramentales { get; set; }
    }
}