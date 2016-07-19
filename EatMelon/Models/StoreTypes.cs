namespace EatMelon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StoreTypes : DbContext
    {
        public StoreTypes()
            : base("name=StoreTypes")
        {
        }

        public virtual DbSet<TB_STORE_TYPE> TB_STORE_TYPE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_STORE_TYPE>()
                .Property(e => e.ID)
                .HasPrecision(20, 0);

            modelBuilder.Entity<TB_STORE_TYPE>()
                .Property(e => e.TYPE)
                .IsUnicode(false);
        }
    }
}
