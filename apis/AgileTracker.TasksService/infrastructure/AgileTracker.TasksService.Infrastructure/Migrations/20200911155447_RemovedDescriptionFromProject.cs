using Microsoft.EntityFrameworkCore.Migrations;

namespace AgileTracker.TasksService.Infrastructure.Migrations
{
    public partial class RemovedDescriptionFromProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
