namespace EatMelon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_COMMENT")]
    public partial class TB_COMMENT
    {
        public decimal U_ID { get; set; }

        [Key]
        [Column(Order = 0)]
        public decimal S_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal P_ID { get; set; }

        [StringLength(20)]
        public string DESCRIPTION { get; set; }

        public decimal O_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal ID { get; set; }

        public byte RANK { get; set; }
    }
}
