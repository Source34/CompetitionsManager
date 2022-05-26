using CompetitionManager.Data.Entities.TaskResults;
using CompetitionManager.Data.Entities.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompetitionManager.Data.Entities.Competitions
{
    public class CompetitionDbModel
    {
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public string Description { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
        public virtual List<Tasks.TaskDbModel>? Tasks { get; set; } = new();
        public virtual List<TeamDbModel>? Teams { get; set; } = new();
        public virtual List<TaskResultDbModel>? Results { get; set; } = new();
    }

    internal class Map : IEntityTypeConfiguration<CompetitionDbModel>
    {
        public void Configure(EntityTypeBuilder<CompetitionDbModel> builder)
        {
            builder.ToTable("Competition")
                .HasKey(p => p.CompetitionId)
                .HasName("pk_competition_id");

            builder.HasMany(p => p.Tasks)
                .WithMany(p => p.Competitions)
                .UsingEntity(j => j.ToTable("TasksCompetitions"));

            builder.HasMany(p => p.Results)
                .WithOne(p => p.Competition)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
