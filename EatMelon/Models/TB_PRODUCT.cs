namespace EatMelon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_PRODUCT")]
    public partial class TB_PRODUCT
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal S_ID { get; set; }

        [StringLength(20)]
        public string TYPE { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        public decimal? PRICE { get; set; }

        public decimal? DISCOUNT_RATE { get; set; }

        [StringLength(20)]
        public string DESCRIPTION { get; set; }

        public decimal? NUM { get; set; }
    }
}
