using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class Coupon_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaVeGiamGia",
                table: "DONHANG",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_MaVeGiamGia",
                table: "DONHANG",
                column: "MaVeGiamGia");

            migrationBuilder.AddForeignKey(
                name: "FK_DONHANG_VEGIAMGIA_MaVeGiamGia",
                table: "DONHANG",
                column: "MaVeGiamGia",
                principalTable: "VEGIAMGIA",
                principalColumn: "MaVeGiamGia",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DONHANG_VEGIAMGIA_MaVeGiamGia",
                table: "DONHANG");

            migrationBuilder.DropIndex(
                name: "IX_DONHANG_MaVeGiamGia",
                table: "DONHANG");

            migrationBuilder.DropColumn(
                name: "MaVeGiamGia",
                table: "DONHANG");
        }
    }
}
