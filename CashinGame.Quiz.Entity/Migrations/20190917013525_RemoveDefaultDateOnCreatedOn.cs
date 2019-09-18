using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CashinGame.Quiz.Entity.Migrations
{
    public partial class RemoveDefaultDateOnCreatedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Question",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComputedColumnSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Option",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComputedColumnSql: "GetUtcDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Category",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComputedColumnSql: "GetUtcDate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Question",
                nullable: false,
                computedColumnSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Option",
                nullable: false,
                computedColumnSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Category",
                nullable: false,
                computedColumnSql: "GetUtcDate()",
                oldClrType: typeof(DateTime));
        }
    }
}
