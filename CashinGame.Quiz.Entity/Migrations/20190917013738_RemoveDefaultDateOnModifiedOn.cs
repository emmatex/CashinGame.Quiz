using Microsoft.EntityFrameworkCore.Migrations;

namespace CashinGame.Quiz.Entity.Migrations
{
    public partial class RemoveDefaultDateOnModifiedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "Option",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "Option",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));
        }
    }
}
