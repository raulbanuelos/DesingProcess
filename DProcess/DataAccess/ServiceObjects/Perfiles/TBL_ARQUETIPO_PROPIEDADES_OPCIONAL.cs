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
    
    public partial class TBL_ARQUETIPO_PROPIEDADES_OPCIONAL
    {
        public int ID_ARQUETIPO_PROPIEDAD_OPCIONAL { get; set; }
        public string CODIGO { get; set; }
        public Nullable<int> ID_PROPIEDAD_OPCIONA { get; set; }
        public string VALOR { get; set; }
    
        public virtual Arquetipo Arquetipo { get; set; }
        public virtual CAT_PROPIEDAD_OPCIONAL CAT_PROPIEDAD_OPCIONAL { get; set; }
    }
}
