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
    
    public partial class CollarMoly_
    {
        public int Id_CollarMoly { get; set; }
        public string Codigo { get; set; }
        public Nullable<double> DimA { get; set; }
        public string Plano { get; set; }
    
        public virtual MaestroHerramentales MaestroHerramentales { get; set; }
    }
}
