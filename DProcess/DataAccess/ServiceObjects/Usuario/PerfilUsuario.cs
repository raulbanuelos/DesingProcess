//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.Usuario
{
    using System;
    using System.Collections.Generic;
    
    public partial class PerfilUsuario
    {
        public int ID_PERFIL_USUARIO { get; set; }
        public string ID_USUARIO { get; set; }
        public Nullable<bool> RGP { get; set; }
        public Nullable<bool> TOOLING { get; set; }
        public Nullable<bool> RAW_MATERIAL { get; set; }
        public Nullable<bool> STANDAR_TIME { get; set; }
        public Nullable<bool> QUOTES { get; set; }
        public Nullable<bool> CIT { get; set; }
        public Nullable<bool> DATA { get; set; }
        public Nullable<bool> USER_PROFILE { get; set; }
        public Nullable<bool> HELP { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}