namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("loai")]
    public partial class loai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public loai()
        {
            tours = new HashSet<tour>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(50)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(1000)]
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tour> tours { get; set; }
    }
}
