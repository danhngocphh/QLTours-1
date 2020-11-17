namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("nguoidi")]
    public partial class nguoidi
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int IdDoan { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(500)]
        public string DSNhanvien { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(500)]
        public string DSKhach { get; set; }

        public virtual doan doan { get; set; }
    }
}
