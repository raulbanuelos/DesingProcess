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
    
    public partial class CollarSpacer
    {
        public int Id_CollarSpacer { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Plano { get; set; }
        public string MedidaNominal { get; set; }
        public Nullable<double> DimE { get; set; }
        public Nullable<double> DimF { get; set; }
        public Nullable<int> ident { get; set; }
    
        public virtual MaestroHerramentales MaestroHerramentales { get; set; }
    }
}