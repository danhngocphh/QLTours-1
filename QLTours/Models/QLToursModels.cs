namespace QLTours.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QLToursModels : DbContext
    {
        public QLToursModels()
            : base("name=QLToursModels")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<chiphi> chiphis { get; set; }
        public virtual DbSet<chitietTour> chitietTours { get; set; }
        public virtual DbSet<diadiem> diadiems { get; set; }
        public virtual DbSet<doan> doans { get; set; }
        public virtual DbSet<gia> gias { get; set; }
        public virtual DbSet<khachhang> khachhangs { get; set; }
        public virtual DbSet<loai> loais { get; set; }
        public virtual DbSet<loaichiphi> loaichiphis { get; set; }
        public virtual DbSet<nguoidi> nguoidis { get; set; }
        public virtual DbSet<tour> tours { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<diadiem>()
                .HasMany(e => e.chitietTours)
                .WithRequired(e => e.diadiem)
                .HasForeignKey(e => e.IdDiaDiem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<doan>()
                .HasMany(e => e.chiphis)
                .WithRequired(e => e.doan)
                .HasForeignKey(e => e.IdDoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<doan>()
                .HasMany(e => e.nguoidis)
                .WithRequired(e => e.doan)
                .HasForeignKey(e => e.IdDoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<gia>()
                .HasMany(e => e.tours)
                .WithRequired(e => e.gia)
                .HasForeignKey(e => e.IdGiaTour)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<khachhang>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<khachhang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<khachhang>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<loai>()
                .HasMany(e => e.tours)
                .WithRequired(e => e.loai)
                .HasForeignKey(e => e.IdLoaiTour)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<loaichiphi>()
                .HasMany(e => e.chiphis)
                .WithRequired(e => e.loaichiphi)
                .HasForeignKey(e => e.IdLoaiChiPhi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<nguoidi>()
                .Property(e => e.DSNhanvien)
                .IsUnicode(false);

            modelBuilder.Entity<nguoidi>()
                .Property(e => e.DSKhach)
                .IsUnicode(false);

            modelBuilder.Entity<tour>()
                .HasMany(e => e.chitietTours)
                .WithRequired(e => e.tour)
                .HasForeignKey(e => e.IdTour)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tour>()
                .HasMany(e => e.doans)
                .WithRequired(e => e.tour)
                .HasForeignKey(e => e.IdTour)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tour>()
                .HasMany(e => e.gias)
                .WithRequired(e => e.tour)
                .HasForeignKey(e => e.IdTour)
                .WillCascadeOnDelete(false);
        }
    }
}
