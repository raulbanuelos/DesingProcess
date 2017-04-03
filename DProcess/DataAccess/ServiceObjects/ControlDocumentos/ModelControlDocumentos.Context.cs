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
        public virtual DbSet<TBL_DEPARTAMENTO> TBL_DEPARTAMENTO { get; set; }
        public virtual DbSet<TBL_DOCUMENTO> TBL_DOCUMENTO { get; set; }
        public virtual DbSet<TBL_ROL> TBL_ROL { get; set; }
        public virtual DbSet<TBL_VERSION> TBL_VERSION { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
