using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetitionManager.Data.Migrations
{
    public partial class _93 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    coachId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codeforcesLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    academicDegree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    university = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coach_id", x => x.coachId);
                });

            migrationBuilder.CreateTable(
                name: "Competition",
                columns: table => new
                {
                    competitionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    competitionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startEventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endEventDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_competition_id", x => x.competitionId);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    taskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    solution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    inputExample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    outputExample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    points = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_id", x => x.taskId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codeforcesLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    university = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fromVstu = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_id", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "TasksCompetitions",
                columns: table => new
                {
                    competitionsCompetitionId = table.Column<int>(type: "int", nullable: false),
                    tasksTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_TasksCompetitions", x => new { x.competitionsCompetitionId, x.tasksTaskId });
                    table.ForeignKey(
                        name: "fK_TasksCompetitions_Competition_competitionsCompetitionId",
                        column: x => x.competitionsCompetitionId,
                        principalTable: "Competition",
                        principalColumn: "competitionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_TasksCompetitions_tasks_tasksTempId1",
                        column: x => x.tasksTaskId,
                        principalTable: "Task",
                        principalColumn: "taskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    teamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    teamLeadId = table.Column<int>(type: "int", nullable: true),
                    coachId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_team_id", x => x.teamId);
                    table.ForeignKey(
                        name: "fK_Teams_Coaches_coachId",
                        column: x => x.coachId,
                        principalTable: "Coaches",
                        principalColumn: "coachId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fK_Teams_users_teamLeadId",
                        column: x => x.teamLeadId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TaskResult",
                columns: table => new
                {
                    taskResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    competitionId = table.Column<int>(type: "int", nullable: true),
                    teamId = table.Column<int>(type: "int", nullable: true),
                    taskId = table.Column<int>(type: "int", nullable: true),
                    elapsedMinutes = table.Column<double>(type: "float", nullable: false),
                    result = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_taskResult_id", x => x.taskResultId);
                    table.ForeignKey(
                        name: "fK_TaskResult_Competition_competitionId",
                        column: x => x.competitionId,
                        principalTable: "Competition",
                        principalColumn: "competitionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fK_TaskResult_tasks_taskTempId",
                        column: x => x.taskId,
                        principalTable: "Task",
                        principalColumn: "taskId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fK_TaskResult_teams_teamTempId",
                        column: x => x.teamId,
                        principalTable: "Teams",
                        principalColumn: "teamId");
                });

            migrationBuilder.CreateTable(
                name: "TeamsCompetitions",
                columns: table => new
                {
                    competitionsCompetitionId = table.Column<int>(type: "int", nullable: false),
                    teamsTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_TeamsCompetitions", x => new { x.competitionsCompetitionId, x.teamsTeamId });
                    table.ForeignKey(
                        name: "fK_TeamsCompetitions_Competition_competitionsCompetitionId",
                        column: x => x.competitionsCompetitionId,
                        principalTable: "Competition",
                        principalColumn: "competitionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_TeamsCompetitions_Teams_teamsTeamId",
                        column: x => x.teamsTeamId,
                        principalTable: "Teams",
                        principalColumn: "teamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamsUsers",
                columns: table => new
                {
                    teamMatesUserId = table.Column<int>(type: "int", nullable: false),
                    teamsMembershipsTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_TeamsUsers", x => new { x.teamMatesUserId, x.teamsMembershipsTeamId });
                    table.ForeignKey(
                        name: "fK_TeamsUsers_Teams_teamsMembershipsTeamId",
                        column: x => x.teamsMembershipsTeamId,
                        principalTable: "Teams",
                        principalColumn: "teamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_TeamsUsers_users_teamMatesTempId1",
                        column: x => x.teamMatesUserId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "iX_TaskResult_competitionId",
                table: "TaskResult",
                column: "competitionId");

            migrationBuilder.CreateIndex(
                name: "iX_TaskResult_taskId",
                table: "TaskResult",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "iX_TaskResult_teamId",
                table: "TaskResult",
                column: "teamId");

            migrationBuilder.CreateIndex(
                name: "iX_TasksCompetitions_tasksTaskId",
                table: "TasksCompetitions",
                column: "tasksTaskId");

            migrationBuilder.CreateIndex(
                name: "iX_Teams_coachId",
                table: "Teams",
                column: "coachId");

            migrationBuilder.CreateIndex(
                name: "iX_Teams_teamLeadId",
                table: "Teams",
                column: "teamLeadId");

            migrationBuilder.CreateIndex(
                name: "iX_TeamsCompetitions_teamsTeamId",
                table: "TeamsCompetitions",
                column: "teamsTeamId");

            migrationBuilder.CreateIndex(
                name: "iX_TeamsUsers_teamsMembershipsTeamId",
                table: "TeamsUsers",
                column: "teamsMembershipsTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskResult");

            migrationBuilder.DropTable(
                name: "TasksCompetitions");

            migrationBuilder.DropTable(
                name: "TeamsCompetitions");

            migrationBuilder.DropTable(
                name: "TeamsUsers");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Competition");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
