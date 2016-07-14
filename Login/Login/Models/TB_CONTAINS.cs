namespace Login.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_CONTAINS")]
    public partial class TB_CONTAINS
    {
        [Key]
        [Column(Order = 0)]
        public decimal O_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal P_ID { get; set; }

        public short? NUM { get; set; }

        public decimal S_ID { get; set; }

        public virtual TB_PRODECT TB_PRODECT { get; set; }
    }
}
