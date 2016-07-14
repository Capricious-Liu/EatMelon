namespace Login.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_ORDER")]
    public partial class TB_ORDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_ORDER()
        {
            TB_COMMENT = new HashSet<TB_COMMENT>();
        }

        public decimal ID { get; set; }

        public decimal S_ID { get; set; }

        public decimal U_ID { get; set; }

        public DateTime? TIME { get; set; }

        public bool? STATE { get; set; }

        public decimal? TOTAL_PRICE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_COMMENT> TB_COMMENT { get; set; }

        public virtual TB_STORE TB_STORE { get; set; }

        public virtual TB_USER TB_USER { get; set; }
    }
}
