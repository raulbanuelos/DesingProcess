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
    
    public partial class TBL_COIL_FEED_ROLLER
    {
        public int ID_COIL_FEED_ROLLER { get; set; }
        public string CODIGO { get; set; }
        public string CODE { get; set; }
        public Nullable<double> DIMA { get; set; }
        public Nullable<double> DIMB { get; set; }
        public Nullable<double> DIMC { get; set; }
        public Nullable<double> DIMD { get; set; }
        public double WIRE_WIDTH_MIN { get; set; }
        public double WIRE_WIDTH_MAX { get; set; }
    
        public virtual MaestroHerramentales MaestroHerramentales { get; set; }
    }
}