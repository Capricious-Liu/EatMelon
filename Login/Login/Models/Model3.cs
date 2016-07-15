namespace Login.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model3 : DbContext
    {
        public Model3()
            : base("name=Model3")
        {
        }

        public virtual DbSet<TB_USER> TB_USER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_USER>()
                .Property(e => e.ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_USER>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<TB_USER>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TB_USER>()
                .Property(e => e.DETAILADDR)
                .IsUnicode(false);

            modelBuilder.Entity<TB_USER>()
                .Property(e => e.CITY)
                .IsUnicode(false);

            modelBuilder.Entity<TB_USER>()
                .Property(e => e.PROVINCE)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<Login.Models.TB_STORE_TYPE> TB_STORE_TYPE { get; set; }

        public System.Data.Entity.DbSet<Login.Models.TB_STORE> TB_STORE { get; set; }
    }
}
