using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgileTracker.TasksService.Infrastructure.Migrations
{
    public partial class DomainExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ProjectGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectGroups_ProjectGroupId",
                        column: x => x.ProjectGroupId,
                        principalTable: "ProjectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurationWeeks = table.Column<int>(nullable: false),
                    StartedOn = table.Column<DateTime>(nullable: false),
                    FinishedOn = table.Column<DateTime>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sprints_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sprints_TaskStatuses",
                columns: table => new
                {
                    SprintId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    IsEnd = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints_TaskStatuses", x => new { x.SprintId, x.Id });
                    table.ForeignKey(
                        name: "FK_Sprints_TaskStatuses_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description_Title = table.Column<string>(nullable: true),
                    Description_Description = table.Column<string>(nullable: true),
                    Description_PointsEstimate = table.Column<int>(nullable: true),
                    Description_AssignedToMemberId = table.Column<string>(nullable: true),
                    Status_Title = table.Column<string>(nullable: true),
                    Status_IsEnd = table.Column<bool>(nullable: true),
                    StartedOn = table.Column<DateTime>(nullable: false),
                    FinishedOn = table.Column<DateTime>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true),
                    SprintId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectGroupId",
                table: "Projects",
                column: "ProjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_ProjectId",
                table: "Sprints",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SprintId",
                table: "Tasks",
                column: "SprintId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sprints_TaskStatuses");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
