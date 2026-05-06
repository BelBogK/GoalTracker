using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
        name: "FK_GoalScenario_AspNetUsers_UserId",
        table: "GoalScenario");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_ChildId",
                table: "GoalScenarioRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_ParentId",
                table: "GoalScenarioRelations");

            migrationBuilder.DropTable(
                name: "GoalScenarioGoalScenario");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScenario_AspNetUsers_UserId",
                table: "GoalScenario",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_ChildId",
                table: "GoalScenarioRelations",
                column: "ChildId",
                principalTable: "GoalScenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_ParentId",
                table: "GoalScenarioRelations",
                column: "ParentId",
                principalTable: "GoalScenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalScenario_AspNetUsers_UserId",
                table: "GoalScenario");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_ChildId",
                table: "GoalScenarioRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_ParentId",
                table: "GoalScenarioRelations");

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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalScenarioGoalScenario_GoalScenario_ParentGoalScenariosId",
                        column: x => x.ParentGoalScenariosId,
                        principalTable: "GoalScenario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalScenarioGoalScenario_ParentGoalScenariosId",
                table: "GoalScenarioGoalScenario",
                column: "ParentGoalScenariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScenario_AspNetUsers_UserId",
                table: "GoalScenario",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_ChildId",
                table: "GoalScenarioRelations",
                column: "ChildId",
                principalTable: "GoalScenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_ParentId",
                table: "GoalScenarioRelations",
                column: "ParentId",
                principalTable: "GoalScenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
