namespace EatMelon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Manages : DbContext
    {
        public Manages()
            : base("name=Manages")
        {
        }

        public virtual DbSet<TB_MANAGE> TB_MANAGE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_MANAGE>()
                .Property(e => e.U_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_MANAGE>()
                .Property(e => e.S_ID)
                .HasPrecision(20, 0);
        }
    }
}
