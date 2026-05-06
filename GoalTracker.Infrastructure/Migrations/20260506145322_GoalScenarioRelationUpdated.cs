using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GoalScenarioRelationUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_CurrentGoalScenarioId",
                table: "GoalScenarioRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_GoalScenarioChildId",
                table: "GoalScenarioRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_GoalScenarioParentId",
                table: "GoalScenarioRelations");

            migrationBuilder.DropIndex(
                name: "IX_GoalScenarioRelations_CurrentGoalScenarioId",
                table: "GoalScenarioRelations");

            migrationBuilder.DropColumn(
                name: "CurrentGoalScenarioId",
                table: "GoalScenarioRelations");

            migrationBuilder.RenameColumn(
                name: "GoalScenarioParentId",
                table: "GoalScenarioRelations",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "GoalScenarioChildId",
                table: "GoalScenarioRelations",
                newName: "ChildId");

            migrationBuilder.RenameIndex(
                name: "IX_GoalScenarioRelations_GoalScenarioParentId",
                table: "GoalScenarioRelations",
                newName: "IX_GoalScenarioRelations_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_GoalScenarioRelations_GoalScenarioChildId",
                table: "GoalScenarioRelations",
                newName: "IX_GoalScenarioRelations_ChildId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "GoalScenario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "GoalScenario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GoalScenario",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_GoalScenario_UserId",
                table: "GoalScenario",
                column: "UserId");

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

            migrationBuilder.DropIndex(
                name: "IX_GoalScenario_UserId",
                table: "GoalScenario");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "GoalScenario");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "GoalScenario");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GoalScenario");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "GoalScenarioRelations",
                newName: "GoalScenarioParentId");

            migrationBuilder.RenameColumn(
                name: "ChildId",
                table: "GoalScenarioRelations",
                newName: "GoalScenarioChildId");

            migrationBuilder.RenameIndex(
                name: "IX_GoalScenarioRelations_ParentId",
                table: "GoalScenarioRelations",
                newName: "IX_GoalScenarioRelations_GoalScenarioParentId");

            migrationBuilder.RenameIndex(
                name: "IX_GoalScenarioRelations_ChildId",
                table: "GoalScenarioRelations",
                newName: "IX_GoalScenarioRelations_GoalScenarioChildId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentGoalScenarioId",
                table: "GoalScenarioRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GoalScenarioRelations_CurrentGoalScenarioId",
                table: "GoalScenarioRelations",
                column: "CurrentGoalScenarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_CurrentGoalScenarioId",
                table: "GoalScenarioRelations",
                column: "CurrentGoalScenarioId",
                principalTable: "GoalScenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_GoalScenarioChildId",
                table: "GoalScenarioRelations",
                column: "GoalScenarioChildId",
                principalTable: "GoalScenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalScenarioRelations_GoalScenario_GoalScenarioParentId",
                table: "GoalScenarioRelations",
                column: "GoalScenarioParentId",
                principalTable: "GoalScenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
