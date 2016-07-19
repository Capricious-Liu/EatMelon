namespace EatMelon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Orders : DbContext
    {
        public Orders()
            : base("name=Orders1")
        {
        }

        public virtual DbSet<TB_ORDER> TB_ORDER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_ORDER>()
                .Property(e => e.ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_ORDER>()
                .Property(e => e.S_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_ORDER>()
                .Property(e => e.U_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_ORDER>()
                .Property(e => e.STATE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TB_ORDER>()
                .Property(e => e.TOTAL_PRICE)
                .HasPrecision(15, 2);
        }
    }
}
