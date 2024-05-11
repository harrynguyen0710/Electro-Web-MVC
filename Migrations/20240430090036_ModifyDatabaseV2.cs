using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDatabaseV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DONHANG_TRANGTHAIDONHANG_MaTrangThaiDonHang",
                table: "DONHANG");

            migrationBuilder.DropForeignKey(
                name: "FK_DONHANG_TRANGTHAITHANHTOAN_MaTrangThaiThanhToan",
                table: "DONHANG");

            migrationBuilder.DropTable(
                name: "TRANGTHAIDONHANG");

            migrationBuilder.DropTable(
                name: "TRANGTHAITHANHTOAN");

            migrationBuilder.DropIndex(
                name: "IX_DONHANG_MaTrangThaiDonHang",
                table: "DONHANG");

            migrationBuilder.DropIndex(
                name: "IX_DONHANG_MaTrangThaiThanhToan",
                table: "DONHANG");

            migrationBuilder.RenameColumn(
                name: "MaTrangThaiThanhToan",
                table: "DONHANG",
                newName: "TrangThaiThanhToan");

            migrationBuilder.RenameColumn(
                name: "MaTrangThaiDonHang",
                table: "DONHANG",
                newName: "TrangThaiDonHang");

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianDangBai",
                table: "TINTUC",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "TrangThaiBaiDang",
                table: "TINTUC",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThoiGianDangBai",
                table: "TINTUC");

            migrationBuilder.DropColumn(
                name: "TrangThaiBaiDang",
                table: "TINTUC");

            migrationBuilder.RenameColumn(
                name: "TrangThaiThanhToan",
                table: "DONHANG",
                newName: "MaTrangThaiThanhToan");

            migrationBuilder.RenameColumn(
                name: "TrangThaiDonHang",
                table: "DONHANG",
                newName: "MaTrangThaiDonHang");

            migrationBuilder.CreateTable(
                name: "TRANGTHAIDONHANG",
                columns: table => new
                {
                    MaTrangThaiDonHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThaiDonHang = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANGTHAIDONHANG", x => x.MaTrangThaiDonHang);
                });

            migrationBuilder.CreateTable(
                name: "TRANGTHAITHANHTOAN",
                columns: table => new
                {
                    MaTrangThaiThanhToan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThaiThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANGTHAITHANHTOAN", x => x.MaTrangThaiThanhToan);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_MaTrangThaiDonHang",
                table: "DONHANG",
                column: "MaTrangThaiDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_MaTrangThaiThanhToan",
                table: "DONHANG",
                column: "MaTrangThaiThanhToan");

            migrationBuilder.AddForeignKey(
                name: "FK_DONHANG_TRANGTHAIDONHANG_MaTrangThaiDonHang",
                table: "DONHANG",
                column: "MaTrangThaiDonHang",
                principalTable: "TRANGTHAIDONHANG",
                principalColumn: "MaTrangThaiDonHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DONHANG_TRANGTHAITHANHTOAN_MaTrangThaiThanhToan",
                table: "DONHANG",
                column: "MaTrangThaiThanhToan",
                principalTable: "TRANGTHAITHANHTOAN",
                principalColumn: "MaTrangThaiThanhToan",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
