namespace EatMelon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_USER")]
    public partial class TB_USER
    {
        public decimal ID { get; set; }

        [Required]
        [StringLength(20)]
        public string PASSWORD { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        [StringLength(60)]
        public string DETAILADDR { get; set; }

        [StringLength(20)]
        public string CITY { get; set; }

        [StringLength(20)]
        public string PROVINCE { get; set; }

        public int? ZIPCODE { get; set; }

        public long? PHONE { get; set; }

        public int? POINT { get; set; }

        public long? CREDIT_NO { get; set; }

        [StringLength(20)]
        public string DISTRICT { get; set; }
    }
}
