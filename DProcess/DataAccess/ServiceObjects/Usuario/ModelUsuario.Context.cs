﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesUsuario : DbContext
    {
        public EntitiesUsuario()
            : base("name=EntitiesUsuario")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PerfilUsuario> PerfilUsuario { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<PrivilegioUsuario> PrivilegioUsuario { get; set; }
        public virtual DbSet<TBL_LECCIONES_APRENDIDAS> TBL_LECCIONES_APRENDIDAS { get; set; }
        public virtual DbSet<TBL_ARCHIVO_LECCIONES> TBL_ARCHIVO_LECCIONES { get; set; }
    }
}
