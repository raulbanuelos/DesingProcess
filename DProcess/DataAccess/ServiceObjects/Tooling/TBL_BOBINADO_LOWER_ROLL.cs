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
    
    public partial class TBL_BOBINADO_LOWER_ROLL
    {
        public int ID_BOBINADO_LOWER_ROLL { get; set; }
        public string CODIGO { get; set; }
        public string DETALLE_RODILLO { get; set; }
        public string DETALLE_ENGRANE { get; set; }
        public Nullable<double> WIRE_WIDTH_MIN { get; set; }
        public Nullable<double> WIRE_WIDTH_MAX { get; set; }
        public Nullable<double> DIA_MIN { get; set; }
        public Nullable<double> DIA_MAX { get; set; }
        public Nullable<double> SIDE_PLATE_DIA { get; set; }
    
        public virtual MaestroHerramentales MaestroHerramentales { get; set; }
    }
}
