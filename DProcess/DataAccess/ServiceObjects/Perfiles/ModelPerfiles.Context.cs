﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesPerfiles : DbContext
    {
        public EntitiesPerfiles()
            : base("name=EntitiesPerfiles")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Arquetipo> Arquetipo { get; set; }
        public virtual DbSet<ArquetipoRings> ArquetipoRings { get; set; }
        public virtual DbSet<CAT_PERFIL> CAT_PERFIL { get; set; }
        public virtual DbSet<CAT_PROPIEDAD> CAT_PROPIEDAD { get; set; }
        public virtual DbSet<CAT_PROPIEDAD_BOOL> CAT_PROPIEDAD_BOOL { get; set; }
        public virtual DbSet<CAT_PROPIEDAD_CADENA> CAT_PROPIEDAD_CADENA { get; set; }
        public virtual DbSet<CAT_TIPO_PERFIL> CAT_TIPO_PERFIL { get; set; }
        public virtual DbSet<ColoresAnillos> ColoresAnillos { get; set; }
        public virtual DbSet<TBL_ARQUETIPO_PROPIEDADES> TBL_ARQUETIPO_PROPIEDADES { get; set; }
        public virtual DbSet<TBL_ARQUETIPO_PROPIEDADES_BOOL> TBL_ARQUETIPO_PROPIEDADES_BOOL { get; set; }
        public virtual DbSet<TBL_ARQUETIPO_PROPIEDADES_CADENA> TBL_ARQUETIPO_PROPIEDADES_CADENA { get; set; }
        public virtual DbSet<TBL_NORMAS> TBL_NORMAS { get; set; }
        public virtual DbSet<TR_NORMAS_ARQUETIPO> TR_NORMAS_ARQUETIPO { get; set; }
        public virtual DbSet<TR_PERFIL_ARQUETIPO> TR_PERFIL_ARQUETIPO { get; set; }
        public virtual DbSet<TR_PROPIEDAD_BOOL_PERFIL> TR_PROPIEDAD_BOOL_PERFIL { get; set; }
        public virtual DbSet<TR_PROPIEDAD_CADENA_PERFIL> TR_PROPIEDAD_CADENA_PERFIL { get; set; }
        public virtual DbSet<TR_PROPIEDAD_PERFIL> TR_PROPIEDAD_PERFIL { get; set; }
        public virtual DbSet<TBL_NO_CAJA> TBL_NO_CAJA { get; set; }
        public virtual DbSet<TBL_PZA_ROLLO_SEGMENTO> TBL_PZA_ROLLO_SEGMENTO { get; set; }
        public virtual DbSet<TBL_ROLLOS_CAJA_SEGMENTOS> TBL_ROLLOS_CAJA_SEGMENTOS { get; set; }
        public virtual DbSet<TBL_ESPEC_PVD_RAILS> TBL_ESPEC_PVD_RAILS { get; set; }
        public virtual DbSet<CAT_OPCION_PROPIEDAD_OPCIONAL> CAT_OPCION_PROPIEDAD_OPCIONAL { get; set; }
        public virtual DbSet<CAT_PROPIEDAD_OPCIONAL> CAT_PROPIEDAD_OPCIONAL { get; set; }
        public virtual DbSet<CAT_TABLA_PROPIEDAD_OPCIONAL> CAT_TABLA_PROPIEDAD_OPCIONAL { get; set; }
        public virtual DbSet<TR_PROPIEDAD_OPCIONAL_PERFIL> TR_PROPIEDAD_OPCIONAL_PERFIL { get; set; }
        public virtual DbSet<TBL_ARQUETIPO_PROPIEDADES_OPCIONAL> TBL_ARQUETIPO_PROPIEDADES_OPCIONAL { get; set; }
        public virtual DbSet<TBL_ESPEC_GAS_NITRIDING_RAILS> TBL_ESPEC_GAS_NITRIDING_RAILS { get; set; }
    }
}
