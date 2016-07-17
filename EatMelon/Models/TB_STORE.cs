namespace EatMelon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_STORE")]
    public partial class TB_STORE
    {
        public decimal ID { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        public decimal? QUALITY_RATING { get; set; }
    }
}
