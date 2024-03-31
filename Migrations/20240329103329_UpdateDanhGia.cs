using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDanhGia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BINHLUAN",
                table: "BINHLUAN");

            migrationBuilder.DropColumn(
                name: "MaNguoiDung",
                table: "BINHLUAN");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "BINHLUAN",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BINHLUAN",
                table: "BINHLUAN",
                columns: new[] { "MaDanhGia", "Id", "MaSanPham" });

            migrationBuilder.CreateIndex(
                name: "IX_BINHLUAN_Id",
                table: "BINHLUAN",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BINHLUAN_MaSanPham",
                table: "BINHLUAN",
                column: "MaSanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_BINHLUAN_AspNetUsers_Id",
                table: "BINHLUAN",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BINHLUAN_DANHGIA_MaDanhGia",
                table: "BINHLUAN",
                column: "MaDanhGia",
                principalTable: "DANHGIA",
                principalColumn: "MaDanhGia",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BINHLUAN_SanPham_MaSanPham",
                table: "BINHLUAN",
                column: "MaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BINHLUAN_AspNetUsers_Id",
                table: "BINHLUAN");

            migrationBuilder.DropForeignKey(
                name: "FK_BINHLUAN_DANHGIA_MaDanhGia",
                table: "BINHLUAN");

            migrationBuilder.DropForeignKey(
                name: "FK_BINHLUAN_SanPham_MaSanPham",
                table: "BINHLUAN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BINHLUAN",
                table: "BINHLUAN");

            migrationBuilder.DropIndex(
                name: "IX_BINHLUAN_Id",
                table: "BINHLUAN");

            migrationBuilder.DropIndex(
                name: "IX_BINHLUAN_MaSanPham",
                table: "BINHLUAN");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BINHLUAN");

            migrationBuilder.AddColumn<int>(
                name: "MaNguoiDung",
                table: "BINHLUAN",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BINHLUAN",
                table: "BINHLUAN",
                columns: new[] { "MaDanhGia", "MaNguoiDung", "MaSanPham" });
        }
    }
}
