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
    
    public partial class TR_PROPIEDAD_OPCIONAL_PERFIL
    {
        public int ID_PROPIEDAD_OPCIONAL_PERFIL { get; set; }
        public Nullable<int> ID_PROPIEDAD_OPCIONAL { get; set; }
        public Nullable<int> ID_PERFIL { get; set; }
    
        public virtual CAT_PERFIL CAT_PERFIL { get; set; }
        public virtual CAT_PROPIEDAD_OPCIONAL CAT_PROPIEDAD_OPCIONAL { get; set; }
    }
}
