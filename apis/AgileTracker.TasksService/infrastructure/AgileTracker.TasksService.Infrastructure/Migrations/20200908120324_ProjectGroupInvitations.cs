using Microsoft.EntityFrameworkCore.Migrations;

namespace AgileTracker.TasksService.Infrastructure.Migrations
{
    public partial class ProjectGroupInvitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectGroupInvitations",
                columns: table => new
                {
                    ProjectGroupId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvitedMemberId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectGroupInvitations", x => new { x.ProjectGroupId, x.Id });
                    table.ForeignKey(
                        name: "FK_ProjectGroupInvitations_ProjectGroups_ProjectGroupId",
                        column: x => x.ProjectGroupId,
                        principalTable: "ProjectGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectGroupInvitations");
        }
    }
}
