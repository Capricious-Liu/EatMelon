namespace EatMelon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Pro_pics : DbContext
    {
        public Pro_pics()
            : base("name=Pro_pics")
        {
        }

        public virtual DbSet<TB_PRO_PIC> TB_PRO_PIC { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_PRO_PIC>()
                .Property(e => e.P_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_PRO_PIC>()
                .Property(e => e.S_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_PRO_PIC>()
                .Property(e => e.PICTURE)
                .IsUnicode(false);
        }
    }
}
