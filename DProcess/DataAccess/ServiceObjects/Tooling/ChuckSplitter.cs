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
    
    public partial class ChuckSplitter
    {
        public int Id_Chuck { get; set; }
        public string Codigo { get; set; }
        public double DiaMin { get; set; }
        public double DiaMax { get; set; }
        public string TipoEnsamble { get; set; }
        public int ID_CHUCK_SPLITTER { get; set; }
        public int Id_Chuck1 { get; set; }
    
        public virtual MaestroHerramentales MaestroHerramentales { get; set; }
    }
}
