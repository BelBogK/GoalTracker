using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGoalScenarioRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoalScenarioId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GoalScenarioGoalScenario",
                columns: table => new
                {
                    ChildGoalScenariosId = table.Column<int>(type: "int", nullable: false),
                    ParentGoalScenariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalScenarioGoalScenario", x => new { x.ChildGoalScenariosId, x.ParentGoalScenariosId });
                    table.ForeignKey(
                        name: "FK_GoalScenarioGoalScenario_GoalScenario_ChildGoalScenariosId",
                        column: x => x.ChildGoalScenariosId,
                        principalTable: "GoalScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_GoalScenarioGoalScenario_GoalScenario_ParentGoalScenariosId",
                        column: x => x.ParentGoalScenariosId,
                        principalTable: "GoalScenario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GoalScenarioRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentGoalScenarioId = table.Column<int>(type: "int", nullable: false),
                    GoalScenarioParentId = table.Column<int>(type: "int", nullable: false),
                    GoalScenarioChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalScenarioRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoalScenarioRelations_GoalScenario_CurrentGoalScenarioId",
                        column: x => x.CurrentGoalScenarioId,
                        principalTable: "GoalScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_GoalScenarioRelations_GoalScenario_GoalScenarioChildId",
                        column: x => x.GoalScenarioChildId,
                        principalTable: "GoalScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_GoalScenarioRelations_GoalScenario_GoalScenarioParentId",
                        column: x => x.GoalScenarioParentId,
                        principalTable: "GoalScenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_GoalScenarioId",
                table: "Projects",
                column: "GoalScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalScenarioGoalScenario_ParentGoalScenariosId",
                table: "GoalScenarioGoalScenario",
                column: "ParentGoalScenariosId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalScenarioRelations_CurrentGoalScenarioId",
                table: "GoalScenarioRelations",
                column: "CurrentGoalScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalScenarioRelations_GoalScenarioChildId",
                table: "GoalScenarioRelations",
                column: "GoalScenarioChildId");

            migrationBuilder.CreateIndex(
                name: "IX_GoalScenarioRelations_GoalScenarioParentId",
                table: "GoalScenarioRelations",
                column: "GoalScenarioParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_GoalScenario_GoalScenarioId",
                table: "Projects",
                column: "GoalScenarioId",
                principalTable: "GoalScenario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_GoalScenario_GoalScenarioId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "GoalScenarioGoalScenario");

            migrationBuilder.DropTable(
                name: "GoalScenarioRelations");

            migrationBuilder.DropIndex(
                name: "IX_Projects_GoalScenarioId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "GoalScenarioId",
                table: "Projects");
        }
    }
}
