using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTask2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskItemId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Effort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procents = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TaskItemId",
                table: "Projects",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_UserId",
                table: "TaskItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_TaskItems_TaskItemId",
                table: "Projects",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_TaskItems_TaskItemId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TaskItemId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "Projects");
        }
    }
}
