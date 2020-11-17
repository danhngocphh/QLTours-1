namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("khachhang")]
    public partial class khachhang
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(30)]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(15)]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(30)]
        public string CMND { get; set; }
    }
}
