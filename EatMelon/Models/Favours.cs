namespace EatMelon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Favours : DbContext
    {
        public Favours()
            : base("name=Favours")
        {
        }

        public virtual DbSet<TB_FAVORS> TB_FAVORS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_FAVORS>()
                .Property(e => e.U_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_FAVORS>()
                .Property(e => e.P_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_FAVORS>()
                .Property(e => e.S_ID)
                .HasPrecision(20, 0);
        }
    }
}
