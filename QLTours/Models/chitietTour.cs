namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("chitietTour")]
    public partial class chitietTour
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int IdTour { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int IdDiaDiem { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int STTDiaDiem { get; set; }

        public virtual diadiem diadiem { get; set; }

        public virtual tour tour { get; set; }
    }
}
