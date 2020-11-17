namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("gia")]
    public partial class gia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public gia()
        {
            tours = new HashSet<tour>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public double SoTien { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int IdTour { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [Column(TypeName = "date")]
        public DateTime TuNgay { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [Column(TypeName = "date")]
        public DateTime DenNgay { get; set; }

        public virtual tour tour { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tour> tours { get; set; }
    }
}
