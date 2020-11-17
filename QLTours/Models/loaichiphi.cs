namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("loaichiphi")]
    public partial class loaichiphi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public loaichiphi()
        {
            chiphis = new HashSet<chiphi>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(50)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(1000)]
        public string Mota { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chiphi> chiphis { get; set; }
    }
}
