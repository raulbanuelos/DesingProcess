//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.Operaciones
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_CALCULO_DETALLE
    {
        public int ID_CALCULO_DETALLE { get; set; }
        public string CODIGO { get; set; }
        public Nullable<double> RING_WIDTH { get; set; }
        public Nullable<double> RING_THICKNESS { get; set; }
        public Nullable<double> RING_DIAMETER { get; set; }
        public Nullable<double> RING_GAP { get; set; }
    
        public virtual Arquetipo Arquetipo { get; set; }
    }
}
