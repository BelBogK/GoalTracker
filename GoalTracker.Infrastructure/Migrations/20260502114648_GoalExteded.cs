using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GoalExteded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalScenario_Goals_GoalId",
                table: "GoalScenario");

            migrationBuilder.DropIndex(
                name: "IX_GoalScenario_GoalId",
                table: "GoalScenario");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStatus",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoalType",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Goals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Reward",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Goals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetDate",
                table: "Goals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GoalGoalScenario",
                columns: table => new
                {
                    GoalsId = table.Column<int>(type: "int", nullable: false),
                    ScenariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalGoalScenario", x => new { x.GoalsId, x.ScenariosId });
                    table.ForeignKey(
                        name: "FK_GoalGoalScenario_GoalScenario_ScenariosId",
                        column: x => x.ScenariosId,
                        principalTable: "GoalScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalGoalScenario_Goals_GoalsId",
                        column: x => x.GoalsId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalProject",
                columns: table => new
                {
                    GoalsId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalProject", x => new { x.GoalsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_GoalProject_Goals_GoalsId",
                        column: x => x.GoalsId,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalGoalScenario_ScenariosId",
                table: "GoalGoalScenario",
                column: "ScenariosId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalProject_ProjectsId",
                table: "GoalProject",
                column: "ProjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalGoalScenario");

            migrationBuilder.DropTable(
                name: "GoalProject");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "GoalType",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Reward",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "TargetDate",
                table: "Goals");

            migrationBuilder.CreateIndex(
                name: "IX_GoalScenario_GoalId",
                table: "GoalScenario",
                column: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScenario_Goals_GoalId",
                table: "GoalScenario",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
