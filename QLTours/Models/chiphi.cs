namespace QLTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("chiphi")]
    public partial class chiphi
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int IdDoan { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public int IdLoaiChiPhi { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        public double Total { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(1000)]
        public string ChiTiet { get; set; }

        public virtual doan doan { get; set; }

        public virtual loaichiphi loaichiphi { get; set; }
    }
}
