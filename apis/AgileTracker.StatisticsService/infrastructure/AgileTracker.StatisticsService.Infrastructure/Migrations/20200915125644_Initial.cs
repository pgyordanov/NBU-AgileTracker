using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgileTracker.StatisticsService.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectGroupOwnership",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalProjectGroupId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectGroupOwnership", x => x.Id);
                    table.UniqueConstraint("AK_ProjectGroupOwnership_ExternalProjectGroupId", x => x.ExternalProjectGroupId);
                });

            migrationBuilder.CreateTable(
                name: "TaskEstimation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(nullable: false),
                    ProjectGroupId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    StartedOn = table.Column<DateTime>(nullable: false),
                    EstimatedToFinishOn = table.Column<DateTime>(nullable: false),
                    ActuallyFinishedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEstimation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEstimation_ProjectGroupOwnership_ProjectGroupId",
                        column: x => x.ProjectGroupId,
                        principalTable: "ProjectGroupOwnership",
                        principalColumn: "ExternalProjectGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGroupOwnership_ExternalProjectGroupId",
                table: "ProjectGroupOwnership",
                column: "ExternalProjectGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskEstimation_ProjectGroupId",
                table: "TaskEstimation",
                column: "ProjectGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskEstimation");

            migrationBuilder.DropTable(
                name: "ProjectGroupOwnership");
        }
    }
}
