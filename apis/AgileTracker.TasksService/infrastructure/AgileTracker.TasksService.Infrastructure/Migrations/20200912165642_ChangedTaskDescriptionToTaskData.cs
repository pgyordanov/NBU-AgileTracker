using Microsoft.EntityFrameworkCore.Migrations;

namespace AgileTracker.TasksService.Infrastructure.Migrations
{
    public partial class ChangedTaskDescriptionToTaskData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description_Title",
                table: "Tasks",
                newName: "Data_Title");

            migrationBuilder.RenameColumn(
                name: "Description_PointsEstimate",
                table: "Tasks",
                newName: "Data_PointsEstimate");

            migrationBuilder.RenameColumn(
                name: "Description_Description",
                table: "Tasks",
                newName: "Data_Description");

            migrationBuilder.RenameColumn(
                name: "Description_AssignedToMemberId",
                table: "Tasks",
                newName: "Data_AssignedToMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data_Title",
                table: "Tasks",
                newName: "Description_Title");

            migrationBuilder.RenameColumn(
                name: "Data_PointsEstimate",
                table: "Tasks",
                newName: "Description_PointsEstimate");

            migrationBuilder.RenameColumn(
                name: "Data_Description",
                table: "Tasks",
                newName: "Description_Description");

            migrationBuilder.RenameColumn(
                name: "Data_AssignedToMemberId",
                table: "Tasks",
                newName: "Description_AssignedToMemberId");
        }
    }
}
