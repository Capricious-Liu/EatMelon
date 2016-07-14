namespace Login.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_DECORATION")]
    public partial class TB_DECORATION
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string FILE_NAME { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal ID { get; set; }

        public virtual TB_STORE TB_STORE { get; set; }
    }
}
