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
    
    public partial class TBL_CALCULO_ARQUETIPO
    {
        public int ID_CALCULO_ARQUETIPO { get; set; }
        public string CODIGO { get; set; }
        public string XML_OPERATION { get; set; }
        public Nullable<double> MAT_REMOVE_WIDTH { get; set; }
        public Nullable<double> MAT_REMOVE_THICKNESS { get; set; }
        public Nullable<double> MAT_REMOVE_DIAMETER { get; set; }
        public Nullable<bool> WORK_GAP { get; set; }
        public Nullable<bool> GAP_FIXED { get; set; }
    
        public virtual Arquetipo Arquetipo { get; set; }
    }
}