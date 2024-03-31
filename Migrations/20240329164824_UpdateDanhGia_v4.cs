using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDanhGia_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BINHLUAN_Id",
                table: "BINHLUAN");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BINHLUAN_Id_MaSanPham",
                table: "BINHLUAN",
                columns: new[] { "Id", "MaSanPham" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_BINHLUAN_Id_MaSanPham",
                table: "BINHLUAN");

            migrationBuilder.CreateIndex(
                name: "IX_BINHLUAN_Id",
                table: "BINHLUAN",
                column: "Id");
        }
    }
}
