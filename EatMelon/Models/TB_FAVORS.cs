namespace EatMelon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_FAVORS")]
    public partial class TB_FAVORS
    {
        [Key]
        [Column(Order = 0)]
        public decimal U_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal P_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal S_ID { get; set; }
    }
}
