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
    
    public partial class ArquetipoRings
    {
        public int ID_ARQUETIPO_RINGS { get; set; }
        public string Codigo { get; set; }
        public Nullable<double> D1Valor { get; set; }
        public string D1Unidad { get; set; }
        public Nullable<double> H1Valor { get; set; }
        public string H1Unidad { get; set; }
        public Nullable<double> FreeGapValor { get; set; }
        public string FreeGapUnidad { get; set; }
        public Nullable<double> MassValor { get; set; }
        public string MassUnidad { get; set; }
        public Nullable<double> TensionValor { get; set; }
        public string TensionUnidad { get; set; }
        public Nullable<double> TensionTolValor { get; set; }
        public string TensionTolUnidad { get; set; }
        public string NoPlano { get; set; }
        public string CustomerPartNumber { get; set; }
        public string CustomerRevisionLevel { get; set; }
        public string Size1 { get; set; }
        public string TipoAnillo { get; set; }
        public string CustomerDocNo { get; set; }
        public string Treatment { get; set; }
        public string EspecTreatment { get; set; }
        public Nullable<double> HardnessMinValor { get; set; }
        public string HardnessMinUnidad { get; set; }
        public Nullable<double> HardnessMaxValor { get; set; }
        public string HardnessMaxUnidad { get; set; }
        public string EspecMaterialBase { get; set; }
        public Nullable<double> OvalityMinValor { get; set; }
        public string OvalityMinUnidad { get; set; }
        public Nullable<double> OvalityMaxValor { get; set; }
        public string OvalityMaxUnidad { get; set; }
    
        public virtual Arquetipo Arquetipo { get; set; }
    }
}
