using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class TinTuc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CHUDE",
                columns: table => new
                {
                    MaChuDe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHUDE", x => x.MaChuDe);
                });

            migrationBuilder.CreateTable(
                name: "TINTUC",
                columns: table => new
                {
                    MaTinTuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaChuDe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TINTUC", x => x.MaTinTuc);
                    table.ForeignKey(
                        name: "FK_TINTUC_CHUDE_MaChuDe",
                        column: x => x.MaChuDe,
                        principalTable: "CHUDE",
                        principalColumn: "MaChuDe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TINTUC_MaChuDe",
                table: "TINTUC",
                column: "MaChuDe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TINTUC");

            migrationBuilder.DropTable(
                name: "CHUDE");
        }
    }
}
