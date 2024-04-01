using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class Coupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TYLEGIAM",
                columns: table => new
                {
                    MaTyLeGiam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoTaTyLeGiam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYLEGIAM", x => x.MaTyLeGiam);
                });

            migrationBuilder.CreateTable(
                name: "VEGIAMGIA",
                columns: table => new
                {
                    MaVeGiamGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThietLap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuongToiDaSuDung = table.Column<int>(type: "int", nullable: false),
                    MaTyLeGiam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEGIAMGIA", x => x.MaVeGiamGia);
                    table.ForeignKey(
                        name: "FK_VEGIAMGIA_TYLEGIAM_MaTyLeGiam",
                        column: x => x.MaTyLeGiam,
                        principalTable: "TYLEGIAM",
                        principalColumn: "MaTyLeGiam",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VEGIAMGIA_MaTyLeGiam",
                table: "VEGIAMGIA",
                column: "MaTyLeGiam");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VEGIAMGIA");

            migrationBuilder.DropTable(
                name: "TYLEGIAM");
        }
    }
}
