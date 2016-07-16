namespace EatMelon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_PRO_PIC")]
    public partial class TB_PRO_PIC
    {
        [Key]
        [Column(Order = 0)]
        public decimal P_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal S_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(512)]
        public string PICTURE { get; set; }
    }
}
