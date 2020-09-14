using Microsoft.EntityFrameworkCore.Migrations;

namespace AgileTracker.TasksService.Infrastructure.Migrations
{
    public partial class ProjectGrouptoProjectRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectGroupMembers_ProjectGroups_ProjectGroupId",
                table: "ProjectGroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectGroups_ProjectGroupId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectGroupId",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectGroupId",
                table: "ProjectGroupMembers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectGroupMembers_ProjectGroups_ProjectGroupId",
                table: "ProjectGroupMembers",
                column: "ProjectGroupId",
                principalTable: "ProjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectGroups_ProjectGroupId",
                table: "Projects",
                column: "ProjectGroupId",
                principalTable: "ProjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectGroupMembers_ProjectGroups_ProjectGroupId",
                table: "ProjectGroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectGroups_ProjectGroupId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectGroupId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectGroupId",
                table: "ProjectGroupMembers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectGroupMembers_ProjectGroups_ProjectGroupId",
                table: "ProjectGroupMembers",
                column: "ProjectGroupId",
                principalTable: "ProjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectGroups_ProjectGroupId",
                table: "Projects",
                column: "ProjectGroupId",
                principalTable: "ProjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
