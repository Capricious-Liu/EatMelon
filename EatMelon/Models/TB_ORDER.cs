namespace EatMelon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_ORDER")]
    public partial class TB_ORDER
    {
        public decimal ID { get; set; }

        public decimal S_ID { get; set; }

        public decimal U_ID { get; set; }

        public DateTime? TIME { get; set; }

        public decimal? STATE { get; set; }

        public decimal? TOTAL_PRICE { get; set; }
    }
}
