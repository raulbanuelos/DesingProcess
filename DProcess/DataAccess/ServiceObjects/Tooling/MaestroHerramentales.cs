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
    
    public partial class MaestroHerramentales
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaestroHerramentales()
        {
            this.CollarBK = new HashSet<CollarBK>();
            this.GuideBarFirstRoughGrind = new HashSet<GuideBarFirstRoughGrind>();
            this.GuideBarSecondRoughGrind = new HashSet<GuideBarSecondRoughGrind>();
            this.CutterSpacerSplitter = new HashSet<CutterSpacerSplitter>();
            this.TBL_EXTERNAL_GUIDE_ROLLER_1PIECE = new HashSet<TBL_EXTERNAL_GUIDE_ROLLER_1PIECE>();
            this.TBL_COIL_CENTER_GUIDE = new HashSet<TBL_COIL_CENTER_GUIDE>();
            this.TBL_COIL_FEED_ROLLER = new HashSet<TBL_COIL_FEED_ROLLER>();
            this.TBL_EXIT_GUIDE = new HashSet<TBL_EXIT_GUIDE>();
            this.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1 = new HashSet<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1>();
            this.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2 = new HashSet<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2>();
            this.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3 = new HashSet<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3>();
            this.TBL_SHIM_OF_THE_CUT_SYSTEM = new HashSet<TBL_SHIM_OF_THE_CUT_SYSTEM>();
        }
    
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaCambio { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioCambio { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> idClasificacionHerramental { get; set; }
        public Nullable<int> idPlano { get; set; }
    
        public virtual ClasificacionHerramental ClasificacionHerramental { get; set; }
        public virtual PLANO_HERRAMENTAL PLANO_HERRAMENTAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollarBK> CollarBK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuideBarFirstRoughGrind> GuideBarFirstRoughGrind { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuideBarSecondRoughGrind> GuideBarSecondRoughGrind { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CutterSpacerSplitter> CutterSpacerSplitter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXTERNAL_GUIDE_ROLLER_1PIECE> TBL_EXTERNAL_GUIDE_ROLLER_1PIECE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_COIL_CENTER_GUIDE> TBL_COIL_CENTER_GUIDE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_COIL_FEED_ROLLER> TBL_COIL_FEED_ROLLER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXIT_GUIDE> TBL_EXIT_GUIDE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1> TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2> TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3> TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_SHIM_OF_THE_CUT_SYSTEM> TBL_SHIM_OF_THE_CUT_SYSTEM { get; set; }
        public virtual CutterSplitter CutterSplitter { get; set; }
    }
}
