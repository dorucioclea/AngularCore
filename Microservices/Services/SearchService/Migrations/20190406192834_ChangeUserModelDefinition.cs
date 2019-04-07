using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchService.Migrations
{
    public partial class ChangeUserModelDefinition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Name");
        }
    }
}
