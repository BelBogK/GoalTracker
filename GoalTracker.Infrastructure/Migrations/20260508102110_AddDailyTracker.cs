using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyTracker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Constraints",
                table: "GoalScenario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpertFlow",
                table: "GoalScenario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOprimisticScenarios",
                table: "GoalScenario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PotentialProblems",
                table: "GoalScenario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SuccessProbability",
                table: "GoalScenario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DailyTrackers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodayIs = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskItemId = table.Column<int>(type: "int", nullable: false),
                    StatusInBegining = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTrackers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyTrackers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DailyTrackers_TaskItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTrackers_TaskItemId",
                table: "DailyTrackers",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyTrackers_UserId",
                table: "DailyTrackers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTrackers");

            migrationBuilder.DropColumn(
                name: "Constraints",
                table: "GoalScenario");

            migrationBuilder.DropColumn(
                name: "ExpertFlow",
                table: "GoalScenario");

            migrationBuilder.DropColumn(
                name: "IsOprimisticScenarios",
                table: "GoalScenario");

            migrationBuilder.DropColumn(
                name: "PotentialProblems",
                table: "GoalScenario");

            migrationBuilder.DropColumn(
                name: "SuccessProbability",
                table: "GoalScenario");
        }
    }
}
