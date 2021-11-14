using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCodeDemo.Migrations
{
    public partial class AddTblHangHoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Loais",
                table: "Loais");

            migrationBuilder.RenameTable(
                name: "Loais",
                newName: "Loai");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loai",
                table: "Loai",
                column: "MaLoai");

            migrationBuilder.CreateTable(
                name: "HangHoa",
                columns: table => new
                {
                    MaHH = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHH = table.Column<string>(maxLength: 100, nullable: false),
                    DonGia = table.Column<double>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false, defaultValue: 0),
                    Hinh = table.Column<string>(nullable: true),
                    MaLoai = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoa", x => x.MaHH);
                    table.ForeignKey(
                        name: "FK_HangHoa_Loai",
                        column: x => x.MaLoai,
                        principalTable: "Loai",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loai_TenLoai",
                table: "Loai",
                column: "TenLoai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HangHoa_MaLoai",
                table: "HangHoa",
                column: "MaLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HangHoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loai",
                table: "Loai");

            migrationBuilder.DropIndex(
                name: "IX_Loai_TenLoai",
                table: "Loai");

            migrationBuilder.RenameTable(
                name: "Loai",
                newName: "Loais");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loais",
                table: "Loais",
                column: "MaLoai");
        }
    }
}
