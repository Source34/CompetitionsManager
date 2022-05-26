using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetitionManager.Data.Migrations
{
    public partial class _94 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_TaskResult_teams_teamTempId",
                table: "TaskResult");

            migrationBuilder.AddForeignKey(
                name: "fK_TaskResult_teams_teamTempId",
                table: "TaskResult",
                column: "teamId",
                principalTable: "Teams",
                principalColumn: "teamId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_TaskResult_teams_teamTempId",
                table: "TaskResult");

            migrationBuilder.AddForeignKey(
                name: "fK_TaskResult_teams_teamTempId",
                table: "TaskResult",
                column: "teamId",
                principalTable: "Teams",
                principalColumn: "teamId");
        }
    }
}
