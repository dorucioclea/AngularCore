using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PostService.Migrations
{
    public partial class IncludeWallOwnerInPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "WallOwnerId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Posts_WallOwnerId",
                table: "Posts",
                column: "WallOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_WallOwnerId",
                table: "Posts",
                column: "WallOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_WallOwnerId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_WallOwnerId",
                table: "Posts");

            migrationBuilder.AlterColumn<Guid>(
                name: "WallOwnerId",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
