namespace EatMelon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Comments : DbContext
    {
        public Comments()
            : base("name=Comments")
        {
        }

        public virtual DbSet<TB_COMMENT> TB_COMMENT { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_COMMENT>()
                .Property(e => e.U_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_COMMENT>()
                .Property(e => e.S_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_COMMENT>()
                .Property(e => e.P_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_COMMENT>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TB_COMMENT>()
                .Property(e => e.O_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_COMMENT>()
                .Property(e => e.ID)
                .HasPrecision(20, 0);
        }
    }
}
