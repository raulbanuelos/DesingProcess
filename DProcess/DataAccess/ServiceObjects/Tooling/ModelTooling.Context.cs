﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesTooling : DbContext
    {
        public EntitiesTooling()
            : base("name=EntitiesTooling")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SplitterSpacerChart> SplitterSpacerChart { get; set; }
        public virtual DbSet<SPlitterSpacerChart2> SPlitterSpacerChart2 { get; set; }
        public virtual DbSet<cutter_angle> cutter_angle { get; set; }
        public virtual DbSet<DiscoDiskus_> DiscoDiskus_ { get; set; }
        public virtual DbSet<GuidePlateBK_> GuidePlateBK_ { get; set; }
        public virtual DbSet<TBL_EXIT_GUIDE> TBL_EXIT_GUIDE { get; set; }
        public virtual DbSet<CollarsCromo_> CollarsCromo_ { get; set; }
        public virtual DbSet<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2> TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2 { get; set; }
        public virtual DbSet<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3> TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3 { get; set; }
        public virtual DbSet<TBL_EXTERNAL_GUIDE_ROLLER_1PIECE> TBL_EXTERNAL_GUIDE_ROLLER_1PIECE { get; set; }
        public virtual DbSet<TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1> TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1 { get; set; }
        public virtual DbSet<TBL_FEED_WHEEL_RECTIFICADOS_FINOS> TBL_FEED_WHEEL_RECTIFICADOS_FINOS { get; set; }
        public virtual DbSet<TBL_COIL_CENTER_GUIDE> TBL_COIL_CENTER_GUIDE { get; set; }
        public virtual DbSet<TBL_COIL_FEED_ROLLER> TBL_COIL_FEED_ROLLER { get; set; }
        public virtual DbSet<TBL_SHIM_OF_THE_CUT_SYSTEM> TBL_SHIM_OF_THE_CUT_SYSTEM { get; set; }
        public virtual DbSet<CollarBK> CollarBK { get; set; }
        public virtual DbSet<CriDiaGuillBK> CriDiaGuillBK { get; set; }
        public virtual DbSet<CriteriosAnillos> CriteriosAnillos { get; set; }
        public virtual DbSet<PLANO_HERRAMENTAL> PLANO_HERRAMENTAL { get; set; }
        public virtual DbSet<BushingCromo_> BushingCromo_ { get; set; }
        public virtual DbSet<CollarScotchbrite_> CollarScotchbrite_ { get; set; }
        public virtual DbSet<LoadingGuideAnillos_> LoadingGuideAnillos_ { get; set; }
        public virtual DbSet<ClosingBandLapeado> ClosingBandLapeado { get; set; }
        public virtual DbSet<GuillotinaEngrave_> GuillotinaEngrave_ { get; set; }
        public virtual DbSet<FrontRearCollarAnillos_> FrontRearCollarAnillos_ { get; set; }
        public virtual DbSet<BarrelLapAnillos_> BarrelLapAnillos_ { get; set; }
        public virtual DbSet<ProtectorSupMoly_> ProtectorSupMoly_ { get; set; }
        public virtual DbSet<ProtectorInfMoly_> ProtectorInfMoly_ { get; set; }
        public virtual DbSet<CollarMoly_> CollarMoly_ { get; set; }
        public virtual DbSet<CamisaMoly_> CamisaMoly_ { get; set; }
        public virtual DbSet<GuillotinaSIM_> GuillotinaSIM_ { get; set; }
        public virtual DbSet<PusherSIM_> PusherSIM_ { get; set; }
        public virtual DbSet<BushingSIM_> BushingSIM_ { get; set; }
        public virtual DbSet<ShieldBK_> ShieldBK_ { get; set; }
        public virtual DbSet<CamBK_> CamBK_ { get; set; }
        public virtual DbSet<BushingFinishMill> BushingFinishMill { get; set; }
        public virtual DbSet<BushingBatesBore_> BushingBatesBore_ { get; set; }
        public virtual DbSet<WorkCam> WorkCam { get; set; }
        public virtual DbSet<CutterCamTurn> CutterCamTurn { get; set; }
        public virtual DbSet<CollarSpacer> CollarSpacer { get; set; }
        public virtual DbSet<ClosingSleeveBK> ClosingSleeveBK { get; set; }
        public virtual DbSet<CutterSpacerSplitter> CutterSpacerSplitter { get; set; }
        public virtual DbSet<CutterSplitter> CutterSplitter { get; set; }
        public virtual DbSet<GuideBarFirstRoughGrind> GuideBarFirstRoughGrind { get; set; }
        public virtual DbSet<ChuckSplitter> ChuckSplitter { get; set; }
        public virtual DbSet<GuideBarFinGrind> GuideBarFinGrind { get; set; }
        public virtual DbSet<GuideBarSecondRoughGrind> GuideBarSecondRoughGrind { get; set; }
        public virtual DbSet<GuillotinaBK_> GuillotinaBK_ { get; set; }
        public virtual DbSet<CriGPBK> CriGPBK { get; set; }
        public virtual DbSet<CriGillBK> CriGillBK { get; set; }
        public virtual DbSet<UretanoSplitter> UretanoSplitter { get; set; }
        public virtual DbSet<ClasificacionHerramental> ClasificacionHerramental { get; set; }
        public virtual DbSet<MaestroHerramentales> MaestroHerramentales { get; set; }
        public virtual DbSet<TBL_MANGA_PVD_ACERO_CARBON> TBL_MANGA_PVD_ACERO_CARBON { get; set; }
        public virtual DbSet<TBL_MANGA_PVD_ACERO_INOXIDABLE> TBL_MANGA_PVD_ACERO_INOXIDABLE { get; set; }
    }
}
