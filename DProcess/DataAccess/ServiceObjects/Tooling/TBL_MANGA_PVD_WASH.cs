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
    
    public partial class TBL_MANGA_PVD_WASH
    {
        public int ID_MANGA_PVD_WASH { get; set; }
        public string CODIGO { get; set; }
        public double DIM_A { get; set; }
        public double DIM_D { get; set; }
    
        public virtual MaestroHerramentales MaestroHerramentales { get; set; }
    }
}
