using Microsoft.EntityFrameworkCore.Migrations;

namespace AgileTracker.StatisticsService.Infrastructure.Migrations
{
    public partial class ChangedTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskEstimation_ProjectGroupOwnership_ProjectGroupId",
                table: "TaskEstimation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskEstimation",
                table: "TaskEstimation");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProjectGroupOwnership_ExternalProjectGroupId",
                table: "ProjectGroupOwnership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectGroupOwnership",
                table: "ProjectGroupOwnership");

            migrationBuilder.RenameTable(
                name: "TaskEstimation",
                newName: "TaskEstimations");

            migrationBuilder.RenameTable(
                name: "ProjectGroupOwnership",
                newName: "ProjectGroupOwnerships");

            migrationBuilder.RenameIndex(
                name: "IX_TaskEstimation_ProjectGroupId",
                table: "TaskEstimations",
                newName: "IX_TaskEstimations_ProjectGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectGroupOwnership_ExternalProjectGroupId",
                table: "ProjectGroupOwnerships",
                newName: "IX_ProjectGroupOwnerships_ExternalProjectGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskEstimations",
                table: "TaskEstimations",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProjectGroupOwnerships_ExternalProjectGroupId",
                table: "ProjectGroupOwnerships",
                column: "ExternalProjectGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectGroupOwnerships",
                table: "ProjectGroupOwnerships",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEstimations_ProjectGroupOwnerships_ProjectGroupId",
                table: "TaskEstimations",
                column: "ProjectGroupId",
                principalTable: "ProjectGroupOwnerships",
                principalColumn: "ExternalProjectGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskEstimations_ProjectGroupOwnerships_ProjectGroupId",
                table: "TaskEstimations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskEstimations",
                table: "TaskEstimations");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProjectGroupOwnerships_ExternalProjectGroupId",
                table: "ProjectGroupOwnerships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectGroupOwnerships",
                table: "ProjectGroupOwnerships");

            migrationBuilder.RenameTable(
                name: "TaskEstimations",
                newName: "TaskEstimation");

            migrationBuilder.RenameTable(
                name: "ProjectGroupOwnerships",
                newName: "ProjectGroupOwnership");

            migrationBuilder.RenameIndex(
                name: "IX_TaskEstimations_ProjectGroupId",
                table: "TaskEstimation",
                newName: "IX_TaskEstimation_ProjectGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectGroupOwnerships_ExternalProjectGroupId",
                table: "ProjectGroupOwnership",
                newName: "IX_ProjectGroupOwnership_ExternalProjectGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskEstimation",
                table: "TaskEstimation",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProjectGroupOwnership_ExternalProjectGroupId",
                table: "ProjectGroupOwnership",
                column: "ExternalProjectGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectGroupOwnership",
                table: "ProjectGroupOwnership",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEstimation_ProjectGroupOwnership_ProjectGroupId",
                table: "TaskEstimation",
                column: "ProjectGroupId",
                principalTable: "ProjectGroupOwnership",
                principalColumn: "ExternalProjectGroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
