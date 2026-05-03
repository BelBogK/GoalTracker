using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProjectAddingNewProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedTime",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ParentProjectId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectId",
                table: "Projects",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Projects_ProjectId",
                table: "Projects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Projects_ProjectId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CompletedTime",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ParentProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Projects");
        }
    }
}
