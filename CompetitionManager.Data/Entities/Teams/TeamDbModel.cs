using Microsoft.EntityFrameworkCore;
using CompetitionManager.Core.Domains.Users;
using CompetitionManager.Core.Domains.Coaches;
using CompetitionManager.Data.Entities.Coaches;
using CompetitionManager.Data.Entities.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CompetitionManager.Data.Entities.TaskResults;
using CompetitionManager.Data.Entities.Competitions;

namespace CompetitionManager.Data.Entities.Teams
{
    public class TeamDbModel
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public int? TeamLeadId { get; set; }
        public UserDbModel? TeamLead { get; set; }
        public int? CoachId { get; set; }
        public CoachDbModel? Coach { get; set; }
        public virtual List<UserDbModel>? TeamMates { get; set; } = new();

        public List<TaskResultDbModel>? Results { get; set; } = new();
        public List<CompetitionDbModel>? Competitions { get; set; } = new();
    }

    internal class Map : IEntityTypeConfiguration<TeamDbModel>
    {
        public void Configure(EntityTypeBuilder<TeamDbModel> builder)
        {
            builder.ToTable("Teams")
                .HasKey(p => p.TeamId)
                .HasName("pk_team_id");

            builder.HasOne(p => p.TeamLead)
                .WithMany(p => p.LeadedTeams)
                .HasForeignKey(p => p.TeamLeadId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Coach)
                .WithMany(p => p.TrainingTeams)
                .HasForeignKey(p => p.CoachId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.TeamMates)
                .WithMany(p => p.TeamsMemberships)
                .UsingEntity(j => j.ToTable("TeamsUsers"));

            builder.HasMany(p => p.Results)
                .WithOne(p => p.Team)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Competitions)
                .WithMany(p => p.Teams)
                .UsingEntity(j => j.ToTable("TeamsCompetitions"));
        }
    }
}
