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
    
    public partial class TR_PERFIL_ARQUETIPO
    {
        public int ID_PERFIL_ARQUETIPO { get; set; }
        public string CODIGO { get; set; }
        public int ID_PERFIL { get; set; }
    
        public virtual Arquetipo Arquetipo { get; set; }
        public virtual CAT_PERFIL CAT_PERFIL { get; set; }
    }
}
