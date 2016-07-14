namespace Login.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_PRODECT")]
    public partial class TB_PRODECT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_PRODECT()
        {
            TB_CHECK_PRODUCT = new HashSet<TB_CHECK_PRODUCT>();
            TB_COMMENT = new HashSet<TB_COMMENT>();
            TB_CONTAINS = new HashSet<TB_CONTAINS>();
            TB_PRO_PIC = new HashSet<TB_PRO_PIC>();
            TB_USER = new HashSet<TB_USER>();
        }

        [Key]
        [Column(Order = 0)]
        public decimal ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal S_ID { get; set; }

        [StringLength(20)]
        public string TYPE { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        public decimal? PRICE { get; set; }

        public decimal? DISCOUNT_RATE { get; set; }

        [StringLength(20)]
        public string DESCRIPTION { get; set; }

        public decimal? NUM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_CHECK_PRODUCT> TB_CHECK_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_COMMENT> TB_COMMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_CONTAINS> TB_CONTAINS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_PRO_PIC> TB_PRO_PIC { get; set; }

        public virtual TB_STORE TB_STORE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_USER> TB_USER { get; set; }
    }
}
