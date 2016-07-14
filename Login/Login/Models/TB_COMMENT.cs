namespace Login.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_COMMENT")]
    public partial class TB_COMMENT
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal S_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal P_ID { get; set; }

        public decimal? O_ID { get; set; }

        public decimal? U_ID { get; set; }

        public bool? RANK { get; set; }

        [StringLength(100)]
        public string DESCRIPTION { get; set; }

        public virtual TB_PRODECT TB_PRODECT { get; set; }

        public virtual TB_ORDER TB_ORDER { get; set; }

        public virtual TB_USER TB_USER { get; set; }
    }
}
