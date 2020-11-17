namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("doan")]
    public partial class doan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public doan()
        {
            chiphis = new HashSet<chiphi>();
            nguoidis = new HashSet<nguoidi>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int IdTour { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(100)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [Column(TypeName = "date")]
        public DateTime NgayDi { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [Column(TypeName = "date")]
        public DateTime NgayVe { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(3000)]
        public string ChiTietChuongTrinh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chiphi> chiphis { get; set; }

        public virtual tour tour { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<nguoidi> nguoidis { get; set; }
    }
}
