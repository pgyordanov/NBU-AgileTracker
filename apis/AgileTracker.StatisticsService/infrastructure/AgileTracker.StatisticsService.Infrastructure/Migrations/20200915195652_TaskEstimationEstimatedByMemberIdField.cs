using Microsoft.EntityFrameworkCore.Migrations;

namespace AgileTracker.StatisticsService.Infrastructure.Migrations
{
    public partial class TaskEstimationEstimatedByMemberIdField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EstimatedByMemberId",
                table: "TaskEstimations",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedByMemberId",
                table: "TaskEstimations");
        }
    }
}
