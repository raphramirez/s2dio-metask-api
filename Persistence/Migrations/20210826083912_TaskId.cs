using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class TaskId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedById",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById1",
                table: "Tasks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedById1",
                table: "Tasks",
                column: "CreatedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedById1",
                table: "Tasks",
                column: "CreatedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedById1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatedById1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "AspNetUsers");

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
    }
}
