using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class ModifyWarranty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WARRANTY_SanPham_ProductId",
                table: "WARRANTY");

            migrationBuilder.DropIndex(
                name: "IX_WARRANTY_ProductId",
                table: "WARRANTY");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "WARRANTY",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WARRANTY_ProductId_OrderId",
                table: "WARRANTY",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WARRANTY_CHITIETDONHANGSANPHAM_ProductId_OrderId",
                table: "WARRANTY",
                columns: new[] { "ProductId", "OrderId" },
                principalTable: "CHITIETDONHANGSANPHAM",
                principalColumns: new[] { "MaSanPham", "MaDonHang" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WARRANTY_CHITIETDONHANGSANPHAM_ProductId_OrderId",
                table: "WARRANTY");

            migrationBuilder.DropIndex(
                name: "IX_WARRANTY_ProductId_OrderId",
                table: "WARRANTY");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "WARRANTY");

            migrationBuilder.CreateIndex(
                name: "IX_WARRANTY_ProductId",
                table: "WARRANTY",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WARRANTY_SanPham_ProductId",
                table: "WARRANTY",
                column: "ProductId",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
