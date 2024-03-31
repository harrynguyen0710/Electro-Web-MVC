using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class DanhGiaSanPham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "DiemDanhGia",
                table: "SanPham",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TongGiaTriDonHang",
                table: "DONHANG",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DonGiaBan",
                table: "CHITIETDONHANGSANPHAM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BINHLUAN",
                columns: table => new
                {
                    MaDanhGia = table.Column<int>(type: "int", nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false),
                    MaSanPham = table.Column<int>(type: "int", nullable: false),
                    NoiDungBinhLuan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BINHLUAN", x => new { x.MaDanhGia, x.MaNguoiDung, x.MaSanPham });
                });

            migrationBuilder.CreateTable(
                name: "DANHGIA",
                columns: table => new
                {
                    MaDanhGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoTaDanhGia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiemDanhGia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DANHGIA", x => x.MaDanhGia);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BINHLUAN");

            migrationBuilder.DropTable(
                name: "DANHGIA");

            migrationBuilder.DropColumn(
                name: "DiemDanhGia",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "TongGiaTriDonHang",
                table: "DONHANG");

            migrationBuilder.DropColumn(
                name: "DonGiaBan",
                table: "CHITIETDONHANGSANPHAM");
        }
    }
}
