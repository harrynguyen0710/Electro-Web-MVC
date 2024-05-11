using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VEGIAMGIA_TYLEGIAM_MaTyLeGiam",
                table: "VEGIAMGIA");

            migrationBuilder.DropTable(
                name: "TYLEGIAM");

            migrationBuilder.DropIndex(
                name: "IX_VEGIAMGIA_MaTyLeGiam",
                table: "VEGIAMGIA");

            migrationBuilder.DropColumn(
                name: "MaTyLeGiam",
                table: "VEGIAMGIA");

            migrationBuilder.AddColumn<float>(
                name: "TyleGiam",
                table: "VEGIAMGIA",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "TomTat",
                table: "TINTUC",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "MaVeGiamGia",
                table: "DONHANG",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TyleGiam",
                table: "VEGIAMGIA");

            migrationBuilder.DropColumn(
                name: "TomTat",
                table: "TINTUC");

            migrationBuilder.AddColumn<int>(
                name: "MaTyLeGiam",
                table: "VEGIAMGIA",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MaVeGiamGia",
                table: "DONHANG",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_VEGIAMGIA_MaTyLeGiam",
                table: "VEGIAMGIA",
                column: "MaTyLeGiam");

            migrationBuilder.AddForeignKey(
                name: "FK_VEGIAMGIA_TYLEGIAM_MaTyLeGiam",
                table: "VEGIAMGIA",
                column: "MaTyLeGiam",
                principalTable: "TYLEGIAM",
                principalColumn: "MaTyLeGiam",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
