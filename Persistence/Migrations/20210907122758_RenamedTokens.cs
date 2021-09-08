using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RenamedTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationToken_AspNetUsers_AppUserId",
                table: "NotificationToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationToken",
                table: "NotificationToken");

            migrationBuilder.RenameTable(
                name: "NotificationToken",
                newName: "NotificationTokens");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationToken_AppUserId",
                table: "NotificationTokens",
                newName: "IX_NotificationTokens_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTokens",
                table: "NotificationTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationTokens_AspNetUsers_AppUserId",
                table: "NotificationTokens",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationTokens_AspNetUsers_AppUserId",
                table: "NotificationTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTokens",
                table: "NotificationTokens");

            migrationBuilder.RenameTable(
                name: "NotificationTokens",
                newName: "NotificationToken");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTokens_AppUserId",
                table: "NotificationToken",
                newName: "IX_NotificationToken_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationToken",
                table: "NotificationToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationToken_AspNetUsers_AppUserId",
                table: "NotificationToken",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
