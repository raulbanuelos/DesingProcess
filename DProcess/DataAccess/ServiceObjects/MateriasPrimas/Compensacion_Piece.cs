//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    using System;
    using System.Collections.Generic;
    
    public partial class Compensacion_Piece
    {
        public int IdCompensacionPiece { get; set; }
        public string IdMaterial { get; set; }
        public Nullable<int> IdTipoAnillo { get; set; }
        public Nullable<double> Compensacion { get; set; }
    
        public virtual material material { get; set; }
        public virtual Tipo_Anillo Tipo_Anillo { get; set; }
    }
}
