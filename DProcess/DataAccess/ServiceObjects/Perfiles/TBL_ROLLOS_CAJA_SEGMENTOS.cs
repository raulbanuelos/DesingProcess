//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.Perfiles
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_ROLLOS_CAJA_SEGMENTOS
    {
        public int ID_ROLLOS_CAJA_SEGMENTOS { get; set; }
        public Nullable<int> ID_NO_CAJA { get; set; }
        public Nullable<double> DIAMETRO_MIN { get; set; }
        public Nullable<double> DIAMETRO_MAX { get; set; }
        public Nullable<int> ROLLOS_CAJA { get; set; }
    
        public virtual TBL_NO_CAJA TBL_NO_CAJA { get; set; }
    }
}
