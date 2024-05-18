﻿// <auto-generated />
using System;
using DACS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DACS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DACS.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NumberAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ADDRESS");
                });

            modelBuilder.Entity("DACS.Models.AppUserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DACS.Models.BinhLuan", b =>
                {
                    b.Property<int>("MaDanhGia")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MaSanPham")
                        .HasColumnType("int");

                    b.Property<string>("NoiDungBinhLuan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ThoiGianBinhLuan")
                        .HasColumnType("datetime2");

                    b.HasKey("MaDanhGia", "Id", "MaSanPham");

                    b.HasAlternateKey("Id", "MaSanPham");

                    b.HasIndex("MaSanPham");

                    b.ToTable("BINHLUAN");
                });

            modelBuilder.Entity("DACS.Models.BoNho", b =>
                {
                    b.Property<int>("MaBoNho")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaBoNho"));

                    b.Property<string>("DungLuongBoNho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaBoNho");

                    b.ToTable("BONHO");
                });

            modelBuilder.Entity("DACS.Models.ChiTietDonHangSanPham", b =>
                {
                    b.Property<int>("MaSanPham")
                        .HasColumnType("int");

                    b.Property<int>("MaDonHang")
                        .HasColumnType("int");

                    b.Property<decimal>("DonGiaBan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SoluongMua")
                        .HasColumnType("int");

                    b.HasKey("MaSanPham", "MaDonHang");

                    b.HasIndex("MaDonHang");

                    b.ToTable("CHITIETDONHANGSANPHAM");
                });

            modelBuilder.Entity("DACS.Models.ChuDe", b =>
                {
                    b.Property<int>("MaChuDe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaChuDe"));

                    b.Property<string>("Mota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenChuDe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaChuDe");

                    b.ToTable("CHUDE");
                });

            modelBuilder.Entity("DACS.Models.DanhGia", b =>
                {
                    b.Property<int>("MaDanhGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDanhGia"));

                    b.Property<int>("DiemDanhGia")
                        .HasColumnType("int");

                    b.Property<string>("MoTaDanhGia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaDanhGia");

                    b.ToTable("DANHGIA");
                });

            modelBuilder.Entity("DACS.Models.DonHang", b =>
                {
                    b.Property<int>("MaDonHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDonHang"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaVeGiamGia")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayLapDonHang")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenKhachHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TongGiaTriDonHang")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TrangThaiDonHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThaiThanhToan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YeuCauKhac")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaDonHang");

                    b.HasIndex("MaVeGiamGia");

                    b.ToTable("DONHANG");
                });

            modelBuilder.Entity("DACS.Models.HinhAnh", b =>
                {
                    b.Property<int>("MaHinhAnh")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHinhAnh"));

                    b.Property<string>("FileHinhAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaSanPham")
                        .HasColumnType("int");

                    b.HasKey("MaHinhAnh");

                    b.HasIndex("MaSanPham");

                    b.ToTable("HINHANH");
                });

            modelBuilder.Entity("DACS.Models.HinhAnhQuangCao", b =>
                {
                    b.Property<int>("MaAnhQuangCao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaAnhQuangCao"));

                    b.Property<string>("FileAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaAnhQuangCao");

                    b.ToTable("HINHANHQUANGCAO");
                });

            modelBuilder.Entity("DACS.Models.LoaiSanPham", b =>
                {
                    b.Property<int>("MaLoaiSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLoaiSanPham"));

                    b.Property<string>("TenLoaiSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaLoaiSanPham");

                    b.ToTable("LOAISANPHAM");
                });

            modelBuilder.Entity("DACS.Models.MauSac", b =>
                {
                    b.Property<int>("MaMauSac")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaMauSac"));

                    b.Property<string>("TenMau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaMauSac");

                    b.ToTable("MAUSAC");
                });

            modelBuilder.Entity("DACS.Models.Ram", b =>
                {
                    b.Property<int>("MaRam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaRam"));

                    b.Property<string>("TenRam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaRam");

                    b.ToTable("RAM");
                });

            modelBuilder.Entity("DACS.Models.SanPham", b =>
                {
                    b.Property<int>("MaSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaSanPham"));

                    b.Property<float?>("DiemDanhGia")
                        .HasColumnType("real");

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("GiaKhuyenMai")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MaBoNho")
                        .HasColumnType("int");

                    b.Property<int>("MaLoaiSanPham")
                        .HasColumnType("int");

                    b.Property<int>("MaMauSac")
                        .HasColumnType("int");

                    b.Property<int>("MaRam")
                        .HasColumnType("int");

                    b.Property<int>("MaSanPhamDacBiet")
                        .HasColumnType("int");

                    b.Property<int>("MaThuongHieu")
                        .HasColumnType("int");

                    b.Property<string>("ManHinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SanPham")
                        .HasColumnType("int");

                    b.Property<int?>("SoLuongDanhGia")
                        .HasColumnType("int");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ThoiHanBaoHanh")
                        .HasColumnType("real");

                    b.HasKey("MaSanPham");

                    b.HasIndex("MaBoNho");

                    b.HasIndex("MaLoaiSanPham");

                    b.HasIndex("MaMauSac");

                    b.HasIndex("MaRam");

                    b.HasIndex("MaSanPhamDacBiet");

                    b.HasIndex("MaThuongHieu");

                    b.ToTable("SanPham", (string)null);

                    b.HasDiscriminator<int>("SanPham").HasValue(0);

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DACS.Models.SanPhamDacBiet", b =>
                {
                    b.Property<int>("MaSanPhamDacBiet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaSanPhamDacBiet"));

                    b.Property<string>("LoaiSanPhamDacBiet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaSanPhamDacBiet");

                    b.ToTable("SANPHAMDACBIET");
                });

            modelBuilder.Entity("DACS.Models.ThuongHieu", b =>
                {
                    b.Property<int>("MaThuongHieu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaThuongHieu"));

                    b.Property<string>("TenThuongHieu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaThuongHieu");

                    b.ToTable("THUONGHIEU");
                });

            modelBuilder.Entity("DACS.Models.TinTuc", b =>
                {
                    b.Property<int>("MaTinTuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTinTuc"));

                    b.Property<string>("HinhAnh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaChuDe")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ThoiGianDangBai")
                        .HasColumnType("datetime2");

                    b.Property<string>("TieuDe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TomTat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThaiBaiDang")
                        .HasColumnType("bit");

                    b.HasKey("MaTinTuc");

                    b.HasIndex("MaChuDe");

                    b.ToTable("TINTUC");
                });

            modelBuilder.Entity("DACS.Models.VeGiamGia", b =>
                {
                    b.Property<int>("MaVeGiamGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaVeGiamGia"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayThietLap")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuongToiDaSuDung")
                        .HasColumnType("int");

                    b.Property<float>("TyleGiam")
                        .HasColumnType("real");

                    b.HasKey("MaVeGiamGia");

                    b.ToTable("VEGIAMGIA");
                });

            modelBuilder.Entity("DACS.Models.Warranty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId", "OrderId");

                    b.ToTable("WARRANTY");
                });

            modelBuilder.Entity("DACS.Models.Wishlist", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("WISHLIST");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DACS.Models.IMac", b =>
                {
                    b.HasBaseType("DACS.Models.SanPham");

                    b.Property<string>("CongNgheCPU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TocDoCPU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Turbo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("DACS.Models.Ipad", b =>
                {
                    b.HasBaseType("DACS.Models.SanPham");

                    b.Property<string>("CongNgheManHinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoPhanGiai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KichThuocVatLy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("DACS.Models.Iphone", b =>
                {
                    b.HasBaseType("DACS.Models.SanPham");

                    b.Property<string>("CameraSau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CameraTruoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("DACS.Models.Laptop", b =>
                {
                    b.HasBaseType("DACS.Models.SanPham");

                    b.Property<string>("CPU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoNhanLuong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrongLuong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VGA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("DACS.Models.Address", b =>
                {
                    b.HasOne("DACS.Models.AppUserModel", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DACS.Models.BinhLuan", b =>
                {
                    b.HasOne("DACS.Models.AppUserModel", "Customer")
                        .WithMany("BinhLuan")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DACS.Models.DanhGia", "DanhGia")
                        .WithMany("BinhLuan")
                        .HasForeignKey("MaDanhGia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DACS.Models.SanPham", "SanPham")
                        .WithMany("BinhLuan")
                        .HasForeignKey("MaSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("DanhGia");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DACS.Models.ChiTietDonHangSanPham", b =>
                {
                    b.HasOne("DACS.Models.DonHang", "DonHang")
                        .WithMany("ChiTietDonHangSanPham")
                        .HasForeignKey("MaDonHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DACS.Models.SanPham", "SanPham")
                        .WithMany("ChiTietDonHangSanPham")
                        .HasForeignKey("MaSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DACS.Models.DonHang", b =>
                {
                    b.HasOne("DACS.Models.VeGiamGia", "VeGiamGia")
                        .WithMany("DonHang")
                        .HasForeignKey("MaVeGiamGia");

                    b.Navigation("VeGiamGia");
                });

            modelBuilder.Entity("DACS.Models.HinhAnh", b =>
                {
                    b.HasOne("DACS.Models.SanPham", "SanPham")
                        .WithMany("HinhAnh")
                        .HasForeignKey("MaSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DACS.Models.SanPham", b =>
                {
                    b.HasOne("DACS.Models.BoNho", "BoNho")
                        .WithMany("SanPham")
                        .HasForeignKey("MaBoNho")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DACS.Models.LoaiSanPham", "LoaiSanPham")
                        .WithMany("SanPham")
                        .HasForeignKey("MaLoaiSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DACS.Models.MauSac", "MauSac")
                        .WithMany("SanPham")
                        .HasForeignKey("MaMauSac")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DACS.Models.Ram", "Ram")
                        .WithMany("SanPham")
                        .HasForeignKey("MaRam")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DACS.Models.SanPhamDacBiet", "SanPhamDacBiet")
                        .WithMany("SanPham")
                        .HasForeignKey("MaSanPhamDacBiet")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DACS.Models.ThuongHieu", "ThuongHieu")
                        .WithMany("SanPham")
                        .HasForeignKey("MaThuongHieu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoNho");

                    b.Navigation("LoaiSanPham");

                    b.Navigation("MauSac");

                    b.Navigation("Ram");

                    b.Navigation("SanPhamDacBiet");

                    b.Navigation("ThuongHieu");
                });

            modelBuilder.Entity("DACS.Models.TinTuc", b =>
                {
                    b.HasOne("DACS.Models.ChuDe", "ChuDe")
                        .WithMany("TinTuc")
                        .HasForeignKey("MaChuDe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChuDe");
                });

            modelBuilder.Entity("DACS.Models.Warranty", b =>
                {
                    b.HasOne("DACS.Models.ChiTietDonHangSanPham", "OrderDetails")
                        .WithMany("Warranties")
                        .HasForeignKey("ProductId", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("DACS.Models.Wishlist", b =>
                {
                    b.HasOne("DACS.Models.SanPham", "SanPham")
                        .WithMany("WishList")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DACS.Models.AppUserModel", "User")
                        .WithMany("WishList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DACS.Models.AppUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DACS.Models.AppUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DACS.Models.AppUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DACS.Models.AppUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DACS.Models.AppUserModel", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("BinhLuan");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("DACS.Models.BoNho", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DACS.Models.ChiTietDonHangSanPham", b =>
                {
                    b.Navigation("Warranties");
                });

            modelBuilder.Entity("DACS.Models.ChuDe", b =>
                {
                    b.Navigation("TinTuc");
                });

            modelBuilder.Entity("DACS.Models.DanhGia", b =>
                {
                    b.Navigation("BinhLuan");
                });

            modelBuilder.Entity("DACS.Models.DonHang", b =>
                {
                    b.Navigation("ChiTietDonHangSanPham");
                });

            modelBuilder.Entity("DACS.Models.LoaiSanPham", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DACS.Models.MauSac", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DACS.Models.Ram", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DACS.Models.SanPham", b =>
                {
                    b.Navigation("BinhLuan");

                    b.Navigation("ChiTietDonHangSanPham");

                    b.Navigation("HinhAnh");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("DACS.Models.SanPhamDacBiet", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DACS.Models.ThuongHieu", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("DACS.Models.VeGiamGia", b =>
                {
                    b.Navigation("DonHang");
                });
#pragma warning restore 612, 618
        }
    }
}
