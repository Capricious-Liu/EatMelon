namespace Login.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##WUDI.TB_STORE")]
    public partial class TB_STORE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_STORE()
        {
            TB_CHECK_STORE = new HashSet<TB_CHECK_STORE>();
            TB_DECORATION = new HashSet<TB_DECORATION>();
            TB_MANAGE = new HashSet<TB_MANAGE>();
            TB_ORDER = new HashSet<TB_ORDER>();
            TB_PRODECT = new HashSet<TB_PRODECT>();
            TB_STORE_TYPE = new HashSet<TB_STORE_TYPE>();
        }

        public decimal ID { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        public decimal? QUALITY_RATING { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_CHECK_STORE> TB_CHECK_STORE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_DECORATION> TB_DECORATION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_MANAGE> TB_MANAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_ORDER> TB_ORDER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_PRODECT> TB_PRODECT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_STORE_TYPE> TB_STORE_TYPE { get; set; }
    }
}
