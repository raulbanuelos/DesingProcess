﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesControlDocumentos : DbContext
    {
        public EntitiesControlDocumentos()
            : base("name=EntitiesControlDocumentos")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TBL_ARCHIVO> TBL_ARCHIVO { get; set; }
        public virtual DbSet<TBL_CONF_DOCUMENTO> TBL_CONF_DOCUMENTO { get; set; }
        public virtual DbSet<TBL_DEPARTAMENTO> TBL_DEPARTAMENTO { get; set; }
        public virtual DbSet<TBL_ESTATUS_DOCUMENTO> TBL_ESTATUS_DOCUMENTO { get; set; }
        public virtual DbSet<TBL_ESTATUS_VERSION> TBL_ESTATUS_VERSION { get; set; }
        public virtual DbSet<TBL_ROL> TBL_ROL { get; set; }
        public virtual DbSet<TR_ROL_USUARIOS> TR_ROL_USUARIOS { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<TBL_VALIDACION_DOCUMENTO> TBL_VALIDACION_DOCUMENTO { get; set; }
        public virtual DbSet<TR_VALIDACION_TIPO_DOCUMENTO> TR_VALIDACION_TIPO_DOCUMENTO { get; set; }
        public virtual DbSet<TBL_BLOQUEO> TBL_BLOQUEO { get; set; }
        public virtual DbSet<TBL_DOCUMENTO> TBL_DOCUMENTO { get; set; }
        public virtual DbSet<TBL_VERSION> TBL_VERSION { get; set; }
        public virtual DbSet<TBL_RECURSO_TIPO_DOCUMENTO> TBL_RECURSO_TIPO_DOCUMENTO { get; set; }
        public virtual DbSet<TBL_TIPO_DOCUMENTO> TBL_TIPO_DOCUMENTO { get; set; }
        public virtual DbSet<TBL_HISTORIAL_VERSION> TBL_HISTORIAL_VERSION { get; set; }
    }
}
