namespace EatMelon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Contains : DbContext
    {
        public Contains()
            : base("name=Contains1")
        {
        }

        public virtual DbSet<TB_CONTAINS> TB_CONTAINS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_CONTAINS>()
                .Property(e => e.O_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_CONTAINS>()
                .Property(e => e.P_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_CONTAINS>()
                .Property(e => e.S_ID)
                .HasPrecision(20, 0);
        }
    }
}
