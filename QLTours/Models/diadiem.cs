namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("diadiem")]
    public partial class diadiem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public diadiem()
        {
            chitietTours = new HashSet<chitietTour>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(50)]
        public string ThanhPho { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(30)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(1000)]
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietTour> chitietTours { get; set; }
    }
}
