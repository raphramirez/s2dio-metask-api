using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Tasks",
                newName: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedById",
                table: "Tasks",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedById",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Tasks",
                newName: "CreatedBy");
        }
    }
}
