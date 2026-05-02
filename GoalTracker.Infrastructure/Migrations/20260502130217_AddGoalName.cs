using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGoalName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalId",
                table: "GoalScenario");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Goals");

            migrationBuilder.AddColumn<int>(
                name: "GoalId",
                table: "GoalScenario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
