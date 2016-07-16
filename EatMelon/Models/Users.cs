namespace EatMelon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Users : DbContext
    {
        public Users()
            : base("name=Users")
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

            modelBuilder.Entity<TB_USER>()
                .Property(e => e.DISTRICT)
                .IsUnicode(false);
        }
    }
}
