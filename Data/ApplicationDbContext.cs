using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DACS.Models;

namespace DACS.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<BoNho> BONHO { get; set; }
        public DbSet<HinhAnh> HINHANH { get; set; }
        public DbSet<IMac> IMAC { get; set; }
        public DbSet<Ipad> IPAD { get; set; }
        public DbSet<Iphone> IPHONE { get; set; }
        public DbSet<Laptop> LAPTOP { get; set; }
        public DbSet<LoaiSanPham> LOAISANPHAM { get; set; }
        public DbSet<MauSac> MAUSAC { get; set; }
        public DbSet<Ram> RAM { get; set; }
        public DbSet<ThuongHieu> THUONGHIEU { get; set; }
        public DbSet<SanPham> SANPHAM { get; set; }
        public DbSet<DonHang> DONHANG { get; set; }
        public DbSet<ChiTietDonHangSanPham> CHITIETDONHANGSANPHAM { get; set; }
        public DbSet<SanPhamDacBiet> SANPHAMDACBIET { get; set; }
        public DbSet<HinhAnhQuangCao> HINHANHQUANGCAO { get; set; }
        public DbSet<DanhGia> DANHGIA { get; set; }
        public DbSet<BinhLuan> BINHLUAN { get; set; }
        public DbSet<VeGiamGia> VEGIAMGIA { get; set; }
        public DbSet<ChuDe> CHUDE {  get; set; }
        public DbSet<TinTuc> TINTUC {  get; set; }
        public DbSet<Wishlist> WISHLIST { get;set; }
        public DbSet<Address> ADDRESS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
   
            modelBuilder.Entity<HinhAnh>()
                .HasOne<SanPham>(s => s.SanPham)
                .WithMany(g => g.HinhAnh)
                .HasForeignKey(s => s.MaSanPham);

            modelBuilder.Entity<SanPham>()
               .HasOne<LoaiSanPham>(l => l.LoaiSanPham)
               .WithMany(s => s.SanPham)
               .HasForeignKey(s => s.MaLoaiSanPham);

            modelBuilder.Entity<SanPham>()
              .HasOne<ThuongHieu>(t => t.ThuongHieu)
              .WithMany(s => s.SanPham)
              .HasForeignKey(s => s.MaThuongHieu);

            modelBuilder.Entity<SanPham>()
                .HasOne<MauSac>(m => m.MauSac)
                .WithMany(s => s.SanPham)
                .HasForeignKey(s => s.MaMauSac);

            modelBuilder.Entity<SanPham>()
                .HasOne<BoNho>(m => m.BoNho)
                .WithMany(s => s.SanPham)
                .HasForeignKey(s => s.MaBoNho);

            modelBuilder.Entity<SanPham>()
                .HasOne<Ram>(m => m.Ram)
                .WithMany(s => s.SanPham)
                .HasForeignKey(s => s.MaRam);

            modelBuilder.Entity<SanPham>()
               .HasOne<SanPhamDacBiet>(l => l.SanPhamDacBiet)
               .WithMany(s => s.SanPham)
               .HasForeignKey(s => s.MaSanPhamDacBiet);

            modelBuilder.Entity<Wishlist>()
                .HasKey(sc => new { sc.ProductId, sc.UserId });
            modelBuilder.Entity<Wishlist>()
                .HasOne(sc => sc.SanPham)
                .WithMany(sc => sc.WishList)
                .HasForeignKey(sc => sc.ProductId);
            modelBuilder.Entity<Wishlist>()
                .HasOne(sc => sc.User)
                .WithMany(sc => sc.WishList)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<ChiTietDonHangSanPham>()
                .HasKey(sc => new { sc.MaSanPham, sc.MaDonHang });

            modelBuilder.Entity<ChiTietDonHangSanPham>()
                .HasOne(sc => sc.SanPham)
                .WithMany(s => s.ChiTietDonHangSanPham)
                .HasForeignKey(sc => sc.MaSanPham);

            modelBuilder.Entity<ChiTietDonHangSanPham>()
                .HasOne(sc => sc.DonHang)
                .WithMany(s => s.ChiTietDonHangSanPham)
                .HasForeignKey(sc => sc.MaDonHang);

            modelBuilder.Entity<Address>()
                .HasOne(sc => sc.User)
                .WithMany(sc => sc.Addresses)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<BinhLuan>()
                .HasKey(bl => new { bl.MaDanhGia, bl.Id, bl.MaSanPham });

            modelBuilder.Entity<BinhLuan>()
                .HasAlternateKey(bl => new { bl.Id, bl.MaSanPham });

            modelBuilder.Entity<BinhLuan>()
                .HasOne<DanhGia>(m => m.DanhGia)
                .WithMany(s => s.BinhLuan)
                .HasForeignKey(s => s.MaDanhGia);

            modelBuilder.Entity<BinhLuan>()
                .HasOne<AppUserModel>(m => m.Customer)
                .WithMany(s => s.BinhLuan)
                .HasForeignKey(s => s.Id);

            modelBuilder.Entity<BinhLuan>()
                .HasOne<SanPham>(m => m.SanPham)
                .WithMany(s => s.BinhLuan)
                .HasForeignKey(s => s.MaSanPham);

            modelBuilder.Entity<DonHang>()
                .HasOne<VeGiamGia>(m => m.VeGiamGia)
                .WithMany(s => s.DonHang)
                .HasForeignKey(s => s.MaVeGiamGia)
                    .IsRequired(false);

            modelBuilder.Entity<TinTuc>()
                .HasOne<ChuDe>(m => m.ChuDe)
                .WithMany(s => s.TinTuc)
                .HasForeignKey(s => s.MaChuDe);


            modelBuilder.Entity<SanPham>()
           .ToTable("SanPham")
           .HasDiscriminator<int>("SanPham")
           .HasValue<Iphone>(1)
           .HasValue<Ipad>(2)
           .HasValue<IMac>(3)
           .HasValue<Laptop>(4)
           .HasValue<SanPham>(0);

        }
    }
}
