namespace Login.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_ADMINISTRATOR")]
    public partial class TB_ADMINISTRATOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_ADMINISTRATOR()
        {
            TB_CHECK_PRODUCT = new HashSet<TB_CHECK_PRODUCT>();
            TB_CHECK_STORE = new HashSet<TB_CHECK_STORE>();
        }

        public decimal ID { get; set; }

        [StringLength(80)]
        public string PASSWORD { get; set; }

        public bool? AUTHORITY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_CHECK_PRODUCT> TB_CHECK_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_CHECK_STORE> TB_CHECK_STORE { get; set; }
    }
}
