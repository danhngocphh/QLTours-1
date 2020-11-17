namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tour")]
    public partial class tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tour()
        {
            chitietTours = new HashSet<chitietTour>();
            doans = new HashSet<doan>();
            gias = new HashSet<gia>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Không được bỏ trống")]
        [StringLength(50)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(1000)]
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int IdLoaiTour { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int IdGiaTour { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietTour> chitietTours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<doan> doans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<gia> gias { get; set; }

        public virtual gia gia { get; set; }

        public virtual loai loai { get; set; }
    }
}
