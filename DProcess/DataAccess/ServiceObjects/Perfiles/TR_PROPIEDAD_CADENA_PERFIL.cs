//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.Perfiles
{
    using System;
    using System.Collections.Generic;
    
    public partial class TR_PROPIEDAD_CADENA_PERFIL
    {
        public int ID_PROPIEDAD_CADENA_PERFIL { get; set; }
        public int ID_PROPIEDAD_CADENA { get; set; }
        public int ID_PERFIL { get; set; }
    
        public virtual CAT_PERFIL CAT_PERFIL { get; set; }
        public virtual CAT_PROPIEDAD_CADENA CAT_PROPIEDAD_CADENA { get; set; }
    }
}
