namespace EatMelon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Stores : DbContext
    {
        public Stores()
            : base("name=Stores")
        {
        }

        public virtual DbSet<TB_STORE> TB_STORE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_STORE>()
                .Property(e => e.ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_STORE>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TB_STORE>()
                .Property(e => e.QUALITY_RATING)
                .HasPrecision(3, 2);
        }
    }
}
