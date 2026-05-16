using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExecutionImprovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionImprovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutionImprovements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoreImproveds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartImprove = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsImproved = table.Column<bool>(type: "bit", nullable: true),
                    TrackedEntitiesType = table.Column<int>(type: "int", nullable: false),
                    EntityeId = table.Column<int>(type: "int", nullable: false),
                    ExecutionImprovementId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoreImproveds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoreImproveds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoreImproveds_ExecutionImprovements_ExecutionImprovementId",
                        column: x => x.ExecutionImprovementId,
                        principalTable: "ExecutionImprovements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionImprovements_UserId",
                table: "ExecutionImprovements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoreImproveds_ExecutionImprovementId",
                table: "HistoreImproveds",
                column: "ExecutionImprovementId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoreImproveds_UserId",
                table: "HistoreImproveds",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoreImproveds");

            migrationBuilder.DropTable(
                name: "ExecutionImprovements");
        }
    }
}
