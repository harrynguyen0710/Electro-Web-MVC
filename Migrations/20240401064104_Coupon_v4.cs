using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class Coupon_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DONHANG_VEGIAMGIA_MaVeGiamGia",
                table: "DONHANG");

            migrationBuilder.AddForeignKey(
                name: "FK_DONHANG_VEGIAMGIA_MaVeGiamGia",
                table: "DONHANG",
                column: "MaVeGiamGia",
                principalTable: "VEGIAMGIA",
                principalColumn: "MaVeGiamGia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DONHANG_VEGIAMGIA_MaVeGiamGia",
                table: "DONHANG");

            migrationBuilder.AddForeignKey(
                name: "FK_DONHANG_VEGIAMGIA_MaVeGiamGia",
                table: "DONHANG",
                column: "MaVeGiamGia",
                principalTable: "VEGIAMGIA",
                principalColumn: "MaVeGiamGia",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
