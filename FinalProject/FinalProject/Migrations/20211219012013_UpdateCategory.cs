using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class UpdateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                table: "Category",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_ParentCategory",
                table: "Category",
                column: "ParentCategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_ParentCategory",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "Category");
        }
    }
}
