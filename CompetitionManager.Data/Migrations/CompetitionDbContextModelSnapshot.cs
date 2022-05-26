﻿// <auto-generated />
using System;
using CompetitionManager.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompetitionManager.Data.Migrations
{
    [DbContext(typeof(CompetitionDbContext))]
    partial class CompetitionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CompetitionDbModelTaskDbModel", b =>
                {
                    b.Property<int>("CompetitionsCompetitionId")
                        .HasColumnType("int")
                        .HasColumnName("competitionsCompetitionId");

                    b.Property<int>("TasksTaskId")
                        .HasColumnType("int")
                        .HasColumnName("tasksTaskId");

                    b.HasKey("CompetitionsCompetitionId", "TasksTaskId")
                        .HasName("pK_TasksCompetitions");

                    b.HasIndex("TasksTaskId")
                        .HasDatabaseName("iX_TasksCompetitions_tasksTaskId");

                    b.ToTable("TasksCompetitions", (string)null);
                });

            modelBuilder.Entity("CompetitionDbModelTeamDbModel", b =>
                {
                    b.Property<int>("CompetitionsCompetitionId")
                        .HasColumnType("int")
                        .HasColumnName("competitionsCompetitionId");

                    b.Property<int>("TeamsTeamId")
                        .HasColumnType("int")
                        .HasColumnName("teamsTeamId");

                    b.HasKey("CompetitionsCompetitionId", "TeamsTeamId")
                        .HasName("pK_TeamsCompetitions");

                    b.HasIndex("TeamsTeamId")
                        .HasDatabaseName("iX_TeamsCompetitions_teamsTeamId");

                    b.ToTable("TeamsCompetitions", (string)null);
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Coaches.CoachDbModel", b =>
                {
                    b.Property<int>("CoachId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("coachId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CoachId"), 1L, 1);

                    b.Property<string>("AcademicDegree")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("academicDegree");

                    b.Property<string>("CodeforcesLogin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("codeforcesLogin");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("firstName");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("patronymic");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("surname");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("university");

                    b.HasKey("CoachId")
                        .HasName("pk_coach_id");

                    b.ToTable("Coaches", (string)null);
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Competitions.CompetitionDbModel", b =>
                {
                    b.Property<int>("CompetitionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("competitionId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompetitionId"), 1L, 1);

                    b.Property<string>("CompetitionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("competitionName");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<DateTime>("EndEventDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("endEventDate");

                    b.Property<DateTime>("StartEventDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("startEventDate");

                    b.HasKey("CompetitionId")
                        .HasName("pk_competition_id");

                    b.ToTable("Competition", (string)null);
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.TaskResults.TaskResultDbModel", b =>
                {
                    b.Property<int>("TaskResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("taskResultId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskResultId"), 1L, 1);

                    b.Property<int?>("CompetitionId")
                        .HasColumnType("int")
                        .HasColumnName("competitionId");

                    b.Property<double>("ElapsedMinutes")
                        .HasColumnType("float")
                        .HasColumnName("elapsedMinutes");

                    b.Property<double>("Result")
                        .HasColumnType("float")
                        .HasColumnName("result");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int")
                        .HasColumnName("taskId");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int")
                        .HasColumnName("teamId");

                    b.HasKey("TaskResultId")
                        .HasName("pk_taskResult_id");

                    b.HasIndex("CompetitionId")
                        .HasDatabaseName("iX_TaskResult_competitionId");

                    b.HasIndex("TaskId")
                        .HasDatabaseName("iX_TaskResult_taskId");

                    b.HasIndex("TeamId")
                        .HasDatabaseName("iX_TaskResult_teamId");

                    b.ToTable("TaskResult", (string)null);
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Tasks.TaskDbModel", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("taskId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"), 1L, 1);

                    b.Property<string>("InputExample")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("inputExample");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("OutputExample")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("outputExample");

                    b.Property<double>("Points")
                        .HasColumnType("float")
                        .HasColumnName("points");

                    b.Property<string>("Solution")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("solution");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("text");

                    b.HasKey("TaskId")
                        .HasName("pk_task_id");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Teams.TeamDbModel", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("teamId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamId"), 1L, 1);

                    b.Property<int?>("CoachId")
                        .HasColumnType("int")
                        .HasColumnName("coachId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int?>("TeamLeadId")
                        .HasColumnType("int")
                        .HasColumnName("teamLeadId");

                    b.HasKey("TeamId")
                        .HasName("pk_team_id");

                    b.HasIndex("CoachId")
                        .HasDatabaseName("iX_Teams_coachId");

                    b.HasIndex("TeamLeadId")
                        .HasDatabaseName("iX_Teams_teamLeadId");

                    b.ToTable("Teams", (string)null);
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Users.UserDbModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("CodeforcesLogin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("codeforcesLogin");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("firstName");

                    b.Property<bool>("FromVstu")
                        .HasColumnType("bit")
                        .HasColumnName("fromVstu");

                    b.Property<string>("GradebookNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("gradebookNumber");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("patronymic");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("surname");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("university");

                    b.HasKey("UserId")
                        .HasName("pk_user_id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("TeamDbModelUserDbModel", b =>
                {
                    b.Property<int>("TeamMatesUserId")
                        .HasColumnType("int")
                        .HasColumnName("teamMatesUserId");

                    b.Property<int>("TeamsMembershipsTeamId")
                        .HasColumnType("int")
                        .HasColumnName("teamsMembershipsTeamId");

                    b.HasKey("TeamMatesUserId", "TeamsMembershipsTeamId")
                        .HasName("pK_TeamsUsers");

                    b.HasIndex("TeamsMembershipsTeamId")
                        .HasDatabaseName("iX_TeamsUsers_teamsMembershipsTeamId");

                    b.ToTable("TeamsUsers", (string)null);
                });

            modelBuilder.Entity("CompetitionDbModelTaskDbModel", b =>
                {
                    b.HasOne("CompetitionManager.Data.Entities.Competitions.CompetitionDbModel", null)
                        .WithMany()
                        .HasForeignKey("CompetitionsCompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_TasksCompetitions_Competition_competitionsCompetitionId");

                    b.HasOne("CompetitionManager.Data.Entities.Tasks.TaskDbModel", null)
                        .WithMany()
                        .HasForeignKey("TasksTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_TasksCompetitions_tasks_tasksTempId1");
                });

            modelBuilder.Entity("CompetitionDbModelTeamDbModel", b =>
                {
                    b.HasOne("CompetitionManager.Data.Entities.Competitions.CompetitionDbModel", null)
                        .WithMany()
                        .HasForeignKey("CompetitionsCompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_TeamsCompetitions_Competition_competitionsCompetitionId");

                    b.HasOne("CompetitionManager.Data.Entities.Teams.TeamDbModel", null)
                        .WithMany()
                        .HasForeignKey("TeamsTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_TeamsCompetitions_Teams_teamsTeamId");
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.TaskResults.TaskResultDbModel", b =>
                {
                    b.HasOne("CompetitionManager.Data.Entities.Competitions.CompetitionDbModel", "Competition")
                        .WithMany("Results")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fK_TaskResult_Competition_competitionId");

                    b.HasOne("CompetitionManager.Data.Entities.Tasks.TaskDbModel", "Task")
                        .WithMany("TaskResults")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fK_TaskResult_tasks_taskTempId");

                    b.HasOne("CompetitionManager.Data.Entities.Teams.TeamDbModel", "Team")
                        .WithMany("Results")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fK_TaskResult_teams_teamTempId");

                    b.Navigation("Competition");

                    b.Navigation("Task");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Teams.TeamDbModel", b =>
                {
                    b.HasOne("CompetitionManager.Data.Entities.Coaches.CoachDbModel", "Coach")
                        .WithMany("TrainingTeams")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fK_Teams_Coaches_coachId");

                    b.HasOne("CompetitionManager.Data.Entities.Users.UserDbModel", "TeamLead")
                        .WithMany("LeadedTeams")
                        .HasForeignKey("TeamLeadId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fK_Teams_users_teamLeadId");

                    b.Navigation("Coach");

                    b.Navigation("TeamLead");
                });

            modelBuilder.Entity("TeamDbModelUserDbModel", b =>
                {
                    b.HasOne("CompetitionManager.Data.Entities.Users.UserDbModel", null)
                        .WithMany()
                        .HasForeignKey("TeamMatesUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_TeamsUsers_users_teamMatesTempId1");

                    b.HasOne("CompetitionManager.Data.Entities.Teams.TeamDbModel", null)
                        .WithMany()
                        .HasForeignKey("TeamsMembershipsTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_TeamsUsers_Teams_teamsMembershipsTeamId");
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Coaches.CoachDbModel", b =>
                {
                    b.Navigation("TrainingTeams");
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Competitions.CompetitionDbModel", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Tasks.TaskDbModel", b =>
                {
                    b.Navigation("TaskResults");
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Teams.TeamDbModel", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("CompetitionManager.Data.Entities.Users.UserDbModel", b =>
                {
                    b.Navigation("LeadedTeams");
                });
#pragma warning restore 612, 618
        }
    }
}
