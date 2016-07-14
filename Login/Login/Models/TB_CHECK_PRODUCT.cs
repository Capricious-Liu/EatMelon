namespace Login.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_CHECK_PRODUCT")]
    public partial class TB_CHECK_PRODUCT
    {
        [Key]
        [Column(Order = 0)]
        public decimal P_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal A_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal S_ID { get; set; }

        public DateTime? TIME { get; set; }

        [StringLength(20)]
        public string STATE { get; set; }

        public virtual TB_ADMINISTRATOR TB_ADMINISTRATOR { get; set; }

        public virtual TB_PRODECT TB_PRODECT { get; set; }
    }
}
