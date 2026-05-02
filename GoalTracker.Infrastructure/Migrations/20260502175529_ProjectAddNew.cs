using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProjectAddNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentStatus",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoalType",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "GoalType",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Projects");
        }
    }
}
