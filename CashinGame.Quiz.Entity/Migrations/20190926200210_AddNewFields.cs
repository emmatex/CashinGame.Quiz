using Microsoft.EntityFrameworkCore.Migrations;

namespace CashinGame.Quiz.Entity.Migrations
{
    public partial class AddNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Category",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Category",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Category");
        }
    }
}
