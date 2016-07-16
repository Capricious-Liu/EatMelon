namespace EatMelon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Products : DbContext
    {
        public Products()
            : base("name=Products")
        {
        }

        public virtual DbSet<TB_PRODUCT> TB_PRODUCT { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_PRODUCT>()
                .Property(e => e.ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_PRODUCT>()
                .Property(e => e.S_ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_PRODUCT>()
                .Property(e => e.TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<TB_PRODUCT>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TB_PRODUCT>()
                .Property(e => e.PRICE)
                .HasPrecision(15, 2);

            modelBuilder.Entity<TB_PRODUCT>()
                .Property(e => e.DISCOUNT_RATE)
                .HasPrecision(2, 2);

            modelBuilder.Entity<TB_PRODUCT>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TB_PRODUCT>()
                .Property(e => e.NUM)
                .HasPrecision(20, 0);
        }
    }
}
