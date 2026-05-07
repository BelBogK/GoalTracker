using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class M2MProjectTaskitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_TaskItems_TaskItemId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TaskItemId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GoalScenario",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ProjectTaskItem",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    TaskItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTaskItem", x => new { x.ProjectsId, x.TaskItemsId });
                    table.ForeignKey(
                        name: "FK_ProjectTaskItem_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProjectTaskItem_TaskItems_TaskItemsId",
                        column: x => x.TaskItemsId,
                        principalTable: "TaskItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTaskItem_TaskItemsId",
                table: "ProjectTaskItem",
                column: "TaskItemsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTaskItem");

            migrationBuilder.AddColumn<int>(
                name: "TaskItemId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GoalScenario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TaskItemId",
                table: "Projects",
                column: "TaskItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_TaskItems_TaskItemId",
                table: "Projects",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id");
        }
    }
}
