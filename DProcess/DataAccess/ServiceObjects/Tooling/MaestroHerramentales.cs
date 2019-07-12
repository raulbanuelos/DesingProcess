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
            this.BarrelLapAnillos_ = new HashSet<BarrelLapAnillos_>();
            this.BushingBatesBore_ = new HashSet<BushingBatesBore_>();
            this.BushingCromo_ = new HashSet<BushingCromo_>();
            this.BushingFinishMill = new HashSet<BushingFinishMill>();
            this.BushingSIM_ = new HashSet<BushingSIM_>();
            this.CamBK_ = new HashSet<CamBK_>();
            this.CamisaMoly_ = new HashSet<CamisaMoly_>();
            this.ChuckSplitter = new HashSet<ChuckSplitter>();
            this.ClosingBandLapeado = new HashSet<ClosingBandLapeado>();
            this.ClosingSleeveBK = new HashSet<ClosingSleeveBK>();
            this.CollarBK = new HashSet<CollarBK>();
            this.CollarMoly_ = new HashSet<CollarMoly_>();
            this.CollarScotchbrite_ = new HashSet<CollarScotchbrite_>();
            this.CollarsCromo_ = new HashSet<CollarsCromo_>();
            this.CollarSpacer = new HashSet<CollarSpacer>();
            this.CutterCamTurn = new HashSet<CutterCamTurn>();
            this.CutterSpacerSplitter = new HashSet<CutterSpacerSplitter>();
            this.CutterSplitter = new HashSet<CutterSplitter>();
            this.FrontRearCollarAnillos_ = new HashSet<FrontRearCollarAnillos_>();
            this.GuideBarFinGrind = new HashSet<GuideBarFinGrind>();
            this.GuideBarFirstRoughGrind = new HashSet<GuideBarFirstRoughGrind>();
            this.GuideBarSecondRoughGrind = new HashSet<GuideBarSecondRoughGrind>();
            this.GuidePlateBK_ = new HashSet<GuidePlateBK_>();
            this.GuillotinaBK_ = new HashSet<GuillotinaBK_>();
            this.GuillotinaEngrave_ = new HashSet<GuillotinaEngrave_>();
            this.GuillotinaSIM_ = new HashSet<GuillotinaSIM_>();
            this.LoadingGuideAnillos_ = new HashSet<LoadingGuideAnillos_>();
            this.ProtectorInfMoly_ = new HashSet<ProtectorInfMoly_>();
            this.ProtectorSupMoly_ = new HashSet<ProtectorSupMoly_>();
            this.PusherSIM_ = new HashSet<PusherSIM_>();
            this.ShieldBK_ = new HashSet<ShieldBK_>();
            this.TBL_COIL_CENTER_GUIDE = new HashSet<TBL_COIL_CENTER_GUIDE>();
            this.TBL_COIL_FEED_ROLLER = new HashSet<TBL_COIL_FEED_ROLLER>();
            this.TBL_EXIT_GUIDE = new HashSet<TBL_EXIT_GUIDE>();
            this.TBL_EXTERNAL_GUIDE_ROLLER_1PIECE = new HashSet<TBL_EXTERNAL_GUIDE_ROLLER_1PIECE>();
            this.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1 = new HashSet<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1>();
            this.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2 = new HashSet<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2>();
            this.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3 = new HashSet<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3>();
            this.TBL_FEED_WHEEL_RECTIFICADOS_FINOS = new HashSet<TBL_FEED_WHEEL_RECTIFICADOS_FINOS>();
            this.TBL_SHIM_OF_THE_CUT_SYSTEM = new HashSet<TBL_SHIM_OF_THE_CUT_SYSTEM>();
            this.UretanoSplitter = new HashSet<UretanoSplitter>();
            this.WorkCam = new HashSet<WorkCam>();
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BarrelLapAnillos_> BarrelLapAnillos_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BushingBatesBore_> BushingBatesBore_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BushingCromo_> BushingCromo_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BushingFinishMill> BushingFinishMill { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BushingSIM_> BushingSIM_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CamBK_> CamBK_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CamisaMoly_> CamisaMoly_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuckSplitter> ChuckSplitter { get; set; }
        public virtual ClasificacionHerramental ClasificacionHerramental { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClosingBandLapeado> ClosingBandLapeado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClosingSleeveBK> ClosingSleeveBK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollarBK> CollarBK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollarMoly_> CollarMoly_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollarScotchbrite_> CollarScotchbrite_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollarsCromo_> CollarsCromo_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollarSpacer> CollarSpacer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CutterCamTurn> CutterCamTurn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CutterSpacerSplitter> CutterSpacerSplitter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CutterSplitter> CutterSplitter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FrontRearCollarAnillos_> FrontRearCollarAnillos_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuideBarFinGrind> GuideBarFinGrind { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuideBarFirstRoughGrind> GuideBarFirstRoughGrind { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuideBarSecondRoughGrind> GuideBarSecondRoughGrind { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuidePlateBK_> GuidePlateBK_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuillotinaBK_> GuillotinaBK_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuillotinaEngrave_> GuillotinaEngrave_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuillotinaSIM_> GuillotinaSIM_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoadingGuideAnillos_> LoadingGuideAnillos_ { get; set; }
        public virtual DiscoDiskus_ DiscoDiskus_ { get; set; }
        public virtual PLANO_HERRAMENTAL PLANO_HERRAMENTAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProtectorInfMoly_> ProtectorInfMoly_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProtectorSupMoly_> ProtectorSupMoly_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PusherSIM_> PusherSIM_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShieldBK_> ShieldBK_ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_COIL_CENTER_GUIDE> TBL_COIL_CENTER_GUIDE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_COIL_FEED_ROLLER> TBL_COIL_FEED_ROLLER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXIT_GUIDE> TBL_EXIT_GUIDE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXTERNAL_GUIDE_ROLLER_1PIECE> TBL_EXTERNAL_GUIDE_ROLLER_1PIECE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1> TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2> TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3> TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_FEED_WHEEL_RECTIFICADOS_FINOS> TBL_FEED_WHEEL_RECTIFICADOS_FINOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_SHIM_OF_THE_CUT_SYSTEM> TBL_SHIM_OF_THE_CUT_SYSTEM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UretanoSplitter> UretanoSplitter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkCam> WorkCam { get; set; }
    }
}
