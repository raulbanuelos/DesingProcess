//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    using System;
    using System.Collections.Generic;
    
    public partial class TR_INTEGRANTES_GRUPO
    {
        public int ID_INTEGRANTES_GRUPO { get; set; }
        public int ID_GRUPO { get; set; }
        public string ID_USUARIO_INTEGRANTE { get; set; }
    
        public virtual TBL_GRUPOS TBL_GRUPOS { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
