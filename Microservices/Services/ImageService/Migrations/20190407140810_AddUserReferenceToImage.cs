using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageService.Migrations
{
    public partial class AddUserReferenceToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ProfilePictureUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AuthorId",
                table: "Images",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Users_AuthorId",
                table: "Images",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Users_AuthorId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Images_AuthorId",
                table: "Images");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Images",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
